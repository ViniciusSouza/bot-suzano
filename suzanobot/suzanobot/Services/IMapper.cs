namespace suzanobot.Services
{
    public interface IMapper<T, TResult>
    {
        TResult Map(T item);
    }
}