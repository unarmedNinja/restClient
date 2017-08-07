using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;

namespace WebAPIClient
{
    public class Api{

        public async Task<string> getData(string url,List<string> acceptHeaders, List<webHeader> headers){

            using( var client = new HttpClient()){;
                client.DefaultRequestHeaders.Accept.Clear();

                foreach(string acceptHeader in acceptHeaders){
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(acceptHeader));
                }                

                foreach(webHeader header in headers){
                    client.DefaultRequestHeaders.Add(header.name, header.value);
                }                    

                var stringTask = client.GetStringAsync(url);

                string msg = await stringTask;
                
                return msg;
            }
        }
    }
}