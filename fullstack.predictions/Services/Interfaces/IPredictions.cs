using fullstack.predictions.Models.Predictions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fullstack.predictions.Services.Interfaces
{
    public interface IPredictions
    {
        long CalculatePosibleDeathDate(ClientModel client);
    }
}
