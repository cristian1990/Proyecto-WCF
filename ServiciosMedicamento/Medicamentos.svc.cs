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
            //Instancio una clase
            MedicamentoCLS oMedicamentoCLS = new MedicamentoCLS();
            
            try
            {
                using (var bd = new MedicoEntities()) //Creo el contexto de acceso a la BD
                {
                    //Busco y almaceno el objeto medicamento encontrado, mediante su ID
                    Medicamento oMedicamento = bd.Medicamento.Where(p => p.IIDMEDICAMENTO == iidMedicamento).First();
                    //Cargo los valores de las prop del objeto
                    oMedicamentoCLS.iidmedicamento = oMedicamento.IIDMEDICAMENTO;
                    oMedicamentoCLS.iidformafarmaceutica = (int)oMedicamento.IIDFORMAFARMACEUTICA;
                    oMedicamentoCLS.nombre = oMedicamento.NOMBRE;
                    oMedicamentoCLS.precio = (decimal)oMedicamento.PRECIO; //Hago el casteo
                    oMedicamentoCLS.stock = (int)oMedicamento.STOCK;
                    oMedicamentoCLS.concentracion = oMedicamento.CONCENTRACION;
                    oMedicamentoCLS.presentacion = oMedicamento.PRESENTACION;
                }
            }
            catch (Exception ex)
            {
                oMedicamentoCLS = null; //Si hay error la lista devuelve null
            }

            //Retorno el medicamento
            return oMedicamentoCLS;
        }

        public int registrarYActualizarMedicamentos(MedicamentoCLS oMedicamentoCLS)
        {
            int rpta = 0;
            try
            {
                using (var bd = new MedicoEntities()) //Creo el contexto de acceso a la BD
                {
                    //Registar
                    if (oMedicamentoCLS.iidmedicamento == 0)
                    {
                        //Creo una nueva instancia de Medicamento y cargo el valor de las propiedades del objeto MedicamentoCLS recibido
                        Medicamento omedicamento = new Medicamento();
                        omedicamento.IIDMEDICAMENTO = oMedicamentoCLS.iidmedicamento;
                        omedicamento.NOMBRE = oMedicamentoCLS.nombre;
                        omedicamento.PRECIO = oMedicamentoCLS.precio;
                        omedicamento.STOCK = oMedicamentoCLS.stock;
                        omedicamento.IIDFORMAFARMACEUTICA = oMedicamentoCLS.iidformafarmaceutica;
                        omedicamento.CONCENTRACION = oMedicamentoCLS.concentracion;
                        omedicamento.PRESENTACION = oMedicamentoCLS.presentacion;
                        omedicamento.BHABILITADO = 1;

                        bd.Medicamento.Add(omedicamento); //Indico que quiero agregar
                        bd.SaveChanges(); //Agrego a la base de datos
                        rpta = 1;
                    }
                    else //Editar (si el ID no es )
                    {
                        //Creo una nueva instancia de Medicamento y busco por id el medicamente mediante el objeto recibido
                        //despues cargo el valor de las propiedades del objeto MedicamentoCLS recibido
                        Medicamento oMedicamento = bd.Medicamento.Where(p => p.IIDMEDICAMENTO == oMedicamentoCLS.iidmedicamento).First();
                        oMedicamento.IIDMEDICAMENTO = oMedicamentoCLS.iidmedicamento;
                        oMedicamento.NOMBRE = oMedicamentoCLS.nombre;
                        oMedicamento.PRECIO = oMedicamentoCLS.precio;
                        oMedicamento.STOCK = oMedicamentoCLS.stock;
                        oMedicamento.IIDFORMAFARMACEUTICA = oMedicamentoCLS.iidformafarmaceutica;
                        oMedicamento.CONCENTRACION = oMedicamentoCLS.concentracion;
                        oMedicamento.PRESENTACION = oMedicamentoCLS.presentacion;

                        bd.SaveChanges(); //Guardo los cambios en la BD
                        rpta = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                rpta = 0;
            }

            //Retorno la rpta, contiene 0 o 1
            return rpta;
        }
    }
}
