
﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
using BusinessObject.Models;
using DataAccess.InterfaceRepo;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.DTOs;

namespace Services.Service
{
    public class AuthService
    {
        private readonly IMemberRepository _memberRepo;
        private readonly IConfiguration _config;

        public AuthService(IMemberRepository memberRepo, IConfiguration config)
        {
            _memberRepo = memberRepo;
            _config = config;
        }

        public async Task<LoginResponseDTO> LoginAsync(LoginDTO loginRequest)
        {
            var member = await _memberRepo.GetMemberByEmailAsync(loginRequest.Email);
            if (member == null || !BCrypt.Net.BCrypt.Verify(loginRequest.Password, member.Password))
            {
                return null; // Email không tồn tại hoặc mật khẩu không đúng
            }
            var response = new LoginResponseDTO
            {
                Email = member.Email,
                Token = GenerateJwtToken(member),
            };
            return response;
        }

        private string GenerateJwtToken(Member member)
        {
            var jwtSettings = _config.GetSection("JwtSettings");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, member.Role),
                new Claim(ClaimTypes.Email, member.Email),
                new Claim(ClaimTypes.NameIdentifier, member.MemberId.ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(jwtSettings["ExpirationMinutes"])),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public async Task<bool> RegisterAsync(RegisterDTO registerRequest)
        {
            // Kiểm tra email đã tồn tại chưa
            var existingUser = await _memberRepo.GetMemberByEmailAsync(registerRequest.Email);
            if (existingUser != null)
            {
                return false; // Email đã tồn tại
            }

            // Hash mật khẩu bằng BCrypt
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerRequest.Password);

            // Tạo người dùng mới
            var newMember = new Member
            {
                Email = registerRequest.Email,
                Password = hashedPassword,
                CompanyName = registerRequest.CompanyName,
                City = registerRequest.City,
                Country = registerRequest.Country,
                Role = "Customer",
            };

            await _memberRepo.AddMemberAsync(newMember);

            return true; // Đăng ký thành công
        }
    }
}
