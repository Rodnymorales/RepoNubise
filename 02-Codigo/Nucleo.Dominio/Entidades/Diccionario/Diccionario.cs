using System;
using System.Collections.Generic;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.Comunes;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.Entidades.Etiquetas;
using System.Collections.ObjectModel;

namespace Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.Entidades.Diccionario
{
	public class Diccionario : Entity<Diccionario>
	{
		# region campos

		private readonly List<Etiqueta> _etiquetaLista = new List<Etiqueta> ();

		#endregion

		#region propiedades

		public override int GetHashCode()
		{
			unchecked
			{
				int result = 17;
				result = result * 23 + base.GetHashCode();
				result = result * 23 + ((_etiquetaLista != null) ? _etiquetaLista.GetHashCode() : 0);
				result = result * 23 + ((Ambiente != null) ? Ambiente.GetHashCode() : 0);
				return result;
			}
		}

		public bool Equals(Diccionario other)
		{
			if (ReferenceEquals(null, other))
			{
				return false;
			}
			if (ReferenceEquals(this, other))
			{
				return true;
			}

			HashSet<Etiqueta> etiquetaSetOriginal = new HashSet<Etiqueta>(_etiquetaLista);
			HashSet<Etiqueta> etiquetaSetComparar = new HashSet<Etiqueta>(other._etiquetaLista);

			return base.Equals(other) &&
				   etiquetaSetOriginal.SetEquals(etiquetaSetComparar) &&
				   Equals(Ambiente, other.Ambiente);
		}

		public override bool Equals(object obj)
		{
			Diccionario temp = obj as Diccionario;
			if (temp == null)
				return false;
			return Equals(temp);
		}

		public IReadOnlyList<Etiqueta> Etiquetas
		{
			get
			{
				return new ReadOnlyCollection<Etiqueta> (_etiquetaLista);
			}
		}

		public string Ambiente { get; set; }

        #endregion

		#region constructores

		private Diccionario (string ambiente)
		{
			Ambiente = ambiente;
		}

		private Diccionario (Guid id, string ambiente)
			: base (id)
		{
			Ambiente = ambiente;
		}

		public static Diccionario CrearNuevoDiccionario (string ambiente)
		{
			return new Diccionario (ambiente);
		}

		public static Diccionario CrearNuevoDiccionario (Guid id, string ambiente)
		{
			return new Diccionario (id, ambiente);
		}

		#endregion

		#region agregar

		public Diccionario AgregarEtiqueta (Etiqueta etiqueta)
		{
			if (etiqueta == null) {
				throw new ArgumentNullException ();
			}

			if (_etiquetaLista.Exists (item => item.Nombre == etiqueta.Nombre)) {
				throw new ArgumentException ("Ya existe una etiqueta con Nombre " + etiqueta.Nombre);
			}

			_etiquetaLista.Add (etiqueta);

			return this;
		}

		public Diccionario AgregarEtiquetas (List<Etiqueta> etiquetas)
		{
			if (etiquetas == null)
            {
				throw new ArgumentNullException ();
			}

			foreach (var item in etiquetas)
            {
				AgregarEtiqueta (item);
			}

			return this;
		}


		#endregion

		#region editar

		public Diccionario ModificarEtiquetas (List<Etiqueta> etiquetas)
		{
			foreach (Etiqueta item in etiquetas) {
				ModificarEtiqueta (item);
			}

			return this;

		}

		public Diccionario ModificarEtiqueta (Etiqueta etiqueta)
		{
			if (_etiquetaLista.Exists (item => item.Id == etiqueta.Id))
            {
				_etiquetaLista [_etiquetaLista.FindIndex (item => item.Id == etiqueta.Id)] = etiqueta;
			}
            else
            {
				AgregarEtiqueta (etiqueta);
			}

			return this;
		}

		#endregion

		#region eliminar

		public void EliminarTodoElDiccionario ()
		{
			_etiquetaLista.Clear ();
		}

		public Diccionario EliminarEtiqueta (Etiqueta etiqueta)
		{
			if (etiqueta == null) {
				throw new ArgumentNullException ();
			}

			_etiquetaLista.Remove (etiqueta);

			return this;
		}

		#endregion

	}
}
