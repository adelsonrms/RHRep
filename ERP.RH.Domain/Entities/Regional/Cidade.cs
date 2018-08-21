namespace RH.Domain.Regional
{
    public class Cidade
    {
        public Cidade()
        {
            Estado = new Estado();
        }

        public int CidadeID { get; set; }
        public string Nome { get; set; }
        public virtual Estado Estado { get; set; }

        public override string ToString()
        {
            return Nome + '-' + Estado.UF;
        }
    }
}