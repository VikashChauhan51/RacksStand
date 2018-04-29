using Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
   public interface ITokenRepository: IRepository<Token, string>
    {
 
        /// <summary>
        ///  Delete elements that comply to specified criteria and add new element.
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="entity">(<see cref="Token"/>) new entity.</param>
        /// <returns>void</returns>
        void DeleteAndAdd( Expression<Func<Token, bool>> predicate, Token entity);
        /// <summary>
        /// Get token details based on predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>(<see cref="Token"/>) response.</returns>
        Token GetSingle(Expression<Func<Token, bool>> predicate);
        /// <summary>
        /// Deactivate token after used it.
        /// </summary>
        /// <param name="status"></param>
        /// <param name="UpdatedOn"></param>
        /// <param name="id"></param>
        void DeactivateToken(byte status, DateTime UpdatedOn, string id);
    }
}
