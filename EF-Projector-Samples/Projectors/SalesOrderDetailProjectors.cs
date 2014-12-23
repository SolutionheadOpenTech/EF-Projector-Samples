using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using EFSplitProjectorTests.Models;
using EFSplitProjectorTests.Projectors.ReturnModels;
using EF_Split_Projector;
using EF_Split_Projector.Helpers.Extensions;
using LinqKit;

namespace EFSplitProjectorTests.Projectors
{
    public static class SalesOrderDetailProjectors
    {
        public static IEnumerable<Expression<Func<SalesOrderDetail, SalesOrderDetailReturn>>> SplitSelectOrderDetail(AdventureWorksContext context)
        {
            var product = ProductProjectors.SelectProductionProduct();
            var specialOffer = SpecialOfferProductProjectors.SelectSpecialOfferProduct();
            var singularProduct = ProductProjectors.SelectProductionProduct().Merge();

            return new Projectors<SalesOrderDetail, SalesOrderDetailReturn>
                {
                    d => new SalesOrderDetailReturn
                        {
                            CarrierTrackingNumber = d.CarrierTrackingNumber,
                            Quantity = d.OrderQty,
                            UnitPrice = d.UnitPrice,
                            UnitDiscount = d.UnitPriceDiscount,
                            LineTotal = d.LineTotal,

                            SpecialOfferProduct = specialOffer.Invoke(d.SpecialOfferProduct),
                            Product = singularProduct.Invoke(d.SpecialOfferProduct.Product)
                        },
                    //{ product, p => d => new SalesOrderDetailReturn
                    //    {
                    //        Product = p.Invoke(d.SpecialOfferProduct.Product)
                    //    }
                    //}
                };
        }
    }
}