using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace KFCBot.src.MeTelegramBot
{
    public static class FileWorker
    {        
        public static List<string> ReadFrom(string path)
        {            
            if(path == null)
            {
                Console.WriteLine("Path has to be not empty!");
                return null;
            }
            if (!File.Exists(path))
            {
                Console.WriteLine("File doesn't exist!");
                return null;
            }
            using (StreamReader sr = new StreamReader(path))
            {
                string s = "";
                List<string> result = new List<string>();
                while ((s = sr.ReadLine()) != null)
                {
                    try
                    {
                        result.Add(s);
                        Console.WriteLine("Done!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Somthing went wrong while I was trying send message to all memebers in the chat!");
                        Console.WriteLine(ex);
                        return null;
                    }
                }
                return result;
            }
        }

        public static bool IsFileExist(string path)
        {
            return File.Exists(path);
        }

        public static bool CreateFile(string name)
        {
            if(name == null)
            {
                Console.WriteLine("The name can't be empty!");
                return false;
            }
            Console.WriteLine($"Creating file {name}");
            try
            {
                using (StreamWriter sw = File.CreateText(name))
                {
                    Console.WriteLine($"File {name} has been created!");
                }
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }            
        }

        public static bool WriteFile(string name, string line)
        {
            if (!IsFileExist(name)) 
            {
                CreateFile(name);
            }
            Console.WriteLine("File already exist! Just adding new user id in it!");
            try
            {
                using (StreamWriter sw = File.AppendText(name))
                {
                    sw.WriteLine(line);
                }
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
    }
}
