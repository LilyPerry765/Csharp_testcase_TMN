using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Enterprise;
using System;

namespace TMN
{
    public class Cryptographer
    {
        #region Constructors Memebers
        static Cryptographer()
        {
            RijndaelManaged algorithm = new RijndaelManaged();

            byte[] cryptoKey = { 1, 2, 1, 4, 5, 6, 7, 8, 9, 0, 
        11, 2, 3, 4, 5, 6 };
            byte[] cryptoIV = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 
        11, 2, 3, 4, 5, 6 };

            encrytor = algorithm.CreateEncryptor(cryptoKey, cryptoIV);
            decrytor = algorithm.CreateDecryptor(cryptoKey, cryptoIV);
        }
        #endregion

        #region Private Fields
        private static readonly ICryptoTransform encrytor;
        private static readonly ICryptoTransform decrytor;
        #endregion

        #region Public Methods

        public static string Encode(string clearString)
        {
            try
            {
                if (clearString == string.Empty)
                {
                    return string.Empty;
                }

                //Converts string to byte array 
                byte[] toEncrypt = Encoding.UTF8.GetBytes(clearString);

                //Creates encryption stream 
                MemoryStream memStream = new MemoryStream();
                CryptoStream encrytorStream = new CryptoStream(memStream, encrytor, CryptoStreamMode.Write);

                //Write data to streamer 
                encrytorStream.Write(toEncrypt, 0, toEncrypt.Length);
                encrytorStream.FlushFinalBlock();

                //Read encoded data from streamer 
                byte[] encrypted = memStream.ToArray();
                return Convert.ToBase64String(encrypted);
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
                return null;
            }
        }

        public static string Encode(byte[] buffer)
        {
            try
            {
                //Creates encryption stream 
                MemoryStream memStream = new MemoryStream();
                CryptoStream encrytorStream = new CryptoStream(memStream, encrytor, CryptoStreamMode.Write);

                //Write data to streamer 
                encrytorStream.Write(buffer, 0, buffer.Length);
                encrytorStream.FlushFinalBlock();

                //Read encoded data from streamer 
                byte[] encrypted = memStream.ToArray();
                return Convert.ToBase64String(encrypted);
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
                return null;
            }
        }

        public static object Decode(byte[] codedStream)
        {
            try
            {
                //Creates decryption stream 
                MemoryStream memStream = new MemoryStream(codedStream);
                CryptoStream decrytorStream = new CryptoStream(memStream, decrytor, CryptoStreamMode.Read);

                IFormatter formatter = new BinaryFormatter();
                return formatter.Deserialize(decrytorStream);
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
                return null;
            }

        }

        public static byte[] Encode(object instance)
        {
            try
            {
                //Creates encryption stream 
                MemoryStream memStream = new MemoryStream();
                CryptoStream encrytorStream = new CryptoStream(memStream, encrytor, CryptoStreamMode.Write);

                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(encrytorStream, instance);

                encrytorStream.Close();
                memStream.Close();

                return memStream.ToArray();
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
                return null;
            }
        }

        public static string Decode(string codedString)
        {
            try
            {
                if (codedString == string.Empty)
                {
                    return string.Empty;
                }

                //Converts string to byte array 
                byte[] toDecrypt = Convert.FromBase64String(codedString);

                //Creates decryption stream 
                MemoryStream memStream = new MemoryStream(toDecrypt);
                CryptoStream decrytorStream = new CryptoStream(memStream, decrytor, CryptoStreamMode.Read);

                //Reads decoded string 
                StreamReader streamReader = new StreamReader(decrytorStream);
                return streamReader.ReadToEnd();
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
                return null;
            }
        }

        public static string ComputeMd5Hash(string clearString)
        {
            MD5CryptoServiceProvider svc = new MD5CryptoServiceProvider();
            byte[] codedStream = svc.ComputeHash(Encoding.UTF8.GetBytes(clearString));
            return Convert.ToBase64String(codedStream);
        }

        public static byte[] ComputeMd5Hash(byte[] clearStream)
        {
            MD5CryptoServiceProvider svc = new MD5CryptoServiceProvider();
            return svc.ComputeHash(clearStream);
        }
        #endregion
    }
}
