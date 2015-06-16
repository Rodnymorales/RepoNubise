using Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Peticion;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Respuesta;

namespace Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Fachada
{
    public interface IAdministradorDeDiccionarios
    {
        ConsultarDiccionariosRespuesta ConsultarDiccionarios();

        ConsultarUnDiccionarioarioRespuesta ConsultarUnDiccionario(ConsultarUnDiccionarioPeticion peticion);

        CrearUnDiccionarioRespuesta CrearUnDiccionario(CrearUnDiccionarioPeticion peticion);

        ModificarUnDiccionarioRespuesta ModificarUnDiccionario(ModificarUnDiccionarioPeticion peticion);

        EliminarUnDiccionarioRespuesta EliminarUnDiccionario(EliminarUnDiccionarioPeticion peticion);
    }
}