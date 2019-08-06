using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fullstack.Kpis.Models
{
    /// <summary>
    /// Entidad Cliente
    /// Almacena la informacion de un cliente
    /// La edad se calcula automaticamente segun la fecha de nacimiento y la hora actual del servidor.
    /// </summary>
    public class Kpi
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public DateTime Fecha { get; set; }
        public decimal PromedioEdad { get; set; }
        public decimal DesviacionEstandar { get; set; }
    }
}