using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using comunes = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Comunes;

namespace Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Comunes
{
	[JsonObject]
	public class Etiquetas
	{
        [JsonProperty ("etiquetas")]
        public List<Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Comunes.Etiqueta> ListaEtiquetas { get; set; }

        [JsonConstructor]
        public Etiquetas()
        {
            this.ListaEtiquetas = new List<comunes.Etiqueta>();
        }
	}
}

