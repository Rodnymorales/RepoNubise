using System;
using System.Linq;
using System.Collections.Generic;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.Entidades.Diccionario;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Comunes;

namespace Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Respuesta
{
    public class ConsultarDiccionariosRespuesta : RespuestaApp<ConsultarDiccionariosRespuesta>
	{     
		public List<Diccionario> ListaDeDiccionarios { get; set; }

		#region constructores

		private ConsultarDiccionariosRespuesta()
		{
			ListaDeDiccionarios = new List<Diccionario>();
		}

		#endregion

	}
}