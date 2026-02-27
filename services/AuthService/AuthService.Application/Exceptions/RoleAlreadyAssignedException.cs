namespace AuthService.Application.Exceptions;

public class RoleAlreadyAssignedException()
    : AppException("A role has already been assigned to this user.");
