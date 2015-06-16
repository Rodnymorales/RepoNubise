using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Comunes;

namespace Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Peticion
{
    public class ConsultarEtiquetasDeDiccionarioPorIdiomaPorDefectoPeticion : PeticionApp<ConsultarEtiquetasDeDiccionarioPorIdiomaPorDefectoPeticion>
	{
		[Required]
		public Guid DiccionarioId { get; set; }

		[Required]
		public string IdiomaPorDefecto { get; set; }

		#region constructores

		private ConsultarEtiquetasDeDiccionarioPorIdiomaPorDefectoPeticion()
		{
			DiccionarioId = Guid.Empty;
			IdiomaPorDefecto = String.Empty;
		}

		#endregion
	}
}