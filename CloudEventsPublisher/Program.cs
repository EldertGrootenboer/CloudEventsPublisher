using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CloudEventsPublisher
{
    /// <summary>
    /// Send CloudEvents messages to an Event Grid Topic.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Endpoint of the Event Grid Topic.
        /// Update this with your own endpoint from the Azure Portal.
        /// </summary>
        private const string TOPIC_ENDPOINT = "<your-topic-endpoint>";

        /// <summary>
        /// Key of the Event Grid Topic.
        /// Update this with your own key from the Azure Portal.
        /// </summary>
        private const string KEY = "<your-access-key>";

        /// <summary>
        /// Topic to which we will be publishing.
        /// Update the subscription id, resource group and topic name here.
        /// </summary>
        public const string TOPIC = "/subscriptions/<your-subscription-id>/resourceGroups/<your-resource-group>/providers/Microsoft.EventGrid/topics/<your-topic-name>";

        /// <summary>
        /// Main method.
        /// </summary>
        public static void Main(string[] args)
        {
            // Set default values
            var entry = string.Empty;

            // Loop until user exits
            while (entry != "e" && entry != "exit")
            {
                // Get entry from user
                Console.WriteLine("Do you want to send an (o)rder, request a (r)epair or (e)xit the application?");
                entry = Console.ReadLine()?.ToLowerInvariant();

                // Get name of the ship
                Console.WriteLine("What is the name of the ship?");
                var shipName = Console.ReadLine();

                CloudEvents cloudEvents;
                switch (entry)
                {
                    case "e":
                    case "exit":
                        continue;
                    case "o":
                    case "order":
                        // Get user input
                        Console.WriteLine("What would you like to order?");
                        var product = Console.ReadLine();
                        Console.WriteLine("How many would you like to order?");
                        var amount = Convert.ToInt32(Console.ReadLine());

                        // Create order event
                        // Event Grid expects a list of events, even when only one event is sent
                        cloudEvents = new CloudEvents { UpdateProperties = new Order { Ship = shipName, Product = product, Amount = amount } };
                        break;
                    case "r":
                    case "repair":
                        // Get user input
                        Console.WriteLine("Which device would you like to get repaired?");
                        var device = Console.ReadLine();
                        Console.WriteLine("Please provide a description of the issue.");
                        var description = Console.ReadLine();

                        // Create repair event
                        // Event Grid expects a list of events, even when only one event is sent
                        cloudEvents = new CloudEvents { UpdateProperties = new Repair { Ship = shipName, Device = device, Description = description } };
                        break;
                    default:
                        Console.Error.WriteLine("Invalid entry received.");
                        continue;
                }

                // Send to Event Grid Topic
                SendEventsToTopic(cloudEvents).Wait();
            }
        }

        /// <summary>
        /// Send events to Event Grid Topic.
        /// </summary>
        private static async Task SendEventsToTopic(CloudEvents cloudEvents)
        {
            // Create a HTTP client which we will use to post to the Event Grid Topic
            var httpClient = new HttpClient();

            // Add key in the request headers
            httpClient.DefaultRequestHeaders.Add("aeg-sas-key", KEY);

            // Event grid expects event data as JSON
            var json = JsonConvert.SerializeObject(cloudEvents);

            // Create request which will be sent to the topic
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Send request
            Console.WriteLine("Sending event to Event Grid...");
            var result = await httpClient.PostAsync(TOPIC_ENDPOINT, content);

            // Show result
            Console.WriteLine($"Event sent with result: {result.ReasonPhrase}");
            Console.WriteLine();
        }
    }
}
