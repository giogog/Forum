using AutoMapper;
using Contracts;
using Domain.Entities;
using Domain.Models;
using System.Security.Claims;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;

    public UserService(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserDto>> GetUsers()
    {
        var users = await _repositoryManager.UserRepository
            .GetUsersWithCondition(u => u.Roles.Any(r => r.RoleId == -1));
        if (!users.Any())
            throw new NotFoundException("Users not found");

        return _mapper.Map<IEnumerable<UserDto>>(users);

    }

    public async Task<UserDto> GetUserWithEmail(string email)
    {
        var user = await _repositoryManager.UserRepository
            .GetUser(u => u.Email == email && u.Roles.Any(r => r.RoleId == -1));
        if (user == null)
            throw new NotFoundException("User On this mail not found");


        return _mapper.Map<UserDto>(user);
    }
    public async Task<User> GetUserWithClaim(ClaimsPrincipal principal)
    {
        var user = await _repositoryManager.UserRepository.GetUserFromClaim(principal);
        if (user == null)
            throw new NotFoundException("User not Found");

        return user;
    }

    public async Task UserBanStatusChange(int userId, Ban ban)
    {
        var user = await _repositoryManager.UserRepository.GetUser(u => u.Id == userId);
        if (user == null)
            throw new NotFoundException("Users not found");

        user.Banned = ban;

        await _repositoryManager.UserRepository.UpdateUser(user);
    }

}
