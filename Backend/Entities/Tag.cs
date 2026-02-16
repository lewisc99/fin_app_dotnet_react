using Domain.Common;
using Domain.Exceptions;

namespace Domain.Entities
{
    public class Tag : BaseEntity<int>
    {
        public string Name { get; private set; }

        public string Description { get; private set; }

        public bool IsActive { get; private set; }

        private readonly List<PaymentTag> _payments = new();
        public IReadOnlyCollection<PaymentTag> Payments =>
        _payments.AsReadOnly();

        private Tag() { }

        public Tag(string name, string description)
        {
            if (string.IsNullOrEmpty(name)) throw new DomainException("Tag name cannot be empty");

            Name = name;
            Description = description;
            IsActive = true;
        }

        public void Deactivate() => IsActive = false;
    }
}
