using Microsoft.JSInterop;
using System.Threading.Tasks;

public class CookieService {
    private readonly IJSRuntime _jsRuntime;

    public CookieService(IJSRuntime jsRuntime) {
        _jsRuntime = jsRuntime;
    }

    public async Task<string> GetCookie(string key) {
        return await _jsRuntime.InvokeAsync<string>("getCookie", key);
    }

    public async Task SetCookie(string key, string value) {
        await _jsRuntime.InvokeVoidAsync("setCookie", key, value);
    }

    public async Task DeleteCookie(string key) {
        await _jsRuntime.InvokeVoidAsync("deleteCookie", key);
    }
}