using System;
using System.Linq;
using System.Collections.Generic;
using utilitario = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Utilitarios;
using comunes = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Comunes;
using appModelosRespuesta = Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Respuesta;

namespace Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Respuesta
{
	public class EliminarEtiquetasAUnDiccionarioRespuesta
	{
		public comunes.Etiquetas Etiquetas { get; set; }

		public Dictionary<string, Guid> Relaciones { get; set; }

		#region constructores

        private EliminarEtiquetasAUnDiccionarioRespuesta(appModelosRespuesta.EliminarEtiquetasAUnDiccionarioRespuesta respuestaApp)
		{
            this.Etiquetas = new comunes.Etiquetas();
            this.Etiquetas.ListaEtiquetas = utilitario.MapeoDominioAWebApiComunes.MapearEtiquetas(respuestaApp.ListaDeEtiquetas);
		}

		public static EliminarEtiquetasAUnDiccionarioRespuesta CrearNuevaRespuesta(appModelosRespuesta.EliminarEtiquetasAUnDiccionarioRespuesta respuestaApp)
		{
			return new EliminarEtiquetasAUnDiccionarioRespuesta(respuestaApp);
		}

		#endregion

	}
}
