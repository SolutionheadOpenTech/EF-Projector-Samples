using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EFSplitProjectorTests.Models;
using EFSplitProjectorTests.Projectors.ReturnModels;
using EF_Split_Projector;
using LinqKit;

namespace EFSplitProjectorTests.Projectors
{
    public static class CustomerProjectors
    {
        public static IEnumerable<Expression<Func<Customer, CustomerOrdersReturn>>> SplitSelectCustomerOrders(AdventureWorksContext context)
        {
            var splitSelectOrder = SalesOrderProjectors.SplitSelectOrder(context);
            var address = AddressProjectors.SelectAddress();
            var contact = ContactProjectors.SelectContact();

            return new Projectors<Customer, CustomerOrdersReturn>
                {
                    c => new CustomerOrdersReturn
                        {
                            AccountNumber = c.AccountNumber,
                            Type = c.CustomerType,
                            Contact = contact.Invoke(c.Individual.Contact),
                            Addresses = c.CustomerAddresses.Select(a => address.Invoke(a.Address)),
                        },
                    { splitSelectOrder, p => c => new CustomerOrdersReturn
                        {
                            Orders = c.SalesOrderHeaders.Select(o => p.Invoke(o))
                        }
                    }
                };
            
        }
    }
}