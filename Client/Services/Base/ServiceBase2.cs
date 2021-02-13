using Learning.Client.Shared;
using Learning.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Learning.Client.Services.Base {
    public class ServiceBase2 {
            protected readonly HttpClient _http;
            public ServiceBase2(HttpClient http) {
                _http = http;
            }
            public async Task<sr<T>> Get<T>(string uri) {
                var response = await _http.GetAsync(uri);

                if (response.IsSuccessStatusCode) {
                    var stream = await response.Content.ReadAsStreamAsync();
                    var t = await stream.TryDeserializeJsonCamelCaseAsync<T>();
                    if (t.Success) {
                        return sr<T>.GetSuccess(t.Data);
                    } else {
                        var s = await response.Content.ReadAsStringAsync();

                        Console.WriteLine("!!" + s);
                        Console.WriteLine("!!" + response.RequestMessage.RequestUri);
                        Console.WriteLine("!!" + response.RequestMessage.Content);

                        return sr<T>.Get(response.RequestMessage.RequestUri + " " + response.Headers.Location + " " + s);
                    }
                } else {
                    var error = await response.Content.ReadAsStringAsync();
                    return sr<T>.Get(response.Headers.Location + error);
                }
            }
            public async Task<sr<Robj>> Post<TJson,Robj>(string uri, TJson json) {
                var response = await _http.PostAsJsonAsync(uri, json);

                if (response.IsSuccessStatusCode) {
                    var stream = await response.Content.ReadAsStreamAsync();
                    var t = await stream.TryDeserializeJsonCamelCaseAsync<Robj>();
                    if (t.Success) {
                        return sr<Robj>.GetSuccess(t.Data);
                    } else {
                        var s = await response.Content.ReadAsStringAsync();

                        Console.WriteLine("!!" + s);
                        Console.WriteLine("!!" + response.RequestMessage.RequestUri);
                        Console.WriteLine("!!" + response.RequestMessage.Content);

                        return sr<Robj>.Get(response.RequestMessage.RequestUri + " " + response.Headers.Location + " " + s);
                    }
                } else {
                    var error = await response.Content.ReadAsStringAsync();
                    return sr<Robj>.Get(response.Headers.Location + error);
                }
            }
            public async Task<sr<bool>> Delete(string uri) {
                var response = await _http.DeleteAsync(uri);

                if (response.IsSuccessStatusCode) {
                    return sr<bool>.GetSuccess(true);
                } else {
                    var error = await response.Content.ReadAsStringAsync();
                    return sr<bool>.Get(response.Headers.Location + error);
                }
            }
    
    }
}
