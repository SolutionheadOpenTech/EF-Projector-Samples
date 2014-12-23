using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using EFSplitProjectorTests.Models;
using EFSplitProjectorTests.Projectors.ReturnModels;
using EF_Split_Projector;

namespace EFSplitProjectorTests.Projectors
{
    public static class ProductProjectors
    {
        public static Expression<Func<Product, ProductReturn>> SelectProduct()
        {
            return Projector<Product>.To(p => new ProductReturn
                {
                    Name = p.Name,
                    Number = p.ProductNumber,
                    Subcategory = p.ProductSubcategory.Name,
                    ModelName = p.ProductModel.Name
                });
        }

        public static IEnumerable<Expression<Func<Product, ProductionProductReturn>>> SelectProductionProduct()
        {
            var bom = BillOfMaterialsProjectors.SelectBillOfMaterial();
            var inventory = ProductInventoryProjectors.SelectProductInventory();

            return new Projectors<Product, ProductionProductReturn>
                {
                    p => new ProductionProductReturn
                        {
                            Name = p.Name,
                            ProductNumber = p.ProductNumber,
                            ProductLine = p.ProductLine,
                            Class = p.Class,
                            Style = p.Style,
                            SubCategory = p.ProductSubcategory.Name
                        },
                    //p => new ProductionProductReturn
                    //    {
                    //    }
                    //{ bom, p => r => new ProductionProductReturn
                    //    {
                    //        BillOfMaterials = r.BillOfMaterials.Select(m => p.Invoke(m))
                    //    }
                    //},
                    //{ inventory, p => r => new ProductionProductReturn
                    //    {
                    //        Inventory = r.ProductInventories.Select(i => p.Invoke(i))
                    //    }
                    //}
                };
        }
    }
}