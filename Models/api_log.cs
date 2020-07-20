using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Leaf.Models
{
    public class Api_log
    {
        private string id;
        private string method;
        private string scheme;
        private string host;
        private string path;
        private string queryString;
        private string remoteIPAddress;
        private string response;
        private string headers;

        public string Id { get => id; set => id = value; }
        public string Method { get => method; set => method = value; }
        public string Scheme { get => scheme; set => scheme = value; }
        public string Host { get => host; set => host = value; }
        public string Path { get => path; set => path = value; }
        public string QueryString { get => queryString; set => queryString = value; }
        public string RemoteIPAddress { get => remoteIPAddress; set => remoteIPAddress = value; }
        public string Response { get => response; set => response = value; }
        public string Headers { get => headers; set => headers = value; }
    }
}
