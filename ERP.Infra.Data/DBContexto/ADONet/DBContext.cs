using RH.Domain.Generic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace RH.Infra.Data
{
    public static class DBContext
    {
        public static SqlConnection Connection => DBConnection.Connection;


        public static IDataReader GetData(string pSQLCommand, SqlConnection connection)
        {
            var cmd = PrepareCommand(pSQLCommand, null, null);
            return cmd.ExecuteReader();
        }

        public static DataTable GetDataTable(string pSQLCommand)
        {
            try
            {
                var da = new SqlDataAdapter(pSQLCommand, DBConnection.Connection);
                var table = new DataTable();
                da.Fill(table);
                return table;
            }
            catch (Exception ex)
            {
                throw new Exceptions.DALExceptionGetDataTable(
                    string.Format("GetDataTable() - Error {0} : ", ex.Message));
            }
        }

        public static DataSet GetDataSet(string pSQLCommand, SqlConnection connection)
        {
            //var cmd = prepareCommand(pSQLCommand, connection);
            return ExecuteDataSet(pSQLCommand, connection);
        }

        private static DataSet ExecuteDataSet(string pSQLCommand, SqlConnection connection)
        {
            DataSet ds = ds = null;
            try
            {
                SqlDataAdapter da = null;
                IDbCommand dbCmd = new SqlCommand(pSQLCommand, connection);
                da = new SqlDataAdapter
                {
                    SelectCommand = (SqlCommand)dbCmd
                };
                ds = new DataSet();
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                throw new Exceptions.DALExceptionExecuteReader(ex.Message);
            }

            return ds;
        }

        public static int Execute(string pSQLCommand, SqlConnection connection, string[] paramName = null,
            string[] paramValues = null)
        {
            var cmd = PrepareCommand(pSQLCommand, paramName, paramValues);

            var intReturn = cmd.ExecuteNonQuery();
            cmd.Dispose();
            cmd = null;
            return intReturn;
        }

        public static string ExecuteScalar(string pSQLCommand, SqlConnection connection, string[] paramName = null,
            string[] paramValues = null)
        {
            var cmd = PrepareCommand(pSQLCommand, paramName, paramValues);
            var intReturn = cmd.ExecuteScalar();
            cmd = null;
            cmd.Dispose();
            return intReturn.ToString();
        }

        public static int CRUD(string CRUD_Command, string[] paramName = null, string[] paramValues = null)
        {
            return Execute(CRUD_Command, Connection, paramName, paramValues);
        }

        public static SqlCommand PrepareCommand(string pSQLCommand, string[] paramName = null,
            string[] paramValues = null)
        {
            try
            {
                var cmd = new SqlCommand {CommandText = pSQLCommand, Connection = Connection};
                cmd = FillParameters(cmd, paramName, paramValues);
                return cmd;
            }
            catch
            {
                throw new Data.Exceptions.DALExceptionCommand();
            }
        }

        private static SqlCommand FillParameters(SqlCommand cmd, string[] paramName, string[] paramValues)
        {
            if (paramName != null)
                for (var i = 0; i < paramName.Length; i++)
                    cmd.Parameters.Add(new SqlParameter(paramName[i], paramValues[i]));
            return cmd;
        }

        internal static IEnumerable<TEntidade> ConvertDataReaderToEntity<TEntidade>(DataTable dt,
            List<Property> list)
        {
            IEnumerable<TEntidade> Linhas = null;
            // dynamic objeto;

            var dr = dt.AsEnumerable().ToList();

            //Linhas = from r in dr
            //select new Funcionario()
            //{


            //};

            return Linhas;
        }
    }
}