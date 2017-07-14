$(function () {
    new Vue({
        el: '#app',
        data: {
            domain: 'www.sina.com',
            ip: '',
            scope: '',
            area: '',
            status: 1
        },
        methods: {
            getArea: function () {
                var app = this;
                if (app.domain == '') {
                    error('请填写域名');
                }
                else if (!isDomain(app.domain)) {
                    error('请输入正确格式的域名');
                }
                else {
                    app.noUse();
                    $.ajax({
                        type: 'POST',
                        url: '/fun/ipquery',
                        data: { word: app.domain },
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
                        this.ip = slt.ip;
                        this.scope = slt.start + ' - ' + slt.end;
                        this.area = slt.country + ' ' + slt.city;
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