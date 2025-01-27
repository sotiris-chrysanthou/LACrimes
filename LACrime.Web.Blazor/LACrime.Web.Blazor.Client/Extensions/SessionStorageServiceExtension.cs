﻿using System.Text.Json;
using System.Text;
using Blazored.SessionStorage;

namespace LACrimes.Web.Blazor.Client.Extensions {
    public static class SessionStorageServiceExtension {
        public static async Task SaveItemEncryptedAsync<T>(this ISessionStorageService sessionStorageService, String key, T item) {
            var itemJson = JsonSerializer.Serialize(item);
            var itemJsonBytes = Encoding.UTF8.GetBytes(itemJson);
            var base64Json = Convert.ToBase64String(itemJsonBytes);
            await sessionStorageService.SetItemAsync(key, base64Json);
        }

        public static async Task<T?> ReadEncriptedItemAsync<T>(this ISessionStorageService sessionStorageService, String key) {
            var base64Json = await sessionStorageService.GetItemAsync<String>(key);
            if(base64Json == null) {
                return default;
            }
            var itemJsonBytes = Convert.FromBase64String(base64Json);
            var itemJson = Encoding.UTF8.GetString(itemJsonBytes);
            var item = JsonSerializer.Deserialize<T>(itemJson);
            return item;
        }
    }
}
