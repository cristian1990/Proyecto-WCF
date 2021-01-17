using ServiciosMedicamento.Clases;
using ServiciosMedicamento.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ServiciosMedicamento
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
    // NOTE: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class Medicamentos : IMedicamentos
    {
        public int eliminarMedicamento(int iidMedicamento)
        {
            int rpta = 0;

            try
            {
                using (var bd = new MedicoEntities()) //Creo una instancia del contexto
                {
                    //Obtengo el objeto cuyo id sea el mismo que se paso como parametro
                    Medicamento oMedicamento = bd.Medicamento.Where(p => p.IIDMEDICAMENTO == iidMedicamento).First();
                    //Hago un borrado logico
                    oMedicamento.BHABILITADO = 0;
                    //Guardo los cambios en la BD
                    bd.SaveChanges();
                    rpta = 1;
                }
            }
            catch (Exception ex)
            {
                //Si no se pudo borrar 
                rpta = 0;
            }
            //Retornamos rpta, tendra el valor de 0 o 1
            return rpta;
        }

        public List<FormaFarmaceuticaCLS> listarFormaFarmaceutica()
        {
            throw new NotImplementedException();
        }

        public List<MedicamentoCLS> listarMedicamentos()
        {
            throw new NotImplementedException();
        }

        public MedicamentoCLS recuperarMedicamento(int iidMedicamento)
        {
            throw new NotImplementedException();
        }

        public int registrarYActualizarMedicamentos(MedicamentoCLS oMedicamentoCLS)
        {
            throw new NotImplementedException();
        }
    }
}
