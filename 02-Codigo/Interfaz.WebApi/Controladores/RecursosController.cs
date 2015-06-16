using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using peticionApi = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Peticion;
using app = Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Fachada;
using respuestaApi = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Respuesta;
using System.Net.Http.Headers;
using utilitario = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Utilitarios;
using comunes = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Comunes;

namespace Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Controladores
{
    [RoutePrefix("api/recursos")]
    public class RecursosController : ApiController
    {

        private readonly app.IGeneradorDeRecursosDeTraduccion aplicacionGenerarRecursos;


        #region Constructor de la clase

        public RecursosController(app.IGeneradorDeRecursosDeTraduccion aplicacionGenerarRecursos)
        {
            // TODO: Complete member initialization
            this.aplicacionGenerarRecursos = aplicacionGenerarRecursos;
        }
        #endregion

        [Route("dicionario/ambiente/{ambiente}")]
        [HttpGet]
        public HttpResponseMessage SolicitarGenerarArchivoDeRecursosPorAmbienteDeDiccionario(string ambiente, HttpRequestMessage peticionHttp)
        {
            //Se instancia el modelo de peticion WebApi como referencia del modelo de peticion de la aplicación 
            var peticionWeb = peticionApi.GenerarRecursosPorIdiomaPeticion.CrearNuevaPeticion(ambiente, peticionHttp);

            // Se llama al metodo de la interfaz IAplicacionMantenimientoDeDiccionario
            var respuestaApp = aplicacionGenerarRecursos.GenerarRecursos(peticionWeb.AppGenerarRecursos);

            //Se solicita cargar el modelo de respuesta del WebApi con la respuesta del metodo fachada de la aplicación
            var respuestaContenido = respuestaApi.GenerarRecursosPorIdiomaRespuesta.CrearNuevaRespuesta(respuestaApp);

            //var respuestaConUriRecurso = this.respuestaGeneracionRecurso.GenerarRecursosPorIdioma(respuestaContenido, peticionWeb.formatoAceptadoPorElCliente);

            return Request.CreateResponse(HttpStatusCode.OK, new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}