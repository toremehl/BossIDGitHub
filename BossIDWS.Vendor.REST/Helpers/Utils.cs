using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml;
using System.Xml.Serialization;
using BossIDWS.Vendor.REST.ReturnObjects;

namespace BossIDWS.Vendor.REST
{
	/// <summary>
	/// 
	/// </summary>
	public static class Utils
	{
		private static Random _rnd;

		/// <summary>
		/// 
		/// </summary>
		static Utils()
		{
			_rnd = new Random();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="settingName"></param>
		/// <returns></returns>
		/// <exception cref="Exception"></exception>
		public static string GetAppSettingUsingConfigurationManager(string settingName)
		{
			try
			{
				return ConfigurationManager.AppSettings[settingName];
			}
			catch (Exception)
			{
				throw new Exception($"Could not find webconfig setting for {settingName}");
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		public static void ApplySettings(string key, string value)
		{
			var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			var settings = configFile.AppSettings.Settings;
			if (settings[key] == null)
			{
				settings.Add(key, value);
			}
			else
			{
				settings[key].Value = value;
			}
			configFile.Save(ConfigurationSaveMode.Modified);
			ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="key"></param>
		public static string GetSetting(string key)
		{
			string setting = string.Empty;

			var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			var settings = configFile.AppSettings.Settings;
			if (settings[key] != null)
			{
				setting = settings[key].Value;
			}

			return setting;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="text"></param>
		private static void WriteToLog(string text)
		{
			File.AppendAllText(@"C:\test.log", "\n" + text);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="serviceUrl"></param>
		/// <param name="url"></param>
		/// <param name="data"></param>
		/// <param name="method"></param>
		/// <param name="apikey"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static RO<T> RequestRestData<T>(string serviceUrl, string url, string data, string method, string apikey)
		{
			WriteToLog($"---------------------Start---------------------");
			WriteToLog($"Url:{url}");
			WriteToLog($"Input params: {data}");
			WriteToLog($"-----------------------------------------------");

			url = serviceUrl + url;

			var response = "";

			using (var client = new WebClient())
			{
				client.BaseAddress = url;
				client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
				client.Headers.Add(HttpRequestHeader.Accept, "application/json");
				client.Headers.Add("api-key", apikey);

				if (method == "GET")
				{
					response = client.DownloadString(new Uri(url));
				}
				else if (method == "POST")
					response = client.UploadString(url, data);
			}

			var ro = Newtonsoft.Json.JsonConvert.DeserializeObject<RO<T>>(response);

			WriteToLog($"ReturnCode: {ro.ReturnCode}");

			if (ro.ReturnValue != null)
			{
				var xml = SerializeObjectToXml(ro.ReturnValue);

				WriteToLog($"Returnvalue: \n{xml}");
				WriteToLog("---------------------End---------------------\n");
			}

			return ro;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="value"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static string SerializeObjectToXml<T>(T value)
		{
			//Create our own namespaces for the output
			var ns = new XmlSerializerNamespaces();

			//Add an empty namespace and empty value
			ns.Add("", "");

			var serializer = new XmlSerializer(value.GetType());
			var settings = new XmlWriterSettings
			{
				Indent = true,
				OmitXmlDeclaration = true

			};


			using (var stream = new StringWriter())
			using (var writer = XmlWriter.Create(stream, settings))
			{
				serializer.Serialize(writer, value, ns);
				var xml = stream.ToString();

				//if (encode)
				//    xml = System.Web.HttpUtility.HtmlEncode(xml);

				return xml;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public static String GetRandomHexString()
		{
			var buffer = new byte[7];
			_rnd.NextBytes(buffer);
			var result = string.Concat(buffer.Select(x => x.ToString("X2")).ToArray());
			//if (digits % 2 == 0)
			//    return result;
			return result + _rnd.Next(16).ToString("X");
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="floor"></param>
		/// <param name="ceiling"></param>
		/// <returns></returns>
		public static int GetRandomNumber(int floor, int ceiling)
		{
			return _rnd.Next(floor, ceiling);
		}
	}
}