using Domain.Core;
using Models.Core;
using Models.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IRackService
    {
        void Add(RackModel item);
        IEnumerable<Rack> Get(RackSearchFilter filter);
        IEnumerable<RackBox> Get(RackBoxSearchFilter filter);
        Rack GetById(string id);
        Rack GetWithRackBoxes(string id);
        void Update(RackModel item);
        void Update(RackBoxModel item);
        void Delete(string id);
    }
}
