namespace StorageApp.Orders.Domain.Contracts
{
    public interface IUserContextAuth
    {
        string UserId { get; }
        string UserName { get; }
        bool IsAuthenticated { get; }
    }
}
