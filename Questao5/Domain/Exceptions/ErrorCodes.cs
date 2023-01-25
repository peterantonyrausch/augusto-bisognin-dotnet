namespace Questao5.Domain.Exceptions;

public static class ErrorCodes
{
    public static BusinessRuleValidationException InvalidAccount() =>
        new("INVALID_ACCOUNT", "A conta informada é inválida.");

    public static BusinessRuleValidationException InactiveAccount() =>
        new("INACTIVE_ACCOUNT", "A conta informada está inativa.");

    public static BusinessRuleValidationException InvalidValue() =>
        new("INVALID_VALUE", "O valor informado não é válido.");

    public static BusinessRuleValidationException InvalidType() =>
        new("INVALID_TYPE", "O tipo do movimento não é válido.");
}