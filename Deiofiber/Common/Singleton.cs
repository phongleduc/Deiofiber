using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Deiofiber.Common
{
    public class Singleton
    {
        private static readonly string _singletonFile = "batch." + DateTime.Today.ToString("yyyyMMdd") + ".sg";
        public static void CreateSingletonFile()
        {
            string fileName = HttpContext.Current.Server.MapPath("~/") + "/logs/" + _singletonFile;
            // Create a file to write to. 
            using (StreamWriter sw = File.CreateText(fileName))
            {
                sw.WriteLine("Singleton File.");
            }
        }

        public static bool IsRunningBatch()
        {
            string fileName = HttpContext.Current.Server.MapPath("~/") + "/logs/" + _singletonFile;
            return File.Exists(fileName);
        }

        public static void DeleteSingletonFile()
        {
            string fileName = HttpContext.Current.Server.MapPath("~/") + "/logs/" + _singletonFile;
            File.Delete(fileName);
        }
    }
}