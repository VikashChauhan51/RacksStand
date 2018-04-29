using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core;
using Models.Core;
using Repository.Interfaces;
using Models.Filter;
using System.Linq.Expressions;
using Utility.Extension;

namespace Services.Services
{
    public class CurrencyService : ICurrencyService
    {
        #region Fields

        private readonly ICurrencyRepository _currencyRepository;
        #endregion
        public CurrencyService(ICurrencyRepository currencyRepository)
        {
            this._currencyRepository = currencyRepository;

            if (this._currencyRepository == null)
                throw new ArgumentNullException("CurrencyService.ICurrencyRepository");

        }

        public void Add(CurrencyModel item)
        {
            try
            {
                if (item == null)
                    throw new ArgumentNullException("item");

                var currency = new Currency
                {
                    Id = item.Id,
                    Name = item.Name,
                    Code = item.Code,
                    Symbol = item.Symbol,
                    CompanyId = item.CompanyId,
                    ExchangeRate = item.ExchangeRate,
                    IsActive = item.IsActive
                };
                this._currencyRepository.Add(currency);
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

                this._currencyRepository.Delete(id);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public IEnumerable<Currency> Get(CurrencySearchFilter filter)
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
                Expression<Func<Currency, bool>> predicate = x =>
                    x.CompanyId == filter.CompanyId &&
    (filter.Keyword == "*" || x.Code.StartsWith(filter.Keyword) || x.Name.StartsWith(filter.Keyword));

                Expression<Func<Currency, bool>> statusFilter = s => s.IsActive == (filter.Active ?? true);
                if (filter.Active != null)
                    predicate = predicate.And(statusFilter);
                return this._currencyRepository.Find(predicate).Skip(filter.Start).Take(filter.Take).ToList();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public Currency GetById(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                    throw new ArgumentNullException("id");

                return this._currencyRepository.Get(id);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void Update(CurrencyModel item)
        {
            try
            {
                if (item == null)
                    throw new ArgumentNullException("item");


                var currency = new Currency
                {
                    Id = item.Id,
                    Name = item.Name,
                    Code = item.Code,
                    Symbol = item.Symbol,
                    CompanyId = item.CompanyId,
                    ExchangeRate = item.ExchangeRate,
                    IsActive = item.IsActive
                };
                this._currencyRepository.Update(currency);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
