using Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Peticion;
using Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Modelos.Respuesta;

namespace Nubise.Hc.Util.I18n.Babel.Nucleo.Aplicacion.Fachada
{
    public interface IAdministradorDeEtiquetas
    {
        ConsultarEtiquetasPorNombreRespuesta ConsultarEtiquetasPorNombre(ConsultarEtiquetasPorNombrePeticion peticion);

        ConsultarEtiquetasDeDiccionarioPorIdiomaRespuesta ConsultarEtiquetasDeDiccionarioPorIdioma(ConsultarEtiquetasDeDiccionarioPorIdiomaPeticion peticion);

        ConsultarEtiquetasDeDiccionarioPorNombreRespuesta ConsultarEtiquetasDeDiccionarioPorNombre(ConsultarEtiquetasDeDiccionarioPorNombrePeticion peticion);

        ConsultarEtiquetasDeDiccionarioPorDescripcionRespuesta ConsultarEtiquetasDeDiccionarioPorDescripcion(ConsultarEtiquetasDeDiccionarioPorDescripcionPeticion peticion);

        ConsultarEtiquetasDeDiccionarioPorEstatusRespuesta ConsultarEtiquetasDeDiccionarioPorEstatus(ConsultarEtiquetasDeDiccionarioPorEstatusPeticion peticion);

        ConsultarEtiquetasDeDiccionarioPorIdiomaPorDefectoRespuesta ConsultarEtiquetasDeDiccionarioPorIdiomaPorDefecto(ConsultarEtiquetasDeDiccionarioPorIdiomaPorDefectoPeticion peticion);

        AgregarEtiquetasAUnDiccionarioRespuesta AgregarEtiquetasAUnDiccionario(AgregarEtiquetasAUnDiccionarioPeticion peticion);

        ModificarEtiquetasAUnDiccionarioRespuesta ModificarEtiquetasAUnDiccionario(ModificarEtiquetasAUnDiccionarioPeticion peticion);

        EliminarEtiquetasAUnDiccionarioRespuesta EliminarEtiquetasAUnDiccionario(EliminarEtiquetasAUnDiccionarioPeticion peticion);
    }
}