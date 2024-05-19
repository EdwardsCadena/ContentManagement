using Microsoft.Extensions.Options;
using Proyecto.Infrastructure.Interfaces;
using Proyecto.Infrastructure.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Infrastructure.Service
{
    public class PasswordService : IPasswordService
    {
        // Aquí se define una variable privada para las opciones de la contraseña
        private readonly PasswordOptions _options;

        // Este es el constructor del servicio, donde las opciones de la contraseña son inyectadas
        public PasswordService(IOptions<PasswordOptions> options)
        {
            // Las opciones de la contraseña inyectadas se asignan a la variable privada para ser usadas en los métodos del servicio
            _options = options.Value;
        }

        // Este método comprueba si una contraseña coincide con un hash dado
        public bool Check(string hash, string password)
        {
            // Aquí se divide el hash en sus partes componentes
            var parts = hash.Split('.');
            if (parts.Length != 3)
            {
                throw new FormatException("Unexpected hash format");
            }

            // Aquí se extraen las partes del hash
            var iterations = Convert.ToInt32(parts[0]);
            var salt = Convert.FromBase64String(parts[1]);
            var key = Convert.FromBase64String(parts[2]);

            // Aquí se genera un nuevo hash para la contraseña proporcionada usando el mismo salt y número de iteraciones
            using (var algorithm = new Rfc2898DeriveBytes(password, salt, iterations))
            {
                var keyToCheck = algorithm.GetBytes(_options.KeySize);

                // Aquí se comprueba si el hash generado coincide con el hash original
                return keyToCheck.SequenceEqual(key);
            }
        }

        // Este método genera un hash para una contraseña dada
        public string Hash(string password)
        {
            // Aquí se genera un nuevo salt y se usa para hashear la contraseña
            using (var algorithm = new Rfc2898DeriveBytes(password, _options.SaltSize, _options.Iterations))
            {
                var key = Convert.ToBase64String(algorithm.GetBytes(_options.KeySize));
                var salt = Convert.ToBase64String(algorithm.Salt);

                // Aquí se devuelve el hash en un formato que incluye el número de iteraciones, el salt y el hash de la contraseña
                return $"{_options.Iterations}.{salt}.{key}";
            }
        }
    }
}