using System;

namespace ec.edu.monster.servicio
{
    public class AuthService
    {
        private const string ValidUser = "MONSTER";
        private const string ValidPassword = "MONSTER69";

        public bool ValidarCredenciales(string usuario, string contrasena)
        {
            var user = (usuario ?? string.Empty).Trim();
            var pass = contrasena ?? string.Empty;

            return string.Equals(user, ValidUser, StringComparison.OrdinalIgnoreCase) &&
                   string.Equals(pass, ValidPassword, StringComparison.Ordinal);
        }
    }
}
