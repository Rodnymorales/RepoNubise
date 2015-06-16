using System.Xml.Serialization;

namespace Nubise.Hc.Util.I18n.Babel.Repositorios.ImplementacionXml.Modelo
{
	public class Traduccion
	{
		[XmlAttribute ("cultura")]
		public string Cultura{ get; set; }

		[XmlAttribute ("tooltip")]
		public string Tooltip{ get; set; }

		[XmlText]
		public string Value{ get; set; }

		public Traduccion ()
		{

		}

		public Traduccion (string cultura, string tooltip, string value)
		{
			Cultura = cultura;
			Tooltip = tooltip;
			Value = value;
		}
	}
}

