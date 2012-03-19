using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataAvail.UralAppService.Test
{
    [TestClass]
    public class UralAppServiceTest
    {
        [TestMethod]
        public void Update_Item_zero_to_nill()
        {
            UralAppServ.DataServiceProvider prr = new UralAppServ.DataServiceProvider(new Uri(@"http://localhost:3360/Service.svc/"));
            var r = prr.Products.Where(p => p.id == 0).First();
            r.name = "nill";
            //prr.AttachTo("Products", r);
            prr.UpdateObject(r);
            prr.SaveChanges();
        }
    }
}
