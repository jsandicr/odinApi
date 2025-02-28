﻿using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using OdinApi.Models.Data.Interfaces;
using OdinApi.Models.Obj;
using System.Text;

namespace OdinApi.Models.Data.Classes
{
    public class UserModel : IUserModel
    {

        private readonly OdinContext _context;
        private readonly IEmailService _email;

        public UserModel(OdinContext context, IEmailService email)
        {
            _context = context;
            _email = email;
        }

        public User GetUserById(int id)
        {
            var query = (from u in _context.User
                            join r in _context.Rol
                            on u.idRol equals r.id
                            join b in _context.Branch
                            on u.idBranch equals b.id
                            where u.id == id
                            select new { User = u, Rol = r, Branch = b }).ToList();

            if (query.Count > 0)
            {
                return query.FirstOrDefault().User;
            }
            else
            {
                return new User();
            }
        }

        public User GetUserByMail(string mail)
        {
            var query = (from u in _context.User
                            where u.mail == mail
                            select u).FirstOrDefault();

            if (query != null)
            {
                return query;
            }
            else
            {
                return new User();
            }
        }

        public List<User> GetUsers()
        {
            var query = (from u in _context.User
                            join r in _context.Rol
                            on u.idRol equals r.id
                            join b in _context.Branch
                            on u.idBranch equals b.id
                            where u.active == true
                            select new { User = u, Rol = r, Branch = b }).ToList();

            if (query != null)
            {
                List<User> users = new List<User>();
                foreach (var q in query)
                {
                    users.Add(q.User);
                }
                return users;
            }
            else
            {
                return new List<User>();
            }
        }

        public List<User> GetClients()
        {
            var query = (from u in _context.User
                            join r in _context.Rol
                            on u.idRol equals r.id
                            join b in _context.Branch
                            on u.idBranch equals b.id
                            where r.name == "Cliente"
                            select new { User = u, Rol = r, Branch = b }).ToList();

            if (query != null)
            {
                List<User> users = new List<User>();
                foreach (var q in query)
                {
                    users.Add(q.User);
                }
                return users;
            }
            else
            {
                return new List<User>();
            }
        }

        public List<User> GetSupervisors()
        {
            var query = (from u in _context.User
                            join r in _context.Rol
                            on u.idRol equals r.id
                            join b in _context.Branch
                            on u.idBranch equals b.id
                            where r.name == "Supervisor"
                            select new { User = u, Rol = r, Branch = b }).ToList();

            if (query != null)
            {
                List<User> users = new List<User>();
                foreach (var q in query)
                {
                    users.Add(q.User);
                }
                return users;
            }
            else
            {
                return new List<User>();
            }
        }

        public User PostUser(User user)
        {
            if (user.password == "")
            {
                user.password = GeneratePassword();
            }
            else
            {
                user.restorePass = false;
            }
            user.password = HashPassword(user.password);

            _context.User.Add(user);
            _context.SaveChanges();
            if (user.restorePass)
            {
                _email.SendUser(user, user.password);
            }
            return user;
        }

        public User DeleteUser(int id)
        {
            User user = _context.User.Find(id);
            if (user != null)
            {
                if (user.active)
                {
                    user.active = false;
                }
                else
                {
                    user.active = true;
                }
                _context.Update(user);
                _context.SaveChanges();
                return user;
            }
            else
            {
                return new User();
            }
        }

        public User PutUser(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            _context.Entry(user).Property(u => u.idBranch).IsModified = true;
  
            _context.SaveChanges();
            return user;
        }

        public User Login(UserDTO userDTO)
        {
            var query = (from u in _context.User
                            join r in _context.Rol
                            on u.idRol equals r.id
                            join b in _context.Branch
                            on u.idBranch equals b.id into branch_join
                            from b in branch_join.DefaultIfEmpty()
                            where u.mail == userDTO.mail && u.password == userDTO.password
                            select new { User = u, Rol = r, Branch = b }).FirstOrDefault();

            if (query != null)
            {
                return query.User;
            }
            else
            {
                return new User();
            }
        }

        public User RestorePasword(RestorePassword user)
        {
            var query = (from u in _context.User
                            join r in _context.Rol on u.idRol equals r.id
                            join b in _context.Branch on u.idBranch equals b.id into branch_join
                            from b in branch_join
                            where u.mail == user.mail && u.phone == user.phone && b != null
                            select new { User = u, Rol = r, Branch = b }).FirstOrDefault();

            if (query != null)
            {
                // Supongamos que tienes un servicio de correo electrónico llamado EmailService
                var mail = new Email();
                mail.To = query.User.mail;
                mail.Subject = "Se ha restablecido su contraseña";

                    // Genera la contraseña utilizando el método GeneratePassword
                    var newPassword = GeneratePassword();
                    // Crea el HTML del cuerpo del correo con estilos CSS
                    var body = @"
                    <html>
                    <head>
                        <style>
                            body {
                                font-family: Arial, sans-serif;
                                background-color: #f2f2f2;
                                padding: 20px;
                            }
                            .container {
                                max-width: 500px;
                                margin: 0 auto;
                                background-color: #ffffff;
                                border-radius: 10px;
                                padding: 40px;
                                box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
                            }
                            h1 {
                                color: #DD6B4D;
                                font-size: 24px;
                                text-align: center;
                                margin-top: 0;
                            }
                            p {
                                font-size: 16px;
                                color: #333333;
                                margin-top: 20px;
                            }
                            .password {
                                font-size: 32px;
                                color: #DD6B4D;
                                text-align: center;
                                margin-top: 20px;
                            }
                            
                            
                        </style>
                    </head>
                    <body>
                        <div class=""container"">
                            <h1>Restablecimiento de contraseña</h1>
                            <p>Estimado/a " + query.User.name + @",</p>
                            <p>Su contraseña ha sido restablecida exitosamente. A continuación, encontrará los detalles de su nueva contraseña:</p>
                            <p class=""password"">" + newPassword + @"</p>                            
                        </div>
                    </body>
                    </html>";
                    mail.Body = body;

                    _email.SendEmail(mail);
                    // Asigna la nueva contraseña al usuario

                var EnPassword = HashPassword(newPassword);
                query.User.password = EnPassword;
                query.User.restorePass = true;

                // Guarda los cambios en la base de datos (suponiendo que estás usando Entity Framework)
                _context.SaveChanges();

                // Devuelve el objeto User si el correo se envió correctamente
                return query.User;
            }
            else
            {
                // Devuelve un objeto User nulo si el correo no existe
                return null;
            }
        }

        private static readonly Random random = new Random();
        private const string Characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public static string GeneratePassword()
        {
            var password = new StringBuilder();

            for (int i = 0; i < 8; i++)
            {
                var character = Characters[random.Next(Characters.Length)];
                password.Append(character);
            }

            return password.ToString();
        }

        public string HashPassword(string password)
        {
            byte[] fixedSalt = new byte[128 / 8];
            //Se utiliza un valor fijo temporalmente
            var salt = Encoding.UTF8.GetBytes("1234567890abcdef");

            // Generar el hash de la contraseña utilizando PBKDF2
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 10000,
                numBytesRequested: 256 / 8
            ));

            return hashed;
        }

        public User ChangePassword(ChangePassword user)
        {
            var query = (from u in _context.User
                            join r in _context.Rol on u.idRol equals r.id
                            join b in _context.Branch on u.idBranch equals b.id into branch_join
                            from b in branch_join.DefaultIfEmpty()
                            where u.id == user.id
                            select new { User = u, Rol = r, Branch = b }).FirstOrDefault();
            var OldPassword = HashPassword(user.oldPassword);

            if (query != null)
            {
                if (query.User.password == OldPassword)
                {
                    var NewPassword = HashPassword(user.password);
                    query.User.password = NewPassword;
                    query.User.restorePass = false;


                    _context.SaveChanges();

                    return query.User;
                }
                return null;
            }
            else
            {
                return null;
            }
        }

        public async Task<User> GetSupervisorSucursal(int id)
        {
            var user = _context.User.First(u => u.id == id);
            var supervisor = _context.User.Join(
                            _context.Rol,
                            u => u.idRol,
                            r => r.id,
                            (u, r) => new { User = u, Rol = r }
                        )
                        .FirstOrDefault(ur => ur.User.idBranch == user.idBranch && ur.Rol.name == "Supervisor");
            if (supervisor != null)
            {
                return supervisor.User;
            }
            return null;
        }
    }
}