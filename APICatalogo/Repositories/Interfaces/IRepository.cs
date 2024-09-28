using System.Linq;
using System.Linq.Expressions;

namespace APICatalogo.Repositories.Interfaces
{
    // Cuidado para não violar o princípio ISP Interface Segregation Principle
    public interface IRepository<T>
    {
        //IQueryable<T> Get();
        Task<IEnumerable<T>> GetAllAsync();
        // Expression classe LINQ permite utilizar suas consultas. Usada para representar expressões de funções lambda
        // FUNC de T que vai retonar um BOOL = delegate é uma função que pode ser passada como argumento que representa uma função lambda
        // recebendo um objeto do tipo T e retornar um BOOL com base no PREDICADO
        
        // Esse método irá aceitar como argumento uma expressão lambda,
        // recebendo um objeto de tipo T e retornará um booleano com base no predicado
        // c => c.Id == id
        Task<T?> GetAsync(Expression<Func<T, bool>> predicate);

        /* Os métodos abaixo manipulam os dados somente na memória
           O acesso ao banco de dados, a interação, se dá apenas no comando SaveChanges() */
        T Create(T entity);
        T Update(T entity);
        T Delete(T entity);
    }
}
