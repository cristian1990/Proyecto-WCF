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
            //Creo una lista 
            List<FormaFarmaceuticaCLS> listaFormaFarmaceutica = new List<FormaFarmaceuticaCLS>();

            try
            {
                using (var bd = new MedicoEntities()) //Creo una instancia del contexto
                {
                    //Listo el listado de tipos de medicamentos, que esten habilitados (1)
                    listaFormaFarmaceutica = bd.FormaFarmaceutica.Where(p => p.BHABILITADO == 1)
                        //Selecciono algunos campos
                        .Select(p => new FormaFarmaceuticaCLS //Retornara un objeto del tipo FormaFarmaceuticaCLS
                        {
                            //Asigno los valores a las prop de la clase FormaFarmaceuticaCLS
                            iidformafarmaceutica = p.IIDFORMAFARMACEUTICA,
                            nombreFormaFarmaceutica = p.NOMBRE

                        }).ToList(); //Ejecuto la consulta
                }
            }
            catch (Exception ex)
            {
                listaFormaFarmaceutica = null; //Si hay error la lista devuelve null

            }

            //Retorno el listado
            return listaFormaFarmaceutica;
        }

        public List<MedicamentoCLS> listarMedicamentos()
        {
            //Creo una lista
            List<MedicamentoCLS> listaMedicamento = new List<MedicamentoCLS>();

            try
            {
                using (var bd = new MedicoEntities()) //Creo el contexto de acceso a la BD
                {
                    listaMedicamento = (from medicamento in bd.Medicamento
                                        join formafarmaceutica in bd.FormaFarmaceutica //Hago un Join
                                        on medicamento.IIDFORMAFARMACEUTICA equals
                                        formafarmaceutica.IIDFORMAFARMACEUTICA
                                        //Selecciono algunos campos
                                        select new MedicamentoCLS //Retornara un objeto del tipo MedicamentoCLS
                                        {
                                            //Asigno los valores a las prop de la clase MedicamentoCLS
                                            iidmedicamento = medicamento.IIDMEDICAMENTO,
                                            nombre = medicamento.NOMBRE,
                                            precio = (decimal)medicamento.PRECIO, //Hago el casteo
                                            nombreFormaFarmaceutica = formafarmaceutica.NOMBRE, //Obtengo el nombre del tipo (Join)
                                            concentracion = medicamento.CONCENTRACION,
                                            presentacion = medicamento.PRESENTACION,
                                            stock = (int)medicamento.STOCK,
                                            bhabilitado = (int)medicamento.BHABILITADO
                                        }).ToList(); //Ejecuto la consulta

                }

            }
            catch (Exception ex)
            {
                listaMedicamento = null; //Si hay error la lista devuelve null

            }

            //Retorno el listado de Medcamentos
            return listaMedicamento;
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
