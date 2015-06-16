using System;
using System.Linq;
using System.Collections.Generic;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.Entidades.Diccionario;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Comunes;

namespace Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Respuesta
{
    public class CrearUnDiccionarioRespuesta : RespuestaApp<CrearUnDiccionarioRespuesta>
	{
		public Diccionario DiccionarioNuevo { get; set; }

		#region constructores

        private CrearUnDiccionarioRespuesta(string ambiente)
		{
            DiccionarioNuevo = Diccionario.CrearNuevoDiccionario(ambiente);
            Relaciones = new Dictionary<string, Guid> { { "diccionario", DiccionarioNuevo.Id } };
		}

        public static CrearUnDiccionarioRespuesta CrearNuevaInstancia(string ambiente)
		{
            return new CrearUnDiccionarioRespuesta(ambiente);
		}

		#endregion
	}
}