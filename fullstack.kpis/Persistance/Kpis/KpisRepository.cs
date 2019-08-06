using fullstack.Kpis.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fullstack.kpis.Persistance.Kpis
{
    public class KpisRepository : IKpisRepository
    {
        readonly MongoContext context;       
        public KpisRepository()
        {
            context = new MongoContext();
        }

        public List<Kpi> Get(int pageIndex, int pageSize)
        {
            return context.GetKpis().Find(_ => true).Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToList();
        }

        public Kpi Get(string kpiId)
        {
            return context.GetKpis().Find(Builders<Kpi>.Filter.Eq(x => x._id, kpiId)).FirstOrDefault();
        }

        public Kpi Get(DateTime date)
        {
            return context.GetKpis().Find(Builders<Kpi>.Filter.Eq(x => x.Fecha ,date.Date)).FirstOrDefault();
        }
        public void Create(Kpi Client)
        {
            var collection = context.GetKpis();
            collection.InsertOne(Client);
        }

        public bool Update(Kpi kpi)
        {
            var collection = context. GetKpis();
            var updateResult = collection.UpdateOne(
                    Builders<Kpi>.Filter.Eq(x => x._id, kpi._id),
                        Builders<Kpi>.Update
                           .Set(x => x.Fecha, kpi.Fecha.Date)
                           .Set(x => x.PromedioEdad, kpi.PromedioEdad)
                           .Set(x => x.DesviacionEstandar, kpi.DesviacionEstandar));

            var result = updateResult.IsAcknowledged && (updateResult.ModifiedCount == 1 || updateResult.ModifiedCount == 0);

            return result;
        }

        public bool Delete(Kpi kpi)
        {
            var collection = context.GetKpis();
            var deleteResult = collection.DeleteOne(
                    Builders<Kpi>.Filter.Eq(x => x._id, kpi._id));

            var result = deleteResult.IsAcknowledged && (deleteResult.DeletedCount == 1 || deleteResult.DeletedCount == 0);

            return result;
        }

        public long TotalKpis()
        {
            try
            {
                return context.GetKpis().CountDocuments(_ => true);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}