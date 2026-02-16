using Domain.ValueObjects;

namespace Domain.Entities
{
    public class PaymentTag
    {
        public PaymentId PaymentId { get; set; }
        public int TagId { get; private set; }

        public DateTimeOffset TaggedAt { get; private set; }

        public Payment Payment { get; private set; } = null!;
        public Tag Tag { get; private set; } = null!;

        private PaymentTag() { }

        internal PaymentTag(PaymentId paymentId, int tagId)
        {
            PaymentId = paymentId;
            TagId = tagId;
            TaggedAt = DateTimeOffset.UtcNow;
        }


    }
}
