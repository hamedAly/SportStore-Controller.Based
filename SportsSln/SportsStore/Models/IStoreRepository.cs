namespace SportsStore.Models
{
    public interface IStoreRepository <T>
    {
        IQueryable<T> Products { get; }
    }
}
 