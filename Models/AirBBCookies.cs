using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace AirBB.Models;

public class AirBBCookies
{
    private const string ResCookie = "airbb_reservation_ids";

    private readonly IRequestCookieCollection _request;
    private readonly IResponseCookies _response;

    public AirBBCookies(IRequestCookieCollection request, IResponseCookies response)
    {
        _request = request;
        _response = response;
    }

    public List<int> GetReservationIds()
    {
        if (!_request.TryGetValue(ResCookie, out var json) || string.IsNullOrWhiteSpace(json))
            return new List<int>();
        try { return JsonSerializer.Deserialize<List<int>>(json) ?? new List<int>(); }
        catch { return new List<int>(); }
    }

    public void SaveReservationIds(List<int> ids)
    {
        var options = new CookieOptions
        {
            Expires = DateTimeOffset.Now.AddDays(7),
            HttpOnly = true,
            IsEssential = true
        };
        _response.Append(ResCookie, JsonSerializer.Serialize(ids), options);
    }

    public void Clear() => _response.Delete(ResCookie);
}
