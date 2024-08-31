namespace Global.Delivery.Domain.Contracts.Validation
{
    public interface IValidableEntity
    {
        ISet<string> Errors { get; }
        bool Validate();
    }
}
