using System;
using FreshFarm.Domain.Entities;

namespace FreshFarm.Contract.Service.Auth;


public interface IAuthRepository
{
    Task AddAsync(UserEntity user);
    Task<UserEntity?> GetUserByEmailAsync(string email);
}