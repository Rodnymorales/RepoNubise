using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System;
using respuestaApi = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Respuesta;
using peticionApi = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Peticion;
using app = Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Fachada;
using modelosApp = Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Peticion;
using System.Net;
using Dominio = Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.Entidades.Diccionario;
using Newtonsoft.Json;

//Pruebas DBAccess 3: Cambios en el codigo
//Pruebas DBAccess 4: Cambios para solicitar Pull Request

namespace Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Controladores
{
    [RoutePrefix("api")]
	public class DiccionariosController : ApiController
    {
        #region propiedades y variables globales
        private readonly app.IAdministradorDeDiccionarios aplicacionMantenimientoDiccionario;

        #endregion

        #region Constructor de la clase
        public DiccionariosController(app.IAdministradorDeDiccionarios aplicacionMantenimientoDiccionario)
        {
            this.aplicacionMantenimientoDiccionario = aplicacionMantenimientoDiccionario;
        }
        #endregion

        #region Metodos GET
        [Route("diccionarios")]
        [HttpGet]
        public HttpResponseMessage ObtenerTodosDiccionarios()
        {


            var respuestaApp = this.aplicacionMantenimientoDiccionario.ConsultarDiccionarios();
            var respuestaContenido = respuestaApi.ConsultarDiccionariosRespuesta.CrearNuevaRespuestaConRespuestaDeAplicacion(respuestaApp);

            return Request.CreateResponse(HttpStatusCode.OK, respuestaContenido, new MediaTypeWithQualityHeaderValue("application/json"));

        }

        [Route("diccionario/id/{id}")]
        [HttpGet]
        public HttpResponseMessage ConsultarUnDiccionario(HttpRequestMessage peticionHttp, string id)
        {
            //Se instancia el modelo de peticion WebApi como referencia del modelo de peticion de la aplicación
            var peticionWeb = peticionApi.ConsultarUnDiccionarioPeticion.CrearUnaNuevaPeticion(peticionHttp, id);

            if (peticionWeb.Respuesta != string.Empty)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, peticionWeb.Respuesta);

            // Se llama al metodo crear diccionario de la interfaz IAdministradorDeDiccionarios
            var respuestaApp = this.aplicacionMantenimientoDiccionario.ConsultarUnDiccionario(peticionWeb.AppDiccionarioPeticion);

            //Se solicita cargar el modelo de respuesta del WebApi con la respuesta del metodo fachada de la aplicación
            var respuestaContenido = respuestaApi.ConsultarUnDiccionarioRespuesta.CrearNuevaRespuestaConRespuestaDeAplicacion(respuestaApp);

            //Devolvemos el diccionario creado seteado como respuesta http
            if (respuestaContenido.Diccionario.Id != peticionWeb.AppDiccionarioPeticion.DiccionarioId)
                return Request.CreateResponse(HttpStatusCode.NotFound, string.Empty);

            return Request.CreateResponse(HttpStatusCode.OK, respuestaContenido, new MediaTypeWithQualityHeaderValue("application/json"));
        }
        #endregion

        #region Metodos POST
        [Route("diccionario")]
        [HttpPost]
        public HttpResponseMessage CrearUnDiccionario(HttpRequestMessage peticion)
        {
            //Se instancia el modelo de peticion WebApi como referencia del modelo de peticion de la aplicación
            var peticionWeb = peticionApi.CrearUnDiccionarioPeticion.CrearUnaNuevaPeticion(peticion);

            if (peticionWeb.Respuesta != string.Empty)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,peticionWeb.Respuesta);

            // Se llama al metodo crear diccionario de la interfaz IAdministradorDeDiccionarios
            var respuestaApp = this.aplicacionMantenimientoDiccionario.CrearUnDiccionario(peticionWeb.AppDiccionarioPeticion);

            //Preguntamos si el id del nuevo diccionario fue creado, en caso de ser vacio se envia codigo de error
            if (respuestaApp.DiccionarioNuevo == null)
                return Request.CreateErrorResponse(HttpStatusCode.Conflict, new Exception("El Servicio no pudo completar su solicitud por problemas internos, intente mas tarde"));

            //Se solicita cargar el modelo de respuesta del WebApi con la respuesta del metodo fachada de la aplicación
            var respuestaContenido = respuestaApi.CrearUnDiccionarioRespuesta.CrearNuevaRespuesta(respuestaApp);

            //Devolvemos el diccionario creado seteado como respuesta http
            return Request.CreateResponse(HttpStatusCode.Created, respuestaContenido, new MediaTypeWithQualityHeaderValue("application/json"));
        }
        #endregion

        #region Metodos Put
        [Route("diccionario/id/{id}")]
        [HttpPut]
        public HttpResponseMessage ModificarUnDiccionario(HttpRequestMessage peticionHttp,string id)
        {

            //Solicitamos el modelo del web api que se encargara de deserializar la peticion e referenciar el modelo de aplica
            var peticionWeb = peticionApi.ModificarUnDiccionarioPeticion.CrearUnaNuevaPeticionDeModificacion(peticionHttp,id);

            if (peticionWeb.Respuesta != string.Empty)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, peticionWeb.Respuesta);

            // Se llama al metodo crear diccionario de la interfaz IAdministradorDeDiccionarios
            var respuestaApp = this.aplicacionMantenimientoDiccionario.ModificarUnDiccionario(peticionWeb.AppDiccionarioPeticion);

            if (respuestaApp.Diccionario == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            //Se solicita cargar el modelo de respuesta del WebApi con la respuesta del metodo fachada de la aplicación
            var respuestaContenido = respuestaApi.ModificarUnDiccionarioRespuesta.CrearNuevaRespuesta(respuestaApp);

            return Request.CreateResponse(HttpStatusCode.OK,respuestaContenido,new MediaTypeWithQualityHeaderValue("application/json"));
        }


        #endregion

        #region Metodos Delete
        [Route("diccionario/id/{id}")]
        [HttpDelete]
        public HttpResponseMessage EliminarUnDiccionario(HttpRequestMessage peticionHttp,string id)
        {
            //Solicitamos el modelo del web api que se encargara de deserializar la peticion e referenciar el modelo de aplica
            var peticionWeb = peticionApi.EliminarUnDiccionarioPeticion.CrearUnaNuevaPeticionDeEliminar(peticionHttp, id);

            // Se llama al metodo crear diccionario de la interfaz IAdministradorDeDiccionarios
            var respuestaApp = this.aplicacionMantenimientoDiccionario.EliminarUnDiccionario(peticionWeb.AppDiccionarioPeticion);

            if (respuestaApp == null)
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, new Exception("El Servicio no pudo completar su solicitud por problemas internos, intente mas tarde"));

            if(respuestaApp.ListaDeDiccionarios == null)
				return Request.CreateResponse(HttpStatusCode.Conflict, string.Empty);

            //Se solicita cargar el modelo de respuesta del WebApi con la respuesta del metodo fachada de la aplicación
            var respuestaContenido = respuestaApi.EliminarUnDiccionarioRespuesta.CrearNuevaInstancia(respuestaApp);

            return Request.CreateResponse(HttpStatusCode.OK, respuestaContenido, new MediaTypeWithQualityHeaderValue("application/json"));
        }
        #endregion
    }
}
