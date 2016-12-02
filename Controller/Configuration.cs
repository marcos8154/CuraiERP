using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EM3.Controller
{
    public class Configuration
    {
        public static string application = "em3";
        public static string server = "localhost";
        public static int port = 8081;
        public static int standard_company = 0;
       
        public static string GetApplication
        {
            get
            {
                return ("http://" + server + ":" + port + "/" + application + "/");                
            }
        }

        public static bool LoadFromLocalSettings()
        {
            StreamReader reader = null;
            try
            {
                string localFile = Directory.GetCurrentDirectory() + @"\Files\ConfigFiles\NetLauncher.dwconf";
                if (!File.Exists(localFile))
                    return false;

                reader = new StreamReader(localFile);
                string line = "";

                while ((line = reader.ReadLine()) != null)
                {

                    if (line.StartsWith("appserver_port"))
                    {
                        port = int.Parse(line.Replace("appserver_port=", ""));
                        continue;
                    }

                    if (line.StartsWith("appserver"))
                    {
                        server = line.Replace("appserver=", "");
                        continue;
                    }
                    
                    if (line.StartsWith("default_application"))
                    {
                        application = line.Replace("default_application=", "");
                        continue;
                    }

                    if(line.StartsWith("standard_company"))
                    {
                        standard_company = int.Parse(line.Replace("standard_company=", ""));
                        continue;
                    }

                    if (line.StartsWith("licenceserver_port"))
                    {
                        LicenceController.port = int.Parse(line.Replace("licenceserver_port=", ""));
                        continue;
                    }

                    if (line.StartsWith("licenceserver"))
                    {
                        LicenceController.server = line.Replace("licenceserver=", "");
                        continue;
                    }
                    
                }

                reader.Close();
                return true;
            }
            catch(Exception ex)
            {
                if (reader != null) reader.Close();
            }

            return false;
        }
    }
}
