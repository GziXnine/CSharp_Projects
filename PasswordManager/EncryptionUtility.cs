using System.Text;

namespace PasswordManager
{
    internal class EncryptionUtility
    {
        private static readonly string originalKey = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvxyz123456789~!@#$%^&*()_-+=";
        private static readonly string alterKey    = "aL#mfWxT%3s!J-*CygFGUO@=$(jzBAKrb4Nc+_uikEV8pt^ReMnP15d2DhZvolqH~9IQYS&)67X";

        public static string Encrypt(string password)
        {
            StringBuilder encryptedPassword = new StringBuilder();
            foreach (var item in password)
                encryptedPassword.Append(alterKey[originalKey.IndexOf(item)]);

            return encryptedPassword.ToString();
        }

        public static string Decrypt(string password)
        {
            StringBuilder encryptedPassword = new StringBuilder();
            foreach (var item in password)
                encryptedPassword.Append(originalKey[alterKey.IndexOf(item)]);

            return encryptedPassword.ToString();
        }
    }
}
