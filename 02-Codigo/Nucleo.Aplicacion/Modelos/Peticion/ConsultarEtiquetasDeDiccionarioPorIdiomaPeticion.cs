using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Comunes;

namespace Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Peticion
{
    public class ConsultarEtiquetasDeDiccionarioPorIdiomaPeticion : PeticionApp<ConsultarEtiquetasDeDiccionarioPorIdiomaPeticion>
	{
		[Required]
		public Guid DiccionarioId { get; set; }

		[Required]
		public string Idioma { get; set; }

		#region constructores

		private ConsultarEtiquetasDeDiccionarioPorIdiomaPeticion()
		{
			DiccionarioId = Guid.Empty;
			Idioma = string.Empty;
		}

		#endregion
	}
}
