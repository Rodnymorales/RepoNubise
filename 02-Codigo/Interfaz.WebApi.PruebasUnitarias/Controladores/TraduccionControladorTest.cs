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
using utilitario = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.PruebasUnitarias.Utilitarios;

namespace Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.PruebasUnitarias
{
    [TestFixture]
    class TraduccionControladorTest
    {
        #region Variables y propiedades globales
        private const string EtiquetaJson = "{ \"textos\": [ { \"cultura\": \"es\", \"texto\": \"aceptar\" }, { \"cultura\": \"es-VE\", \"texto\": \"aceptar\" }, { \"cultura\": \"en\", \"texto\": \"accept\" }, { \"cultura\": \"en-US\", \"texto\": \"accept\" } ] }";

        private const string EtiquetaJsonCompleta = "{ \"etiquetas\": [ { \"id\": \"094bf626-d137-47ea-81fa-19a0aaceedf5\", \"activo\": true, \"idiomapordefecto\": \"es-VE\", \"nombre\": \"app.common.aceptar\", \"descripcion\": \"Aceptar\", \"textos\": [ { \"cultura\": \"es\", \"texto\": \"aceptar\" }, { \"cultura\": \"es-VE\", \"texto\": \"aceptar\" }, { \"cultura\": \"en\", \"texto\": \"accept\" }, { \"cultura\": \"en-US\", \"texto\": \"accept\" } ] }, { \"id\": \"98d723fd-b301-41e2-90a2-90c66a6835b8\", \"activo\": true, \"idiomapordefecto\": \"es-VE\", \"nombre\": \"app.common.cancelar\", \"descripcion\": \"cancelar\", \"textos\": [ { \"cultura\": \"es\", \"texto\": \"cancelar\" }, { \"cultura\": \"es-VE\", \"texto\": \"cancelar\" }, { \"cultura\": \"en\", \"texto\": \"cancel\" }, { \"cultura\": \"en-US\", \"texto\": \"cancel\" } ] }, { \"id\": \"c4d2f76e-cc6a-4481-853c-47f1cd7eafdc\", \"activo\": true, \"idiomapordefecto\": \"en\", \"nombre\": \"app.common.usuario\", \"descripcion\": \"Campo de texto usuario\", \"textos\": [ { \"cultura\": \"es\", \"texto\": \"usuario\" }, { \"cultura\": \"es-VE\", \"texto\": \"usuario\" }, { \"cultura\": \"en\", \"texto\": \"user\" }, { \"cultura\": \"en-US\", \"texto\": \"user\" } ] }, { \"id\": \"07eca348-ae16-43e4-a16f-0f8039ab1e35\", \"activo\": true, \"idiomapordefecto\": \"en\", \"nombre\": \"app.common.contraseña\", \"descripcion\": \"Campo de texto contraseña\", \"textos\": [ { \"cultura\": \"es\", \"texto\": \"contraseña\" }, { \"cultura\": \"es-VE\", \"texto\": \"contraseña\" }, { \"cultura\": \"en\", \"texto\": \"password\" }, { \"cultura\": \"en-US\", \"texto\": \"password\" } ] } ] }";

        app.IAdministradorDeTraducciones AppMantenimientoTraducciones { get; set; }
        #endregion

        #region Constructor de las pruebas
        public TraduccionControladorTest()
        {
            // Se inicializa el proxy del NSustitute para posteriormente inyectar los mocks la dependencia
            this.AppMantenimientoTraducciones = Substitute.For<app.IAdministradorDeTraducciones>();
        }
        #endregion

        #region Metodos de pruebas (POST)
        [Test]
        public void DeberiaPoderRetornarHttpStatusCodeOkAlSolicitarAgregarTraduccionesAUnEtiquetaDeUnDiccionarioRetornaListaDeTraducciones()
        {
            //Arrange
            var controlador = new controladores.TraduccionesController(this.AppMantenimientoTraducciones);
            controlador.Configuration = new HttpConfiguration();

            var agregarTraduccionesAUnaEtiquetaDeUnDiccionario = appModelosRespuesta.AgregarTraduccionesAUnaEtiquetaDeUnDiccionarioRespuesta.CrearNuevaInstancia();

            agregarTraduccionesAUnaEtiquetaDeUnDiccionario.Relaciones.Clear();
            agregarTraduccionesAUnaEtiquetaDeUnDiccionario.Relaciones.Add("diccionario", new Guid("9a39ad6d-62c8-42bf-a8f7-66417b2b08d0"));
            agregarTraduccionesAUnaEtiquetaDeUnDiccionario.Relaciones.Add("etiqueta", new Guid("8a87f8a7-3df9-4d90-9478-350b964fc888"));

            agregarTraduccionesAUnaEtiquetaDeUnDiccionario.ListaDeTraducciones = new List<dominio.Etiquetas.Traduccion>();
            var listaTraducciones = JsonConvert.DeserializeObject<comunes.Traducciones>(EtiquetaJson);
            agregarTraduccionesAUnaEtiquetaDeUnDiccionario.ListaDeTraducciones = utilitario.MapearEntidades.MapearTraduccionesTipoComunesConTipoAplicacionParaMock(listaTraducciones.Traducciones1);

            this.AppMantenimientoTraducciones.AgregarTraduccionesAUnaEtiquetaDeUnDiccionario(Arg.Any<appModelosPeticion.AgregarTraduccionesAUnaEtiquetaDeUnDiccionarioPeticion>()).ReturnsForAnyArgs<appModelosRespuesta.AgregarTraduccionesAUnaEtiquetaDeUnDiccionarioRespuesta>(agregarTraduccionesAUnaEtiquetaDeUnDiccionario);

            controlador.Request = new HttpRequestMessage(HttpMethod.Post, "api/diccionario/9a39ad6d-62c8-42bf-a8f7-66417b2b08d0/etiqueta/8a87f8a7-3df9-4d90-9478-350b964fc888/");
            controlador.Request.Content = new StringContent(EtiquetaJson);

            controlador.Request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var id1 = "9a39ad6d-62c8-42bf-a8f7-66417b2b08d0";
            var id2 = "8a87f8a7-3df9-4d90-9478-350b964fc888";
            
            //Act
            var respuesta = controlador.AgregarTraduccionesAUnaEtiquetaDeUnDiccionario(id1,id2,controlador.Request);
            
            //Assert
            respuesta.StatusCode.ShouldEqual(HttpStatusCode.Created);
        }

        [Test]
        public void DeberiaPoderRetornarHttpStatusCodeBadRequestAlSolicitarAgregarTraduccionesAUnEtiquetaDeUnDiccionarioRetornaParametroEtiquetasMalDefinido()
        {
            //Arrange
            var controlador = new controladores.TraduccionesController(this.AppMantenimientoTraducciones);
            controlador.Configuration = new HttpConfiguration();

            var agregarTraduccionesAUnaEtiquetaDeUnDiccionario = appModelosRespuesta.AgregarTraduccionesAUnaEtiquetaDeUnDiccionarioRespuesta.CrearNuevaInstancia();

            agregarTraduccionesAUnaEtiquetaDeUnDiccionario.Relaciones.Clear();
            agregarTraduccionesAUnaEtiquetaDeUnDiccionario.Relaciones.Add("diccionario", new Guid("9a39ad6d-62c8-42bf-a8f7-66417b2b08d0"));
            agregarTraduccionesAUnaEtiquetaDeUnDiccionario.Relaciones.Add("etiqueta", new Guid("8a87f8a7-3df9-4d90-9478-350b964fc888"));

            agregarTraduccionesAUnaEtiquetaDeUnDiccionario.ListaDeTraducciones = new List<dominio.Etiquetas.Traduccion>();
            var listaTraducciones = JsonConvert.DeserializeObject<comunes.Traducciones>(EtiquetaJson);
            agregarTraduccionesAUnaEtiquetaDeUnDiccionario.ListaDeTraducciones = utilitario.MapearEntidades.MapearTraduccionesTipoComunesConTipoAplicacionParaMock(listaTraducciones.Traducciones1);

            this.AppMantenimientoTraducciones.AgregarTraduccionesAUnaEtiquetaDeUnDiccionario(Arg.Any<appModelosPeticion.AgregarTraduccionesAUnaEtiquetaDeUnDiccionarioPeticion>()).ReturnsForAnyArgs<appModelosRespuesta.AgregarTraduccionesAUnaEtiquetaDeUnDiccionarioRespuesta>(agregarTraduccionesAUnaEtiquetaDeUnDiccionario);

            controlador.Request = new HttpRequestMessage(HttpMethod.Post, "api/diccionario/9a39ad6d-62c8-42bf-a8f7-66417b2b08d0/etiqueta/8a87f8a7-3df9-4d90-9478-350b964fc888/");
            controlador.Request.Content = new StringContent("");

            controlador.Request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var id1 = "9a39ad6d-62c8-42bf-a8f7-66417b2b08d0";
            var id2 = "8a87f8a7-3df9-4d90-9478-350b964fc888";

            //Act
            var respuesta = controlador.AgregarTraduccionesAUnaEtiquetaDeUnDiccionario(id1, id2, controlador.Request);

            //Assert
            respuesta.StatusCode.ShouldEqual(HttpStatusCode.BadRequest, "Formato de la lista de etiquetas se encuentra mal definida");
        }

        [Test]
        public void DeberiaPoderRetonarHttpStatusCodeNotFoundAlSolicitarAgregarTraduccionesAUnEtiquetaDeUnDiccionarioRetornaDiccionarioNoEncontrado()
        {
            //Arrange
            var controlador = new controladores.TraduccionesController(this.AppMantenimientoTraducciones);
            controlador.Configuration = new HttpConfiguration();

            var agregarTraduccionesAUnaEtiquetaDeUnDiccionario = appModelosRespuesta.AgregarTraduccionesAUnaEtiquetaDeUnDiccionarioRespuesta.CrearNuevaInstancia();

            agregarTraduccionesAUnaEtiquetaDeUnDiccionario.Relaciones.Clear();
            agregarTraduccionesAUnaEtiquetaDeUnDiccionario.Relaciones.Add("diccionario", new Guid("00000000-0000-0000-0000-000000000000"));

            this.AppMantenimientoTraducciones.AgregarTraduccionesAUnaEtiquetaDeUnDiccionario(Arg.Any<appModelosPeticion.AgregarTraduccionesAUnaEtiquetaDeUnDiccionarioPeticion>()).ReturnsForAnyArgs<appModelosRespuesta.AgregarTraduccionesAUnaEtiquetaDeUnDiccionarioRespuesta>(agregarTraduccionesAUnaEtiquetaDeUnDiccionario);

            controlador.Request = new HttpRequestMessage(HttpMethod.Post, "api/diccionario/9a39ad6d-62c8-42bf-a8f7-66417b2b08d0/etiqueta/8a87f8a7-3df9-4d90-9478-350b964fc888/");
            controlador.Request.Content = new StringContent(EtiquetaJson);

            controlador.Request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var id1 = "9a39ad6d-62c8-42bf-a8f7-66417b2b08d0";
            var id2 = "8a87f8a7-3df9-4d90-9478-350b964fc888";

            //Act
            var respuesta = controlador.AgregarTraduccionesAUnaEtiquetaDeUnDiccionario(id1, id2, controlador.Request);

            //Assert
            respuesta.StatusCode.ShouldEqual(HttpStatusCode.NotFound,"Diccionario no encontrado");
        }

        [Test]
        public void DeberiaPoderRetonarHttpStatusCodeNotFoundAlSolicitarAgregarTraduccionesAUnEtiquetaDeUnDiccionarioRetornaEtiquetaNoEncontrada()
        {
            //Arrange
            var controlador = new controladores.TraduccionesController(this.AppMantenimientoTraducciones);
            controlador.Configuration = new HttpConfiguration();

            var agregarTraduccionesAUnaEtiquetaDeUnDiccionario = appModelosRespuesta.AgregarTraduccionesAUnaEtiquetaDeUnDiccionarioRespuesta.CrearNuevaInstancia();

            agregarTraduccionesAUnaEtiquetaDeUnDiccionario.Relaciones.Clear();
            agregarTraduccionesAUnaEtiquetaDeUnDiccionario.Relaciones.Add("diccionario", new Guid("9a39ad6d-62c8-42bf-a8f7-66417b2b08d0"));
            agregarTraduccionesAUnaEtiquetaDeUnDiccionario.Relaciones.Add("etiqueta", new Guid("00000000-0000-0000-0000-000000000000"));

            this.AppMantenimientoTraducciones.AgregarTraduccionesAUnaEtiquetaDeUnDiccionario(Arg.Any<appModelosPeticion.AgregarTraduccionesAUnaEtiquetaDeUnDiccionarioPeticion>()).ReturnsForAnyArgs<appModelosRespuesta.AgregarTraduccionesAUnaEtiquetaDeUnDiccionarioRespuesta>(agregarTraduccionesAUnaEtiquetaDeUnDiccionario);

            controlador.Request = new HttpRequestMessage(HttpMethod.Post, "api/diccionario/9a39ad6d-62c8-42bf-a8f7-66417b2b08d0/etiqueta/8a87f8a7-3df9-4d90-9478-350b964fc888/");
            controlador.Request.Content = new StringContent(EtiquetaJson);

            controlador.Request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var id1 = "9a39ad6d-62c8-42bf-a8f7-66417b2b08d0";
            var id2 = "8a87f8a7-3df9-4d90-9478-350b964fc888";

            //Act
            var respuesta = controlador.AgregarTraduccionesAUnaEtiquetaDeUnDiccionario(id1, id2, controlador.Request);

            //Assert
            respuesta.StatusCode.ShouldEqual(HttpStatusCode.NotFound,"Etiqueta no encontrada");
        }
        #endregion

        #region Metodos de pruebas (PUT)
        [Test]
        public void DeberiaPoderRetornarHttpStatusCodeOkAlSolicitarModificarLasTraduccionesDeUnaEtiquetaDeUnDiccionarioRetornaListaDeEtiquetas()
        {
            //Arrange
            var controlador = new controladores.TraduccionesController(this.AppMantenimientoTraducciones);
            controlador.Configuration = new HttpConfiguration();

            var modificarTraduccionesAUnaEtiquetaDeUnDiccionario = appModelosRespuesta.ModificarTraduccionesAUnaEtiquetaDeUnDiccionarioRespuesta.CrearNuevaInstancia();

            modificarTraduccionesAUnaEtiquetaDeUnDiccionario.Relaciones.Clear();
            modificarTraduccionesAUnaEtiquetaDeUnDiccionario.Relaciones.Add("diccionario", new Guid("9a39ad6d-62c8-42bf-a8f7-66417b2b08d0"));
            modificarTraduccionesAUnaEtiquetaDeUnDiccionario.Relaciones.Add("etiqueta", new Guid("8a87f8a7-3df9-4d90-9478-350b964fc888"));

            modificarTraduccionesAUnaEtiquetaDeUnDiccionario.ListaDeTraducciones = new List<dominio.Etiquetas.Traduccion>();
            var listaTraducciones = JsonConvert.DeserializeObject<comunes.Traducciones>(EtiquetaJson);
            modificarTraduccionesAUnaEtiquetaDeUnDiccionario.ListaDeTraducciones = utilitario.MapearEntidades.MapearTraduccionesTipoComunesConTipoAplicacionParaMock(listaTraducciones.Traducciones1);

            this.AppMantenimientoTraducciones.ModificarTraduccionesAUnaEtiquetaDeUnDiccionario(Arg.Any<appModelosPeticion.ModificarTraduccionesAUnaEtiquetaDeUnDiccionarioPeticion>()).ReturnsForAnyArgs<appModelosRespuesta.ModificarTraduccionesAUnaEtiquetaDeUnDiccionarioRespuesta>(modificarTraduccionesAUnaEtiquetaDeUnDiccionario);

            controlador.Request = new HttpRequestMessage(HttpMethod.Put, "api/diccionario/9a39ad6d-62c8-42bf-a8f7-66417b2b08d0/etiqueta/8a87f8a7-3df9-4d90-9478-350b964fc888/");
            controlador.Request.Content = new StringContent(EtiquetaJson);

            controlador.Request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var id1 = "9a39ad6d-62c8-42bf-a8f7-66417b2b08d0";
            var id2 = "8a87f8a7-3df9-4d90-9478-350b964fc888";

            //Act
            var respuesta = controlador.ModificarTraduccionesAUnaEtiquetaDeUnDiccionario(id1, id2, controlador.Request);

            //Assert
            respuesta.StatusCode.ShouldEqual(HttpStatusCode.OK);
        }

        [Test]
        public void DeberiaPoderRetornarHttpStatusCodeBadRequestAlSolicitarModificarTraduccionesDeUnaEtiquetaDeUnDiccionarioRetornaParametroEtiquetasMalDefinido()
        {
            //Arrange
            var controlador = new controladores.TraduccionesController(this.AppMantenimientoTraducciones);
            controlador.Configuration = new HttpConfiguration();

            var modificarTraduccionesAUnaEtiquetaDeUnDiccionario = appModelosRespuesta.ModificarTraduccionesAUnaEtiquetaDeUnDiccionarioRespuesta.CrearNuevaInstancia();

            modificarTraduccionesAUnaEtiquetaDeUnDiccionario.Relaciones.Clear();
            modificarTraduccionesAUnaEtiquetaDeUnDiccionario.Relaciones.Add("diccionario", new Guid("9a39ad6d-62c8-42bf-a8f7-66417b2b08d0"));
            modificarTraduccionesAUnaEtiquetaDeUnDiccionario.Relaciones.Add("etiqueta", new Guid("8a87f8a7-3df9-4d90-9478-350b964fc888"));

            modificarTraduccionesAUnaEtiquetaDeUnDiccionario.ListaDeTraducciones = new List<dominio.Etiquetas.Traduccion>();
            var listaTraducciones = JsonConvert.DeserializeObject<comunes.Traducciones>(EtiquetaJson);
            modificarTraduccionesAUnaEtiquetaDeUnDiccionario.ListaDeTraducciones = utilitario.MapearEntidades.MapearTraduccionesTipoComunesConTipoAplicacionParaMock(listaTraducciones.Traducciones1);

            this.AppMantenimientoTraducciones.ModificarTraduccionesAUnaEtiquetaDeUnDiccionario(Arg.Any<appModelosPeticion.ModificarTraduccionesAUnaEtiquetaDeUnDiccionarioPeticion>()).ReturnsForAnyArgs<appModelosRespuesta.ModificarTraduccionesAUnaEtiquetaDeUnDiccionarioRespuesta>(modificarTraduccionesAUnaEtiquetaDeUnDiccionario);

            controlador.Request = new HttpRequestMessage(HttpMethod.Put, "api/diccionario/9a39ad6d-62c8-42bf-a8f7-66417b2b08d0/etiqueta/8a87f8a7-3df9-4d90-9478-350b964fc888/");
            controlador.Request.Content = new StringContent("");

            controlador.Request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var id1 = "9a39ad6d-62c8-42bf-a8f7-66417b2b08d0";
            var id2 = "8a87f8a7-3df9-4d90-9478-350b964fc888";

            //Act
            var respuesta = controlador.ModificarTraduccionesAUnaEtiquetaDeUnDiccionario(id1, id2, controlador.Request);

            //Assert
            respuesta.StatusCode.ShouldEqual(HttpStatusCode.BadRequest, "Formato de la lista de etiquetas se encuentra mal definida");
        }

        [Test]
        public void DeberiaPoderRetornarHttpStatusNotFoundAlSolicitarModificarLasTraduccionesAUnaEtiquetaDeUnDiccionarioRetornaDiccionarioNoEncontrado()
        {
            //Arrange
            var controlador = new controladores.TraduccionesController(this.AppMantenimientoTraducciones);
            controlador.Configuration = new HttpConfiguration();

            var modificarTraduccionesAUnaEtiquetaDeUnDiccionario = appModelosRespuesta.ModificarTraduccionesAUnaEtiquetaDeUnDiccionarioRespuesta.CrearNuevaInstancia();

            modificarTraduccionesAUnaEtiquetaDeUnDiccionario.Relaciones.Clear();
            modificarTraduccionesAUnaEtiquetaDeUnDiccionario.Relaciones.Add("diccionario", new Guid("00000000-0000-0000-0000-000000000000"));

            modificarTraduccionesAUnaEtiquetaDeUnDiccionario.ListaDeTraducciones = new List<dominio.Etiquetas.Traduccion>();
            var listaTraducciones = JsonConvert.DeserializeObject<comunes.Traducciones>(EtiquetaJson);
            modificarTraduccionesAUnaEtiquetaDeUnDiccionario.ListaDeTraducciones = utilitario.MapearEntidades.MapearTraduccionesTipoComunesConTipoAplicacionParaMock(listaTraducciones.Traducciones1);

            this.AppMantenimientoTraducciones.ModificarTraduccionesAUnaEtiquetaDeUnDiccionario(Arg.Any<appModelosPeticion.ModificarTraduccionesAUnaEtiquetaDeUnDiccionarioPeticion>()).ReturnsForAnyArgs<appModelosRespuesta.ModificarTraduccionesAUnaEtiquetaDeUnDiccionarioRespuesta>(modificarTraduccionesAUnaEtiquetaDeUnDiccionario);

            controlador.Request = new HttpRequestMessage(HttpMethod.Put, "api/diccionario/9a39ad6d-62c8-42bf-a8f7-66417b2b08d0/etiqueta/8a87f8a7-3df9-4d90-9478-350b964fc888/");
            controlador.Request.Content = new StringContent(EtiquetaJson);

            controlador.Request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var id1 = "9a39ad6d-62c8-42bf-a8f7-66417b2b08d0";
            var id2 = "8a87f8a7-3df9-4d90-9478-350b964fc888";

            //Act
            var respuesta = controlador.ModificarTraduccionesAUnaEtiquetaDeUnDiccionario(id1, id2, controlador.Request);

            //Assert
            respuesta.StatusCode.ShouldEqual(HttpStatusCode.NotFound, "Diccionario no encontrado");
        }

        [Test]
        public void DeberiaPoderRetornarHttpStatusNotFoundAlSolicitarModificarLasTraduccionesAUnaEtiquetaDeUnDiccionarioRetornaEtiquetaNoEncontrada()
        {
            //Arrange
            var controlador = new controladores.TraduccionesController(this.AppMantenimientoTraducciones);
            controlador.Configuration = new HttpConfiguration();

            var modificarTraduccionesAUnaEtiquetaDeUnDiccionario = appModelosRespuesta.ModificarTraduccionesAUnaEtiquetaDeUnDiccionarioRespuesta.CrearNuevaInstancia();

            modificarTraduccionesAUnaEtiquetaDeUnDiccionario.Relaciones.Clear();
            modificarTraduccionesAUnaEtiquetaDeUnDiccionario.Relaciones.Add("diccionario", new Guid("9a39ad6d-62c8-42bf-a8f7-66417b2b08d0"));
            modificarTraduccionesAUnaEtiquetaDeUnDiccionario.Relaciones.Add("etiqueta", new Guid("00000000-0000-0000-0000-000000000000"));

            modificarTraduccionesAUnaEtiquetaDeUnDiccionario.ListaDeTraducciones = new List<dominio.Etiquetas.Traduccion>();
            var listaTraducciones = JsonConvert.DeserializeObject<comunes.Traducciones>(EtiquetaJson);
            modificarTraduccionesAUnaEtiquetaDeUnDiccionario.ListaDeTraducciones = utilitario.MapearEntidades.MapearTraduccionesTipoComunesConTipoAplicacionParaMock(listaTraducciones.Traducciones1);

            this.AppMantenimientoTraducciones.ModificarTraduccionesAUnaEtiquetaDeUnDiccionario(Arg.Any<appModelosPeticion.ModificarTraduccionesAUnaEtiquetaDeUnDiccionarioPeticion>()).ReturnsForAnyArgs<appModelosRespuesta.ModificarTraduccionesAUnaEtiquetaDeUnDiccionarioRespuesta>(modificarTraduccionesAUnaEtiquetaDeUnDiccionario);

            controlador.Request = new HttpRequestMessage(HttpMethod.Put, "api/diccionario/9a39ad6d-62c8-42bf-a8f7-66417b2b08d0/etiqueta/8a87f8a7-3df9-4d90-9478-350b964fc888/");
            controlador.Request.Content = new StringContent(EtiquetaJson);

            controlador.Request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var id1 = "9a39ad6d-62c8-42bf-a8f7-66417b2b08d0";
            var id2 = "8a87f8a7-3df9-4d90-9478-350b964fc888";

            //Act
            var respuesta = controlador.ModificarTraduccionesAUnaEtiquetaDeUnDiccionario(id1, id2, controlador.Request);

            //Assert
            respuesta.StatusCode.ShouldEqual(HttpStatusCode.NotFound, "Etiqueta no encontrada");
        }
        #endregion

        #region Metodos de pruebas (DELETE)

        [Test]
        public void DeberiaPoderRetornarHttpStatusCodeOkAlSolicitarEliminarLasTraduccionesAUnEtiquetaDeUnDiccionarioRetornaListaTraduccionesRestantes()
        {
            //Arrange
            var controlador = new controladores.TraduccionesController(this.AppMantenimientoTraducciones);
            controlador.Configuration = new HttpConfiguration();

            var eliminarTraduccionesAUnaEtiquetaDeUnDiccionario = appModelosRespuesta.EliminarTraduccionesAUnaEtiquetaDeUnDiccionarioRespuesta.CrearNuevaInstancia();

            eliminarTraduccionesAUnaEtiquetaDeUnDiccionario.Relaciones.Clear();
            eliminarTraduccionesAUnaEtiquetaDeUnDiccionario.Relaciones.Add("diccionario", new Guid("9a39ad6d-62c8-42bf-a8f7-66417b2b08d0"));
            eliminarTraduccionesAUnaEtiquetaDeUnDiccionario.Relaciones.Add("etiqueta", new Guid("8a87f8a7-3df9-4d90-9478-350b964fc888"));

            eliminarTraduccionesAUnaEtiquetaDeUnDiccionario.ListaDeTraducciones = new List<dominio.Etiquetas.Traduccion>();
            var listaTraducciones = JsonConvert.DeserializeObject<comunes.Traducciones>(EtiquetaJson);
            eliminarTraduccionesAUnaEtiquetaDeUnDiccionario.ListaDeTraducciones = utilitario.MapearEntidades.MapearTraduccionesTipoComunesConTipoAplicacionParaMock(listaTraducciones.Traducciones1);

            this.AppMantenimientoTraducciones.EliminarTraduccionesAUnaEtiquetaDeUnDiccionario(Arg.Any<appModelosPeticion.EliminarTraduccionesAUnaEtiquetaDeUnDiccionarioPeticion>()).ReturnsForAnyArgs<appModelosRespuesta.EliminarTraduccionesAUnaEtiquetaDeUnDiccionarioRespuesta>(eliminarTraduccionesAUnaEtiquetaDeUnDiccionario);

            controlador.Request = new HttpRequestMessage(HttpMethod.Delete, "api/diccionario/9a39ad6d-62c8-42bf-a8f7-66417b2b08d0/etiqueta/8a87f8a7-3df9-4d90-9478-350b964fc888/");
            controlador.Request.Content = new StringContent(EtiquetaJson);

            controlador.Request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var id1 = "9a39ad6d-62c8-42bf-a8f7-66417b2b08d0";
            var id2 = "8a87f8a7-3df9-4d90-9478-350b964fc888";

            //Act
            var respuesta = controlador.EliminarTraduccionesAUnaEtiquetaDeunDiccionario(id1, id2, controlador.Request);

            //Assert
            respuesta.StatusCode.ShouldEqual(HttpStatusCode.OK);
        }

        [Test]
        public void DeberiaPoderRetornarHttpStatusCodeBadRequestAlSolicitarEliminarLasTraduccionesAUnaEtiquetaDeUnDiccionarioRetornaListaTraduccionesRestantes()
        {
            //Arrange
            var controlador = new controladores.TraduccionesController(this.AppMantenimientoTraducciones);
            controlador.Configuration = new HttpConfiguration();

            var eliminarTraduccionesAUnaEtiquetaDeUnDiccionario = appModelosRespuesta.EliminarTraduccionesAUnaEtiquetaDeUnDiccionarioRespuesta.CrearNuevaInstancia();

            eliminarTraduccionesAUnaEtiquetaDeUnDiccionario.Relaciones.Clear();
            eliminarTraduccionesAUnaEtiquetaDeUnDiccionario.Relaciones.Add("diccionario", new Guid("9a39ad6d-62c8-42bf-a8f7-66417b2b08d0"));
            eliminarTraduccionesAUnaEtiquetaDeUnDiccionario.Relaciones.Add("etiqueta", new Guid("8a87f8a7-3df9-4d90-9478-350b964fc888"));

            eliminarTraduccionesAUnaEtiquetaDeUnDiccionario.ListaDeTraducciones = new List<dominio.Etiquetas.Traduccion>();
            var listaTraducciones = JsonConvert.DeserializeObject<comunes.Traducciones>(EtiquetaJsonCompleta);
            eliminarTraduccionesAUnaEtiquetaDeUnDiccionario.ListaDeTraducciones = utilitario.MapearEntidades.MapearTraduccionesTipoComunesConTipoAplicacionParaMock(listaTraducciones.Traducciones1);

            this.AppMantenimientoTraducciones.EliminarTraduccionesAUnaEtiquetaDeUnDiccionario(Arg.Any<appModelosPeticion.EliminarTraduccionesAUnaEtiquetaDeUnDiccionarioPeticion>()).ReturnsForAnyArgs<appModelosRespuesta.EliminarTraduccionesAUnaEtiquetaDeUnDiccionarioRespuesta>(eliminarTraduccionesAUnaEtiquetaDeUnDiccionario);

            controlador.Request = new HttpRequestMessage(HttpMethod.Delete, "api/diccionario/9a39ad6d-62c8-42bf-a8f7-66417b2b08d0/etiqueta/8a87f8a7-3df9-4d90-9478-350b964fc888/");
            controlador.Request.Content = new StringContent(EtiquetaJsonCompleta);

            controlador.Request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var id1 = "9a39ad6d-62c8-42bf-a8f7-66417b2b08d0";
            var id2 = "8a87f8a7-3df9-4d90-9478-350b964fc888";

            //Act
            var respuesta = controlador.EliminarTraduccionesAUnaEtiquetaDeunDiccionario(id1, id2, controlador.Request);

            //Assert
            respuesta.StatusCode.ShouldEqual(HttpStatusCode.BadRequest);
        }

        [Test]
        public void DeberiaPoderRetornarHttpStatusNotFoundAlSolicitarEliminarLasTraduccionesAUnaEtiquetaDeUnDiccionarioRetornaDiccionarioNoEncontrado()
        {
            //Arrange
            var controlador = new controladores.TraduccionesController(this.AppMantenimientoTraducciones);
            controlador.Configuration = new HttpConfiguration();

            var eliminarTraduccionesAUnaEtiquetaDeUnDiccionario = appModelosRespuesta.EliminarTraduccionesAUnaEtiquetaDeUnDiccionarioRespuesta.CrearNuevaInstancia();

            eliminarTraduccionesAUnaEtiquetaDeUnDiccionario.Relaciones.Clear();
            eliminarTraduccionesAUnaEtiquetaDeUnDiccionario.Relaciones.Add("diccionario", new Guid("00000000-0000-0000-0000-000000000000"));

            eliminarTraduccionesAUnaEtiquetaDeUnDiccionario.ListaDeTraducciones = new List<dominio.Etiquetas.Traduccion>();
            var listaTraducciones = JsonConvert.DeserializeObject<comunes.Traducciones>(EtiquetaJson);
            eliminarTraduccionesAUnaEtiquetaDeUnDiccionario.ListaDeTraducciones = utilitario.MapearEntidades.MapearTraduccionesTipoComunesConTipoAplicacionParaMock(listaTraducciones.Traducciones1);

            this.AppMantenimientoTraducciones.EliminarTraduccionesAUnaEtiquetaDeUnDiccionario(Arg.Any<appModelosPeticion.EliminarTraduccionesAUnaEtiquetaDeUnDiccionarioPeticion>()).ReturnsForAnyArgs<appModelosRespuesta.EliminarTraduccionesAUnaEtiquetaDeUnDiccionarioRespuesta>(eliminarTraduccionesAUnaEtiquetaDeUnDiccionario);

            controlador.Request = new HttpRequestMessage(HttpMethod.Delete, "api/diccionario/9a39ad6d-62c8-42bf-a8f7-66417b2b08d0/etiqueta/8a87f8a7-3df9-4d90-9478-350b964fc888/");
            controlador.Request.Content = new StringContent(EtiquetaJson);

            controlador.Request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var id1 = "9a39ad6d-62c8-42bf-a8f7-66417b2b08d0";
            var id2 = "8a87f8a7-3df9-4d90-9478-350b964fc888";

            //Act
            var respuesta = controlador.EliminarTraduccionesAUnaEtiquetaDeunDiccionario(id1, id2, controlador.Request);

            //Assert
            respuesta.StatusCode.ShouldEqual(HttpStatusCode.NotFound, "Diccionario no encontrado");
        }

        [Test]
        public void DeberiaPoderRetornarHttpStatusNotFoundAlSolicitarEliminarLasTraduccionesAUnaEtiquetaDeUnDiccionarioRetornaEtiquetaNoEncontrada()
        {
            //Arrange
            var controlador = new controladores.TraduccionesController(this.AppMantenimientoTraducciones);
            controlador.Configuration = new HttpConfiguration();

            var eliminarTraduccionesAUnaEtiquetaDeUnDiccionario = appModelosRespuesta.EliminarTraduccionesAUnaEtiquetaDeUnDiccionarioRespuesta.CrearNuevaInstancia();

            eliminarTraduccionesAUnaEtiquetaDeUnDiccionario.Relaciones.Clear();
            eliminarTraduccionesAUnaEtiquetaDeUnDiccionario.Relaciones.Add("diccionario", new Guid("9a39ad6d-62c8-42bf-a8f7-66417b2b08d0"));
            eliminarTraduccionesAUnaEtiquetaDeUnDiccionario.Relaciones.Add("etiqueta", new Guid("00000000-0000-0000-0000-000000000000"));

            eliminarTraduccionesAUnaEtiquetaDeUnDiccionario.ListaDeTraducciones = new List<dominio.Etiquetas.Traduccion>();
            var listaTraducciones = JsonConvert.DeserializeObject<comunes.Traducciones>(EtiquetaJson);
            eliminarTraduccionesAUnaEtiquetaDeUnDiccionario.ListaDeTraducciones = utilitario.MapearEntidades.MapearTraduccionesTipoComunesConTipoAplicacionParaMock(listaTraducciones.Traducciones1);

            this.AppMantenimientoTraducciones.EliminarTraduccionesAUnaEtiquetaDeUnDiccionario(Arg.Any<appModelosPeticion.EliminarTraduccionesAUnaEtiquetaDeUnDiccionarioPeticion>()).ReturnsForAnyArgs<appModelosRespuesta.EliminarTraduccionesAUnaEtiquetaDeUnDiccionarioRespuesta>(eliminarTraduccionesAUnaEtiquetaDeUnDiccionario);

            controlador.Request = new HttpRequestMessage(HttpMethod.Delete, "api/diccionario/9a39ad6d-62c8-42bf-a8f7-66417b2b08d0/etiqueta/8a87f8a7-3df9-4d90-9478-350b964fc888/");
            controlador.Request.Content = new StringContent(EtiquetaJson);

            controlador.Request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var id1 = "9a39ad6d-62c8-42bf-a8f7-66417b2b08d0";
            var id2 = "8a87f8a7-3df9-4d90-9478-350b964fc888";

            //Act
            var respuesta = controlador.EliminarTraduccionesAUnaEtiquetaDeunDiccionario(id1, id2, controlador.Request);

            //Assert
            respuesta.StatusCode.ShouldEqual(HttpStatusCode.NotFound, "Etiqueta No Encontrada");
        }
        #endregion

    }
}
