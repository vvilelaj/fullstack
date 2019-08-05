using fullstack.clients.Models;
using fullstack.clients.Services;
using fullstack.clients.Services.Interfaces;
using fullstack.shared.Controllers;
using fullstack.shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace fullstack.clients.Controllers
{
    [RoutePrefix("api/clients")]
    public class ClientsController : BaseApiController
    {
        private readonly IClientsService clientsService;

        public ClientsController()
        {
            clientsService = new ClientsService();
        }

        /// <summary>
        /// Exponer los clientes registrados en la colección
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpGet]
        public IHttpActionResult  Get()
        {
            var result = new JsonResultModel<PagedResultModel<Client>>() { Success = false }; ;

            var stopWatch = GetStopWatch();

            try
            {
                result.Result = clientsService.Get(1, 50);
                result.Success = true;
                stopWatch.Stop();
                LogManager.AddInfo("ClientsController.GET");
            }
            catch (Exception ex)
            {
                stopWatch.Stop();
                LogManager.AddError("ClientsController", "Get", string.Empty, string.Empty, stopWatch.ElapsedTicks, ex);

                result.Message = ex.Message;
            }

            return Ok(result);
        }

        [Route("")]
        [HttpGet]
        public IHttpActionResult Get(int pageIndex, int pageSize)
        {
            LogManager.AddInfo(DateTime.Now + $" BEGIN : ClientsController.GET pageIndex:{pageIndex}  pageSize:{pageSize}");

            var result = new JsonResultModel<PagedResultModel<Client>>() { Success = false }; ;

            var stopWatch = GetStopWatch();

            try
            {
                result.Result = clientsService.Get(pageIndex, pageSize);
                result.Success = true;
                LogManager.AddInfo(DateTime.Now + $" SUCCESS : ClientsController.GET pageIndex:{pageIndex}  pageSize:{pageSize}");
                stopWatch.Stop();
            }
            catch (Exception ex)
            {
                stopWatch.Stop();
                LogManager.AddError("ClientsController", "Get", string.Empty, string.Empty, stopWatch.ElapsedTicks, ex);

                result.Message = ex.Message;
            }

            return Ok(result);
        }

        /// <summary>
        /// Permite persistir un cliente en la colección
        /// </summary>
        /// <param name="client">Consta de nombre,apellido y fecha de nacimiento</param>
        /// <returns></returns>
        [Route("")]
        public IHttpActionResult Post([FromBody] Client client)
        {
            LogManager.AddInfo(DateTime.Now + $" BEGIN : ClientsController.Post Nombre:{client.Nombre}, Apellido:{client.Apellido}");
            var result = new JsonResultModel<string>() { Success = false }; ;

            var stopWatch = GetStopWatch();

            try
            {
                clientsService.Create(client);
                result.Success = true;
                LogManager.AddInfo(DateTime.Now + $" SUCCESS : ClientsController.Post Nombre:{client.Nombre}, Apellido:{client.Apellido}");
                stopWatch.Stop();
            }
            catch (Exception ex)
            {
                stopWatch.Stop();
                LogManager.AddError("ClientsController", "Post", string.Empty , string.Empty, stopWatch.ElapsedTicks, ex);
                //
                result.Message = ex.Message;
            }

            return Ok(result);
        }

        /// <summary>
        /// Permite actualizar un cliente mediante su id.
        /// </summary>
        /// <param name="clientId">id de cliente para la actualización</param>
        /// <param name="client">la informacion del cliente que sera actualizada</param>
        /// <returns></returns>
        [Route("{clientId}")]
        [HttpPatch]
        public IHttpActionResult Patch(string clientId, [FromBody] Client client)
        {
            LogManager.AddInfo(DateTime.Now + $" BEGIN : ClientsController.patch clientId:{clientId}");
            var result = new JsonResultModel<string>() { Success = false }; ;

            var stopWatch = GetStopWatch();

            try
            {
                clientsService.Update(clientId, client);
                result.Success = true;
                LogManager.AddInfo(DateTime.Now + $" BEGIN : ClientsController.patch clientId:{clientId}");
                stopWatch.Stop();
            }
            catch (Exception ex)
            {
                stopWatch.Stop();
                LogManager.AddError("ClientsController", "patch", string.Empty, string.Empty, stopWatch.ElapsedTicks, ex);
                //
                result.Message = ex.Message;
            }

            return Ok(result);
        }

        /// <summary>
        /// Permite eliminar un cliente de la colección mediante su id
        /// </summary>
        /// <param name="clientId">id de cliente para la eliminación</param>
        /// <returns></returns>
        [Route("{clientId}")]
        [HttpDelete]
        public IHttpActionResult Delete(string clientId)
        {
            LogManager.AddInfo(DateTime.Now + $" BEGIN : ClientsController.delete clientId:{clientId}");
            var result = new JsonResultModel<string>() { Success = false }; ;

            var stopWatch = GetStopWatch();

            try
            {
                clientsService.Delete(clientId);
                result.Success = true;
                LogManager.AddInfo(DateTime.Now + $" BEGIN : ClientsController.delete clientId:{clientId}");
                stopWatch.Stop();
            }
            catch (Exception ex)
            {
                stopWatch.Stop();
                LogManager.AddError("ClientsController", "delete", string.Empty, string.Empty, stopWatch.ElapsedTicks, ex);
                //
                result.Message = ex.Message;
            }

            return Ok(result);
        }
    }
}
