using System;
using System.Linq;
using System.Collections.Generic;
using Nubise.Hc.Utils.I18n.Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.Comunes;

namespace Nubise.Hc.Utils.I18n.Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.Entidades.Etiquetas
{
	public class Traducciones : ValueObject<Traducciones>
	{
		public readonly Dictionary<Cultura, Valor> dict;

		private Traducciones ()
		{
			dict = new Dictionary<Cultura, Valor> ();
		}

		public static Traducciones CrearNuevaTraduccion ()
		{
			return new Traducciones ();
		}

		public Traducciones AgregarTraduccion (Traduccion traduccion)
		{
			this.dict.Add (traduccion.cultura, traduccion.valor);

			return this;
		}

		public Traducciones EliminarTraduccion (Cultura cultura)
		{
			this.dict.Remove (cultura);

			return this;
		}

		public Traducciones ModificarTraduccion (Traduccion traduccion)
		{
			if (dict.ContainsKey (traduccion.cultura) == true) {
				dict [traduccion.cultura] = traduccion.valor;
			} else {
				this.AgregarTraduccion (traduccion);
			}

			return this;
		}

		//		public Traduccion this [Cultura cultura] {
		//			get {
		//				if (dict.ContainsKey (cultura) == true) {
		//					return Traduccion.CrearNuevaTraduccion (cultura, dict [cultura]);
		//				}
		//				return null;
		//			}
		//			set {
		//				throw new NotImplementedException ();
		//			}
		//		}
	}
}