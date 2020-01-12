using System;
using System.Text.RegularExpressions;

namespace Codenation.Challenge
{
    public class CesarCypher : ICrypt, IDecrypt
    {
        public string Crypt(string message)
        { 
            if (message == string.Empty)
            {
                return string.Empty;
            }
            if (message == null)
            {
                throw new ArgumentNullException();
            }
            var socaracterregular = Regex.IsMatch(message, @"^[a-zA-Z0-9 ]*$");
            if (!socaracterregular)
            {
                throw new ArgumentOutOfRangeException();
            }
          
            var msg = message.ToLower();
            var resultado = string.Empty;
            foreach (var simbolo in msg)
            {
                var codigo = !char.IsLetter(simbolo)
                    ? simbolo
                    : (char)((((simbolo + 3) - 'a') % 26) + 'a');
                resultado += codigo;

            }
 
            return resultado;
        }

        public string Decrypt(string message)
        {
            if (message == string.Empty)
            {
                return string.Empty;
            }
            if (message == null)
            {
                throw new ArgumentNullException();
            }
            var socaracterregular = Regex.IsMatch(message, @"^[a-zA-Z0-9 ]*$");
            if (!socaracterregular)
            {
                throw new ArgumentOutOfRangeException();
            }

            var msg = message.ToLower();
            var resultado = string.Empty;
            foreach (var simbolo in msg)
            {
                var codigo = !char.IsLetter(simbolo)
                    ? simbolo
                    : (char)((((simbolo + 23) - 'a') % 26) + 'a');
                resultado += codigo;
            }

            return resultado;
        }
    }
}
