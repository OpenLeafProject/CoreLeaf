using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;

namespace Leaf.Data
{
    public class ConnectionDB
    {
        private static string LEAF_PRE;
        private static string LEAF_PRO;
        private static string LEAF_TESTING;

        private static string CONNECTIONSTRING;

        private readonly IConfiguration _config;

        public ConnectionDB(IConfiguration config)
        {
            _config = config;

            LEAF_PRE     = _config.GetValue<String>("ConnectionStrings:Debug");
            LEAF_PRO     = _config.GetValue<String>("ConnectionStrings:Prod");
            LEAF_TESTING = _config.GetValue<String>("ConnectionStrings:Testing");

            if (_config.GetValue<Boolean>("Run:Testing"))
            {
                CONNECTIONSTRING = LEAF_TESTING;
            } 
            else if (_config.GetValue<Boolean>("Run:Production"))
            {
                CONNECTIONSTRING = LEAF_PRO;
            } else
            {
                CONNECTIONSTRING = LEAF_PRE;
            }
        }
        
        public String getConnectString()
        {
            Debug.Write($"[Connecting with: '{CONNECTIONSTRING}]'");
            return CONNECTIONSTRING;
        }
    }
}
