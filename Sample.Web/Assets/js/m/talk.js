$(function () {
    var varSpace = {
        page: 1,
        messagers:["王小二", "马铃薯", "兵豆", "甜椒", "周星星", "大豆", "monkey", "张自新", "jack", "苏二", "小黄狗", "自忠", "田二妞", "小绵羊", "黄风", "黄小萌", "李建国", "花少", "玫瑰花", "粉红茉莉", "周小侠", "闪电", "秦朗", "大保剑", "李刚", "王芳", "李伟", "刘忠", "陈苍", "静音人", "李百", "杜夫", "黑居易", "水桶", "张全", "孙晓虎", "黄鹤", "洪荒之力", "小试牛刀"]
    };

    $(".btnRandom").click(function () {
        var name = varSpace.messagers[Math.floor(Math.random() * varSpace.messagers.length)];
        $("#txtName").val(name);
    });

    $(".btnLeave").click(function () {
        var $self = $(this);
        $self.attr("disabled", "disabled");
        var name = $("#txtName").val().trim();
        var content = $("#txtContent").val().trim();
        var code = $("#txtCode").val().trim();
        if (content == "") {
            $self.removeAttr("disabled");
            alert("留言内容不能为空");
            return;
        }
        else if (content.length >= 500) {
            $self.removeAttr("disabled");
            alert("留言内容不能超过500个字符");
            return;
        }
        else if (code == "") {
            $self.removeAttr("disabled");
            alert("验证码不能为空");
            return;
        }
        else {
            $.post("/m/leavemessage", { Name: name, Content: content, Code: code }, function (slt) {
                if (slt && slt.Flag) {
                    location.reload();
                }
                else {
                    $self.removeAttr("disabled");
                    alert(slt.Message);
                }
            });
        }
    });

    $(".message-more").click(function () {
        varSpace.page++;
        $.post("/m/talklist", { "page": varSpace.page }, function (data) {
            if (data) {
                var arr = [];
                for (var i = 0; i < data.length;i++) {
                    arr.push('<div class="media message-item">');
                    arr.push('<div class="media-left">');
                    if (encodeURIComponent(data[i].Name) == '%E7%AB%99%E9%95%BF') {
                        arr.push('<img class="media-object message-avatar" src="/Images/manage-avatar.jpg" alt="">');
                    }
                    else {
                        arr.push('<img class="media-object message-avatar" src="/Images/default-avatar.jpg" alt="">');
                    }
                    arr.push('</div>');
                    arr.push('<div class="media-body">');
                    arr.push('<h4 class="media-heading message-name">'+data[i].Name+'</h4>');
                    arr.push('<p class="message-content">' + data[i].Content + '</p>');
                    arr.push('<p>');
                    arr.push('<span class="message-time">' + data[i].IntimeStr + '</span>');
                    arr.push('</p>');
                    arr.push('</div>');
                    arr.push('</div>');
                }
                $(".message-more").before(arr.join(''));
                if (varSpace.page == $("#hidTotalPages").val()) {
                    $(".message-more").hide();
                }
            }
        });
    });
});