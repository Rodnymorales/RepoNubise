using System;
using System.Reflection;
using System.Collections.Generic;

namespace Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Comunes
{
	public abstract class PeticionApp<T> : IEquatable<T> where T : PeticionApp<T>
	{
		public Dictionary<string,Guid> Relaciones { get; set; }

		protected PeticionApp ()
		{
			Relaciones = new Dictionary<string, Guid> ();
		}

        public static T CrearNuevaInstancia()
		{
			return (T)Activator.CreateInstance (typeof(T), true);
		}

		#region Igualdad

		public override bool Equals (object obj)
		{
			if (obj == null)
				return false;
			T other = obj as T;
			return Equals (other);
		}

		public override int GetHashCode ()
		{
			IEnumerable<FieldInfo> fields = GetFields ();
			int startValue = 17;
			int multiplier = 59;
			int hashCode = startValue;

			foreach (FieldInfo field in fields) {
				object value = field.GetValue (this);

				if (value != null)
					hashCode = hashCode * multiplier + value.GetHashCode ();
			}

			return hashCode;
		}

		public virtual bool Equals (T other)
		{
			if (other == null)
				return false;
			Type t = GetType ();
			Type otherType = other.GetType ();

			if (t != otherType)
				return false;

			FieldInfo[] fields = t.GetFields (BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

			foreach (FieldInfo field in fields) {
				object value1 = field.GetValue (other);
				object value2 = field.GetValue (this);

				if (value1 == null) {
					if (value2 != null)
						return false;
				} else if (!value1.Equals (value2))
					return false;
			}

			return true;
		}

		private IEnumerable<FieldInfo> GetFields ()
		{
			Type t = GetType ();
			List<FieldInfo> fields = new List<FieldInfo> ();

			while (t != typeof(object)) {
				fields.AddRange (t.GetFields (BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public));
				t = t.BaseType;
			}

			return fields;
		}

		public static bool operator == (PeticionApp<T> x, PeticionApp<T> y)
		{
			if (object.ReferenceEquals (x, null)) {
				return object.ReferenceEquals (y, null);
			}

			return x.Equals (y);
		}

		public static bool operator != (PeticionApp<T> x, PeticionApp<T> y)
		{
			return !(x == y);
		}

		#endregion
	}
}


