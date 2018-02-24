using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spotlightgrabber
{
    class Program
    {
        static void Main(string[] args)
        {
            bool herp = false;

            string root = "";

            if (args.Length == 0)
            {
                root = GetRootPath();
            } else
            {
                herp = true;
                if (Directory.Exists(args[0]))
                {
                    root = args[0];
                }
            }

            Console.WriteLine("root is {0}", root);
            

            int imgCount = 0;

            string[] fileEntries = Directory.GetFiles(Environment.ExpandEnvironmentVariables(@"%localappdata%\Packages\Microsoft.Windows.ContentDeliveryManager_cw5n1h2txyewy\LocalState\Assets"));

            foreach (string file in fileEntries)
            {
                Bitmap img = new Bitmap(file);

                if (img.Width == 1920)
                {
                    string filePath = Path.Combine(root, Path.GetFileName(file)) + ".jpg";

                    if (!File.Exists(filePath))
                    {
                        if (!Directory.Exists(root) && imgCount == 0)
                        {
                            Directory.CreateDirectory(root);
                        }

                        File.Copy(file, filePath);
                        imgCount++;
                    }
                }
            }
            Console.WriteLine("Found {0} images! Press any key to exit program.", imgCount);

            if (!herp)
            {
                Console.ReadKey();
                Process.Start(root);
            }
        }
        private static string GetRootPath()
        {
            string[] config = File.ReadAllLines("config.txt");
            string rootLine = config[0];

            string rootPathSubstr = "root=";
            string rootPath = rootLine.Substring(rootPathSubstr.IndexOf(rootPathSubstr) + rootPathSubstr.Length);

            if (rootPath == @"%USERPROFILE%\Desktop\Spotlight Images")
            {
                rootPath = Environment.ExpandEnvironmentVariables(rootPath);
            }

            return rootPath;
        }

    }
}
