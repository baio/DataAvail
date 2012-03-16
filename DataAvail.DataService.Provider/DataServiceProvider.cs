using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Data.Services.Toolkit.Providers;
using Microsoft.Data.Services.Toolkit.QueryModel;
using System.Reflection;

namespace DataAvail.DataService.Provider
{
    /*
    public class PagingProvider : GenericPagingProvider
    {
        private static Dictionary<string, int> _dict = new[] { new { k = "CustomService.Item", v = 1 } }.ToDictionary(k => k.k, v => v.v);

        public PagingProvider(object DataSource)
            : base(_dict, DataSource)
        { }

        public override object[] GetContinuationToken(IEnumerator enumerator)
        {
            return base.GetContinuationToken(enumerator);
        }

        public override void SetContinuationToken(IQueryable query, ResourceType resourceType, object[] continuationToken)
        {
            base.SetContinuationToken(query, resourceType, continuationToken);
        }
    }
     */

    public abstract class DataServiceProvider : ODataContext
    {
        public DataServiceProvider(string RepositoryTypeTemplate)
            : this(null, RepositoryTypeTemplate)
        {
        }

        public DataServiceProvider(Assembly Assembly,  string RepositoryTypeTemplate)
        {
            _assembly = Assembly ?? Assembly.GetAssembly(this.GetType());
            _repositoryTypeTemplate = RepositoryTypeTemplate;
        }

        private readonly Assembly _assembly;
        private readonly string _repositoryTypeTemplate;

        public override object RepositoryFor(string fullTypeName)
        {

            string typeName = fullTypeName.Replace("[]", string.Empty).Substring(fullTypeName.LastIndexOf('.') + 1);
            Type repoType = _assembly.GetType(string.Format(_repositoryTypeTemplate , typeName));
            if (repoType == null) throw new NotSupportedException("The specified type is not an Entity inside the OData API");
            return Activator.CreateInstance(repoType);
        }
    }
}
