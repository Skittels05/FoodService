namespace AuthService.Application.Exceptions;

public class NotFoundByAuth0Exception(string auth0Id)
    : AppException($"User with Auth0Id '{auth0Id}' not found in the local database.");
