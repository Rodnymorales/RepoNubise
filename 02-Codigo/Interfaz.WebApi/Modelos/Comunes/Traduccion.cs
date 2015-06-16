using System;
using Newtonsoft.Json;
using comunes = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Comunes;

namespace Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Comunes
{
    [JsonObject("textos")]
	public class Traduccion
	{
		[JsonProperty ("cultura")]
		public string Cultura{ get; set; }

        [JsonProperty("tooltip")]
		public string Tooltip{ get; set; }

		[JsonProperty("texto")]
		public string Value{ get; set; }

        [JsonConstructor]
		public Traduccion ()
		{

		}

	}
}

