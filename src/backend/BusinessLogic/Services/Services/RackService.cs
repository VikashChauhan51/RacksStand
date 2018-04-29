using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core;
using Models.Core;
using Models.Filter;
using Repository.Interfaces;
using Enums.Core;
using System.Linq.Expressions;
using Services.Core;
using Utility.Extension;

namespace Services.Services
{
    public class RackService : IRackService
    {
        #region Fields

        private readonly IRackRepository _rackRepository;
        #endregion
        public RackService(IRackRepository rackRepository)
        {
            this._rackRepository = rackRepository;

            if (this._rackRepository == null)
                throw new ArgumentNullException("RackService.IRackRepository");

        }

        public void Add(RackModel item)
        {
            try
            {
                if (item == null)
                    throw new ArgumentNullException("item");

                var entity = new Rack
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    RoomId = item.RoomId,
                    CompanyId=item.CompanyId,
                    Rows = item.Rows,
                    Columns = item.Columns,
                    BoxCapacity=item.BoxCapacity,
                    CreatedBy = item.CreatedBy,
                    CreatedOn = DateTime.UtcNow,
                    Status = (byte)StatusType.Active,
                    SecondaryStatus = (byte)SecondaryStatusType.Empty
                };
                entity.Boxes = RackHalper.GenerateBoxes(item.Rows, item.Columns, item.Id,item.CompanyId);
                this._rackRepository.Add(entity);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void Delete(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                    throw new ArgumentNullException("id");

                var item = this._rackRepository.Get(id);
                if (item != null)
                {
                    item.Status = (byte)StatusType.Deleted;
                    this._rackRepository.Update(item);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public IEnumerable<RackBox> Get(RackBoxSearchFilter filter)
        {
            try
            {
                if (filter == null)
                    throw new ArgumentNullException("filter");

                if (string.IsNullOrEmpty(filter.CompanyId))
                    throw new ArgumentNullException("CompanyId");

                if (string.IsNullOrEmpty(filter.Keyword))
                    filter.Keyword = "*";

                if (filter.Take <= 0)
                    filter.Take = 100;

                Expression<Func<RackBox, bool>> predicate = x =>
                   x.CompanyId == filter.CompanyId &&
   (filter.Keyword == "*" || x.Name.StartsWith(filter.Keyword));

                if (!string.IsNullOrEmpty(filter.RackId))
                    predicate = predicate.And(x => x.RackId == filter.RackId);

                    return this._rackRepository.Find(predicate).Skip(filter.Start).Take(filter.Take).ToList();

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public IEnumerable<Rack> Get(RackSearchFilter filter)
        {
            try
            {
                if (filter == null)
                    throw new ArgumentNullException("filter");

                if (string.IsNullOrEmpty(filter.CompanyId))
                    throw new ArgumentNullException("CompanyId");

                if (string.IsNullOrEmpty(filter.Keyword))
                    filter.Keyword = "*";

                if (filter.Take <= 0)
                    filter.Take = 100;

                Expression<Func<Rack, bool>> predicate = x =>
                   x.CompanyId == filter.CompanyId && x.Status == (byte)StatusType.Active &&
   (filter.Keyword == "*" || x.Name.StartsWith(filter.Keyword));

                if (!string.IsNullOrEmpty(filter.RoomId))
                    predicate = predicate.And(x => x.RoomId == filter.RoomId);

                return this._rackRepository.Find(predicate).Skip(filter.Start).Take(filter.Take).ToList();

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public Rack GetById(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                    throw new ArgumentNullException("id");

                return this._rackRepository.Get(id);

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public Rack GetWithRackBoxes(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                    throw new ArgumentNullException("id");

                return this._rackRepository.GetById(id);

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void Update(RackBoxModel item)
        {
            try
            {
                if (item == null)
                    throw new ArgumentNullException("item");

                Expression<Func<RackBox>> entity = () => new RackBox
                {
                    Name = item.Name,
                    Description = item.Description,
                    SecondaryStatus=item.SecondaryStatus
                };
                this._rackRepository.Update(entity,x=>x.Id==item.Id);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void Update(RackModel item)
        {
            try
            {
                if (item == null)
                    throw new ArgumentNullException("item");

                Expression<Func<Rack>> entity = () => new Rack
                {
                    Name = item.Name,
                    Description = item.Description,
                    UpdatedBy = item.UpdatedBy,
                    UpdatedOn = DateTime.UtcNow,
                    Status = item.Status,
                    SecondaryStatus=item.SecondaryStatus,
                    BoxCapacity=item.BoxCapacity
                };
                this._rackRepository.Update(entity,x=>x.Id==item.Id);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
     
    }
}
