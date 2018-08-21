using System.Web.Services;

namespace ERP.RH.WSApi
{
    [WebService(Namespace = "http://tempuri.org")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class Service : WebService
    {
        [WebMethod]
        public string ObtemMensagem(string msg)
        {
            return "Mensagem informada no parametro : " + msg;
        }
    }
}