$(function () {
    $("#btnBarcode").click(function () {
        var type = $("#selType").val();
        var code = $("#txtCode").val();
        var width = $("#txtWidth").val();
        var height = $("#txtHeight").val();
        var isTrans = $("#chkTrans").is(":checked");
        if (code == "") {
            error("请填写要编码的内容");
        }
        else {
            var url = "/fun/barcode?type=" + type + "&code=" + code + "&width=" + width + "&height=" + height + "&isTrans=" + isTrans;
            url = encodeURI(url);
            $("#imgPreview").attr("src", url);
        }
    });
});