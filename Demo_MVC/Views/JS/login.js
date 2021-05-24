layui.use(['form'], function () {
    var $ = layui.jquery;
    var form = layui.form;
    //登录
    form.on('submit(login)', function (data) {
        var index = layer.load(1, { shade: [0.3, '#fff'] });
        $.ajax({
            url: './../Login/getlogin',
            type: 'post',
            dataType: 'json',
            data: {
                'longin_username': data.field.longin_username,
                'longin_password': data.field.longin_password
            },
            success: function (data) {
                layer.close(index);
                if (data.code == 'fail') {
                    alert(data.message)
                } else {
                    window.location.href = data.message + '?userName=' + data.userName;
                }
            }
        });
    });
  //回车登录
    $(document).keyup(function (event) {
        if (event.keyCode == 13) {
            $('#login').trigger("click");
        }
    });
})