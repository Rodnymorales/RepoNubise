using System;
using System.Collections.Generic;
using System.Linq;
using comunes = Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.Modelos.Comunes;

namespace Nubise.Hc.Util.I18n.Babel.Interfaz.WebApi.PruebasUnitarias.Utilitarios
{
    class ConfigurarLlamadasHttpMock
    {

        public comunes.Diccionario DiccionarioMock { get; set; }
        public comunes.Etiqueta EtiquetaMock { get; set; }
        public comunes.Traduccion TraduccionMock { get; set; }

        private ConfigurarLlamadasHttpMock(string ambiente, string idDiccionario)
        {
            this.DiccionarioMock = new comunes.Diccionario();
            this.DiccionarioMock.Id = new Guid(idDiccionario);
            this.DiccionarioMock.Ambiente = ambiente;
        }

        private ConfigurarLlamadasHttpMock(string ambiente, string idDiccionario, Dictionary<string, string> parametros)
        {
            this.DiccionarioMock = new comunes.Diccionario();
            this.DiccionarioMock.Id = new Guid(idDiccionario);
            this.DiccionarioMock.Ambiente = ambiente;
            this.EtiquetaMock = new comunes.Etiqueta();
            string valorParametro;

            parametros.TryGetValue("Descripcion", out valorParametro);

            this.EtiquetaMock.Descripcion = valorParametro;


        }

        public static ConfigurarLlamadasHttpMock ConfigurarMockPeticionHttp(string ambiente, string idDiccionario = "00000000-0000-0000-0000-000000000000")
        {
            return new ConfigurarLlamadasHttpMock(ambiente, idDiccionario);
        }


        public static ConfigurarLlamadasHttpMock ConfigurarMockPeticionHttp(string ambiente, string idDiccionario, Dictionary<string, string> parametros)
        {
            return new ConfigurarLlamadasHttpMock(ambiente, idDiccionario,parametros);
        }
    }
}