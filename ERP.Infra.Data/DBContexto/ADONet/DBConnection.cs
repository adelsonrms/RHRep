//Namespaces de acesso a dados
//Usar somente o necessário

using RH.Infra.Data.Exceptions;
using System;
using System.Data.SqlClient;

/// <summary>
/// Namespace onde ficam as classes da camada de acesso a dados
/// </summary>
namespace RH.Infra.Data
{
    public enum ConnectionState
    {
        Opened,
        Close
    }

    /// <summary>
    ///     Implementa as funcionalidades que interage com o banco de dados.
    /// </summary>
    public class DBConnection : IDisposable
    {
        /// <summary>
        ///     Variavel local privada que manterá a instancia da Conexão
        /// </summary>
        private static SqlConnection _cnn;

        /// <summary>
        ///     Recupera o status da conexão (Somente leitura)
        /// </summary>
        public ConnectionState ConnectionState
        {
            get
            {
                if (_cnn != null)
                    return (ConnectionState) _cnn.State;
                return ConnectionState.Close;
                ;
            }
        }

        /// <summary>
        ///     Retorna a conexão aberta. Caso nao esteja aberta, abre-a
        /// </summary>
        public static SqlConnection Connection
        {
            get
            {
                if (_cnn == null) InitConnection();
                return _cnn;
            }
        }

  

        public static SqlConnection Connect()
        {
#pragma warning disable IDE0022 // Use expression body for methods
            return InitConnection();
#pragma warning restore IDE0022 // Use expression body for methods
        }

        /// <summary>
        ///     Efetua a conexão com o banco de dados atraves da string de conexão armazenada no Web.Config
        /// </summary>
        /// <returns>Retorna um objeto SqlConnection aberto (ou não em caso de falha)</returns>
        private static SqlConnection InitConnection()
        {
            try
            {
                //Inicializa uma nova conexão
                _cnn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["cS_VSLocalDB"]);


                _cnn.Open();
            }
            catch (Exception sex)
            {
                _cnn = null;
                throw new DALExceptionConnectionOpen(string.Format(
                    "Ocorreu um erro ao realizar a conexão com o banco de dados.\n Detalhes : {0}", sex.Message));
            }

            return _cnn;
        }

        /// <summary>
        ///     Inicializa uma conexão permitindo forçar a criação de uma nova instancia
        /// </summary>
        /// <param name="bForceNew">Se true, a conexão atual será finalizada e uma nova será aberta</param>
        /// <returns></returns>
        private SqlConnection GetConnection(bool bForceNew = false)
        {
            if (bForceNew) CloseConnection();
            ;
            return InitConnection();
        }

        /// <summary>
        ///     Finaliza a conexão atualmente ativa
        /// </summary>
        /// <returns></returns>
        private bool CloseConnection()
        {
            var ret = true;
            try
            {
                if (_cnn != null) _cnn.Close();
            }
            catch (Exception ex)
            {
                ret = false;
                throw new Data.Exceptions.DALExceptionConnectionError(string.Format(
                    "Ocorreu um erro ao finalizar a conexão com o banco de dados\n Erro : {0}", ex.Message));
            }
            finally
            {
                _cnn = null;
                _cnn.Dispose();
            }

            return ret;
        }

        public void Dispose()
        {
            _cnn.Dispose();
        }
    }
}