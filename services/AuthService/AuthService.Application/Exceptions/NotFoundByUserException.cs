namespace AuthService.Application.Exceptions;

public class NotFoundByUserException: Exception
{
    public NotFoundByUserException(string name, Guid id)
    : base($"\"{name}\" with user id ({id}) was not found.")
    {
    }
}
