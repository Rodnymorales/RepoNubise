using System;
using System.Linq;
using System.Collections.Generic;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Comunes;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.Entidades.Etiquetas;

namespace Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Respuesta
{
    public class ConsultarEtiquetasDeDiccionarioPorEstatusRespuesta : RespuestaApp<ConsultarEtiquetasDeDiccionarioPorEstatusRespuesta>
	{
		public List<Etiqueta> ListaDeEtiquetas { get; set; }

		#region constructores

		private ConsultarEtiquetasDeDiccionarioPorEstatusRespuesta()
		{
            Relaciones = new Dictionary<string, Guid> { { "diccionario", Guid.Empty } };
			ListaDeEtiquetas = new List<Etiqueta>();
		}

		#endregion
	}
}