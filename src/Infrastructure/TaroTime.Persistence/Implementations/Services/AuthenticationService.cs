using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaroTime.Application.DTOs.AppUsers;
using TaroTime.Application.DTOs.Tokens;
using TaroTime.Application.Interfaces.Services;
using TaroTime.Domain.Entities;
using TaroTime.Domain.Enums;

namespace TaroTime.Persistence.Implementations.Services
{
    internal class AuthenticationService: IAuthenticationService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;

        public AuthenticationService(UserManager<AppUser> userManager, 
            IMapper mapper,
            IConfiguration configuration,
            ITokenService tokenService,
            SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _configuration = configuration;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }
        public async Task RegisterAsync(RegisterDto userDto)
        {
            AppUser user = _mapper.Map<AppUser>(userDto);
            IdentityResult result = await _userManager.CreateAsync( user, userDto.Password);

            if (!result.Succeeded)
            {
                StringBuilder sb = new();
                foreach (IdentityError error in result.Errors)
                {
                    sb.Append(error.Description);
                }
                throw new Exception(sb.ToString());
            }
            await _userManager.AddToRoleAsync(user,UserRole.Member.ToString());

        }

        public async Task<TokenResponseDto> LoginAsync(LoginDto userDto)
        {
            AppUser user= await _userManager.Users.FirstOrDefaultAsync(u=>u.UserName==userDto.UsernameOrEmail || u.Email==userDto.UsernameOrEmail);
            if (user==null)
            {
                throw new Exception("username,email or password is incorrect");
            }

            bool result = await _userManager.CheckPasswordAsync(user, userDto.Password);
            if (!result)
            {
                await _userManager.AccessFailedAsync(user);
                throw new Exception("username, email or password is incorrect");
            }
            var roles = await _userManager.GetRolesAsync(user);
            return  _tokenService.CreateAccessToken(user, roles ,15);
        }

        public async Task<string> ForgotPasswordAsync(ForgotPasswordDto userdto)
        {
            AppUser user = await _userManager.FindByEmailAsync(userdto.Email);
            if (user == null)
            {
                throw new Exception("user not found");
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            return token;
        }

        public async Task ResetPasswordAsync(ResetPasswordDto userdto)
        {


            AppUser user = await _userManager.FindByEmailAsync(userdto.UsernameOrEmail);
            if (user == null)
                throw new Exception("User not found");

            if (userdto.NewPassword != userdto.ConfirmPassword)
                throw new Exception("Passwords do not match");

            var result = await _userManager.ResetPasswordAsync(
                user,
                userdto.Token,
                userdto.NewPassword
            );


            if (!result.Succeeded)
            {
                StringBuilder sb = new();
                foreach (IdentityError error in result.Errors)
                {
                    sb.Append(error.Description);
                }
                throw new Exception(sb.ToString());
            }
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

    }
}

