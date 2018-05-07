namespace CloudEventsPublisher
{
    /// <summary>
    /// Event sent for a specific ship.
    /// </summary>
    public class ShipEvent
    {
        /// <summary>
        /// Name of the ship.
        /// </summary>
        public string Ship { get; set; }

        /// <summary>
        /// Type of event.
        /// </summary>
        public string Type { get; set; }
    }
}