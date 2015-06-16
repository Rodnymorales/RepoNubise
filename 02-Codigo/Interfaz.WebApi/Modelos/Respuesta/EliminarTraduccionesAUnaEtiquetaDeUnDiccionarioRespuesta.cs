using System;
using System.Linq;
using System.Collections.Generic;
using comunes = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Comunes;
using appModelosRespuesta = Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Respuesta;
using utilitario = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Utilitarios;

namespace Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Respuesta
{
	public class EliminarTraduccionesAUnaEtiquetaDeUnDiccionarioRespuesta
	{
        public comunes.Traducciones Traducciones { get; set; }

		public Dictionary<string, Guid> Relaciones { get; set; }

		#region constructores

        private EliminarTraduccionesAUnaEtiquetaDeUnDiccionarioRespuesta(appModelosRespuesta.EliminarTraduccionesAUnaEtiquetaDeUnDiccionarioRespuesta respuestaApp)
		{
            Traducciones = new comunes.Traducciones();
            Traducciones.Traducciones1 = utilitario.MapeoDominioAWebApiComunes.MapearTraducciones(respuestaApp.ListaDeTraducciones);
		}

        public static EliminarTraduccionesAUnaEtiquetaDeUnDiccionarioRespuesta CrearNuevaRespuesta(appModelosRespuesta.EliminarTraduccionesAUnaEtiquetaDeUnDiccionarioRespuesta respuestaApp)
		{
			return new EliminarTraduccionesAUnaEtiquetaDeUnDiccionarioRespuesta(respuestaApp);
		}

		#endregion
	}
}