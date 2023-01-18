using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDB
{
    public class test1
    {
        MongoClient client;
        IMongoDatabase database;
        IMongoCollection<BsonDocument> collection;

        public test1()
        {

            client = new MongoClient("mongodb://localhost:27017");
            database = client.GetDatabase("TEstDB");
            collection = database.GetCollection<BsonDocument>("Urunler");


        }

        public async  Task CreateDocument()
        {

            var PruductDTO = new BsonDocument
            {
                { "name", "MongoDB" },
                { "type", "Database" },
                { "count", 1 },
                { "info", new BsonDocument
                    {
                        { "x", 203 },
                        { "y", 102 }
                    }
                }
            };

            PruductDTO.Add("ekelement", 123);
            PruductDTO.Add("Beytullah", "Büyüktolu");
            var dcname = PruductDTO.GetElement("name");
            var dcname1 = PruductDTO.GetElement("ekelement");
            var deneme3 = PruductDTO.GetValue("name");
            var deneme4 = PruductDTO.GetValue("ekelement").ToInt64();

            var cl = new MongoClient("mongodb://localhost:27017");
            var db = cl.GetDatabase("TestDB");
            var cll = db.GetCollection<BsonDocument>("Urunler");

            

            //var resdoc = await cll.FindAsync(new BsonDocument());

            await cll.InsertOneAsync(PruductDTO);

            var resdoc = await cll.Find(new BsonDocument())
                
                .FirstOrDefaultAsync();

            var reslist = await cll.Find(new BsonDocument())
                
                .ToListAsync();

            var aaa = resdoc.GetValue("ekelement");



        }




    }
}
