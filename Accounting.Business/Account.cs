using Accounting.DataLayer.Context;
using Accounting.ViewModels.Accounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Business
{
    public class Account
    {
        public static ReportViewModel ReportFormMain()
        {
            ReportViewModel rp = new ReportViewModel();
            using(UnitOfWork db = new UnitOfWork())
            {
                DateTime startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 01);
                DateTime endDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 30);

                var receive = db.AccountingRepository.Get(r => r.TypeId == 1 && r.DateTitle >= startDate && r.DateTitle <= endDate).Select(r => r.Amount).ToList();
                var pay = db.AccountingRepository.Get(r => r.TypeId == 2 && r.DateTitle >= startDate && r.DateTitle <= endDate).Select(r => r.Amount).ToList();

                rp.Receive = receive.Sum();
                rp.Pay = pay.Sum();
                rp.Balance = (receive.Sum() - pay.Sum());
            }
            return rp;
        }
    }
}
