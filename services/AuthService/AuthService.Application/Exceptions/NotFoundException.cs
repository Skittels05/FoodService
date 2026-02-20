namespace AuthService.Application.Exceptions;

public class NotFoundException(string name, Guid id)
    : AppException($"\"{name}\" with id ({id}) was not found.");
