using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ServiciosMedicamento.Clases
{
    [DataContract]
    public class MedicamentoCLS
    {
        //Ordeno las propiedades utilizando Order, asi hace por orden alfabetico
        [DataMember(Order = 0)] //Serializa esta prop primero
        public int iidmedicamento { get; set; }
        [DataMember(Order = 1)]
        public string nombre { get; set; }
        [DataMember(Order = 2)]
        public string concentracion { get; set; }
        [DataMember(Order = 3)]
        public int iidformafarmaceutica { get; set; }
        [DataMember(Order = 4)]
        public string nombreFormaFarmaceutica { get; set; } //Prop agregada para mostrar el nombre y no el ID
        [DataMember(Order = 5)]
        public decimal precio { get; set; }
        [DataMember(Order = 6)]
        public int stock { get; set; }
        [DataMember(Order = 7)]
        public string presentacion { get; set; }
        [DataMember(Order = 8)]
        public int bhabilitado { get; set; }
    }
}