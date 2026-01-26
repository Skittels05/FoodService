namespace AuthService.Application.Exceptions;

public class NotFoundByUserException(string name, Guid id)
    : Exception($"\"{name}\" with user id ({id}) was not found.");
