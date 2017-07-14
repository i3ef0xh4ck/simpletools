$(function () {
    var http_query_result;
    new Vue({
        el: '#app',
        data: {
            method: 'get',
            url: 'http://www.ofmonkey.com',
            param: '',
            result: '',
            status: 1
        },
        methods: {
            request: function () {
                if (this.url == '') {
                    error('请填写网址!');
                    return false;
                }
                this.setButtonStatus(0);
                this.result = '';
                var loading = layer.load(2);
                var app = this;
                $.ajax({
                    type: 'POST',
                    url: '/fun/httpreq',
                    data: { 'Method': app.method, 'Url': app.url, 'Params': app.param },
                    success: function (slt) {
                        if (slt && slt.Flag) {
                            http_query_result = slt.Data;
                            app.result = slt.Data;
                        }
                        else {
                            http_query_result = '';
                            if (slt.Message) {
                                error(slt.Message);
                            } else {
                                error(slt);
                            }
                        }
                        app.setButtonStatus(1);
                        layer.close(loading);
                    }
                });
            },
            setButtonStatus: function (status) {
                this.status = status;
            }
        }
    });
    $('#btnRecover').click(function () {
        $('#html_text').val(http_query_result);
    });
});