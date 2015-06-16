using System.Collections.Generic;
using comunes = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Comunes;
using Newtonsoft.Json;

namespace Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Comunes
{
    [JsonObject]
	public class Diccionarios
	{
		#region propiedades

		[JsonProperty("diccionarios")]
		public List<comunes.Diccionario> ListaDiccionarios{ get; set; }

		#endregion

		#region constructores
        [JsonConstructor]
		public Diccionarios ()
		{
			this.ListaDiccionarios = new List<comunes.Diccionario> ();
		}

		#endregion


	}
}
