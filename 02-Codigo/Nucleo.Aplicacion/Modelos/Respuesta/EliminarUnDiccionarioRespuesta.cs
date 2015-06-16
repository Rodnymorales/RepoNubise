using System;
using System.Linq;
using System.Collections.Generic;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Comunes;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.Entidades.Diccionario;

namespace Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Respuesta
{
    public class EliminarUnDiccionarioRespuesta : RespuestaApp<EliminarUnDiccionarioRespuesta>
	{
        #region Propiedades

        /// <summary>
        /// Obtiene o establece el identificador del Diccionario a eliminar
        /// </summary>
        public List<Diccionario> ListaDeDiccionarios { get; set; }

        #endregion

        #region Constructores

        /// <summary>
        /// 
        /// </summary>
	    private EliminarUnDiccionarioRespuesta()
	    {
            ListaDeDiccionarios = new List<Diccionario>();
	    }

        #endregion
	}
}