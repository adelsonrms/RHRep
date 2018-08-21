using System;
using System.Data;

namespace RH.Infra.Data
{
    internal class DBReader
    {
        /// <summary>
        ///     Executa um comando no banco de dados com uma conexão ativa e retorna um DataReader com os dados.
        /// </summary>
        /// <param name="sqlCommand">Comando SQL</param>
        /// <returns></returns>
        public IDataReader GetReader(string sqlCommand)
        {
            using (var cnn = DBConnection.Connection)
            {
                IDataReader red;
                try
                {
                    red = DBContext.GetData(sqlCommand, cnn);
                }
                //Erro especifico na abertura da conexão
                catch (Exceptions.DALExceptionConnectionOpen)
                {
                }
                //Erro especifico na conexçao
                catch (Exceptions.DALExceptionConnectionError)
                {
                }
                //Erro qualquer
                catch (Exception ex)
                {
                    throw new Exceptions.DALExceptionExecuteReader(ex.Message);
                }
                finally
                {
                    red = null;
                }

                //Retorno do reader
                return red;
            }
        }
    }
}