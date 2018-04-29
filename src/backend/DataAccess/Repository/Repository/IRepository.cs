using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IRepository<TEntity, in TKey>  
    {
        /// <summary>
        /// get entity by id.
        /// Error:
        /// Throw an error when id parameter value is null.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>(void)</returns>
        TEntity Get(TKey id);
        /// <summary>
        /// add an entity.
        /// Error:
        /// Throw an error when entity parameter value is null.
        /// </summary>
        /// <param name="entity"></param>
        void Add(TEntity entity);
        /// <summary>
        /// update an entity.
        /// Error:
        /// Throw an error when entity parameter value is null.
        /// </summary>
        /// <param name="entity"></param>
        void Update(TEntity entity);
        /// <summary>
        /// update specific fields of entity based on predicate.
        /// </summary>
        /// <param name="updateFields"></param>
        /// <param name="predicate"></param>
        void Update(System.Linq.Expressions.Expression<Func<TEntity>> updateFields, System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// delete an entity.
        /// Error:
        /// Throw an error when entity parameter value is null.
        /// </summary>
        /// <param name="entity"></param>
        void Delete(TEntity entity);
        /// <summary>
        ///  delete elements that comply to specified criteria.
        /// </summary>
        /// <param name="predicate"></param>
        void Delete(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// serch entities
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IQueryable<TEntity> Find(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// check entities exists.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        bool Exists(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate);
       
       
    }
}
