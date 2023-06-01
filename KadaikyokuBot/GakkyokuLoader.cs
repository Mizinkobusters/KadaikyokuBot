using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using Microsoft.Extensions.Configuration;

namespace KadaikyokuBot
{
    class GakkyokuLoader
    {
        private const string REGION = "jp2";
        private string token;
        private string url;

        public void loadKadaikyoku()
        {
            string path = Directory.GetCurrentDirectory();
            DirectoryInfo di = new DirectoryInfo(path);
            DirectoryInfo diParent = di.Parent.Parent;
            var builder = new ConfigurationBuilder()
                .AddJsonFile(path: $"{diParent.FullName}\\appsettings.json");
            var configuration = builder.Build();
            token = (string)configuration["RecToken"];

            url = "https://api.chunirec.net/2.0/music/showall.json?region=" + REGION + "&token=" + token;

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse res = (HttpWebResponse)req.GetResponse();
            
            using (res)
            {
                using (var resStream = res.GetResponseStream())
                {
                    var statusCode = res.StatusCode;
                    Console.WriteLine($"Status Code: {statusCode}");

                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<Gakkyoku.Rootobject>));
                    Gakkyoku.gakkyokuList = (List<Gakkyoku.Rootobject>)serializer.ReadObject(resStream);

                    Console.WriteLine($"Num of loaded titles: {Gakkyoku.gakkyokuList.Count}");
                    Console.WriteLine($"Database loading completed.");
                }
            }
        }
    }
}
