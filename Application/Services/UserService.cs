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
    public async Task<AuthorizedUserDto> GetAuthorizedUserData(int id)
    {
        var user = await _repositoryManager.UserRepository
            .GetUser(u => u.Id == id);
        if (user == null)
            throw new NotFoundException("User On this id not found");


        return _mapper.Map<AuthorizedUserDto>(user);
    }

    public async Task UpdateAuthorizedUser(int id, AuthorizedUserDto authorizedUserDto)
    {
        var user = await _repositoryManager.UserRepository
            .GetUser(u => u.Id == id);
        if (user == null)
            throw new NotFoundException("User On this id not found");
        var checkUser = await _repositoryManager.UserRepository.GetUser(u=>u.UserName.ToLower() == authorizedUserDto.Username.ToLower() && u.UserName != user.UserName);
        if (checkUser != null)
            throw new UsernameIsTakenException("You can't take this Username");
        user.Name = authorizedUserDto.Name;
        user.Surname = authorizedUserDto.Surname;
        user.UserName = authorizedUserDto.Username;

        await _repositoryManager.UserRepository.UpdateUser(user);
        

        
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
