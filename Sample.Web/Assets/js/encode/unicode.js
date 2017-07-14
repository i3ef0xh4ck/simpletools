$(function () {
    $("#btnUnicodeEncode").click(function () {
        var text = $("#txtSourceUnicode").val();
        if (text == "") {
            error("请填写要编码的内容");
        }
        else {
            var u = new Unicode()
            $("#txtDestUnicode").val(u.encode(text));
        }
    });

    $("#btnUnicodeDecode").click(function () {
        var text = $("#txtDestUnicode").val();
        if (text == "") {
            error("请填写要解码的内容");
        }
        else {
            var u = new Unicode()
            $("#txtSourceUnicode").val(u.decode(text));
        }
    });
});

function Unicode() {
    this.encode = function (str) {
        var res = [];
        for (var i = 0; i < str.length; i++)
            res[i] = ("00" + str.charCodeAt(i).toString(16)).slice(-4);
        return "\\u" + res.join("\\u");
    }
    this.decode = function (str) {
        str = str.replace(/\\/g, "%");
        return unescape(str);
    }
}