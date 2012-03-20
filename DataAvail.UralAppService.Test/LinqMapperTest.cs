using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAvail.LinqMapper;
using DataAvail.UralAppService.Models;
using System.Linq.Expressions;


namespace DataAvail.UralAppService.Test
{
    [TestClass]
    public class LinqMapperTest
    {
        [TestMethod]
        public void select_products()
        {
            Mapper.CreateMap<DataAvail.UralAppModel.Tag, Tag>()
                .ForMember(p => p.id, opt => opt.MapFrom(p => p.Id))
                .ForMember(p => p.name, opt => opt.MapFrom(p => p.Name));

            //Func<DataAvail.UralAppModel.Tag, Expression<Func<DataAvail.UralAppModel.Tag, Tag>>> func = (p) => Mapper.MapFunc<DataAvail.UralAppModel.Tag, Tag>();
            Expression<Func<DataAvail.UralAppModel.Tag, Tag>> func1 = x => new Tag { id = x.Id, name = x.Name };

            Mapper.CreateMap<DataAvail.UralAppModel.Product, Product>()
                //.ForMember(p => p.id, opt => opt.MapFrom(p => p.Id))
                //.ForMember(p => p.name, opt => opt.MapFrom(p => p.Name))
                //.ForMember(p => p.name, opt => opt.ResolveUsing(p => p.Name + "..."))
                //.ForMember(p => p.ProducerId, opt => opt.MapFrom(p => p.ProducerId))
                .ForMember(p => p.Tags, opt => opt.ResolveUsing(p => p.ProductTagMaps.AsQueryable().Select(s => s.Tag).Select(Mapper.MapExpression<UralAppModel.Tag, Tag>())));
                //.ForMember(p => p.Tags, opt => opt.ResolveUsing(p => Mapper.GetExpression<DataAvail.UralAppModel.Tag, Tag>(p.ProductTagMaps.Select(x => x.Tag))));

            /* new Tag { id = x.Tag.Id, name = x.Tag.Name } */

            DataAvail.UralAppModel.Model model = new UralAppModel.Model();
            var q = LinqMapper.Mapper.Map<DataAvail.UralAppModel.Product, Product>(model.Products);
            var r = q.ToArray();
        }

        public static Func<DataAvail.UralAppModel.Tag, Tag> GetExpr()
        {
            return x => new Tag { id = x.Id, name = x.Name };
        }
    }
}
