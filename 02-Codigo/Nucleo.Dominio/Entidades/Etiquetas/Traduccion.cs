using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.Comunes;

namespace Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.Entidades.Etiquetas
{
	public class Traduccion : ValueObject<Traduccion>
	{
		[Required]
		public Cultura Cultura { get; set; }

		[Required]
		public string Texto { get; set; }


		public string ToolTip { get; set; }

		public Traduccion ()
		{
		}

		private Traduccion (Cultura cultura, string texto)
		{
			this.Cultura = cultura;
			this.Texto = texto;
			this.ToolTip = string.Empty;
		}

		private Traduccion (Cultura cultura, string texto, string toolTip)
		{
			this.Cultura = cultura;
			this.Texto = texto;
			this.ToolTip = toolTip;
		}

		public static Traduccion CrearNuevaTraduccion (Cultura cultura, string texto)
		{
			var instancia = new Traduccion (cultura, texto);

			Validator.ValidateObject (instancia, new ValidationContext (instancia), true);

			return instancia;
		}



		public static Traduccion CrearNuevaTraduccion (Cultura cultura, string texto, string toolTip)
		{
			var instancia = new Traduccion (cultura, texto, toolTip);

			Validator.ValidateObject (instancia, new ValidationContext (instancia), true);

			return instancia;
		}
	}
}