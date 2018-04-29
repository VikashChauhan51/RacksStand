using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core;
using Models.Core;
using Repository.Interfaces;
using System.Linq.Expressions;
using Models.Filter;
using Utility.Extension;

namespace Services.Services
{
    public class TaxService : ITaxService
    {
        #region Fields

        private readonly ITaxRepository _taxRepository;
        #endregion
        public TaxService(ITaxRepository taxRepository)
        {
            this._taxRepository = taxRepository;

            if (this._taxRepository == null)
                throw new ArgumentNullException("TaxService.ITaxRepository");

        }
        public void Add(TaxModel item)
        {
            try
            {
                if (item == null)
                    throw new ArgumentNullException("item");

                var tax = new Tax
                {
                    Id = item.Id,
                    Name = item.Name,
                    Code = item.Code,
                    Rate = item.Rate,
                    CompanyId = item.CompanyId,
                    IsActive = item.IsActive,
                    IsCompound = item.IsCompound
                };
                this._taxRepository.Add(tax);
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

                this._taxRepository.Delete(id);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public IEnumerable<Tax> Get(TaxSearchFilter filter)
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

                  Expression<Func<Tax, bool>> predicate = x =>
                 x.CompanyId == filter.CompanyId &&
(filter.Keyword == "*" || x.Code.StartsWith(filter.Keyword) || x.Name.StartsWith(filter.Keyword));

                Expression<Func<Tax, bool>> statusFilter = s => s.IsActive == (filter.Active ?? true);

                Expression<Func<Tax, bool>> taxFilter = s => s.IsCompound == (filter.IsCompound ?? true);
                
                if (filter.Active != null)
                    predicate = predicate.And(statusFilter);

                if (filter.IsCompound != null)
                    predicate = predicate.And(taxFilter);

                return this._taxRepository.Find(predicate).Skip(filter.Start).Take(filter.Take).ToList();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public Tax GetById(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                    throw new ArgumentNullException("id");

                return this._taxRepository.Get(id);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void Update(TaxModel item)
        {
            try
            {
                if (item == null)
                    throw new ArgumentNullException("item");

                var tax = new Tax
                {
                    Id = item.Id,
                    Name = item.Name,
                    Code = item.Code,
                    Rate = item.Rate,
                    CompanyId = item.CompanyId,
                    IsActive = item.IsActive,
                    IsCompound = item.IsCompound
                };
                this._taxRepository.Update(tax);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
