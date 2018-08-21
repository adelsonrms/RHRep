namespace ERP.Shared.ValueObjects
{
    public class Nome
    {
        private string _nome;
        private string[] _nomeSplit;
        public Nome(string nome) {
            _nome = nome;
            _nomeSplit = splitNome();
        }

        public string PrimeiroNome { get { return getPrimeiroNome(); } }
        public string SobreNome { get { return getSobreNome(); } }
        public string UltimoNome { get { return getUltimoNome(); } }

        public string NomeCompleto => string.Format("{0} {1} {2}", PrimeiroNome, SobreNome, UltimoNome);
        public string NomeCurto => string.Format("{0} {1}", PrimeiroNome, UltimoNome);

        #region Metodos Privados de Apoio

        private string getPrimeiroNome() {return splitNome()[0];}
        private string getUltimoNome()
        {
            return _nomeSplit[_nomeSplit.Length-1];
        }
        private string getSobreNome()
        {
            string _sn="";
            for (int i = 1; i < _nomeSplit.Length-1; i++)
            {
                _sn = _sn + _nomeSplit[i];
            }
            return _sn;
        }

        private string[] splitNome()
        {
            return _nome.Split(" ".ToCharArray());
        }

        #endregion

        public override string ToString()
        {
            return NomeCompleto;
        }
    }
}