using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;

namespace Villain
{
    public class VillainDAO : IVillainDAO
    {
        private IMongoDatabase _database;

        public VillainDAO(IMongoClient client)
        {
            _database = client.GetDatabase("CloudFoundry_oumhg86d_nq48b1sc");
        }

        public List<Villain> GetVillains()
        {
            var collection = _database.GetCollection<Villain>("villain");
            var filter = Builders<Villain>.Filter.Empty;
            var result = collection.Find(filter).ToList();

            return result;
        }

        public Villain GetVillain(string name)
        {
            var collection = _database.GetCollection<Villain>("villain");
            var builder = Builders<Villain>.Filter;
            var filter = builder.Where(m => m.Name == name);
            var result = collection.Find(filter).FirstOrDefault();

            return result;
        }

        public Villain InsertVillain(Villain villain)
        {
            var collection = _database.GetCollection<Villain>("villain");
            collection.InsertOne(villain);

            return villain;
        }

        public Villain UpdateVillain(Villain villain)
        {
            var collection = _database.GetCollection<Villain>("villain");
            var builder = Builders<Villain>.Filter;
            var filter = builder.Where(m => m.Name == villain.Name);
            var update = Builders<Villain>.Update
                .Set("name", villain.Name)
                .Set("edition", villain.Edition)
                .Set("rules", villain.Rules);
                
            collection.UpdateOne(filter, update);
            
            return villain;
        }
    }
}