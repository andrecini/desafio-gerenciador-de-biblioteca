using Desafios.GerenciadorBiblioteca.Domain.Exceptions;
using Desafios.GerenciadorBiblioteca.Service.Security.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace Desafios.GerenciadorBiblioteca.Service.Security
{
    public class CipherService(IConfiguration configuration) : ICipherService
    {
        private readonly string _key = configuration["EncryptionKey"];

        public string Decrypt(string cipherText)    
        {
            byte[] fullCipher = Convert.FromBase64String(cipherText);

            using MemoryStream msDecrypt = new(fullCipher);
            using Aes aesAlg = Aes.Create();
            byte[] iv = new byte[sizeof(int)];
            msDecrypt.Read(iv, 0, iv.Length);
            iv = new byte[BitConverter.ToInt32(iv, 0)];
            msDecrypt.Read(iv, 0, iv.Length);

            aesAlg.Key = Encoding.UTF8.GetBytes(_key);
            aesAlg.IV = iv;

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using CryptoStream csDecrypt = new(msDecrypt, decryptor, CryptoStreamMode.Read);
            using StreamReader srDecrypt = new(csDecrypt);
            return srDecrypt.ReadToEnd();
        }

        public string Encrypt(string plainText)
        {
            using Aes aesAlg = Aes.Create();
            aesAlg.Key = Encoding.UTF8.GetBytes(_key);
            aesAlg.GenerateIV();

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using MemoryStream msEncrypt = new();
            msEncrypt.Write(BitConverter.GetBytes(aesAlg.IV.Length), 0, sizeof(int));
            msEncrypt.Write(aesAlg.IV, 0, aesAlg.IV.Length);
            using (CryptoStream csEncrypt = new(msEncrypt, encryptor, CryptoStreamMode.Write))
            using (StreamWriter swEncrypt = new(csEncrypt))
            {
                swEncrypt.Write(plainText);
            }

            return Convert.ToBase64String(msEncrypt.ToArray());
        }

        public bool ValidatePassword(string password, string userPassword)
        {
            var requestPassword = Decrypt(password);
            var userRequest = Decrypt(userPassword);

            if (requestPassword != userRequest)
                throw new CustomException("Email e/ou Senha Inválidos.", HttpStatusCode.Unauthorized);

            return true;
        }

        public bool ValidatePasswordPolicy(string password)
        {
            if (password.Length < 8 && !password.Any(char.IsSymbol))
                throw new CustomException("Padrão de senha inválido.", HttpStatusCode.UnprocessableEntity);

            return true;
        }
    }
}
