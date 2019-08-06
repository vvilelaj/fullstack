using fullstack.clients.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fullstack.clients.Persistance.Clients
{
    public class ClientsRepository : IClientsRepository
    {
        readonly MongoContext context;       
        public ClientsRepository()
        {
            context = new MongoContext();
        }

        public List<Client> Get(int pageIndex, int pageSize)
        {
            return context.GetClients().Find(_ => true).Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToList();
        }

        public Client Get(string clientId)
        {
            return context.GetClients().Find(Builders<Client>.Filter.Eq(x => x._id, clientId)).FirstOrDefault();
        }

        public void Create(Client Client)
        {
            var collection = context.GetClients();
            collection.InsertOne(Client);
        }

        public bool Update(Client Client)
        {
            var collection = context. GetClients();
            var updateResult = collection.UpdateOne(
                    Builders<Client>.Filter.Eq(x => x._id, Client._id),
                        Builders<Client>.Update
                           .Set(x => x.Nombre, Client.Nombre)
                           .Set(x => x.Apellido, Client.Apellido)
                           .Set(x => x.FechaNacimiento, Client.FechaNacimiento)
                           .Set(x => x.FechaProbableMuerte, Client.FechaProbableMuerte)
                           );

            var result = updateResult.IsAcknowledged && (updateResult.ModifiedCount == 1 || updateResult.ModifiedCount == 0);

            return result;
        }

        public bool Delete(Client Client)
        {
            var collection = context.GetClients();
            var deleteResult = collection.DeleteOne(
                    Builders<Client>.Filter.Eq(x => x._id, Client._id));

            var result = deleteResult.IsAcknowledged && (deleteResult.DeletedCount == 1 || deleteResult.DeletedCount == 0);

            return result;
        }

        public long TotalClients()
        {
            try
            {
                return context.GetClients().CountDocuments(_ => true);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}