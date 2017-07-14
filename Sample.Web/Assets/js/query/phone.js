$(function () {
    new Vue({
        el: '#app',
        data: {
            phone: '13056785678',
            scope: '',
            area: '',
            status: 1
        },
        methods: {
            getArea: function () {
                var app = this;
                if (app.phone == '') {
                    error('请填写手机');
                }
                else if (!isMobile(app.phone)) {
                    error('请输入正确格式的手机');
                }
                else {
                    app.noUse();
                    $.ajax({
                        type: 'POST',
                        url: '/fun/mobilequery',
                        data: { word: app.phone },
                        success: function (slt) {
                            app.callback(slt);
                        }
                    });
                }
            },
            callback: function (slt) {
                if (slt && slt.Message) {
                    error(slt.Message);
                }
                else {
                    if (slt) {
                        this.scope = slt.start + ' - ' + slt.end;
                        this.area = slt.location;
                        hideError();
                    }
                    else {
                        error('获取不到数据，请重试');
                    }
                }
                this.use();
            },
            use: function () {
                this.status = 1;
            },
            noUse: function () {
                this.status = 0;
            }
        }
    });
});