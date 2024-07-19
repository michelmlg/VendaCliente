using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;

namespace Prova
{
    class DAL
    {
        private static String strConexao = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BDFarinha.mdb";
        private static OleDbConnection conn = new OleDbConnection(strConexao);
        private static OleDbCommand strSQL;
        private static OleDbDataReader result;

        public static void conecta()
        {
            try
            {
                conn.Open();
            }
            catch (Exception)
            {
                Erro.setMsg("Problemas ao se conectar ao Banco de Dados");
            }

        }

        public static void desconecta()
        {
            conn.Close();
        }

        public static void consultaUmCliente()
        {
            String aux = "select * from TabClientes where cnpj = '" + Cliente.getCNPJ() + "'";

            strSQL = new OleDbCommand(aux, conn);
            result = strSQL.ExecuteReader();
            Erro.setErro(false);
            if (result.Read())
            {
                Cliente.setNome(result.GetString(1));
            }
            else
                Erro.setMsg("Cliente não cadastrado.");
        }

        public static void populaVendaDR()
        {
            String aux = "select * from TabVendasCliente where cnpj = '" + Cliente.getCNPJ() + "'";

            strSQL = new OleDbCommand(aux, conn);
            result = strSQL.ExecuteReader();
        }

        public static void getProximaVenda()
        {
            Erro.setErro(false);
            if (result.Read())
            {
                VendaCliente.setCodigo(result.GetValue(0).ToString());
                VendaCliente.setCNPJ(result.GetString(1));
                VendaCliente.setData(result.GetString(2));
                VendaCliente.setToneladas(result.GetString(3));
                VendaCliente.setvalor(result.GetString(4));
            }
            else
                Erro.setErro(true);
            
        }
    }
}
