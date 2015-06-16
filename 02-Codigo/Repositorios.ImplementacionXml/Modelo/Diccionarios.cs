using System.Collections.Generic;
using System.Xml.Serialization;

namespace Nubise.Hc.Util.I18n.Babel.Repositorios.ImplementacionXml.Modelo
{
	[XmlRoot ("diccionarios")]
	public class Diccionarios
	{
		#region propiedades

		[XmlElement ("diccionario")]
		public List<Diccionario> ListaDiccionarios{ get; set; }

		#endregion

		#region constructores

		public Diccionarios ()
		{
			ListaDiccionarios = new List<Diccionario> ();
		}

		public Diccionarios (Diccionario diccionario)
		{
			ListaDiccionarios = new List<Diccionario> {diccionario};
		}

		#endregion


	}
}
