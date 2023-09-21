﻿using System.Collections.Generic;
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
        public static int shelfId;
        public static int slotQuantity;
        public static string shelfProduct;
        public static int productId;
        public static string productName;
        public static string productCategory;
        public static string productManufacturer;
        public static string productOfCustomer;
        public static string productImageURL;
        public static int batteryUsageTimeOfRobot;
        public static int remainingBatteryOfRobot;
        public static int batteryTravelDistanceOfRobot;
        public static int productQuantity;
        public static string robotCarryingProductName;
        public static int robotCarryingProductQuantity;
        public static int orderFullillment;
        [FunctionName("negotiate")]
        public static SignalRConnectionInfo GetSignalRInfo(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req,
            [SignalRConnectionInfo(HubName = "dttelemetry")] SignalRConnectionInfo connectionInfo)
        {
            return connectionInfo;
        }
        [FunctionName("broadcast")]
        public static Task SendMessage(
            [EventGridTrigger] EventGridEvent eventGridEvent,
            [SignalR(HubName = "dttelemetry")] IAsyncCollector<SignalRMessage> signalRMessages,
            ILogger log)
        {
            JObject eventGridData = (JObject)JsonConvert.DeserializeObject(eventGridEvent.Data.ToString());
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
                });
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
                        {"warehouseid", warehouseid },
                        {"timeInterval", timeInterval },
                        {"shelfId", shelfId },
                        {"slotQuantity", slotQuantity },
                        {"shelfProduct", shelfProduct },
                        {"productId", productId },
                        {"productName", productName },
                        {"productCategory", productCategory },
                        {"productManufacturer", productManufacturer },
                        {"productOfCustomer", productOfCustomer },
                        {"productImageURL", productImageURL },
                        {"batteryUsageTimeOfRobot", batteryUsageTimeOfRobot },
                        {"remainingBatteryOfRobot", remainingBatteryOfRobot },
                        {"batteryTravelDistanceOfRobot", batteryTravelDistanceOfRobot },
                        {"productQuantity", productQuantity },
                        {"robotCarryingProductName", robotCarryingProductName },
                        {"robotCarryingProductQuantity", robotCarryingProductQuantity },
                        {"orderFullillment", orderFullillment }
                    };
                    return signalRMessages.AddAsync(
                        new SignalRMessage
                        {
                            Target = "PropertyMessage",
                            Arguments = new[] { property }
                        });
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