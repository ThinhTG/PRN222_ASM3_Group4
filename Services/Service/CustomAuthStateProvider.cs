using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Services.DTOs;

namespace Services.Service
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly IJSRuntime _jsRuntime;
        private ClaimsPrincipal _currentUser = new(new ClaimsIdentity());
        private bool _isInitialized;
        
        public CustomAuthStateProvider(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if (!_isInitialized)
            {
                // Trả về trạng thái rỗng khi chưa khởi tạo (prerendering)
                return new AuthenticationState(_currentUser);
            }

            string token = null;
            try
            {
                token = await _jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "authToken");
                Console.WriteLine($"GetAuthenticationStateAsync - Token: {token ?? "null"}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving token: {ex.Message}");
                return new AuthenticationState(_currentUser);
            }

            var identity = string.IsNullOrEmpty(token)
                ? new ClaimsIdentity()
                : new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");

            _currentUser = new ClaimsPrincipal(identity);
            Console.WriteLine($"User authenticated: {_currentUser.Identity.IsAuthenticated}");
            return new AuthenticationState(_currentUser);
        }

        public async Task<string?> GetUserRoleAsync()
        {
            var token = await _jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "authToken");
            if (string.IsNullOrEmpty(token))
                return null;

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var roleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "http MOST schemas.microsoft.com/ws/2008/06/identity/claims/role");
            return roleClaim?.Value;
        }

        public async Task InitializeAsync()
        {
            string token = await _jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "authToken");
            var identity = string.IsNullOrEmpty(token)
                ? new ClaimsIdentity()
                : new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");

            _currentUser = new ClaimsPrincipal(identity);
            _isInitialized = true;
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_currentUser)));
            Console.WriteLine($"Initialized auth state: {_currentUser.Identity.IsAuthenticated}");
        }
        
        public async Task LoginAsync(LoginResponseDTO res)
        {
            await _jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "authToken", res.Token);
            var identity = new ClaimsIdentity(ParseClaimsFromJwt(res.Token), "jwt");
            _currentUser = new ClaimsPrincipal(identity);
            _isInitialized = true;
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_currentUser)));
            Console.WriteLine("Login successful, notifying state change.");
        }

        public async Task LogoutAsync()
        {
            await _jsRuntime.InvokeVoidAsync("sessionStorage.removeItem", "authToken");
            _currentUser = new ClaimsPrincipal(new ClaimsIdentity());
            _isInitialized = true;
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_currentUser)));
            Console.WriteLine("Logout successful, notifying state change.");
        }

        public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwt);
            return token.Claims;
        }
    }
}