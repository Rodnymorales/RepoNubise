using Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Peticion;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Servicios
{
    public interface IRecursos
    {
		Dictionary<string, string> GenerarRecursosPorIdioma(GenerarRecursosPorIdiomaPeticion peticionWebApi);
    }
}