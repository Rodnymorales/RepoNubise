using Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Peticion;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.Entidades.Diccionario;
using Newtonsoft.Json;
using NSubstitute;
using NUnit.Framework;
using Should;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using app = Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Fachada;
using appModelosRespuesta = Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Respuesta;
using controladores = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Controladores;
using webApiModelosRespuesta = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Respuesta;
using utilitario = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.PruebasUnitarias.Utilitarios;
using comunes = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Comunes;


namespace Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.PruebasUnitarias
{
    [TestFixture]
    public class DiccionarioControladorTest
    {
        #region variables y propiedades globales
        private const string AmbienteTestPrueba = "Prueba";
        private const string EtiquetaJson = "{ \"etiquetas\": [ { \"id\": \"094bf626-d137-47ea-81fa-19a0aaceedf5\", \"activo\": true, \"idiomapordefecto\": \"es-VE\", \"nombre\": \"app.common.aceptar\", \"descripcion\": \"Aceptar\", \"textos\": [ { \"cultura\": \"es\", \"texto\": \"aceptar\" }, { \"cultura\": \"es-VE\", \"texto\": \"aceptar\" }, { \"cultura\": \"en\", \"texto\": \"accept\" }, { \"cultura\": \"en-US\", \"texto\": \"accept\" } ] }, { \"id\": \"98d723fd-b301-41e2-90a2-90c66a6835b8\", \"activo\": true, \"idiomapordefecto\": \"es-VE\", \"nombre\": \"app.common.cancelar\", \"descripcion\": \"cancelar\", \"textos\": [ { \"cultura\": \"es\", \"texto\": \"cancelar\" }, { \"cultura\": \"es-VE\", \"texto\": \"cancelar\" }, { \"cultura\": \"en\", \"texto\": \"cancel\" }, { \"cultura\": \"en-US\", \"texto\": \"cancel\" } ] }, { \"id\": \"c4d2f76e-cc6a-4481-853c-47f1cd7eafdc\", \"activo\": true, \"idiomapordefecto\": \"en\", \"nombre\": \"app.common.usuario\", \"descripcion\": \"Campo de texto usuario\", \"textos\": [ { \"cultura\": \"es\", \"texto\": \"usuario\" }, { \"cultura\": \"es-VE\", \"texto\": \"usuario\" }, { \"cultura\": \"en\", \"texto\": \"user\" }, { \"cultura\": \"en-US\", \"texto\": \"user\" } ] }, { \"id\": \"07eca348-ae16-43e4-a16f-0f8039ab1e35\", \"activo\": true, \"idiomapordefecto\": \"en\", \"nombre\": \"app.common.contraseña\", \"descripcion\": \"Campo de texto contraseña\", \"textos\": [ { \"cultura\": \"es\", \"texto\": \"contraseña\" }, { \"cultura\": \"es-VE\", \"texto\": \"contraseña\" }, { \"cultura\": \"en\", \"texto\": \"password\" }, { \"cultura\": \"en-US\", \"texto\": \"password\" } ] } ] }";
        private const string DiccionarioConEtiquetasJson = "{\"diccionarios\":[{\"etiquetas\": [ { \"id\": \"094bf626-d137-47ea-81fa-19a0aaceedf5\", \"activo\": true, \"idiomapordefecto\": \"es-VE\", \"nombre\": \"app.common.aceptar\", \"descripcion\": \"Aceptar\", \"textos\": [ { \"cultura\": \"es\", \"texto\": \"aceptar\" }, { \"cultura\": \"es-VE\", \"texto\": \"aceptar\" }, { \"cultura\": \"en\", \"texto\": \"accept\" }, { \"cultura\": \"en-US\", \"texto\": \"accept\" } ] }, { \"id\": \"98d723fd-b301-41e2-90a2-90c66a6835b8\", \"activo\": true, \"idiomapordefecto\": \"es-VE\", \"nombre\": \"app.common.cancelar\", \"descripcion\": \"cancelar\", \"textos\": [ { \"cultura\": \"es\", \"texto\": \"cancelar\" }, { \"cultura\": \"es-VE\", \"texto\": \"cancelar\" }, { \"cultura\": \"en\", \"texto\": \"cancel\" }, { \"cultura\": \"en-US\", \"texto\": \"cancel\" } ] }, { \"id\": \"c4d2f76e-cc6a-4481-853c-47f1cd7eafdc\", \"activo\": true, \"idiomapordefecto\": \"en\", \"nombre\": \"app.common.usuario\", \"descripcion\": \"Campo de texto usuario\", \"textos\": [ { \"cultura\": \"es\", \"texto\": \"usuario\" }, { \"cultura\": \"es-VE\", \"texto\": \"usuario\" }, { \"cultura\": \"en\", \"texto\": \"user\" }, { \"cultura\": \"en-US\", \"texto\": \"user\" } ] }, { \"id\": \"07eca348-ae16-43e4-a16f-0f8039ab1e35\", \"activo\": true, \"idiomapordefecto\": \"en\", \"nombre\": \"app.common.contraseña\", \"descripcion\": \"Campo de texto contraseña\", \"textos\": [ { \"cultura\": \"es\", \"texto\": \"contraseña\" }, { \"cultura\": \"es-VE\", \"texto\": \"contraseña\" }, { \"cultura\": \"en\", \"texto\": \"password\" }, { \"cultura\": \"en-US\", \"texto\": \"password\" } ] } ],\"id\":\"a1fa3369-bc3f-4ebc-9cac-5677cbaa8114\"}]}";

        private readonly app.IAdministradorDeDiccionarios appMantenimientoDiccionario;

        #endregion

        #region Constructor de las pruebas
        public DiccionarioControladorTest()
        {
            // Se inicializa el proxy del NSustitute para posteriormente inyectar los mocks la dependencia
            this.appMantenimientoDiccionario = Substitute.For<app.IAdministradorDeDiccionarios>();

            
        }
        #endregion

        #region Pruebas de tipos de instancias
        [Test]
        public void DeberiaPoderRetornarElTipoDeObjetoControladorDiccionariosControllerParaCualquierSolicitudDelControlador()
        { 
            //Assert
            var controlador = new controladores.DiccionariosController(this.appMantenimientoDiccionario);
            controlador.ShouldBeType<controladores.DiccionariosController>();
        }
        #endregion

        #region Pruebas de consultar (GET)
        [Test]
        public void DeberiaPoderRetornarHttpStatusCodeOkCuandoSeConsultanTodosLosDiccionariosRetornaLaListaDiccionarios()
        {
            //Arrange
            var controlador = new controladores.DiccionariosController(this.appMantenimientoDiccionario);
            controlador.Configuration = new HttpConfiguration();
            var consultarDiccionariosRespuesta = appModelosRespuesta.ConsultarDiccionariosRespuesta.CrearNuevaInstancia();

            var diccionarios = JsonConvert.DeserializeObject<comunes.Diccionarios>(DiccionarioConEtiquetasJson);
            consultarDiccionariosRespuesta.ListaDeDiccionarios = utilitario.MapearEntidades.MapearDiccionariosTipoComunesConTipoAplicacionParaMock(diccionarios);

            this.appMantenimientoDiccionario.ConsultarDiccionarios().ReturnsForAnyArgs<appModelosRespuesta.ConsultarDiccionariosRespuesta>(consultarDiccionariosRespuesta);

            controlador.Request = new HttpRequestMessage(HttpMethod.Get,"api/diccionarios");
            
            //Act
            var respuesta = controlador.ObtenerTodosDiccionarios();
            
            //Assert
            respuesta.StatusCode.ShouldEqual(HttpStatusCode.OK);
            var validarContenidoRespuesta = JsonConvert.DeserializeObject<webApiModelosRespuesta.ConsultarDiccionariosRespuesta>(respuesta.Content.ReadAsStringAsync().Result);

            validarContenidoRespuesta.ListaDeDiccionarios.ShouldNotBeEmpty();
        }



        [Test]
        public void DeberiaPoderRetornarHttpStatusCodeOkCuandoSeConsultaUnDiccionarioPorSuGuidRetornaElDiccionarioLleno()
        {
            //Arrange
            // Se crea una nueva instancia del controlador inyectandole la interfaz con los metodos mock que se configuraran en las pruebas
            var controlador = new controladores.DiccionariosController(this.appMantenimientoDiccionario);
            controlador.Configuration = new HttpConfiguration();

            var consultarUnDiccionarioRespuesta = appModelosRespuesta.ConsultarUnDiccionarioarioRespuesta.CrearNuevaInstancia(AmbienteTestPrueba);
            consultarUnDiccionarioRespuesta.Diccionario = Diccionario.CrearNuevoDiccionario(new Guid("8a87f8a7-3df9-4d90-9478-350b964fc888"), AmbienteTestPrueba);

            var etiquetas = JsonConvert.DeserializeObject<comunes.Etiquetas>(EtiquetaJson);

            consultarUnDiccionarioRespuesta.Diccionario.AgregarEtiquetas(utilitario.MapearEntidades.MapearEtiquetasTipoComunesConTipoAplicacionParaMock(etiquetas.ListaEtiquetas));

            this.appMantenimientoDiccionario.ConsultarUnDiccionario(Arg.Any<ConsultarUnDiccionarioPeticion>()).ReturnsForAnyArgs<appModelosRespuesta.ConsultarUnDiccionarioarioRespuesta>(consultarUnDiccionarioRespuesta);

            controlador.Request = new HttpRequestMessage(HttpMethod.Get, "http:/localhost:80/api/diccionario/8a87f8a7-3df9-4d90-9478-350b964fc888");

            var diccionario = utilitario.ConfigurarLlamadasHttpMock.ConfigurarMockPeticionHttp(AmbienteTestPrueba, "8a87f8a7-3df9-4d90-9478-350b964fc888");

            controlador.Request.Content = new StringContent(JsonConvert.SerializeObject(diccionario.DiccionarioMock));
            controlador.Request.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

            //Act
            var respuesta = controlador.ConsultarUnDiccionario(controlador.Request, "8a87f8a7-3df9-4d90-9478-350b964fc888");
            
            //Assert
            respuesta.StatusCode.ShouldEqual(HttpStatusCode.OK);
            var validarContenidoRespuesta = JsonConvert.DeserializeObject<webApiModelosRespuesta.ConsultarUnDiccionarioRespuesta>(respuesta.Content.ReadAsStringAsync().Result);

            validarContenidoRespuesta.Diccionario.ShouldNotBeNull();
            validarContenidoRespuesta.Relaciones.ShouldBeEmpty();

        }

        [Test]
        public void DeberiaPoderRetornarHttpStatusCodeBadRequestCuandoSeConsultaUnDiccionarioPorSuGuidRetornaDiccionarioNoEncontrado()
        {
            //Arrange
            var controlador = new controladores.DiccionariosController(this.appMantenimientoDiccionario);
            controlador.Configuration = new HttpConfiguration();
            var consultarUnDiccionarioRespuesta = appModelosRespuesta.ConsultarUnDiccionarioarioRespuesta.CrearNuevaInstancia(AmbienteTestPrueba);
            consultarUnDiccionarioRespuesta.Diccionario = Diccionario.CrearNuevoDiccionario(new Guid("8a87f8a7-3df9-4d90-9478-350b964fc888"), AmbienteTestPrueba);

            this.appMantenimientoDiccionario.ConsultarUnDiccionario(Arg.Any<ConsultarUnDiccionarioPeticion>()).ReturnsForAnyArgs<appModelosRespuesta.ConsultarUnDiccionarioarioRespuesta>(consultarUnDiccionarioRespuesta);

            controlador.Request = new HttpRequestMessage(HttpMethod.Get, "api/diccionario/9a39ad6d-62c8-42bf-a8f7-66417b2b08d0");

            var diccionario = utilitario.ConfigurarLlamadasHttpMock.ConfigurarMockPeticionHttp(AmbienteTestPrueba, "8a87f8a7-3df9-4d90-9478-350b964fc888");

            controlador.Request.Content = new StringContent(JsonConvert.SerializeObject(diccionario.DiccionarioMock));
            controlador.Request.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

            //Act
            var respuesta = controlador.ConsultarUnDiccionario(controlador.Request, "9a39ad6d-62c8-42bf-a8f7");

            //Assert
            respuesta.StatusCode.ShouldEqual(HttpStatusCode.BadRequest, "Formato de Guid no valido, la valor del id del diccionario debe tener la siguiente estructura, ejemplo: 9a39ad6d-62c8-42bf-a8f7-66417b2b08d0");
        }

        [Test]
        public void DeberiaPoderRetornarHttpStatusCodeNotFoundCuandoSeConsultaUnDiccionarioPorSuGuidRetornaDiccionarioNoEncontrado()
        {
            //Arrange
            var controlador = new controladores.DiccionariosController(this.appMantenimientoDiccionario);
            controlador.Configuration = new HttpConfiguration();
            var consultarUnDiccionarioRespuesta = appModelosRespuesta.ConsultarUnDiccionarioarioRespuesta.CrearNuevaInstancia(AmbienteTestPrueba);
            consultarUnDiccionarioRespuesta.Diccionario = Diccionario.CrearNuevoDiccionario(new Guid("8a87f8a7-3df9-4d90-9478-350b964fc888"), AmbienteTestPrueba);
            this.appMantenimientoDiccionario.ConsultarUnDiccionario(Arg.Any<ConsultarUnDiccionarioPeticion>()).ReturnsForAnyArgs<appModelosRespuesta.ConsultarUnDiccionarioarioRespuesta>(consultarUnDiccionarioRespuesta);

            controlador.Request = new HttpRequestMessage(HttpMethod.Get, "api/diccionario/9a39ad6d-62c8-42bf-a8f7-66417b2b08d0");

            var diccionario = utilitario.ConfigurarLlamadasHttpMock.ConfigurarMockPeticionHttp(AmbienteTestPrueba, "8a87f8a7-3df9-4d90-9478-350b964fc888");

            controlador.Request.Content = new StringContent(JsonConvert.SerializeObject(diccionario.DiccionarioMock));
            controlador.Request.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

            //Act
            var respuesta = controlador.ConsultarUnDiccionario(controlador.Request, "9a39ad6d-62c8-42bf-a8f7-66417b2b08d0");

            //Assert
            respuesta.StatusCode.ShouldEqual(HttpStatusCode.NotFound);
        }

        

        [Test]
        public void DeberiaPoderRetornarHttpStatusCodeOkCuandoSeConsultaUnDiccionarioPorSuGuidRetornaUnDiccionarioSinEtiquetas()
        {
            //Arrange
            var controlador = new controladores.DiccionariosController(this.appMantenimientoDiccionario);
            controlador.Configuration = new HttpConfiguration();
            var consultarUnDiccionarioRespuesta = appModelosRespuesta.ConsultarUnDiccionarioarioRespuesta.CrearNuevaInstancia(AmbienteTestPrueba);
            consultarUnDiccionarioRespuesta.Diccionario = Diccionario.CrearNuevoDiccionario(new Guid("8a87f8a7-3df9-4d90-9478-350b964fc888"), AmbienteTestPrueba);
            this.appMantenimientoDiccionario.ConsultarUnDiccionario(Arg.Any<ConsultarUnDiccionarioPeticion>()).ReturnsForAnyArgs<appModelosRespuesta.ConsultarUnDiccionarioarioRespuesta>(consultarUnDiccionarioRespuesta);
            controlador.Request = new HttpRequestMessage(HttpMethod.Get, "http:/localhost:80/api/diccionario/165db3e4-d705-406b-bce0-2738b25c9023");
            var diccionario = utilitario.ConfigurarLlamadasHttpMock.ConfigurarMockPeticionHttp(AmbienteTestPrueba, "8a87f8a7-3df9-4d90-9478-350b964fc888");

            controlador.Request.Content = new StringContent(JsonConvert.SerializeObject(diccionario.DiccionarioMock));
            controlador.Request.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

            //Act
            var respuesta = controlador.ConsultarUnDiccionario(controlador.Request, "8a87f8a7-3df9-4d90-9478-350b964fc888");
            var validarContenidoRespuesta = JsonConvert.DeserializeObject<webApiModelosRespuesta.ConsultarUnDiccionarioRespuesta>(respuesta.Content.ReadAsStringAsync().Result);

            //Assert
            respuesta.StatusCode.ShouldEqual(HttpStatusCode.OK);
            validarContenidoRespuesta.Diccionario.Etiquetas.ShouldBeEmpty();
        }
        #endregion

        #region Pruebas de creacion (POST)
        [Test]
        public void DeberiaPoderRetornarHttpStatusCodeCreatedCuandoSeSolicitaCreaUnNuevoDiccionarioRetornaElDiccionarioNuevo() 
        {                
            //Arrange
            var controlador = new controladores.DiccionariosController(this.appMantenimientoDiccionario);
            controlador.Configuration = new HttpConfiguration();
            var crearUnDiccionarioRespuesta = appModelosRespuesta.CrearUnDiccionarioRespuesta.CrearNuevaInstancia(AmbienteTestPrueba);
            this.appMantenimientoDiccionario.CrearUnDiccionario(Arg.Any<CrearUnDiccionarioPeticion>()).ReturnsForAnyArgs<appModelosRespuesta.CrearUnDiccionarioRespuesta>(crearUnDiccionarioRespuesta);

            var etiquetas = JsonConvert.DeserializeObject<comunes.Etiquetas>(EtiquetaJson);

            crearUnDiccionarioRespuesta.DiccionarioNuevo.AgregarEtiquetas(utilitario.MapearEntidades.MapearEtiquetasTipoComunesConTipoAplicacionParaMock(etiquetas.ListaEtiquetas));

            controlador.Request = new HttpRequestMessage(HttpMethod.Post, "api/diccionarios");
            var diccionario = utilitario.ConfigurarLlamadasHttpMock.ConfigurarMockPeticionHttp(AmbienteTestPrueba, "8a87f8a7-3df9-4d90-9478-350b964fc888");

            controlador.Request.Content = new StringContent(JsonConvert.SerializeObject(diccionario.DiccionarioMock));
            controlador.Request.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
            
            //Act
            var respuesta = controlador.CrearUnDiccionario(controlador.Request);
            var validarContenidoRespuesta = JsonConvert.DeserializeObject<webApiModelosRespuesta.CrearUnDiccionarioRespuesta>(respuesta.Content.ReadAsStringAsync().Result);

            //Assert
            respuesta.StatusCode.ShouldEqual(HttpStatusCode.Created);
            validarContenidoRespuesta.DiccionarioNuevo.ShouldNotBeNull();

        }

        [Test]
        public void DeberiaPoderRetornarHttpStatusCodeBadRequestCuandoSeSolicitaCrearUnNuevoDiccionarioRetornaParametroDiccionarioMalDefinido()
        {
            //Arrange
            var controlador = new controladores.DiccionariosController(this.appMantenimientoDiccionario);
            controlador.Configuration = new HttpConfiguration();
            var crearUnDiccionarioRespuesta = appModelosRespuesta.CrearUnDiccionarioRespuesta.CrearNuevaInstancia(AmbienteTestPrueba);
            crearUnDiccionarioRespuesta.DiccionarioNuevo = Diccionario.CrearNuevoDiccionario(new Guid("8a87f8a7-3df9-4d90-9478-350b964fc888"), AmbienteTestPrueba);

            this.appMantenimientoDiccionario.CrearUnDiccionario(Arg.Any<CrearUnDiccionarioPeticion>()).ReturnsForAnyArgs<appModelosRespuesta.CrearUnDiccionarioRespuesta>(crearUnDiccionarioRespuesta);

            controlador.Request = new HttpRequestMessage(HttpMethod.Post, "api/diccionario/");

            var diccionario = utilitario.ConfigurarLlamadasHttpMock.ConfigurarMockPeticionHttp(AmbienteTestPrueba);

            diccionario.DiccionarioMock.Ambiente = null;

            controlador.Request.Content = new StringContent(JsonConvert.SerializeObject(diccionario.DiccionarioMock));
            controlador.Request.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

            //Act
            var respuesta = controlador.CrearUnDiccionario(controlador.Request);

            //Assert
            respuesta.StatusCode.ShouldEqual(HttpStatusCode.BadRequest, "El formato del diccionario proporcionado se encuentra mal definido");
        }

        [Test]
        public void DeberiaPoderRetornarHttpStatusCodeConflictCuandoSeSolicitaCrearUnNuevoDiccionarioRetornaErrorInternoDelServidor()
        {
            //Arrange
            var controlador = new controladores.DiccionariosController(this.appMantenimientoDiccionario);
            controlador.Configuration = new HttpConfiguration();
            var crearUnDiccionarioRespuesta = appModelosRespuesta.CrearUnDiccionarioRespuesta.CrearNuevaInstancia(AmbienteTestPrueba);
            crearUnDiccionarioRespuesta.DiccionarioNuevo = null;
            this.appMantenimientoDiccionario.CrearUnDiccionario(Arg.Any<CrearUnDiccionarioPeticion>()).Returns(crearUnDiccionarioRespuesta);

            controlador.Request = new HttpRequestMessage(HttpMethod.Post, "api/diccionarios");
            var diccionario = utilitario.ConfigurarLlamadasHttpMock.ConfigurarMockPeticionHttp(AmbienteTestPrueba, "8a87f8a7-3df9-4d90-9478-350b964fc888");

            controlador.Request.Content = new StringContent(JsonConvert.SerializeObject(diccionario.DiccionarioMock));
            controlador.Request.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

            //Act
            var respuesta = controlador.CrearUnDiccionario(controlador.Request);
            var validarContenidoRespuesta = JsonConvert.DeserializeObject<webApiModelosRespuesta.CrearUnDiccionarioRespuesta>(respuesta.Content.ReadAsStringAsync().Result);

            //Assert
            respuesta.StatusCode.ShouldEqual(HttpStatusCode.Conflict);
            validarContenidoRespuesta.DiccionarioNuevo.ShouldBeNull();

        }
        #endregion

        #region Pruebas de modificación (PUT)
        [Test]
        public void DeberiaPoderRetornarHttpStatusCodeOkCuandoSeSolicitaModificarUnDiccionarioRetornaElDiccionarioModificado()
        {
            //Arrange
            var controlador = new controladores.DiccionariosController(this.appMantenimientoDiccionario);
            controlador.Configuration = new HttpConfiguration();
            var modificarUnDiccionarioRespuesta = appModelosRespuesta.ModificarUnDiccionarioRespuesta.CrearNuevaInstancia();
            modificarUnDiccionarioRespuesta.Diccionario = Diccionario.CrearNuevoDiccionario(new Guid("9a39ad6d-62c8-42bf-a8f7-66417b2b08d0"), AmbienteTestPrueba);
            this.appMantenimientoDiccionario.ModificarUnDiccionario(Arg.Any<ModificarUnDiccionarioPeticion>()).ReturnsForAnyArgs<appModelosRespuesta.ModificarUnDiccionarioRespuesta>(modificarUnDiccionarioRespuesta);

            var etiquetas = JsonConvert.DeserializeObject<comunes.Etiquetas>(EtiquetaJson);

            modificarUnDiccionarioRespuesta.Diccionario.AgregarEtiquetas(utilitario.MapearEntidades.MapearEtiquetasTipoComunesConTipoAplicacionParaMock(etiquetas.ListaEtiquetas));

            controlador.Request = new HttpRequestMessage(HttpMethod.Put, "api/diccionario/9a39ad6d-62c8-42bf-a8f7-66417b2b08d0");
            var diccionario = utilitario.ConfigurarLlamadasHttpMock.ConfigurarMockPeticionHttp(AmbienteTestPrueba, "8a87f8a7-3df9-4d90-9478-350b964fc888");

            controlador.Request.Content = new StringContent(JsonConvert.SerializeObject(diccionario.DiccionarioMock));
            controlador.Request.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

            //Act
            var respuesta = controlador.ModificarUnDiccionario(controlador.Request, "9a39ad6d-62c8-42bf-a8f7-66417b2b08d0");
            var validarContenidoRespuesta = JsonConvert.DeserializeObject<webApiModelosRespuesta.ModificarUnDiccionarioRespuesta>(respuesta.Content.ReadAsStringAsync().Result);

            //Assert
            respuesta.StatusCode.ShouldEqual(HttpStatusCode.OK);
            validarContenidoRespuesta.Diccionario.Ambiente.ShouldEqual(AmbienteTestPrueba);

        }

        [Test]
        public void DeberiaPoderRetornarHttpStatusCodeBadRequestCuandoSeSolicitaModificarUnDiccionarioRetornaParametroDiccionarioMalDefinido()
        {
            //Arrange
            var controlador = new controladores.DiccionariosController(this.appMantenimientoDiccionario);
            controlador.Configuration = new HttpConfiguration();
            var modificarUnDiccionarioRespuesta = appModelosRespuesta.ModificarUnDiccionarioRespuesta.CrearNuevaInstancia();
            modificarUnDiccionarioRespuesta.Diccionario = Diccionario.CrearNuevoDiccionario(new Guid("8a87f8a7-3df9-4d90-9478-350b964fc888"), AmbienteTestPrueba);

            this.appMantenimientoDiccionario.ModificarUnDiccionario(Arg.Any<ModificarUnDiccionarioPeticion>()).ReturnsForAnyArgs<appModelosRespuesta.ModificarUnDiccionarioRespuesta>(modificarUnDiccionarioRespuesta);

            controlador.Request = new HttpRequestMessage(HttpMethod.Put, "api/diccionario/8a87f8a7-3df9-4d90-9478-350b964fc888");

            var diccionario = utilitario.ConfigurarLlamadasHttpMock.ConfigurarMockPeticionHttp(AmbienteTestPrueba);

            diccionario.DiccionarioMock.Ambiente = null;

            controlador.Request.Content = new StringContent(JsonConvert.SerializeObject(diccionario.DiccionarioMock));
            controlador.Request.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

            var idDiccionario = "8a87f8a7-3df9-4d90-9478-350b964fc888";

            //Act
            var respuesta = controlador.ModificarUnDiccionario(controlador.Request, idDiccionario);

            //Assert
            respuesta.StatusCode.ShouldEqual(HttpStatusCode.BadRequest, "El formato del diccionario proporcionado se encuentra mal definido");
        }

        [Test]
        public void DeberiaPoderRetornarHttpStatusCodeNotFoundCuandoSeSolicitaModificarUnDiccionarioRetornaDiccionarioNoEncontrado()
        {
            //Arrange
            var controlador = new controladores.DiccionariosController(this.appMantenimientoDiccionario);
            controlador.Configuration = new HttpConfiguration();
            var modificarUnDiccionarioRespuesta = appModelosRespuesta.ModificarUnDiccionarioRespuesta.CrearNuevaInstancia();
            modificarUnDiccionarioRespuesta.Diccionario = Diccionario.CrearNuevoDiccionario(new Guid("9a39ad6d-62c8-42bf-a8f7-66417b2b08d0"), AmbienteTestPrueba);
            modificarUnDiccionarioRespuesta.Diccionario = null;
            this.appMantenimientoDiccionario.ModificarUnDiccionario(Arg.Any<ModificarUnDiccionarioPeticion>()).ReturnsForAnyArgs<appModelosRespuesta.ModificarUnDiccionarioRespuesta>(modificarUnDiccionarioRespuesta);


            controlador.Request = new HttpRequestMessage(HttpMethod.Put, "api/diccionario/9a39ad6d-62c8-42bf-a8f7-66417b2b08d0");
            var diccionario = utilitario.ConfigurarLlamadasHttpMock.ConfigurarMockPeticionHttp(AmbienteTestPrueba, "8a87f8a7-3df9-4d90-9478-350b964fc888");

            controlador.Request.Content = new StringContent(JsonConvert.SerializeObject(diccionario.DiccionarioMock));
            controlador.Request.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

            //Act
            var respuesta = controlador.ModificarUnDiccionario(controlador.Request, "9a39ad6d-62c8-42bf-a8f7-66417b2b08d0");

            //Assert
            respuesta.StatusCode.ShouldEqual(HttpStatusCode.NotFound);
        }

        #endregion

        #region Pruebas de eliminación (DELETE)
        [Test]
        public void DeberiaPoderRetornarHttpStatusCodeOkCuandoSeSolicitaEliminarUnDiccionarioRetornaLaListaDeDiccionarios()
        {
            //Arrange
            var controlador = new controladores.DiccionariosController(this.appMantenimientoDiccionario);
            controlador.Configuration = new HttpConfiguration();
            var eliminarDiccionarioRespuesta = appModelosRespuesta.EliminarUnDiccionarioRespuesta.CrearNuevaInstancia();

            var diccionarios = JsonConvert.DeserializeObject<comunes.Diccionarios>(DiccionarioConEtiquetasJson);
            eliminarDiccionarioRespuesta.ListaDeDiccionarios = utilitario.MapearEntidades.MapearDiccionariosTipoComunesConTipoAplicacionParaMock(diccionarios);

            appMantenimientoDiccionario.EliminarUnDiccionario(Arg.Any<EliminarUnDiccionarioPeticion>()).ReturnsForAnyArgs<appModelosRespuesta.EliminarUnDiccionarioRespuesta>(eliminarDiccionarioRespuesta);

            
            controlador.Request = new HttpRequestMessage(HttpMethod.Delete, "api/diccionario/9a39ad6d-62c8-42bf-a8f7-66417b2b08d0");
            var diccionario = utilitario.ConfigurarLlamadasHttpMock.ConfigurarMockPeticionHttp(AmbienteTestPrueba, "8a87f8a7-3df9-4d90-9478-350b964fc888");

            controlador.Request.Content = new StringContent(JsonConvert.SerializeObject(diccionario.DiccionarioMock));
            controlador.Request.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

            //Act
            var respuesta = controlador.EliminarUnDiccionario(controlador.Request, "9a39ad6d-62c8-42bf-a8f7-66417b2b08d0");

            //Assert
            respuesta.StatusCode.ShouldEqual(HttpStatusCode.OK);
            var validarContenidoRespuesta = JsonConvert.DeserializeObject<webApiModelosRespuesta.EliminarUnDiccionarioRespuesta>(respuesta.Content.ReadAsStringAsync().Result);

            validarContenidoRespuesta.ListaDiccionarios.Count.ShouldBeGreaterThanOrEqualTo(1);
        }

        [Test]
        public void DeberiaPoderRetornarHttpStatusCodeConflictCuandoSeSolicitaEliminarUnDiccionario()
        {
            //Arrange
            var controlador = new controladores.DiccionariosController(this.appMantenimientoDiccionario);
            controlador.Configuration = new HttpConfiguration();
            
            var eliminarDiccionarioRespuesta = appModelosRespuesta.EliminarUnDiccionarioRespuesta.CrearNuevaInstancia();

            eliminarDiccionarioRespuesta.ListaDeDiccionarios = null;

            appMantenimientoDiccionario.EliminarUnDiccionario(Arg.Any<EliminarUnDiccionarioPeticion>()).ReturnsForAnyArgs<appModelosRespuesta.EliminarUnDiccionarioRespuesta>(eliminarDiccionarioRespuesta);

            controlador.Request = new HttpRequestMessage(HttpMethod.Delete, "api/diccionario/9a39ad6d-62c8-42bf-a8f7-66417b2b08d0");
            var diccionario = utilitario.ConfigurarLlamadasHttpMock.ConfigurarMockPeticionHttp(AmbienteTestPrueba, "8a87f8a7-3df9-4d90-9478-350b964fc888");

            controlador.Request.Content = new StringContent(JsonConvert.SerializeObject(diccionario.DiccionarioMock));
            controlador.Request.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

            //Act
            var respuesta = controlador.EliminarUnDiccionario(controlador.Request, "9a39ad6d-62c8-42bf-a8f7-66417b2b08d0");

            //Assert
            respuesta.StatusCode.ShouldEqual(HttpStatusCode.Conflict);
        }

        [Test]
        public void DeberiaPoderRetornarHttpStatusCodeInternalServerErrorCuandoSeSolicitaEliminarUnDiccionario()
        {
            //Arrange
            var controlador = new controladores.DiccionariosController(this.appMantenimientoDiccionario);
            controlador.Configuration = new HttpConfiguration();

            var eliminarDiccionarioRespuesta = appModelosRespuesta.EliminarUnDiccionarioRespuesta.CrearNuevaInstancia();

            eliminarDiccionarioRespuesta = null;

            appMantenimientoDiccionario.EliminarUnDiccionario(Arg.Any<EliminarUnDiccionarioPeticion>()).ReturnsForAnyArgs<appModelosRespuesta.EliminarUnDiccionarioRespuesta>(eliminarDiccionarioRespuesta);

            controlador.Request = new HttpRequestMessage(HttpMethod.Delete, "api/diccionario/9a39ad6d-62c8-42bf-a8f7-66417b2b08d0");
            var diccionario = utilitario.ConfigurarLlamadasHttpMock.ConfigurarMockPeticionHttp(AmbienteTestPrueba, "8a87f8a7-3df9-4d90-9478-350b964fc888");

            controlador.Request.Content = new StringContent(JsonConvert.SerializeObject(diccionario.DiccionarioMock));
            controlador.Request.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

            //Act
            var respuesta = controlador.EliminarUnDiccionario(controlador.Request, "9a39ad6d-62c8-42bf-a8f7-66417b2b08d0");

            //Assert
            respuesta.StatusCode.ShouldEqual(HttpStatusCode.InternalServerError);
        }
        #endregion

    }

}

