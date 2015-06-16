using System;
using System.Net.Http;
using System.Web.Http;
using System.Net;
using Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Models.Response;
using System.Net.Http.Headers;



namespace  Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Controllers
{
	public class DiccionariosController : ApiController
	{
		[HttpGet]
		[Route ("api/diccionarios")]
		public HttpResponseMessage ObtenerTodosLosDiccionarios ()
		{
			//Obtener contenido respuesta
			var responsemodel = new ObtenerTodosLosDiccionariosResponse ();
			//Preparar respuesta
			HttpResponseMessage response = Request.CreateResponse (HttpStatusCode.OK, responsemodel, new MediaTypeWithQualityHeaderValue ("application/json"));
			//Retornar respuesta
			return response;
		}

		[HttpGet]
		[Route ("api/diccionario/id/{id}")]
		public HttpResponseMessage ObtenerUnDiccionarioEspecifico (int id)
		{
			HttpResponseMessage response;
			var responsemodel = new ObtenerUnDiccionarioEspecificoResponse ();
			response = id == 1 ? Request.CreateResponse (HttpStatusCode.OK, responsemodel, new MediaTypeWithQualityHeaderValue ("application/json")) : Request.CreateResponse (HttpStatusCode.NotFound, responsemodel, new MediaTypeWithQualityHeaderValue ("application/json"));
			return response;
		}
	}

}

