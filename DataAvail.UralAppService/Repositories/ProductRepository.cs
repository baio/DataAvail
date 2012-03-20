using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAvail.UralAppService.Models;
using DataAvail.DataService.Provider;
using System.Data.Objects;
using System.Collections;
using DataAvail.LinqMapper;

namespace DataAvail.UralAppService.Repositories
{

    public class ProductRepository : Repository<DataAvail.UralAppModel.Product, Product>
    {
        /*
        public IQueryable<Product> GetProductsByProducer(string id)
        {
            return GetAllByForeignKey("ProducerId", id);
        }
         */

        /*
        protected override IQueryable<Product> OnGetQuery(IQueryable<Product> Query)
        {
            return base.Queryable.Select(p => new Product
            {
                id = p.Id,
                name = p.Name,
                ProducerId = p.ProducerId,
                Tags = ((DataAvail.UralAppModel.Model)Context).ProductTagMaps.Where(s => s.ProductId == p.Id).Select(s => new Tag { id = s.Tag.Id, name = s.Tag.Name })
            });
        }
         */
    }
}