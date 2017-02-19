////__________________________________________________
/// Light status Results
///____________________________________________________
var LightStatusesResult = function (data, status) {
    $("#lightStatusesResult").html(data);
}

var LightStatusesResultLeft = function (data, status) {
    $("#lightStatuseLeftResult").html(data);
}

var LightStatusesResultRight = function (data, status) {
    $("#lightStatuseRightResult").html(data);
}

var LightStatusesChangeResultRight = function (data, status) {

    var url = "/api/lights/statuses/rightlight?=" + new Date().getTime();
    aClient = new HttpClient();
    aClient.get(url, LightStatusesChangeResultRightComplete);
}

var LightStatusesChangeResultLeft = function (data, status) {
    var url = "/api/lights/statuses/leftlight?=" + new Date().getTime();
    aClient = new HttpClient();
    aClient.get(url, LightStatusesChangeResultLeftComplete);
}

var LightStatusesChangeResultRightComplete = function (data, status) {
    $("#lightStatuseRightResultChange").html(data);
}

var LightStatusesChangeResultLeftComplete = function (data, status) {
    $("#lightStatuseLeftResultChange").html(data);
}


$(document).ready(function () {

    //Click event to retrieve all light statuses
    $("#ButtonLightStatuses").click(function () {
        var url = "/api/lights/statuses?=" + new Date().getTime();
        aClient = new HttpClient();
        aClient.get(url, LightStatusesResult);
    });

    //Click event to retrieve the status of the left light
    $("#ButtonLightStatusLeft").click(function () {
        var url = "/api/lights/statuses/leftlight?=" + new Date().getTime();
        aClient = new HttpClient();
        aClient.get(url, LightStatusesResultLeft);
    });


    $("#ButtonLightStatusRight").click(function () {
        var url = "/api/lights/statuses/rightlight?=" + new Date().getTime();
        aClient = new HttpClient();
        aClient.get(url, LightStatusesResultRight);
    });


    $("#ButtonTurnRightLightOn").click(function () {
        var WebServiceUrl = "/api/lights/statuses";
        var lightdata = { "IsLightOn": true, "Description": "RightLight", "LightPosition": 0, "LightGPIO": 7 };
        lightdata.IsLightOn = true;
        aClient = new HttpClient();
        aClient.put(WebServiceUrl, lightdata, LightStatusesChangeResultRight);
    });

    $("#ButtonTurnRightLightOff").click(function () {
        var WebServiceUrl = "/api/lights/statuses";
        var lightdata = { "IsLightOn": false, "Description": "RightLight", "LightPosition": 0, "LightGPIO": 7 };
        lightdata.IsLightOn = false;
        aClient = new HttpClient();
        aClient.put(WebServiceUrl, lightdata, LightStatusesChangeResultRight);
    });

    $("#ButtonTurnLeftLightOn").click(function () {
        var WebServiceUrl = "/api/lights/statuses";
        var lightdata = { "IsLightOn": true, "Description": "leftLight", "LightPosition": 0, "LightGPIO": 7 };
        lightdata.IsLightOn = true;
        var aClient = new HttpClient();
        aClient.put(WebServiceUrl, lightdata, LightStatusesChangeResultLeft);
    });

    $("#ButtonTurnLeftLightOff").click(function () {
        var WebServiceUrl = "/api/lights/statuses";
        var lightdata = { "IsLightOn": false, "Description": "LeftLight", "LightPosition": 0, "LightGPIO": 7 };
        lightdata.IsLightOn = false;
        aClient = new HttpClient();
        aClient.put(WebServiceUrl, lightdata, LightStatusesChangeResultLeft);
    });
});