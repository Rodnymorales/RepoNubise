using System;
using System.Xml.Serialization;

namespace Nubise.Hc.Util.I18n.Babel.Repositorios.ImplementacionXml.Modelo
{
	public class Etiqueta
	{
		[XmlAttribute ("id")]
		public Guid Id { get; set; }

		[XmlAttribute ("nombre")]
		public string Nombre { get; set; }

		[XmlAttribute ("activo")]
		public bool Activo { get; set; }

		[XmlAttribute ("default")]
		public string IdiomaPorDefecto { get; set; }

		[XmlElement ("nombre")]
		public string NombreEtiqueta { get ; set ; }

		[XmlElement ("descripcion")]
		public string Descripcion { get; set; }

		[XmlElement ("traducciones")]
		public Traducciones Traducciones{ get; set; }
        
		public Etiqueta ()
		{
			Id = Guid.NewGuid ();
			Activo = false;
			IdiomaPorDefecto = string.Empty;
		}
	}
}