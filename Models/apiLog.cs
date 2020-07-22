using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Leaf.Models
{
    public class ApiLog
    {
        private string id;
        private string method;
        private string scheme;
        private string host;
        private string path;
        private string queryString;
        private string remoteIPAdress;
        private string response;
        private string headers;
        private DateTime insertDate;
        private DateTime updateDate;

        private readonly IConfiguration _config;

        public string Id { get => id; set => id = value; }
        public string Method { get => method; set => method = value; }
        public string Scheme { get => scheme; set => scheme = value; }
        public string Host { get => host; set => host = value; }
        public string Path { get => path; set => path = value; }
        public string QueryString { get => queryString; set => queryString = value; }
        public string RemoteIPAdress { get => remoteIPAdress; set => remoteIPAdress = value; }
        public string Response { get => response; set => response = value; }
        public string Headers { get => headers; set => headers = value; }
        public DateTime InsertDate { get => insertDate; set => insertDate = value; }
        public DateTime UpdateDate { get => updateDate; set => updateDate = value; }

        public ApiLog(IConfiguration config)
        {
            _config = config;
        }

        public ApiLog(string id, IConfiguration config)
        {
            _config = config;
            LoadById(id);
        }

        private void LoadById(string id)
        {
            using (Leaf.Datalayers.ApiLog.DataLayer dl = new Leaf.Datalayers.ApiLog.DataLayer(_config))
            {
                System.Data.DataTable dt = dl.loadById(id);
                if (dt.Rows.Count == 1)
                {
                    this.id = dt.Rows[0]["id"].ToString();
                    Method = dt.Rows[0]["method"].ToString();
                    Scheme = dt.Rows[0]["scheme"].ToString();
                    Host = dt.Rows[0]["host"].ToString();
                    Path = dt.Rows[0]["path"].ToString();
                    QueryString = dt.Rows[0]["querystring"].ToString();
                    RemoteIPAdress = dt.Rows[0]["remoteipadress"].ToString();
                    Response = dt.Rows[0]["response"].ToString();
                    Headers = dt.Rows[0]["headers"].ToString();
                    InsertDate = DateTime.Parse(dt.Rows[0]["insertdate"].ToString());
                    UpdateDate = DateTime.Parse(dt.Rows[0]["updatedate"].ToString());
                }
                else
                {
                    throw new System.NullReferenceException("Log not found");
                }
            }
        }

        public ApiLog Create()
        {
            using (Leaf.Datalayers.ApiLog.DataLayer dl = new Leaf.Datalayers.ApiLog.DataLayer(_config))
            {


                if (dl.Create(this) == 1)
                {
                    LoadById(this.Id);
                    return this;
                }
                else
                {
                    return null;
                }
            }
        }

        public ApiLog Save()
        {
            if (!(this.Id != null || this.Id != ""))
            {
                throw new System.IO.InvalidDataException("Cannot save apilog without setted NHC");
            }

            using (Leaf.Datalayers.ApiLog.DataLayer dl = new Leaf.Datalayers.ApiLog.DataLayer(_config))
            {
                if (dl.Save(this) == 1)
                {
                    LoadById(this.Id);
                    return this;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}

