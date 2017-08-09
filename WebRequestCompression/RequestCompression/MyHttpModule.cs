using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.IO;
using System.IO.Compression;
using Newtonsoft.Json;

namespace WebRequestCompression.RequestCompression
{
    public class MyHttpModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.BeginRequest += Application_BeginRequest;
        }

        /// <summary>
        /// Funkcja, której zadaniem jest nadpisanie przychodzącego requesta z klienta do serwera
        /// zamiast kilkudziesięciu pól przychodzi tylko jedno pole ze skompresowaną zawartością
        /// zostaje one odczytane i zdekompresowane
        /// następnie klucze które zostały usunięte przy submicie są odtwarzane
        /// przez co serwer widzi wysyłane pola w nienaruszonej postaci
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void Application_BeginRequest(Object source, EventArgs e)
        {
            HttpApplication application = (HttpApplication)source;
            HttpContext context = application.Context;

            if (context.Request.Path.EndsWith(".aspx") && context.Request.HttpMethod == "POST")
            {
                // zapisywanie wielkości wysyłanego requesta (z klienta na serwer)
                HttpContext.Current.Application["requestSize"] = context.Request.ContentLength / 1024;                

                //nadpisywanie requesta
                var requestCallback = new Func<string, string>(content =>
                {
                    var httpValueCollection = HttpUtility.ParseQueryString(content);
                    var compressedData = httpValueCollection["HiddenFieldCompressedData"];
                    List<JsonObject> jsonObjectList = new List<JsonObject>();

                    if (compressedData.Length > 0)
                    {
                        var outputStream = new MemoryStream(Convert.FromBase64String(compressedData));

                        using (ZipArchive archive = new ZipArchive(outputStream, ZipArchiveMode.Read))
                        {
                            foreach (ZipArchiveEntry entry in archive.Entries)
                            {
                                using (var stream = entry.Open())
                                using (var reader = new StreamReader(stream))
                                {
                                    string result = reader.ReadToEnd();
                                    jsonObjectList = JsonConvert.DeserializeObject<List<JsonObject>>(result);
                                }
                            }
                        }
                    }

                    // dodawanie kluczy wraz z wartosciami usuniętych po stronie klienta 
                    foreach (var jsonObjectItem in jsonObjectList)
                    {
                        content += "%3D&" + jsonObjectItem.id + "=" + jsonObjectItem.value;
                    }

                    System.Collections.Specialized.NameValueCollection httpValueCollectionFinal = HttpUtility.ParseQueryString(content);

                    return httpValueCollectionFinal.ToString();
                });
                context.Request.Filter = new RequestFilter(context.Request.Filter, context.Request.ContentEncoding, requestCallback);                                                                  
            }
        }

        public void Dispose()
        {
        }
    }
}