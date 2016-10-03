////__________________________________________________
/// Ultra Sonic Result
///____________________________________________________
var UltraSonicStatus = function (data, status) {
    $("#UltraSonicStatusResult").html(data);
}

$(document).ready(function () {

    //____________________________________________________________________________
    //Sonic sensor
    //____________________________________________________________________________   

    $("#ButtonUltraSonicStatus").click(function () {
        var url = "/api/ultrasonic/statuses?=" + new Date().getTime();
        aClient = new HttpClient();
        aClient.get(url, UltraSonicStatus);
    });

});