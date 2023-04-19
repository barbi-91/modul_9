using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;

namespace DemoWebshop.Services
{
    public static class SessionExtensions
    {
        //2metode extenzije
        //1 citanje
        //2 dohvat

        //serilaziranje
        public static void SetCartObjectAsJson(
            this ISession session, 
            string key, 
            object value
            ) 
        { 
            session.SetString(
                key, 
                JsonConvert.SerializeObject(value)
                );        
        }

        //deserijaliziranje
        public static List<CartItem> GetCartObjectFromJson(
            this ISession session,
            string key
            )
        {
            var value = session.GetString(key);
            return value == null ? new List<CartItem>() : JsonConvert.DeserializeObject<List<CartItem>>(value);
        }


    }
}
