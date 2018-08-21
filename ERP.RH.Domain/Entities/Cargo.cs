using ERP.RH.Domain;

namespace RH.Domain.Entities
{
    public class Cargo:EFEntity
    {
        #region propriedades públicas 
        /// <summary>
        /// id do tipo de atividade
        /// </summary>
        public override int Id { get; set; }
        /// <summary>
        /// nome do tipo de atividade
        /// </summary>
        public override string Nome { get; set; }
        public virtual string CBO { get; set; }

        #endregion

    }
}