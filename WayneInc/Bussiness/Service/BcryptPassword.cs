using Microsoft.AspNetCore.Identity;

namespace WayneInc.Bussiness.Service
{
    public class BcryptPassword : IPasswordHasher
    {
        //Encripta la contraseña
        public string Hash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        //Verifica la contraseña
        public bool Verify(string password, string passwordHasher)
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHasher);
        }
    }
}
