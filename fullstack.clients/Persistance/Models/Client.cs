using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fullstack.clients.Models
{
    /// <summary>
    /// Entidad Cliente
    /// Almacena la informacion de un cliente
    /// La edad se calcula automaticamente segun la fecha de nacimiento y la hora actual del servidor.
    /// </summary>
    public class Client
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int Edad
        {
            get
            {
                var now = DateTime.Now;
                var totalDays = (now - FechaNacimiento).TotalDays;
                return Convert.ToInt32(totalDays < 0 ? 0 : Math.Floor(totalDays / 365));
            }
        }
        public DateTime FechaProbableMuerte { get; set; }
    }
}