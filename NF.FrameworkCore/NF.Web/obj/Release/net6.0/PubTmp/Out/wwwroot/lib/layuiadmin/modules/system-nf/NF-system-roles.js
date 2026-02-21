/**
 @Name：角色
 @Author：dyk 20180813
 */
layui.define(['table', 'form'], function (exports) {
    var $ = layui.$
        , table = layui.table
        , setter = layui.setter
        , admin = layui.admin
        , form = layui.form;
    var loadindex = wooutil.loading();
    table.render({
        elem: '#NF-system-role'
        , url: '/System/Role/GetList?rand=' + wooutil.getRandom()
        , cols: [[
            { type: 'numbers', fixed: 'left' },
            { type: 'checkbox', fixed: 'left' }
            , { field: 'Name', title: '角色名称', minWidth: 130, templet: '#nameTpl', fixed: 'left' }
            , { field: 'No', title: '编号', width: 130 }
            , { field: 'Remark', title: '备注', width: 150 }
            , { field: 'CreateUserName', title: '创建人', width: 120 }
            , { field: 'CreateDatetime', width: 110, title: '创建时间' }
            , { field: 'Rstate', width: 80, title: '状态', templet: '#rolestateTpl', unresize: true }
            , { title: '操作', width: 150, align: 'center', fixed: 'right', toolbar: '#table-system-roletbar' }
        ]]
        , page: true
        , loading: true
        , height: setter.table.height_2
        , limit: setter.table.limit
        , limits: setter.table.limits
         , done: function (res) {
             layer.close(loadindex);
         }

    });

    //监听工具条
    table.on('tool(NF-system-role)', function (obj) {
        var _data = obj.data;
        if (obj.event === 'del') {
            wooutil.deleteInfo({ tableId: "NF-system-role", data: obj, url: '/System/Role/Delete' });

        }
        else if (obj.event === 'edit') {
            layer.open({
                type: 2
                , title: '修改角色'
                , content: '/System/Role/Build?Id=' + obj.data.Id + "&rand=" + wooutil.getRandom()
                , maxmin: true
                // ,area: ['60%', '80%']   
                , area: [window.screen.width / 2 + 'px', window.screen.height / 2 + 'px'] //宽高
                , btn: ['确定', '取消']
                , btnAlign: 'c'
                , yes: function (index, layero) {
                   
                    var iframeWindow = window['layui-layer-iframe' + index]
                        , submitID = 'Nf-SystemAdmin-RoleFormSubmit'
                        , submit = layero.find('iframe').contents().find('#' + submitID);
                    //监听提交
                    iframeWindow.layui.form.on('submit(' + submitID + ')', function (data) {
                        var field = data.field; //获取提交的字段
                        var res = wooutil.UniqueValObj({
                            url: '/System/Role/CheckInputValExist',
                            fieldName: 'Name',
                            inputVal: field.Name,
                            CurrId: field.Id
                        });
                        if (!res) {
                            wooutil.OpenSubmitForm({
                                table: table,
                                url: "/System/Role/UpdateSave",
                                tableId: 'NF-system-role',
                                data: field,
                                index: index
                            });
                           
                        } else {
                            layer.msg('当前角色已经存在！');

                        }
                        return false;

                    });

                    submit.trigger('click');
                },
                success: function (layero, index) {
                    //layer.full(index);
                    //wooutil.openTip();
                }
            });
        }
        else if (obj.event === 'detail') {
            var tr = $(obj.tr);
            layer.open({
                type: 2
                , title: '查看角色'
                , content: '/System/Role/Detail?Id=' + obj.data.Id + "&rand=" + wooutil.getRandom()
                , maxmin: true
                , area: [window.screen.width / 2 + 'px', window.screen.height / 2 + 'px'] //宽高
                , success: function (layero, index) {
                    layer.full(index);
                    wooutil.openTip();
                }
            });
        }
    });

    /******************************************查看信息begin*******************************************************************/
    
    /*******************************************查看信息end********************************************************************/
    layer.close(loadindex);
    exports('systemRoles', {})
});