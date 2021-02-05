using Learning.Client.Shared;
using Learning.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Learning.Client.Services {
    public static class ServiceExtensions {
        public static async Task<sr<T>> ReturnSerializedContentOrError<T>(HttpResponseMessage response) {
            var stream = await response.Content.ReadAsStreamAsync();
            var result = await stream.TryDeserializeJsonCamelCaseAsync<T>();
            return result;
        }
    }
}
