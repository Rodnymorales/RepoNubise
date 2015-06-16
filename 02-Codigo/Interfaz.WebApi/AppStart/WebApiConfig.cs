using System;
using System.Linq;
using System.Web.Http;

namespace Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.AppStart
{
	public static class WebApiConfig
	{
		public static void Register (HttpConfiguration config)
		{
			// Configuración y servicios de API web

			// Rutas de API web
			config.MapHttpAttributeRoutes ();			
		}
	}
}
