using System;
using System.Collections.Generic;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.Entidades.Etiquetas;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Comunes;

namespace Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Respuesta
{
    public class ModificarEtiquetasAUnDiccionarioRespuesta : RespuestaApp<ModificarEtiquetasAUnDiccionarioRespuesta>
	{
        #region Propiedades

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Etiqueta> ListaDeEtiquetas { get; set; }

        #endregion

        #region Constructores

	    /// <summary>
	    /// 
	    /// </summary>
        private ModificarEtiquetasAUnDiccionarioRespuesta()
	    {
			Relaciones = new Dictionary<string, Guid> { { "diccionario", Guid.Empty } };
			ListaDeEtiquetas = new List<Etiqueta>();
	    }

        #endregion
	}
}