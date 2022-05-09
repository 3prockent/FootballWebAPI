namespace FootballWebAPI.Data.Json
{
    public interface IConverter<T>
    {
        public object ToJson(T obj);
    }
}
