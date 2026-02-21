/**
 *  @Name：nf登录日志管理
 */
layui.define(['table', 'table'], function (exports) {
    var layer = layui.layer
        , $ = layui.$
        , setter = layui.setter
        , admin = layui.admin
        , form = layui.form
        , table = layui.table;
    var loadindex = wooutil.loading();
    //登录日志
    table.render({
        elem: '#NF-user-back-loginlog'
        , url: '/System/LoginLog/GetList'
        , page: true
        , loading: true
        , height: setter.table.height_2
        , limit: setter.table.limit
        , limits: setter.table.limits
        , cols: [[
            { type: 'numbers', fixed: 'left' },
            { type: 'checkbox', fixed: 'left' }
            //, { field: 'id', width: 80, title: 'Id', sort: true }
            , { field: 'LoginUserName', title: '登录用户' }
            , { field: 'LoginIp', title: '登录IP' }
            , { field: 'ResultDic', title: '登录结果' }
            , { field: 'CreateDatetime', title: '登录时间' }

        ]],
        done: function (res) {  
            layer.close(loadindex);    
        }

    });

    //删除日志
    $(".dellloginlog").click(function () {
        var checkStatus = table.checkStatus('NF-user-back-loginlog')
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
                url: '/System/LoginLog/Delete',
                data: { Ids: tempId.toString() },
                done: function (res) {
                    
                    wooutil.reloadTable({
                        tableId: 'NF-user-back-loginlog',
                        table: table
                    });
                    layer.msg('已删除');
                }

            })
        })

    })
   
    exports('systemLoginlog', {});
});