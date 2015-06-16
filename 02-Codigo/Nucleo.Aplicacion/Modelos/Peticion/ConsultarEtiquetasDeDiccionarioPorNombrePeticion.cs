using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Comunes;

namespace Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Peticion
{
    public class ConsultarEtiquetasDeDiccionarioPorNombrePeticion : PeticionApp<ConsultarEtiquetasDeDiccionarioPorNombrePeticion>
	{
		[Required]
		public Guid DiccionarioId { get; set; }

		[Required]
		public string Nombre { get; set; }

		#region constructores

		private ConsultarEtiquetasDeDiccionarioPorNombrePeticion()
		{
			DiccionarioId = Guid.Empty;
			Nombre = string.Empty;
		}

		#endregion
	}
}