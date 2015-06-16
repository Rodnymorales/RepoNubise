using System;
using System.Linq;
using System.Collections.Generic;
using utilitario = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Utilitarios;
using appModelosRespuesta = Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Respuesta;
using comunes = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Comunes;

namespace Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Respuesta
{
	public class AgregarTraduccionesAUnaEtiquetaDeUnDiccionarioRespuesta 
	{
		public comunes.Traducciones Traducciones { get; set; }

		public Dictionary<string, Guid> Relaciones { get; set; }

		#region constructores

		private AgregarTraduccionesAUnaEtiquetaDeUnDiccionarioRespuesta(appModelosRespuesta.AgregarTraduccionesAUnaEtiquetaDeUnDiccionarioRespuesta respuestaApp)
		{
			this.Traducciones = new comunes.Traducciones();
            this.Traducciones.Traducciones1 = utilitario.MapeoDominioAWebApiComunes.MapearTraducciones(respuestaApp.ListaDeTraducciones);
		}
		#endregion
        public static AgregarTraduccionesAUnaEtiquetaDeUnDiccionarioRespuesta CrearNuevaRespuesta(appModelosRespuesta.AgregarTraduccionesAUnaEtiquetaDeUnDiccionarioRespuesta respuestaApp)
		{
            return new AgregarTraduccionesAUnaEtiquetaDeUnDiccionarioRespuesta(respuestaApp);
		}

	}
}