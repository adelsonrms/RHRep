using ERP.Shared.ValueObjects;

namespace ERP.RH.Domain
{
    public class EFEntity
    {
        public virtual int Id { get; set; }
        public virtual string Nome { get; set; }
        public virtual string UsuarioInclusao { get; set; }
        public virtual System.DateTime? MomentoInclusao { get; set; }
        public virtual string UsuarioEdicao { get; set; }
        public virtual System.DateTime? MomentoEdicao { get; set; }
    }

}
