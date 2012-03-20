using System;
using System.Collections.Generic;
using System.Data.Services;
using System.Data.Services.Common;
using System.Linq;
using System.ServiceModel.Web;
using System.Web;
using DataAvail.DataService;
using System.ServiceModel;
using DataAvail.LinqMapper;
using DataAvail.UralAppService.Models;

namespace DataAvail.UralAppService
{
    [JSONPSupportBehavior]
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class Service : DataService<DataServiceProvider>
    {
        // Этот метод вызывается только один раз для инициализации серверных политик.
        public static void InitializeService(DataServiceConfiguration config)
        {
            // TODO: задайте правила, чтобы указать, какие наборы сущностей и служебные операции являются видимыми, обновляемыми и т.д.
            // Примеры:
            config.SetEntitySetAccessRule("*", EntitySetRights.All);
            // config.SetServiceOperationAccessRule("СлужебнаяОперация", ServiceOperationRights.All);
            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V3;
            config.DataServiceBehavior.AcceptProjectionRequests = true; //???

            //Mapping


            //Products
            Mapper.CreateMap<DataAvail.UralAppModel.Product, Product>()
                .ForMember(p => p.id, opt => opt.MapFrom(p => p.Id))
                .ForMember(p => p.name, opt => opt.MapFrom(p => p.Name))
                .ForMember(p => p.ProducerId, opt => opt.MapFrom(p => p.ProducerId))
                .ForMember(p => p.Tags, opt => opt.ResolveUsing(p => p.ProductTagMaps.Select(s => new Tag { id = s.Tag.Id, name = s.Tag.Name })));

            AutoMapper.Mapper.CreateMap<DataAvail.UralAppModel.Product, Product>()
                .ForMember(p => p.id, opt => opt.MapFrom(p => p.Id))
                .ForMember(p => p.name, opt => opt.MapFrom(p => p.Name));

            AutoMapper.Mapper.CreateMap<Product, DataAvail.UralAppModel.Product>()
                .ForMember(p => p.Id, opt => opt.MapFrom(p => p.id))
                .ForMember(p => p.Name, opt => opt.MapFrom(p => p.name));

            //Producers
            Mapper.CreateMap<DataAvail.UralAppModel.Producer, Producer>()
                .ForMember(p => p.id, opt => opt.MapFrom(p => p.Id))
                .ForMember(p => p.name, opt => opt.MapFrom(p => p.Name));

            AutoMapper.Mapper.CreateMap<DataAvail.UralAppModel.Producer, Producer>()
                .ForMember(p => p.id, opt => opt.MapFrom(p => p.Id))
                .ForMember(p => p.name, opt => opt.MapFrom(p => p.Name));

            AutoMapper.Mapper.CreateMap<Producer, DataAvail.UralAppModel.Producer>()
                .ForMember(p => p.Id, opt => opt.MapFrom(p => p.id))
                .ForMember(p => p.Name, opt => opt.MapFrom(p => p.name));

            //Tags
            Mapper.CreateMap<DataAvail.UralAppModel.Tag, Tag>()
                .ForMember(p => p.id, opt => opt.MapFrom(p => p.Id))
                .ForMember(p => p.name, opt => opt.MapFrom(p => p.Name));

            AutoMapper.Mapper.CreateMap<DataAvail.UralAppModel.Tag, Tag>()
                .ForMember(p => p.id, opt => opt.MapFrom(p => p.Id))
                .ForMember(p => p.name, opt => opt.MapFrom(p => p.Name));

            AutoMapper.Mapper.CreateMap<Tag, DataAvail.UralAppModel.Tag>()
                .ForMember(p => p.Id, opt => opt.MapFrom(p => p.id))
                .ForMember(p => p.Name, opt => opt.MapFrom(p => p.name));

        }

    }
}
