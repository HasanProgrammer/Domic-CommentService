using Domic.Core.Domain.Contracts.Abstracts;
using Domic.Core.Domain.Exceptions;

namespace Domic.Domain.ArticleCommentAnswer.ValueObjects;

public class Answer : ValueObject
{
    public readonly string Value;

    /// <summary>
    /// 
    /// </summary>
    public Answer() {}
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <exception cref="InValidValueObjectException"></exception>
    public Answer(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException("فیلد پاسخ کامنت الزامی می باشد !");

        if (value.Length is > 800 or < 10)
            throw new DomainException("فیلد پاسخ کامنت نباید بیشتر از 800 و کمتر از 10 عبارت داشته باشد !");

        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}