var HttpClient = function () {


    this.get = function (aUrl, aCallback) {
        var anHttpRequest = new XMLHttpRequest();
        anHttpRequest.onreadystatechange = function () {
            if (anHttpRequest.readyState == 4 && anHttpRequest.status == 200)
                aCallback(anHttpRequest.responseText);
        }

        anHttpRequest.open("GET", aUrl, true);
        anHttpRequest.setRequestHeader('Cache-Control', 'no-cache');
        anHttpRequest.send(null);

    }

    this.put = function (aUrl, data, aCallback) {
        var anHttpRequest = new XMLHttpRequest();

        anHttpRequest.onreadystatechange = function () {
            //if ((anHttpRequest.readyState == 4 && anHttpRequest.status == 200) ||
            //    (anHttpRequest.readyState == 4 && anHttpRequest.status == 201))
            //    aCallback(anHttpRequest.responseText);
        }

        var jsondata = JSON.stringify(data);

        anHttpRequest.open("POST", aUrl, true);

        anHttpRequest.setRequestHeader("Accept", "application/json");
        anHttpRequest.setRequestHeader("Content-Type", "application/json");
        anHttpRequest.setRequestHeader("Content-length", jsondata.length);

        anHttpRequest.send(jsondata);

        aCallback(jsondata);
    }
}

function generateUUID() {
    var d = new Date().getTime();
    if (window.performance && typeof window.performance.now === "function") {
        d += performance.now(); //use high-precision timer if available
    }
    var uuid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = (d + Math.random() * 16) % 16 | 0;
        d = Math.floor(d / 16);
        return (c == 'x' ? r : (r & 0x3 | 0x8)).toString(16);
    });
    return uuid;
}

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

////__________________________________________________
/// Servo Status Result
///____________________________________________________
var ServoStatus = function (data, status) {
    $("#ServoStatus").html(data);
}

var ServoStatusResult = function (data, status) {
    var url = "/api/servo/statuses?=" + new Date().getTime();
    aClient = new HttpClient();
    aClient.get(url, ServoStatusResultComplete);
}

var ServoStatusResultComplete = function (data, status) {
    $("#ServoChangeStatusResults").html(data);

}
////__________________________________________________
/// Ultra Sonic Result
///____________________________________________________
var UltraSonicStatus = function (data, status) {
    $("#UltraSonicStatusResult").html(data);
}
////__________________________________________________
/// End of results
///____________________________________________________



$(document).ready(function () {
    $("#ButtonLightStatuses").click(function () {
        var url = "/api/lights/statuses?=" + new Date().getTime();
        aClient = new HttpClient();
        aClient.get(url, LightStatusesResult);
    });
});


$(document).ready(function () {
    //Click event to retrieve all light statuses
    //$("#ButtonLightStatuses").click(function () {
    //    var url = "/api/lights/statuses?=" + new Date().getTime();
    //    aClient = new HttpClient();
    //    aClient.get(url, LightStatusesResult);
    //});

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

    //____________________________________________________________________________
    //Servo Statuses and Position
    //____________________________________________________________________________

    $("#ButtonServoStatus").click(function () {
        var url = "/api/servo/statuses?=" + new Date().getTime();
        aClient = new HttpClient();
        aClient.get(url, ServoStatus);
    });

    $("#ButtonServoStatus0").click(function () {
        var WebServiceUrl = "/api/servo/statuses";
        var servoData = { "servoStatus": "zeroDegrees", "description": "LaunchServo", "servoGPIO": 13 };
        servoData.servoStatus = "zeroDegrees";
        aClient = new HttpClient();
        aClient.put(WebServiceUrl, servoData, ServoStatusResult);
    });

    $("#ButtonServoStatus90").click(function () {
        var WebServiceUrl = "/api/servo/statuses";
        var servoData = { "servoStatus": "NinetyDegrees", "description": "LaunchServo", "servoGPIO": 13 };
        servoData.servoStatus = "NinetyDegrees";
        aClient = new HttpClient();
        aClient.put(WebServiceUrl, servoData, ServoStatusResult);
    });

    $("#ButtonServoStatus180").click(function () {
        var WebServiceUrl = "/api/servo/statuses";
        var servoData = { "servoStatus": "OneEightyDegrees", "description": "LaunchServo", "servoGPIO": 13 };
        servoData.servoStatus = "OneEightyDegrees";
        aClient = new HttpClient();
        aClient.put(WebServiceUrl, servoData, ServoStatusResult);
    });

    //____________________________________________________________________________
    //Sonic sensor
    //____________________________________________________________________________   

    $("#ButtonUltraSonicStatus").click(function () {
        var url = "/api/ultrasonic/statuses?=" + new Date().getTime();
        aClient = new HttpClient();
        aClient.get(url, UltraSonicStatus);
    });

});

