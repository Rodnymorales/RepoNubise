using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Comunes
{
    [JsonObject]
    public class EtiquetasRecursos
    {
        [JsonProperty("etiquetas")]
        public List<string> Etiquetas { get; set; }

        [JsonConstructor]
        public EtiquetasRecursos()
        {
            this.Etiquetas = new List<string>();
        }

    }
}