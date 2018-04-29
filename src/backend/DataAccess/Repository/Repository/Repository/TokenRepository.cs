using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core;
using System.Linq.Expressions;
using Repository.Core;
using ServiceStack.OrmLite;

namespace Repository.Repository
{
    public class TokenRepository : ITokenRepository
    {
        #region Fields

        private readonly IDbConnectionProvider _provider;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of the  <see cref="TokenRepository" /> class.
        /// </summary>
        /// <param name="provider">The provider.</param>      
        public TokenRepository(IDbConnectionProvider provider)
        {
            this._provider = provider;
            if (this._provider == null)
                throw new ArgumentNullException("TokenRepository.IDbConnectionProvider");
        }
        #endregion
        public  void Add(Token entity)
        {
            if (entity == null)
                throw new ArgumentNullException("TokenRepository.entity");

             _provider.Perform(con => con.Save(entity, true));
        }

        public void DeactivateToken(byte status, DateTime UpdatedOn, string id)
        {
            _provider.Perform(con => con.UpdateOnly(() => new Token { Status = status, UpdatedOn = UpdatedOn }, where: p => p.Id == id));
        }

        public  void Delete(Expression<Func<Token, bool>> predicate)
        {
             _provider.Perform(con => con.Delete(predicate));
        }

        public  void Delete(Token entity)
        {
            if (entity == null)
                throw new ArgumentNullException("TokenRepository.entity");

             _provider.Perform(con => con.Delete(entity));
        }

        public  void DeleteAndAdd(Expression<Func<Token, bool>> predicate, Token entity)
        {
            if (entity == null)
                throw new ArgumentNullException("TokenRepository.entity");
            //delete records based on specified criteria.
             Delete(predicate);
            //add new entity.
             Add(entity);
        }
 

        public bool Exists(Expression<Func<Token, bool>> predicate)
        {
            return  _provider.Perform(con => con.Exists(predicate));
        }

        public IQueryable<Token> Find(Expression<Func<Token, bool>> predicate)
        {
            return  _provider.Perform(con => con.Select(predicate)).AsQueryable();
        }

        public Token Get(string id)
        {
            if (id == null)
                throw new ArgumentNullException("TokenRepository.id");

            return  _provider.Perform(con => con.Single<Token>(q => q.Id == id));
        }

        public Token GetSingle(Expression<Func<Token, bool>> predicate)
        {
            return _provider.Perform(con => con.Single(predicate));
        }

        public  void Update(Token entity)
        {
            if (entity == null)
                throw new ArgumentNullException("TokenRepository.entity");

             _provider.Perform(con => con.Update(entity));
        }

        public void Update(Expression<Func<Token>> updateFields, Expression<Func<Token, bool>> predicate)
        {
            _provider.Perform(con => con.UpdateOnly(updateFields, predicate));
        }
    }
}
