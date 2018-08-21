namespace RH.Domain.Regional
{
    public class Endereco //Base.Entidades.EntityBase
    {
        public int EnderecoID { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string CEP { get; set; }
        public string Bairro { get; set; }

        public virtual Cidade Cidade { get; set; }
        //public virtual Funcionario Funcionario { get; set; }

        public override string ToString()
        {
            return string.Concat(Logradouro, ", ", Numero, " CEP : ", CEP, " ", Cidade.Nome, "-", Cidade.Estado.UF);
        }
    }
}