namespace InventoryManagement.Domain.StockAggregateRoot.ValueObjects
{
    public sealed class StockId : ValueObject
    {
        public Guid Value { get; }

        private ProductId(Guid Id)
        {
            Value = Id;
        }

        public static StockId CreateUnique()
        {
            return new ProductId(Guid.NewGuid());
        }
        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
        public static implicit operator Guid(StockId Id) => Id.Value;
        public static implicit operator StockId(Guid Id) => new StockId(Id);

    }