$(function () {
    $("#btnUrlEncode").click(function () {
        var text = $("#txtSourceUrl").val();
        if (text == "") {
            error("请填写要编码的内容");
        }
        else {
            $("#txtDestUrl").val(encodeURIComponent(text).toLowerCase());
        }
    });

    $("#btnUrlDecode").click(function () {
        var text = $("#txtDestUrl").val();
        if (text == "") {
            error("请填写要解码的内容");
        }
        else {
            $("#txtSourceUrl").val(decodeURIComponent(text));
        }
    });
});