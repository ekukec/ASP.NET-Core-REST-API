using ASP.NET_Core_REST_API.DTOs;

namespace ASP.NET_Core_REST_API.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDTO> RegisterAsync(RegisterDTO dto);
        Task<AuthResponseDTO> LoginAsync(LoginDTO dto);
    }
}