namespace RH.Domain.Entities
{
    /// <summary>
    ///     Representa os telefones usualmente utilizados por uma pessoa
    /// </summary>
    public class Telefone //: //Entidade
    {
        //public Telefone()
        //{
        //    TelefoneID = System.Guid.NewGuid().ToString().Substring(1, 6);
        //}
        public int TelefoneID { get; set; }
        public string Residencial { get; set; }
        public string Celular { get; set; }

        public string Comercial { get; set; }
        //public virtual Funcionario Funcionario { get; set; }
    }
}