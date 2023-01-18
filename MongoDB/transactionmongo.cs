using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDB
{


    public class transactionmongo
    {
        MongoClient client;
        IMongoDatabase database;
        IMongoCollection<BsonDocument> collection;

        public transactionmongo()
        {

            client = new MongoClient("mongodb://localhost:27017");
            database = client.GetDatabase("foo");
            collection = database.GetCollection<BsonDocument>("bar");

        }


        public async Task startTrn()
        {

            using (var session = await client.StartSessionAsync())
            {
                // Begin transaction
                session.StartTransaction();

                try
                {



                    var tv = new BsonDocument
                    {
                        { "Description", "Television" },
                        { "SKU", 4001 },
                        { "Price", 2000 },
                    };

                    var book = new BsonDocument
                    {
                        { "Description" , "A funny book" },
                        { "SKU", 43221 },
                        { "Price", 19.99 }
                    };

                    var dogBowl = new BsonDocument
                    {
                        { "Description" , "Bowl for Fido" },
                        { "SKU", 123 },
                        { "Price", 40 }
                    };

                    await collection.InsertOneAsync(session, tv);
                    await collection.InsertOneAsync(session, book);
                    await collection.InsertOneAsync(session, dogBowl);

                    var resultsBeforeUpdates = await collection
                                       .Find<BsonDocument>(session, Builders<BsonDocument>.Filter.Empty)
                                       .ToListAsync();

                    // Increase all the prices by 10% for all products
                    var update = new UpdateDefinitionBuilder<BsonDocument>()
                            .Mul<Double>(r => r.GetValue("Price").AsDouble, 1.1);

                    await collection.UpdateManyAsync(session,
                            Builders<BsonDocument>.Filter.Empty,
                            update);

                    await session.CommitTransactionAsync();
                }
                catch (Exception)
                {
                    await session.AbortTransactionAsync();

                }

            }


        }
    }

}




