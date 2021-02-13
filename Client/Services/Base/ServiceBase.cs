using Learning.Client.Shared;
using Learning.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Learning.Client.Services {
    public class ServiceBase {
        protected HttpClient _http;
        public ServiceBase(HttpClient http) {
            _http = http;
        }

        public async Task<sr<T>> Get<T>(string uri) {
            try {
                var response = await _http.GetAsync(uri);
                if (response.IsSuccessStatusCode) {
                    var stream = await response.Content.ReadAsStreamAsync();
                    var sr = await stream.TryDeserializeJsonCamelCaseAsync<T>();
                    if (sr.Success) {
                        return sr;
                    } else {
                        var s = await response.Content.ReadAsStringAsync();
                        return sr<T>.Get(s);
                    }
                } else {
                    var message = await response.Content.ReadAsStringAsync();
                    return sr<T>.Get(message);
                }
            } catch (Exception e) {
                return sr<T>.Get(e);
            }
        }
    
    }
}
