namespace DataModel.Models.DataBase
{
    public interface IEntity<T>
    {
        public T Id { get; init; }
    }
}