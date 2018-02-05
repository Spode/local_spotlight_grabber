using System;
using System.Collections.Generic;
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
            
            string root = GetRootPath();
            bool foundImg = false;

            string[] fileEntries = Directory.GetFiles(Environment.ExpandEnvironmentVariables(@"%localappdata%\Packages\Microsoft.Windows.ContentDeliveryManager_cw5n1h2txyewy\LocalState\Assets"));

            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }

            foreach (string file in fileEntries)
            {
                Bitmap img = new Bitmap(file);

                if (img.Width == 1920)
                {                                                            
                    string filePath = Path.Combine(root,Path.GetFileName(file)) + ".jpg";

                    if (!File.Exists(filePath))
                    {
                        File.Copy(file, filePath);
                        Console.WriteLine("Found landscape img " + Path.GetFileName(file) + ". Saving.");
                        foundImg = true;
                    }                    
                }
            }

            if (foundImg)
            {
                Console.WriteLine("Found images! Press any key to exit program.");
            } else
            {
                Console.WriteLine("Did not find any new images! Press any key to exit program.");
            }

            
            Console.ReadKey();
        }
        private static string GetRootPath()
        {           
            string[] config = File.ReadAllLines("config.txt");
            string rootLine = config[0];

            string rootPathSubstr = "root=";
            string rootPath = rootLine.Substring(rootPathSubstr.IndexOf(rootPathSubstr) + rootPathSubstr.Length);

            if (rootPath == "%USERPROFILE%\\Desktop\\Spotlight Images")
            {
                rootPath = Environment.ExpandEnvironmentVariables(rootPath);
                Console.WriteLine(rootPath);
            }

            return rootPath;
        }

    }
}
