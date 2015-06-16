using System;
using System.Linq;
using System.Collections.Generic;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Comunes;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.Entidades.Etiquetas;

namespace Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Respuesta
{
    public class ConsultarEtiquetasDeDiccionarioPorNombreRespuesta : RespuestaApp<ConsultarEtiquetasDeDiccionarioPorNombreRespuesta>
	{
		public List<Etiqueta> ListaDeEtiquetas { get; set; }

		//public Dictionary<string, Guid> Relaciones { get; set; }

		#region constructores

		private ConsultarEtiquetasDeDiccionarioPorNombreRespuesta()
		{
			Relaciones = new Dictionary<string, Guid> { { "diccionario", Guid.Empty }};
			ListaDeEtiquetas = new List<Etiqueta>();
		}

        #endregion
	}
}