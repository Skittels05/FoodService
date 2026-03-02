namespace AuthService.Application.Exceptions;

public class AccessDeniedException()
    : AppException($"You don't have access for this resource");
