$(function () {
    $("#btnMd5").click(function () {
        var text = $("#txtSourceMd5").val();
        if (text == "") {
            error("请填写要加密的内容");
        }
        else {
            if (text.length >= CONST_MAX_LENGTH) {
                error("要加密的内容不能超过10000个字符");
            }
            else {
                var half = false;
                if ($("#rad16").is(":checked")) {
                    half = true;
                }
                $.ajax({
                    type: "POST",
                    url: "/fun/md5",
                    data: { "text": text, "half": half },
                    success: function (slt) {
                        if (slt && slt.Flag) {
                            $("#txtDestMd5").val(slt.Data);
                        }
                        else {
                            if (slt.Message) {
                                error(slt.Message);
                            } else {
                                error(slt);
                            }
                        }
                    }
                });
            }
        }
    });
});