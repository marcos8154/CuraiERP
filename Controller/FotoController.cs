using EM3.Windows;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace EM3.Controller
{
    public class FotoController
    {
        public static int Save(string fileName, int id)
        {
            try
            {
                WebClient client = new WebClient();
                byte[] result = client.UploadFile(Configuration.GetApplication + "img-save?id=" + id, fileName);
                string response = Encoding.ASCII.GetString(result);
                OperationResult opRes = JsonConvert.DeserializeObject<OperationResult>(response);
                return int.Parse(opRes.entity.ToString());
            }
            catch (Exception ex)
            {
                new MsgAlerta(ex.Message);
            }
            return 0;
        }

        public static string GetFile(string name, int id)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    RequestHelper rh = new RequestHelper();
                    rh.AddParameter("id", id);
                    rh.AddParameter("nome", name);
                    rh.Send("img-get");
                    if (rh.HasSuccess)
                    {
                        if (File.Exists(@"c:\temp\" + name + id + ".jpg")) File.Delete(@"c:\temp\" + name + id + ".jpg");
                        string imgPathServer = rh.Result.entity.ToString();
                        string url = Configuration.GetApplication + imgPathServer;
                        client.DownloadFile(new Uri(url), @"c:\temp\" + name  + id + ".jpg");
                        return @"c:\temp\" + name + id + ".jpg";
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return string.Empty;
        }
    }
}
