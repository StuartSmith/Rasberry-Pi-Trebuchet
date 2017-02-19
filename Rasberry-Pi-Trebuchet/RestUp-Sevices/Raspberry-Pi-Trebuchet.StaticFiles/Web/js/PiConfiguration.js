
////__________________________________________________
/// Pi Configuration status Results
///____________________________________________________
var ButtonRefreshResult = function (data, status) {
    debugger;

    var configurationValues = JSON.parse(data);

    UpdateCheckBox(configurationValues, "AllowSendingofData", "#tglAllowSendingofData");
    UpdateCheckBox(configurationValues, "AllowSendingToastLightData", "#tglAllowSendingToastLightData");
    UpdateCheckBox(configurationValues, "AllowSendingToastServoData", "#tglAllowSendingToastServoData");
    UpdateCheckBox(configurationValues, "AllowSendingUltraSonicData", "#tglAllowSendingUltraSonicData");

    UpdateTextBox(configurationValues, "AzureIOTConnectionString", "#AzureConnectinString");
    UpdateTextBox(configurationValues, "ToastWebSendURL", "#AzureToastServiceURL");
    $("#RefreshData").html("");
}

var ButtonSaveResult = function (data, status) {
    debugger;


    var configurationValues = JSON.parse(data);

    var saveValues = "";
    for (var k in configurationValues) {
        var sline = "Name = " + configurationValues[k].name + " Value = " + configurationValues[k].value + " \r\n ";
        saveValues = saveValues.concat(sline);

    }

    swal("Value Saved", saveValues, "success");    
    $("#SavingData").html("");
}

//_______________________________________
// Grab a checkbox from the html, retrieve
// its value and update an array that will eventually
// be sent back to the rest service
// ControlName: is the name of the check box control on the HTML
// ValueName: is the name of the value to save in the array
//_______________________________________
var UpdateArrarywithCheckedValue = function (arr, ControlName, ValueName) {
    var checkedvalue = $(ControlName).prop('checked');
    arr.push({ name: ValueName, value: (checkedvalue.toString()) });
}
//_______________________________________
// Grab the value of a textbox control from the html, retrieve
// its value and update an array that will eventually
// be sent back to the rest service
// ControlName: is the name of the check box control on the HTML
// ValueName: is the name of the value to save in the array
//_______________________________________
var UpdateArrarywithTextBoxValue = function (arr, ControlName, ValueName) {
    var checkedvalue = $(ControlName).prop('value');
    arr.push({ name: ValueName, value: (checkedvalue.toString()) });
}


var UpdateCheckBox = function (configurationValues, ValueName, CheckBoxName) {
    var valuePair = configurationValues.filter(function (configurationValues) { return configurationValues.name.toUpperCase() == ValueName.toUpperCase() });
    if (valuePair.length > 0) {
        if (valuePair[0].value.toUpperCase() == "true".toUpperCase())
            $(CheckBoxName).prop("checked", true);
        else
            $(CheckBoxName).prop("checked", false);
    }
}

var UpdateTextBox = function (configurationValues, ValueName, TextBoxName) {
    var valuePair = configurationValues.filter(function (configurationValues) { return configurationValues.name.toUpperCase() == ValueName.toUpperCase() });

    if (valuePair.length > 0) {
        $(TextBoxName).prop("value", valuePair[0].value);
    }
}

$(document).ready(function () {

    //Click event to retrieve all light statuses
    $("#ButtonConfigurationRefresh").click(function () {

        var url = "/api/piconfiguration?=" + new Date().getTime();
        aClient = new HttpClient();
        aClient.get(url, ButtonRefreshResult);
        $("#RefreshData").html("Refreshing Data ...");
    });

    $("#ButtonConfigurationSave").click(function () {

        var arr = [];

        UpdateArrarywithCheckedValue(arr, "#tglAllowSendingofData", "AllowSendingofData");
        UpdateArrarywithCheckedValue(arr, "#tglAllowSendingToastLightData", "AllowSendingToastLightData");
        UpdateArrarywithCheckedValue(arr, "#tglAllowSendingToastServoData", "AllowSendingToastServoData");
        UpdateArrarywithCheckedValue(arr, "#tglAllowSendingUltraSonicData", "AllowSendingUltraSonicData");

        UpdateArrarywithTextBoxValue(arr, "#AzureConnectinString", "AzureIOTConnectionString");
        UpdateArrarywithTextBoxValue(arr, "#AzureToastServiceURL", "ToastWebSendURL");

        var WebServiceUrl = "/api/piconfiguration";
        var aClient = new HttpClient();
        aClient.put(WebServiceUrl, arr, ButtonSaveResult);
        $("#SavingData").html("Saving Data ...");
    });
});