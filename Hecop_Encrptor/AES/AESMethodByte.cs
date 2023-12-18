using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Hecop_Encryptor.AES
{
    internal class AESMethodByte
    {
        static int saltBytes
        {
            get
            {
                return Convert.ToInt32(GenerateRandomSalt());
            }
        }

        public static byte[] GenerateRandomSalt()
        {
            byte[] data = new byte[32];

            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                for (int i = 0; i < 10; i++)
                {
                    rng.GetBytes(data);
                }
            }

            return data;
        }

        public static byte[] EncryptBytesAES(byte[] data, string password)
        {

            byte[] encryptedData;

            using (Aes aes = Aes.Create())
            {
                byte[] key = new Rfc2898DeriveBytes(password, saltBytes).GetBytes(32);
                aes.Key = key;
                aes.IV = new byte[16];
                aes.KeySize = 256; aes.BlockSize = 128;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        cs.Write(data, 0, data.Length);
                    }

                    encryptedData = ms.ToArray();
                }
            }

            return encryptedData;
        }

        public static byte[] DecryptBytesAES(byte[] encryptedData, string password)
        {
            byte[] decryptedText;

            using (Aes aes = Aes.Create())
            {
                byte[] key = new Rfc2898DeriveBytes(password, saltBytes).GetBytes(32);
                aes.Key = key;
                aes.IV = new byte[16];
                aes.KeySize = 256; aes.BlockSize = 128;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream(encryptedData))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader reader = new StreamReader(cs))
                        {
                            decryptedText = Encoding.UTF8.GetBytes(reader.ReadToEnd());
                        }
                    }
                }
            }

            return decryptedText;
        }
    }
}
