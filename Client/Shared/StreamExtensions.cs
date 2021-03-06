﻿using Learning.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Learning.Client.Shared {

    public static class StreamExtensions {
        public static ValueTask<T?> DeserializeJsonCamelCaseAsync<T>(this System.IO.Stream stream) {
            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            return System.Text.Json.JsonSerializer.DeserializeAsync<T>(stream, options);
        }
        public static async Task<sr<T>> TryDeserializeJsonCamelCaseAsync<T>(this System.IO.Stream stream) {
            try {
                var t = await DeserializeJsonCamelCaseAsync<T>(stream);
                return sr<T>.GetSuccess(t);
            } catch (Exception e){
                return sr<T>.Get(e.Message);
            }
        }

    }
}
