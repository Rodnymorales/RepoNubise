using System;
using System.Linq;
using System.Collections.Generic;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Comunes;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.Entidades.Diccionario;

namespace Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Respuesta
{
	/// <summary>
	/// Luego de consultar el nombre de la etiqueta enviado en todos los diccionarios,
	/// se debe retornar la lista de diccionarios con todas las coincidencias del nombre de la etiqueta.
	/// </summary>
    public class ConsultarEtiquetasPorNombreRespuesta : RespuestaApp<ConsultarEtiquetasPorNombreRespuesta>
	{
		public List<Diccionario> ListaDeDiccionarios { get; set; }

		#region constructores

		private ConsultarEtiquetasPorNombreRespuesta()
		{
			ListaDeDiccionarios = new List<Diccionario>();
		}

		#endregion
	}
}