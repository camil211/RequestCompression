using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebRequestCompression
{
    /// <summary>
    /// Pomocnicza klasa słuążaca do deserializacji Jsona
    /// </summary>
    public class JsonObject
    {
        public string id { get; set; }

        public string value  { get; set; }        
    }
}