namespace InternalApi.Validators
{
    public abstract class BaseValidator
    {
        protected static List<string>? BaseValidate<T>(T model)
        {
            if (model is null)
                return new List<string> { "Input model is null." };

            return null;
        }
    }
}
