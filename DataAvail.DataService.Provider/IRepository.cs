using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;

namespace DataAvail.DataService.Provider
{
    internal interface IRepository
    {
        void SetContext(ObjectContext Context);
    }
}
