using Domain.Core;
using Enums.Core;
using Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Core
{
    public class RackHalper
    {
        public static List<Room> GenerateRooms(ICollection<RoomModel> rooms)
        {

            if (rooms == null)
                throw new ArgumentNullException("rooms");

            if (rooms.Any(x => string.IsNullOrEmpty(x.StoreId)))
                throw new ArgumentNullException("Invalid storeId of any rooms");

            if (rooms.Any(x => string.IsNullOrEmpty(x.CompanyId)))
                throw new ArgumentNullException("Invalid companyId of any rooms");

            if (rooms.Any(x => string.IsNullOrEmpty(x.CreatedBy)))
                throw new ArgumentNullException("createdBy");

            List<Room> result = new List<Room>();

            foreach (var item in rooms)
            {
                var entity = new Room
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    StoreId = item.StoreId,
                    CompanyId = item.CompanyId,
                    CreatedBy = item.CreatedBy,
                    CreatedOn = DateTime.UtcNow,
                    Status = (byte)StatusType.Active,
                    SecondaryStatus = (byte)SecondaryStatusType.Empty
                };
                //add racks of room.
                entity.Racks = GenerateRacks(item.Racks);
                result.Add(entity);
            }
            return result;

        }
        public static Room GenerateRoom(string storeId, string companyId, string createdBy)
        {
            if (string.IsNullOrEmpty(storeId))
                throw new ArgumentNullException("storeId");

            if (string.IsNullOrEmpty(companyId))
                throw new ArgumentNullException("companyId");

            if (string.IsNullOrEmpty(createdBy))
                throw new ArgumentNullException("createdBy");

            var entity = new Room
            {
                Id = Guid.NewGuid().ToString("N"),
                Name = "Default",
                Description = "Default",
                StoreId = storeId,
                CompanyId = companyId,
                CreatedBy = createdBy,
                CreatedOn = DateTime.UtcNow,
                Status = (byte)StatusType.Active,
                SecondaryStatus = (byte)SecondaryStatusType.Empty
            };
            //add  default rack of room.
            entity.Racks.Add(GenerateRack(entity.Id, entity.CompanyId, entity.CreatedBy));
            return entity;
        }
        public static Rack GenerateRack(string roomId, string companyId, string createdBy)
        {
            if (string.IsNullOrEmpty(roomId))
                throw new ArgumentNullException("roomId");

            if (string.IsNullOrEmpty(companyId))
                throw new ArgumentNullException("companyId");

            if (string.IsNullOrEmpty(createdBy))
                throw new ArgumentNullException("createdBy");

            var entity = new Rack
            {
                Id = Guid.NewGuid().ToString("N"),
                Name = "Default",
                Description = "Default",
                RoomId = roomId,
                CompanyId = companyId,
                BoxCapacity = 10,
                Rows = 10,
                Columns = 10,
                CreatedBy = createdBy,
                CreatedOn = DateTime.UtcNow,
                Status = (byte)StatusType.Active,
                SecondaryStatus = (byte)SecondaryStatusType.Empty
            };
            // add boxes of rack.
            entity.Boxes = GenerateBoxes(entity.Rows, entity.Columns, entity.Id,entity.CompanyId);

            return entity;
        }
        public static List<Rack> GenerateRacks(ICollection<RackModel> racks)
        {

            if (racks == null)
                throw new ArgumentNullException("rooms");

            if (racks.Any(x => string.IsNullOrEmpty(x.RoomId)))
                throw new ArgumentNullException("Invalid roomId of any rooms");

            if (racks.Any(x => string.IsNullOrEmpty(x.CompanyId)))
                throw new ArgumentNullException("Invalid companyId of any rooms");

            if (racks.Any(x => string.IsNullOrEmpty(x.CreatedBy)))
                throw new ArgumentNullException("createdBy");

            List<Rack> result = new List<Rack>();
            foreach (var item in racks)
            {
                var entity = new Rack
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    RoomId = item.RoomId,
                    CompanyId = item.CompanyId,
                    Rows = item.Rows,
                    Columns = item.Columns,
                    CreatedBy = item.CreatedBy,
                    CreatedOn = DateTime.UtcNow,
                    Status = (byte)StatusType.Active,
                    SecondaryStatus = (byte)SecondaryStatusType.Empty
                };
                // add boxes of rack.
                entity.Boxes = GenerateBoxes(entity.Rows, entity.Columns, entity.Id, entity.CompanyId);
                result.Add(entity);
            }
            return result;
        }
        public static List<RackBox> GenerateBoxes(int rows, int columns, string rackId, string companyId)
        {
            if (rows <= 0 || columns <= 0 || string.IsNullOrEmpty(rackId) || string.IsNullOrEmpty(companyId))
                throw new ArgumentException("Invalid arguments");

            List<RackBox> result = new List<RackBox>();

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    var item = new RackBox
                    {
                        Id = Guid.NewGuid().ToString("N"),
                        CompanyId = companyId,
                        Name = "Box",
                        Description = "Box",
                        RackId = rackId,
                        Row = i,
                        Column = j,
                        SecondaryStatus=(byte)SecondaryStatusType.Empty
                    };
                    result.Add(item);

                }

            }
            return result;
        }
    }
}
