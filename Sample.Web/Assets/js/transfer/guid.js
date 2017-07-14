$(function () {
    $("#btnGetGuid").click(function () {
        $.ajax({
            type: "POST",
            url: "/fun/guid",
            data: null,
            success: function (slt) {
                if (slt && slt.Flag) {
                    $("#txtSourceGuid").val(slt.Data);
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
    });
});