using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Analyze.DesktopApp.Common
{
    public class Security
    {
        public static string HMACSHA256Hash(string text)
        {
            var key = "NY2023@";
            // change according to your needs, an UTF8Encoding
            // could be more suitable in certain situations
            var encoding = Encoding.UTF8;

            byte[] textBytes = encoding.GetBytes(text);
            byte[] keyBytes = encoding.GetBytes(key);

            byte[] hashBytes;

            using (HMACSHA256 hash = new HMACSHA256(keyBytes))
                hashBytes = hash.ComputeHash(textBytes);

            return Convert.ToBase64String(hashBytes);
        }

        public static void MesSuccess(string mes = "", string title = "")
        {
            var mesContent = "Đã lưu dữ liệu!";
            var mesTitle = "Phản hồi";
            if (!string.IsNullOrWhiteSpace(mes))
            {
                mesContent = mes;
            }
            if(!string.IsNullOrWhiteSpace(title))
            {
                mesTitle = title;
            }
            MessageBox.Show(mesContent, mesTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void MesError(string mes = "", string title = "")
        {
            var mesContent = "Lỗi không xác định!";
            var mesTitle = "Cảnh báo";
            if (!string.IsNullOrWhiteSpace(mes))
            {
                mesContent = mes;
            }
            if (!string.IsNullOrWhiteSpace(title))
            {
                mesTitle = title;
            }
            MessageBox.Show(mesContent, mesTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        //public static string MD5Hash(string input)
        //{
        //    using (MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider())
        //    {
        //        StringBuilder hash = new StringBuilder();
        //        byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));
        //        for (int i = 0; i < bytes.Length; i++)
        //        {
        //            hash.Append(bytes[i].ToString("x2"));
        //        }
        //        return hash.ToString();
        //    }
        //}

        //public static string Encrypt(string plainText)
        //{
        //    byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

        //    byte[] keyBytes = new Rfc2898DeriveBytes(ConstantValue.PasswordHash, Encoding.ASCII.GetBytes(ConstantValue.SaltKey)).GetBytes(256 / 8);
        //    var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
        //    var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(ConstantValue.VIKey));

        //    byte[] cipherTextBytes;

        //    using (var memoryStream = new MemoryStream())
        //    {
        //        using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
        //        {
        //            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
        //            cryptoStream.FlushFinalBlock();
        //            cipherTextBytes = memoryStream.ToArray();
        //            cryptoStream.Close();
        //        }
        //        memoryStream.Close();
        //    }
        //    return Convert.ToBase64String(cipherTextBytes);
        //}

        //public static string Decrypt(string encryptedText)
        //{
        //    try
        //    {
        //        byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);
        //        byte[] keyBytes = new Rfc2898DeriveBytes(ConstantValue.PasswordHash, Encoding.ASCII.GetBytes(ConstantValue.SaltKey)).GetBytes(256 / 8);
        //        var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };

        //        var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(ConstantValue.VIKey));
        //        var memoryStream = new MemoryStream(cipherTextBytes);
        //        var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
        //        byte[] plainTextBytes = new byte[cipherTextBytes.Length];

        //        int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
        //        memoryStream.Close();
        //        cryptoStream.Close();
        //        return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
        //    }
        //    catch(Exception ex)
        //    {
        //        NLogLogger.PublishException(ex, $"Security:Decrypt: {ex.Message}");
        //        return null;
        //    }
        //}
    }
}
