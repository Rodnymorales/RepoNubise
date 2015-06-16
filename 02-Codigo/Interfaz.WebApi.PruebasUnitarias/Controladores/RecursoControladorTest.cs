using System;
using System.Linq;
using NUnit.Framework;
using app = Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Fachada;
using appModelosRespuesta = Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Respuesta;
using appModelosPeticion = Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Peticion;
using Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Controladores;
using System.Collections.Generic;
using System.Net.Http;
using Should;
using System.Net;
using comunes = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Comunes;
using NSubstitute;
using System.Web.Http;
using webApi = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Servicios;

namespace Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.PruebasUnitarias
{
    [TestFixture]
    class RecursoControladorTest
    {
        #region Variables y propiedades globales

        public const string Ambiente = "desarrollo";
        public const string ListaNombresEtiquetas = "{ \"etiquetas\": [ \"app.common.aceptar\", \"app.common.cancelar\", \"app.common.usuario\" ] }";

        public app.IGeneradorDeRecursosDeTraduccion AppGenerarRecursos { get; set; }
        //public webApi.

        #endregion

        #region Contructor de la clase
        public RecursoControladorTest()
        {
            this.AppGenerarRecursos = Substitute.For<app.IGeneradorDeRecursosDeTraduccion>();
        }
        #endregion

        #region Metodos de pruebas (GET)
        [Test]
        public void DeberiaPoderRetornarHttpStatusCodeOkCuandoSeSolicitaGenerarUnosArchivosDeRecursosPorIdiomaRetornaDireccionConlosArchivosDeRecursos()
        {
            //Arrange
            var generarArchivoRecurso = appModelosRespuesta.GenerarRecursosPorIdiomaRespuesta.CrearNuevaInstancia();
            generarArchivoRecurso.Relaciones = new Dictionary<string, Guid>();
            var controlador = new Controladores.RecursosController(this.AppGenerarRecursos);

            controlador.Configuration = new HttpConfiguration();
            controlador.Request = new HttpRequestMessage(HttpMethod.Get, "api/recursos/diccionario/desarrollo");
            controlador.Request.Content = new StringContent(ListaNombresEtiquetas);

            this.AppGenerarRecursos.GenerarRecursos(Arg.Any<appModelosPeticion.GenerarRecursosPorIdiomaPeticion>()).ReturnsForAnyArgs<appModelosRespuesta.GenerarRecursosPorIdiomaRespuesta>(generarArchivoRecurso);

            //Act
            var respuesta = controlador.SolicitarGenerarArchivoDeRecursosPorAmbienteDeDiccionario(Ambiente, controlador.Request);

            //Assert
            respuesta.StatusCode.ShouldEqual(HttpStatusCode.OK);
        }

        public void DeberiaPoderRetornarHttpStatusCodeBadRequestCuandoSeSolicitaGenerarUnosArchivosDeRecursosPorIdiomaRetornaParametroformatoNoSoportado()
        {
            //Arrange
            
        }

        #endregion
    }
}
