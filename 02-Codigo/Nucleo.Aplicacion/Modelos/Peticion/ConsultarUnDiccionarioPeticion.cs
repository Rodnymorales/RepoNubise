using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Comunes;

namespace Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Peticion
{
    public class ConsultarUnDiccionarioPeticion : PeticionApp<ConsultarUnDiccionarioPeticion>
	{
		[Required]
		public Guid DiccionarioId { get; set; }

		#region constructores

		private ConsultarUnDiccionarioPeticion()
		{
			DiccionarioId = Guid.Empty;
		}

		#endregion
	}
}