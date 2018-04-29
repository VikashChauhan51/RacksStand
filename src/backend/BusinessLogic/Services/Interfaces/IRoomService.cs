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
    public interface IRoomService
    {
        void Add(RoomModel item);
        void AddDefault(RoomModel item);
        IEnumerable<Room> Get(RoomSearchFilter filter);
        Room GetById(string id);
        void Update(RoomModel item);
        void Delete(string id);
    }
}
