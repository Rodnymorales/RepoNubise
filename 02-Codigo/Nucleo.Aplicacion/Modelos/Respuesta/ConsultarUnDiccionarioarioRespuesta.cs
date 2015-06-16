using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.Entidades.Diccionario;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Comunes;

namespace Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Respuesta
{
    public class ConsultarUnDiccionarioarioRespuesta : RespuestaApp<ConsultarUnDiccionarioarioRespuesta>
	{
		public Diccionario Diccionario { get; set; }


		#region constructores

		private ConsultarUnDiccionarioarioRespuesta(string ambiente)
		{
			Relaciones = new Dictionary<string, Guid> { { "diccionario", Guid.Empty } };
            Diccionario = Diccionario.CrearNuevoDiccionario(ambiente);
		}

        public static ConsultarUnDiccionarioarioRespuesta CrearNuevaInstancia(string ambiente) {
            return new ConsultarUnDiccionarioarioRespuesta(ambiente);
        }

		#endregion
	}
}