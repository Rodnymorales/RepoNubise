using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Comunes;

namespace Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Peticion
{
	/// <summary>
	/// Se consulta el nombre de la etiqueta enviado en todos los diccionarios
	/// </summary>
    public class ConsultarEtiquetasPorNombrePeticion : PeticionApp<ConsultarEtiquetasPorNombrePeticion>
	{
		[Required]
		public string Nombre { get; set; }

		#region Constructores

		private ConsultarEtiquetasPorNombrePeticion()
		{
			Nombre = string.Empty;
		}

		#endregion
	}
}