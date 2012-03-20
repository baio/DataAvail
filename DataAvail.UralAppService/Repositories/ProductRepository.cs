using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAvail.UralAppService.Models;
using DataAvail.DataService.Provider;
using System.Data.Objects;
using System.Collections;

namespace DataAvail.UralAppService.Repositories
{

    public class ProductRepository : Repository<DataAvail.UralAppModel.Product, Product>
    {
        public IQueryable<Product> GetProductsByProducer(string id)
        {
            return GetAllByForeignKey("ProducerId", id);
        }
    }
}