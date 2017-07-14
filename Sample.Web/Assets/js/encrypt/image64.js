$(function () {
    $("#btnImage64").click(function () {
        $("#image64file").click();
    });
    $("#image64file").change(function () {
        readFile();
    });
    function readFile() {
        var file = document.getElementById("image64file").files[0];
        if (!/image\/\w+/.test(file.type)) {
            error("请上传正确格式的图片文件");
            return false;
        }
        var reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onload = function (e) {
            $("#img64Preview").attr("src", this.result);
            $("#txtImage64Result").val(this.result);
            $(".img-info").show().text("图片大小：" + Math.ceil(file.size / 1024) + "KB");
        }
    }
});