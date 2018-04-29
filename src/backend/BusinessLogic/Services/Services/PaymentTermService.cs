using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core;
using Models.Core;
using Repository.Interfaces;

namespace Services.Services
{
    public class PaymentTermService : IPaymentTermService
    {
        #region Fields

        private readonly IPaymentTermRepository _paymentTermRepository;
        #endregion
        public PaymentTermService(IPaymentTermRepository paymentTermRepository)
        {
            this._paymentTermRepository = paymentTermRepository;

            if (this._paymentTermRepository == null)
                throw new ArgumentNullException("PaymentTermService.IPaymentTermRepository");

        }
        public void Add(PaymentTermModel item, Request request, Session session)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            if (session == null)
                throw new ArgumentNullException("session");
            var paymentTerm = new PaymentTerm
            {
                Id = item.Id,
                Name = item.Name,
                Code = item.Code,
                NumberOfDays = item.NumberOfDays,
                CompanyId = session.CompanyId,
            };
            this._paymentTermRepository.Add(paymentTerm);
        }

        public IEnumerable<PaymentTerm> Get(string keyword, string companyId, int start, int take = 100)
        {
            //TODO: serch based on keyword is pending.
            if (string.IsNullOrEmpty(companyId))
                throw new ArgumentNullException("Get.companyId");
            return this._paymentTermRepository.Find(x => x.CompanyId == companyId).Skip(start).Take(take).ToList();
        }

        public PaymentTerm GetById(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException("id");

            return this._paymentTermRepository.Get(id);
        }

        public void Update(PaymentTermModel item, Request request, Session session)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            if (session == null)
                throw new ArgumentNullException("session");

            var paymentTerm = new PaymentTerm
            {
                Id = item.Id,
                Name = item.Name,
                Code = item.Code,
                NumberOfDays = item.NumberOfDays,
                CompanyId = session.CompanyId,
            };
            this._paymentTermRepository.Update(paymentTerm);
        }
    }
}
