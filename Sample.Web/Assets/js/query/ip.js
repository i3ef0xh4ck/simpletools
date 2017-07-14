$(function () {
    new Vue({
        el: '#app',
        data: {
            ip: '59.63.248.121',
            scope: '',
            area: '',
            status: 1
        },
        methods: {
            getArea: function () {
                var app = this;
                if (app.ip == '') {
                    error('请填写IP');
                }
                else if (!isIp(app.ip)) {
                    error('请输入正确的IP');
                }
                else {
                    app.notUse();
                    $.ajax({
                        type: 'POST',
                        url: '/fun/ipquery',
                        data: { word: app.ip },
                        success: function (slt) {
                            if (slt && slt.Message) {
                                error(slt.Message);                               
                            }
                            else {
                                if (slt) {
                                    app.scope = slt.start + ' - ' + slt.end;
                                    app.area = slt.country + slt.city;
                                }
                                else {
                                    error('获取不到数据，请重试');
                                }
                            }
                            app.use();
                        }
                    });
                }
            },
            use: function () {
                this.status = 1;
            },
            notUse: function () {
                this.status = 0;
            }
        }
    });
});