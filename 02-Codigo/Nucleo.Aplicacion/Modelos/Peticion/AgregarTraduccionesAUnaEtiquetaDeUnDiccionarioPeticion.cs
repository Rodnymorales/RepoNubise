using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.Entidades.Etiquetas;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Comunes;

namespace Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Peticion
{
    public class AgregarTraduccionesAUnaEtiquetaDeUnDiccionarioPeticion : PeticionApp<AgregarTraduccionesAUnaEtiquetaDeUnDiccionarioPeticion>
	{
		[Required]
		public List<Traduccion> ListaDeTraducciones { get; set; }

		[Required]
		public Guid EtiquetaId { get; set; }

		[Required]
		public Guid DiccionarioId { get; set; }

		#region constructores

		private AgregarTraduccionesAUnaEtiquetaDeUnDiccionarioPeticion()
		{
			ListaDeTraducciones = new List<Traduccion>();
			EtiquetaId = Guid.Empty;
			DiccionarioId = Guid.Empty;
		}

		#endregion
	}
}