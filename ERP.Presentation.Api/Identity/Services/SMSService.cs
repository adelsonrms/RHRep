using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace ERP.Presentation.Api.Identity.Services
{
    public class SMSService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Classe de Servico do SMS
            return Task.FromResult(0);
        }
    }
}