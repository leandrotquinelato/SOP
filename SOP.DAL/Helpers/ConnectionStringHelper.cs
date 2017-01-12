
using System;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace SOP.DAL.Helpers
{
    /// <summary>
    /// Classe utilitária de geração da connection string
    /// </summary>
    public static class ConnectionStringHelper
    {


        /// <summary>
        /// Método responsável por descriptografar a senha que está contida no web.config
        /// </summary>
        /// <param name="cipherString">Senha criptografada</param>
        /// <param name="useHashing">Parâmetro que indica se a chave é para ser utilizada como Hash</param>
        /// <returns>Senha descriptografada</returns>
        public static string DecryptPassword(string cipherString, bool useHashing, string chaveString = "")
        {
            byte[] keyArray;
            //get the byte code of the string

            byte[] toEncryptArray = Convert.FromBase64String(cipherString);

            System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();
            //Get your key from config file to open the lock!
            if (chaveString.Equals(String.Empty))
            {
                chaveString = (string)settingsReader.GetValue("SecurityKey_TPI", typeof(String));
            }
            if (useHashing)
            {
                //if hashing was used get the hash code with regards to your key
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(chaveString));
                //release any resource held by the MD5CryptoServiceProvider

                hashmd5.Clear();
            }
            else
            {
                //if hashing was not implemented get the byte code of the key
                keyArray = UTF8Encoding.UTF8.GetBytes(chaveString);
            }

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes.
            //We choose ECB(Electronic code Book)

            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(
            toEncryptArray, 0, toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor
            tdes.Clear();
            //return the Clear decrypted TEXT
            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        public static string Descriptografar(string hash,  string chaveHash)
        {
            return Encryption.Encryption.Decrypt(hash, chaveHash);
        }
    }
}
