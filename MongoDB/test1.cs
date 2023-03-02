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

            //client = new MongoClient("mongodb://localhost:27017");
            //database = client.GetDatabase("TEstDB");
            //collection = database.GetCollection<BsonDocument>("Urunler");


        }

        public async Task AtlasTest()
        {
            var settings = MongoClientSettings.FromConnectionString("mongodb+srv://yurtseverozgur:oZ19751975@appcluster.cziqwqm.mongodb.net/?retryWrites=true&w=majority");
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("test");
            //---
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
            PruductDTO.Add("Burak", "Yılmaz");
            var dcname = PruductDTO.GetElement("name");
            var dcname1 = PruductDTO.GetElement("ekelement");
            var deneme3 = PruductDTO.GetValue("name");
            var deneme4 = PruductDTO.GetValue("ekelement").ToInt64();
            //---
            var Testdb = client.GetDatabase("TestDB");
            var Urunlercll = Testdb.GetCollection<BsonDocument>("Urunler");
            var Urunler1cll = Testdb.GetCollection<BsonDocument>("Urunler1");

            using (var session = await client.StartSessionAsync())
            {
                session.StartTransaction();

                await Urunlercll.InsertOneAsync(session, PruductDTO);
                await Urunler1cll.InsertOneAsync(session, PruductDTO);

                await session.CommitTransactionAsync();

            }

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
            PruductDTO.Add("Burak", "Yılmaz");
            var dcname = PruductDTO.GetElement("name");
            var dcname1 = PruductDTO.GetElement("ekelement");
            var deneme3 = PruductDTO.GetValue("name");
            var deneme4 = PruductDTO.GetValue("ekelement").ToInt64();

            //var cl = new MongoClient("mongodb://localhost:27017");
            //var cl = new MongoClient("mongodb://127.0.0.1:27017/?directConnection=true&serverSelectionTimeoutMS=2000&appName=mongosh+1.6.2");

            

            var cl = new MongoClient("mongodb://127.0.0.1:27017");



            var db = cl.GetDatabase("TestDB");
            var UrunlerCll = db.GetCollection<BsonDocument>("Urunler");



            //var resdoc = await c" ll.FindAsync(new BsonDocument());
            using (var session = await cl.StartSessionAsync())
            {
                session.StartTransaction();

                await UrunlerCll.InsertOneAsync(session,PruductDTO);

                await session.CommitTransactionAsync();

            }
            

            var resdoc = await UrunlerCll.Find(new BsonDocument())                
                .FirstOrDefaultAsync();

            var reslist = await UrunlerCll.Find(new BsonDocument())
                
                .ToListAsync();

            var aaa = resdoc.GetValue("ekelement");



        }




    }
}
