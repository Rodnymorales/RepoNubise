using System;
using System.Linq;
using System.Collections.Generic;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.Entidades.Diccionario;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Comunes;

namespace Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Respuesta
{
    public class ModificarUnDiccionarioRespuesta : RespuestaApp<ModificarUnDiccionarioRespuesta>
    {
        #region Propiedades

        /// <summary>
        /// 
        /// </summary>
        public Diccionario Diccionario { get; set; }

        #endregion
       
        #region Constructores

	    /// <summary>
	    /// 
	    /// </summary>
        private ModificarUnDiccionarioRespuesta()
	    {
			Relaciones = new Dictionary<string, Guid> { { "diccionario", Guid.Empty } };
	    }

        #endregion
	}
}
