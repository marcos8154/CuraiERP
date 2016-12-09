using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.Linq;
using System.Text;

namespace EM3.Controller
{
    public class Commons
    {
        public static DateTime ServerDate
        {
            get
            {
                RequestHelper rh = new RequestHelper();
                rh.Send("serverdate");

                string result = rh.Result.message;
                return Convert.ToDateTime(result);
            }
        }

        public static DataTable exceldata(string filePath)
        {
            string _conectionstring;
            _conectionstring = @"Provider=Microsoft.Jet.OLEDB.4.0;";
            _conectionstring += "Data Source=C:\\Temp\\CFOP.xls;";
            _conectionstring += "Extended Properties='Excel 8.0;HDR=YES;'";

            OleDbConnection cn = new OleDbConnection(_conectionstring);
            OleDbCommand cmd = new OleDbCommand("Select * from CFOP", cn);

            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataTable dt = new DataTable();

            try
            {

                da.Fill(dt);
                cmd.CommandType = CommandType.Text;
                cn.Open();
                cmd.ExecuteReader();
                return dt;
            }
            catch (OleDbException ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            finally
            {
                cn.Close();
                cn.Dispose();
                cmd.Dispose();
            }
        }
    }
}
