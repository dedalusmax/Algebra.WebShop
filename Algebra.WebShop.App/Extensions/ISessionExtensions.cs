﻿using Algebra.WebShop.App.Models;
using Newtonsoft.Json;

namespace Algebra.WebShop.App.Extensions;

public static class ISessionExtensions
{
    private const string CART_SESSION_KEY = "_cart";

    public static List<CartItem> GetCart(this ISession session)
    {
        var sessionData = session.GetString(CART_SESSION_KEY);

        return string.IsNullOrEmpty(sessionData) ? [] : JsonConvert.DeserializeObject<List<CartItem>>(sessionData)!;
    }

    public static void SetCart(this ISession session, List<CartItem> value)
    {
        var sessionData = JsonConvert.SerializeObject(value);
        session.SetString(CART_SESSION_KEY, sessionData);
    }
}
