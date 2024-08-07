using System.Security.Cryptography;
using System.Text;

namespace UploadingCaseImages.Integrations.Common.Extensions;
public static class StringExtensions
{
	public static string GetSecureHashForString(this string str, string key, KeyType keyType)
	{
		byte[] messageBytes = Encoding.UTF8.GetBytes(str);
		byte[] keyBytes = keyType == KeyType.UTF8
			? Encoding.UTF8.GetBytes(key)
			: HexStringToByteArray(key);

		using var hmac = new HMACSHA256(keyBytes);
		byte[] hmacBytes = hmac.ComputeHash(messageBytes);

		return BitConverter.ToString(hmacBytes).Replace("-", string.Empty);
	}

	private static byte[] HexStringToByteArray(string hex)
	{
		int numberChars = hex.Length;
		byte[] bytes = new byte[numberChars / 2];
		for (int i = 0; i < numberChars; i += 2)
		{
			bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
		}

		return bytes;
	}
}
