using System;
using System.Collections.Specialized;
using System.Xml;
using IBatisNet.Common.Utilities;
using IBatisNet.DataMapper;
using IBatisNet.DataMapper.Configuration;

namespace CML.Intercamber.Business.Helper
{

    /// <summary>
    /// Ibatis mapper singleton, usage : 
    /// IList list = Mapper.Instance().QueryForList("PermitNoForYearList", values);
    /// </summary>
    public class IBatisHelper
    {
        private static string connectionString = null;

        /// <summary>
        /// a appeller avant toute requete ! 
        /// </summary>
        /// <param name="c"></param>
        public static void RegisterConnectionString(string c)
        {
            connectionString = c;
        }

        private static volatile ISqlMapper _mapper = null;
        protected static void Configure(object obj)
        {
            _mapper = null;
        }

        /// <summary>
        /// chargement de SqlMap.config
        /// </summary>
        protected static void InitMapper()
        {
            if (connectionString == null)
                throw new Exception("Must register connection string first");

            NameValueCollection properties = new NameValueCollection(); 
            properties.Add("connectionString", connectionString);

            ConfigureHandler handler = new ConfigureHandler(Configure);
            DomSqlMapBuilder builder = new DomSqlMapBuilder();
            builder.Properties = properties;

            XmlDocument sqlMapConfig = Resources.GetEmbeddedResourceAsXmlDocument("CML.Intercamber.Business.IBatisConfig.SqlMap.config,IntercamberBusiness"); 
            _mapper = builder.Configure(sqlMapConfig); 
        }

        public static ISqlMapper Instance()
        {
            if (_mapper == null)
            {
                lock (typeof(SqlMapper))
                {
                    if (_mapper == null) // double-check
                    {
                        InitMapper();
                    }
                }
            }
            return _mapper;
        }
        public static ISqlMapper Get()
        {
            return Instance();
        }

    }
}