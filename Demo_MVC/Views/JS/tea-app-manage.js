var currentData = '';

layui.use(['form', 'table', 'layer'], function () {
    var $ = layui.$
    var form = layui.form;
    var table = layui.table;
    var layer = layui.layer;
    GetTeacherApp("", "qiyong");
    form.on('select(type)', function (type) {
        showTable(currentData, type.value);
    });
    $("#addAPP").on('click', function () {
        layer.open({
            type: 2,
            title: false,
            shadeClose: true,
            shade: [0],
            maxmin: false,
            area: ['893px', '700px'],
            content: ['AppInfo.html?Type=1', 'no']
        });
    });
    //监听工具条
    table.on('tool(test)', function (obj) {
        var data = obj.data;
        if (obj.event === 'del') {
            swal({
                icon: 'warning',
                title: '确认删除应用："' + data.LinkName + '"？',
                buttons: ["取消", {
                    closeModal: false,
                    text: "确认"
                }]
            }).then(function (value) {
                if (value) {
                    DeleteApp(data.ID_);
                }
            });
        } else if (obj.event === 'edit') {
            layer.open({
                type: 2,
                title: false,
                shadeClose: true,
                shade: [0],
                maxmin: false, //开启最大化最小化按钮
                area: ['893px', '700px'],
                content: ['AppInfo.html?ID=' + data.ID_, 'no']
            });
        }
    });


    //删除应用
    function DeleteApp(ID) {
        $.ajax({
            url: './../TeaApp/DeleteApp',
            type: 'post',
            data: {
                'ID': ID,
            },
            success: function (data) {
                if (data == 'success') {
                    swal({
                        icon: 'success',
                        title: '删除成功！',
                    }).then(function () {
                        location = location;
                    });
                }
            }
        });
    }

    //查找应用
    $("#find").on('click', function () {
        var kw = $("#kw").val();
        GetTeacherApp(kw);
    });
    //回车搜索
    document.onkeydown = function (event) {
        var e = event || window.event || arguments.callee.caller.arguments[0];
        if (e && e.keyCode == 13) {
            $("#find").click();
        }
    }
    //查找应用
    function GetTeacherApp(key, type) {
        var index = layer.load(1, { shade: [0.3, '#fff'] }); //0代表加载的风格，支持0-2
        $.ajax({
            url: './../TeaApp/GetTeacherApp',
            type: 'post',
            data: {
                'kw': key
            },
            dataType: 'json',
            success: function (data) {
                if (data.indexOf("err@") == -1) {
                    currentData = data;
                    showTable(data, type);
                    layer.close(index);
                }
            }
        });
    }
    //显示表格
    function showTable(data, type) {
        var showData = [];
        switch (type) {
            case "qiyong"://启用
                data.forEach(function (app) {
                    if (app.Status == '1')
                        showData.push(app);
                });
                break;
            case "tingyong"://停用
                data.forEach(function (app) {
                    if (app.Status == '0')
                        showData.push(app);
                });
                break;
            default:
                showData = data;
                break;
        }
        table.render({
            elem: '#test'
            , data: showData
            , cols: [[
                {
                    align: 'center', title: '序号', width: 80, templet: function (data) {
                        return (data.LAY_TABLE_INDEX + 1);
                    }
                }
                , { field: 'LinkName', title: '应用名称', width: 150, align: 'center', }
                , {
                    title: '应用图标', align: 'center', width: 120, templet: function (data) {
                        var html = '<img style="display: inline-block; width: 50%; height: 100%;" src="' + data.LinkImges + '">';
                        return html;
                    }
                }
                , { field: 'LinkAddress', title: '应用链接', align: 'center' }
                , {
                    title: '状态', width: 100, align: 'center', templet: function (data) {
                        var html = data.Status == '1' ? "启用" : '<span style="color:red">停用</span>';
                        return html;
                    }
                }
                , { title: '操作', toolbar: '#barDemo', width: 200, align: 'center' }
            ]]
            , page: true
        });
    }
});


