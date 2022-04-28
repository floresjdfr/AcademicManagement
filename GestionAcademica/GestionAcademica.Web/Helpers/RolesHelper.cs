using GestionAcademica.Models;
using GestionAcademica.Web.Controllers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionAcademica.Web.Helpers
{
    public enum ERole
    {
        ADMINISTRADOR = 1,
        MATRICULADOR = 2,
        PROFESOR = 3,
        ALUMNO = 4
    }

    public static class RolesHelper
    {
        

        public static bool Login(HttpContext context, User user)
        {
            try
            {
                context.Session.SetObjectAsJson("User", user);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool Logout(HttpContext context)
        {
            try
            {
                context.Session.Remove("User");
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool IsLoggedin(HttpContext context)
        {
            try
            {
                var user = context.Session.GetObjectFromJson<User>("User");
                return user != null;
            }
            catch
            {
                return false;
            }
        }
        public static bool IsAuthorized(HttpContext context, ERole[] rolesAllowed)
        {
            try
            {
                if (!IsLoggedin(context)) throw new Exception();

                var user = context.Session.GetObjectFromJson<User>("User");
                var actualRole = (ERole)user.UserType.ID;
                return rolesAllowed.Contains(actualRole);
            }
            catch
            {
                return false;
            }
        }

    }
}

