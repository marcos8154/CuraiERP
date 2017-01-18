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
                    if (!string.IsNullOrEmpty(Result.message))
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
                MessageBox.Show(@"O NetLauncher não conseguiu se recuperar de uma falha na comunicação cliente-servidor e precisará ser encerrado.

Processos afetados:
 * Curae Série V

Possíveis Causas:
 * Falha de rede
 * O servidor demorou muito tempo para responder devido a sobrecargas no sistema
 * O banco de dados pode estar indisponível (servico parado)
 * O Application Server pode estar indisponível (servico parado)
 * Rede configurada de forma inadequada
 * Ocorreu uma falha de energia e a estação servidor foi desligada

Solução rápida:
* Verifique suas configurações de rede e inicie o NetLauncher novamente.
* Reinicie a estação servidor (em caso de falha geral)
* Acione o suporte Doware. 

TOP-CLIENT error -467", "Comunicação interrompida", MessageBoxButton.OK, MessageBoxImage.Error);
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
                parameters += paramName + "=" + paramValue.ToString().Replace(",", ".");
            else
                parameters += paramName + "=" + paramValue.ToString().Replace("&", "%26");
        }
    }
}
