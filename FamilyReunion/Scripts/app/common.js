
window.common = (function () {
    var common = {};

    common.getFragment = function getFragment() {
        if (window.location.hash.indexOf("#") === 0) {
            return parseQueryString(window.location.hash.substr(1));
        } else {
            return {};
        }
    };

    function parseQueryString(queryString) {
        var data = {},
            pairs, pair, separatorIndex, escapedKey, escapedValue, key, value;

        if (queryString === null) {
            return data;
        }

        pairs = queryString.split("&");

        for (var i = 0; i < pairs.length; i++) {
            pair = pairs[i];
            separatorIndex = pair.indexOf("=");

            if (separatorIndex === -1) {
                escapedKey = pair;
                escapedValue = null;
            } else {
                escapedKey = pair.substr(0, separatorIndex);
                escapedValue = pair.substr(separatorIndex + 1);
            }

            key = decodeURIComponent(escapedKey);
            value = decodeURIComponent(escapedValue);

            data[key] = value;
        }

        return data;
    }

    common.api = function (url, successFunc) {
        $.ajax({
            method: 'get',
            url: url,
            contentType: "application/json; charset=utf-8",
            headers: {
                'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
            },
            success: successFunc
        });
    };

    function validate(data) {
        if (typeof (data) == 'object') {
            data = JSON.stringify(data);
        }
        return data;
    }

    common.apiAdd = function (url, data, successFunc) {
        data = validate(data);
        $.ajax({
            method: 'post',
            url: url,
            contentType: "application/json; charset=utf-8",
            headers: {
                'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
            },
            data: data,
            success: successFunc
        });
    };

    common.apiEdit = function (url, id, data, successFunc) {
        data = validate(data)
        $.ajax({
            method: 'put',
            url: url + '/' + id,
            contentType: "application/json; charset=utf-8",
            headers: {
                'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
            },
            data: data,
            success: successFunc
        });
    };

    common.apiDelete = function (url, id, successFunc) {
        $.ajax({
            method: 'delete',
            url: url + '/' + id,
            contentType: "application/json; charset=utf-8",
            headers: {
                'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
            },
            success: successFunc
        });
    };

    return common;
})();