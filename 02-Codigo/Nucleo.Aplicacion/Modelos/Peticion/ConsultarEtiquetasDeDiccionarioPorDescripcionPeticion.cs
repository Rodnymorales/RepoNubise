using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Comunes;

namespace Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Peticion
{
    public class ConsultarEtiquetasDeDiccionarioPorDescripcionPeticion : PeticionApp<ConsultarEtiquetasDeDiccionarioPorDescripcionPeticion>
	{
		[Required]
		public Guid DiccionarioId { get; set; }

		[Required]
		public string Descripcion { get; set; }

		#region constructores

		private ConsultarEtiquetasDeDiccionarioPorDescripcionPeticion()
		{
			DiccionarioId = Guid.Empty;
			Descripcion = string.Empty;
		}

		#endregion
	}
}