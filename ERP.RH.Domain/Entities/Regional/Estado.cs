namespace RH.Domain.Regional
{
    public class Estado
    {
        public int EstadoID { get; set; }
        public string Nome { get; set; }
        public string Codigo { get; set; }
        public string UF { get; set; }

        public override string ToString()
        {
            return UF + '-' + Nome;
        }
    }
}