using System;

namespace CloudEventsPublisher
{
    /// <summary>
    /// Representation of the CloudEvents specification, to be sent to Event Grid Topic.
    /// </summary>
    class CloudEvents
    {
        /// <summary>
        /// This will be used to update the Source and Data properties.
        /// </summary>
        public ShipEvent UpdateProperties
        {
            set
            {
                Source = $"{Program.TOPIC}#{value.Ship}/{value.Type}";
                Data = value;
            }
        }

        /// <summary>
        /// Gets the version number of the CloudEvents specification which has been used.
        /// </summary>
        public string CloudEventsVersion { get; }

        /// <summary>
        /// Gets the registered event type for this event source.
        /// </summary>
        public string EventType { get; }

        /// <summary>
        /// Gets the The version of the eventType.
        /// </summary>
        public string EventTypeVersion { get; }

        /// <summary>
        /// Gets the event producer properties.
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// Gets the unique identifier for the event.
        /// </summary>
        public string EventID { get; }

        /// <summary>
        /// Gets the time the event is generated based on the provider's UTC time.
        /// </summary>
        public string EventTime { get; }

        /// <summary>
        /// Gets or sets the event data specific to the resource provider.
        /// </summary>
        public ShipEvent Data { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public CloudEvents()
        {
            EventID = Guid.NewGuid().ToString();
            EventType = "shipevent";
            EventTime = DateTime.UtcNow.ToString("o");
        }
    }
}
