namespace CatalogManagement.SharedKernel;

public class Result<TValue>
{

    public TValue? Value { get; }
    public bool IsSucces { get; }
    public Error? Error { get; }

    private Result(bool isSuccess, Error? error = null, TValue? value = default)
    {
        
        if (isSuccess && value is null)
        {
            throw new ArgumentNullException(nameof(error));
        }

        if (!isSuccess && error is null)
        {
            throw new ArgumentNullException(nameof(error));
        }

        IsSucces = isSuccess;
        Error = error;
        Value = value;
    }

    public TNext Match<TNext>(Func<TValue, TNext> onSuccess, Func<Error, TNext> onFail)
    {
        if (IsSucces)
        {
            return onSuccess(Value);
        }

        return onFail(Error);
    }


    public static Result<TValue> Success(TValue value)
    {
        return new Result<TValue>(true, default, value);
    }
    public static Result<TValue> Fail(Error error)
    {
        return new Result<TValue>(false, error, default);
    }

}
