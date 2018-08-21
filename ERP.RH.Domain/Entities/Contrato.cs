using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RH.Domain.Entities
{
    public class Contrato
    {
        public int Id { get; set; }
        public virtual int IdFuncionario { get; set; }
        public virtual string DataAdmissao { get; set; }
        public virtual string DataDemissao { get; set; }

        [ForeignKey("Modalidade")]
        public virtual int IdModalidade { get; set; }
        [ForeignKey("Cargo")]
        public virtual int IdCargo { get; set; }
        public virtual bool ativo { get; set; }
        public virtual float Salario { get; set; }
        public string TempoDeCasa { get; set; }
        //public virtual Funcionario Funcionario { get; set; }
        public virtual Cargo Cargo { get; set; }
        public virtual Modalidade Modalidade { get; set; }

        public string StatusContrato
        {
            get { return DataDemissao != null ? "Demitido" : "Ativo"; }
        }

        public int QtdDependentes { get; set; }
    }

    public class DataContrato
    {
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public DataContrato(Contrato contrato)
        {
            if (contrato.DataAdmissao != null) { DataInicio = DateTime.Parse(contrato.DataAdmissao); }
            DataFim = DateTime.Today;
            if (contrato.DataDemissao != null) { DataFim = DateTime.Parse(contrato.DataDemissao); }
        }
    }
}