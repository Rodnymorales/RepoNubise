
using Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Fachada;
using System.Web;
using WebApiModelosRespuesta = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Respuesta;
using WebApiModelosPeticion = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Peticion;

namespace Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Servicios.GeneradorDeRecursos
{
	public class CrearArchivoDeRecursoEnJson : GeneradorRecursos, IRecursos
	{
		private IGeneradorDeRecursosDeTraduccion aplicacionRecursos;

		#region Constructor de la clase
		public CrearArchivoDeRecursoEnJson(IGeneradorDeRecursosDeTraduccion aplicacionRecursos)
		{
			this.aplicacionRecursos = aplicacionRecursos;
		}
		#endregion

		public Dictionary<string, string> GenerarRecursosPorIdioma(WebApiModelosPeticion.GenerarRecursosPorIdiomaPeticion peticionWebApi)
		{
			var generarRecursosApp = this.aplicacionRecursos.GenerarRecursos(peticionWebApi.AppGenerarRecursos);



			var respuesta = new Dictionary<string, string>() { { "sehaojseas", "jsadsdlas" } };

			return respuesta;
		}

	}
}