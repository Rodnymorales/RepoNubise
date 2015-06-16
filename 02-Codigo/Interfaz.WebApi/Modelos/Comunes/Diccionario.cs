using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using comunes = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Comunes;

namespace Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Comunes
{
    [JsonObject("diccionario")]
	public class Diccionario
	{
		#region propiedades

        [JsonProperty("id")]
		public Guid Id { get; set; }

		[JsonProperty ("ambiente")]
		public string Ambiente{ get; set; }

        [JsonProperty("etiquetas")]
        public List<comunes.Etiqueta> Etiquetas;

		#endregion

		#region constructores

        [JsonConstructor]
		public Diccionario ()
		{
            this.Etiquetas = new List<Etiqueta>();
		}

		#endregion
	}
}

