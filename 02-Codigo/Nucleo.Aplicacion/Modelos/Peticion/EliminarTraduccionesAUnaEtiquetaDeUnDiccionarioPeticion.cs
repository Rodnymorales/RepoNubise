using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Comunes;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.Entidades.Etiquetas;

namespace Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Peticion
{
    public class EliminarTraduccionesAUnaEtiquetaDeUnDiccionarioPeticion : PeticionApp<EliminarTraduccionesAUnaEtiquetaDeUnDiccionarioPeticion>
	{
		[Required]
		public List<Traduccion> ListaDeTraducciones { get; set; }

		[Required]
		public Guid EtiquetaId { get; set; }

		[Required]
		public Guid DiccionarioId { get; set; }

		#region constructores

		private EliminarTraduccionesAUnaEtiquetaDeUnDiccionarioPeticion()
		{
			ListaDeTraducciones = new List<Traduccion>();
			EtiquetaId = Guid.Empty;
			DiccionarioId = Guid.Empty;
		}

		#endregion
	}
}