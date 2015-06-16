using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.Comunes;
using System.ComponentModel.DataAnnotations;

namespace Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.Entidades.Etiquetas
{
	public class Etiqueta : Entity<Etiqueta>
	{
		private readonly List<Traduccion> listaTextos = new List<Traduccion> ();

		public bool Activo { get; set; }


		public string IdiomaPorDefecto { get; set; }


		public string Descripcion { get; set; }

		[Required]
		public string Nombre { get; set; }


		public override int GetHashCode()
		{
			unchecked
			{
				int result = 17;
				result = result * 23 + base.GetHashCode();
				result = result * 23 + ((listaTextos != null) ? this.listaTextos.GetHashCode() : 0);
				result = result * 23 + this.Activo.GetHashCode();
				result = result * 23 + ((IdiomaPorDefecto != null) ? this.IdiomaPorDefecto.GetHashCode() : 0);
				result = result * 23 + ((Descripcion != null) ? this.Descripcion.GetHashCode() : 0);
				result = result * 23 + ((Nombre != null) ? this.Nombre.GetHashCode() : 0);
				return result;
			}
		}

		public bool Equals(Etiqueta other)
		{
			if (ReferenceEquals(null, other))
			{
				return false;
			}
			if (ReferenceEquals(this, other))
			{
				return true;
			}

			HashSet<Traduccion> traduccionSetOriginal = new HashSet<Traduccion>(this.listaTextos);
			HashSet<Traduccion> traduccionSetComparar = new HashSet<Traduccion>(other.listaTextos);

			return base.Equals(other) &&
				   traduccionSetOriginal.SetEquals(traduccionSetComparar) &&
				   this.Activo == other.Activo &&
				   Equals(this.IdiomaPorDefecto, other.IdiomaPorDefecto) &&
				   Equals(this.Descripcion, other.Descripcion) &&
				   Equals(this.Nombre, other.Nombre);
		}

		public override bool Equals(object obj)
		{
			Etiqueta temp = obj as Etiqueta;
			if (temp == null)
				return false;
			return this.Equals(temp);
		}

		public IReadOnlyList<Traduccion> Textos
		{
			get
			{
				return new ReadOnlyCollection<Traduccion> (this.listaTextos);
			}
		}

		private Etiqueta (string nombre)
		{
			this.Nombre = nombre;
		}

		private Etiqueta (Guid id)
			: base (id)
		{
		}

		public static Etiqueta CrearNuevaEtiqueta (Guid id)
		{
			return new Etiqueta (id);
		}

		public static Etiqueta CrearNuevaEtiqueta (string nombre)
		{
			var entidad = new Etiqueta (nombre);

			Validator.ValidateObject (entidad, new ValidationContext (entidad), true);

			return entidad;
		}

		public Etiqueta AgregarTraduccion (Traduccion traduccion)
		{
			Validator.ValidateObject (traduccion, new ValidationContext (traduccion), true);

			if (this.listaTextos.Exists (item => item.Cultura.CodigoIso == traduccion.Cultura.CodigoIso)) {
				throw new ArgumentException ("Ya existe una traducción con código Iso " + traduccion.Cultura.CodigoIso);
			}

			this.listaTextos.Add (traduccion);

			return this;
		}

		public Etiqueta AgregarTraducciones (List<Traduccion> traducciones)
		{
			if (traducciones == null) {
				throw new ArgumentNullException ();
			}

			foreach (Traduccion item in traducciones) {
				this.AgregarTraduccion (item);
			}

			return this;
		}

		public Etiqueta EliminarTraduccion (Traduccion traduccion)
		{
			this.listaTextos.Remove (traduccion);

			return this;
		}

		public Etiqueta EliminarTraducciones (List<Traduccion> traducciones)
		{
			foreach (Traduccion item in traducciones) {
				this.EliminarTraduccion (item);
			}

			return this;
		}

		public Etiqueta ModificarTraduccion (Traduccion traduccion)
		{
			if (this.listaTextos.Exists (item => item.Cultura.CodigoIso == traduccion.Cultura.CodigoIso)) {
				this.listaTextos [this.listaTextos.FindIndex (item => item.Cultura.CodigoIso == traduccion.Cultura.CodigoIso)] = traduccion;
			} else {
				this.AgregarTraduccion (traduccion);
			}

			return this;
		}

		public Etiqueta ModificarTraducciones (List<Traduccion> traducciones)
		{
			foreach (Traduccion item in traducciones) {
				this.ModificarTraduccion (item);
			}

			return this;
		}
	}
}