using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Comunes;

namespace Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Peticion
{
    public class ConsultarEtiquetasDeDiccionarioPorEstatusPeticion : PeticionApp<ConsultarEtiquetasDeDiccionarioPorEstatusPeticion>
	{
		[Required]
		public Guid DiccionarioId { get; set; }

		[Required]
		public bool Estatus { get; set; }

		#region constructores

		private ConsultarEtiquetasDeDiccionarioPorEstatusPeticion()
		{
			DiccionarioId = Guid.Empty;
			Estatus = false;
		}

		#endregion
	}
}
