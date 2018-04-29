using Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
   public interface IRackRepository: IRepository<Rack, string>
    {
        Rack GetById(string id, bool withBoxes = true);
        void Update(Expression<Func<RackBox>> updateFields, Expression<Func<RackBox, bool>> predicate);
        IQueryable<RackBox> Find(Expression<Func<RackBox, bool>> predicate);
    }
}
