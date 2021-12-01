using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace DecodeEncode
{
    public static class EncodeDecode
    {
        ///// <summary>
        ///// Encrypts the data ToBase64String
        ///// </summary>
        ///// <param name="data">string to be encrypted</param>
        ///// <returns>Encrypted Data</returns>
        // public readonly static string KEY = ConfigurationManager.AppSettings["EncodeDecodeKey"].ToString();
        // Encrypt a Querystring

       // public readonly static string KEY = ConfigurationManager.AppSettings["EncodeDecodeKey"].ToString();
        public static string Encode(string stringToEncrypt)
        {

            if (stringToEncrypt != null && stringToEncrypt != "")
            {
                byte[] key;
                byte[] IV = { 0x01, 0x12, 0x23, 0x34, 0x45, 0x56, 0x67, 0x78 };
                key = Encoding.UTF8.GetBytes(KEY);

                using (var oDESCrypto = new DESCryptoServiceProvider())
                {
                    byte[] inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);
                    MemoryStream oMemoryStream = new MemoryStream();
                    CryptoStream oCryptoStream = new CryptoStream(oMemoryStream,
                    oDESCrypto.CreateEncryptor(key, IV), CryptoStreamMode.Write);
                    oCryptoStream.Write(inputByteArray, 0, inputByteArray.Length);
                    oCryptoStream.FlushFinalBlock();
                    string clearText = Convert.ToBase64String(oMemoryStream.ToArray()).Replace("/", "@~@");
                    return clearText;
                }
            }
            else
            {
                return stringToEncrypt;
            }
        }
        // Decrypt a Querystring
        public static string Decode(string stringToDecrypt)
        {
            if (stringToDecrypt != null && stringToDecrypt != "" && stringToDecrypt != "0")
            {
                stringToDecrypt = stringToDecrypt.Replace("@~@", "/");
                byte[] key;
                byte[] IV = { 0x01, 0x12, 0x23, 0x34, 0x45, 0x56, 0x67, 0x78 };
                stringToDecrypt = stringToDecrypt.Replace(" ", "+");
                byte[] inputByteArray;
                try
                {

                    key = Encoding.UTF8.GetBytes(KEY);
                    var des = new DESCryptoServiceProvider();
                    inputByteArray = Convert.FromBase64String(stringToDecrypt);
                    MemoryStream ms = new MemoryStream();
                    CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write);
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    Encoding encoding = Encoding.UTF8;
                    return encoding.GetString(ms.ToArray());

                }
                catch (Exception ex)
                {
                    // ErrorLogger.ErrorLog("Utility/EncodeDecode", "Decode", ex.ToString());
                    return stringToDecrypt;
                }
            }
            else if (stringToDecrypt == "0")
            {
                return null;
            }
            else
            {
                return stringToDecrypt;
            }
        }
    }
}
