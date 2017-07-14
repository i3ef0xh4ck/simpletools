$(function () {
    $("#btnErcode").click(function () {
        var text = $("#selText").val();
        var level = $("#selLevel").val();
        var scale = $("#txtScale").val();
        var margin = $("#selMargin").val();
        var isTrans = $("#chkTrans").is(":checked");
        if (text == "") {
            error("请填写内容");
        }
        else {
            var url = "/fun/ercode?text=" + text + "&level=" + level + "&scale=" + scale + "&margin=" + margin + "&isTrans=" + isTrans;
            url = encodeURI(url);
            $("#imgPreview").attr("src", url);
        }
    });
});