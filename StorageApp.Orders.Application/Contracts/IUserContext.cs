namespace StorageApp.Orders.Application.Contracts
{
    internal interface IUserContext
    {
        string UserId { get; }
        bool IsAuthenticated { get; }
    }
}
