using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace System.Net
{
	public static class UriExtensions
	{
		public static string GetWebRequestOutput(this Uri uri)
		{
			HttpWebRequest webRequest = HttpWebRequest.Create(uri) as HttpWebRequest;
			using (var responseReader = new StreamReader(webRequest.GetResponse().GetResponseStream()))
			{
				return responseReader.ReadToEnd();
			}
		}
	}
}
