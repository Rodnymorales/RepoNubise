using System;
using System.Linq;
using System.Globalization;
using System.Collections;

namespace Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.Servicios
{
	public class ServicioDeCulturasIso
	{
		public IEnumerable CulturasEspecificas { get; private set; }

		public IEnumerable CulturasNeutrales { get; private set; }

		private ServicioDeCulturasIso ()
		{
			this.CulturasEspecificas = CultureInfo.GetCultures (CultureTypes.SpecificCultures);
			this.CulturasNeutrales = CultureInfo.GetCultures (CultureTypes.NeutralCultures);
		}

		public static ServicioDeCulturasIso CrearObjetoDeCulturasIso ()
		{
			return new ServicioDeCulturasIso ();
		}

	
	}

}