using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace SignalRFunctions
{
    public static class SignalRFunctions
    {
        public static string warehouseid;
        public static string timeInterval;
        public static double shelfId1;
        public static double shelfId2;
        public static double slotQuantity1;
        public static double slotQuantity2;
        public static string shelfProduct1;
        public static string shelfProduct2;
        public static double productId1;
        public static double productId2;
        public static double productId3;
        public static double productId4;
        public static string productName1;
        public static string productName2;
        public static string productName3;
        public static string productName4;
        public static string productCategory1;
        public static string productCategory2;
        public static string productCategory3;
        public static string productCategory4;
        public static string productManufacturer1;
        public static string productManufacturer2;
        public static string productManufacturer3;
        public static string productManufacturer4;
        public static string productOfCustomer1;
        public static string productOfCustomer2;
        public static string productOfCustomer3;
        public static string productOfCustomer4;
        public static double batteryUsageTimeOfRobot1;
        public static double batteryUsageTimeOfRobot2;
        public static double remainingBatteryOfRobot1;
        public static double remainingBatteryOfRobot2;
        public static double batteryTravelDistanceOfRobot1;
        public static double batteryTravelDistanceOfRobot2;
        public static double productQuantity1;
        public static double productQuantity2;
        public static double productQuantity3;
        public static double productQuantity4;
        public static string robotCarryingProductName1;
        public static string robotCarryingProductName2;
        public static double robotCarryingProductQuantity1;
        public static double robotCarryingProductQuantity2;
        public static double orderFullillment;

        [FunctionName("negotiate")]
        public static SignalRConnectionInfo GetSignalRInfo(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req,
            [SignalRConnectionInfo(HubName = "dttelemetry")] SignalRConnectionInfo connectionInfo
        )
        {
            return connectionInfo;
        }

        [FunctionName("broadcast")]
        public static Task SendMessage(
            [EventGridTrigger] EventGridEvent eventGridEvent,
            [SignalR(HubName = "dttelemetry")] IAsyncCollector<SignalRMessage> signalRMessages,
            ILogger log
        )
        {
            JObject eventGridData = (JObject)
                JsonConvert.DeserializeObject(eventGridEvent.Data.ToString());
            if (eventGridEvent.EventType.Contains("telemetry"))
            {
                var data = eventGridData.SelectToken("data");

                var telemetryMessage = new Dictionary<object, object>();
                foreach (JProperty property in data.Children())
                {
                    log.LogInformation(property.Name + " - " + property.Value);
                    telemetryMessage.Add(property.Name, property.Value);
                }
                return signalRMessages.AddAsync(
                    new SignalRMessage
                    {
                        Target = "TelemetryMessage",
                        Arguments = new[] { telemetryMessage }
                    }
                );
            }
            else
            {
                try
                {
                    warehouseid = eventGridEvent.Subject;

                    var data = eventGridData.SelectToken("data");
                    var patch = data.SelectToken("patch");
                    var property = new Dictionary<object, object>
                    {
                        { "warehouseid", warehouseid },
                        { "timeInterval", timeInterval },
                        { "shelfId1", shelfId1 },
                        { "shelfId2", shelfId2 },
                        { "slotQuantity1", slotQuantity1 },
                        { "slotQuantity2", slotQuantity2 },
                        { "shelfProduct1", shelfProduct1 },
                        { "shelfProduct2", shelfProduct2 },
                        { "productId1", productId1 },
                        { "productId2", productId2 },
                        { "productId3", productId3 },
                        { "productId4", productId4 },
                        { "productName1", productName1 },
                        { "productName2", productName2 },
                        { "productName3", productName3 },
                        { "productName4", productName4 },
                        { "productCategory1", productCategory1 },
                        { "productCategory2", productCategory2 },
                        { "productCategory3", productCategory3 },
                        { "productCategory4", productCategory4 },
                        { "productManufacturer1", productManufacturer1 },
                        { "productManufacturer2", productManufacturer2 },
                        { "productManufacturer3", productManufacturer3 },
                        { "productManufacturer4", productManufacturer4 },
                        { "productOfCustomer1", productOfCustomer1 },
                        { "productOfCustomer2", productOfCustomer2 },
                        { "productOfCustomer3", productOfCustomer3 },
                        { "productOfCustomer4", productOfCustomer4 },
                        { "batteryUsageTimeOfRobot1", batteryUsageTimeOfRobot1 },
                        { "batteryUsageTimeOfRobot2", batteryUsageTimeOfRobot2 },
                        { "remainingBatteryOfRobot1", remainingBatteryOfRobot1 },
                        { "remainingBatteryOfRobot2", remainingBatteryOfRobot2 },
                        { "batteryTravelDistanceOfRobot1", batteryTravelDistanceOfRobot1 },
                        { "batteryTravelDistanceOfRobot2", batteryTravelDistanceOfRobot2 },
                        { "productQuantity1", productQuantity1 },
                        { "productQuantity2", productQuantity2 },
                        { "productQuantity3", productQuantity3 },
                        { "productQuantity4", productQuantity4 },
                        { "robotCarryingProductName1", robotCarryingProductName1 },
                        { "robotCarryingProductName2", robotCarryingProductName2 },
                        { "robotCarryingProductQuantity1", robotCarryingProductQuantity1 },
                        { "robotCarryingProductQuantity2", robotCarryingProductQuantity2 },
                        { "orderFullillment", orderFullillment }
                    };
                    return signalRMessages.AddAsync(
                        new SignalRMessage
                        {
                            Target = "PropertyMessage",
                            Arguments = new[] { property }
                        }
                    );
                }
                catch (Exception e)
                {
                    log.LogInformation(e.Message);
                    return null;
                }
            }
        }
    }
}
