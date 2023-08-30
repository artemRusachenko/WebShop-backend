namespace lesson1_Simple_Functions___Controller.DTOs.OrdersDtos.enums
{
    public class OrderState
    {
        public enum State : int
        {
            New,
            Accepted,
            InWay,
            Completed,
            Rejected
        }
    }
}
