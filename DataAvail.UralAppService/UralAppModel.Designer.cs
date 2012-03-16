﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Devart Entity Developer tool using Entity Framework EntityObject template.
// Code is generated on: 16.03.2012 17:40:54
//
// Changes to this file may cause incorrect behavior and will be lost if
// the code is regenerated.
//------------------------------------------------------------------------------

using System;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Data.EntityClient;
using System.ComponentModel;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Data;
using System.Linq;
using System.Data.Common;

[assembly: EdmSchemaAttribute()]
#region EDM Relationship Metadata


#endregion

namespace DataAvail.UralAppModel
{

    #region Model

    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public partial class Model : ObjectContext
    {
        #region Constructors

        /// <summary>
        /// Initialize a new Model object.
        /// </summary>
        public Model() : 
                base(@"name=UralAppModelConnectionString", "Model")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }

        /// <summary>
        /// Initializes a new Model object using the connection string found in the 'Model' section of the application configuration file.
        /// </summary>
        public Model(string connectionString) : 
                base(connectionString, "Model")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }

        /// <summary>
        /// Initialize a new Model object.
        /// </summary>
        public Model(EntityConnection connection) : base(connection, "Model")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }

        #endregion

        #region Partial Methods

        partial void OnContextCreated();

        #endregion

        #region ObjectSet Properties

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<Product> Products
        {
            get
            {
                if ((_Products == null))
                {
                    _Products = base.CreateObjectSet<Product>("Products");
                }
                return _Products;
            }
        }
        private ObjectSet<Product> _Products;

        #endregion
        #region AddTo Methods

        /// <summary>
        /// Deprecated Method for adding a new object to the Products EntitySet.
        /// </summary>
        public void AddToProducts(Product product)
        {
            base.AddObject("Products", product);
        }

        #endregion
    }

    #endregion

    #region Entity Classes

    #region Product

    /// <summary>
    /// There are no comments for DataAvail.UralAppModel.Product in the schema.
    /// </summary>
    /// <KeyProperties>
    /// Id
    /// </KeyProperties>
    [EdmEntityTypeAttribute(NamespaceName="DataAvail.UralAppModel", Name="Product")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Product : EntityObject
    {
        #region Factory Method

        /// <summary>
        /// Create a new Product object.
        /// </summary>
        /// <param name="id">Initial value of Id.</param>
        /// <param name="name">Initial value of Name.</param>
        public static Product CreateProduct(int id, string name)
        {
            Product product = new Product();
            product.Id = id;
            product.Name = name;
            return product;
        }

        #endregion

        #region Properties
    
    /// <summary>
    /// There are no comments for Id in the schema.
    /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public int Id
        {
            get
            {
                int value = _Id;
                OnGetId(ref value);
                return value;
            }
            set
            {
                if (_Id != value)
                {
                  OnIdChanging(ref value);
                  ReportPropertyChanging("Id");
                  _Id = StructuralObject.SetValidValue(value);
                  ReportPropertyChanged("Id");
                  OnIdChanged();
              }
            }
        }
        private int _Id;
        partial void OnGetId(ref int value);
        partial void OnIdChanging(ref int value);
        partial void OnIdChanged();
    
    /// <summary>
    /// There are no comments for Name in the schema.
    /// </summary>
        [EdmScalarPropertyAttribute(IsNullable=false)]
        [DataMemberAttribute()]
        public string Name
        {
            get
            {
                string value = _Name;
                OnGetName(ref value);
                return value;
            }
            set
            {
                if (_Name != value)
                {
                  OnNameChanging(ref value);
                  ReportPropertyChanging("Name");
                  _Name = StructuralObject.SetValidValue(value, false);
                  ReportPropertyChanged("Name");
                  OnNameChanged();
              }
            }
        }
        private string _Name;
        partial void OnGetName(ref string value);
        partial void OnNameChanging(ref string value);
        partial void OnNameChanged();

        #endregion
    }
    #endregion

    #endregion
}
