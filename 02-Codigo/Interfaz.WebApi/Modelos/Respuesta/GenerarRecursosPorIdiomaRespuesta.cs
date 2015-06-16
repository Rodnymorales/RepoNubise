using System;
using System.Linq;
using System.Collections.Generic;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.Entidades.Etiquetas;
using comunesApp = Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Comunes;
using appModelosRespuesta = Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Respuesta;

namespace Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Respuesta
{
	public class GenerarRecursosPorIdiomaRespuesta
	{
        public Dictionary<string, List<comunesApp.TraduccionRecurso>> RecursosPorIdioma { get; set; }

		#region constructores

        private GenerarRecursosPorIdiomaRespuesta(appModelosRespuesta.GenerarRecursosPorIdiomaRespuesta respuestaApp)
		{
			RecursosPorIdioma = new Dictionary<string, List<comunesApp.TraduccionRecurso>>();
            RecursosPorIdioma = respuestaApp.RecursosPorIdioma;
		}

		#endregion

        public static GenerarRecursosPorIdiomaRespuesta CrearNuevaRespuesta(appModelosRespuesta.GenerarRecursosPorIdiomaRespuesta respuestaApp)
        {
            return new GenerarRecursosPorIdiomaRespuesta(respuestaApp);
        }
	}
}
