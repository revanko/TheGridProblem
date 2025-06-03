namespace TheGridProblem.Models
{
    public interface ICustomParsable<T>
    {
        static abstract bool TryParse(string input, out T? result);
    }
}