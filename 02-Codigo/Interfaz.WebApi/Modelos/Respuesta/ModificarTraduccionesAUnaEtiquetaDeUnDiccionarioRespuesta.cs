using System;
using utilitario = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Utilitarios;
using System.Linq;
using appModelosRespuesta = Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Respuesta;
using comunes = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Comunes;

namespace Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Respuesta
{
	public class ModificarTraduccionesAUnaEtiquetaDeUnDiccionarioRespuesta
	{
        #region Propiedades

	    public comunes.Traducciones Traducciones { get; set; }

        #endregion

        #region Constructores


        private ModificarTraduccionesAUnaEtiquetaDeUnDiccionarioRespuesta(appModelosRespuesta.ModificarTraduccionesAUnaEtiquetaDeUnDiccionarioRespuesta respuestaApp)
        {
            Traducciones = new comunes.Traducciones();
            Traducciones.Traducciones1 = utilitario.MapeoDominioAWebApiComunes.MapearTraducciones(respuestaApp.ListaDeTraducciones);
	    }

        public static ModificarTraduccionesAUnaEtiquetaDeUnDiccionarioRespuesta CrearNuevaRespuesta(appModelosRespuesta.ModificarTraduccionesAUnaEtiquetaDeUnDiccionarioRespuesta respuestaApp)
	    {
            return new ModificarTraduccionesAUnaEtiquetaDeUnDiccionarioRespuesta(respuestaApp);
	    }
        #endregion

     
	}
}