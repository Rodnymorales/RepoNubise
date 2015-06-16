using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using app = Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Fachada;
using peticionApi = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Peticion;
using respuestaApi = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Respuesta;

namespace Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Controladores
{
    [RoutePrefix("api")]
    public class TraduccionesController : ApiController
    {
        #region propiedades y variables globales
        private readonly app.IAdministradorDeTraducciones aplicacionMantenimientoDiccionario;

        #endregion

        #region Constructor de la clase
        public TraduccionesController(app.IAdministradorDeTraducciones aplicacionMantenimientoDiccionario)
        {
            this.aplicacionMantenimientoDiccionario = aplicacionMantenimientoDiccionario;
        }
        #endregion

        #region Metodos de consulta (GET)
        
        #endregion

        #region Metodos de inserción (POST)
        [Route("diccionario/id/{id1}/etiqueta/id/{id2}/traducciones")]
        [HttpPost]
        public HttpResponseMessage AgregarTraduccionesAUnaEtiquetaDeUnDiccionario(string id1, string id2, HttpRequestMessage peticionHttp)
        {
            //Se instancia el modelo de peticion WebApi como referencia del modelo de peticion de la aplicación 
            var peticionWeb = peticionApi.AgregarTraduccionesAUnaEtiquetaDeUnDiccionarioPeticion.CrearNuevaPeticion(id1,id2,peticionHttp);

            if (peticionWeb.Respuesta != string.Empty)
               return Request.CreateErrorResponse(HttpStatusCode.BadRequest, peticionWeb.Respuesta);

            // Se llama al metodo de la interfaz IAdministradorDeTraducciones
            var respuestaApp = this.aplicacionMantenimientoDiccionario.AgregarTraduccionesAUnaEtiquetaDeUnDiccionario(peticionWeb.AppEtiquetasDiccionarioPeticion);

            if (respuestaApp.Relaciones["diccionario"] == new Guid("00000000-0000-0000-0000-000000000000"))
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Diccionario no encontrado");

            if (respuestaApp.Relaciones["etiqueta"] == new Guid("00000000-0000-0000-0000-000000000000"))
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Etiqueta no encontrada");

            //Se solicita cargar el modelo de respuesta del WebApi con la respuesta del metodo fachada de la aplicación
            var respuestaContenido = respuestaApi.AgregarTraduccionesAUnaEtiquetaDeUnDiccionarioRespuesta.CrearNuevaRespuesta(respuestaApp);

            return Request.CreateResponse(HttpStatusCode.Created, respuestaContenido, new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [Route("diccionario/id/{id1}/etiqueta/id/{id2}/traducciones")]
        [HttpDelete]
        public HttpResponseMessage EliminarTraduccionesAUnaEtiquetaDeunDiccionario(string id1, string id2, HttpRequestMessage peticionHttp)
        {
            //Se instancia el modelo de peticion WebApi como referencia del modelo de peticion de la aplicación 
            var peticionWeb = peticionApi.EliminarTraduccionesAUnaEtiquetaDeUnDiccionarioPeticion.CrearNuevaPeticion(id1, id2,peticionHttp);

            if (peticionWeb.Respuesta != string.Empty)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, peticionWeb.Respuesta);

            // Se llama al metodo de la interfaz IAdministradorDeTraducciones
            var respuestaApp = this.aplicacionMantenimientoDiccionario.EliminarTraduccionesAUnaEtiquetaDeUnDiccionario(peticionWeb.AppEtiquetasDiccionarioPeticion);

            if (respuestaApp.Relaciones["diccionario"] == new Guid("00000000-0000-0000-0000-000000000000"))
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Diccionario no encontrado");

            if (respuestaApp.Relaciones["etiqueta"] == new Guid("00000000-0000-0000-0000-000000000000"))
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Etiqueta no encontrada");

            //Se solicita cargar el modelo de respuesta del WebApi con la respuesta del metodo fachada de la aplicación
            var respuestaContenido = respuestaApi.EliminarTraduccionesAUnaEtiquetaDeUnDiccionarioRespuesta.CrearNuevaRespuesta(respuestaApp);

            return Request.CreateResponse(HttpStatusCode.OK, respuestaContenido, new MediaTypeWithQualityHeaderValue("application/json"));
        }
        #endregion

        #region Metodos de Modificacion (PUT)

        [Route("diccionario/id/{id1}/etiqueta/id/{id2}/traducciones")]
        [HttpPut]
        public HttpResponseMessage ModificarTraduccionesAUnaEtiquetaDeUnDiccionario(string id1, string id2, HttpRequestMessage peticionHttp)
        {
            //Se instancia el modelo de peticion WebApi como referencia del modelo de peticion de la aplicación 
            var peticionWeb = peticionApi.ModificarTraduccionesAUnaEtiquetaDeUnDiccionarioPeticion.CrearNuevaPeticion(id1, id2, peticionHttp);

            if (peticionWeb.Respuesta != string.Empty)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, peticionWeb.Respuesta);

            // Se llama al metodo de la interfaz IAdministradorDeTraducciones
            var respuestaApp = this.aplicacionMantenimientoDiccionario.ModificarTraduccionesAUnaEtiquetaDeUnDiccionario(peticionWeb.AppEtiquetasDiccionarioPeticion);

            if (respuestaApp.Relaciones["diccionario"] == new Guid("00000000-0000-0000-0000-000000000000"))
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Diccionario no encontrado");

            if (respuestaApp.Relaciones["etiqueta"] == new Guid("00000000-0000-0000-0000-000000000000"))
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Etiqueta no encontrada");

            //Se solicita cargar el modelo de respuesta del WebApi con la respuesta del metodo fachada de la aplicación
            var respuestaContenido = respuestaApi.ModificarTraduccionesAUnaEtiquetaDeUnDiccionarioRespuesta.CrearNuevaRespuesta(respuestaApp);

            return Request.CreateResponse(HttpStatusCode.OK, respuestaContenido, new MediaTypeWithQualityHeaderValue("application/json"));
        }
        #endregion


    }
}
