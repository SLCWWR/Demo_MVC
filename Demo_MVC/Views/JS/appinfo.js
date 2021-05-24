layui.use(['element', 'form', 'upload'], function () {
    var element = layui.element;
    var $ = layui.$
    var form = layui.form;
    var upload = layui.upload;
    $("#back").on("click", function () {
        var index = parent.layer.getFrameIndex(window.name); //先得到当前iframe层的索引
        parent.layer.close(index);

    });

    var uploadInst = upload.render({
        elem: '#test1'
        , auto: false
        , choose: function (obj) {
            obj.preview(function (index, file, result) {
                $('#icon').attr('src', result);
                $('#iconImage').val(result);
            });
        }
    });
    var ID = getQueryStringByName("ID");
    var type = getQueryStringByName("type");
    initForm(ID);

    form.on('submit(submit)', function (data) {
        submitApp(data.field, ID, type);
    });

    function initForm(ID) {
        if (ID == "")
            return;
        $.ajax({
            url: './../TeaApp/GetAppInfo',
            type: 'get',
            data: { ID: ID },
            dataType: 'json',
            success: function (data) {
                if (data.indexOf("err@") == -1) {
                    form.val('example', {
                        "name": data[0].LinkName
                        , "link": data[0].LinkAddress
                        , "remark": data[0].Remark
                        , "orderNum": data[0].OrderNum
                        , "open": data[0].Status == 1 ? true : false
                        , "iconImg": data[0].LinkImges
                    })
                    $("#icon").attr("src", data[0].LinkImges);
                }
            }

        });
    }

    function submitApp(data, ID, type) {
        if (typeof (data.open) == 'undefined') {
            data.status = 0
        } else {            
            data.status = 1
        }
        var type = ID == "" ? "AddApp" : "ModifyApp?ID=" + ID;
        $.ajax({
            url: './../TeaApp/' + type,
            type: 'post',
            data: data,
            success: function (data) {
                if (data == 'success') {
                    swal({
                        icon: 'success',
                        title: '操作成功！',
                    }).then(function () {
                        parent.location = parent.location;
                    });
                }
            }
        });
    }
});