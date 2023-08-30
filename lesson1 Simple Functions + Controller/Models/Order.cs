namespace lesson1_Simple_Functions___Controller.Models
{
    public class Order : BaseEntity
    {
        public State State { get; set; } = State.New;

        public decimal TotalPrice { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }

        public List<OrderItem>? OrderItems { get; set; }

        public string StateInfo
        {
            get
            {
                if (State == State.New)
                {
                    return "The order has been created";
                }
                else if (State == State.Accepted)
                {
                    return "Order confirmed";
                }
                else if (State == State.InWay)
                {
                    return "Order on the way";
                }
                else if (State == State.Completed)
                {
                    return "Order completed";
                }
                else if (State == State.Rejected)
                {
                    return "Order rejected";
                }
                else
                {
                    return "Unknown state";
                }
            }
        }
    }

    public enum State : int
    {
        New,
        Accepted,
        InWay,
        Completed,
        Rejected
    }
}