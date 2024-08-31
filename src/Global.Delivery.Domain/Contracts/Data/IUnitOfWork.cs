namespace Global.Delivery.Domain.Contracts.Data
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
    }
}
