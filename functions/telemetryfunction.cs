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
                // After this is deployed, you need to turn the Managed Identity Status to "On",
                // Grab Object Id of the function and assigned "Azure Digital Twins Owner (Preview)" role
                // to this function identity in order for this function to be authorized on ADT APIs.
                //Authenticate with Digital Twins
                var credentials = new DefaultAzureCredential();
                log.LogInformation(credentials.ToString());
                DigitalTwinsClient client = new DigitalTwinsClient(
                    new Uri(adtServiceUrl), credentials, new DigitalTwinsClientOptions
                    { Transport = new HttpClientTransport(httpClient) });
                log.LogInformation($"ADT service client connection created.");

                if (eventGridEvent != null && eventGridEvent.Data != null)
                {

                    JObject deviceMessage = (JObject)JsonConvert.DeserializeObject(eventGridEvent.Data.ToString());
                    string deviceId = (string)deviceMessage["systemProperties"]["iothub-connection-device-id"];
                    var ID = deviceMessage["body"]["warehouseid"];
                    var TimeInterval = deviceMessage["body"]["TimeInterval"];
                    var ShelfId = deviceMessage["body"]["ShelfId"];
                    var SlotOnShelf = deviceMessage["body"]["SlotOnShelf"];
                    var ProductId = deviceMessage["body"]["ProductId"];
                    var ProductName = deviceMessage["body"]["ProductName"];
                    var ProductCategory = deviceMessage["body"]["ProductCategory"];
                    var ProductQuantity = deviceMessage["body"]["ProductQuantity"];
                    var ProductManufacturer = deviceMessage["body"]["ProductManufacturer"];
                    var ProductOfCustomer = deviceMessage["body"]["ProductOfCustomer"];
                    var ProductImageURL = deviceMessage["body"]["ProductImageURL"];
                    var BatteryUsageTimeOfRobot = deviceMessage["body"]["BatteryUsageTimeOfRobot"];
                    var RemainingBatteryOfRobot = deviceMessage["body"]["RemainingBatteryOfRobot"];
                    var BatteryTravelDistanceOfRobot = deviceMessage["body"]["BatteryTravelDistanceOfRobot"];
                    var ProductQuantityByRobot = deviceMessage["body"]["ProductQuantityByRobot"];
                    var RobotCarryingProductName = deviceMessage["body"]["RobotCarryingProductName"];
                    var RobotCarryingProductQuantity = deviceMessage["body"]["RobotCarryingProductQuantity"];


                    log.LogInformation($"Device:{deviceId} Device Id is: {ID}");
                    log.LogInformation($"Device:{deviceId} Time interval is: {TimeInterval}");
                    log.LogInformation($"Device:{deviceId} SlotOnShelf is: {SlotOnShelf}");
                    log.LogInformation($"Device:{deviceId} ShelfId is: {ShelfId}");
                    log.LogInformation($"Device:{deviceId} ProductId is: {ProductId}");
                    log.LogInformation($"Device: {deviceId} ProductName is: {ProductName}");
                    log.LogInformation($"Device: {deviceId} ProductCategory is: {ProductCategory}");
                    log.LogInformation($"Device: {deviceId} ProductQuantity is: {ProductQuantity}");
                    log.LogInformation($"Device: {deviceId} ProductManufacturer is: {ProductManufacturer}");
                    log.LogInformation($"Device: {deviceId} ProductOfCustomer is: {ProductOfCustomer}");
                    log.LogInformation($"Device: {deviceId} ProductCategory is: {ProductImageURL}");
                    log.LogInformation($"Device: {deviceId} BatteryUsageTimeOfRobot is: {BatteryUsageTimeOfRobot}");
                    log.LogInformation($"Device: {deviceId} RemainingBatteryOfRobot is: {RemainingBatteryOfRobot}");
                    log.LogInformation($"Device: {deviceId} BatteryTravelDistanceOfRobot is: {BatteryTravelDistanceOfRobot}");
                    log.LogInformation($"Device: {deviceId} ProductQuantityByRobot is: {ProductQuantityByRobot}");
                    log.LogInformation($"Device: {deviceId} RobotCarryingProductName is: {RobotCarryingProductName}");
                    log.LogInformation($"Device: {deviceId} RobotCarryingProductQuantity is: {RobotCarryingProductQuantity}");

                    var updateProperty = new JsonPatchDocument();
                    var turbineTelemetry = new Dictionary<string, Object>()
                    {
                        ["warehouseid"] = ID,
                        ["TimeInterval"] = TimeInterval,
                        ["ShelfId"] = ShelfId,
                        ["SlotOnShelf"] = SlotOnShelf,
                        ["ProductId"] = ProductId,
                        ["ProductName"] = ProductName,
                        ["ProductCategory"] = ProductCategory,
                        ["ProductManufacturer"] = ProductManufacturer,
                        ["ProductOfCustomer"] = ProductOfCustomer,
                        ["ProductImageURL"] = ProductImageURL,
                        ["BatteryUsageTimeOfRobot"] = BatteryUsageTimeOfRobot,
                        ["RemainingBatteryOfRobot"] = RemainingBatteryOfRobot,
                        ["BatteryTravelDistanceOfRobot"] = BatteryTravelDistanceOfRobot,
                        ["ProductQuantityByRobot"] = ProductQuantityByRobot,
                        ["RobotCarryingProductName"] = RobotCarryingProductName,
                        ["RobotCarryingProductQuantity"] = RobotCarryingProductQuantity

                    };
                    updateProperty.AppendAdd("/warehouseid", ID.Value<string>());

                    log.LogInformation(updateProperty.ToString());
                    try
                    {
                        await client.PublishTelemetryAsync(deviceId, Guid.NewGuid().ToString(), JsonConvert.SerializeObject(turbineTelemetry));
                    }
                    catch (Exception e)
                    {
                        log.LogInformation(e.Message);
                    }
                }
            }
            catch (Exception e)
            {
                log.LogInformation(e.Message);
            }
        }
    }
}