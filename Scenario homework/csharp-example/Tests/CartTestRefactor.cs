using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_example
{
    internal class CartTestRefactor : TestBase
    {
        [Test]
        public void ProductCartTestRefactor()
        {
            int ProductsCount = 3;
            app.AddPorductsInCart(ProductsCount);
            app.RemovePorductsFromCart(ProductsCount);
        }
    }
}
