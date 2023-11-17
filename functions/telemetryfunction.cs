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
                string deviceId = (string)deviceMessage["systemProperties"]["iothub-connection-device-id"];
                var ID = deviceMessage["body"]["warehouseid"] != null ? deviceMessage["body"]["warehouseid"] : "";
                var timeInterval = deviceMessage["body"]["timeInterval"] != null ? deviceMessage["body"]["timeInterval"] : "";
                var shelfId = deviceMessage["body"]["shelfId"] != null ? deviceMessage["body"]["shelfId"] : "";
                var slotQuantity = deviceMessage["body"]["slotQuantity"] != null ? deviceMessage["body"]["slotQuantity"] : 0;
                var shelfProduct = deviceMessage["body"]["shelfProduct"] != null ? deviceMessage["body"]["shelfProduct"] : "";
                var productId = deviceMessage["body"]["productId"] != null ? deviceMessage["body"]["productId"] : 0;
                var productName = deviceMessage["body"]["productName"] != null ? deviceMessage["body"]["productName"] : "";
                var productCategory = deviceMessage["body"]["productCategory"] != null ? deviceMessage["body"]["productCategory"] : "";
                var productManufacturer = deviceMessage["body"]["productManufacturer"] != null ? deviceMessage["body"]["productManufacturer"] : "";
                var productOfCustomer = deviceMessage["body"]["productOfCustomer"] != null ? deviceMessage["body"]["productOfCustomer"] : "";
                var productImageURL = deviceMessage["body"]["productImageURL"] != null ? deviceMessage["body"]["productImageURL"] : "";
                var batteryUsageTimeOfRobot = deviceMessage["body"]["batteryUsageTimeOfRobot"] != null ? deviceMessage["body"]["batteryUsageTimeOfRobot"] : 0;
                var remainingBatteryOfRobot = deviceMessage["body"]["remainingBatteryOfRobot"] != null ? deviceMessage["body"]["remainingBatteryOfRobot"] : 0;
                var batteryTravelDistanceOfRobot = deviceMessage["body"]["batteryTravelDistanceOfRobot"] != null ? deviceMessage["body"]["batteryTravelDistanceOfRobot"] : 0;
                var productQuantity = deviceMessage["body"]["productQuantity"] != null ? deviceMessage["body"]["productQuantity"] : 0;
                var robotCarryingProductName = deviceMessage["body"]["robotCarryingProductName"] != null ? deviceMessage["body"]["robotCarryingProductName"] : "";
                var robotCarryingProductQuantity = deviceMessage["body"]["robotCarryingProductQuantity"] != null ? deviceMessage["body"]["robotCarryingProductQuantity"] : 0;
                var orderFullillment = deviceMessage["body"]["orderFullillment"] != null ? deviceMessage["body"]["orderFullillment"] : 0;
                log.LogInformation($"Device:{deviceId} Device Id is: {ID}");
                log.LogInformation($"Device:{deviceId} Time interval is: {timeInterval}");
                log.LogInformation($"Device:{deviceId} SlotQuantity is: {slotQuantity}");
                log.LogInformation($"Device:{deviceId} ShelfProduct is: {shelfProduct}");
                log.LogInformation($"Device:{deviceId} ShelfId is: {shelfId}");
                log.LogInformation($"Device:{deviceId} ProductId is: {productId}");
                log.LogInformation($"Device: {deviceId} ProductName is: {productName}");
                log.LogInformation($"Device: {deviceId} ProductCategory is: {productCategory}");
                log.LogInformation($"Device: {deviceId} ProductManufacturer is: {productManufacturer}");
                log.LogInformation($"Device: {deviceId} ProductOfCustomer is: {productOfCustomer}");
                log.LogInformation($"Device: {deviceId} ProductCategory is: {productImageURL}");
                log.LogInformation($"Device: {deviceId} BatteryUsageTimeOfRobot is: {batteryUsageTimeOfRobot}");
                log.LogInformation($"Device: {deviceId} RemainingBatteryOfRobot is: {remainingBatteryOfRobot}");
                log.LogInformation($"Device: {deviceId} BatteryTravelDistanceOfRobot is: {batteryTravelDistanceOfRobot}");
                log.LogInformation($"Device: {deviceId} ProductQuantity is: {productQuantity}");
                log.LogInformation($"Device: {deviceId} RobotCarryingProductName is: {robotCarryingProductName}");
                log.LogInformation($"Device: {deviceId} RobotCarryingProductQuantity is: {robotCarryingProductQuantity}");
                log.LogInformation($"Device: {deviceId} OrderFullillment is: {orderFullillment}");
                var updateProperty = new JsonPatchDocument();
                var turbineTelemetry = new Dictionary<string, Object>()
                {
                    ["warehouseid"] = ID,
                    ["timeInterval"] = timeInterval,
                    ["shelfId"] = shelfId,
                    ["slotQuantity"] = slotQuantity,
                    ["shelfProduct"] = shelfProduct,
                    ["productId"] = productId,
                    ["productName"] = productName,
                    ["productCategory"] = productCategory,
                    ["productManufacturer"] = productManufacturer,
                    ["productOfCustomer"] = productOfCustomer,
                    ["productImageURL"] = productImageURL,
                    ["batteryUsageTimeOfRobot"] = batteryUsageTimeOfRobot,
                    ["remainingBatteryOfRobot"] = remainingBatteryOfRobot,
                    ["batteryTravelDistanceOfRobot"] = batteryTravelDistanceOfRobot,
                    ["productQuantity"] = productQuantity,
                    ["robotCarryingProductName"] = robotCarryingProductName,
                    ["robotCarryingProductQuantity"] = robotCarryingProductQuantity,
                    ["orderFullillment"] = orderFullillment
                };
                updateProperty.AppendReplace("/warehouseid", ID.Value<string>());
                updateProperty.AppendReplace("/timeInterval", timeInterval.Value<string>());
                updateProperty.AppendReplace("/shelfId", shelfId.Value<int>());
                updateProperty.AppendReplace("/slotQuantity", slotQuantity.Value<int>());
                updateProperty.AppendReplace("/shelfProduct", shelfProduct.Value<string>());
                updateProperty.AppendReplace("/productId", productId.Value<int>());
                updateProperty.AppendReplace("/productName", productName.Value<string>());
                updateProperty.AppendReplace("/productCategory", productCategory.Value<string>());
                updateProperty.AppendReplace("/productManufacturer", productManufacturer.Value<string>());
                updateProperty.AppendReplace("/productOfCustomer", productOfCustomer.Value<string>());
                updateProperty.AppendReplace("/productImageURL", productImageURL.Value<string>());
                updateProperty.AppendReplace("/batteryUsageTimeOfRobot", batteryUsageTimeOfRobot.Value<int>());
                updateProperty.AppendReplace("/remainingBatteryOfRobot", remainingBatteryOfRobot.Value<int>());
                updateProperty.AppendReplace("/batteryTravelDistanceOfRobot", batteryTravelDistanceOfRobot.Value<int>());
                updateProperty.AppendReplace("/productQuantity", productQuantity.Value<int>());
                updateProperty.AppendReplace("/robotCarryingProductName", robotCarryingProductName.Value<string>());
                updateProperty.AppendReplace("/robotCarryingProductQuantity", robotCarryingProductQuantity.Value<int>());
                updateProperty.AppendReplace("/orderFullillment", orderFullillment.Value<int>());
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