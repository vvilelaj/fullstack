
using fullstack.kpis.Services;
using fullstack.kpis.Services.Interfaces;
using fullstack.Kpis.Models;
using fullstack.shared.Controllers;
using fullstack.shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace fullstack.kpis.Controllers
{
    [RoutePrefix("api/kpis")]
    public class KpisController : BaseApiController
    {
        private readonly IKpisService kpisService;

        public KpisController()
        {
            kpisService = new KpisService();
        }

        /// <summary>
        /// Exponer los kpis registrados en la colección
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpGet]
        public IHttpActionResult  Get()
        {
            var result = new JsonResultModel<PagedResultModel<Kpi>>() { Success = false }; ;

            var stopWatch = GetStopWatch();

            try
            {
                result.Result = kpisService.Get(1, 50);
                result.Success = true;
                stopWatch.Stop();
                LogManager.AddInfo("KpisController.GET");
            }
            catch (Exception ex)
            {
                stopWatch.Stop();
                LogManager.AddError("KpisController", "Get", string.Empty, string.Empty, stopWatch.ElapsedTicks, ex);

                result.Message = ex.Message;
            }

            return Ok(result);
        }

        [Route("")]
        [HttpGet]
        public IHttpActionResult Get(int pageIndex, int pageSize)
        {
            LogManager.AddInfo(DateTime.Now + $" BEGIN : KpisController.GET pageIndex:{pageIndex}  pageSize:{pageSize}");

            var result = new JsonResultModel<PagedResultModel<Kpi>>() { Success = false }; ;

            var stopWatch = GetStopWatch();

            try
            {
                result.Result = kpisService.Get(pageIndex, pageSize);
                result.Success = true;
                LogManager.AddInfo(DateTime.Now + $" SUCCESS : KpisController.GET pageIndex:{pageIndex}  pageSize:{pageSize}");
                stopWatch.Stop();
            }
            catch (Exception ex)
            {
                stopWatch.Stop();
                LogManager.AddError("KpisController", "Get", string.Empty, string.Empty, stopWatch.ElapsedTicks, ex);

                result.Message = ex.Message;
            }

            return Ok(result);
        }

        /// <summary>
        /// Permite persistir un kpi en la colección
        /// </summary>
        /// <param name="kpi">Consta de Fecha, Promedio de Edad y Desviación Estandar de Edad</param>
        /// <returns></returns>
        [Route("")]
        public IHttpActionResult Post([FromBody] Kpi kpi)
        {
            LogManager.AddInfo(DateTime.Now + $" BEGIN : KpisController.Post Fecha:{kpi.Fecha}, PromedioEdad:{kpi.PromedioEdad}, DesviacionEstandar:{kpi.DesviacionEstandar}");
            var result = new JsonResultModel<string>() { Success = false }; ;

            var stopWatch = GetStopWatch();

            try
            {
                kpisService.Create(kpi);
                result.Success = true;
                LogManager.AddInfo(DateTime.Now + $" SUCCESS : KpisController.Post Fecha:{kpi.Fecha}, PromedioEdad:{kpi.PromedioEdad}, DesviacionEstandar:{kpi.DesviacionEstandar}");
                stopWatch.Stop();
            }
            catch (Exception ex)
            {
                stopWatch.Stop();
                LogManager.AddError("KpisController", "Post", string.Empty , string.Empty, stopWatch.ElapsedTicks, ex);
                //
                result.Message = ex.Message;
            }

            return Ok(result);
        }

        /// <summary>
        /// Permite actualizar un Kpi mediante su id.
        /// </summary>
        /// <param name="kpiId">id de kpi para la actualización</param>
        /// <param name="client">la informacion del kpi que sera actualizada</param>
        /// <returns></returns>
        [Route("{kpiId}")]
        [HttpPatch]
        public IHttpActionResult Patch(string kpiId, [FromBody] Kpi client)
        {
            LogManager.AddInfo(DateTime.Now + $" BEGIN : KpisController.patch kpiId:{kpiId}");
            var result = new JsonResultModel<string>() { Success = false }; ;

            var stopWatch = GetStopWatch();

            try
            {
                kpisService.Update(kpiId, client);
                result.Success = true;
                LogManager.AddInfo(DateTime.Now + $" BEGIN : KpisController.patch kpiId:{kpiId}");
                stopWatch.Stop();
            }
            catch (Exception ex)
            {
                stopWatch.Stop();
                LogManager.AddError("KpisController", "patch", string.Empty, string.Empty, stopWatch.ElapsedTicks, ex);
                //
                result.Message = ex.Message;
            }

            return Ok(result);
        }

        /// <summary>
        /// Permite eliminar un kpiId de la colección mediante su id
        /// </summary>
        /// <param name="kpiId">id de kpiId para la eliminación</param>
        /// <returns></returns>
        [Route("{kpiId}")]
        [HttpDelete]
        public IHttpActionResult Delete(string kpiId)
        {
            LogManager.AddInfo(DateTime.Now + $" BEGIN : KpisController.delete kpiId:{kpiId}");
            var result = new JsonResultModel<string>() { Success = false }; ;

            var stopWatch = GetStopWatch();

            try
            {
                kpisService.Delete(kpiId);
                result.Success = true;
                LogManager.AddInfo(DateTime.Now + $" BEGIN : KpisController.delete kpiId:{kpiId}");
                stopWatch.Stop();
            }
            catch (Exception ex)
            {
                stopWatch.Stop();
                LogManager.AddError("KpisController", "delete", string.Empty, string.Empty, stopWatch.ElapsedTicks, ex);
                //
                result.Message = ex.Message;
            }

            return Ok(result);
        }
    }
}
