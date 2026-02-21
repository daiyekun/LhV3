
/**
 @Name：系统新加字段
 @Author：dyk 20180724
 */
layui.define(['table', 'form', 'userinfoutility', 'soulTable'], function (exports) {
    var $ = layui.$
        , table = layui.table
        , setter = layui.setter
        , admin = layui.admin
        , userinfoutility = layui.userinfoutility
        , soulTable = layui.soulTable
        , form = layui.form;
    var loadindex = wooutil.loading();
    //部门管理
    table.render({
        toolbar: '#tooluser'
        , defaultToolbar: ['filter']
        , cellMinWidth: 80
        , overflow: {
            type: 'tips'//内容超过设置
            , color: 'black' // 字体颜色
            , bgColor: 'white' // 背景色
        }
        , elem: '#NF-system-SysField'
        , url: '/System/SysField/GetList?rand=' + wooutil.getRandom()
        , cols: [[
            { type: 'numbers', fixed: 'left' },
            { type: 'checkbox', fixed: 'left' }
            , { field: 'Id', title: 'Id', width: 50, hide: true  }
            , { field: 'Lable', title: '字段标题', width: 110, filter: true }
            , { field: 'Fname', title: '字段名字(数据库)', width: 110, filter: true }
            , { field: 'FieldTypeName', title: '字段类型', width: 110, filter: true }
            , { field: 'RequiredName', title: '必填', width: 110, filter: true }
            , { field: 'IsListName', width: 100, title: '显示列表', filter: true }
            , { field: 'TagName', width: 100, title: '用于', filter: true }
            , { field: 'SelData', width: 100, title: '选择框内容', filter: true }
            , { field: 'Zbpx', width: 100, title: '自定义排序列', edit: 'text'}
            , { field: 'Isqy', width: 150, title: '是否启用', templet: '#userstateTpl', unresize: true }
            , { title: '操作', width: 220, align: 'center', fixed: 'right', toolbar: '#table-system-user' }
        ]]
        , page: true
        , loading: true
        , height: setter.table.height_4
        , limit: setter.table.limit
        , limits: setter.table.limits
        , filter: {
            //列表服务器缓存
            //items: ['column', 'data', 'condition', 'editCondition', 'excel', 'clearCache'],
            cache: true
            , bottom: false
        }
        , done: function (res) {
            soulTable.render(this)
            layer.close(loadindex);
            $("input[name=keyWord]").val($("input[name=hide_keyWord]").val());
            $("input[name=hide_keyWord]").val("");
            userinfoutility.stateEvent({ tableId: 'NF-system-SysField' });//注册状态流转事件
        }

    });
    //编辑列
    table.on('edit(NF-system-SysField)', function (obj) {
        var value = obj.value //得到修改后的值
            , data = obj.data //得到所在行所有键值
            , field = obj.field; //得到字段
        // layer.msg('[ID: ' + data.id + '] ' + field + ' 字段更改为：' + value);
        var er = obj.data.Id;
        var et = obj.data.Zbpx
        admin.req({
            url: '/System/SysField/UpdateInvoiceDesc',
            data: { Id: data.Id, field: obj.field, fdv: value },
            done: function (res) {
                table.reload("NF-system-SysField");
            }

        });

    });
    /**事件 */
    var active = {
        search: function () {//查询
            $("input[name=hide_keyWord]").val($("input[name=keyWord]").val());
            table.reload('NF-system-SysField', {
                page: { curr: 1 }
                , where: {
                    keyWord: $("input[name=keyWord]").val()

                }
            });

        },

        submitState: function (evtobj) {//提交状态
            userinfoutility.updateSate({
                tableId: 'NF-system-SysField'
                , url: '/System/SysField/UpdateField'
                , evtobj: evtobj
            });
        },
        batchdel: function () {//删除
            wooutil.deleteDatas({ tableId: 'NF-system-SysField', table: table, url: '/System/SysField/Delete' });

        },
        add: function () {
            layer.open({
                type: 2
                , title: '新增系统用户'
                , content: '/System/SysField/Build'
                , maxmin: true
                , area: ['60%', '80%']
                , btn: ['确定', '取消']
                , btnAlign: 'c'
                , skin: "layer-ext-myskin"
                , yes: function (index, layero) {
                    var iframeWindow = window['layui-layer-iframe' + index]
                        , submitID = 'Nf-System-SysFieldSubmit'
                        , submit = layero.find('iframe').contents().find('#' + submitID);
                    //监听提交
                    iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                        var field = obj.field; //获取提交的字段

                            wooutil.OpenSubmitForm({
                                table: table,
                                url: "/System/SysField/AddSave",
                                tableId: 'NF-system-SysField',
                                data: field,
                                index: index
                            });
                      
                        return false;


                    });

                    submit.trigger('click');
                },
                success: function (layero, index) {
                    layer.full(index);
                    wooutil.openTip();
                }
            });
        }
        ,
      
  
    }
    /**工具箱*/
    table.on('toolbar(NF-system-SysField)', function (obj) {
        switch (obj.event) {
            case 'stateChange'://状态流转
                active.submitState(this);
                break;
            case "batchdel"://删除
                active.batchdel();
                break;
         
            case "clear":
                soulTable.clearCache("NF-system-SysField")
                layer.msg('已还原！', { icon: 1, time: 1000 })
                break;
            case 'search':
                active.search();
                break;
            case "add":
                active.add();
                break;


        }
    });

    //监听工具条
    table.on('tool(NF-system-SysField)', function (obj) {
        var _data = obj.data;
        if (obj.event === 'del') {
            wooutil.deleteInfo({ tableId: "NF-system-SysField", data: obj, url: '/System/SysField/Delete' });
        }
        else if (obj.event === 'edit') {
            layer.open({
                type: 2
                , title: '修改系统字段'
                , content: '/System/SysField/Build?Id=' + obj.data.Id + "&rand=" + wooutil.getRandom()
                , maxmin: true
                // ,area: ['60%', '80%']   
                , area: [window.screen.width / 2 + 'px', window.screen.height / 2 + 'px'] //宽高
                , btn: ['确定', '取消']
                , btnAlign: 'c'
                , yes: function (index, layero) {
                    var iframeWindow = window['layui-layer-iframe' + index]
                        , submitID = 'Nf-System-SysFieldSubmit'
                        , submit = layero.find('iframe').contents().find('#' + submitID);
                    //监听提交
                    iframeWindow.layui.form.on('submit(' + submitID + ')', function (data) {
                        var field = data.field; //获取提交的字段
                            wooutil.OpenSubmitForm({
                                table: table,
                                url: "/System/SysField/UpdateSave",
                                tableId: 'NF-system-SysField',
                                data: field,
                                index: index
                            });
                        return false;
                    });
                    submit.trigger('click');
                },
                success: function (layero, index) {
                    layer.full(index);
                    wooutil.openTip();
                }
            });
        }
  
    });

    exports('systemSysField', {})
});