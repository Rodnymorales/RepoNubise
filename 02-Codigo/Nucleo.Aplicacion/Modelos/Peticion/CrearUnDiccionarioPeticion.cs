using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Comunes;

namespace Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Peticion
{
    public class CrearUnDiccionarioPeticion : PeticionApp<CrearUnDiccionarioPeticion>
	{
		[Required]
		public string Ambiente { get; set; }

		#region Constructores

		private CrearUnDiccionarioPeticion(string ambiente)
		{
            Ambiente = ambiente;
		}

        public static CrearUnDiccionarioPeticion CrearNuevaInstancia(string ambiente) {
            return new CrearUnDiccionarioPeticion(ambiente);
        }

		#endregion
	}
}