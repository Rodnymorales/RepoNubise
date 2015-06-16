using System.Collections.Generic;
using System;
using System.Reflection;
using Newtonsoft.Json;

namespace Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Comunes
{
	public abstract class RespuestaWeb <T> : IEquatable<T> where T : RespuestaWeb<T>
	{
        [JsonProperty("relaciones")]
		public Dictionary<string,Uri> Relaciones{ get; set; }

		protected RespuestaWeb ()
		{
			Relaciones = new Dictionary<string,Uri> ();
		}

		public static T CrearNuevaRespuestaVacia ()
		{
			return (T)Activator.CreateInstance (typeof(T), true);
		}

		#region Igualdad

		public override bool Equals (object obj)
		{
			if (obj == null)
				return false;
			var other = obj as T;
			return Equals (other);
		}

		public override int GetHashCode ()
		{
			IEnumerable<FieldInfo> fields = GetFields ();
			const int startValue = 17;
			const int multiplier = 59;
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

		IEnumerable<FieldInfo> GetFields ()
		{
			var t = GetType ();
			var fields = new List<FieldInfo> ();

			while (t != typeof(object)) {
				fields.AddRange (t.GetFields (BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public));
				t = t.BaseType;
			}

			return fields;
		}

		public static bool operator == (RespuestaWeb<T> x, RespuestaWeb<T> y)
		{
			return object.ReferenceEquals (x, null) ? object.ReferenceEquals (y, null) : x.Equals (y);

		}

		public static bool operator != (RespuestaWeb<T> x, RespuestaWeb<T> y)
		{
			return !(x == y);
		}

		#endregion
	}
}

