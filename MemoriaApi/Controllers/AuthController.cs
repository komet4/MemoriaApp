using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using MemoriaApi.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace MemoriaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginModel model)
        {
            try
            {
                var user = await AuthenticateUserAsync(model.Username, model.Password);

                if (user != null)
                {
                    var token = GenerateAccessToken(user);
                    return Ok(new { token });
                }

                return Unauthorized();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing your request.", error = ex.Message });
            }
        }
        
        [AllowAnonymous]
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshTokenAsync(RefreshTokenModel model)
        {
            try
            {
                var validatedToken = ValidateRefreshToken(model.RefreshToken);

                if (validatedToken == null)
                {
                    return Unauthorized();
                }

                var user = await GetUserById(validatedToken.UserId);

                if (user == null)
                {
                    return Unauthorized();
                }

                var accessToken = GenerateAccessToken(user);
                return Ok(new { accessToken });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing your request.", error = ex.Message });
            }
        }
        private async Task<User> GetUserById(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            return user;
            
        }
        
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private RefreshToken ValidateRefreshToken(string refreshToken)
        {
            
            var storedToken = _context.RefreshTokens.FirstOrDefault(t => t.Token == refreshToken);

            if (storedToken == null || !IsRefreshTokenValid(storedToken))
            {
                return null;
            }

            return storedToken;
            
        }
        
        private bool IsRefreshTokenValid(RefreshToken refreshToken)
        {
            return refreshToken.ExpiryDate > DateTime.UtcNow;
        }
        
        private async Task<User> AuthenticateUserAsync(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

            if (user != null && VerifyPassword(password, user.PasswordHash))
            {
                return user;
            }

            return null;
        }

        private bool VerifyPassword(string password, byte[] storedHash)
        {
            using (var hmac = new HMACSHA512())
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i])
                        return false;
                }
            }

            return true;
        }

        private string GenerateAccessToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtSettings:ExpirationInDays"]));

            var token = new JwtSecurityToken(
                _configuration["JwtSettings:Issuer"],
                _configuration["JwtSettings:Audience"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [Authorize]
        [HttpGet("secured")]
        public IActionResult SecuredEndpoint()
        {
            // Этот метод доступен только авторизованным пользователям
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var username = User.FindFirst(ClaimTypes.Name)?.Value;

            return Ok(new { Message = "This is a secured endpoint", UserId = userId, Username = username });
        }
    }
}
