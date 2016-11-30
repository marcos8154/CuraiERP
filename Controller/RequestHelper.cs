using EM3.Windows;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows;

namespace EM3.Controller
{
    public class RequestHelper
    {
        private string parameters = string.Empty;
        public bool isHandled = false;

        public OperationResult Result { get; set; }

        public bool HasSuccess
        {
            get
            {
                if (Result.status == 600)
                    return true;
                else
                {
                    if (isHandled)
                        return false;
                    new MsgAlerta(Result.message);
                }
                return false;
            }
        }

        public string Send(string controller)
        {
            if (isHandled)
                return string.Empty;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Configuration.GetApplication + controller);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";

                if (!string.IsNullOrEmpty(parameters))
                {
                    byte[] b = Encoding.UTF8.GetBytes(parameters);
                    request.ContentLength = b.Length;

                    Stream stream = request.GetRequestStream();
                    stream.Write(b, 0, b.Length);
                    stream.Close();
                }

                WebResponse response = request.GetResponse();
                Stream data = response.GetResponseStream();
                StreamReader reader = new StreamReader(data);
                string result = reader.ReadToEnd();

                reader.Close();
                response.Close();

                Result = JsonConvert.DeserializeObject<OperationResult>(result);
                return string.IsNullOrEmpty(result) ? string.Empty : result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("O sistema não conseguiu se recuperar de uma falha na comunicação cliente-servidor e precisará ser encerrado. \nVerifique as configurações de rede ou acione o suporte Doware. \nTOP-CLIENT error -467", "Comunicação interrompida", MessageBoxButton.OK, MessageBoxImage.Error);
                System.Environment.Exit(0);
            }

            return string.Empty;
        }

        public void AddParameter(string paramName, object paramValue)
        {
            if (paramValue == null)
                paramValue = string.Empty;

            if (paramValue.ToString().Contains("%"))
            {
                isHandled = true;
                new MsgAlerta("Caracteres inválidos encontrados (" + paramName + " = %)");
                Result = new OperationResult() { status = 100, message = string.Empty, entity = new object() };
                return;
            }
            if (!string.IsNullOrEmpty(parameters))
                parameters += "&";

            double doubleValue = 0;

            if (double.TryParse(paramValue.ToString(), out doubleValue))
            {
                parameters += paramName + "=" + paramValue.ToString().Replace(",", ".");
            }
            else
                parameters += paramName + "=" + paramValue.ToString().Replace("&", "%26");
        }
    }
}
