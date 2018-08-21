namespace ERP.Shared.ValueObjects
{
    public class Email
    {
        public Email()
        {
        }

        public Email(string endereco)
        {
            Endereco = endereco;
        }

        public string Endereco { get; set; }
        public string Dominio => Endereco.Split("@".ToCharArray())[1];

        public override string ToString()
        {
            return Endereco;
        }
    }
}