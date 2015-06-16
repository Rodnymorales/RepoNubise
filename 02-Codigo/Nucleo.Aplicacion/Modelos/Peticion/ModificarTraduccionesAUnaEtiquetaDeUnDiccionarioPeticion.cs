using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.Entidades.Etiquetas;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Comunes;

namespace Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Peticion
{
    public class ModificarTraduccionesAUnaEtiquetaDeUnDiccionarioPeticion : PeticionApp<ModificarTraduccionesAUnaEtiquetaDeUnDiccionarioPeticion>
    {
        #region Propiedades

        /// <summary>
        /// Obtiene o establece el identificador del Diccionario de la Etiqueta a la cual se le modificarán las traducciones
        /// </summary>
		[Required]
        public Guid DiccionarioId { get; set; }

        /// <summary>
        /// /// Obtiene o establece el identificador de la Etiqueta a la cual se le modificarán las traducciones
        /// </summary>
		[Required]
		public Guid EtiquetaId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
		[Required]
		public List<Traduccion> ListaDeTraducciones { get; set; }


        #endregion

        #region Constructores

        /// <summary>
        /// 
        /// </summary>
        private ModificarTraduccionesAUnaEtiquetaDeUnDiccionarioPeticion()
        {
			DiccionarioId = Guid.Empty;
			EtiquetaId = Guid.Empty;
			ListaDeTraducciones = new List<Traduccion>();
		}

        #endregion
    }
}