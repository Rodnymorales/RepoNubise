using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Comunes
{
	public class TraduccionRecurso : IEquatable<TraduccionRecurso>
	{
		public string Nombre { get; set; }

		public string Texto { get; set; }

		public string Tooltip { get; set; }

		#region constructores

		private TraduccionRecurso(string nombre, string texto, string tooltip)
		{
			Nombre = nombre;
			Texto = texto;
			Tooltip = tooltip;
		}
		public static TraduccionRecurso CrearNuevaInstancia(string nombre, string texto, string tooltip)
		{
			return new TraduccionRecurso(nombre, texto, tooltip);
		}

		#endregion


		#region Igualdad

		public override int GetHashCode()
		{
			unchecked
			{
				int result = 17;
				result = result * 23 + ((Nombre != null) ? this.Nombre.GetHashCode() : 0);
				result = result * 23 + ((Texto != null) ? this.Texto.GetHashCode() : 0);
				result = result * 23 + ((Tooltip != null) ? this.Tooltip.GetHashCode() : 0);
				return result;
			}
		}

		public bool Equals(TraduccionRecurso other)
		{
			if (ReferenceEquals(null, other))
			{
				return false;
			}
			if (ReferenceEquals(this, other))
			{
				return true;
			}
			return Equals(this.Nombre, other.Nombre) &&
				   Equals(this.Texto, other.Texto) &&
				   Equals(this.Tooltip, other.Tooltip);
		}

		public override bool Equals(object obj)
		{
			TraduccionRecurso temp = obj as TraduccionRecurso;
			if (temp == null)
				return false;
			return this.Equals(temp);
		}


		#endregion
	}
}
