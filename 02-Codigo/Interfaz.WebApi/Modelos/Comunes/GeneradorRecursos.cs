using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiModelosRespuesta = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Respuesta;

namespace Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Comunes
{
    public abstract class GeneradorRecursos
    {
        public Dictionary<string, string> RutasDeArchivosPorCultura { get; set; }

        public GeneradorRecursos()
        {

        }

        public GeneradorRecursos(Dictionary<string,string> RutasDeArchivosPorCultura)
        {

        }


    }
}