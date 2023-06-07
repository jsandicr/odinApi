using OdinApi.Controllers;
using OdinApi.Models.Data.Interfaces;
using OdinApi.Models.Obj;
using System.Security.Claims;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace OdinApi.Models.Data.Classes
{
    public class UserModel : IUserModel
    {

        private readonly OdinContext _context;
        private readonly EmailController _emailController;

        public UserModel(OdinContext context, EmailController emailController)
        {
            _context = context;
            _emailController = emailController;
        }

        public User GetUserById(int id)
        {
            try
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
            catch (Exception)
            {
                return new User();
            }
        }

        public User GetUserByMail(string mail)
        {
            try
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
            catch (Exception)
            {
                return new User();
            }
        }

        public List<User> GetUsers()
        {
            try
            {
                var query = (from u in _context.User
                             join r in _context.Rol
                             on u.idRol equals r.id
                             join b in _context.Branch
                             on u.idBranch equals b.id
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
            catch (Exception)
            {
                return new List<User>();
            }
        }

        public User PostUser(User user)
        {
            try
            {
                _context.User.Add(user);
                _context.SaveChanges();
                return user;
            }
            catch (Exception)
            {
                return new User();
            }
        }

        public User DeleteUser(int id)
        {
            try
            {
                User user = _context.User.Find(id);
                if (user != null)
                {
                    _context.Remove(user);
                    _context.SaveChanges();
                    return user;
                }
                else
                {
                    return new User();
                }
                
            }
            catch (Exception)
            {
                return new User();
            }
        }

        public User PutUser(User user)
        {
            try
            {
                _context.Update(user);
                _context.SaveChanges();
                return user;
            }
            catch (Exception)
            {
                return new User();
            }
        }

        public User Login(UserDTO userDTO)
        {
            try
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
            catch (Exception)
            {
                return new User();
            }
        }

        public User RestorePasword(RestorePassword user)
        {
            try
            {
                var query = (from u in _context.User
                             join r in _context.Rol on u.idRol equals r.id
                             join b in _context.Branch on u.idBranch equals b.id
                             where u.mail == user.mail && u.phone == user.phone
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
                            .button {
                                display: inline-block;
                                background-color: #DD6B4D;
                                color: #ffffff;
                                padding: 12px 24px;
                                font-size: 18px;
                                text-decoration: none;
                                border-radius: 5px;
                                margin-top: 30px;
                                text-align: center;
                            }
                            .button:hover {
                                background-color: #C54E3D;
                            }
                        </style>
                    </head>
                    <body>
                        <div class=""container"">
                            <h1>Restablecimiento de contraseña</h1>
                            <p>Estimado/a " + query.User.name + @",</p>
                            <p>Su contraseña ha sido restablecida exitosamente. A continuación, encontrará los detalles de su nueva contraseña:</p>
                            <p class=""password"">" + newPassword + @"</p>
                            <p>Por motivos de seguridad, le recomendamos cambiar su contraseña después de iniciar sesión.</p>
                            <p>
                                Para iniciar sesión, haga clic en el siguiente botón:
                                <br>
                                <a class=""button"" href=""https://example.com/login"">Iniciar sesión</a>
                            </p>
                        </div>
                    </body>
                    </html>";
                    mail.Body = body;

                    _emailController.SendEmail(mail);
                    // Asigna la nueva contraseña al usuario
                    query.User.password = newPassword;

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
            catch (Exception)
            {
                // Devuelve un objeto User nulo si ocurrió un error
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
    }
}
