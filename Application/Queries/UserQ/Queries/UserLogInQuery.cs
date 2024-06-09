using MediatR;

namespace Application.Queries.UserQ.Queries;

public record UserLogInQuery(string Email, string Password) : IRequest<string>;
