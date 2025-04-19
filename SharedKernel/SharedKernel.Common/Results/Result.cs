using SharedKernel.Common.Erros;

namespace SharedKernel.Common.Results;
public class Result<TValue> : IResult
{
    public TValue? Value { get; private set; }
    public bool IsSuccess { get; private set; }
    public List<Error>? Errors { get; }

    private Result(bool isSuccess, List<Error>? errors = null, TValue? value = default)
    {
        if (isSuccess && value is null)
        {
            throw new ArgumentNullException(nameof(errors));
        }

        if (!isSuccess && (errors is null || errors.Count < 1))
        {
            throw new ArgumentNullException(nameof(errors));
        }

        IsSuccess = isSuccess;
        Errors = errors;
        Value = value;
    }

    public TNext Match<TNext>(Func<TValue, TNext> onSuccess, Func<List<Error>, TNext> onFail)
    {
        if (IsSuccess)
        {
            return onSuccess(Value!);
        }

        return onFail(Errors!);
    }

    public static Result<TValue> Success(TValue value)
    {
        return new Result<TValue>(true, default, value);
    }

    public static Result<TValue> Fail(Error error)
    {
        return new Result<TValue>(false, [error], default);
    }

    public static Result<TValue> Fail(List<Error> errors)
    {
        return new Result<TValue>(false, errors, default);
    }

    public static implicit operator Result<TValue>(TValue value)
    {
        return Result<TValue>.Success(value);
    }

    public static implicit operator Result<TValue>(List<Error> errs)
    {
        return Result<TValue>.Fail(errs);
    }
    public static implicit operator Result<TValue>(Error err)
    {
        return Result<TValue>.Fail(err);
    }
}
