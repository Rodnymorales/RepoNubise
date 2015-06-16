using Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Comunes;
using Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Peticion;
using Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Servicios;
using NSubstitute;
using NUnit.Framework;
using System.Net.Http;
using Newtonsoft.Json;
using Should;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Fachada;
using peticionApp = Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Peticion;
using respuestaApp = Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Respuesta;
using System.Collections.Generic;
using comunes = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Comunes;
using utilitario = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Utilitarios;
using dominioEtiqueta = Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.Entidades.Etiquetas;
using dominio = Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.Entidades.Diccionario;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Comunes;
using System.Linq;
using System;
using Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Servicios.GeneradorDeRecursos;

namespace Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.PruebasUnitarias.Servicios
{
	[TestFixture]
	public class ServicioGenerarRecursoJsonTest
	{
		private EtiquetasRecursos listaDeEtiquetasMock;
		private readonly Guid diccionarioBaseId = new Guid("a1fa3369-bc3f-4ebc-9cac-5677cbaa8114");
		private const string ambienteTestPrueba = "desarrollo";
		private const string listaDeEtiquetasParaBuscar = "{ \"etiquetas\": [ \"app.common.aceptar\", \"app.common.cancelar\", \"app.common.usuario\" ] }";

		private const string EtiquetaJson = "{ \"etiquetas\": [ { \"id\": \"094bf626-d137-47ea-81fa-19a0aaceedf5\", \"activo\": true, \"idiomapordefecto\": \"es-VE\", \"nombre\": \"app.common.aceptar\", \"descripcion\": \"Aceptar\", \"textos\": [ { \"cultura\": \"es\", \"texto\": \"aceptar\", \"tooltip\": \"el tooltip aceptar\" }, { \"cultura\": \"es-VE\", \"texto\": \"aceptar\", \"tooltip\": \"el tooltip aceptar\" }, { \"cultura\": \"en\", \"texto\": \"accept\", \"tooltip\": \"the accept tooltip\" }, { \"cultura\": \"en-US\", \"texto\": \"accept\", \"tooltip\": \"the accept tooltip\" } ] }, { \"id\": \"98d723fd-b301-41e2-90a2-90c66a6835b8\", \"activo\": true, \"idiomapordefecto\": \"es-VE\", \"nombre\": \"app.common.cancelar\", \"descripcion\": \"cancelar\", \"textos\": [ { \"cultura\": \"es\", \"texto\": \"cancelar\", \"tooltip\": \"el tooltip cancelar\" }, { \"cultura\": \"es-VE\", \"texto\": \"cancelar\", \"tooltip\": \"el tooltip cancelar\" }, { \"cultura\": \"en\", \"texto\": \"cancel\", \"tooltip\": \"the cancel tooltip\" }, { \"cultura\": \"en-US\", \"texto\": \"cancel\", \"tooltip\": \"the cancel tooltip\" } ] }, { \"id\": \"c4d2f76e-cc6a-4481-853c-47f1cd7eafdc\", \"activo\": true, \"idiomapordefecto\": \"en\", \"nombre\": \"app.common.usuario\", \"descripcion\": \"Campo de texto usuario\", \"textos\": [ { \"cultura\": \"es\", \"texto\": \"usuario\", \"tooltip\": \"el nombre del usuario\" }, { \"cultura\": \"es-VE\", \"texto\": \"usuario\", \"tooltip\": \"el nombre del usuario\" }, { \"cultura\": \"en\", \"texto\": \"user\", \"tooltip\": \"the user name\" }, { \"cultura\": \"en-US\", \"texto\": \"user\", \"tooltip\": \"the user name\" } ] }, { \"id\": \"07eca348-ae16-43e4-a16f-0f8039ab1e35\", \"activo\": true, \"idiomapordefecto\": \"en\", \"nombre\": \"app.common.contraseña\", \"descripcion\": \"Campo de texto contraseña\", \"textos\": [ { \"cultura\": \"es\", \"texto\": \"contraseña\" }, { \"cultura\": \"es-VE\", \"texto\": \"contraseña\" }, { \"cultura\": \"en\", \"texto\": \"password\" }, { \"cultura\": \"en-US\", \"texto\": \"password\" } ] } ] }";


		Dictionary<string, List<TraduccionRecurso>> DicionarioDeEtiquetasPorIdioma()
		{
			var recursosPorIdioma = new Dictionary<string, List<TraduccionRecurso>>();

			var etiquetas = JsonConvert.DeserializeObject<comunes.Etiquetas>(EtiquetaJson);
			List<dominioEtiqueta.Etiqueta> listaDeEtiquetas = utilitario.MapeoWebApiComunesADominio.MapearEtiquetas(etiquetas.ListaEtiquetas);

			var diccionario = dominio.Diccionario.CrearNuevoDiccionario(diccionarioBaseId, ambienteTestPrueba);

			diccionario.AgregarEtiquetas(listaDeEtiquetas);

			var listaDeCulturasDisponibles = (from etiq in diccionario.Etiquetas.ToList()
											  from tra in etiq.Textos.ToList()
											  select tra.Cultura).Distinct();

			foreach (dominioEtiqueta.Cultura item in listaDeCulturasDisponibles)
			{
				var prueba = (from etiq in diccionario.Etiquetas.ToList()
							  from tra in etiq.Textos.ToList()
							  where tra.Cultura.CodigoIso == item.CodigoIso
							  select TraduccionRecurso.CrearNuevaInstancia(etiq.Nombre, tra.Texto, tra.ToolTip)
								  ).ToList();

				recursosPorIdioma.Add(item.CodigoIso, prueba);
			}

			return recursosPorIdioma;
		}

		[Test]
		public void DeberiaPoderGenerarArchivosDeRecursoJsonPorIdioma()
		{
			//Arrange
			var aplicacionRecursos = Substitute.For<IGeneradorDeRecursosDeTraduccion>();

			var respuesta = respuestaApp.GenerarRecursosPorIdiomaRespuesta.CrearNuevaInstancia();

			respuesta.RecursosPorIdioma = DicionarioDeEtiquetasPorIdioma();
			respuesta.Relaciones["diccionario"] = diccionarioBaseId;

			var requestHttp = new HttpRequestMessage(HttpMethod.Get, "api/recursos/ambiente/desarrollo");
			requestHttp.Content = new StringContent(listaDeEtiquetasParaBuscar);
			var generarRecursoJson = new CrearArchivoDeRecursoEnJson(aplicacionRecursos);

			aplicacionRecursos.GenerarRecursos(Arg.Any<peticionApp.GenerarRecursosPorIdiomaPeticion>()).ReturnsForAnyArgs<respuestaApp.GenerarRecursosPorIdiomaRespuesta>(respuesta);

			//Act
			var listaDeArchivosDeRecurso = generarRecursoJson.GenerarRecursosPorIdioma(GenerarRecursosPorIdiomaPeticion.CrearNuevaPeticion("desarrollo", requestHttp));
			//Assert
			listaDeArchivosDeRecurso.ShouldNotBeEmpty();
		}
	}
}
