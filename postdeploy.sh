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

    echo "Create Device warehouseid1"
    az dt twin create -n $adtname --dtmi $warehouseid --twin-id "warehouseid1"
    az dt twin update -n $adtname --twin-id "warehouseid1" --json-patch '[{"op":"add", "path":"/warehouseid", "value": "'"warehouseid1"'"},
    {"op":"add", "path":"/timeInterval", "value": ""},{"op":"add", "path":"/shelfId", "value": 0},{"op":"add", "path":"/slotQuantity", "value": 0},
    {"op":"add", "path":"/shelfProduct", "value": ""},{"op":"add", "path":"/productId", "value": 0},{"op":"add", "path":"/productName", "value": ""},
    {"op":"add", "path":"/productCategory", "value": ""},{"op":"add", "path":"/productManufacturer", "value": ""},{"op":"add", "path":"/productOfCustomer", "value": ""},
    {"op":"add", "path":"/productImageURL", "value": ""},{"op":"add", "path":"/batteryUsageTimeOfRobot", "value": 0},{"op":"add", "path":"/remainingBatteryOfRobot", "value": 0},
    {"op":"add", "path":"/batteryTravelDistanceOfRobot", "value": 0},{"op":"add", "path":"/productQuantity", "value": 0},{"op":"add", "path":"/robotCarryingProductName", "value": ""},
    {"op":"add", "path":"/robotCarryingProductQuantity", "value": 0}, ,{"op":"add", "path":"/orderFullillment", "value": 0}]'

echo "Create Device warehouseid2"
    az dt twin create -n $adtname --dtmi $warehouseid --twin-id "warehouseid2"
    az dt twin update -n $adtname --twin-id "warehouseid2" --json-patch '[{"op":"add", "path":"/warehouseid", "value": "'"warehouseid2"'"},
    {"op":"add", "path":"/timeInterval", "value": ""},{"op":"add", "path":"/shelfId", "value": 0},{"op":"add", "path":"/slotQuantity", "value": 0},
    {"op":"add", "path":"/shelfProduct", "value": ""},{"op":"add", "path":"/productId", "value": 0},{"op":"add", "path":"/productName", "value": ""},
    {"op":"add", "path":"/productCategory", "value": ""},{"op":"add", "path":"/productManufacturer", "value": ""},{"op":"add", "path":"/productOfCustomer", "value": ""},
    {"op":"add", "path":"/productImageURL", "value": ""},{"op":"add", "path":"/batteryUsageTimeOfRobot", "value": 0},{"op":"add", "path":"/remainingBatteryOfRobot", "value": 0},
    {"op":"add", "path":"/batteryTravelDistanceOfRobot", "value": 0},{"op":"add", "path":"/productQuantity", "value": 0},{"op":"add", "path":"/robotCarryingProductName", "value": ""},
    {"op":"add", "path":"/robotCarryingProductQuantity", "value": 0}, ,{"op":"add", "path":"/orderFullillment", "value": 0}]'

    echo "Create Device warehouseid3"
    az dt twin create -n $adtname --dtmi $warehouseid --twin-id "warehouseid3"
    az dt twin update -n $adtname --twin-id "warehouseid3" --json-patch '[{"op":"add", "path":"/warehouseid", "value": "'"warehouseid3"'"},
    {"op":"add", "path":"/timeInterval", "value": ""},{"op":"add", "path":"/shelfId", "value": 0},{"op":"add", "path":"/slotQuantity", "value": 0},
    {"op":"add", "path":"/shelfProduct", "value": ""},{"op":"add", "path":"/productId", "value": 0},{"op":"add", "path":"/productName", "value": ""},
    {"op":"add", "path":"/productCategory", "value": ""},{"op":"add", "path":"/productManufacturer", "value": ""},{"op":"add", "path":"/productOfCustomer", "value": ""},
    {"op":"add", "path":"/productImageURL", "value": ""},{"op":"add", "path":"/batteryUsageTimeOfRobot", "value": 0},{"op":"add", "path":"/remainingBatteryOfRobot", "value": 0},
    {"op":"add", "path":"/batteryTravelDistanceOfRobot", "value": 0},{"op":"add", "path":"/productQuantity", "value": 0},{"op":"add", "path":"/robotCarryingProductName", "value": ""},
    {"op":"add", "path":"/robotCarryingProductQuantity", "value": 0}, ,{"op":"add", "path":"/orderFullillment", "value": 0}]'

    echo "Create Device warehouseid4"
    az dt twin create -n $adtname --dtmi $warehouseid --twin-id "warehouseid4"
    az dt twin update -n $adtname --twin-id "warehouseid4" --json-patch '[{"op":"add", "path":"/warehouseid", "value": "'"warehouseid4"'"},
    {"op":"add", "path":"/timeInterval", "value": ""},{"op":"add", "path":"/shelfId", "value": 0},{"op":"add", "path":"/slotQuantity", "value": 0},
    {"op":"add", "path":"/shelfProduct", "value": ""},{"op":"add", "path":"/productId", "value": 0},{"op":"add", "path":"/productName", "value": ""},
    {"op":"add", "path":"/productCategory", "value": ""},{"op":"add", "path":"/productManufacturer", "value": ""},{"op":"add", "path":"/productOfCustomer", "value": ""},
    {"op":"add", "path":"/productImageURL", "value": ""},{"op":"add", "path":"/batteryUsageTimeOfRobot", "value": 0},{"op":"add", "path":"/remainingBatteryOfRobot", "value": 0},
    {"op":"add", "path":"/batteryTravelDistanceOfRobot", "value": 0},{"op":"add", "path":"/productQuantity", "value": 0},{"op":"add", "path":"/robotCarryingProductName", "value": ""},
    {"op":"add", "path":"/robotCarryingProductQuantity", "value": 0}, ,{"op":"add", "path":"/orderFullillment", "value": 0}]'

    echo "Create Device warehouseid5"
    az dt twin create -n $adtname --dtmi $warehouseid --twin-id "warehouseid5"
    az dt twin update -n $adtname --twin-id "warehouseid5" --json-patch '[{"op":"add", "path":"/warehouseid", "value": "'"warehouseid5"'"},
    {"op":"add", "path":"/timeInterval", "value": ""},{"op":"add", "path":"/shelfId", "value": 0},{"op":"add", "path":"/slotQuantity", "value": 0},
    {"op":"add", "path":"/shelfProduct", "value": ""},{"op":"add", "path":"/productId", "value": 0},{"op":"add", "path":"/productName", "value": ""},
    {"op":"add", "path":"/productCategory", "value": ""},{"op":"add", "path":"/productManufacturer", "value": ""},{"op":"add", "path":"/productOfCustomer", "value": ""},
    {"op":"add", "path":"/productImageURL", "value": ""},{"op":"add", "path":"/batteryUsageTimeOfRobot", "value": 0},{"op":"add", "path":"/remainingBatteryOfRobot", "value": 0},
    {"op":"add", "path":"/batteryTravelDistanceOfRobot", "value": 0},{"op":"add", "path":"/productQuantity", "value": 0},{"op":"add", "path":"/robotCarryingProductName", "value": ""},
    {"op":"add", "path":"/robotCarryingProductQuantity", "value": 0}, ,{"op":"add", "path":"/orderFullillment", "value": 0}]'

    echo "Create Device warehouseid6"
    az dt twin create -n $adtname --dtmi $warehouseid --twin-id "warehouseid6"
    az dt twin update -n $adtname --twin-id "warehouseid6" --json-patch '[{"op":"add", "path":"/warehouseid", "value": "'"warehouseid6"'"},
    {"op":"add", "path":"/timeInterval", "value": ""},{"op":"add", "path":"/shelfId", "value": 0},{"op":"add", "path":"/slotQuantity", "value": 0},
    {"op":"add", "path":"/shelfProduct", "value": ""},{"op":"add", "path":"/productId", "value": 0},{"op":"add", "path":"/productName", "value": ""},
    {"op":"add", "path":"/productCategory", "value": ""},{"op":"add", "path":"/productManufacturer", "value": ""},{"op":"add", "path":"/productOfCustomer", "value": ""},
    {"op":"add", "path":"/productImageURL", "value": ""},{"op":"add", "path":"/batteryUsageTimeOfRobot", "value": 0},{"op":"add", "path":"/remainingBatteryOfRobot", "value": 0},
    {"op":"add", "path":"/batteryTravelDistanceOfRobot", "value": 0},{"op":"add", "path":"/productQuantity", "value": 0},{"op":"add", "path":"/robotCarryingProductName", "value": ""},
    {"op":"add", "path":"/robotCarryingProductQuantity", "value": 0}, ,{"op":"add", "path":"/orderFullillment", "value": 0}]'

    echo "Create Device warehouseid7"
    az dt twin create -n $adtname --dtmi $warehouseid --twin-id "warehouseid7"
    az dt twin update -n $adtname --twin-id "warehouseid7" --json-patch '[{"op":"add", "path":"/warehouseid", "value": "'"warehouseid7"'"},
    {"op":"add", "path":"/timeInterval", "value": ""},{"op":"add", "path":"/shelfId", "value": 0},{"op":"add", "path":"/slotQuantity", "value": 0},
    {"op":"add", "path":"/shelfProduct", "value": ""},{"op":"add", "path":"/productId", "value": 0},{"op":"add", "path":"/productName", "value": ""},
    {"op":"add", "path":"/productCategory", "value": ""},{"op":"add", "path":"/productManufacturer", "value": ""},{"op":"add", "path":"/productOfCustomer", "value": ""},
    {"op":"add", "path":"/productImageURL", "value": ""},{"op":"add", "path":"/batteryUsageTimeOfRobot", "value": 0},{"op":"add", "path":"/remainingBatteryOfRobot", "value": 0},
    {"op":"add", "path":"/batteryTravelDistanceOfRobot", "value": 0},{"op":"add", "path":"/productQuantity", "value": 0},{"op":"add", "path":"/robotCarryingProductName", "value": ""},
    {"op":"add", "path":"/robotCarryingProductQuantity", "value": 0}, ,{"op":"add", "path":"/orderFullillment", "value": 0}]'

    echo "Create Device warehouseid8"
    az dt twin create -n $adtname --dtmi $warehouseid --twin-id "warehouseid8"
    az dt twin update -n $adtname --twin-id "warehouseid8" --json-patch '[{"op":"add", "path":"/warehouseid", "value": "'"warehouseid8"'"},
    {"op":"add", "path":"/timeInterval", "value": ""},{"op":"add", "path":"/shelfId", "value": 0},{"op":"add", "path":"/slotQuantity", "value": 0},
    {"op":"add", "path":"/shelfProduct", "value": ""},{"op":"add", "path":"/productId", "value": 0},{"op":"add", "path":"/productName", "value": ""},
    {"op":"add", "path":"/productCategory", "value": ""},{"op":"add", "path":"/productManufacturer", "value": ""},{"op":"add", "path":"/productOfCustomer", "value": ""},
    {"op":"add", "path":"/productImageURL", "value": ""},{"op":"add", "path":"/batteryUsageTimeOfRobot", "value": 0},{"op":"add", "path":"/remainingBatteryOfRobot", "value": 0},
    {"op":"add", "path":"/batteryTravelDistanceOfRobot", "value": 0},{"op":"add", "path":"/productQuantity", "value": 0},{"op":"add", "path":"/robotCarryingProductName", "value": ""},
    {"op":"add", "path":"/robotCarryingProductQuantity", "value": 0}, ,{"op":"add", "path":"/orderFullillment", "value": 0}]'



# az eventgrid topic create -g $rgname --name $egname -l $location
az dt endpoint create eventgrid --dt-name $adtname --eventgrid-resource-group $rgname --eventgrid-topic $egname --endpoint-name "$egname-ep"
az dt route create --dt-name $adtname --endpoint-name "$egname-ep" --route-name "$egname-rt"

# Create Subscriptions
az eventgrid event-subscription create --name "$egname-broadcast-sub" --source-resource-id $egid --endpoint "$funcappid/functions/broadcast" --endpoint-type azurefunction

# Retrieve and Upload models to blob storage
az storage blob upload-batch --account-name $storagename -d $containername -s "./adtwarehouse/assets"