using RH.Domain.Entities;
using RH.Infra.Data.DBContexts;

namespace RH.Infra.Data.Repositories
{
    /// <summary>
    ///     Implementação concreta do repositório para o Funcionario.
    /// </summary>
    /// <remarks>
    ///     Herda a classe generica 'GenericRepository'.
    ///     Caso haja alguma consulta/funcionalidade especifica para Funcionários, essa deve ser feita aqui.
    ///     A classe generica possui as funcionalidades gerais, comumns a todas as entidades como : Consulta, Alterar, Inserir,
    ///     Deletar
    /// </remarks>
    public class FuncionarioRepositorio : Repositorio<Funcionario>
    {
        /// <summary>
        ///     Inicializa o repositório com o contexto ja instanciado.
        ///     Passa a instancia para a classe mae (base)
        /// </summary>
        public FuncionarioRepositorio(TecnunDbContext db) : base(db)
        {
        }
        //Caso tenha funcionalidades especificas, criar as rotinas de consulta no banco de dados Aqui
    }
}