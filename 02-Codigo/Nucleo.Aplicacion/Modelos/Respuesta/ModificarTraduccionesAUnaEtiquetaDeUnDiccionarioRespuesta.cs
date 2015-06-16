using System;
using System.Collections.Generic;
using System.Linq;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Comunes;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.Entidades.Etiquetas;

namespace Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Respuesta
{
    public class ModificarTraduccionesAUnaEtiquetaDeUnDiccionarioRespuesta : RespuestaApp<ModificarTraduccionesAUnaEtiquetaDeUnDiccionarioRespuesta>
	{
        #region Propiedades

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
	    public List<Traduccion> ListaDeTraducciones { get; set; }

        #endregion

        #region Constructores

	    /// <summary>
	    /// 
	    /// </summary>
	    private ModificarTraduccionesAUnaEtiquetaDeUnDiccionarioRespuesta()
	    {
	        Relaciones = new Dictionary<string, Guid> {{"diccionario", Guid.Empty}, {"etiqueta", Guid.Empty}};
			ListaDeTraducciones = new List<Traduccion>();
	    }


        #endregion
	}
}