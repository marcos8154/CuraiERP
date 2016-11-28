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

        public static bool RegisterUser(Usuarios usuario)
        {
            LicenceUser user = new LicenceUser() { ID = usuario.Id, NAME = usuario.Nome, ACTIVE = usuario.Ativo };
            string json = JsonConvert.SerializeObject(user);

            DBXCommand cmd = new DBXCommand();
            cmd.Execute("RU " + json);
            ResponseObject ro = ClientService.ReceiveResponse();
            if (ro.Message.Equals("1"))
                return true;

            new MsgAlerta(ro.Message);
            return false;
        }

        public static bool AuthorizeAdd()
        {
            try
            {
                DBXCommand cmd = new DBXCommand();
                cmd.Execute("AN " + UsuariosController.GetCount(1));
                ResponseObject ro = ClientService.ReceiveResponse();
                return ro.Message.Equals("1");
            }
            catch(Exception ex)
            {
                MessageBox.Show("O sistema não conseguiu se recuperar de uma falha ao comunicar com o Licence Server e será encerrado. \nAcione o suporte Doware.\nLicenceServer -50", "Conexão com Licence Server", MessageBoxButton.OK, MessageBoxImage.Error);
                System.Environment.Exit(0);
            }

            return false;
        }

        public static bool UpdateUser(Usuarios usuario)
        {
            LicenceUser user = new LicenceUser() { ID = usuario.Id, NAME = usuario.Nome, ACTIVE = usuario.Ativo };
            string json = JsonConvert.SerializeObject(user);

            DBXCommand cmd = new DBXCommand();
            cmd.Execute("UU " + json);

            ResponseObject ro = ClientService.ReceiveResponse();
            return (ro.Message.Equals("1"));
        }

        public static bool Authorize(int id)
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

        internal static bool RemoveUser(int id)
        {
            DBXCommand cmd = new DBXCommand();
            cmd.Execute("DU " + id);
            ResponseObject ro = ClientService.ReceiveResponse();
            return ro.Message.Equals("1");
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
