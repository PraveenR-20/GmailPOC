using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Gmail_POC.Data.Common
{
    public static partial class Encryption
    {

        #region Encrypt Secrets
        /// <summary>
        /// Method used for encrypt secrets
        /// </summary>
        /// <param name="inputText"></param>
        /// <param name="encryptionKey"></param>
        /// <returns></returns>
        public static string EncryptSecrets(string inputText, string encryptionKey)
        {
            string encyptKey = encryptionKey;
            byte[] clearBytes = Encoding.Unicode.GetBytes(inputText);
            using (Aes aes_encrypter = Aes.Create())
            {
                Rfc2898DeriveBytes  pdb_bytes= new
                Rfc2898DeriveBytes(encyptKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65,0x64, 0x65, 0x76
            });
                aes_encrypter.Key = pdb_bytes.GetBytes(32);
                aes_encrypter.IV = pdb_bytes.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes_encrypter.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    inputText = Convert.ToBase64String(ms.ToArray());
                }               
            }            
            return inputText;
        }
        #endregion

        #region Decrypt Secrets
        /// <summary>
        /// Method used for decrypt secrets
        /// </summary>
        /// <param name="inputText"></param>
        /// <param name="encryptionKey"></param>
        /// <returns></returns>
        public static string DecryptSecrets(string inputText, string encryptionKey)
        {
            string encyptKey = encryptionKey;
            inputText = inputText.Replace(" ", "+");
            byte[] clearBytes = Convert.FromBase64String(inputText);
            using (Aes aes_encrypter = Aes.Create())
            {
                Rfc2898DeriveBytes pdb_bytes = new Rfc2898DeriveBytes(encyptKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65,0x64, 0x65, 0x76
            });
                aes_encrypter.Key = pdb_bytes.GetBytes(32);
                aes_encrypter.IV = pdb_bytes.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes_encrypter.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    inputText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return inputText;
        }
        #endregion
    }

}