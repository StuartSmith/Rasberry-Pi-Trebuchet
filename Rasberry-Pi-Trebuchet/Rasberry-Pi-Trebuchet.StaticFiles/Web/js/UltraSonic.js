////__________________________________________________
/// Ultra Sonic Result
///____________________________________________________
var UltraSonicStatus = function (data, status) {
    $("#UltraSonicStatusResult").html(data);
}


var UltraSonicStartStatus = function (data, status) {
    $("#UltraSonicStartStatusResult").html(data);
}


var UltraSonicStatusLastRun = function (data, status) {
    $("#UltraSonicLatestRunResult").html(data);
}

var UltraSonicRemovalResult = function (data, status) {
    $("#UltraSonicRemovalResult").html(data);
}

var IsUltraSonicRunningResult = function (data, status) {
    $("#IsUltraSonicRunningResult").html(data);
}



$(document).ready(function () {



    $("#ButtonIsUltraSonicsRunning").click(function () {
        var url = "/api/ultrasonic/IsUltraSonicRunning?=" + new Date().getTime();
        aClient = new HttpClient();

        aClient.get(url, IsUltraSonicRunningResult);
    });

    $("#ButtonUltraSonicStatus").click(function () {
        var url = "/api/ultrasonic/ultrasonicruns?=" + new Date().getTime();
        aClient = new HttpClient();
        aClient.get(url, UltraSonicStatus);
    });

    $("#ButtonUltraSonicStartRun").click(function () {
        var WebServiceUrl = "/api/ultrasonic/startrun";
        var RunData = { "TimeInSecondsToRunSensor": 10 };
        aClient = new HttpClient();
        aClient.put(WebServiceUrl, RunData, UltraSonicStartStatus);
    });

    $("#ButtonUltraSonicLastRun").click(function () {
        var url = "/api/ultrasonic/lastrun?=" + new Date().getTime();
        aClient = new HttpClient();
        aClient.get(url, UltraSonicStatusLastRun);
    });


    $("#ButtonRemoveUltraSonicsRun").click(function () {

        var url = "/api/ultrasonic/RemoveUltraSonicRuns";
        aClient = new HttpClient();
        aClient.delete(url, UltraSonicRemovalResult);
    });



});