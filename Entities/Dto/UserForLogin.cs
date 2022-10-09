using Core.Entities.Abstract;

namespace Entities.Dto;

public class UserForLogin : IDto
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
}