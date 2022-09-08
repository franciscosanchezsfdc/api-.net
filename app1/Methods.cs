using System;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json.Nodes;

namespace app1
{
    public class Methods
    {
        private int objectStatus { get; set; }

        public Methods()
        {
        }

        public async Task<HttpStatusCode> Patch(string filePath)
        {
            var uri = "https://pi.localhost.com/api/v5/objects/files/5?fields=id,name,url,size,bitlyIsPersonalized,bitlyShortUrl,vanityUrl,vanityUrlPath,createdById,updatedById,createdAt,updatedAt,salesforceId,campaignId,trackerDomainId,isTracked,folderId";
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"File [{filePath}] not found.");
            }
            using (var client = new HttpClient())
            {
                using (var multipartFormDataContent = new MultipartFormDataContent("95b354af-e8c9-4dcd-a944-bc69794ac1479"))
                {
                    var values = new[]
                    {
                    new KeyValuePair<string, string>("input", "{\"name\": \"Test It\"}"),
                     //other values
            };

                    foreach (var keyValuePair in values)
                    {
                        multipartFormDataContent.Add(new StringContent(keyValuePair.Value),
                            String.Format("\"{0}\"", keyValuePair.Key));
                    }

                    multipartFormDataContent.Add(new ByteArrayContent(File.ReadAllBytes(filePath)),
                        '"' + "file" + '"',
                        '"' + "Test_now_again.txt" + '"');

                    var requestUri = uri;
                    var request = new HttpRequestMessage()
                    {
                        RequestUri = new Uri(uri),
                        Method = HttpMethod.Patch,
                    };
                    client.DefaultRequestHeaders.Add("Pardot-Business-Unit-Id", "0Uvxx00000036A964B");
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer CustomToken315");
                    
                    var result = client.PatchAsync(requestUri, multipartFormDataContent).Result;
                    Console.WriteLine(await result.Content.ReadAsStringAsync());
                    return result.StatusCode;
                }
            }
        }
    }
}

