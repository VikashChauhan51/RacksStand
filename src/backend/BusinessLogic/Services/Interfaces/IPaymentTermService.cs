using Domain.Core;
using Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IPaymentTermService
    {
        void Add(PaymentTermModel item, Request request, Session session);
        IEnumerable<PaymentTerm> Get(string keyword, string companyId, int start, int take = 100);
        PaymentTerm GetById(string id);
        void Update(PaymentTermModel item, Request request, Session session);
    }
}
