using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Blazored.LocalStorage;
using System.Runtime.CompilerServices;
using WebDB;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace ElectrumDev_Website.Data
{
    public class BrowserService
    {
        private readonly IJSRuntime _js;

        public BrowserService(IJSRuntime js)
        {
            _js = js;
        }

        public async Task<BrowserDimension> GetDimensions()
        {
            return await _js.InvokeAsync<BrowserDimension>("getDimensions");
        }

    }

    public class BrowserDimension
    {
        public int Width { get; set; }
        public int Height { get; set; }
    }


    public class WebData
	{
		public static string "KEY HERE";

		public static string EncryptString(string key, string plainText)
		{
			byte[] iv = new byte[16];
			byte[] array;

			using (var aes = System.Security.Cryptography.Aes.Create())
			{
				aes.Key = System.Text.Encoding.UTF8.GetBytes(key);
				aes.IV = iv;

				System.Security.Cryptography.ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

				using (MemoryStream memoryStream = new MemoryStream())
				{
					using (var cryptoStream = new System.Security.Cryptography.CryptoStream((Stream)memoryStream, encryptor, System.Security.Cryptography.CryptoStreamMode.Write))
					{
						using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
						{
							streamWriter.Write(plainText);
						}

						array = memoryStream.ToArray();
					}
				}
			}

			return Convert.ToBase64String(array);
		}

		public static string DecryptString(string key, string cipherText)
		{
			byte[] iv = new byte[16];
			byte[] buffer = Convert.FromBase64String(cipherText);

			using (Aes aes = Aes.Create())
			{
				aes.Key = Encoding.UTF8.GetBytes(key);
				aes.IV = iv;
				ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

				using (MemoryStream memoryStream = new MemoryStream(buffer))
				{
					using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
					{
						using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
						{
							return streamReader.ReadToEnd();
						}
					}
				}
			}
		}

		public static async Task<bool> ValidateUser(ILocalStorageService storage)
		{
			string usrPass = await storage.GetItemAsync<string>("UserPasswordEncrypted");
			string usrName = await storage.GetItemAsync<string>("UserUsername");

			List<WebDB.Users.Model> users = WebDB.Users.Access.GetItems();
			foreach (var user in users)
			{
				if (user.Password == usrPass && (user.Username == usrName || user.Email == usrName.ToLower()))
				{
					return true;
				}
			}
			Console.WriteLine("False ret");
			return false;
		}

		public static async Task<WebDB.Users.Model> GetCurrentUser(ILocalStorageService storage)
		{
			string usrPass = await storage.GetItemAsync<string>("UserPasswordEncrypted");
			string usrName = await storage.GetItemAsync<string>("UserUsername");

			List<WebDB.Users.Model> users = WebDB.Users.Access.GetItems();
			foreach (var user in users)
			{
				if (user.Password == usrPass && (user.Username == usrName || user.Email == usrName.ToLower()))
				{
					return user;
				}
			}
			return new WebDB.Users.Model();
		}

	}
}
