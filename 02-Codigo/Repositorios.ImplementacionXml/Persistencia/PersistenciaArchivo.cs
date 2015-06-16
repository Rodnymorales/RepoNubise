using Nubise.Hc.Util.I18n.Babel.Repositorios.ImplementacionXml.Modelo;
using System.Xml.Serialization;
using System.IO;

namespace Nubise.Hc.Util.I18n.Babel.Repositorios.ImplementacionXml.Persistencia
{
    public class PersistenciaArchivo : IPersistencia
    {

        public Diccionarios LeerXml(string directorio, XmlSerializer serializador)
        {
            var reader = new StreamReader(directorio);
            object obj;

            using (reader)
            {
                obj = serializador.Deserialize(reader);
            }

            return (Diccionarios)obj;
        }

        public Diccionarios EscribirXml(string directorio,XmlSerializer serializador, Diccionarios diccionarios)
        {            
            //try
            //{
                File.Delete(directorio);                              

                using (TextWriter writer = new StreamWriter(directorio))
                {
                    serializador.Serialize(writer, diccionarios);
                    
                }
            return LeerXml(directorio, serializador);
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception(ex.Message);
            //}
        }
    }
}
