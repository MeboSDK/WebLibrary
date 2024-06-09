using MediatR;

namespace Application.Commands.UserC.Commands;

public record UserRegisterCommand(string Username, string Email, string Password) : IRequest<string>;
