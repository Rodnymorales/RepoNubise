using System;
using System.Linq;
using System.Collections.Generic;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Comunes;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.Entidades.Etiquetas;

namespace Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Respuesta
{
    public class EliminarTraduccionesAUnaEtiquetaDeUnDiccionarioRespuesta : RespuestaApp<EliminarTraduccionesAUnaEtiquetaDeUnDiccionarioRespuesta>
	{
		public List<Traduccion> ListaDeTraducciones { get; set; }

		#region constructores

		private EliminarTraduccionesAUnaEtiquetaDeUnDiccionarioRespuesta()
		{
			Relaciones = new Dictionary<string, Guid> { { "diccionario", Guid.Empty }, { "etiqueta", Guid.Empty } };
			ListaDeTraducciones = new List<Traduccion>();
		}

		#endregion
	}
}