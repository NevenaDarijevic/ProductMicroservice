using Product.Models;
using Product.Models.Helpers;
using Product.Models.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Product.Data
{
     public interface IProductRepository
    {
        
        Proizvod GetProductById(long id);
        void Create(Proizvod proizvod);
        bool SaveChanges();
        void Update(Proizvod proizvod);
        PagedList<Proizvod> GetByCriteria(Expression<Func<Models.Proizvod, bool>> filter, ProductParameters productParameters);
    }
}
