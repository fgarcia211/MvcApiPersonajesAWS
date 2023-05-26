﻿using MvcApiPersonajesAWS.Models;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace MvcApiPersonajesAWS.Services
{
    public class ServiceApiPersonajes
    {
        private string UrlApi;
        private MediaTypeWithQualityHeaderValue Header;

        public ServiceApiPersonajes(IConfiguration configuration)
        {
            this.UrlApi = configuration.GetValue<string>("ApiUrls:ApiPersonajes");
            this.Header =
                new MediaTypeWithQualityHeaderValue("application/json");
        }

        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClientHandler handler = new HttpClientHandler())
            {
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicy) =>
                {
                    return true;
                };

                using (HttpClient client = new HttpClient(handler))
                {
                    client.BaseAddress = new Uri(this.UrlApi);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(this.Header);
                    HttpResponseMessage response =
                        await client.GetAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        T data = await response.Content.ReadAsAsync<T>();
                        return data;
                    }
                    else
                    {
                        return default(T);
                    }
                }

            }
            
        }

        private async Task<HttpStatusCode> InsertApiAsync<T>(string request, T objeto)
        {
            using (HttpClientHandler handler = new HttpClientHandler())
            {
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicy) =>
                {
                    return true;
                };

                using (HttpClient client = new HttpClient(handler))
                {
                    client.BaseAddress = new Uri(this.UrlApi);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(this.Header);

                    string json = JsonConvert.SerializeObject(objeto);

                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(request, content);
                    return response.StatusCode;
                }

            }

        }

        private async Task<HttpStatusCode> DeleteApiAsync<T>(string request)
        {
            using (HttpClientHandler handler = new HttpClientHandler())
            {
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicy) =>
                {
                    return true;
                };

                using (HttpClient client = new HttpClient(handler))
                {
                    client.BaseAddress = new Uri(this.UrlApi);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(this.Header);

                    HttpResponseMessage response = await client.DeleteAsync(request);
                    return response.StatusCode;
                }

            }

        }

        private async Task<HttpStatusCode> PutApiAsync<T>(string request, T objeto)
        {
            using (HttpClientHandler handler = new HttpClientHandler())
            {
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicy) =>
                {
                    return true;
                };

                using (HttpClient client = new HttpClient(handler))
                {
                    client.BaseAddress = new Uri(this.UrlApi);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(this.Header);

                    string json = JsonConvert.SerializeObject(objeto);

                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync(request, content);
                    return response.StatusCode;
                }

            }

        }

        public async Task<List<Personaje>> GetPersonajesAsync()
        {
            string request = "/api/personajes";
            List<Personaje> personajes =
                await this.CallApiAsync<List<Personaje>>(request);
            return personajes;
        }
        
        public async Task InsertPersonajeAsync(Personaje personaje)
        {
            string request = "/api/personajes/" + personaje.Nombre + "/" + personaje.Imagen;
            await this.InsertApiAsync<string>(request, "");
        }

        public async Task PutPersonajeAsync(Personaje personaje)
        {
            string request = "/api/personajes/" + personaje.IdPersonaje + "/" + personaje.Nombre + "/" + personaje.Imagen;
            await this.PutApiAsync<string>(request, "");
        }

        public async Task DeletePersonajeAsync(int id)
        {
            string request = "/api/personajes/" + id;
            await this.DeleteApiAsync<string>(request);
        }
    }
}
