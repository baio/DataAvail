using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAvail.UralAppService.Models;

namespace DataAvail.UralAppService
{
    public class DataServiceProvider : DataAvail.DataService.Provider.DataServiceProvider
    {
        public DataServiceProvider()
            : base("DataAvail.UralAppService.Repositories.{0}Repository")
        {
        }

        public IQueryable<Product> Products
        {
            get
            {
                return base.CreateQuery<Product>();
            }
        }

    }
}