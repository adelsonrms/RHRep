namespace RH.Domain.Entities
{
    public class Contato //:EntityBase
    {
        public int ContatoID { get; set; }
        public int TipoContatoID { get; set; }
        public string ValorContato { get; set; }

        public override string ToString()
        {
            return ValorContato;
        }
    }
}