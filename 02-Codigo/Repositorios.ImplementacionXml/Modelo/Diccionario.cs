using System;
using System.Xml.Serialization;

namespace Nubise.Hc.Util.I18n.Babel.Repositorios.ImplementacionXml.Modelo
{
	[XmlRoot ("diccionario")]
	public class Diccionario
	{
		#region propiedades

		[XmlAttribute ("id")]
		public Guid Id { get; set; }

		[XmlAttribute ("ambiente")]
		public string Ambiente{ get; set; }

		[XmlElement ("etiquetas")]
		public Etiquetas Etiquetas;

		#endregion

		#region constructores

		public Diccionario (string ambiente)
		{
			this.Id = Guid.NewGuid ();
			this.Ambiente = ambiente;
			this.Etiquetas = new Etiquetas ();
		}

		public Diccionario ()
		{
			this.Id = Guid.NewGuid ();
		}

		#endregion
	}
}

