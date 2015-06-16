using System;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.Repositorios;
using System.Collections.Generic;
using Nubise.Hc.Util.I18n.Babel.Repositorios.ImplementacionXml.Configuracion;
using Nubise.Hc.Util.I18n.Babel.Repositorios.ImplementacionXml.Persistencia;
using EntidadDom = Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.Entidades;
using EntidadRepo = Nubise.Hc.Util.I18n.Babel.Repositorios.ImplementacionXml.Modelo;
using System.Xml.Serialization;
using AutoMapper;
using System.Linq;
using Nubise.Hc.Util.I18n.Babel.Repositorios.ImplementacionXml.Modelo;

namespace Nubise.Hc.Util.I18n.Babel.Repositorios.ImplementacionXml.Implementacion
{
    public class DiccionarioRepositorioXmlImpl : IDiccionarioRepositorio
    {

        #region "Atributos Privados"

        private readonly string _directorio;
        private readonly XmlSerializer _serializador = new XmlSerializer(typeof(Diccionarios));
        private readonly IPersistencia _persistencia;

        #endregion

        #region "Constructor"

        /// <summary>
        ///  Método: DiccionarioRepositorioXmlImpl
        ///  Desarrollador: Johans Cuéllar
        ///  Creado: 22/05/2015
        ///  Descripción: Constructor de la clase DiccionarioRepositorioXmlImpl.        
        /// </summary>
        /// <param name="directorio"></param>
        /// <param name="persistencia"></param>
        /// <param name="serializador"></param>       
        public DiccionarioRepositorioXmlImpl(string directorio, IPersistencia persistencia, XmlSerializer serializador)
        {
            AutoMapperConfig.SetAutoMapperConfiguration();
            _directorio = directorio;
            _serializador = serializador;
            _persistencia = persistencia;
        }


        #endregion

        #region "IDiccionarioRepositorio implementation"

        /// <summary>	
        ///  Método: ObtenerDiccionarios
        ///  Desarrollador: Johans Cuéllar
        ///  Creado: 20/05/2015
        ///  Descripción: Método que busca todos los diccionarios contenido en el xml repositario.        
        /// </summary>
        /// <returns>Lista de tipo EntidadDom.Diccionario.Diccionario</returns>
        public List<EntidadDom.Diccionario.Diccionario> ObtenerDiccionarios()
        {
            var diccionariosRepositorio = _persistencia.LeerXml(_directorio, _serializador);

            return diccionariosRepositorio.ListaDiccionarios.Select(MapearRepositorioConDiccionario).ToList();
        }

        /// <summary>	
        ///  Método: MapearRepositorioConDiccionario
        ///  Desarrollador: Johans Cuéllar
        ///  Creado: 14/05/2015
        ///  Descripción: Método que mapea dinámicamente objetos repositorios a objetos dominio.        
        /// </summary>
        /// <param name="diccionarioRepo">Diccionario de tipo repositorio</param>
        /// <returns>Un diccionario de tipo objetos de dominio</returns>
        private EntidadDom.Diccionario.Diccionario MapearRepositorioConDiccionario(Diccionario diccionarioRepo)
        {
            var diccionarioDominio = EntidadDom.Diccionario.Diccionario.CrearNuevoDiccionario(diccionarioRepo.Id,
                diccionarioRepo.Ambiente);

            for (var i = 00; i < diccionarioRepo.Etiquetas.ListaEtiquetas.Count(); i++)
            {
                diccionarioDominio.AgregarEtiqueta(
                    Mapper.Map<EntidadDom.Etiquetas.Etiqueta>(diccionarioRepo.Etiquetas.ListaEtiquetas[i]));

                for (var x = 0; x < diccionarioRepo.Etiquetas.ListaEtiquetas[i].Traducciones.Traducciones1.Count(); x++)
                {
                    var cultura = EntidadDom.Etiquetas.Cultura.CrearNuevaCultura(
                        diccionarioRepo.Etiquetas.ListaEtiquetas[i].Traducciones.Traducciones1[x].Cultura);
                    var traduccion = Mapper.Map<EntidadDom.Etiquetas.Traduccion>(
                        diccionarioRepo.Etiquetas.ListaEtiquetas[i].Traducciones.Traducciones1[x]);
                    traduccion.Cultura = cultura;
                    diccionarioDominio.Etiquetas[i].AgregarTraduccion(traduccion);
                }
            }
            return diccionarioDominio;
        }

        /// <summary>	
        ///  Método: MapearDiccionarioConRepositorio
        ///  Desarrollador: Johans Cuéllar
        ///  Creado: 15/05/2015
        ///  Descripción: Método que mapea dinámicamente objetos dominio a objetos repositorio.        
        /// </summary>
        /// <param name="diccionarioDom">Dicionario de tipo dominio</param>
        /// <returns>Diccionario mapeado de tipo repositorio</returns>
        private Diccionario MapearDiccionarioConRepositorio(Nucleo.Dominio.Entidades.Diccionario.Diccionario diccionarioDom)
        {
            var dicctionarioRepo = new Diccionario
            {
                Id = diccionarioDom.Id,
                Ambiente = diccionarioDom.Ambiente,
                Etiquetas = new Etiquetas()
            };

            foreach (var etiqueta in diccionarioDom.Etiquetas)
            {
                var etiquetaMapper = new Etiqueta
                {
                    Activo = etiqueta.Activo,
                    Descripcion = etiqueta.Descripcion,
                    Id = etiqueta.Id,
                    IdiomaPorDefecto = etiqueta.IdiomaPorDefecto,
                    Nombre = etiqueta.Nombre,
                    NombreEtiqueta = etiqueta.Nombre,
                    Traducciones = new Traducciones()

                };
                foreach (var textoMapper in etiqueta.Textos.Select(texto => new Traduccion
                {
                    Cultura = texto.Cultura.CodigoIso,
                    Tooltip = texto.ToolTip,
                    Value = texto.Texto
                }))
                {
                    etiquetaMapper.Traducciones.Traducciones1.Add(textoMapper);
                }
                dicctionarioRepo.Etiquetas.ListaEtiquetas.Add(etiquetaMapper);
            }
            return dicctionarioRepo;
        }

        /// <summary>	
        ///  Método: SalvarDiccionarios
        ///  Desarrollador: Johans Cuéllar
        ///  Creado: 12/05/2015
        ///  Descripción: Método que guarda los diccionarios en el xml repositario.
        /// </summary>        
        /// <param name="diccionarioLista">Lista de Diccionarios de tipo Dominio.Entidades.Diccionario.Diccionario</param>
        /// <returns>Lista de Diccionarios de tipo EntidadDom.Diccionario.Diccionario</returns>
        public IEnumerable<Nucleo.Dominio.Entidades.Diccionario.Diccionario> SalvarDiccionarios(
            IEnumerable<Nucleo.Dominio.Entidades.Diccionario.Diccionario> diccionarioLista)
        {
            var dirRepositario = new Diccionarios { ListaDiccionarios = new List<Diccionario>() };

            if (diccionarioLista.Count() == 0)
            {
                throw new ArgumentNullException();
            }

            foreach (var diccionario in diccionarioLista)
            {
                dirRepositario.ListaDiccionarios.Add(MapearDiccionarioConRepositorio(diccionario));
            }

            _persistencia.EscribirXml(_directorio, _serializador, dirRepositario);
            var dirRepositarioDom = _persistencia.LeerXml(_directorio, _serializador);
            return dirRepositarioDom.ListaDiccionarios.Select(MapearRepositorioConDiccionario).ToList();
        }

        /// <summary>	
        ///  Método: ObtenerUnDiccionario
        ///  Desarrollador: Johans Cuéllar
        ///  Creado: 14/05/2015
        ///  Descripción: Método que busca los diccionarios en el xml repositario.
        /// </summary>    
        /// <param name="idDiccionario">Recibe el id del diccionario a buscar de tipo Guid</param>
        /// <returns>Un diccionario de tipo EntidadDom.Diccionario.Diccionario</returns>
        public Nucleo.Dominio.Entidades.Diccionario.Diccionario ObtenerUnDiccionario(Guid idDiccionario)
        {
            EntidadDom.Diccionario.Diccionario diccionarioDom;
            var diccionarioRep = _persistencia.LeerXml(_directorio, _serializador);
            var diccionario = diccionarioRep.ListaDiccionarios.ToList().Find(e => e.Id == idDiccionario);

            if (diccionario == null)
            {
                return null;
            }

            diccionarioDom = MapearRepositorioConDiccionario(diccionario);
            return diccionarioDom;
        }

        /// <summary>	
        ///  Método: EliminarUnDiccionario
        ///  Desarrollador: Johans Cuéllar
        ///  Creado: 18/05/2015
        ///  Descripción: Método que elimina un diccionario en el xml repositario.
        /// </summary>
        /// <param name="idDiccionario">Id del diccionario a buscar de tipo Guid</param>
        /// <returns>Lista de diccionarios de tipo EntidadDom.Diccionario.Diccionario</returns>
        public List<Nucleo.Dominio.Entidades.Diccionario.Diccionario> EliminarUnDiccionario(Guid idDiccionario)
        {
            var diccionarioDom = new List<Nucleo.Dominio.Entidades.Diccionario.Diccionario>();
            var diccionarioRep = _persistencia.LeerXml(_directorio, _serializador);
            var dicSearch = diccionarioRep.ListaDiccionarios.Find(e => e.Id == new Guid(idDiccionario.ToString()));

            if (dicSearch != null)
            {
                diccionarioRep.ListaDiccionarios.Remove(dicSearch);
            }
            else
            {
                throw new NullReferenceException();
            }

            var dirRepositarioDom = _persistencia.EscribirXml(_directorio, _serializador, diccionarioRep);

            foreach (var dirRep in dirRepositarioDom.ListaDiccionarios)
            {
                diccionarioDom.Add(MapearRepositorioConDiccionario(dirRep));
            }

            return diccionarioDom;
        }

        /// <summary>	
        ///  Método: EliminarDiccionarios
        ///  Desarrollador: Johans Cuéllar
        ///  Creado: 18/05/2015
        ///  Descripción: Método que busca los diccionarios en el xml repositario para ser eliminados.
        /// </summary> 
        /// <param name="idDiccionarioList">Lista de id's de diccionarios a buscar de tipo Guid's</param>
        /// <returns>Lista de diccionarios de tipo Dominio.Entidades.Diccionario.Diccionario</returns>
        public List<Nucleo.Dominio.Entidades.Diccionario.Diccionario> EliminarDiccionarios(List<Guid> idDiccionarioList)
        {
            var diccionarioDom = new List<EntidadDom.Diccionario.Diccionario>();
            var diccionarioRep = _persistencia.LeerXml(_directorio, _serializador);
            foreach (var dicSearch in idDiccionarioList.Select(idDiccionario =>
                            diccionarioRep.ListaDiccionarios.Find(e => e.Id == new Guid(idDiccionario.ToString()))))
            {
                if (dicSearch != null)
                {
                    diccionarioRep.ListaDiccionarios.Remove(dicSearch);
                }
                else
                {
                    throw new NullReferenceException();
                }
            }
            var dirRepositarioDom = _persistencia.EscribirXml(_directorio, _serializador, diccionarioRep);

            diccionarioDom.AddRange(dirRepositarioDom.ListaDiccionarios.Select(MapearRepositorioConDiccionario));
            return diccionarioDom;
        }

        /// <summary>	
        ///  Método: SalvarUnDiccionario
        ///  Desarrollador: Johans Cuéllar
        ///  Creado: 21/05/2015
        ///  Descripción: Método que salva un diccionario en el xml repositario.
        /// </summary>   
        /// <param name="diccionario">Diccionario de tipo EntidadDom.Diccionario.Diccionario</param>
        /// <returns>Diccionario de tipo EntidadDom.Diccionario.Diccionario</returns>
        public EntidadDom.Diccionario.Diccionario SalvarUnDiccionario(EntidadDom.Diccionario.Diccionario diccionario)
        {
            var exist = false;

            var diccionarioRep = _persistencia.LeerXml(_directorio, _serializador);

            if (diccionario == null)
            {
                throw new Exception();
            }

            foreach (var direp in diccionarioRep.ListaDiccionarios)
            {
                if (direp.Id != diccionario.Id) continue;
                var dirRepoReplace = MapearDiccionarioConRepositorio(diccionario);
                direp.Etiquetas.ListaEtiquetas.Clear();
                foreach (var etiquetas in dirRepoReplace.Etiquetas.ListaEtiquetas)
                {
                    direp.Etiquetas.ListaEtiquetas.Add(etiquetas);
                }
                direp.Ambiente = diccionario.Ambiente;
                exist = true;
            }
            if (exist == false)
            {
                if (diccionarioRep.ListaDiccionarios.Find(e => e.Ambiente == diccionario.Ambiente) != null)
                {
                    throw new Exception();
                }
                diccionarioRep.ListaDiccionarios.Add(MapearDiccionarioConRepositorio(diccionario));
            }
            //_persistencia.EscribirXml(_directorio, _serializador, diccionarioRep);
            //var diccionarioRepDom = _persistencia.LeerXml(_directorio, _serializador);    
            var diccionarioRepDom = _persistencia.EscribirXml(_directorio, _serializador, diccionarioRep);
            return (from dirRep in diccionarioRepDom.ListaDiccionarios
                    where dirRep.Id == diccionario.Id
                    select MapearRepositorioConDiccionario(dirRep)).FirstOrDefault();
        }

        #endregion
    }
}

