namespace AuthService.Application.Exceptions;

public class NotFoundException(string name, Guid id)
    : Exception($"\"{name}\" with id ({id}) was not found.");
