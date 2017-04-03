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

var AzureMSGListnerLogResult = function (data, status) {
    debugger;
    var MsgLogValues = JSON.parse(data);

    var table = document.getElementById("tblAzureMSGListenerLog");
    var tableBody = document.getElementById("tblAzureMSGListenerLogbody");
    //table.innerHTML = "<tbody></tbody>";
    //var row = table.insertRow(0);
    tableBody.innerHTML="";


    for (var i = 0; i < MsgLogValues.length; i++) {
        var row = document.createElement("tr");

        var cell_MSGGUID = document.createElement("td");
        cell_MSGGUID.textContent = MsgLogValues[i].MSGGUID;
        row.appendChild(cell_MSGGUID);

        var cell_ProcessedRequestDateTime = document.createElement("td");
        cell_ProcessedRequestDateTime.textContent = MsgLogValues[i].ProcessedRequestDateTime;
        row.appendChild(cell_ProcessedRequestDateTime);

        var cell_RestUpMsgRequest = document.createElement("td");
        cell_RestUpMsgRequest.textContent = MsgLogValues[i].RestUpMsgRequest;
        row.appendChild(cell_RestUpMsgRequest);

        var cell_Response = document.createElement("td");
        cell_Response.textContent = MsgLogValues[i].Response;
        row.appendChild(cell_Response);

        tableBody.appendChild(row);

        //tr.append("<td>" + MsgLogValues[i].MSGGUID + "</td>");
        //tr.append("<td>" + MsgLogValues[i].ProcessedRequestDateTime + "</td>");
        //tr.append("<td>" + MsgLogValues[i].RestUpMsgRequest + "</td>");
        //tr.append("<td>" + MsgLogValues[i].Response + "</td>");
        //tableBody.appendChild(tr);
       // $('#tblAzureMSGListenerLog tr:last').after(tr);
        //$('#tblAzureMSGListenerLog').append('<tr');
    }
    //$('#myTable tr:last').after 
      
        
   // });

   //$("#AzureMSGListenerLogResult").html(data);
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


    $("#ButtonGetMSGListnerLog").click(function () {
        var url = "/api/azuremsglistener/loggedmessages?=" + new Date().getTime();
        aClient = new HttpClient();
        aClient.get(url, AzureMSGListnerLogResult);
    });

});