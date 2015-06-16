using System;
using System.Linq;
using System.Collections.Generic;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.Entidades.Etiquetas;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Comunes;

namespace Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Respuesta
{
    public class ConsultarEtiquetasDeDiccionarioPorIdiomaPorDefectoRespuesta : RespuestaApp<ConsultarEtiquetasDeDiccionarioPorIdiomaPorDefectoRespuesta>
	{
		public List<Etiqueta> ListaDeEtiquetas { get; set; }

		#region constructores

		private ConsultarEtiquetasDeDiccionarioPorIdiomaPorDefectoRespuesta()
		{
			Relaciones = new Dictionary<string, Guid> { { "diccionario", Guid.Empty } };
			ListaDeEtiquetas = new List<Etiqueta>();
		}

		#endregion
	}
}