using FreshFarm.Domain.Dtos.User;
using FreshFarm.Domain.Entities;

namespace FreshFarm.Application.Mapper;

public class UserMapper
{
     public UserDto MapToDto(UserEntity entity)
        {
            return new UserDto
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email
            };
        }
}
