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

        //$.getJSON(aUrl, function (data) {
        //    aCallback(data)
        //});






    }

    this.put = function (aUrl,data, aCallback) {
        var anHttpRequest = new XMLHttpRequest();
        anHttpRequest.onreadystatechange = function () {
            if (anHttpRequest.readyState == 4 && anHttpRequest.status == 200)
                aCallback(anHttpRequest.responseText);
        }

        anHttpRequest.open("PUT", aUrl, true);
        anHttpRequest.send( JSON.stringify(data));
      
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





var ButtonRight = false;
var ButtonLeft = false;



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


        var url = "/api/lights/statuses/rightlight";
        aClient = new HttpClient();
        aClient.get(url, LightStatusesChangeResultRightComplete);
}

var LightStatusesChangeResultRightComplete = function (data, status) {  
       $("#lightStatusChangeRightResultOff").html(data);
       $("#lightStatusChangeRightResultOn").html(data);
    
}


$(document).ready(function () {
    //Click event to retrieve all light statuses
    $("#ButtonLightStatuses").click(function () {
        var url = "/api/lights/statuses";
        aClient = new HttpClient();
        aClient.get(url, LightStatusesResult);
    });

    //Click event to retrieve the status of the left light
    $("#ButtonLightStatusLeft").click(function () {
        var url = "/api/lights/statuses/leftlight";
        aClient = new HttpClient();
        aClient.get(url, LightStatusesResultLeft);
    });


    $("#ButtonLightStatusRight").click(function () {
        var url = "/api/lights/statuses/rightlight";
        aClient = new HttpClient();
        aClient.get(url, LightStatusesResultRight);
    });


 $("#ButtonTurnRightLightOn").click(function () {
                
       var WebServiceUrl = "/api/lights/statuses";          
       var lightdata = { "IsLightOn": true, "Description": "RightLight", "LightPosition": 0, "LightGPIO": 7 };           
       lightdata.IsLightOn = true;   
       aClient = new HttpClient();    
       aClient.put( WebServiceUrl, lightdata, LightStatusesChangeResultRight);
     });

    $("#ButtonTurnRightLightOff").click(function () {
                
       var WebServiceUrl = "/api/lights/statuses";          
       var lightdata = { "IsLightOn": false, "Description": "RightLight", "LightPosition": 0, "LightGPIO": 7 };           
       lightdata.IsLightOn = false;   
       aClient = new HttpClient();    
       aClient.put( WebServiceUrl, lightdata, LightStatusesChangeResultRight);
     });

});