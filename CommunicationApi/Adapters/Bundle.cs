using CommunicationApi.Adapters.Abstract;
using TestPackages.Bookkeepings;
using TestPackages.Utils.Enums;
using static TestPackages.Bookkeepings.AbstractOrder;

namespace CommunicationApi.Adapters
{
    public class Bundle
    {
        // Attributes
        public Guid Id { get; init; }
        public Guid HeadId { get; init; }
        private readonly BundleTypes Type;
        private readonly AbstractAerial Aerial;
        private readonly AbstractSecurity Security;
        private readonly AbstractReliability Reliability;
        public ExpertAdvisorStateType State { get; set; }

        // Constructor
        public Bundle(BundleTypes type, Guid headId, ExpertAdvisorStateType initialState, AbstractAerial aerial,AbstractSecurity security, AbstractReliability reliability)
        {
            reliability.Security = security;
            security.Aerial = aerial;
            Type = type;
            HeadId = headId;
            Aerial = aerial;
            Security = security;
            Reliability = reliability;
            Id = Guid.NewGuid();
            State = initialState;
        }

        // Functions & methods
        public AbstractOrder OpenOrder(DirectionType direction, double volume, double openPrice)
        {
            return Reliability.OpenOrder(direction, volume, openPrice);
        }

        public AbstractOrder CloseOrder(AbstractOrder order)
        {
            return Reliability.CloseOrder(order);
        }

        public AbstractOrder OrderStatus(AbstractOrder order)
        {
            return Reliability.OrderStatus(order);
        }

        public void ChangeExpertAdvisorStateType(ExpertAdvisorStateType state)
        {
            Reliability.ChangeExpertAdvisorStateType((ExpertAdvisorStateType)state);
        }

        // Helping functions
        public override string ToString()
        {
            return $"Id: {Id}, HeadId: {HeadId}, Type: {Enum.GetName(typeof(BundleTypes), Type)}, State: {Enum.GetName(typeof(ExpertAdvisorStateType), State)}";
        }

        public override bool Equals(object? obj)
        {
            return obj is Bundle bundle &&
                   Id.Equals(bundle.Id) &&
                   HeadId.Equals(bundle.HeadId) &&
                   Type == bundle.Type &&
                   EqualityComparer<AbstractAerial>.Default.Equals(Aerial, bundle.Aerial) &&
                   EqualityComparer<AbstractSecurity>.Default.Equals(Security, bundle.Security) &&
                   EqualityComparer<AbstractReliability>.Default.Equals(Reliability, bundle.Reliability);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, HeadId, Type, Aerial, Security, Reliability);
        }
    }
}
