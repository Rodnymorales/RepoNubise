using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Nubise.Hc.Util.I18n.Babel.Repositorios.ImplementacionXml.Modelo
{
	[XmlRoot ("traducciones")]
	public class Traducciones
	{
		[XmlElement ("traduccion")]
		public List<Traduccion> Traducciones1{ get; set; }

		public Traducciones ()
		{
			Traducciones1 = new List<Traduccion> ();
		}

		public Traducciones (Traduccion traduccion)
		{
			Traducciones1 = new List<Traduccion> {traduccion};
		}
	}
}

