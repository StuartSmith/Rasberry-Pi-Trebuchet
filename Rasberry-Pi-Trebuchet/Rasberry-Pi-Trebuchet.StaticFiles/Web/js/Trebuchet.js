////__________________________________________________
/// Ultra Sonic Result
///____________________________________________________
var TrebuchetFires = function (data, status) {
    $("#ButtonFireTrebuchetResults").html(data);
}

var TrebuchetReset = function (data, status) {
    $("#ButtonResetTrebuchetResults").html(data);
}




$(document).ready(function () {

    $("#ButtonFireTrebuchet").click(function () {
        var url = "/api/trebuchet/fire";
        aClient = new HttpClient();
        aClient.put(url, null, TrebuchetFires);
    });

   

    $("#ButtonResetTrebuchet").click(function () {
        var url = "/api/trebuchet/reset";
        aClient = new HttpClient();
        aClient.put(url, null, TrebuchetReset);
       
    });
});