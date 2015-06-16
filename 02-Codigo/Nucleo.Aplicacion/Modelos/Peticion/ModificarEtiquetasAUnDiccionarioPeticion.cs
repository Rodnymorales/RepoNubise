using System;
using System.Collections.Generic;
using System.Linq;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.Entidades.Etiquetas;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Comunes;

namespace Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Peticion
{
    public class ModificarEtiquetasAUnDiccionarioPeticion : PeticionApp<ModificarEtiquetasAUnDiccionarioPeticion>
    {

        #region Propiedades

        /// <summary>
        /// Obtiene o establece el identificador del Diccionario al cual se le moficarán las Etiquetas
        /// </summary>
        public Guid DiccionarioId { get; set; }

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
        private ModificarEtiquetasAUnDiccionarioPeticion()
	    {
			ListaDeEtiquetas = new List<Etiqueta>();
			DiccionarioId = Guid.Empty;
	    }

        #endregion

    }
}
