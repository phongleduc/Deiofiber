using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace Deiofiber.Common
{
    public class Logger
    {
        public static void Log(string content)
        {

            //set up a filestream
            using (FileStream fs = new FileStream(WebConfigurationManager.AppSettings["Deiofiber.LogFolder"] + string.Format("log_{0}.txt", DateTime.Now.ToString("yyyyMMdd")), FileMode.OpenOrCreate, FileAccess.Write))
            {
                //set up a streamwriter for adding text
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    //find the end of the underlying filestream
                    sw.BaseStream.Seek(0, SeekOrigin.End);

                    //add the text
                    sw.WriteLine(DateTime.Now + ": " + content);
                }
            }
        }
    }
}