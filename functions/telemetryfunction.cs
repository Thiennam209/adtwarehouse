using Azure;
using Azure.Core.Pipeline;
using Azure.DigitalTwins.Core;
using Azure.Identity;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Collections.Generic;

namespace My.Function
{
    // This class processes telemetry events from IoT Hub, reads temperature of a device
    // and sets the "Temperature" property of the device with the value of the telemetry.
    public class telemetryfunction
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private static string adtServiceUrl = Environment.GetEnvironmentVariable("ADT_SERVICE_URL");

        [FunctionName("telemetryfunction")]
        public async void Run([EventGridTrigger] EventGridEvent eventGridEvent, ILogger log)
        {
            try
            {
                var credentials = new DefaultAzureCredential();
                log.LogInformation(credentials.ToString());
                DigitalTwinsClient client = new DigitalTwinsClient(
                    new Uri(adtServiceUrl), credentials, new DigitalTwinsClientOptions
                    { Transport = new HttpClientTransport(httpClient) });
                log.LogInformation($"ADT service client connection created.");
                JObject deviceMessage = (JObject)JsonConvert.DeserializeObject(eventGridEvent.Data.ToString());
                string deviceId = "warehouseid1";
                var ID = "warehouseid1";
                var timeInterval = deviceMessage["body"]["timeInterval"] != null ? deviceMessage["body"]["timeInterval"] : "";
                var shelfId1 = deviceMessage["body"]["shelfId1"] != null ? deviceMessage["body"]["shelfId1"] : "";
				var shelfId2 = deviceMessage["body"]["shelfId2"] != null ? deviceMessage["body"]["shelfId2"] : "";
                var slotQuantity1 = deviceMessage["body"]["slotQuantity1"] != null ? deviceMessage["body"]["slotQuantity1"] : 0;
				var slotQuantity2 = deviceMessage["body"]["slotQuantity2"] != null ? deviceMessage["body"]["slotQuantity2"] : 0;
                var shelfProduct1 = deviceMessage["body"]["shelfProduct1"] != null ? deviceMessage["body"]["shelfProduct1"] : "";
				var shelfProduct2 = deviceMessage["body"]["shelfProduct2"] != null ? deviceMessage["body"]["shelfProduct2"] : "";
				var shelfProduct3 = deviceMessage["body"]["shelfProduct3"] != null ? deviceMessage["body"]["shelfProduct3"] : "";
				var shelfProduct4 = deviceMessage["body"]["shelfProduct4"] != null ? deviceMessage["body"]["shelfProduct4"] : "";
                var productId1 = deviceMessage["body"]["productId1"] != null ? deviceMessage["body"]["productId1"] : 0;
				var productId2 = deviceMessage["body"]["productId2"] != null ? deviceMessage["body"]["productId2"] : 0;
				var productId3 = deviceMessage["body"]["productId3"] != null ? deviceMessage["body"]["productId3"] : 0;
				var productId4 = deviceMessage["body"]["productId4"] != null ? deviceMessage["body"]["productId4"] : 0;
                var productName1 = deviceMessage["body"]["productName1"] != null ? deviceMessage["body"]["productName1"] : "";
				var productName2 = deviceMessage["body"]["productName2"] != null ? deviceMessage["body"]["productName2"] : "";
				var productName3 = deviceMessage["body"]["productName3"] != null ? deviceMessage["body"]["productName3"] : "";
				var productName4 = deviceMessage["body"]["productName4"] != null ? deviceMessage["body"]["productName4"] : "";
                var productCategory1 = deviceMessage["body"]["productCategory1"] != null ? deviceMessage["body"]["productCategory1"] : "";
				var productCategory2 = deviceMessage["body"]["productCategory2"] != null ? deviceMessage["body"]["productCategory2"] : "";
				var productCategory3 = deviceMessage["body"]["productCategory3"] != null ? deviceMessage["body"]["productCategory3"] : "";
				var productCategory4 = deviceMessage["body"]["productCategory4"] != null ? deviceMessage["body"]["productCategory4"] : "";
                var productManufacturer1 = deviceMessage["body"]["productManufacturer1"] != null ? deviceMessage["body"]["productManufacturer1"] : "";
				var productManufacturer2 = deviceMessage["body"]["productManufacturer2"] != null ? deviceMessage["body"]["productManufacturer2"] : "";
				var productManufacturer3 = deviceMessage["body"]["productManufacturer3"] != null ? deviceMessage["body"]["productManufacturer3"] : "";
				var productManufacturer4 = deviceMessage["body"]["productManufacturer4"] != null ? deviceMessage["body"]["productManufacturer4"] : "";
                var productOfCustomer1 = deviceMessage["body"]["productOfCustomer1"] != null ? deviceMessage["body"]["productOfCustomer1"] : "";
				var productOfCustomer2 = deviceMessage["body"]["productOfCustomer2"] != null ? deviceMessage["body"]["productOfCustomer2"] : "";
				var productOfCustomer3 = deviceMessage["body"]["productOfCustomer3"] != null ? deviceMessage["body"]["productOfCustomer3"] : "";
				var productOfCustomer4 = deviceMessage["body"]["productOfCustomer4"] != null ? deviceMessage["body"]["productOfCustomer4"] : "";
                var batteryUsageTimeOfRobot1 = deviceMessage["body"]["batteryUsageTimeOfRobot1"] != null ? deviceMessage["body"]["batteryUsageTimeOfRobot1"] : 0;
				var batteryUsageTimeOfRobot2 = deviceMessage["body"]["batteryUsageTimeOfRobot2"] != null ? deviceMessage["body"]["batteryUsageTimeOfRobot2"] : 0;
                var remainingBatteryOfRobot1 = deviceMessage["body"]["remainingBatteryOfRobot1"] != null ? deviceMessage["body"]["remainingBatteryOfRobot1"] : 0;
				var remainingBatteryOfRobot2 = deviceMessage["body"]["remainingBatteryOfRobot2"] != null ? deviceMessage["body"]["remainingBatteryOfRobot2"] : 0;
                var batteryTravelDistanceOfRobot1 = deviceMessage["body"]["batteryTravelDistanceOfRobot1"] != null ? deviceMessage["body"]["batteryTravelDistanceOfRobot1"] : 0;
				var batteryTravelDistanceOfRobot2 = deviceMessage["body"]["batteryTravelDistanceOfRobot2"] != null ? deviceMessage["body"]["batteryTravelDistanceOfRobot2"] : 0;
                var productQuantity1 = deviceMessage["body"]["productQuantity1"] != null ? deviceMessage["body"]["productQuantity1"] : 0;
				var productQuantity2 = deviceMessage["body"]["productQuantity2"] != null ? deviceMessage["body"]["productQuantity2"] : 0;
				var productQuantity3 = deviceMessage["body"]["productQuantity3"] != null ? deviceMessage["body"]["productQuantity3"] : 0;
				var productQuantity4 = deviceMessage["body"]["productQuantity4"] != null ? deviceMessage["body"]["productQuantity4"] : 0;
                var robotCarryingProductName1 = deviceMessage["body"]["robotCarryingProductName1"] != null ? deviceMessage["body"]["robotCarryingProductName1"] : "";
				var robotCarryingProductName2 = deviceMessage["body"]["robotCarryingProductName2"] != null ? deviceMessage["body"]["robotCarryingProductName2"] : "";
                var robotCarryingProductQuantity1 = deviceMessage["body"]["robotCarryingProductQuantity1"] != null ? deviceMessage["body"]["robotCarryingProductQuantity1"] : 0;
				var robotCarryingProductQuantity2 = deviceMessage["body"]["robotCarryingProductQuantity2"] != null ? deviceMessage["body"]["robotCarryingProductQuantity2"] : 0;
                var orderFullillment = deviceMessage["body"]["orderFullillment"] != null ? deviceMessage["body"]["orderFullillment"] : 0;

                var updateProperty = new JsonPatchDocument();
                var turbineTelemetry = new Dictionary<string, Object>()
                {
                    ["warehouseid"] = ID,
                    ["timeInterval"] = timeInterval,
                    ["shelfId1"] = shelfId1,
					["shelfId2"] = shelfId2,
                    ["slotQuantity1"] = slotQuantity1,
					["slotQuantity2"] = slotQuantity2,
                    ["shelfProduct1"] = shelfProduct1,
					["shelfProduct2"] = shelfProduct2,
                    ["productId1"] = productId1,
					["productId2"] = productId2,
					["productId3"] = productId3,
					["productId4"] = productId4,
                    ["productName1"] = productName1,
					["productName2"] = productName2,
					["productName3"] = productName3,
					["productName4"] = productName4,
                    ["productCategory1"] = productCategory1,
					["productCategory2"] = productCategory2,
					["productCategory3"] = productCategory3,
					["productCategory4"] = productCategory4,
                    ["productManufacturer1"] = productManufacturer1,
					["productManufacturer2"] = productManufacturer2,
					["productManufacturer3"] = productManufacturer3,
					["productManufacturer4"] = productManufacturer4,
                    ["productOfCustomer1"] = productOfCustomer1,
					["productOfCustomer2"] = productOfCustomer2,
					["productOfCustomer3"] = productOfCustomer3,
					["productOfCustomer4"] = productOfCustomer4,
                    ["batteryUsageTimeOfRobot1"] = batteryUsageTimeOfRobot1,
					["batteryUsageTimeOfRobot2"] = batteryUsageTimeOfRobot2,
                    ["remainingBatteryOfRobot1"] = remainingBatteryOfRobot1,
					["remainingBatteryOfRobot2"] = remainingBatteryOfRobot2,
                    ["batteryTravelDistanceOfRobot1"] = batteryTravelDistanceOfRobot1,
					["batteryTravelDistanceOfRobot2"] = batteryTravelDistanceOfRobot2,
                    ["productQuantity1"] = productQuantity1,
					["productQuantity2"] = productQuantity2,
					["productQuantity3"] = productQuantity3,
					["productQuantity4"] = productQuantity4,
                    ["robotCarryingProductName1"] = robotCarryingProductName1,
					["robotCarryingProductName2"] = robotCarryingProductName2,
                    ["robotCarryingProductQuantity1"] = robotCarryingProductQuantity1,
					["robotCarryingProductQuantity2"] = robotCarryingProductQuantity2,
                    ["orderFullillment"] = orderFullillment
                };
<<<<<<< HEAD
                updateProperty.AppendReplace("/warehouseid", ID.Value<string>());
=======
                updateProperty.AppendReplace("/warehouseid", ID);
>>>>>>> dcaac46b238c2b83b4afcc82f41a8da913a7e4c7
                updateProperty.AppendReplace("/timeInterval", timeInterval.Value<string>());
                updateProperty.AppendReplace("/shelfId1", shelfId1.Value<double>());
				updateProperty.AppendReplace("/shelfId2", shelfId2.Value<double>());
                updateProperty.AppendReplace("/slotQuantity1", slotQuantity1.Value<double>());
				updateProperty.AppendReplace("/slotQuantity2", slotQuantity2.Value<double>());
                updateProperty.AppendReplace("/shelfProduct1", shelfProduct1.Value<string>());
				updateProperty.AppendReplace("/shelfProduct2", shelfProduct2.Value<string>());
                updateProperty.AppendReplace("/productId1", productId1.Value<double>());
				updateProperty.AppendReplace("/productId2", productId2.Value<double>());
				updateProperty.AppendReplace("/productId3", productId3.Value<double>());
				updateProperty.AppendReplace("/productId4", productId4.Value<double>());
                updateProperty.AppendReplace("/productName1", productName1.Value<string>());
				updateProperty.AppendReplace("/productName2", productName2.Value<string>());
				updateProperty.AppendReplace("/productName3", productName3.Value<string>());
				updateProperty.AppendReplace("/productName4", productName4.Value<string>());
                updateProperty.AppendReplace("/productCategory1", productCategory1.Value<string>());
				updateProperty.AppendReplace("/productCategory2", productCategory2.Value<string>());
				updateProperty.AppendReplace("/productCategory3", productCategory3.Value<string>());
				updateProperty.AppendReplace("/productCategory4", productCategory4.Value<string>());
                updateProperty.AppendReplace("/productManufacturer1", productManufacturer1.Value<string>());
				updateProperty.AppendReplace("/productManufacturer2", productManufacturer2.Value<string>());
				updateProperty.AppendReplace("/productManufacturer3", productManufacturer3.Value<string>());
				updateProperty.AppendReplace("/productManufacturer4", productManufacturer4.Value<string>());
                updateProperty.AppendReplace("/productOfCustomer1", productOfCustomer1.Value<string>());
				updateProperty.AppendReplace("/productOfCustomer2", productOfCustomer2.Value<string>());
				updateProperty.AppendReplace("/productOfCustomer3", productOfCustomer3.Value<string>());
				updateProperty.AppendReplace("/productOfCustomer4", productOfCustomer4.Value<string>());
                updateProperty.AppendReplace("/batteryUsageTimeOfRobot1", batteryUsageTimeOfRobot1.Value<double>());
				updateProperty.AppendReplace("/batteryUsageTimeOfRobot2", batteryUsageTimeOfRobot2.Value<double>());
                updateProperty.AppendReplace("/remainingBatteryOfRobot1", remainingBatteryOfRobot1.Value<double>());
				updateProperty.AppendReplace("/remainingBatteryOfRobot2", remainingBatteryOfRobot2.Value<double>());
                updateProperty.AppendReplace("/batteryTravelDistanceOfRobot1", batteryTravelDistanceOfRobot1.Value<double>());
				updateProperty.AppendReplace("/batteryTravelDistanceOfRobot2", batteryTravelDistanceOfRobot2.Value<double>());
                updateProperty.AppendReplace("/productQuantity1", productQuantity1.Value<double>());
				updateProperty.AppendReplace("/productQuantity2", productQuantity2.Value<double>());
				updateProperty.AppendReplace("/productQuantity3", productQuantity3.Value<double>());
				updateProperty.AppendReplace("/productQuantity4", productQuantity4.Value<double>());
                updateProperty.AppendReplace("/robotCarryingProductName1", robotCarryingProductName1.Value<string>());
				updateProperty.AppendReplace("/robotCarryingProductName2", robotCarryingProductName2.Value<string>());
                updateProperty.AppendReplace("/robotCarryingProductQuantity1", robotCarryingProductQuantity1.Value<double>());
				updateProperty.AppendReplace("/robotCarryingProductQuantity2", robotCarryingProductQuantity2.Value<double>());
                updateProperty.AppendReplace("/orderFullillment", orderFullillment.Value<double>());
                log.LogInformation(updateProperty.ToString());
                try
                {
                    await client.PublishTelemetryAsync(deviceId, Guid.NewGuid().ToString(), JsonConvert.SerializeObject(turbineTelemetry));
                    await client.UpdateDigitalTwinAsync(deviceId, updateProperty);
                }
                catch (Exception e)
                {
                    log.LogInformation(e.Message);
                }
            }
            catch (Exception e)
            {
                log.LogInformation(e.Message);
            }
        }
    }
}