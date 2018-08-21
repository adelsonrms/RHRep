using System;
using System.Collections.Generic;
using System.Reflection;

namespace RH.Domain.Generic
{
    /// <summary>
    ///     Representa a classe que deverá ser herdada por outras para obter informações automaticas das propriedades
    /// </summary>
    public class EntityBase
    {
        private List<Property> _Properties;
        private object thisClass;

        /// <summary>
        ///     Construtor base para inicialização do controler
        /// </summary>
        public EntityBase()
        {
        }

        /// <summary>
        ///     Ao passar uma instancia de uma Classe, recupera a lista de propriedades da classe
        /// </summary>
        /// <param name="objClass"></param>
        public EntityBase(object objClass)
        {
            GetProperties(objClass);
        }

        public List<Property> Properties
        {
            get
            {
                if (_Properties == null) _Properties = GetProperties(thisClass);
                return _Properties
                    ;
            }
            set => _Properties = value;
        }

        private List<string> PropString { get; set; }

        /// <summary>
        ///     Define a instancia da classe a ser analisada
        /// </summary>
        /// <param name="objClass">Instancia da classe</param>
        internal void SetChildClass(object objClass)
        {
            thisClass = objClass;
        }

        /// <summary>
        ///     Usando Reflections.GetProperties, analisa as informações da Classe obtem os nomes e valores das propriedades
        /// </summary>
        /// <param name="objClass">Instancia da classe</param>
        /// <returns>Lista de Propriedades (Property)</returns>
        internal List<Property> GetProperties(object objClass)
        {
            thisClass = objClass;
            var list = objClass.GetType().GetProperties();

            Properties = new List<Property>();
            PropString = new List<string>();

            var attC = GetMapFlag(objClass.GetType());

            ////list.ForEach(p =>
            ////{
            ////    if ((p.DeclaringType.Name == thisClass.GetType().Name))
            ////    {
            ////        //var att = getMapFlag(p.GetType());
            ////        //Condições para adicionar a propriedade na lista ou nao
            ////        // 1) A classe tem a marcação e o valor é true e 
            ////        var att = p.GetType().GetCustomAttributesData();
            ////        //MappForField vAtt;
            ////        if (att.Count >0)
            ////        {
            ////            Properties.Add(new Property() { Name = p.Name, Value = getMemberValue(objClass, p), DataType = p.PropertyType.Name });
            ////            propString.Add(string.Format("[{0}]", p.Name));
            ////        }

            ////    }
            ////})
            ////;
            return Properties;
        }

        private MappForField GetMapFlag(dynamic obj)
        {
            var attC = obj.GetCustomAttributesData();
            //MappForField vAtt;

            if (attC.Count >= 1 && attC[0].Constructor.DeclaringType.Name == "MappForField")
            {
                var vAtt = obj.GetCustomAttributes(true);
                return vAtt[0];
            }

            return null;
        }

        /// <summary>
        ///     Obtem o valor de uma propriedade a informar o nome
        /// </summary>
        /// <param name="pName">Nome da Propriedade</param>
        /// <returns>Retorna o conteudo da propriedade</returns>
        public object GetValue(string pName)
        {
            var propMember = thisClass.GetType().GetProperty(pName);
            return GetMemberValue(thisClass, propMember);
        }

        /// <summary>
        ///     Salva um valor na propriedae
        /// </summary>
        /// <param name="pName">Nome da Propriedae</param>
        /// <param name="pValue">Valor a ser salvo</param>
        public void SetValue(string pName, object pValue)
        {
            var propMember = thisClass.GetType().GetProperty(pName);
            SetMemberValue(thisClass, propMember, pValue);
        }

        /// <summary>
        ///     Metodo abstrato que salva o valor proprieamente dito no objeto da classe atraves da função membro.GetValue()
        /// </summary>
        /// <param name="objeto">Instancia do Objeto</param>
        /// <param name="membro">Instancia do membro do objeto</param>
        /// <param name="valor">Valor</param>
        private void SetMemberValue(object objeto, PropertyInfo membro, object valor)
        {
            try
            {
                if (membro.GetGetMethod().IsStatic)
                    membro.SetValue(null, valor, null);
                else
                    membro.SetValue(objeto, valor, null);
            }
            catch (Exception ex)
            {
                throw new Exception("setMemberValue() - Erro :" + ex.Message);
            }
        }

        /// <summary>
        ///     Recupera o valor de um membro de uma classe (Normalmente Propriedade) atraves da função membro.GetValue()
        /// </summary>
        /// <param name="objeto">Instancia do Objeto</param>
        /// <param name="membro">Instancia do membro do objeto</param>
        /// <returns></returns>
        private object GetMemberValue(object objeto, PropertyInfo membro)
        {
            if (membro.GetGetMethod().IsStatic)
                return membro.GetValue(null, null);
            return membro.GetValue(objeto, null);
        }

        #region "Consultas SELECT abstraidas"

        //DONE : Feito
        /// <summary>
        ///     Recupera a String do SELECT ALL FROM TABLE referente à classe
        /// </summary>
        private string TSQLSelectAll =>
            string.Format("SELECT {0} FROM {1}", GetSelectFields(), thisClass.GetType().Name);

        public string TSQLInsertAll => string.Format("INSERT INTO {0} ({1}) VALUES ({2})", thisClass.GetType().Name,
            GetSelectFields(), GetInsertFields());


        /// <summary>
        ///     Monta uma String SELECT considerando todos os campos (SELECT * FROM) e ja filtra por ID especificando o valor do ID
        /// </summary>
        /// <param name="id">ID a ser filtrado</param>
        /// <returns>String com o SELECT montado</returns>
        public string TSQLSelectByID(int id)
        {
            return string.Format("{0} WHERE ID={1}", TSQLSelectAll, id);
        }

        /// <summary>
        ///     Monta a string SELECT ja assumindo o ID atual da classe
        /// </summary>
        /// <returns>String com o SELECT montado</returns>
        public string TSQLSelectByID()
        {
            return TSQLSelectByID(int.Parse(GetValue("ID").ToString()));
        }

        /// <summary>
        ///     Monta um SELECT para o filtro de um campo especifico
        /// </summary>
        /// <param name="fieldName">Nome do Campoa ser filtrado</param>
        /// <param name="fieldValue">Valor a ser filtrado</param>
        /// <returns>String com o SELECT montado</returns>
        public string TSQLSelectByField(string fieldName, string fieldValue)
        {
            return string.Format("{0} WHERE {2}={3}", TSQLSelectAll, fieldName, fieldValue);
        }

        /// <summary>
        ///     Monta um SELECT para o filtro de um campo especifico
        /// </summary>
        /// <param name="fieldName">Nome do Campoa ser filtrado</param>
        /// <param name="fieldValue">Valor a ser filtrado</param>
        /// <returns>String com o SELECT montado</returns>
        public string TSQLSelectByField()
        {
            return TSQLSelectAll;
        }

        /// <summary>
        ///     Baseado nas propriedades da classe, Monta uma string seprada por virgula com os campos para uso nos SELECT
        /// </summary>
        /// <returns></returns>
        private string GetSelectFields()
        {
            if (_Properties == null) GetProperties(thisClass);
            return string.Join("  ,", PropString.ToArray());
        }


        public string GetInsertFields()
        {
            GetProperties(thisClass);
            var sql_insert = "";

            foreach (var p in _Properties) sql_insert = sql_insert + string.Format(",{0}", GetTSQLValue(p));
            return sql_insert.Trim().Substring(1);
        }

        public string GetUpdateFields()
        {
            GetProperties(thisClass);
            var sql_update = "";

            foreach (var p in _Properties)
                sql_update = sql_update + string.Format(",[{0}]={1}", p.Name, GetTSQLValue(p));
            return sql_update;
        }

        private object GetTSQLValue(Property p)
        {
            {
                var retorn = "";
                switch (p.DataType.ToLower())
                {
                    case "string":
                        retorn = "'" + p.Value + "'";
                        break;
                    default:
                        retorn = p.Value.ToString();
                        break;
                }

                ;
                return retorn;
            }
            ;
        }

        #endregion
    }

    /// <summary>
    ///     Representa uma Propriedade generica
    /// </summary>
    public class Property
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public string DataType { get; set; }
    }

    /// <summary>
    ///     Cria um atributo customizado para marcar uma propriedade como campo mapeado
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class MappForField : Attribute
    {
        public MappForField(bool map)
        {
            Map = map;
        }

        public bool Map { get; set; }
    }
}