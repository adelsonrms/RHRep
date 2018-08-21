using RH.Domain.Entities;

namespace ERP.RH.Application
{
    public class AnexoApplication:EntityApplication<Anexo>
    {
        public void SalvarAnexo(Anexo anexo)
        {
            base.Salvar(anexo);
        }

        public Anexo ObterAnexoPorId(int id)
        {
            return base.ObterPorId(id);
        }
    }
}
