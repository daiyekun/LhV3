/**
 *  @Name：nf登录日志管理
 */
layui.define(['table'], function (exports) {
    var layer = layui.layer
        , $ = layui.$
        , setter = layui.setter
        , admin = layui.admin
        , form = layui.form
        , table = layui.table;
    var loadindex = wooutil.loading();
    //登录日志
    table.render({
        elem: '#NF-user-back-actionlog'
        , url: '/System/Actionlog/GetList'
        , page: true
        , loading: true
        , height: setter.table.height_2
        , limit: setter.table.limit
        , limits: setter.table.limits
        , cols: [[
            { type: 'numbers', fixed: 'left' }
            ,{ type: 'checkbox', fixed: 'left' }
            //, { field: 'id', width: 80, title: 'Id', sort: true }
            , { field: 'UserName', title: '操作人', width: 120 }
            , { field: 'CreateDatetime', title: '操作时间', width: 140 }
            , { field: 'OptionTypeDic', title: '操作类型' ,width: 100 }
            , { field: 'ActionTitle', title: '操作主题', width: 150 }
            , { field: 'RequestIp', title: 'IP', width: 120 }
            , { field: 'ControllerName', title: '控制器名称', width: 120 }
            , { field: 'ActionName', title: '执行方法', width: 120 }
            , { field: 'RequestUrl', title: '请求URL', width: 140 }
            , { field: 'RequestMethodDic', title: '请求方式', width: 80 }
            , { field: 'RequestData', title: '请求数据', width: 120 }
            , { field: 'Remark', title: '操作描述', width: 150 }
           

        ]]
        , done: function (res) {
            layer.close(loadindex);
        }
    });

    //删除日志
    $(".dellactionlog").click(function () {
        var checkStatus = table.checkStatus('NF-user-back-actionlog')
            , checkData = checkStatus.data; //得到选中的数据
        if (checkData.length === 0) {
            return layer.msg('请选择数据');
        }

        var tempId = [];
        for (var i = 0; i < checkData.length; i++) {
            tempId.push(checkData[i].Id);
        }

        layer.confirm('确定删除选中的数据？', { icon: 3, title: '提示信息' }, function (index) {
            admin.req({
                url: '/System/ActionLog/Delete',
                data: { Ids: tempId.toString() },
                done: function (res) {
                   
                    wooutil.reloadTable({
                        tableId: 'NF-user-back-actionlog',
                        table: table
                    });
                    layer.msg('已删除');
                }

            })
        })

    })

    exports('systemActionlog', {});
});