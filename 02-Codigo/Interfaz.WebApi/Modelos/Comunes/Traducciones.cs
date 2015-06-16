using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using comunes = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Comunes;

namespace Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Comunes
{
	[JsonObject]
	public class Traducciones
	{
        [JsonProperty("textos")]
        public List<comunes.Traduccion> Traducciones1 { get; set; }

        [JsonConstructor]
        public Traducciones()
        {
            this.Traducciones1 = new List<Traduccion>();
        }
    }
}

