$(function () {
    $("#btnUrlEncodeUtf8").click(function () {
        var text = $("#txtSourceUrlUtf8").val();
        if (text == "") {
            error("请填写要编码的内容");
        }
        else {
            var u = new UTF8();
            $("#txtDestUrlUtf8").val(u.encode(text));
        }
    });

    $("#btnUrlDecodeUtf8").click(function () {
        var text = $("#txtDestUrlUtf8").val();
        if (text == "") {
            error("请填写要编码的内容");
        }
        else {
            var u = new UTF8();
            $("#txtSourceUrlUtf8").val(u.decode(text));
        }
    });
});

function UTF8() {
    this.encode = function (str) {
        var temp = "", rs = "";
        for (var i = 0, len = str.length; i < len; i++) {
            temp = str.charCodeAt(i).toString(16);
            rs += "\\u" + new Array(5 - temp.length).join("0") + temp;
        }
        return rs;
    }
    this.decode = function (str) {
        return str.replace(/(\\u)(\w{4}|\w{2})/gi, function ($0, $1, $2) {
            return String.fromCharCode(parseInt($2, 16));
        });
    }
}