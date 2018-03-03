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

$(document).ready(function () {
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
        aClient.post(WebServiceUrl, servoData, ServoStatusResult);
    });

    $("#ButtonServoStatus90").click(function () {
        var WebServiceUrl = "/api/servo/statuses";
        var servoData = { "servoStatus": "NinetyDegrees", "description": "LaunchServo", "servoGPIO": 13 };
        servoData.servoStatus = "NinetyDegrees";
        aClient = new HttpClient();
        aClient.post(WebServiceUrl, servoData, ServoStatusResult);
    });

    $("#ButtonServoStatus180").click(function () {
        var WebServiceUrl = "/api/servo/statuses";
        var servoData = { "servoStatus": "OneEightyDegrees", "description": "LaunchServo", "servoGPIO": 13 };
        servoData.servoStatus = "OneEightyDegrees";
        aClient = new HttpClient();
        aClient.post(WebServiceUrl, servoData, ServoStatusResult);
    });
});