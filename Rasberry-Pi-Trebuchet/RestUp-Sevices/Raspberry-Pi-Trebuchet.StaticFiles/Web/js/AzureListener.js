////__________________________________________________
/// Azure Status Results
///____________________________________________________
var AzureRegisterDeviceResult = function (data, status) {
    $("#AzureRegisterDeviceStatusesResult").html(data);
}

var AzureMSGListnerResult = function (data, status) {
    $("#AzureMSGListnerStatusResult").html(data);
}

var AzureMSGListnerActionResult = function (data, status) {
    $("#AzureMSGListenerChangeResult").html(data);
}


$(document).ready(function () {
    
    //Click event to retrieve all light statuses
    $("#ButtonAzureRegisterDevice").click(function () {
     
        var WebServiceUrl = "/api/azuremsglistener/registerdevice";       
        aClient = new HttpClient();
        aClient.put(WebServiceUrl,true, AzureRegisterDeviceResult);
    });

    $("#ButtonGetMSGListnerStatus").click(function () {
        var url = "/api/azuremsglistener/status?=" + new Date().getTime();
        aClient = new HttpClient();
        aClient.get(url, AzureMSGListnerResult);
    });


    $("#ButtonStartAzureMSGListener").click(function () {
        var WebServiceUrl = "/api/azuremsglistener/start";
        aClient = new HttpClient();
        aClient.put(WebServiceUrl, null, AzureMSGListnerActionResult);
    });

    $("#ButtonStopAzureMSGListener").click(function () {
        var WebServiceUrl = "/api/azuremsglistener/stop";
        aClient = new HttpClient();
        aClient.put(WebServiceUrl, null, AzureMSGListnerActionResult);
    });

});