using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAvail.UralAppService.Models;
using DataAvail.DataService.Provider;
using System.Data.Objects;

namespace DataAvail.UralAppService.Repositories
{

    public class ProductRepository : Repository<DataAvail.UralAppModel.Product, Product>
    {
        public ProductRepository()
        {                        
        }

        private readonly ObjectContext _context = new DataAvail.UralAppModel.Model();
        
        protected override ObjectContext Context
        {
            get { return _context; }
        }
    }
}