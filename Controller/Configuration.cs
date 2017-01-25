using EM3.Windows;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;

namespace EM3.Controller
{
    public class Configuration
    {
        public static string application = "em3";
        public static string server = "localhost";
        public static int port = 8081;
        public static int standard_company = 0;
        public static int nav_mode = 0; //0 = Tabs; 1 = Windows;
        public static bool quiet_mode = false;
        public static int licence_mode = 1; //1 - producao; 0 - homologacao;

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
                    if (line.StartsWith("licence_mode"))
                    {
                        string value = line.Replace("licence_mode=", "");
                        licence_mode = int.Parse(value);
                        continue;
                    }

                    if (line.StartsWith("interface"))
                    {
                        string value = line.Replace("interface=", "");
                        nav_mode = (value.Equals("windows") ? 1 : 0);
                        continue;
                    }

                    if (line.StartsWith("quiet"))
                    {
                        string value = line.Replace("quiet=", "");
                        quiet_mode = value.Equals("enabled");
                        continue;
                    }

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

                    if (line.StartsWith("standard_company"))
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
            catch (Exception ex)
            {
                if (reader != null) reader.Close();
            }

            return false;
        }

        static bool has_startedSetup = false;

        public static void Setup()
        {
            if (has_startedSetup) return;
            has_startedSetup = true;

            RequestHelper rh = new RequestHelper();
            rh.Send("getdb_type");

            string file = (Directory.GetCurrentDirectory() + @"\Files\Tables\" + rh.Result.message) + ".sql";
            int max = Encoding.Default.GetString(File.ReadAllBytes(file)).Split(';').Length;

            WaitWindow ww = new WaitWindow();
            ww.txTitulo.Text = "Preparando base de dados";
            ww.Progresso.Maximum = (double)max;
            ww.Show();

            new Thread(() =>
            {
                ww.Dispatcher.Invoke(new Action<WaitWindow>(w => ww.txSubTitulo.Text = "Teste"), ww);
                StreamReader reader = new StreamReader(file);
                string line = "";

                while ((line += reader.ReadLine()) != "/*EOF*/")
                {
                    if (string.IsNullOrEmpty(line)) continue;

                    if (line.StartsWith("/*") && line.EndsWith("*/"))
                    {
                        string tabela = "criando tabela " + (line.Replace("/*", "").Replace("*/", "").TrimEnd().TrimStart());
                        ww.Dispatcher.Invoke(new Action<WaitWindow>(w => ww.txSubTitulo.Text = tabela), ww);
                        line = line.Replace(line, "");
                        continue;
                    }
                    if (line.EndsWith(";"))
                    {
                        if (SendQuery(line))
                        {
                            line = "";
                            ww.Dispatcher.Invoke(new Action<WaitWindow>(w => ww.Progresso.Incresses(1)), ww);
                        }
                        else
                            System.Environment.Exit(0);
                    }
                }
                ww.Dispatcher.Invoke(new Action<WaitWindow>(w => ww.Close()), ww);
                EmpresasController.CriarEmpresaTeste();
                MessageBox.Show("A criação das tabelas foi concluida. \nO sistema deve será encerrado para concluir outras alterações. \nAguarde alguns segundos e inicie novamente.", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                Environment.Exit(0);
            }).Start();
        }

        private static bool SendQuery(string query)
        {
            query = query.Replace("%", "");
            query = query.Replace("&", "");

            RequestHelper rh = new RequestHelper();
            rh.AddParameter("query", query);
            rh.Send("ps_execute");

            if(rh.Result.status != 600)
            {
                MessageBox.Show("Erro durante a execução do escript\n\n " + query, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }
    }
}
