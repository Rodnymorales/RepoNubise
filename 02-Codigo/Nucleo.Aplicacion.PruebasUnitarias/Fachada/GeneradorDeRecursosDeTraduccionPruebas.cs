using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Fachada.Implementacion;
using Shouldly;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.Repositorios;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Comunes;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Peticion;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Respuesta;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.Entidades.Diccionario;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.Entidades.Etiquetas;
using NSubstitute;

namespace Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.PruebasUnitarias.Fachada
{
	[TestFixture]
	public class GeneradorDeRecursosDeTraduccionPruebas
	{
		private const string AmbienteTestPrueba = "desarrollo";
        private readonly List<String> listaDeEtiquetasParaBuscar = new List<string> { "app.common.editar", "app.common.eliminar", "app.common.aceptar", "app.common.cancelar" };
        private readonly List<String> listaDeEtiquetasParaBuscarNoExisten = new List<string> { "esta etiqueta no existe", "en el diccionario" };

		private readonly Guid diccionarioBaseId = new Guid("a1fa3369-bc3f-4ebc-9cac-5677cbaa8114");

		private readonly Guid etiquetaAceptarId = new Guid("8a87f8a7-3df9-4d90-9478-350b964fc888");
		private readonly Guid etiquetaCancelarId = new Guid("9a39ad6d-62c8-42bf-a8f7-66417b2b08d0");
		private readonly Guid etiquetaEditarId = new Guid("0260b80b-4ac6-40a6-b5eb-b57916eaab2b");
		private readonly Guid etiquetaEliminarId = new Guid("e2850768-35df-46bb-8f79-48b06ba45528");

		#region Definiciones de diccionarios Mocks

		private Diccionario DiccionarioBaseConEtiquetas()
		{
			var diccionario = Diccionario.CrearNuevoDiccionario(diccionarioBaseId, AmbienteTestPrueba);

			return diccionario.AgregarEtiquetas(EtiquetasDelDiccionario());
		}

		private Diccionario DiccionarioSinTraduciones()
		{
			var diccionario = Diccionario.CrearNuevoDiccionario(diccionarioBaseId, AmbienteTestPrueba);

			return diccionario.AgregarEtiquetas(new List<Etiqueta> { EtiquetaAceptarSinTraducciones(), EtiquetaCancelarSinTraducciones(), EtiquetaEditarSinTraducciones(), EtiquetaEliminarSinTraducciones() });
		}

		#endregion


		#region Definiciones de etiquetas Mocks

		private Etiqueta EtiquetaAceptarSinTraducciones()
		{
			var etiquetaAceptar = Etiqueta.CrearNuevaEtiqueta(etiquetaAceptarId);

			etiquetaAceptar.IdiomaPorDefecto = "es-VE";
			etiquetaAceptar.Nombre = "app.common.aceptar";
			etiquetaAceptar.Descripcion = "Traducciones del botón aceptar.";
			etiquetaAceptar.Activo = true;

			return etiquetaAceptar;
		}

		private Etiqueta EtiquetaCancelarSinTraducciones()
		{
			var etiquetaCancelar = Etiqueta.CrearNuevaEtiqueta(etiquetaCancelarId);

			etiquetaCancelar.IdiomaPorDefecto = "en-US";
			etiquetaCancelar.Nombre = "app.common.cancelar";
			etiquetaCancelar.Descripcion = "Traducciones del botón cancelar.";
			etiquetaCancelar.Activo = true;

			return etiquetaCancelar;
		}

		private Etiqueta EtiquetaEditarSinTraducciones()
		{
			var etiquetaEditar = Etiqueta.CrearNuevaEtiqueta(etiquetaEditarId);

			etiquetaEditar.IdiomaPorDefecto = "en-US";
			etiquetaEditar.Nombre = "app.common.editar";
			etiquetaEditar.Descripcion = "Traducciones del botón editar.";
			etiquetaEditar.Activo = true;

			return etiquetaEditar;
		}

		private Etiqueta EtiquetaEliminarSinTraducciones()
		{
			var etiquetaEliminar = Etiqueta.CrearNuevaEtiqueta(etiquetaEliminarId);

			etiquetaEliminar.IdiomaPorDefecto = "es-VE";
			etiquetaEliminar.Nombre = "app.common.eliminar";
			etiquetaEliminar.Descripcion = "Traducciones del botón eliminar.";
			etiquetaEliminar.Activo = true;

			return etiquetaEliminar;
		}

		private List<Etiqueta> EtiquetasDelDiccionario()
		{
			var etiquetaAcetpar = EtiquetaAceptarSinTraducciones();
			var etiquetaCancelar = EtiquetaCancelarSinTraducciones();
			var etiquetaEditar = EtiquetaEditarSinTraducciones();
			var etiquetaEliminar = EtiquetaEliminarSinTraducciones();

			etiquetaAcetpar.AgregarTraducciones(TraduccionesDeLaEtiquetaAceptar());
			etiquetaCancelar.AgregarTraducciones(TraduccionesDeLaEtiquetaCancelar());
			etiquetaEditar.AgregarTraducciones(TraduccionesDeLaEtiquetaEditar());
			etiquetaEliminar.AgregarTraducciones(TraduccionesDeLaEtiquetaEliminar());


			return new List<Etiqueta> { etiquetaAcetpar, etiquetaCancelar, etiquetaEditar, etiquetaEliminar };
		}

		#endregion

		#region Definiciones de traducciones Mocks

		private List<Traduccion> TraduccionesDeLaEtiquetaAceptar()
		{
			var listaDeTraduccionesAceptar = new List<Traduccion>();

			var traduccionAceptarEs = Traduccion.CrearNuevaTraduccion(Cultura.CrearNuevaCultura("es"), "aceptar");
			var traduccionAceptarEsVe = Traduccion.CrearNuevaTraduccion(Cultura.CrearNuevaCultura("es-VE"), "aceptar");
			var traduccionAceptarEn = Traduccion.CrearNuevaTraduccion(Cultura.CrearNuevaCultura("en"), "accept");
			var traduccionAceptarEnUs = Traduccion.CrearNuevaTraduccion(Cultura.CrearNuevaCultura("en-US"), "accept");

			traduccionAceptarEs.ToolTip = "Presione el botón aceptar";
			traduccionAceptarEsVe.ToolTip = "Presione el botón aceptar";
			traduccionAceptarEn.ToolTip = "Click the accept button";
			traduccionAceptarEnUs.ToolTip = "Click the accept button";

			listaDeTraduccionesAceptar.Add(traduccionAceptarEs);
			listaDeTraduccionesAceptar.Add(traduccionAceptarEsVe);
			listaDeTraduccionesAceptar.Add(traduccionAceptarEn);
			listaDeTraduccionesAceptar.Add(traduccionAceptarEnUs);

			return listaDeTraduccionesAceptar;
		}

		private List<Traduccion> TraduccionesDeLaEtiquetaCancelar()
		{
			var listaDeTraduccionesCancelar = new List<Traduccion>();

			var traduccionCancelarEs = Traduccion.CrearNuevaTraduccion(Cultura.CrearNuevaCultura("es"), "cancelar");
			var traduccionCancelarEsVe = Traduccion.CrearNuevaTraduccion(Cultura.CrearNuevaCultura("es-VE"), "cancelar");
			var traduccionCancelarEn = Traduccion.CrearNuevaTraduccion(Cultura.CrearNuevaCultura("en"), "cancel");
			var traduccionCancelarEnUs = Traduccion.CrearNuevaTraduccion(Cultura.CrearNuevaCultura("en-US"), "cancel");

			traduccionCancelarEs.ToolTip = "Presione el botón cancelar";
			traduccionCancelarEsVe.ToolTip = "Presione el botón aceptar";
			traduccionCancelarEn.ToolTip = "Click the cancel button";
			traduccionCancelarEnUs.ToolTip = "Click the cancel button";

			listaDeTraduccionesCancelar.Add(traduccionCancelarEs);
			listaDeTraduccionesCancelar.Add(traduccionCancelarEsVe);
			listaDeTraduccionesCancelar.Add(traduccionCancelarEn);
			listaDeTraduccionesCancelar.Add(traduccionCancelarEnUs);

			return listaDeTraduccionesCancelar;
		}

		private List<Traduccion> TraduccionesDeLaEtiquetaEditar()
		{
			var listaDeTraduccionesEditar = new List<Traduccion>();

			var traduccionEditarEs = Traduccion.CrearNuevaTraduccion(Cultura.CrearNuevaCultura("es"), "Editar");
			var traduccionEditarEsVe = Traduccion.CrearNuevaTraduccion(Cultura.CrearNuevaCultura("es-VE"), "Editar");
			var traduccionEditarEn = Traduccion.CrearNuevaTraduccion(Cultura.CrearNuevaCultura("en"), "Edit");
			var traduccionEditarEnUs = Traduccion.CrearNuevaTraduccion(Cultura.CrearNuevaCultura("en-US"), "Edit");

			traduccionEditarEs.ToolTip = "Presione el botón editar";
			traduccionEditarEsVe.ToolTip = "Presione el botón editar";
			traduccionEditarEn.ToolTip = "Click the edit button";
			traduccionEditarEnUs.ToolTip = "Click the edir button";

			listaDeTraduccionesEditar.Add(traduccionEditarEs);
			listaDeTraduccionesEditar.Add(traduccionEditarEsVe);
			listaDeTraduccionesEditar.Add(traduccionEditarEn);
			listaDeTraduccionesEditar.Add(traduccionEditarEnUs);

			return listaDeTraduccionesEditar;
		}

		private List<Traduccion> TraduccionesDeLaEtiquetaEliminar()
		{
			var listaDeTraduccionesEliminar = new List<Traduccion>();

			var traduccionEliminarEs = Traduccion.CrearNuevaTraduccion(Cultura.CrearNuevaCultura("es"), "Eliminar");
			var traduccionEliminarEsVe = Traduccion.CrearNuevaTraduccion(Cultura.CrearNuevaCultura("es-VE"), "Eliminar");
			var traduccionEliminarEn = Traduccion.CrearNuevaTraduccion(Cultura.CrearNuevaCultura("en"), "Delete");
			var traduccionEliminarEnUs = Traduccion.CrearNuevaTraduccion(Cultura.CrearNuevaCultura("en-US"), "Delete");

			traduccionEliminarEs.ToolTip = "Presione el botón eliminar";
			traduccionEliminarEsVe.ToolTip = "Presione el botón eliminar";
			traduccionEliminarEn.ToolTip = "Click the delete button";
			traduccionEliminarEnUs.ToolTip = "Click the delete button";

			listaDeTraduccionesEliminar.Add(traduccionEliminarEs);
			listaDeTraduccionesEliminar.Add(traduccionEliminarEsVe);
			listaDeTraduccionesEliminar.Add(traduccionEliminarEn);
			listaDeTraduccionesEliminar.Add(traduccionEliminarEnUs);

			return listaDeTraduccionesEliminar;
		}

		#endregion

		#region

		Dictionary<string, List<TraduccionRecurso>> DicionarioDeEtiquetasPorIdioma()
		{
			var recursosPorIdioma = new Dictionary<string, List<TraduccionRecurso>>();

			var diccionario = DiccionarioBaseConEtiquetas();

			var listaEtiquetas = new List<Cultura>();
		
			var listaDeCulturasDisponibles = (from etiq in diccionario.Etiquetas.ToList()
											  from tra in etiq.Textos.ToList()
											  select tra.Cultura).Distinct();

			foreach (Cultura item in listaDeCulturasDisponibles)
			{
				var listaDeTraduccionesPorUnaCultura = (from etiq in diccionario.Etiquetas.ToList()
														  from tra in etiq.Textos.ToList()
														  where tra.Cultura.CodigoIso == item.CodigoIso
														  select TraduccionRecurso.CrearNuevaInstancia(etiq.Nombre, tra.Texto, tra.ToolTip)
															  ).ToList();

				recursosPorIdioma.Add(item.CodigoIso, listaDeTraduccionesPorUnaCultura);
			}

			return recursosPorIdioma;
		}

		#endregion


		#region Definiciones de las respuestas esperadas

		private GenerarRecursosPorIdiomaRespuesta RespuestaEsperadaGenerarRecursosTodasLasEtiquetas()
		{
			var respuesta = GenerarRecursosPorIdiomaRespuesta.CrearNuevaInstancia();

			respuesta.RecursosPorIdioma = DicionarioDeEtiquetasPorIdioma();
			respuesta.Relaciones["diccionario"] = diccionarioBaseId;

			return respuesta;
		}

		#endregion

		//[Test]
		//[Category("Generar recursos por idioma")]
		//public void DeberiaPoderObtenerUnErrorDeTipoNullReferenceExceptionCuandoNoSeEncontroElDiccionarioSolicitadoParaConsultar()
		//{
		//	//Arrange
		//	var diccionarioRepositorio = Substitute.For<IDiccionarioRepositorio>();
		//	var diccionarioInicialDeLasPruebas = DiccionarioBaseConEtiquetas();

		//	//TODO: cambiar por ambiente luego de que se haga el método.
		//	diccionarioRepositorio.ObtenerUnDiccionario(diccionarioBaseId).Returns(diccionarioInicialDeLasPruebas);

		//	var peticion = GenerarRecursosPorIdiomaPeticion.CrearNuevaInstancia();
		//	peticion.Ambiente = "Cualquier otro diccionario.";
		//	peticion.ListaDeEtiquetas = listaDeEtiquetasParaBuscar;

		//	var serviciosApi = new GeneradorDeRecursosDeTraduccion(diccionarioRepositorio);

		//	//Act y Assert
		//	Should.Throw<NullReferenceException>(
		//		() => serviciosApi.GenerarRecursos(peticion)
		//	);
		//}

		[Test]
		[Category("Generar recursos por idioma")]
		public void DeberiaPoderObtenerUnErrorDeTipoExceptionCuandoSeEnviaUnAmbienteVacioParaConsultar()
		{
			//Arrange
			var diccionarioRepositorio = Substitute.For<IDiccionarioRepositorio>();
			var diccionarioInicialDeLasPruebas = DiccionarioBaseConEtiquetas();

			//TODO: cambiar por ambiente luego de que se haga el método.
			diccionarioRepositorio.ObtenerUnDiccionario(diccionarioBaseId).Returns(diccionarioInicialDeLasPruebas);

			var peticion = GenerarRecursosPorIdiomaPeticion.CrearNuevaInstancia();
			peticion.Ambiente = string.Empty;
			peticion.ListaDeEtiquetas = listaDeEtiquetasParaBuscar;

			var serviciosApi = new GeneradorDeRecursosDeTraduccion(diccionarioRepositorio);

			//Act y Assert
			Should.Throw<Exception>(
				() => serviciosApi.GenerarRecursos(peticion)
			);
		}

		[Test]
		[Category("Generar recursos por idioma")]
		public void DeberiaPoderObtenerUnErrorDeTipoExceptionCuandoSeEnviaUnaListaVaciaNombresDeEtiquetasParaConsultar()
		{
			//Arrange
			var diccionarioRepositorio = Substitute.For<IDiccionarioRepositorio>();
			var diccionarioInicialDeLasPruebas = DiccionarioBaseConEtiquetas();

			//TODO: cambiar por ambiente luego de que se haga el método.
			diccionarioRepositorio.ObtenerUnDiccionario(diccionarioBaseId).Returns(diccionarioInicialDeLasPruebas);

			var peticion = GenerarRecursosPorIdiomaPeticion.CrearNuevaInstancia();
			peticion.Ambiente = AmbienteTestPrueba;
			peticion.ListaDeEtiquetas = new List<String>();

			var serviciosApi = new GeneradorDeRecursosDeTraduccion(diccionarioRepositorio);

			//Act y Assert
			Should.Throw<Exception>(
				() => serviciosApi.GenerarRecursos(peticion)
			);
		}

		[Test]
		[Category("Generar recursos por idioma")]
		public void DeberiaPoderObtenerUnErrorDeTipoExceptionCuandoNoSeEncontroNingunaEtiquetaSolicitadaEnElDiccionario()
		{
			//Arrange
			var diccionarioRepositorio = Substitute.For<IDiccionarioRepositorio>();
			var diccionarioInicialDeLasPruebas = DiccionarioBaseConEtiquetas();

			//TODO: cambiar por ambiente luego de que se haga el método.
			diccionarioRepositorio.ObtenerUnDiccionario(diccionarioBaseId).Returns(diccionarioInicialDeLasPruebas);

			var peticion = GenerarRecursosPorIdiomaPeticion.CrearNuevaInstancia();
			peticion.Ambiente = AmbienteTestPrueba;
			peticion.ListaDeEtiquetas = listaDeEtiquetasParaBuscarNoExisten;

			var serviciosApi = new GeneradorDeRecursosDeTraduccion(diccionarioRepositorio);

			//Act y Assert
			Should.Throw<Exception>(
				() => serviciosApi.GenerarRecursos(peticion)
			);
		}

		[Test]
		[Category("Generar recursos por idioma")]
		public void DeberiaPoderObtenerUnErrorDeTipoExceptionCuandoNoSeEncontroNingunaCulturaParaConsultar()
		{
			//Arrange
			var diccionarioRepositorio = Substitute.For<IDiccionarioRepositorio>();
			var diccionarioInicialDeLasPruebas = DiccionarioSinTraduciones();

			//TODO: cambiar por ambiente luego de que se haga el método.
			diccionarioRepositorio.ObtenerUnDiccionario(diccionarioBaseId).Returns(diccionarioInicialDeLasPruebas);

			var peticion = GenerarRecursosPorIdiomaPeticion.CrearNuevaInstancia();
			peticion.Ambiente = AmbienteTestPrueba;
			peticion.ListaDeEtiquetas = listaDeEtiquetasParaBuscarNoExisten;

			var serviciosApi = new GeneradorDeRecursosDeTraduccion(diccionarioRepositorio);

			//Act y Assert
			Should.Throw<Exception>(
				() => serviciosApi.GenerarRecursos(peticion)
			);
		}

		[Test]
		[Category("Generar recursos por idioma")]
		public void DeberiaPoderObtenerLaListaDeEtiquetasPorIdiomaDeUnDiccionarioExistente()
		{
			//Arrange
			var diccionarioRepositorio = Substitute.For<IDiccionarioRepositorio>();
			var diccionarioInicialDeLasPruebas = DiccionarioBaseConEtiquetas();

			//TODO: cambiar por ambiente luego de que se haga el método.
			diccionarioRepositorio.ObtenerUnDiccionario(diccionarioBaseId).Returns(diccionarioInicialDeLasPruebas);

			var peticion = GenerarRecursosPorIdiomaPeticion.CrearNuevaInstancia();
			peticion.Ambiente = AmbienteTestPrueba;
			peticion.ListaDeEtiquetas = listaDeEtiquetasParaBuscar;

			var serviciosApi = new GeneradorDeRecursosDeTraduccion(diccionarioRepositorio);

			var respuestaEsperada = RespuestaEsperadaGenerarRecursosTodasLasEtiquetas();

			//Act
			var respuestaReal = serviciosApi.GenerarRecursos(peticion);

			//Assert
			//respuestaEsperada.ShouldSatisfyAllConditions(
			//	() => respuestaEsperada.RecursosPorIdioma.ShouldBe(respuestaReal.RecursosPorIdioma),
			//	() => respuestaEsperada.Relaciones.ShouldBe(respuestaReal.Relaciones)
			//);
			CollectionAssert.AreEquivalent(respuestaEsperada.RecursosPorIdioma, respuestaReal.RecursosPorIdioma);
		}
	}
}
