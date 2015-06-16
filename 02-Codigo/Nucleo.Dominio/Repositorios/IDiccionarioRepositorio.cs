using System;
using System.Collections.Generic;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.Entidades.Diccionario;

namespace Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.Repositorios
{
	public interface IDiccionarioRepositorio
	{
		#region Consultas

		List<Diccionario> ObtenerDiccionarios ();

        Diccionario ObtenerUnDiccionario(Guid idDiccionario);

		#endregion

		#region Salvar

        Diccionario SalvarUnDiccionario(Diccionario diccionario);
		IEnumerable<Diccionario> SalvarDiccionarios (IEnumerable<Diccionario> diccionarioLista);

		#endregion

        #region "Eliminar"

        List<Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.Entidades.Diccionario.Diccionario> EliminarDiccionarios(List<Guid> idDiccionarioList);
        List<Nubise.Hc.Util.I18n.Babel.Nucleo.Dominio.Entidades.Diccionario.Diccionario> EliminarUnDiccionario(Guid idDiccionario);
       

        #endregion

    }
}
