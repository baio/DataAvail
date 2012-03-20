using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAvail.LinqMapper;
using DataAvail.UralAppService.Models;


namespace DataAvail.UralAppService.Test
{
    [TestClass]
    public class LinqMapperTest
    {
        [TestMethod]
        public void select_products()
        {
            Mapper.CreateMap<DataAvail.UralAppModel.Product, Product>()
                .ForMember(p => p.id, opt => opt.MapFrom(p => p.Id))
                //.ForMember(p => p.name, opt => opt.MapFrom(p => p.Name))
                .ForMember(p => p.name, opt => opt.ResolveUsing(p => p.Name + "..."))
                .ForMember(p => p.ProducerId, opt => opt.MapFrom(p => p.ProducerId))
                .ForMember(p => p.Tags, opt => opt.ResolveUsing(p => p.ProductTagMaps.Select(x => new Tag { id = x.Tag.Id, name = x.Tag.Name })));

            DataAvail.UralAppModel.Model model = new UralAppModel.Model();
            var q = LinqMapper.Mapper.Map<DataAvail.UralAppModel.Product, Product>(model.Products);
            var r = q.ToArray();
        }

    }
}
