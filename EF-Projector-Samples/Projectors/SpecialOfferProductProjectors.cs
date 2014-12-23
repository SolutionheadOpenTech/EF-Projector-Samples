using System;
using System.Linq.Expressions;
using EFSplitProjectorTests.Models;
using EFSplitProjectorTests.Projectors.ReturnModels;
using EF_Split_Projector;
using EF_Split_Projector.Helpers.Extensions;

namespace EFSplitProjectorTests.Projectors
{
    public static class SpecialOfferProductProjectors
    {
        public static Expression<Func<SpecialOfferProduct, SpecialOfferProductReturn>> SelectSpecialOfferProduct()
        {
            return ProductProjectors.SelectProduct().Merge(Projector<SpecialOfferProduct>.To(p => new SpecialOfferProductReturn
                {
                    Description = p.SpecialOffer.Description,
                    Type = p.SpecialOffer.Type,
                    Category = p.SpecialOffer.Category,
                    Discount = p.SpecialOffer.DiscountPct
                }), s => s.Product);
        }
    }
}