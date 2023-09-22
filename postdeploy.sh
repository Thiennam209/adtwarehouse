iothubname=$1
adtname=$2
rgname=$3
location=$4
egname=$5
egid=$6
funcappid=$7
storagename=$8
containername=$9

echo "iot hub name: ${iothubname}"
echo "adt name: ${adtname}"
echo "rg name: ${rgname}"
echo "location: ${location}"
echo "egname: ${egname}"
echo "egid: ${egid}"
echo "funcappid: ${funcappid}"
echo "storagename: ${storagename}"
echo "containername: ${containername}"

# echo 'installing azure cli extension'
az config set extension.use_dynamic_install=yes_without_prompt
az extension add --name azure-iot -y

# echo 'retrieve files'
git clone https://github.com/Thiennam209/adtwarehouse

# echo 'input model'
warehouseid=$(az dt model create -n $adtname --models ./adtwarehouse/models/warehouse.json --query [].id -o tsv)

# echo 'instantiate ADT Instances'


    echo "Create Warehouse warehouseid1"
    az dt twin create -n $adtname --dtmi $warehouseid --twin-id "warehouseid1"
    az dt twin update -n $adtname --twin-id "warehouseid1" --json-patch '[{"op":"add", "path":"/warehouseid", "value": "'"warehouseid1"'"},
    {"op":"add", "path":"/timeInterval", "value": ""},{"op":"add", "path":"/shelfId1", "value": 0},{"op":"add", "path":"/shelfId2", "value": 0},{"op":"add", "path":"/slotQuantity1", "value": 0},{"op":"add", "path":"/slotQuantity2", "value": 0},
    {"op":"add", "path":"/shelfProduct1", "value": ""},{"op":"add", "path":"/shelfProduct2", "value": ""},{"op":"add", "path":"/productId1", "value": 0},{"op":"add", "path":"/productId2", "value": 0},{"op":"add", "path":"/productId3", "value": 0},{"op":"add", "path":"/productId4", "value": 0},
    {"op":"add", "path":"/productName1", "value": ""},{"op":"add", "path":"/productName2", "value": ""},{"op":"add", "path":"/productName3", "value": ""},{"op":"add", "path":"/productName4", "value": ""},
    {"op":"add", "path":"/productCategory1", "value": ""},{"op":"add", "path":"/productCategory2", "value": ""},{"op":"add", "path":"/productCategory3", "value": ""},{"op":"add", "path":"/productCategory4", "value": ""},
    {"op":"add", "path":"/productManufacturer1", "value": ""},{"op":"add", "path":"/productManufacturer2", "value": ""},{"op":"add", "path":"/productManufacturer3", "value": ""},{"op":"add", "path":"/productManufacturer4", "value": ""},
    {"op":"add", "path":"/productOfCustomer1", "value": ""},{"op":"add", "path":"/productOfCustomer2", "value": ""},{"op":"add", "path":"/productOfCustomer3", "value": ""},{"op":"add", "path":"/productOfCustomer4", "value": ""},
    {"op":"add", "path":"/batteryUsageTimeOfRobot1", "value": 0},{"op":"add", "path":"/batteryUsageTimeOfRobot2", "value": 0},{"op":"add", "path":"/remainingBatteryOfRobot1", "value": 0},{"op":"add", "path":"/remainingBatteryOfRobot2", "value": 0}
    {"op":"add", "path":"/batteryTravelDistanceOfRobot1", "value": 0},{"op":"add", "path":"/batteryTravelDistanceOfRobot2", "value": 0},
    {"op":"add", "path":"/productQuantity1", "value": 0},{"op":"add", "path":"/productQuantity2", "value": 0},{"op":"add", "path":"/productQuantity3", "value": 0},{"op":"add", "path":"/productQuantity4", "value": 0},
    {"op":"add", "path":"/robotCarryingProductName1", "value": ""},{"op":"add", "path":"/robotCarryingProductName2", "value": ""},
    {"op":"add", "path":"/robotCarryingProductQuantity1", "value": 0},{"op":"add", "path":"/robotCarryingProductQuantity2", "value": 0},{"op":"add", "path":"/orderFullillment", "value": 0}]'


# az eventgrid topic create -g $rgname --name $egname -l $location
az dt endpoint create eventgrid --dt-name $adtname --eventgrid-resource-group $rgname --eventgrid-topic $egname --endpoint-name "$egname-ep"
az dt route create --dt-name $adtname --endpoint-name "$egname-ep" --route-name "$egname-rt"

# Create Subscriptions
az eventgrid event-subscription create --name "$egname-broadcast-sub" --source-resource-id $egid --endpoint "$funcappid/functions/broadcast" --endpoint-type azurefunction

# Retrieve and Upload models to blob storage
az storage blob upload-batch --account-name $storagename -d $containername -s "./adtwarehouse/assets"
