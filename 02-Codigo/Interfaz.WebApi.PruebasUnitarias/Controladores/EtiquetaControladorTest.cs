using System;
using System.Linq;
using NUnit.Framework;
using Newtonsoft.Json;
using Should;
using appModelosPeticion = Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Peticion;
using app = Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Fachada;
using appModelosRespuesta = Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Respuesta;
using NSubstitute;
using controladores = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Controladores;
using System.Net;
using comunes = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Comunes;
using dominio = Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.Entidades;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using webApiModelosRespuesta = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Respuesta;
using utilitario = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.PruebasUnitarias.Utilitarios;

namespace Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.PruebasUnitarias
{
    [TestFixture]
    class EtiquetaControladorTest
    {
        #region Variables y propiedades globales

        private const string AmbienteTestPrueba = "Prueba";

        private const string EtiquetaJson = "{ \"etiquetas\": [ { \"id\": \"094bf626-d137-47ea-81fa-19a0aaceedf5\", \"activo\": true, \"idiomapordefecto\": \"es-VE\", \"nombre\": \"app.common.aceptar\", \"descripcion\": \"Aceptar\", \"textos\": [ { \"cultura\": \"es\", \"texto\": \"aceptar\" }, { \"cultura\": \"es-VE\", \"texto\": \"aceptar\" }, { \"cultura\": \"en\", \"texto\": \"accept\" }, { \"cultura\": \"en-US\", \"texto\": \"accept\" } ] }, { \"id\": \"98d723fd-b301-41e2-90a2-90c66a6835b8\", \"activo\": true, \"idiomapordefecto\": \"es-VE\", \"nombre\": \"app.common.cancelar\", \"descripcion\": \"cancelar\", \"textos\": [ { \"cultura\": \"es\", \"texto\": \"cancelar\" }, { \"cultura\": \"es-VE\", \"texto\": \"cancelar\" }, { \"cultura\": \"en\", \"texto\": \"cancel\" }, { \"cultura\": \"en-US\", \"texto\": \"cancel\" } ] }, { \"id\": \"c4d2f76e-cc6a-4481-853c-47f1cd7eafdc\", \"activo\": true, \"idiomapordefecto\": \"en\", \"nombre\": \"app.common.usuario\", \"descripcion\": \"Campo de texto usuario\", \"textos\": [ { \"cultura\": \"es\", \"texto\": \"usuario\" }, { \"cultura\": \"es-VE\", \"texto\": \"usuario\" }, { \"cultura\": \"en\", \"texto\": \"user\" }, { \"cultura\": \"en-US\", \"texto\": \"user\" } ] }, { \"id\": \"07eca348-ae16-43e4-a16f-0f8039ab1e35\", \"activo\": true, \"idiomapordefecto\": \"en\", \"nombre\": \"app.common.contraseña\", \"descripcion\": \"Campo de texto contraseña\", \"textos\": [ { \"cultura\": \"es\", \"texto\": \"contraseña\" }, { \"cultura\": \"es-VE\", \"texto\": \"contraseña\" }, { \"cultura\": \"en\", \"texto\": \"password\" }, { \"cultura\": \"en-US\", \"texto\": \"password\" } ] } ] }";

        private const string DiccionarioConEtiquetasJson = "{\"diccionarios\":[{\"etiquetas\": [ { \"id\": \"094bf626-d137-47ea-81fa-19a0aaceedf5\", \"activo\": true, \"idiomapordefecto\": \"es-VE\", \"nombre\": \"app.common.aceptar\", \"descripcion\": \"Aceptar\", \"textos\": [ { \"cultura\": \"es\", \"texto\": \"aceptar\" }, { \"cultura\": \"es-VE\", \"texto\": \"aceptar\" }, { \"cultura\": \"en\", \"texto\": \"accept\" }, { \"cultura\": \"en-US\", \"texto\": \"accept\" } ] }, { \"id\": \"98d723fd-b301-41e2-90a2-90c66a6835b8\", \"activo\": true, \"idiomapordefecto\": \"es-VE\", \"nombre\": \"app.common.cancelar\", \"descripcion\": \"cancelar\", \"textos\": [ { \"cultura\": \"es\", \"texto\": \"cancelar\" }, { \"cultura\": \"es-VE\", \"texto\": \"cancelar\" }, { \"cultura\": \"en\", \"texto\": \"cancel\" }, { \"cultura\": \"en-US\", \"texto\": \"cancel\" } ] }, { \"id\": \"c4d2f76e-cc6a-4481-853c-47f1cd7eafdc\", \"activo\": true, \"idiomapordefecto\": \"en\", \"nombre\": \"app.common.usuario\", \"descripcion\": \"Campo de texto usuario\", \"textos\": [ { \"cultura\": \"es\", \"texto\": \"usuario\" }, { \"cultura\": \"es-VE\", \"texto\": \"usuario\" }, { \"cultura\": \"en\", \"texto\": \"user\" }, { \"cultura\": \"en-US\", \"texto\": \"user\" } ] }, { \"id\": \"07eca348-ae16-43e4-a16f-0f8039ab1e35\", \"activo\": true, \"idiomapordefecto\": \"en\", \"nombre\": \"app.common.contraseña\", \"descripcion\": \"Campo de texto contraseña\", \"textos\": [ { \"cultura\": \"es\", \"texto\": \"contraseña\" }, { \"cultura\": \"es-VE\", \"texto\": \"contraseña\" }, { \"cultura\": \"en\", \"texto\": \"password\" }, { \"cultura\": \"en-US\", \"texto\": \"password\" } ] } ],\"id\":\"a1fa3369-bc3f-4ebc-9cac-5677cbaa8114\"}]}";

        private readonly app.IAdministradorDeEtiquetas appMantenimientoDiccionarioEtiquetas;

        #endregion

        #region Constructor de las pruebas
        public EtiquetaControladorTest()
        {
            // Se inicializa el proxy del NSustitute para posteriormente inyectar los mocks la dependencia
            this.appMantenimientoDiccionarioEtiquetas = Substitute.For<app.IAdministradorDeEtiquetas>();

            
        }
        #endregion

        #region Pruebas de Consulta (GET)
        [Test]
        public void DeberiaPoderRetornarHttpStatusCodeOkCuandoSeConsultaUnasEtiquetasPorNombreRetornaListaDeDiccionariosConEtiquetas()
        {
            //arrange
            //Se crea la instancia del controlador de etiquetas
            var controlador = new controladores.EtiquetasController(this.appMantenimientoDiccionarioEtiquetas);
            controlador.Configuration = new HttpConfiguration();
            var consultarEtiquetasPorNombre = appModelosRespuesta.ConsultarEtiquetasPorNombreRespuesta.CrearNuevaInstancia();
            
            consultarEtiquetasPorNombre.ListaDeDiccionarios = new List<Nucleo.Dominio.Entidades.Diccionario.Diccionario>();

            var diccionarios = JsonConvert.DeserializeObject<comunes.Diccionarios>(DiccionarioConEtiquetasJson);

            consultarEtiquetasPorNombre.ListaDeDiccionarios = utilitario.MapearEntidades.MapearDiccionariosTipoComunesConTipoAplicacionParaMock(diccionarios);
            string nombreEtiqueta = "aceptar";

            this.appMantenimientoDiccionarioEtiquetas.ConsultarEtiquetasPorNombre(Arg.Any<appModelosPeticion.ConsultarEtiquetasPorNombrePeticion>()).ReturnsForAnyArgs<appModelosRespuesta.ConsultarEtiquetasPorNombreRespuesta>(consultarEtiquetasPorNombre);

            controlador.Request = new HttpRequestMessage(HttpMethod.Get, "api/etiquetas/nombre/aceptar");

             //Act
            var respuesta = controlador.ConsultarEtiquetasPorNombre(nombreEtiqueta);

            //Assert
            respuesta.StatusCode.ShouldEqual(HttpStatusCode.OK);
        }

        [Test]
        public void DeberiaPoderRetornarHttpStatusCodeNotFoundCuandoLaConsultaDeEtiquetasPorNombreRetornaListaEtiquetasVacia()
        {
            //arrange
            //Se crea la instancia del controlador de etiquetas
            var controlador = new controladores.EtiquetasController(this.appMantenimientoDiccionarioEtiquetas);
            controlador.Configuration = new HttpConfiguration();
            var consultarEtiquetasPorNombre = appModelosRespuesta.ConsultarEtiquetasPorNombreRespuesta.CrearNuevaInstancia();
            consultarEtiquetasPorNombre.ListaDeDiccionarios = new List<Nucleo.Dominio.Entidades.Diccionario.Diccionario>();

            string nombreEtiqueta = "aceptar";

            this.appMantenimientoDiccionarioEtiquetas.ConsultarEtiquetasPorNombre(Arg.Any<appModelosPeticion.ConsultarEtiquetasPorNombrePeticion>()).ReturnsForAnyArgs<appModelosRespuesta.ConsultarEtiquetasPorNombreRespuesta>(consultarEtiquetasPorNombre);

            controlador.Request = new HttpRequestMessage(HttpMethod.Get, "api/etiquetas/nombre/aceptar");

            //Act
            var respuesta = controlador.ConsultarEtiquetasPorNombre(nombreEtiqueta);

            //Assert
            respuesta.StatusCode.ShouldEqual(HttpStatusCode.NoContent);
        }

        [Test]
        public void DeberiaPoderRetornarHttpStatusCodeOkCuandoSeConsultaUnasEtiquetasPorIdiomaPorDefectoDeUnDiccionarioRetornaLaListaDeEtiquetas() 
        {
            //Arrange
            //Se crea la instancia del controlador de etiquetas
            var controlador = new controladores.EtiquetasController(this.appMantenimientoDiccionarioEtiquetas);
            controlador.Configuration = new HttpConfiguration();
            var consultarEtiquetasDeUnDiccionarioPorIdiomaPorDefecto = appModelosRespuesta.ConsultarEtiquetasDeDiccionarioPorIdiomaPorDefectoRespuesta.CrearNuevaInstancia();
            
            var listaEtiquetas = JsonConvert.DeserializeObject<comunes.Etiquetas>(EtiquetaJson);
            consultarEtiquetasDeUnDiccionarioPorIdiomaPorDefecto.ListaDeEtiquetas = utilitario.MapearEntidades.MapearEtiquetasTipoComunesConTipoAplicacionParaMock(listaEtiquetas.ListaEtiquetas);

            consultarEtiquetasDeUnDiccionarioPorIdiomaPorDefecto.Relaciones = new Dictionary<string, Guid>();
            consultarEtiquetasDeUnDiccionarioPorIdiomaPorDefecto.Relaciones.Add("diccionario", new Guid("9a39ad6d-62c8-42bf-a8f7-66417b2b08d0"));

            this.appMantenimientoDiccionarioEtiquetas.ConsultarEtiquetasDeDiccionarioPorIdiomaPorDefecto(Arg.Any<appModelosPeticion.ConsultarEtiquetasDeDiccionarioPorIdiomaPorDefectoPeticion>()).ReturnsForAnyArgs<appModelosRespuesta.ConsultarEtiquetasDeDiccionarioPorIdiomaPorDefectoRespuesta>(consultarEtiquetasDeUnDiccionarioPorIdiomaPorDefecto);

            controlador.Request = new HttpRequestMessage(HttpMethod.Get, "api/diccionario/9a39ad6d-62c8-42bf-a8f7-66417b2b08d0/etiquetas/idiomapordefecto/es-VE");

            //Act
            var respuesta = controlador.ConsultarEtiquetasDiccionarioPorIdiomaPorDefecto("9a39ad6d-62c8-42bf-a8f7-66417b2b08d0", "es-VE");

             var validarContenidoRespuesta = JsonConvert.DeserializeObject<webApiModelosRespuesta.ConsultarEtiquetasDeDiccionarioPorIdiomaPorDefectoRespuesta>(respuesta.Content.ReadAsStringAsync().Result);

            //Assert
            respuesta.StatusCode.ShouldEqual(HttpStatusCode.OK);
            validarContenidoRespuesta.Etiquetas.ListaEtiquetas.ShouldNotBeEmpty();
        }

        [Test]
        public void DeberiaPoderRetornarHttpStatusCodeNoContentCuandoSeConsultaUnasEtiquetasPorIdiomaPorDefectoDeUnDiccionarioRetornaLaListaDeEtiquetasVacia()
        {
            //Arrange
            //Se crea la instancia del controlador de etiquetas
            var controlador = new controladores.EtiquetasController(this.appMantenimientoDiccionarioEtiquetas);
            controlador.Configuration = new HttpConfiguration();
            var consultarEtiquetasDeUnDiccionarioPorIdiomaPorDefecto = appModelosRespuesta.ConsultarEtiquetasDeDiccionarioPorIdiomaPorDefectoRespuesta.CrearNuevaInstancia();
            consultarEtiquetasDeUnDiccionarioPorIdiomaPorDefecto.ListaDeEtiquetas = new List<dominio.Etiquetas.Etiqueta>();
            consultarEtiquetasDeUnDiccionarioPorIdiomaPorDefecto.Relaciones = new Dictionary<string, Guid>(); //{"diccionario",};
            consultarEtiquetasDeUnDiccionarioPorIdiomaPorDefecto.Relaciones.Add("diccionario", new Guid("9a39ad6d-62c8-42bf-a8f7-66417b2b08d0"));

            this.appMantenimientoDiccionarioEtiquetas.ConsultarEtiquetasDeDiccionarioPorIdiomaPorDefecto(Arg.Any<appModelosPeticion.ConsultarEtiquetasDeDiccionarioPorIdiomaPorDefectoPeticion>()).ReturnsForAnyArgs<appModelosRespuesta.ConsultarEtiquetasDeDiccionarioPorIdiomaPorDefectoRespuesta>(consultarEtiquetasDeUnDiccionarioPorIdiomaPorDefecto);

            controlador.Request = new HttpRequestMessage(HttpMethod.Get, "api/diccionario/9a39ad6d-62c8-42bf-a8f7-66417b2b08d0/etiquetas/idiomapordefecto/es-VE");

            //Act
            var respuesta = controlador.ConsultarEtiquetasDiccionarioPorIdiomaPorDefecto("9a39ad6d-62c8-42bf-a8f7-66417b2b08d0", "es-VE");

            //Assert
            respuesta.StatusCode.ShouldEqual(HttpStatusCode.NoContent);

        }

        [Test]
        public void DeberiaPoderRetornarHttpStatusCodeNotFoundCuandoSeConsultanUnasEtiquetasDeUnDiccionarioPorIdiomaPorDefectoRetornaDiccionarioNoEncontrado()
        {
            //Arrange
            var controlador = new controladores.EtiquetasController(this.appMantenimientoDiccionarioEtiquetas);
            controlador.Configuration = new HttpConfiguration();
            var consultarEtiquetasDeUnDiccionarioPorIdiomaPorDefecto = appModelosRespuesta.ConsultarEtiquetasDeDiccionarioPorIdiomaPorDefectoRespuesta.CrearNuevaInstancia();
            consultarEtiquetasDeUnDiccionarioPorIdiomaPorDefecto.ListaDeEtiquetas = new List<dominio.Etiquetas.Etiqueta>();

            this.appMantenimientoDiccionarioEtiquetas.ConsultarEtiquetasDeDiccionarioPorIdiomaPorDefecto(Arg.Any<appModelosPeticion.ConsultarEtiquetasDeDiccionarioPorIdiomaPorDefectoPeticion>()).ReturnsForAnyArgs<appModelosRespuesta.ConsultarEtiquetasDeDiccionarioPorIdiomaPorDefectoRespuesta>(consultarEtiquetasDeUnDiccionarioPorIdiomaPorDefecto);

            controlador.Request = new HttpRequestMessage(HttpMethod.Get, "api/diccionario/9a39ad6d-62c8-42bf-a8f7-66417b2b08d0/etiquetas/idiomapordefecto/es-VE");

            //Act
            var respuesta = controlador.ConsultarEtiquetasDiccionarioPorIdiomaPorDefecto("9a39ad6d-62c8-42bf-a8f7-66417b2b08d0", "es-VE");

            //Assert
            respuesta.StatusCode.ShouldEqual(HttpStatusCode.NotFound);
        }

        [Test]
        public void DeberiaPoderRetornarHttpStatusCodeOkCuandoSeConsultanUnasEtiquetasDeUnDiccionarioPorDescripcionRetornaLaListaDeEtiquetasEncontradas()
        {
            //Arrange
            var controlador = new controladores.EtiquetasController(this.appMantenimientoDiccionarioEtiquetas);
            controlador.Configuration = new HttpConfiguration();

            var consultarEtiquetasDeUnDiccionarioPorDescripcion = appModelosRespuesta.ConsultarEtiquetasDeDiccionarioPorDescripcionRespuesta.CrearNuevaInstancia();
            consultarEtiquetasDeUnDiccionarioPorDescripcion.ListaDeEtiquetas = new List<dominio.Etiquetas.Etiqueta>();

            var listaEtiquetas = JsonConvert.DeserializeObject<comunes.Etiquetas>(EtiquetaJson);

            consultarEtiquetasDeUnDiccionarioPorDescripcion.Relaciones.Clear();
            consultarEtiquetasDeUnDiccionarioPorDescripcion.Relaciones.Add("diccionario", new Guid("9a39ad6d-62c8-42bf-a8f7-66417b2b08d0")); 
            consultarEtiquetasDeUnDiccionarioPorDescripcion.ListaDeEtiquetas = utilitario.MapearEntidades.MapearEtiquetasTipoComunesConTipoAplicacionParaMock(listaEtiquetas.ListaEtiquetas);

                         this.appMantenimientoDiccionarioEtiquetas.ConsultarEtiquetasDeDiccionarioPorDescripcion(Arg.Any<appModelosPeticion.ConsultarEtiquetasDeDiccionarioPorDescripcionPeticion>()).ReturnsForAnyArgs<appModelosRespuesta.ConsultarEtiquetasDeDiccionarioPorDescripcionRespuesta>(consultarEtiquetasDeUnDiccionarioPorDescripcion);

            controlador.Request = new HttpRequestMessage(HttpMethod.Get, "api/diccionario/9a39ad6d-62c8-42bf-a8f7-66417b2b08d0/etiquetas/descripcion");

            Dictionary<string, string> parametros = new Dictionary<string, string>();

            parametros.Add("Descripcion", "Etiquetas para eliminar diccionarios");

            var etiqueta = utilitario.ConfigurarLlamadasHttpMock.ConfigurarMockPeticionHttp(AmbienteTestPrueba, "8a87f8a7-3df9-4d90-9478-350b964fc888", parametros);

            controlador.Request.Content = new StringContent(JsonConvert.SerializeObject(etiqueta.EtiquetaMock));
            controlador.Request.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

            //Act
            var respuesta = controlador.ConsultarEtiquetasDiccionarioPorDescripcion("9a39ad6d-62c8-42bf-a8f7-66417b2b08d0", controlador.Request);

            //Assert
            respuesta.StatusCode.ShouldEqual(HttpStatusCode.OK);
        }

        [Test]
        public void DeberiaPoderRetornarHttpStatusCodeNotFoundCuandoSeConsultanUnasEtiquetasDeUnDiccionarioPorDescripcionRetornaDiccionarioNoEncontrado()
        {
            //Arrange
            var controlador = new controladores.EtiquetasController(this.appMantenimientoDiccionarioEtiquetas);
            controlador.Configuration = new HttpConfiguration();
            
            var consultarEtiquetasDeUnDiccionarioPorDescripcion = appModelosRespuesta.ConsultarEtiquetasDeDiccionarioPorDescripcionRespuesta.CrearNuevaInstancia();
            
            consultarEtiquetasDeUnDiccionarioPorDescripcion.ListaDeEtiquetas = new List<dominio.Etiquetas.Etiqueta>();
           
            var listaEtiquetas = JsonConvert.DeserializeObject<comunes.Etiquetas>(EtiquetaJson);
            consultarEtiquetasDeUnDiccionarioPorDescripcion.ListaDeEtiquetas = utilitario.MapearEntidades.MapearEtiquetasTipoComunesConTipoAplicacionParaMock(listaEtiquetas.ListaEtiquetas);
            this.appMantenimientoDiccionarioEtiquetas.ConsultarEtiquetasDeDiccionarioPorDescripcion(Arg.Any<appModelosPeticion.ConsultarEtiquetasDeDiccionarioPorDescripcionPeticion>()).ReturnsForAnyArgs<appModelosRespuesta.ConsultarEtiquetasDeDiccionarioPorDescripcionRespuesta>(consultarEtiquetasDeUnDiccionarioPorDescripcion);

            controlador.Request = new HttpRequestMessage(HttpMethod.Get, "api/diccionario/9a39ad6d-62c8-42bf-a8f7-66417b2b08d0/etiquetas/descripcion");

            Dictionary<string,string> parametros = new Dictionary<string,string>();

            parametros.Add("Descripcion", "Etiquetas para eliminar diccionarios");

            var etiqueta = utilitario.ConfigurarLlamadasHttpMock.ConfigurarMockPeticionHttp(AmbienteTestPrueba, "8a87f8a7-3df9-4d90-9478-350b964fc888",parametros);

            controlador.Request.Content = new StringContent(JsonConvert.SerializeObject(etiqueta.EtiquetaMock));
            controlador.Request.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

            //Act
            var respuesta = controlador.ConsultarEtiquetasDiccionarioPorDescripcion("9a39ad6d-62c8-42bf-a8f7-66417b2b08d0",controlador.Request);

            //Assert
            respuesta.StatusCode.ShouldEqual(HttpStatusCode.NotFound);

        }

        [Test]
        public void DeberiaPoderRetornarHttpStatusCodeNoContentCuandoSeConsultanUnasEtiquetasDeUnDiccionarioPorDescripcionRetornaListaEtiquetasVacia()
        {
            //Arrange
            var controlador = new controladores.EtiquetasController(this.appMantenimientoDiccionarioEtiquetas);
            controlador.Configuration = new HttpConfiguration();

            var consultarEtiquetasDeUnDiccionarioPorDescripcion = appModelosRespuesta.ConsultarEtiquetasDeDiccionarioPorDescripcionRespuesta.CrearNuevaInstancia();

            consultarEtiquetasDeUnDiccionarioPorDescripcion.ListaDeEtiquetas = new List<dominio.Etiquetas.Etiqueta>();

            consultarEtiquetasDeUnDiccionarioPorDescripcion.Relaciones.Clear();
            consultarEtiquetasDeUnDiccionarioPorDescripcion.Relaciones.Add("diccionario", new Guid("9a39ad6d-62c8-42bf-a8f7-66417b2b08d0"));

            this.appMantenimientoDiccionarioEtiquetas.ConsultarEtiquetasDeDiccionarioPorDescripcion(Arg.Any<appModelosPeticion.ConsultarEtiquetasDeDiccionarioPorDescripcionPeticion>()).ReturnsForAnyArgs<appModelosRespuesta.ConsultarEtiquetasDeDiccionarioPorDescripcionRespuesta>(consultarEtiquetasDeUnDiccionarioPorDescripcion);

            controlador.Request = new HttpRequestMessage(HttpMethod.Get, "api/diccionario/9a39ad6d-62c8-42bf-a8f7-66417b2b08d0/etiquetas/descripcion");

            Dictionary<string, string> parametros = new Dictionary<string, string>();

            parametros.Add("Descripcion", "Etiquetas para eliminar diccionarios");

            var etiqueta = utilitario.ConfigurarLlamadasHttpMock.ConfigurarMockPeticionHttp(AmbienteTestPrueba, "8a87f8a7-3df9-4d90-9478-350b964fc888", parametros);

            controlador.Request.Content = new StringContent(JsonConvert.SerializeObject(etiqueta.EtiquetaMock));
            controlador.Request.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

            //Act
            var respuesta = controlador.ConsultarEtiquetasDiccionarioPorDescripcion("9a39ad6d-62c8-42bf-a8f7-66417b2b08d0", controlador.Request);

            //Assert
            respuesta.StatusCode.ShouldEqual(HttpStatusCode.NoContent);
        }

        [Test]
        public void DeberiaPoderRetornarHttpStatusCodeOkCuandoSeConsultanUnasEtiquetasDeUnDiccionarioPorEstatusRetornaLaListaDeEtiquetas()
        {
            //Arrange
            var controlador = new controladores.EtiquetasController(this.appMantenimientoDiccionarioEtiquetas);
            controlador.Configuration = new HttpConfiguration();
            var consultarEtiquetasDeUnDiccionarioPorEstatus = appModelosRespuesta.ConsultarEtiquetasDeDiccionarioPorEstatusRespuesta.CrearNuevaInstancia();

            var listaEtiquetas = JsonConvert.DeserializeObject<comunes.Etiquetas>(EtiquetaJson);

            consultarEtiquetasDeUnDiccionarioPorEstatus.Relaciones.Clear();
            consultarEtiquetasDeUnDiccionarioPorEstatus.Relaciones.Add("diccionario", new Guid("9a39ad6d-62c8-42bf-a8f7-66417b2b08d0"));
            consultarEtiquetasDeUnDiccionarioPorEstatus.ListaDeEtiquetas = utilitario.MapearEntidades.MapearEtiquetasTipoComunesConTipoAplicacionParaMock(listaEtiquetas.ListaEtiquetas);

            this.appMantenimientoDiccionarioEtiquetas.ConsultarEtiquetasDeDiccionarioPorEstatus(Arg.Any<appModelosPeticion.ConsultarEtiquetasDeDiccionarioPorEstatusPeticion>()).ReturnsForAnyArgs<appModelosRespuesta.ConsultarEtiquetasDeDiccionarioPorEstatusRespuesta>(consultarEtiquetasDeUnDiccionarioPorEstatus);

            var idDiccionario = "8a87f8a7-3df9-4d90-9478-350b964fc888";
            var estatus = "Inactivo";

            controlador.Request = new HttpRequestMessage(HttpMethod.Get, "api/diccionario/9a39ad6d-62c8-42bf-a8f7-66417b2b08d0/etiquetas/estatus/Inactivo");

            //Act
            var respuesta = controlador.ConsultarEtiquetasDiccionarioPorEstatus(idDiccionario, estatus);

            //Assert
            respuesta.StatusCode.ShouldEqual(HttpStatusCode.OK);
        }

        [Test]
        public void DeberiaPoderRetornarHttpStatusCodeBadRequestCuandoSeConsultanUnasEtiquetasDeUnDiccionarioPorEstatusRetornaParametroEstatusMalDefinido()
        {
            //Arrange
            var controlador = new controladores.EtiquetasController(this.appMantenimientoDiccionarioEtiquetas);
            controlador.Configuration = new HttpConfiguration();
            var consultarEtiquetasDeUnDiccionarioPorEstatus = appModelosRespuesta.ConsultarEtiquetasDeDiccionarioPorEstatusRespuesta.CrearNuevaInstancia();

            var listaEtiquetas = JsonConvert.DeserializeObject<comunes.Etiquetas>(EtiquetaJson);

            consultarEtiquetasDeUnDiccionarioPorEstatus.Relaciones.Clear();
            consultarEtiquetasDeUnDiccionarioPorEstatus.Relaciones.Add("diccionario", new Guid("9a39ad6d-62c8-42bf-a8f7-66417b2b08d0"));
            consultarEtiquetasDeUnDiccionarioPorEstatus.ListaDeEtiquetas = utilitario.MapearEntidades.MapearEtiquetasTipoComunesConTipoAplicacionParaMock(listaEtiquetas.ListaEtiquetas);

            this.appMantenimientoDiccionarioEtiquetas.ConsultarEtiquetasDeDiccionarioPorEstatus(Arg.Any<appModelosPeticion.ConsultarEtiquetasDeDiccionarioPorEstatusPeticion>()).ReturnsForAnyArgs<appModelosRespuesta.ConsultarEtiquetasDeDiccionarioPorEstatusRespuesta>(consultarEtiquetasDeUnDiccionarioPorEstatus);

            var idDiccionario = "8a87f8a7-3df9-4d90-9478-350b964fc888";
            var estatus = "miestatus";

            controlador.Request = new HttpRequestMessage(HttpMethod.Get, "api/diccionario/9a39ad6d-62c8-42bf-a8f7-66417b2b08d0/etiquetas/estatus/miestatus");

            //Act
            var respuesta = controlador.ConsultarEtiquetasDiccionarioPorEstatus(idDiccionario, estatus);

            //Assert
            respuesta.StatusCode.ShouldEqual(HttpStatusCode.BadRequest);
        }

        [Test]
        public void DeberiaPoderRetornarHttpStatusCodeNotFoundCuandoSeConultanUnasEtiquetasDeUnDiccionarioPorEstatusRetornaDiccionarioNoEncontrado()
        {
            //Arrange
            var controlador = new controladores.EtiquetasController(this.appMantenimientoDiccionarioEtiquetas);
            controlador.Configuration = new HttpConfiguration();
            var consultarEtiquetasDeUnDiccionarioPorEstatus = appModelosRespuesta.ConsultarEtiquetasDeDiccionarioPorEstatusRespuesta.CrearNuevaInstancia();

            var listaEtiquetas = JsonConvert.DeserializeObject<comunes.Etiquetas>(EtiquetaJson);

            consultarEtiquetasDeUnDiccionarioPorEstatus.ListaDeEtiquetas = utilitario.MapearEntidades.MapearEtiquetasTipoComunesConTipoAplicacionParaMock(listaEtiquetas.ListaEtiquetas);

            consultarEtiquetasDeUnDiccionarioPorEstatus.Relaciones.Clear();
            consultarEtiquetasDeUnDiccionarioPorEstatus.Relaciones.Add("diccionario", new Guid("00000000-0000-0000-0000-000000000000"));

            this.appMantenimientoDiccionarioEtiquetas.ConsultarEtiquetasDeDiccionarioPorEstatus(Arg.Any<appModelosPeticion.ConsultarEtiquetasDeDiccionarioPorEstatusPeticion>()).ReturnsForAnyArgs<appModelosRespuesta.ConsultarEtiquetasDeDiccionarioPorEstatusRespuesta>(consultarEtiquetasDeUnDiccionarioPorEstatus);

            var idDiccionario = "8a87f8a7-3df9-4d90-9478-350b964fc888";
            var estatus = "Activo";

            controlador.Request = new HttpRequestMessage(HttpMethod.Get, "api/diccionario/9a39ad6d-62c8-42bf-a8f7-66417b2b08d0/etiquetas/estatus/Activo");

            //Act
            var respuesta = controlador.ConsultarEtiquetasDiccionarioPorEstatus(idDiccionario, estatus);

            //Assert
            respuesta.StatusCode.ShouldEqual(HttpStatusCode.NotFound);
        }

        [Test]
        public void DeberiaPoderRetornarHttpStatusCodeNoContentCuandoSeConsultanUnasEtiquetasDeUnDiccionarioPorEstatusRetornaListaEtiquetasVacia()
        {
            //Arrange
            var controlador = new controladores.EtiquetasController(this.appMantenimientoDiccionarioEtiquetas);
            controlador.Configuration = new HttpConfiguration();
            var consultarEtiquetasDeUnDiccionarioPorEstatus = appModelosRespuesta.ConsultarEtiquetasDeDiccionarioPorEstatusRespuesta.CrearNuevaInstancia();

            consultarEtiquetasDeUnDiccionarioPorEstatus.ListaDeEtiquetas = new List<dominio.Etiquetas.Etiqueta>();
            consultarEtiquetasDeUnDiccionarioPorEstatus.Relaciones.Clear();
            consultarEtiquetasDeUnDiccionarioPorEstatus.Relaciones.Add("diccionario", new Guid("9a39ad6d-62c8-42bf-a8f7-66417b2b08d0"));

            this.appMantenimientoDiccionarioEtiquetas.ConsultarEtiquetasDeDiccionarioPorEstatus(Arg.Any<appModelosPeticion.ConsultarEtiquetasDeDiccionarioPorEstatusPeticion>()).ReturnsForAnyArgs<appModelosRespuesta.ConsultarEtiquetasDeDiccionarioPorEstatusRespuesta>(consultarEtiquetasDeUnDiccionarioPorEstatus);

            var idDiccionario = "8a87f8a7-3df9-4d90-9478-350b964fc888";
            var estatus = "Activo";

            controlador.Request = new HttpRequestMessage(HttpMethod.Get, "api/diccionario/9a39ad6d-62c8-42bf-a8f7-66417b2b08d0/etiquetas/estatus/Activo");

            //Act
            var respuesta = controlador.ConsultarEtiquetasDiccionarioPorEstatus(idDiccionario, estatus);

            //Assert
            respuesta.StatusCode.ShouldEqual(HttpStatusCode.NoContent);
        }

        [Test]
        public void DeberiaPoderRetornarHttpStatusCodeOkCuandoSeConsultanUnasEtiquetasDeUnDiccionarioPorIdiomaRetornaLaListaDeEtiquetas()
        {
            //Arrange
            var controlador = new controladores.EtiquetasController(this.appMantenimientoDiccionarioEtiquetas);
            controlador.Configuration = new HttpConfiguration();

            var consultarEtiquetasDeUnDiccionarioPorIdioma = appModelosRespuesta.ConsultarEtiquetasDeDiccionarioPorIdiomaRespuesta.CrearNuevaInstancia();

            consultarEtiquetasDeUnDiccionarioPorIdioma.ListaDeEtiquetas = new List<dominio.Etiquetas.Etiqueta>();

            var listaEtiquetas = JsonConvert.DeserializeObject<comunes.Etiquetas>(EtiquetaJson);

            consultarEtiquetasDeUnDiccionarioPorIdioma.ListaDeEtiquetas = utilitario.MapearEntidades.MapearEtiquetasTipoComunesConTipoAplicacionParaMock(listaEtiquetas.ListaEtiquetas);

            consultarEtiquetasDeUnDiccionarioPorIdioma.Relaciones.Clear();
            consultarEtiquetasDeUnDiccionarioPorIdioma.Relaciones.Add("diccionario", new Guid("9a39ad6d-62c8-42bf-a8f7-66417b2b08d0"));

            this.appMantenimientoDiccionarioEtiquetas.ConsultarEtiquetasDeDiccionarioPorIdioma(Arg.Any<appModelosPeticion.ConsultarEtiquetasDeDiccionarioPorIdiomaPeticion>()).ReturnsForAnyArgs<appModelosRespuesta.ConsultarEtiquetasDeDiccionarioPorIdiomaRespuesta>(consultarEtiquetasDeUnDiccionarioPorIdioma);

            controlador.Request = new HttpRequestMessage(HttpMethod.Get, "api/diccionario/9a39ad6d-62c8-42bf-a8f7-66417b2b08d0/etiquetas/idioma/es-VE");

            var idDiccionario = "8a87f8a7-3df9-4d90-9478-350b964fc888";
            var idioma = "es-VE";

            //Act
            var respuesta = controlador.ConsultarEtiquetasDiccionarioPorIdioma(idDiccionario,idioma);

            //Assert
            respuesta.StatusCode.ShouldEqual(HttpStatusCode.OK);
        }

        [Test]
        public void DeberiaPoderRetornarHttpStatusCodeBadRequestCuandoSeConsultanUnasEtiquetasDeUnDiccionarioPorIdiomaRetornaParametroIdiomaMalDefinido()
        {
            //Arrange
            var controlador = new controladores.EtiquetasController(this.appMantenimientoDiccionarioEtiquetas);
            controlador.Configuration = new HttpConfiguration();

            var consultarEtiquetasDeUnDiccionarioPorIdioma = appModelosRespuesta.ConsultarEtiquetasDeDiccionarioPorIdiomaRespuesta.CrearNuevaInstancia();

            this.appMantenimientoDiccionarioEtiquetas.ConsultarEtiquetasDeDiccionarioPorIdioma(Arg.Any<appModelosPeticion.ConsultarEtiquetasDeDiccionarioPorIdiomaPeticion>()).ReturnsForAnyArgs<appModelosRespuesta.ConsultarEtiquetasDeDiccionarioPorIdiomaRespuesta>(consultarEtiquetasDeUnDiccionarioPorIdioma);

            controlador.Request = new HttpRequestMessage(HttpMethod.Get, "api/diccionario/9a39ad6d-62c8-42bf-a8f7-66417b2b08d0/etiquetas/idioma/idiomainventado");

            var idDiccionario = "8a87f8a7-3df9-4d90-9478-350b964fc888";
            var idioma = "idiomainventado";

            //Act
            var respuesta = controlador.ConsultarEtiquetasDiccionarioPorIdioma(idDiccionario, idioma);

            //Assert
            respuesta.StatusCode.ShouldEqual(HttpStatusCode.BadRequest);
        }

        [Test]
        public void DeberiaPoderRetornarHttpNotFoundCuandoSeConsultanUnasEtiquetasDeUnDiccionarioPorIdiomaRetornaDiccionarioNoEncontrado()
        {
            //Arrange
            var controlador = new controladores.EtiquetasController(this.appMantenimientoDiccionarioEtiquetas);
            controlador.Configuration = new HttpConfiguration();

            var consultarEtiquetasDeUnDiccionarioPorIdioma = appModelosRespuesta.ConsultarEtiquetasDeDiccionarioPorIdiomaRespuesta.CrearNuevaInstancia();

            consultarEtiquetasDeUnDiccionarioPorIdioma.Relaciones.Clear();
            consultarEtiquetasDeUnDiccionarioPorIdioma.Relaciones.Add("diccionario", new Guid("00000000-0000-0000-0000-000000000000"));

            this.appMantenimientoDiccionarioEtiquetas.ConsultarEtiquetasDeDiccionarioPorIdioma(Arg.Any<appModelosPeticion.ConsultarEtiquetasDeDiccionarioPorIdiomaPeticion>()).ReturnsForAnyArgs<appModelosRespuesta.ConsultarEtiquetasDeDiccionarioPorIdiomaRespuesta>(consultarEtiquetasDeUnDiccionarioPorIdioma);

            controlador.Request = new HttpRequestMessage(HttpMethod.Get, "api/diccionario/9a39ad6d-62c8-42bf-a8f7-66417b2b08d0/etiquetas/idioma/es-VE");

            var idDiccionario = "9a39ad6d-62c8-42bf-a8f7-66417b2b08d0";
            var idioma = "es-VE";

            //Act
            var respuesta = controlador.ConsultarEtiquetasDiccionarioPorIdioma(idDiccionario, idioma);

            //Assert
            respuesta.StatusCode.ShouldEqual(HttpStatusCode.NotFound);
        }

        [Test]
        public void DeberiaPoderRetornarHttpNoContentCuandoSeConsultanUnasEtiquetasDeUnDiccionarioPorIdiomaRetornaListaDeEtiquetasVacia()
        {
            //Arrange
            var controlador = new controladores.EtiquetasController(this.appMantenimientoDiccionarioEtiquetas);
            controlador.Configuration = new HttpConfiguration();

            var consultarEtiquetasDeUnDiccionarioPorIdioma = appModelosRespuesta.ConsultarEtiquetasDeDiccionarioPorIdiomaRespuesta.CrearNuevaInstancia();

            consultarEtiquetasDeUnDiccionarioPorIdioma.ListaDeEtiquetas = new List<dominio.Etiquetas.Etiqueta>();

            consultarEtiquetasDeUnDiccionarioPorIdioma.Relaciones.Clear();
            consultarEtiquetasDeUnDiccionarioPorIdioma.Relaciones.Add("diccionario", new Guid("9a39ad6d-62c8-42bf-a8f7-66417b2b08d0"));

            this.appMantenimientoDiccionarioEtiquetas.ConsultarEtiquetasDeDiccionarioPorIdioma(Arg.Any<appModelosPeticion.ConsultarEtiquetasDeDiccionarioPorIdiomaPeticion>()).ReturnsForAnyArgs<appModelosRespuesta.ConsultarEtiquetasDeDiccionarioPorIdiomaRespuesta>(consultarEtiquetasDeUnDiccionarioPorIdioma);

            controlador.Request = new HttpRequestMessage(HttpMethod.Get, "api/diccionario/9a39ad6d-62c8-42bf-a8f7-66417b2b08d0/etiquetas/idioma/es-VE");

            var idDiccionario = "9a39ad6d-62c8-42bf-a8f7-66417b2b08d0";
            var idioma = "es-VE";

            //Act
            var respuesta = controlador.ConsultarEtiquetasDiccionarioPorIdioma(idDiccionario, idioma);

            //Assert
            respuesta.StatusCode.ShouldEqual(HttpStatusCode.NoContent);
        }

        [Test]
        public void DeberiaPoderRetornarHttpStatusCodeOkCuandoSeConsultanUnasEtiquetasDeUnDiccionarioPorNombreRetornaLaListaDeEtiquetasEncontradas()
        {
            //Arrange
            var controlador = new controladores.EtiquetasController(this.appMantenimientoDiccionarioEtiquetas);
            controlador.Configuration = new HttpConfiguration();

            var consultarEtiquetasDeUnDiccionarioPorNombre = appModelosRespuesta.ConsultarEtiquetasDeDiccionarioPorNombreRespuesta.CrearNuevaInstancia();

            consultarEtiquetasDeUnDiccionarioPorNombre.Relaciones.Clear();
            consultarEtiquetasDeUnDiccionarioPorNombre.Relaciones.Add("diccionario", new Guid("9a39ad6d-62c8-42bf-a8f7-66417b2b08d0"));

            consultarEtiquetasDeUnDiccionarioPorNombre.ListaDeEtiquetas = new List<dominio.Etiquetas.Etiqueta>();
            var listaEtiquetas = JsonConvert.DeserializeObject<comunes.Etiquetas>(EtiquetaJson);
            consultarEtiquetasDeUnDiccionarioPorNombre.ListaDeEtiquetas = utilitario.MapearEntidades.MapearEtiquetasTipoComunesConTipoAplicacionParaMock(listaEtiquetas.ListaEtiquetas);

            this.appMantenimientoDiccionarioEtiquetas.ConsultarEtiquetasDeDiccionarioPorNombre(Arg.Any<appModelosPeticion.ConsultarEtiquetasDeDiccionarioPorNombrePeticion>()).ReturnsForAnyArgs<appModelosRespuesta.ConsultarEtiquetasDeDiccionarioPorNombreRespuesta>(consultarEtiquetasDeUnDiccionarioPorNombre);

            controlador.Request = new HttpRequestMessage(HttpMethod.Get, "api/diccionario/9a39ad6d-62c8-42bf-a8f7-66417b2b08d0/etiquetas/nombre/app.common.aceptar");

            var idDiccionario = "9a39ad6d-62c8-42bf-a8f7-66417b2b08d0";
            var nombre = "app.common.aceptar";

            //Act
            var respuesta = controlador.ConsultarEtiquetasDiccionarioPorNombre(idDiccionario,nombre);

            //Assert
            respuesta.StatusCode.ShouldEqual(HttpStatusCode.OK);
        }

        [Test]
        public void DeberiaPoderRetornarHttpStatusCodeNotFoundCuandoSeConultanUnasEtiquetasDeUnDiccionarioPorNombreRetornaDiccionarioNoEncontrado()
        {
            //Arrange
            var controlador = new controladores.EtiquetasController(this.appMantenimientoDiccionarioEtiquetas);
            controlador.Configuration = new HttpConfiguration();

            var consultarEtiquetasDeUnDiccionarioPorNombre = appModelosRespuesta.ConsultarEtiquetasDeDiccionarioPorNombreRespuesta.CrearNuevaInstancia();

            consultarEtiquetasDeUnDiccionarioPorNombre.Relaciones.Clear();
            consultarEtiquetasDeUnDiccionarioPorNombre.Relaciones.Add("diccionario", new Guid("00000000-0000-0000-0000-000000000000"));

            this.appMantenimientoDiccionarioEtiquetas.ConsultarEtiquetasDeDiccionarioPorNombre(Arg.Any<appModelosPeticion.ConsultarEtiquetasDeDiccionarioPorNombrePeticion>()).ReturnsForAnyArgs<appModelosRespuesta.ConsultarEtiquetasDeDiccionarioPorNombreRespuesta>(consultarEtiquetasDeUnDiccionarioPorNombre);

            controlador.Request = new HttpRequestMessage(HttpMethod.Get, "api/diccionario/9a39ad6d-62c8-42bf-a8f7-66417b2b08d0/etiquetas/nombre/app.common.aceptar");

            var idDiccionario = "9a39ad6d-62c8-42bf-a8f7-66417b2b08d0";
            var nombre = "app.common.aceptar";

            //Act
            var respuesta = controlador.ConsultarEtiquetasDiccionarioPorNombre(idDiccionario, nombre);

            //Assert
            respuesta.StatusCode.ShouldEqual(HttpStatusCode.NotFound);
        }

        [Test]
        public void DeberiaPoderRetornarHttpStatusCodeNoContentCuandoSeConsultanUnasEtiquetasDeUnDiccionarioPorNombreRetornaListaDeEtiquetasVacia()
        {
            //Arrange
            var controlador = new controladores.EtiquetasController(this.appMantenimientoDiccionarioEtiquetas);
            controlador.Configuration = new HttpConfiguration();

            var consultarEtiquetasDeUnDiccionarioPorNombre = appModelosRespuesta.ConsultarEtiquetasDeDiccionarioPorNombreRespuesta.CrearNuevaInstancia();

            consultarEtiquetasDeUnDiccionarioPorNombre.ListaDeEtiquetas = new List<dominio.Etiquetas.Etiqueta>();

            consultarEtiquetasDeUnDiccionarioPorNombre.Relaciones.Clear();
            consultarEtiquetasDeUnDiccionarioPorNombre.Relaciones.Add("diccionario", new Guid("9a39ad6d-62c8-42bf-a8f7-66417b2b08d0"));

            this.appMantenimientoDiccionarioEtiquetas.ConsultarEtiquetasDeDiccionarioPorNombre(Arg.Any<appModelosPeticion.ConsultarEtiquetasDeDiccionarioPorNombrePeticion>()).ReturnsForAnyArgs<appModelosRespuesta.ConsultarEtiquetasDeDiccionarioPorNombreRespuesta>(consultarEtiquetasDeUnDiccionarioPorNombre);

            controlador.Request = new HttpRequestMessage(HttpMethod.Get, "api/diccionario/9a39ad6d-62c8-42bf-a8f7-66417b2b08d0/etiquetas/nombre/app.common.aceptar");

            var idDiccionario = "9a39ad6d-62c8-42bf-a8f7-66417b2b08d0";
            var nombre = "app.common.aceptar";

            //Act
            var respuesta = controlador.ConsultarEtiquetasDiccionarioPorNombre(idDiccionario, nombre);

            //Assert
            respuesta.StatusCode.ShouldEqual(HttpStatusCode.NoContent);
        }
        #endregion

        #region Pruebas de Modificacion (PUT)
        [Test]
        public void DeberiaPoderRetornarHttpStatusCodeOkCuandoSeSolicitaModificarEtiquetasDeUnDiccionarioRetornaListaDeEtiquetasModificadas()
        {
            //Arrange
            var controlador = new controladores.EtiquetasController(this.appMantenimientoDiccionarioEtiquetas);
            controlador.Configuration = new HttpConfiguration();

            var modificarEtiquetasDeUnDiccionario = appModelosRespuesta.ModificarEtiquetasAUnDiccionarioRespuesta.CrearNuevaInstancia();

            modificarEtiquetasDeUnDiccionario.ListaDeEtiquetas = new List<dominio.Etiquetas.Etiqueta>();
            var listaEtiquetas = JsonConvert.DeserializeObject<comunes.Etiquetas>(EtiquetaJson);
            modificarEtiquetasDeUnDiccionario.ListaDeEtiquetas = utilitario.MapearEntidades.MapearEtiquetasTipoComunesConTipoAplicacionParaMock(listaEtiquetas.ListaEtiquetas);

            modificarEtiquetasDeUnDiccionario.Relaciones.Clear();
            modificarEtiquetasDeUnDiccionario.Relaciones.Add("diccionario", new Guid("9a39ad6d-62c8-42bf-a8f7-66417b2b08d0"));

            this.appMantenimientoDiccionarioEtiquetas.ModificarEtiquetasAUnDiccionario(Arg.Any<appModelosPeticion.ModificarEtiquetasAUnDiccionarioPeticion>()).ReturnsForAnyArgs<appModelosRespuesta.ModificarEtiquetasAUnDiccionarioRespuesta>(modificarEtiquetasDeUnDiccionario);

            controlador.Request = new HttpRequestMessage(HttpMethod.Put, "api/diccionario/9a39ad6d-62c8-42bf-a8f7-66417b2b08d0/etiquetas/");

            var etiqueta = utilitario.ConfigurarLlamadasHttpMock.ConfigurarMockPeticionHttp(AmbienteTestPrueba, "8a87f8a7-3df9-4d90-9478-350b964fc888");

            controlador.Request.Content = new StringContent(EtiquetaJson);

            controlador.Request.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

            var respuesta = controlador.ModificarEtiquetasAUnDiccionario(controlador.Request,"8a87f8a7-3df9-4d90-9478-350b964fc888");

            respuesta.StatusCode.ShouldEqual(HttpStatusCode.OK);
        }
        
        [Test]
        public void DeberiaPoderRetornarHttpStatusCodeBadRequestCuandoSeSolicitaModificarEtiquetasDeUnDiccionarioRetornaParametroListaEtiquetasMalDefinido()
        {
            //Arrange
            var controlador = new controladores.EtiquetasController(this.appMantenimientoDiccionarioEtiquetas);
            controlador.Configuration = new HttpConfiguration();

            var modificarEtiquetasDeUnDiccionario = appModelosRespuesta.ModificarEtiquetasAUnDiccionarioRespuesta.CrearNuevaInstancia();

            this.appMantenimientoDiccionarioEtiquetas.ModificarEtiquetasAUnDiccionario(Arg.Any<appModelosPeticion.ModificarEtiquetasAUnDiccionarioPeticion>()).ReturnsForAnyArgs<appModelosRespuesta.ModificarEtiquetasAUnDiccionarioRespuesta>(modificarEtiquetasDeUnDiccionario);

            controlador.Request = new HttpRequestMessage(HttpMethod.Put, "api/diccionario/9a39ad6d-62c8-42bf-a8f7-66417b2b08d0/etiquetas/");

            var etiqueta = utilitario.ConfigurarLlamadasHttpMock.ConfigurarMockPeticionHttp(AmbienteTestPrueba, "8a87f8a7-3df9-4d90-9478-350b964fc888");

            controlador.Request.Content = new StringContent(JsonConvert.SerializeObject(etiqueta.EtiquetaMock));
            controlador.Request.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

            //Act
            var respuesta = controlador.ModificarEtiquetasAUnDiccionario(controlador.Request, "8a87f8a7-3df9-4d90-9478-350b964fc888");

            //Assert
            respuesta.StatusCode.ShouldEqual(HttpStatusCode.BadRequest);
        }

        [Test]
        public void DeberiaPoderRetornarHttpStatusNotFoundCuandoSeSolicitaModificarEtiquetasDeUnDiccionarioRetornaDiccionarioNoEncontrado()
        {
            //Arrange
            var controlador = new controladores.EtiquetasController(this.appMantenimientoDiccionarioEtiquetas);
            controlador.Configuration = new HttpConfiguration();

            var modificarEtiquetasDeUnDiccionario = appModelosRespuesta.ModificarEtiquetasAUnDiccionarioRespuesta.CrearNuevaInstancia();

            modificarEtiquetasDeUnDiccionario.Relaciones.Clear();
            modificarEtiquetasDeUnDiccionario.Relaciones.Add("diccionario", new Guid("00000000-0000-0000-0000-000000000000"));

            this.appMantenimientoDiccionarioEtiquetas.ModificarEtiquetasAUnDiccionario(Arg.Any<appModelosPeticion.ModificarEtiquetasAUnDiccionarioPeticion>()).ReturnsForAnyArgs<appModelosRespuesta.ModificarEtiquetasAUnDiccionarioRespuesta>(modificarEtiquetasDeUnDiccionario);

            controlador.Request = new HttpRequestMessage(HttpMethod.Put, "api/diccionario/9a39ad6d-62c8-42bf-a8f7-66417b2b08d0/etiquetas/");

            var etiqueta = utilitario.ConfigurarLlamadasHttpMock.ConfigurarMockPeticionHttp(AmbienteTestPrueba, "8a87f8a7-3df9-4d90-9478-350b964fc888");

            controlador.Request.Content = new StringContent(EtiquetaJson);

            controlador.Request.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

            //Act
            var respuesta = controlador.ModificarEtiquetasAUnDiccionario(controlador.Request, "8a87f8a7-3df9-4d90-9478-350b964fc888");

            //Assert
            respuesta.StatusCode.ShouldEqual(HttpStatusCode.NotFound);
        }
        #endregion

        #region Pruebas de Agregación(POST)
        [Test]
        public void DeberiaPoderRetornarHttpStatusCodeCreatedCuandoSeSolicitaAgregarEtiquetasAUnDiccionarioRetornaDiccionarioConSusEtiquetas()
        {
            //Arrange
            var controlador = new controladores.EtiquetasController(this.appMantenimientoDiccionarioEtiquetas);
            controlador.Configuration = new HttpConfiguration();

            var agregarEtiquetasAUnDiccionario = appModelosRespuesta.AgregarEtiquetasAUnDiccionarioRespuesta.CrearNuevaInstancia();

            var listaEtiquetas = JsonConvert.DeserializeObject<comunes.Etiquetas>(EtiquetaJson);
            agregarEtiquetasAUnDiccionario.ListaDeEtiquetas = utilitario.MapearEntidades.MapearEtiquetasTipoComunesConTipoAplicacionParaMock(listaEtiquetas.ListaEtiquetas);

            agregarEtiquetasAUnDiccionario.Relaciones.Clear();
            agregarEtiquetasAUnDiccionario.Relaciones.Add("diccionario", new Guid("9a39ad6d-62c8-42bf-a8f7-66417b2b08d0"));

            this.appMantenimientoDiccionarioEtiquetas.AgregarEtiquetasAUnDiccionario(Arg.Any<appModelosPeticion.AgregarEtiquetasAUnDiccionarioPeticion>()).ReturnsForAnyArgs<appModelosRespuesta.AgregarEtiquetasAUnDiccionarioRespuesta>(agregarEtiquetasAUnDiccionario);

            controlador.Request = new HttpRequestMessage(HttpMethod.Post, "api/diccionario/9a39ad6d-62c8-42bf-a8f7-66417b2b08d0/etiquetas/");

            controlador.Request.Content = new StringContent(EtiquetaJson);

            controlador.Request.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
            
            var idDiccionario = "8a87f8a7-3df9-4d90-9478-350b964fc888";
            
            //Act
            var respuesta = controlador.AgregarEtiquetasAUnDiccionario(idDiccionario,controlador.Request);

            //Assert
            respuesta.StatusCode.ShouldEqual(HttpStatusCode.Created);
        }

        [Test]
        public void DeberiaPoderRetornarHttpStatusCodeBadRequestCuandoSeSolicitaAgregarEtiquetasAUnDiccionarioRetornaParamatrosDeEtiquetasMalDefinido()
        {
            //Arrange
            var controlador = new controladores.EtiquetasController(this.appMantenimientoDiccionarioEtiquetas);
            controlador.Configuration = new HttpConfiguration();

            var agregarEtiquetasAUnDiccionario = appModelosRespuesta.AgregarEtiquetasAUnDiccionarioRespuesta.CrearNuevaInstancia();

            this.appMantenimientoDiccionarioEtiquetas.AgregarEtiquetasAUnDiccionario(Arg.Any<appModelosPeticion.AgregarEtiquetasAUnDiccionarioPeticion>()).ReturnsForAnyArgs<appModelosRespuesta.AgregarEtiquetasAUnDiccionarioRespuesta>(agregarEtiquetasAUnDiccionario);

            controlador.Request = new HttpRequestMessage(HttpMethod.Post, "api/diccionario/9a39ad6d-62c8-42bf-a8f7-66417b2b08d0/etiquetas/");

            var etiqueta = utilitario.ConfigurarLlamadasHttpMock.ConfigurarMockPeticionHttp(AmbienteTestPrueba, "8a87f8a7-3df9-4d90-9478-350b964fc888");

            controlador.Request.Content = new StringContent(JsonConvert.SerializeObject(etiqueta.EtiquetaMock));

            controlador.Request.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

            var idDiccionario = "8a87f8a7-3df9-4d90-9478-350b964fc888";

            //Act
            var respuesta = controlador.AgregarEtiquetasAUnDiccionario(idDiccionario, controlador.Request);

            //Assert
            respuesta.StatusCode.ShouldEqual(HttpStatusCode.BadRequest);
        }


        [Test]
        public void DeberiaPoderRetornarHttpStatusNotFoundCuandoSeSolicitaAgregarEtiquetasAUnDiccionarioRetornaDiccionarioNoEncontrado()
        {
            //Arrange
            var controlador = new controladores.EtiquetasController(this.appMantenimientoDiccionarioEtiquetas);
            controlador.Configuration = new HttpConfiguration();

            var agregarEtiquetasAUnDiccionario = appModelosRespuesta.AgregarEtiquetasAUnDiccionarioRespuesta.CrearNuevaInstancia();

            agregarEtiquetasAUnDiccionario.Relaciones.Clear();
            agregarEtiquetasAUnDiccionario.Relaciones.Add("diccionario", new Guid("00000000-0000-0000-0000-000000000000"));

            this.appMantenimientoDiccionarioEtiquetas.AgregarEtiquetasAUnDiccionario(Arg.Any<appModelosPeticion.AgregarEtiquetasAUnDiccionarioPeticion>()).ReturnsForAnyArgs<appModelosRespuesta.AgregarEtiquetasAUnDiccionarioRespuesta>(agregarEtiquetasAUnDiccionario);

            controlador.Request = new HttpRequestMessage(HttpMethod.Post, "api/diccionario/9a39ad6d-62c8-42bf-a8f7-66417b2b08d0/etiquetas/");

            var etiqueta = utilitario.ConfigurarLlamadasHttpMock.ConfigurarMockPeticionHttp(AmbienteTestPrueba, "8a87f8a7-3df9-4d90-9478-350b964fc888");

            controlador.Request.Content = new StringContent(EtiquetaJson);

            controlador.Request.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

            var idDiccionario = "8a87f8a7-3df9-4d90-9478-350b964fc888";

            //Act
            var respuesta = controlador.AgregarEtiquetasAUnDiccionario(idDiccionario, controlador.Request);

            //Assert
            respuesta.StatusCode.ShouldEqual(HttpStatusCode.NotFound);
        }
        #endregion

        #region Pruebas de Eliminacion (DELETE)
        [Test]
        public void DeberiaPoderRetornarHttpStatusCodeDeleteCuandoSeSolicitaEliminarEtiquetasAUnDiccionarioRetornaDiccionarioConSusEtiquetas()
        {
            //Arrange
            var controlador = new controladores.EtiquetasController(this.appMantenimientoDiccionarioEtiquetas);
            controlador.Configuration = new HttpConfiguration();

            var eliminarEtiquetasAUnDiccionario = appModelosRespuesta.EliminarEtiquetasAUnDiccionarioRespuesta.CrearNuevaInstancia();

            var listaEtiquetas = JsonConvert.DeserializeObject<comunes.Etiquetas>(EtiquetaJson);
            eliminarEtiquetasAUnDiccionario.ListaDeEtiquetas = utilitario.MapearEntidades.MapearEtiquetasTipoComunesConTipoAplicacionParaMock(listaEtiquetas.ListaEtiquetas);

            eliminarEtiquetasAUnDiccionario.Relaciones.Clear();
            eliminarEtiquetasAUnDiccionario.Relaciones.Add("diccionario", new Guid("9a39ad6d-62c8-42bf-a8f7-66417b2b08d0"));

            this.appMantenimientoDiccionarioEtiquetas.EliminarEtiquetasAUnDiccionario(Arg.Any<appModelosPeticion.EliminarEtiquetasAUnDiccionarioPeticion>()).ReturnsForAnyArgs<appModelosRespuesta.EliminarEtiquetasAUnDiccionarioRespuesta>(eliminarEtiquetasAUnDiccionario);

            controlador.Request = new HttpRequestMessage(HttpMethod.Delete, "api/diccionario/9a39ad6d-62c8-42bf-a8f7-66417b2b08d0/etiquetas/");

            controlador.Request.Content = new StringContent(EtiquetaJson);

            controlador.Request.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

            var idDiccionario = "8a87f8a7-3df9-4d90-9478-350b964fc888";

            //Act
            var respuesta = controlador.EliminarEtiquetasAUnDiccionario(idDiccionario, controlador.Request);

            //Assert
            respuesta.StatusCode.ShouldEqual(HttpStatusCode.OK);
        }

        [Test]
        public void DeberiaPoderRetornarHttpStatusCodeBadRequestCuandoSeSolicitaEliminarEtiquetasAUnDiccionarioRetornaParametroEtiquetasMalDefinido()
        {
            //Arrange
            var controlador = new controladores.EtiquetasController(this.appMantenimientoDiccionarioEtiquetas);
            controlador.Configuration = new HttpConfiguration();

            var eliminarEtiquetasAUnDiccionario = appModelosRespuesta.EliminarEtiquetasAUnDiccionarioRespuesta.CrearNuevaInstancia();

            var listaEtiquetas = JsonConvert.DeserializeObject<comunes.Etiquetas>(EtiquetaJson);
            eliminarEtiquetasAUnDiccionario.ListaDeEtiquetas = utilitario.MapearEntidades.MapearEtiquetasTipoComunesConTipoAplicacionParaMock(listaEtiquetas.ListaEtiquetas);

            eliminarEtiquetasAUnDiccionario.Relaciones.Clear();
            eliminarEtiquetasAUnDiccionario.Relaciones.Add("diccionario", new Guid("9a39ad6d-62c8-42bf-a8f7-66417b2b08d0"));

            this.appMantenimientoDiccionarioEtiquetas.EliminarEtiquetasAUnDiccionario(Arg.Any<appModelosPeticion.EliminarEtiquetasAUnDiccionarioPeticion>()).ReturnsForAnyArgs<appModelosRespuesta.EliminarEtiquetasAUnDiccionarioRespuesta>(eliminarEtiquetasAUnDiccionario);

            controlador.Request = new HttpRequestMessage(HttpMethod.Delete, "api/diccionario/9a39ad6d-62c8-42bf-a8f7-66417b2b08d0/etiquetas/");

            controlador.Request.Content = new StringContent("");

            controlador.Request.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

            var idDiccionario = "8a87f8a7-3df9-4d90-9478-350b964fc888";

            //Act
            var respuesta = controlador.EliminarEtiquetasAUnDiccionario(idDiccionario, controlador.Request);

            //Assert
            respuesta.StatusCode.ShouldEqual(HttpStatusCode.BadRequest);
        }

        [Test]
        public void DeberiaPoderRetornarHttpStatusCodeNotFoundCuandoSeSolicitaEliminarEtiquetasDeUnDiccionarioRetornaDiccionarioNoEncontrado()
        {
            //arrange
            //Se crea la instancia del controlador de etiquetas
            var controlador = new controladores.EtiquetasController(this.appMantenimientoDiccionarioEtiquetas);
            controlador.Configuration = new HttpConfiguration();
            var eliminarEtiquetasDiccionario = appModelosRespuesta.EliminarEtiquetasAUnDiccionarioRespuesta.CrearNuevaInstancia();

            var listaEtiquetas = JsonConvert.DeserializeObject<comunes.Etiquetas>(EtiquetaJson);
            eliminarEtiquetasDiccionario.ListaDeEtiquetas = utilitario.MapearEntidades.MapearEtiquetasTipoComunesConTipoAplicacionParaMock(listaEtiquetas.ListaEtiquetas);

            eliminarEtiquetasDiccionario.Relaciones.Clear();
            eliminarEtiquetasDiccionario.Relaciones.Add("diccionario", new Guid("00000000-0000-0000-0000-000000000000"));

            this.appMantenimientoDiccionarioEtiquetas.EliminarEtiquetasAUnDiccionario(Arg.Any<appModelosPeticion.EliminarEtiquetasAUnDiccionarioPeticion>()).ReturnsForAnyArgs<appModelosRespuesta.EliminarEtiquetasAUnDiccionarioRespuesta>(eliminarEtiquetasDiccionario);

            controlador.Request = new HttpRequestMessage(HttpMethod.Delete, "api/diccionario/9a39ad6d-62c8-42bf-a8f7-66417b2b08d0/etiquetas/");
            var idDiccionario = "8a87f8a7-3df9-4d90-9478-350b964fc888";

            controlador.Request.Content = new StringContent(EtiquetaJson);

            //Act
            var respuesta = controlador.EliminarEtiquetasAUnDiccionario(idDiccionario, controlador.Request);

            //Assert
            respuesta.StatusCode.ShouldEqual(HttpStatusCode.NotFound);
        }
        #endregion

    }
}
