namespace AuthService.Application.Exceptions;

public class UnauthorizedException()
    : AppException("Invalid token or token is missing");
