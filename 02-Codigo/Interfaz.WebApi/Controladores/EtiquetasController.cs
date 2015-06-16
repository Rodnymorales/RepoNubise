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
    public class EtiquetasController : ApiController
    {
        #region propiedades y variables globales
        private readonly app.IAdministradorDeEtiquetas aplicacionMantenimientoDiccionario;

        #endregion

        #region Constructor de la clase
        public EtiquetasController(app.IAdministradorDeEtiquetas aplicacionMantenimientoDiccionario)
        {
            this.aplicacionMantenimientoDiccionario = aplicacionMantenimientoDiccionario;
        }
        #endregion

        #region Metodos de consulta (GET)
               
        [Route("etiquetas/nombre/{nombre}")]
        [HttpGet]
        public HttpResponseMessage ConsultarEtiquetasPorNombre(string nombre)
        {
            //Se instancia el modelo de peticion WebApi como referencia del modelo de peticion de la aplicación 
            var peticionWeb = peticionApi.ConsultarEtiquetasPorNombrePeticion.CrearNuevaPeticion(nombre);

            // Se llama al metodo crear diccionario de la interfaz IAdministradorDeEtiquetas
            var respuestaApp = this.aplicacionMantenimientoDiccionario.ConsultarEtiquetasPorNombre(peticionWeb.AppEtiquetaPeticion);

            if (respuestaApp.ListaDeDiccionarios.Count() == 0)
                return Request.CreateErrorResponse(HttpStatusCode.NoContent, string.Empty);

            //Se solicita cargar el modelo de respuesta del WebApi con la respuesta del metodo fachada de la aplicación
            var respuestaContenido = respuestaApi.ConsultarEtiquetasPorNombreRespuesta.CrearNuevaRespuesta(respuestaApp);

            return Request.CreateResponse(HttpStatusCode.OK, respuestaContenido, new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [Route("diccionario/id/{id1}/etiquetas/idiomapordefecto/{idioma}")]
        [HttpGet]
        public HttpResponseMessage ConsultarEtiquetasDiccionarioPorIdiomaPorDefecto(string id1, string idioma)
        {
            //Se instancia el modelo de peticion WebApi como referencia del modelo de peticion de la aplicación 
            var peticionWeb = peticionApi.ConsultarEtiquetasDeDiccionarioPorIdiomaPorDefectoPeticion.CrearUnaNuevaPeticion(id1,idioma);

            // Se llama al metodo crear diccionario de la interfaz IAdministradorDeEtiquetas
            var respuestaApp = this.aplicacionMantenimientoDiccionario.ConsultarEtiquetasDeDiccionarioPorIdiomaPorDefecto(peticionWeb.AppEtiquetasDicionarioPeticion);

            if (respuestaApp.Relaciones["diccionario"] == new Guid("00000000-0000-0000-0000-000000000000"))
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, string.Empty);

            //Se solicita cargar el modelo de respuesta del WebApi con la respuesta del metodo fachada de la aplicación
            var respuestaContenido = respuestaApi.ConsultarEtiquetasDeDiccionarioPorIdiomaPorDefectoRespuesta.CrearNuevaRespuesta(respuestaApp);

            if (respuestaContenido.Etiquetas.ListaEtiquetas.Count() == 0)
                return Request.CreateErrorResponse(HttpStatusCode.NoContent, "La busqueda solicita no arrojo ningun resultado");

            return Request.CreateResponse(HttpStatusCode.OK, respuestaContenido, new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [Route("diccionario/id/{id1}/etiquetas/descripcion")]
        [HttpGet]
        public HttpResponseMessage ConsultarEtiquetasDiccionarioPorDescripcion(string id1,HttpRequestMessage peticionHttp)
        {
            //Se instancia el modelo de peticion WebApi como referencia del modelo de peticion de la aplicación 
            var peticionWeb = peticionApi.ConsultarEtiquetasDeDiccionarioPorDescripcionPeticion.CrearNuevaPeticion(id1,peticionHttp);

            // Se llama al metodo crear diccionario de la interfaz IAdministradorDeEtiquetas
            var respuestaApp = this.aplicacionMantenimientoDiccionario.ConsultarEtiquetasDeDiccionarioPorDescripcion(peticionWeb.AppEtiquetasDiccionarioPeticion);

            if (respuestaApp.Relaciones["diccionario"] == new Guid("00000000-0000-0000-0000-000000000000"))
				return Request.CreateErrorResponse(HttpStatusCode.NotFound, string.Empty);

            //Se solicita cargar el modelo de respuesta del WebApi con la respuesta del metodo fachada de la aplicación
            var respuestaContenido = respuestaApi.ConsultarEtiquetasDeDiccionarioPorDescripcionRespuesta.CrearNuevaRespuesta(respuestaApp);

            if (respuestaContenido.Etiquetas.ListaEtiquetas.Count() == 0)
                return Request.CreateErrorResponse(HttpStatusCode.NoContent, "La busqueda solicita no arrojo ningun resultado");

            return Request.CreateResponse(HttpStatusCode.OK, respuestaContenido, new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [Route("diccionario/id/{id1}/etiquetas/estatus/{estatus}")]
        [HttpGet]
        public HttpResponseMessage ConsultarEtiquetasDiccionarioPorEstatus(string id1, string estatus)
        {
            //Se instancia el modelo de peticion WebApi como referencia del modelo de peticion de la aplicación 
            var peticionWeb = peticionApi.ConsultarEtiquetasDeDiccionarioPorEstatusPeticion.CrearNuevaPeticion(id1, estatus);

            if (peticionWeb.Respuesta != string.Empty)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,peticionWeb.Respuesta);

            // Se llama al metodo crear diccionario de la interfaz IAdministradorDeEtiquetas
            var respuestaApp = this.aplicacionMantenimientoDiccionario.ConsultarEtiquetasDeDiccionarioPorEstatus(peticionWeb.AppEtiquetasDiccionarioPeticion);

            if (respuestaApp.Relaciones["diccionario"] == new Guid("00000000-0000-0000-0000-000000000000"))
				return Request.CreateErrorResponse(HttpStatusCode.NotFound, string.Empty);

            //Se solicita cargar el modelo de respuesta del WebApi con la respuesta del metodo fachada de la aplicación
            var respuestaContenido = respuestaApi.ConsultarEtiquetasDeDiccionarioPorEstatusRespuesta.CrearNuevaRespuesta(respuestaApp);

            if (respuestaContenido.Etiquetas.ListaEtiquetas.Count == 0)
                return Request.CreateResponse(HttpStatusCode.NoContent);

            return Request.CreateResponse(HttpStatusCode.OK, respuestaContenido, new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [Route("diccionario/id/{id1}/etiquetas/idioma/{idioma}")]
        [HttpGet]
        public HttpResponseMessage ConsultarEtiquetasDiccionarioPorIdioma(string id1, string idioma)
        {
            //Se instancia el modelo de peticion WebApi como referencia del modelo de peticion de la aplicación 
            var peticionWeb = peticionApi.ConsultarEtiquetasDeDiccionarioPorIdiomaPeticion.CrearNuevaPeticion(id1, idioma);

            if (peticionWeb.Respuesta != string.Empty)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, peticionWeb.Respuesta);

            // Se llama al metodo crear diccionario de la interfaz IAdministradorDeEtiquetas
            var respuestaApp = this.aplicacionMantenimientoDiccionario.ConsultarEtiquetasDeDiccionarioPorIdioma(peticionWeb.AppEtiquetasDiccionarioPeticion);

            if (respuestaApp.Relaciones["diccionario"] == new Guid("00000000-0000-0000-0000-000000000000"))
				return Request.CreateErrorResponse(HttpStatusCode.NotFound, string.Empty);

            //Se solicita cargar el modelo de respuesta del WebApi con la respuesta del metodo fachada de la aplicación
            var respuestaContenido = respuestaApi.ConsultarEtiquetasDeDiccionarioPorIdiomaRespuesta.CrearNuevaRespuesta(respuestaApp);

            if (respuestaContenido.Etiquetas.ListaEtiquetas.Count() == 0)
                return Request.CreateResponse(HttpStatusCode.NoContent);

            return Request.CreateResponse(HttpStatusCode.OK, respuestaContenido, new MediaTypeWithQualityHeaderValue("application/json"));
        }


        [Route("diccionario/id/{id1}/etiquetas/nombre/{nombre}")]
        [HttpGet]
        public HttpResponseMessage ConsultarEtiquetasDiccionarioPorNombre(string id1, string nombre)
        {
            //Se instancia el modelo de peticion WebApi como referencia del modelo de peticion de la aplicación 
            var peticionWeb = peticionApi.ConsultarEtiquetasDeDiccionarioPorNombrePeticion.CrearNuevaPeticion(id1, nombre);

            // Se llama al metodo crear diccionario de la interfaz IAdministradorDeEtiquetas
            var respuestaApp = this.aplicacionMantenimientoDiccionario.ConsultarEtiquetasDeDiccionarioPorNombre(peticionWeb.AppEtiquetasDiccionarioPeticion);

            if (respuestaApp.Relaciones["diccionario"] == new Guid("00000000-0000-0000-0000-000000000000"))
				return Request.CreateErrorResponse(HttpStatusCode.NotFound, string.Empty);

            //Se solicita cargar el modelo de respuesta del WebApi con la respuesta del metodo fachada de la aplicación
            var respuestaContenido = respuestaApi.ConsultarEtiquetasDeDiccionarioPorNombreRespuesta.CrearNuevaRespuesta(respuestaApp);

            if (respuestaContenido.Etiquetas.ListaEtiquetas.Count() == 0)
                return Request.CreateResponse(HttpStatusCode.NoContent);

            return Request.CreateResponse(HttpStatusCode.OK, respuestaContenido, new MediaTypeWithQualityHeaderValue("application/json"));
        }
        #endregion

        #region Metodos de modificacion (PUT)

        [Route("diccionario/id/{id1}/etiquetas")]
        [HttpPut]
        public HttpResponseMessage ModificarEtiquetasAUnDiccionario(HttpRequestMessage peticionHttp,string id1)
        {
            //Se instancia el modelo de peticion WebApi como referencia del modelo de peticion de la aplicación 
            var peticionWeb = peticionApi.ModificarEtiquetasAUnDiccionarioPeticion.CrearNuevaPeticion(peticionHttp, id1);

            if (peticionWeb.Respuesta != string.Empty)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, peticionWeb.Respuesta);

            // Se llama al metodo crear diccionario de la interfaz IAdministradorDeEtiquetas
            var respuestaApp = this.aplicacionMantenimientoDiccionario.ModificarEtiquetasAUnDiccionario(peticionWeb.AppEtiquetasDiccionarioPeticion);

            if (respuestaApp.Relaciones["diccionario"] == new Guid("00000000-0000-0000-0000-000000000000"))
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, string.Empty);

            //Se solicita cargar el modelo de respuesta del WebApi con la respuesta del metodo fachada de la aplicación
            var respuestaContenido = respuestaApi.ModificarEtiquetasAUnDiccionarioRespuesta.CrearNuevaRespuesta(respuestaApp);

            return Request.CreateResponse(HttpStatusCode.OK, respuestaContenido, new MediaTypeWithQualityHeaderValue("application/json"));
        }
        #endregion

        

        #region Metodos de insercion (POST)
        [Route("diccionario/id/{id1}/etiquetas")]
        [HttpPost]
        public HttpResponseMessage AgregarEtiquetasAUnDiccionario(string id1, HttpRequestMessage peticionHttp)
        {
            //Se instancia el modelo de peticion WebApi como referencia del modelo de peticion de la aplicación 
            var peticionWeb = peticionApi.AgregarEtiquetasAUnDiccionarioPeticion.CrearNuevaPeticion(id1, peticionHttp);

            if (peticionWeb.Respuesta != string.Empty)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, peticionWeb.Respuesta);

            // Se llama al metodo crear diccionario de la interfaz IAdministradorDeEtiquetas
            var respuestaApp = this.aplicacionMantenimientoDiccionario.AgregarEtiquetasAUnDiccionario(peticionWeb.AppEtiquetasDiccionarioPeticion);

            if (respuestaApp.Relaciones["diccionario"] == new Guid("00000000-0000-0000-0000-000000000000"))
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, string.Empty);

            //Se solicita cargar el modelo de respuesta del WebApi con la respuesta del metodo fachada de la aplicación
            var respuestaContenido = respuestaApi.AgregarEtiquetasAUnDiccionarioRespuesta.CrearNuevaRespuesta(respuestaApp);

            return Request.CreateResponse(HttpStatusCode.Created, respuestaContenido, new MediaTypeWithQualityHeaderValue("application/json"));
        }
        #endregion

        
        #region Metodos de Eliminacion (DELETE)
        [Route("diccionario/id/{id1}/etiquetas")]
        public HttpResponseMessage EliminarEtiquetasAUnDiccionario(string id1, HttpRequestMessage peticionHttp)
        {
            //Se instancia el modelo de peticion WebApi como referencia del modelo de peticion de la aplicación 
            var peticionWeb = peticionApi.EliminarEtiquetasAUnDiccionarioPeticion.CrearNuevaPeticion(id1, peticionHttp);

            if (peticionWeb.Respuesta != string.Empty)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, peticionWeb.Respuesta);

            // Se llama al metodo crear diccionario de la interfaz IAdministradorDeEtiquetas
            var respuestaApp = this.aplicacionMantenimientoDiccionario.EliminarEtiquetasAUnDiccionario(peticionWeb.AppEtiquetasDiccionarioPeticion);

            if (respuestaApp.Relaciones["diccionario"] == new Guid("00000000-0000-0000-0000-000000000000"))
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, string.Empty);

            //Se solicita cargar el modelo de respuesta del WebApi con la respuesta del metodo fachada de la aplicación
            var respuestaContenido = respuestaApi.EliminarEtiquetasAUnDiccionarioRespuesta.CrearNuevaRespuesta(respuestaApp);

            return Request.CreateResponse(HttpStatusCode.OK, respuestaContenido, new MediaTypeWithQualityHeaderValue("application/json"));
        }
        #endregion
    }
}
