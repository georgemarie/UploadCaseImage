using System.Security.Cryptography;
using System.Text;

namespace UploadingCaseImages.Services.Utility
{
	public static class PasswordCoder
	{
		private static readonly Random Random = new();
		private static readonly byte[] Key = Encoding.UTF8.GetBytes("Utility@eVadiGital$2022#^utiLiTY");
		private static readonly byte[] IV = Encoding.UTF8.GetBytes("ReVaMpUtilty2022");

		public static byte[] GenerateSalt()
		{
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";
			string salt = new(Enumerable.Repeat(chars, 12)
			  .Select(s => s[Random.Next(s.Length)]).ToArray());
			return Convert.FromBase64String(salt);
		}

		public static string Encrypt(string password, byte[] salt)
		{
			byte[] encrypted;
			string encodedSalt = Convert.ToBase64String(salt);
			if (string.IsNullOrEmpty(password)) return string.Empty;
			password += encodedSalt;
			using (AesManaged aes = new())
			{
				// Create encryptor
				ICryptoTransform encryptor = aes.CreateEncryptor(Key, IV);

				// Create MemoryStream
				using (MemoryStream ms = new())
				{
					// Create crypto stream using the CryptoStream class. This class is the key to encryption
					// and encrypts and decrypts data from any given stream. In this case, we will pass a memory stream
					// to encrypt
					using (CryptoStream cs = new(ms, encryptor, CryptoStreamMode.Write))
					{
						// Create StreamWriter and write data to a stream
						using (StreamWriter sw = new(cs))
							sw.Write(password);
						encrypted = ms.ToArray();
					}
				}

				return Convert.ToBase64String(encrypted);
			}
		}

		public static string Decrypt(string cipherText, byte[] salt)
		{
			string plainText = null;
			byte[] cipherTextEncoded = Convert.FromBase64String(cipherText);
			using (AesManaged aes = new())
			{
				// Create a decryptor
				ICryptoTransform decryptor = aes.CreateDecryptor(Key, IV);

				// Create the streams used for decryption.
				using (MemoryStream ms = new(cipherTextEncoded))
				{
					// Create crypto stream
					using (CryptoStream cs = new(ms, decryptor, CryptoStreamMode.Read))
					{
						// Read crypto stream
						using (StreamReader reader = new(cs))
							plainText = reader.ReadToEnd();
					}
				}
			}

			string encodedSalt = Convert.ToBase64String(salt);
			plainText = plainText.Substring(0, plainText.Length - encodedSalt.Length);
			return plainText;
		}
	}
}
