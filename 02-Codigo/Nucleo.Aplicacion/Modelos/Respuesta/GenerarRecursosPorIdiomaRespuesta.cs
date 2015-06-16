using System;
using System.Linq;
using System.Collections.Generic;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.Entidades.Etiquetas;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Comunes;

namespace Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Respuesta
{
	public class GenerarRecursosPorIdiomaRespuesta : RespuestaApp<GenerarRecursosPorIdiomaRespuesta>
	{
		public Dictionary<string, List<TraduccionRecurso>> RecursosPorIdioma { get; set; }

		#region constructores

		private GenerarRecursosPorIdiomaRespuesta()
		{
			RecursosPorIdioma = new Dictionary<string, List<TraduccionRecurso>>();
			Relaciones = new Dictionary<string, Guid> { { "diccionario", Guid.Empty } };
		}

		#endregion
	}
}
