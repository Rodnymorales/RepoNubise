using System;
using System.Xml.Serialization;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Comunes
{
    [JsonObject]
	public class Etiqueta
	{
		[JsonProperty ("id")]
		public Guid Id { get; set; }

        [JsonProperty("activo")]
		public bool Activo { get; set; }

        [JsonProperty("idiomapordefecto")]
		public string IdiomaPorDefecto { get; set; }

        [JsonProperty("nombre")]
		public string Nombre { get ; set ; }

        [JsonProperty("descripcion")]
		public string Descripcion { get; set; }

        [JsonProperty("textos")]
		public List<Traduccion> Traducciones{ get; set; }


        [JsonConstructor]
		public Etiqueta ()
		{
			this.Activo = false;
			this.IdiomaPorDefecto = string.Empty;
            this.Traducciones = new List<Traduccion>();

		}
	}
}