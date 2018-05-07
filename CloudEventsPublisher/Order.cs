namespace CloudEventsPublisher
{
    /// <summary>
    /// Used to place an order.
    /// </summary>
    public class Order : ShipEvent
    {
        /// <summary>
        /// Name of the product.
        /// </summary>
        public string Product { get; set; }

        /// <summary>
        /// Number of items to be ordered.
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public Order()
        {
            Type = "Order";
        }
    }
}