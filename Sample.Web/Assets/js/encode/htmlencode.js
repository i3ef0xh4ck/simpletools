$(function () {
    $("#btnHtmlEncode").click(function () {
        var text = $("#txtSourceHtml").val();
        if (text == "") {
            error("请填写要编码的内容");
        }
        else {
            $("#txtDestHtml").val(htmlencode(text));
        }
    });

    $("#btnHtmlDecode").click(function () {
        var text = $("#txtDestHtml").val();
        if (text == "") {
            error("请填写要解码的内容");
        }
        else {
            $("#txtSourceHtml").val(htmldecode(text));
        }
    });
});

function htmlencode(s) {
    var div = document.createElement('div');
    div.appendChild(document.createTextNode(s));
    return div.innerHTML;
}
function htmldecode(s) {
    var div = document.createElement('div');
    div.innerHTML = s;
    return div.innerText || div.textContent;
}