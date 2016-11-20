using DBX.Entities;
using DBX_VisualClient.SERVICE;
using DBXConnector;
using EM3.Windows;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace EM3.Controller
{
    public class LicenceController
    {
        public static bool Connect()
        {
            DBXConnection conn = new DBXConnection();
            conn.Configure("localhost", 14449, string.Empty, "LICENCEDB");

            return ClientService.CONNECTED;
        }

        public static bool RegisterUser(int id)
        {
            Usuarios usuario = UsuariosController.Find(id);
            string json = JsonConvert.SerializeObject(usuario);

            DBXCommand cmd = new DBXCommand();
            cmd.Execute("RU " + json);
            ResponseObject ro = ClientService.ReceiveResponse();
            if (ro.Message.Equals("1"))
                return true;

            new MsgAlerta(ro.Message);
            return false;
        }

        public static bool Autorize(int id)
        {
            try
            {
                DBXCommand cmd = new DBXCommand();
                cmd.Execute("VR " + id);
                ResponseObject ro = ClientService.ReceiveResponse();
                if (ro.Message.Equals("1"))
                    return true;

                new MsgAlerta(ro.Message);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("O sistema não conseguiu se recuperar de uma falha ao comunicar com o Licence Server e será encerrado. \nAcione o suporte Doware.\nLicenceServer -50");
                System.Environment.Exit(0);
            }

            return false;
        }
    }


    [Serializable]
    public class LicenceUser
    {
        public int ID { get; set; }

        public string NAME { get; set; }

        public bool ACTIVE { get; set; }
    }
}
