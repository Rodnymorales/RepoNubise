using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Comunes;

namespace Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Peticion
{
	public class GenerarRecursosPorIdiomaPeticion : PeticionApp<GenerarRecursosPorIdiomaPeticion>
	{
		[Required]
		public List<String> ListaDeEtiquetas { get; set; }

		[Required]
		public String Ambiente { get; set; }

		#region constructores

		private GenerarRecursosPorIdiomaPeticion()
		{
			ListaDeEtiquetas = new List<String>();
			Ambiente = String.Empty;
		}
        
        #endregion
	}
}
