using AutoMapper;
using Contracts;
using Domain.Entities;
using Domain.Exception;
using Domain.Models;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;

        public UserService(IRepositoryManager repositoryManager, IMapper mapper, ILogger<UserService> logger)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<UserDto>> GetUsers()
        {
            var users = await _repositoryManager.UserRepository
                .GetUsersWithCondition(u => u.Roles.Any(r => r.RoleId == -1));
            if (!users.Any())
            {
                _logger.LogWarning("No users found with the specified condition.");
                throw new NotFoundException("Users not found");
            }

            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<AuthorizedUserDto> GetAuthorizedUserData(int id)
        {
            var user = await _repositoryManager.UserRepository
                .GetUser(u => u.Id == id);
            if (user == null)
            {
                _logger.LogWarning("User not found with ID {Id}", id);
                throw new NotFoundException("User not found");
            }

            return _mapper.Map<AuthorizedUserDto>(user);
        }

        public async Task UpdateAuthorizedUser(int id, AuthorizedUserDto authorizedUserDto)
        {
            await _repositoryManager.BeginTransactionAsync();
            try
            {
                var user = await _repositoryManager.UserRepository
                    .GetUser(u => u.Id == id);
                if (user == null)
                {
                    _logger.LogWarning("User not found with ID {Id}", id);
                    throw new NotFoundException("User not found");
                }

                var checkUser = await _repositoryManager.UserRepository
                    .GetUser(u => u.UserName.ToLower() == authorizedUserDto.Username.ToLower() && u.UserName != user.UserName);
                if (checkUser != null)
                {
                    _logger.LogWarning("Username {Username} is already taken.", authorizedUserDto.Username);
                    throw new UsernameIsTakenException("Username is already taken.");
                }

                user.Name = authorizedUserDto.Name;
                user.Surname = authorizedUserDto.Surname;
                user.UserName = authorizedUserDto.Username;

                await _repositoryManager.UserRepository.UpdateUser(user);
                await _repositoryManager.SaveAsync();
                await _repositoryManager.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating authorized user with ID {Id}", id);
                await _repositoryManager.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<UserDto> GetUserWithEmail(string email)
        {
            var user = await _repositoryManager.UserRepository
                .GetUser(u => u.Email == email && u.Roles.Any(r => r.RoleId == -1));
            if (user == null)
            {
                _logger.LogWarning("User not found with email {Email}", email);
                throw new NotFoundException("User not found");
            }

            return _mapper.Map<UserDto>(user);
        }

        public async Task<User> GetUserWithClaim(ClaimsPrincipal principal)
        {
            var user = await _repositoryManager.UserRepository.GetUserFromClaim(principal);
            if (user == null)
            {
                _logger.LogWarning("User not found with the given claims.");
                throw new NotFoundException("User not found");
            }

            return user;
        }

        public async Task UserBanStatusChange(int userId, Ban ban)
        {
            await _repositoryManager.BeginTransactionAsync();
            try
            {
                var user = await _repositoryManager.UserRepository.GetUser(u => u.Id == userId);
                if (user == null)
                {
                    _logger.LogWarning("User not found with ID {UserId}", userId);
                    throw new NotFoundException("User not found");
                }

                user.Banned = ban;

                await _repositoryManager.UserRepository.UpdateUser(user);
                await _repositoryManager.SaveAsync();
                await _repositoryManager.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while changing ban status for user with ID {UserId}", userId);
                await _repositoryManager.RollbackTransactionAsync();
                throw;
            }
        }
    }
}
