using Nubise.Hc.Util.I18n.Babel.Repositorios.ImplementacionXml.Modelo;
using System.Xml.Serialization;

namespace Nubise.Hc.Util.I18n.Babel.Repositorios.ImplementacionXml.Persistencia
{
    public interface IPersistencia
    {
        Diccionarios LeerXml(string directorio, XmlSerializer serializador);
        Diccionarios EscribirXml(string directorio, XmlSerializer serializador, Diccionarios diccionarios);
       

    }
}
