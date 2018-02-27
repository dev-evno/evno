using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC01.AppCode
{
    public class Common
    {

        public static string encrypt(string value, string key)
        {
            System.Security.Cryptography.MACTripleDES mac3des = new System.Security.Cryptography.MACTripleDES();
            System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            mac3des.Key = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(key));
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(value)) + "-" + Convert.ToBase64String(mac3des.ComputeHash(System.Text.Encoding.UTF8.GetBytes(value)));
            //string encryptedQueryString = TamperProofStringEncode(hfOrderId.Value, "F44fggjj");
        }

        private string decrypt(string value, string key)
        {
            string dataValue = "";
            string calcHash = "";
            string storedHash = "";

            System.Security.Cryptography.MACTripleDES mac3des = new System.Security.Cryptography.MACTripleDES();
            System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            mac3des.Key = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(key));

            try
            {
                dataValue = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(value.Split('-')[0]));
                storedHash = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(value.Split('-')[1]));
                calcHash = System.Text.Encoding.UTF8.GetString(mac3des.ComputeHash(System.Text.Encoding.UTF8.GetBytes(dataValue)));

                if (storedHash != calcHash)
                {
                    
                }
            }
            catch (Exception ex)
            {
              
            }

            return dataValue;

        }
    }
}