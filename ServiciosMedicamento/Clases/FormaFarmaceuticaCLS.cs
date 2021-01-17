using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosMedicamento.Clases
{
    //Esta clase y sus propiedades no se exponen en el Web Service, por lo cual 
    //no agrego las anotaciones DataContract y DataMember
    public class FormaFarmaceuticaCLS
    {
        public string nombreFormaFarmaceutica { get; set; }
        public int iidformafarmaceutica { get; set; }
    }
}