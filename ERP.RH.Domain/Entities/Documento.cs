namespace RH.Domain.Entities
{
    public class Documento
    {
        public virtual int Id { get; set; }

        public virtual int IdFuncionario { get; set; }

        public virtual string RG_Numero { get; set; }
        public virtual string RG_OrgaoEmissor { get; set; }
        public virtual string RG_UF { get; set; }
        public virtual string RG_DtEmissao { get; set; }
        public virtual string CPF { get; set; }
        public virtual string PIS { get; set; }
        public virtual string TituloEleitoral_Num { get; set; }
        public virtual string TituloEleitoral_Zona { get; set; }
        public virtual string CartHabilitacao_Numero { get; set; }
        public virtual string CartHabilitacao_Categoria { get; set; }
        public virtual string CartTrabalho_Num { get; set; }
        public virtual string CartTrabalho_Serie { get; set; }
        public virtual string CertNascimento_Livro { get; set; }
        public virtual string CBO { get; set; }
        public virtual string NomeDoPai { get; set; }
        public virtual string NomeDaMae { get; set; }
    }
}