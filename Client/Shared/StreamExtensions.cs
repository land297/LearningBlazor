using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Learning.Client.Shared {
    public static class StreamExtensions {
        public static async Task<T> DeserializeJsonCamelCaseAsync<T>(this System.IO.Stream stream) {
            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            return await System.Text.Json.JsonSerializer.DeserializeAsync<T>(stream, options);
        }
    }
}
