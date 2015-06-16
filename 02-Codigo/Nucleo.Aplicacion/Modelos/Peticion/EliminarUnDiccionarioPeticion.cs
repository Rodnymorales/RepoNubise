using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Comunes;

namespace Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Peticion
{
    public class EliminarUnDiccionarioPeticion : PeticionApp<EliminarUnDiccionarioPeticion>
    {
        #region Propiedades

        /// <summary>
        /// Obtiene o establece el identificador del Diccionario a eliminar
        /// </summary>
        [Required]
		public Guid DiccionarioId { get; set; }

        #endregion

        #region Constructores

        /// <summary>
        /// 
        /// </summary>
	    private EliminarUnDiccionarioPeticion()
	    {
			DiccionarioId = Guid.Empty;
	    }

        #endregion

    }
}