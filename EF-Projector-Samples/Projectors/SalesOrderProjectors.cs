using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EFSplitProjectorTests.Models;
using EFSplitProjectorTests.Projectors.ReturnModels;
using LinqKit;
using EF_Split_Projector;

namespace EFSplitProjectorTests.Projectors
{
    public static class SalesOrderProjectors
    {
        public static IEnumerable<Expression<Func<SalesOrderHeader, SalesOrderReturn>>> SplitSelectOrder(AdventureWorksContext context)
        {
            var detail = SalesOrderDetailProjectors.SplitSelectOrderDetail(context);
            var contact = ContactProjectors.SelectContact();

            return new Projectors<SalesOrderHeader, SalesOrderReturn>
                {
                    h => new SalesOrderReturn
                        {
                            SalesOrderNumber = h.SalesOrderNumber,
                            PurchaseOrderNumber = h.PurchaseOrderNumber,
                            AccountNumber = h.AccountNumber,
                            ShipMethod = h.ShipMethod.Name,
                            Reasons = h.SalesOrderHeaderSalesReasons.Select(r => r.SalesReason.Name),
                        },
                    h => new SalesOrderReturn
                        {
                            SalesPerson = contact.Invoke(h.SalesPerson.Employee.Contact),
                            TotalValue = h.SalesOrderDetails.Any() ? h.SalesOrderDetails.Sum(s => s.OrderQty * (s.UnitPrice * ((decimal) 1.0 - s.UnitPriceDiscount))) : (decimal) 0.0,
                        },
                    { detail, p => h => new SalesOrderReturn
                        {
                            Details = h.SalesOrderDetails.Select(d => p.Invoke(d))
                        }
                    }
                };
        }
    }
}