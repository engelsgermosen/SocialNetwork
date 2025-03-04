using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace SocialNetwork.Core.Application.Helpers
{
    public static class SessionHelper
    {
        public static void Set<T> (this ISession session, string key, T value)
        {
            session.SetString(key,JsonConvert.SerializeObject(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            var data = session.GetString(key);
            return data == null ? default : JsonConvert.DeserializeObject<T>(data);
        }
    }
}
