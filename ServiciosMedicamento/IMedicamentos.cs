using ServiciosMedicamento.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ServiciosMedicamento
{
    // NOTA: Esta interfaz contiene los metodos que se van a exponer afuera, que queremos que sean consumidos por un cliente
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IMedicamentos
    {
        // Listado de Medicamentos
        [OperationContract]
        List<MedicamentoCLS> listarMedicamentos();

        //Lista forma farmaceutica (Tipo de medicamento)
        [OperationContract]
        List<FormaFarmaceuticaCLS> listarFormaFarmaceutica();

        //Recuperar medicamento
        [OperationContract]
        MedicamentoCLS recuperarMedicamento(int iidMedicamento);

        //Agregar y editar medicamento
        [OperationContract]
        int registrarYActualizarMedicamentos(MedicamentoCLS oMedicamentoCLS);

        //Eliminar medicamento
        [OperationContract]
        int eliminarMedicamento(int iidMedicamento);

    }


    // Utilice un contrato de datos, como se ilustra en el ejemplo siguiente, para agregar tipos compuestos a las operaciones de servicio.

}
