$(function () {
    $("#btnRegexMatch").click(function () {
        $("#txtRegexResult").val("");
        var pattern = $("#txtRegexPattern").val().trim();
        var content = $("#txtRegexContent").val().trim();
        var ignoreCase = "";
        if ($("#chkIgnoreCase").is(":checked")) {
            ignoreCase = "i";
        }
        if (pattern == "") {
            error("请填写正则");
            return;
        }
        else if (content == "") {
            error("请填写内容");
            return;
        }
        else {
            var reg = new RegExp(pattern, "g" + ignoreCase);
            var arr = reg.exec(content);
            var text = '';
            if (arr) {
                $("#txtRegexTip").val("共 " + arr.length + " 个匹配项");
                for (var i = 0; i < arr.length; i++) {
                    text += ("$" + i + " -> " + arr[i]) + "\r\n";
                }
            }
            else {
                $("#txtRegexTip").val("没有匹配结果，请检查正则");
                return;
            }
            $("#txtRegexResult").val(text);
        }
    });
})