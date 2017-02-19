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
            if ((anHttpRequest.readyState == 4 && anHttpRequest.status == 200) ||
                (anHttpRequest.readyState == 4 && anHttpRequest.status == 201))
                aCallback(anHttpRequest.responseText);
        }

        var jsondata = JSON.stringify(data);

        anHttpRequest.open("POST", aUrl, true);

        anHttpRequest.setRequestHeader("Accept", "application/json");
        anHttpRequest.setRequestHeader("Content-Type", "application/json");
        anHttpRequest.setRequestHeader("Content-length", jsondata.length);

        anHttpRequest.send(jsondata);

        //aCallback(jsondata);
    }

    this.delete = function (aUrl, aCallback) {

        var anHttpRequest = new XMLHttpRequest();
        anHttpRequest.onreadystatechange = function () {
            if (anHttpRequest.readyState == 4 && anHttpRequest.status == 200)
                aCallback("Removed All Runs");
            if (anHttpRequest.readyState == 4 && anHttpRequest.status == 204)
                aCallback("No Runs found to remove");
        }

        anHttpRequest.open("DELETE", aUrl, true);
        anHttpRequest.setRequestHeader('Cache-Control', 'no-cache');
        anHttpRequest.send(null);

    }
}