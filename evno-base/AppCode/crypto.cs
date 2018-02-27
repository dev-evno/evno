using System;

namespace MVC01.Crypto
{
    public class crypto
    {

        public static string encrypt(string value, string key)
        {
            System.Security.Cryptography.MACTripleDES mac3des = new System.Security.Cryptography.MACTripleDES();
            System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            mac3des.Key = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(key));
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(value)) + "-" + Convert.ToBase64String(mac3des.ComputeHash(System.Text.Encoding.UTF8.GetBytes(value)));
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
<<<<<<< HEAD:evno-base/AppCode/Common.cs
                    
=======
>>>>>>> 3c3aeb15f248076a875d72881270dacd032327d9:evno-base/AppCode/crypto.cs
                }
            }
            catch (Exception ex)
            {
<<<<<<< HEAD:evno-base/AppCode/Common.cs
              
=======
>>>>>>> 3c3aeb15f248076a875d72881270dacd032327d9:evno-base/AppCode/crypto.cs
            }

            return dataValue;

        }

        public string mydemo()
        {
            return "hey whatsup";
        }
    }
}