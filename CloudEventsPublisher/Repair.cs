namespace CloudEventsPublisher
{
    /// <summary>
    /// Used to request a repair.
    /// </summary>
    public class Repair : ShipEvent
    {
        /// <summary>
        /// Device which needs to be repaired.
        /// </summary>
        public string Device { get; set; }

        /// <summary>
        /// Description of the defect.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public Repair()
        {
            Type = "Repair";
        }
    }
}
