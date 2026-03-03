using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace RacksStands.Framework.Base.Urls;


public static class UrlHelper
{
    // ==============================
    // VALIDATION
    // ==============================

    public static bool IsAbsolute(string url) =>
        Uri.TryCreate(url, UriKind.Absolute, out _);

    public static bool IsRelative(string url) =>
        Uri.TryCreate(url, UriKind.Relative, out _);

    public static bool IsHttpUrl(string url)
    {
        if (!Uri.TryCreate(url, UriKind.Absolute, out var uri))
            return false;

        return uri.Scheme == Uri.UriSchemeHttp ||
               uri.Scheme == Uri.UriSchemeHttps;
    }

    // ==============================
    // COMBINE
    // ==============================

    public static string Combine(string baseUrl, string relativePath)
    {
        if (string.IsNullOrWhiteSpace(baseUrl))
            throw new ArgumentNullException(nameof(baseUrl));

        if (string.IsNullOrWhiteSpace(relativePath))
            return baseUrl;

        var baseUri = new Uri(baseUrl.EndsWith("/") ? baseUrl : baseUrl + "/");
        return new Uri(baseUri, relativePath).ToString();
    }

    // ==============================
    // QUERY STRING PARSING
    // ==============================

    public static Dictionary<string, string> ParseQuery(string url)
    {
        if (!Uri.TryCreate(url, UriKind.Absolute, out var uri))
            throw new ArgumentException("Invalid URL");

        return uri.Query
            .TrimStart('?')
            .Split('&', StringSplitOptions.RemoveEmptyEntries)
            .Select(q => q.Split('='))
            .ToDictionary(
                x => WebUtility.UrlDecode(x[0]),
                x => x.Length > 1 ? WebUtility.UrlDecode(x[1]) : string.Empty
            );
    }

    // ==============================
    // ADD OR UPDATE QUERY PARAMS
    // ==============================

    public static string AddOrUpdateQueryParams(
        string url,
        IDictionary<string, string> parameters)
    {
        if (!Uri.TryCreate(url, UriKind.Absolute, out var uri))
            throw new ArgumentException("Invalid URL");

        var queryParams = ParseQuery(url);

        foreach (var kvp in parameters)
        {
            queryParams[kvp.Key] = kvp.Value;
        }

        var newQuery = string.Join("&",
            queryParams.Select(kvp =>
                $"{WebUtility.UrlEncode(kvp.Key)}={WebUtility.UrlEncode(kvp.Value)}"));

        var uriBuilder = new UriBuilder(uri)
        {
            Query = newQuery
        };

        return uriBuilder.ToString();
    }

    // ==============================
    // REMOVE QUERY PARAM
    // ==============================

    public static string RemoveQueryParam(string url, string key)
    {
        var queryParams = ParseQuery(url);

        if (queryParams.Remove(key))
        {
            return AddOrUpdateQueryParams(url.Split('?')[0], queryParams);
        }

        return url;
    }

    // ==============================
    // GET SINGLE QUERY VALUE
    // ==============================

    public static string? GetQueryValue(string url, string key)
    {
        var queryParams = ParseQuery(url);
        return queryParams.TryGetValue(key, out var value)
            ? value
            : null;
    }
}
