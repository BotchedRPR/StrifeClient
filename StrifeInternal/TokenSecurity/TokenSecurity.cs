using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace StrifeClient.StrifeInternal.TokenSecurity
{
    internal class TokenSecurity
    {
        public static bool SaveTokenWithPassword(string token)
        {
            PasswordInputWindow piw = new();
            piw.ShowDialog();
            Logger.LogDebug("Hashing. Please wait...");
            string hash;
            if (piw.password == null)
            {
                Logger.LogError("Password was null?");
            }
#pragma warning disable CS8604 // Possible null reference argument.
            hash = GetSHA512(piw.password);
#pragma warning restore CS8604 // Possible null reference argument.
            Logger.LogDebug("Hash complete.");
            Logger.LogDebug("Saving Token to file...");
            try
            {
                File.WriteAllTextAsync(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\StrifeClient\password.hash", hash);
            }
            catch (Exception ex)
            {
                Logger.LogError("Something went wrong while saving the password hash.\nException " + ex.Message);
                return false;
            }
            Logger.LogSuccess("Password hash stored. Encrypting Token.");
            try
            {
                File.WriteAllTextAsync(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\StrifeClient\token.raw", token);
            }
            catch (Exception ex)
            {
                Logger.LogError("Something went wrong while saving the token.\nException " + ex.Message);
                return false;
            }
            Logger.LogSuccess("Token saved. Encrypting file with hashed password.");
            EncryptFile(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\StrifeClient\token.raw", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\StrifeClient\token.enc", hash, salt, iterations);
            File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\StrifeClient\token.raw");
            Logger.LogSuccess("All done.");
            return true;
        }
        public static bool SaveTokenInsecurely(string token)
        {
            Logger.LogWarning("***WARNING***"); 
            Logger.LogWarning("This authentication method is INSECURE. Please consider hashing!");
            try
            {
                File.WriteAllTextAsync(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\StrifeClient\token.raw", token);
            }
            catch (Exception ex)
            {
                Logger.LogError("Something went wrong while saving the token.\nException " + ex.Message);
                return false;
            }
            Logger.LogSuccess("Token saved.");
            Logger.LogSuccess("All done.");
            return true;
        }
        public static string GetSHA512(string input)
        {
            using var hashAlgorithm = SHA512.Create();
            var byteValue = Encoding.UTF8.GetBytes(input);
            var byteHash = hashAlgorithm.ComputeHash(byteValue);
            return Convert.ToBase64String(byteHash);
        }
        // Snippet credit: https://stackoverflow.com/questions/9237324/encrypting-decrypting-large-files-net
        // Rfc2898DeriveBytes constants:
        public static readonly byte[] salt = new byte[] { 0x11, 0x21, 0x11, 0x21, 0x21, 0x21, 0x11, 0x11 }; // Password is hashed already, we need minimal salt
        public static int iterations = 1042; // Recommendation is >= 1000.

        /// <summary>Decrypt a file.</summary>
        /// <remarks>NB: "Padding is invalid and cannot be removed." is the Universal CryptoServices error.  Make sure the password, salt and iterations are correct before getting nervous.</remarks>
        /// <param name="sourceFilename">The full path and name of the file to be decrypted.</param>
        /// <param name="destinationFilename">The full path and name of the file to be output.</param>
        /// <param name="password">The password for the decryption.</param>
        /// <param name="salt">The salt to be applied to the password.</param>
        /// <param name="iterations">The number of iterations Rfc2898DeriveBytes should use before generating the key and initialization vector for the decryption.</param>
        public static void DecryptFile(string sourceFilename, string destinationFilename, string password, byte[] salt, int iterations)
        {
            AesManaged aes = new AesManaged();
            aes.BlockSize = aes.LegalBlockSizes[0].MaxSize;
            aes.KeySize = aes.LegalKeySizes[0].MaxSize;
            // NB: Rfc2898DeriveBytes initialization and subsequent calls to   GetBytes   must be eactly the same, including order, on both the encryption and decryption sides.
            Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(password, salt, iterations);
            aes.Key = key.GetBytes(aes.KeySize / 8);
            aes.IV = key.GetBytes(aes.BlockSize / 8);
            aes.Mode = CipherMode.CBC;
            ICryptoTransform transform = aes.CreateDecryptor(aes.Key, aes.IV);

            using (FileStream destination = new FileStream(destinationFilename, FileMode.CreateNew, FileAccess.Write, FileShare.None))
            {
                using (CryptoStream cryptoStream = new CryptoStream(destination, transform, CryptoStreamMode.Write))
                {
                    try
                    {
                        using (FileStream source = new FileStream(sourceFilename, FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            source.CopyTo(cryptoStream);
                        }
                    }
                    catch (CryptographicException exception)
                    {
                        if (exception.Message == "Padding is invalid and cannot be removed.")
                            throw new ApplicationException("Universal Microsoft Cryptographic Exception (Not to be believed!)", exception);
                        else
                            throw;
                    }
                }
            }
        }

        /// <summary>Encrypt a file.</summary>
        /// <param name="sourceFilename">The full path and name of the file to be encrypted.</param>
        /// <param name="destinationFilename">The full path and name of the file to be output.</param>
        /// <param name="password">The password for the encryption.</param>
        /// <param name="salt">The salt to be applied to the password.</param>
        /// <param name="iterations">The number of iterations Rfc2898DeriveBytes should use before generating the key and initialization vector for the decryption.</param>
        public static void EncryptFile(string sourceFilename, string destinationFilename, string password, byte[] salt, int iterations)
        {
            AesManaged aes = new AesManaged();
            aes.BlockSize = aes.LegalBlockSizes[0].MaxSize;
            aes.KeySize = aes.LegalKeySizes[0].MaxSize;
            // NB: Rfc2898DeriveBytes initialization and subsequent calls to   GetBytes   must be eactly the same, including order, on both the encryption and decryption sides.
            Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(password, salt, iterations);
            aes.Key = key.GetBytes(aes.KeySize / 8);
            aes.IV = key.GetBytes(aes.BlockSize / 8);
            aes.Mode = CipherMode.CBC;
            ICryptoTransform transform = aes.CreateEncryptor(aes.Key, aes.IV);

            using (FileStream destination = new FileStream(destinationFilename, FileMode.CreateNew, FileAccess.Write, FileShare.None))
            {
                using (CryptoStream cryptoStream = new CryptoStream(destination, transform, CryptoStreamMode.Write))
                {
                    using (FileStream source = new FileStream(sourceFilename, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        source.CopyTo(cryptoStream);
                    }
                }
            }
        }
    }
}

