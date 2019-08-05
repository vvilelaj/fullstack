using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fullstack.predictions.Models.Predictions
{
    public class ClientModel
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int Edad { get; set; }
    }
}