using fullstack.clients.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Web;

namespace fullstack.clients.Persistance
{
    public class MongoContext
    {
        private string userName = "vvilelaj";
        private string host = "vvilelaj.documents.azure.com";
        private int port = 10255;
        private string password = "6zK9DVpJ6kcpGkf8XLbBEYIjXsNQgECpNuI2ChEQB0Wtpfabjrx8H5gN5GigmzEgXQdxRL3PwEmaAY8JxwrxCA==";
        private string dbName = "vvilelaj";

        public MongoContext()
        {

        }

        private IMongoDatabase GetDataBase()
        {
            MongoClientSettings settings = new MongoClientSettings();
            settings.Server = new MongoServerAddress(host, port);
            settings.UseSsl = true;
            settings.SslSettings = new SslSettings();
            settings.SslSettings.EnabledSslProtocols = SslProtocols.Tls12;

            MongoIdentity identity = new MongoInternalIdentity(dbName, userName);
            MongoIdentityEvidence evidence = new PasswordEvidence(password);

            settings.Credential = new MongoCredential("SCRAM-SHA-1", identity, evidence);

            MongoClient client = new MongoClient(settings);
            var database = client.GetDatabase(dbName);
            return database;
        }

        public IMongoCollection<Client> GetClients()
        {
            return GetDataBase().GetCollection<Client>("Clients");
        }

        

    }
}