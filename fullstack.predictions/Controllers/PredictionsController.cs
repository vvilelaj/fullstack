using fullstack.predictions.Models.Predictions;
using fullstack.predictions.Services;
using fullstack.predictions.Services.Interfaces;
using fullstack.shared.Controllers;
using fullstack.shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace fullstack.predictions.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [RoutePrefix("api/predictions")]
    public class PredictionsController : BaseApiController
    {

        private readonly IPredictions predictionsService;

        public PredictionsController()
        {
            predictionsService = new PredictionsService();
        }

        /// <summary>
        /// Permite enviar los datos de un cliente para que realizar el calculo de la fecha probable de fallecimiento.
        /// </summary>
        /// <param name="client">Consta de nombre,apellido,edad y fecha de nacimiento</param>
        /// <returns>Objeto que contiene la cantidad de ticks para el calculo de la fecha en los consumidores</returns>
        [Route("PossibleDeathDate")]
        [HttpPost]
        public IHttpActionResult Post([FromBody] ClientModel client)
        {
            LogManager.AddInfo(DateTime.Now + $" BEGIN : PredictionsController.Post Nombre:{client.Nombre}, Apellido:{client.Apellido}, Edad:{ client.Edad} y Fecha de Nacimiento:{ client.FechaNacimiento}");

            var result = new JsonResultModel<long>() { Success = false }; ;

            var stopWatch = GetStopWatch();

            try
            {
                result.Result = predictionsService.CalculatePosibleDeathDate(client);
                result.Success = true;
                LogManager.AddInfo(DateTime.Now + $" SUCCESS : PredictionsController.Post Nombre:{client.Nombre}, Apellido:{client.Apellido}, Edad:{ client.Edad} y Fecha de Nacimiento:{ client.FechaNacimiento}");
                stopWatch.Stop();
            }
            catch (Exception ex)
            {
                stopWatch.Stop();
                LogManager.AddError("PredictionsController", "Post", string.Empty, string.Empty, stopWatch.ElapsedTicks, ex);
                //
                result.Message = ex.Message;
            }

            return Ok(result);
        }
    }
}
