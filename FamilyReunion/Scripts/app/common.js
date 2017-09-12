
window.common = (function () {
    let common = {};

    common.getFragment = function getFragment() {
        if (window.location.hash.indexOf("#") === 0) {
            return parseQueryString(window.location.hash.substr(1));
        } else {
            return {};
        }
    };

    function parseQueryString(queryString) {
        let data = {},
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

    common.api = function (url, successFunc, errorFunc) {
        $.ajax({
            method: 'get',
            url: url,
            contentType: "application/json; charset=utf-8",
            headers: {
                'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
            },
            success: successFunc,
            error: errorFunc || common.errorFunc
        });
    };

    function validate(data) {
        if (typeof (data) == 'object') {
            data = JSON.stringify(data);
        }
        return data;
    }

    common.apiAdd = function (url, data, successFunc, errorFunc) {
        data = validate(data);
        $.ajax({
            method: 'post',
            url: url,
            contentType: "application/json; charset=utf-8",
            headers: {
                'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
            },
            data: data,
            success: successFunc,
            error: errorFunc || common.errorFunc
        });
    };

    common.apiEdit = function (url, id, data, successFunc, errorFunc) {
        data = validate(data)
        $.ajax({
            method: 'put',
            url: url + '/' + id,
            contentType: "application/json; charset=utf-8",
            headers: {
                'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
            },
            data: data,
            success: successFunc,
            error: errorFunc || common.errorFunc
        });
    };

    common.apiDelete = function (url, id, successFunc, errorFunc) {
        $.ajax({
            method: 'delete',
            url: url + '/' + id,
            contentType: "application/json; charset=utf-8",
            headers: {
                'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
            },
            success: successFunc,
            error: errorFunc
        });
    };

    common.errorFunc = function (response) {
        if (typeof (response.responseJSON) == "object") {
            let data = response.responseJSON;
            if (data.message) {
                if (data.modelState) {
                    var message = '';
                    $.each(data.modelState, function (index, item) {
                        for (var property in item) {
                            message += item[property] + '\n';
                        }
                    });
                    alert(message);
                } else {
                    alert(data.message);
                }
            }
        }
    };

    return common;
})();