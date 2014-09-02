using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Cdts.Utility.Data
{
    public class MongoDBHelper : IDisposable
    {
        private static string conn = ConfigurationManager.AppSettings["MongoDBConn"];
        private MongoServer server = null;
        private MongoDatabase db = null;
        private MongoCollection collection = null;

        public MongoDBHelper()
        {
            server = MongoServer.Create(conn);
        }


        public void GetDB(string name)
        {

            db = server.GetDatabase(name);
        }

        public void GetCollection(string name)
        {
            if (db == null) return;
            collection = db.GetCollection(name);
        }

        public void Insert(Dictionary<string, object> dicts)
        {
            if (collection == null) return;
            BsonDocument entity = new BsonDocument();
            foreach (KeyValuePair<string, object> item in dicts)
            {
                string name = item.Key;
                object value = item.Value;
                entity.Add(name, BsonValue.Create(value));
            }
            collection.Insert(entity);
        }

        public void Update(Dictionary<string, object> dicts, string key)
        {
            Delete(key, dicts[key]);
            Insert(dicts);
        }

        public void Delete(string key, object value)
        {
            if (collection == null) return;
            QueryDocument query = new QueryDocument(key, BsonValue.Create(value));
            collection.Remove(query);
        }


        public List<Dictionary<string, object>> Query(string key, object value)
        {
            if (collection == null) return null;
            QueryDocument query = new QueryDocument(key, BsonValue.Create(value));
            MongoCursor<BsonDocument> cursor = collection.FindAs<BsonDocument>(query);
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            if (cursor != null)
            {
                foreach (BsonDocument item in cursor)
                {
                    list.Add(item.ToDictionary());
                }
            }
            return list;
        }

        public List<Dictionary<string, object>> Query(string key, object value, string orderKey)
        {
            if (collection == null) return null;
            QueryDocument query = new QueryDocument(key, BsonValue.Create(value));
            var array = collection.FindAs<BsonDocument>(query).OrderBy(q => q[orderKey]).ToArray<BsonDocument>();
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            if (array != null)
            {
                foreach (BsonDocument item in array)
                {
                    list.Add(item.ToDictionary());
                }
            }
            return list;
        }
        public List<Dictionary<string, object>> Query(string key, object value, int orderNo)
        {
            if (collection == null) return null;
            QueryDocument query = new QueryDocument(key, BsonValue.Create(value));
            var array = collection.FindAs<BsonDocument>(query).OrderBy(q => q[orderNo]).ToArray<BsonDocument>();
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            if (array != null)
            {
                foreach (BsonDocument item in array)
                {
                    list.Add(item.ToDictionary());
                }
            }
            return list;
        }

        public void DropCollection()
        {
            if (collection == null) return;
            collection.Drop();
        }

        public void Dispose()
        {
            if (server != null)
            {
                server.Disconnect();
            }
        }
    }
}
