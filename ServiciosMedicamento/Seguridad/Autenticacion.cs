using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace ServiciosMedicamento.Seguridad
{
    //UserNamePasswordValidator: Sirve para validar que los autenticados solo puedan acceder a los servicios
    public class Autenticacion : UserNamePasswordValidator
    {
        //  Sobreescribimos el metodo Validate, necesario por heredar de UserNamePasswordValidator
        public override void Validate(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentException("El usuario no puede ser nulo o vacio");
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("El password no puede ser nulo o vacio");
            }
            //Usuario: crismo | Contraseña: 1234
            if (!(userName.ToLower().Equals("crismo") && password.ToLower().Equals("1234")))
            {
                throw new FaultException("Usuario o contraseña incorrecto");
            }
        }
    }
}