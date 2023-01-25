namespace Questao5.Domain.Exceptions;

public class BusinessRuleValidationException : ApplicationException
{
    public string Code { get; }

    public BusinessRuleValidationException(string code, string description) : base(description)
    {
        Code = code;
    }
}