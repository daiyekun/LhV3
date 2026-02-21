/**
 @Name：用户
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
        , elem: '#NF-system-user'
        , url: '/System/UserInfor/GetList?rand=' + wooutil.getRandom()
        , cols: [[
            { type: 'numbers', fixed: 'left' },
            { type: 'checkbox', fixed: 'left' }
            , { field: 'Name', title: '用户名称', minWidth: 160, templet: '#nameTpl', fixed: 'left', filter: true }
            , { field: 'DeptName', title: '所属机构', width: 130, filter: true }
            , { field: 'DisplyName', title: '显示名称', width: 110, filter: true }
            , { field: 'SexDic', width: 100, title: '性别', templet: '#userSexTpl', filter: true }
            , { field: 'Age', width: 100, title: '年龄', filter: true}
            , { field: 'Tel', width: 100, title: '电话', filter: true}
            , { field: 'Mobile', width: 120, title: '手机', filter: true }
            , { field: 'Email', width: 120, title: 'E-Mail', filter: true}
            , { field: 'State', width: 130, title: '状态', templet: '#userstateDic', filter: true}
            , { field: 'Ustart', width: 150, title: '开通手机端', templet: '#userstateTpl', unresize: true }
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
            userinfoutility.stateEvent({ tableId: 'NF-system-user' });//注册状态流转事件
        }

    });

    /**事件 */
    var active = {
        search: function () {//查询
            $("input[name=hide_keyWord]").val($("input[name=keyWord]").val());
            table.reload('NF-system-user', {
                page: { curr: 1 }
                , where: {
                    keyWord: $("input[name=keyWord]").val()

                }
            });

        },

        submitState: function (evtobj) {//提交状态
            userinfoutility.updateSate({
                tableId: 'NF-system-user'
                , url: '/System/UserInfor/UpdateField'
                , evtobj: evtobj
            });
        },
        batchdel: function () {//删除
            wooutil.deleteDatas({ tableId: 'NF-system-user', table: table, url: '/System/UserInfor/Delete' });
        
        },
        add: function () {
            layer.open({
                type: 2
                , title: '新增系统用户'
                , content: '/System/UserInfor/Build'
                , maxmin: true
                , area: ['60%', '80%']
                , btn: ['确定', '取消']
                , btnAlign: 'c'
                , skin: "layer-ext-myskin"
                , yes: function (index, layero) {
                    var iframeWindow = window['layui-layer-iframe' + index]
                        , submitID = 'Nf-SystemAdmin-UserFormSubmit'
                        , submit = layero.find('iframe').contents().find('#' + submitID);
                    //监听提交
                    iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                        var field = obj.field; //获取提交的字段
                        var fieldval = obj.field.Name;
                        //确认密码
                        if (field.Pwd !== field.Repass) {
                            return layer.msg('两次密码输入不一致');
                        }
                        var resname = wooutil.UniqueValObj({
                            url: '/System/UserInfor/CheckInputValExist',
                            fieldName: 'Name',
                            inputVal: fieldval,
                            currId: 0
                        });
                        if (!resname) {
                            wooutil.OpenSubmitForm({
                                table: table,
                                url: "/System/UserInfor/AddSave",
                                tableId: 'NF-system-user',
                                data: field,
                                index: index
                            });
                        } else {
                            return layer.msg('当前用户已经存在！');
                        }
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
        menuallot: function () {//菜单权限
            var checkStatus = table.checkStatus("NF-system-user")
                , checkData = checkStatus.data; //得到选中的数据
            if (checkData.length === 0) {
                return layer.msg('请选择数据');
            } else if (checkData.length > 1) {
                return layer.msg('有且只能选择一条数据！');
            }
            var tmpId = checkData[0].Id;

            layer.open({
                type: 2
                , title: '菜单分配'
                , content: '/System/SysModel/SelectModel?Id=' + tmpId + "&setType=0&rand=" + wooutil.getRandom()
                , maxmin: true
                , area: ['60%', '80%']
                // , area:['550px','600px'] //[window.screen.width / 2 + 'px', window.screen.height / 2 + 'px'] //宽高
                , btn: ['确定', '取消']
                , btnAlign: 'c'
                , yes: function (index, layero) {
                   
                    var iframeWindow = window['layui-layer-iframe' + index]
                        , submitID = 'Nf-SystemAdmin-RoleModelSubmit'
                        , submit = layero.find('iframe').contents().find('#' + submitID);
                    //监听提交
                    iframeWindow.layui.form.on('submit(' + submitID + ')', function (data) {
                        var field = data.field; //获取提交的字段
                     
                        // wooutil.SaveForm(null, "/System/UserInfor/AllotSysModels", "", field, '操作成功！', index);
                        wooutil.OpenSubmitForm({
                            table: null,
                            url: "/System/UserInfor/AllotSysModels",
                            tableId: '',
                            data: field,
                            index: index
                        });
                        parent.location.reload();
                        return false;
                    });

                    submit.trigger('click');
                },
                success: function (layero, index) {
                    layer.full(index);
                    wooutil.openTip();
                   
                   
                }
            });


        },
        actionallot: function () {//功能权限
            var checkStatus = table.checkStatus("NF-system-user")
                , checkData = checkStatus.data; //得到选中的数据
            if (checkData.length === 0) {
                return layer.msg('请选择数据');
            } else if (checkData.length > 1) {
                return layer.msg('有且只能选择一条数据！');
            }
            var tmpId = checkData[0].Id;
            layer.open({
                type: 2
                , title: '功能权限分配'
                , content: '/System/SysFunction/UserPermissonAllot?Id=' + tmpId + "&setType=0&rand=" + wooutil.getRandom()
                , maxmin: true
                , area: ['60%', '80%']
                , success: function (layero, index) {
                    layer.full(index);
                    wooutil.openTip();
                }
            });

        }
    }
    /**工具箱*/
    table.on('toolbar(NF-system-user)', function (obj) {
        switch (obj.event) {
            case 'stateChange'://状态流转
                active.submitState(this);
                break;
            case "batchdel"://删除
                active.batchdel();
                break;
            case "menuallot"://菜单权限
                active.menuallot();
                break;
            case "actionallot"://功能权限
                active.actionallot();
                break;
            case "clear":
                soulTable.clearCache("NF-system-user")
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
    table.on('tool(NF-system-user)', function (obj) {
        var _data = obj.data;
        if (obj.event === 'del') {
            wooutil.deleteInfo({ tableId: "NF-system-user", data: obj, url: '/System/UserInfor/Delete' });
        }
        else if (obj.event === 'edit') {
            layer.open({
                type: 2
                , title: '修改用户'
                , content: '/System/UserInfor/Build?Id=' + obj.data.Id + "&rand=" + wooutil.getRandom()
                , maxmin: true
                // ,area: ['60%', '80%']   
                , area: [window.screen.width / 2 + 'px', window.screen.height / 2 + 'px'] //宽高
                , btn: ['确定', '取消']
                , btnAlign: 'c'
                , yes: function (index, layero) {
                    var iframeWindow = window['layui-layer-iframe' + index]
                        , submitID = 'Nf-SystemAdmin-UserFormSubmit'
                        , submit = layero.find('iframe').contents().find('#' + submitID);
                    //监听提交
                    iframeWindow.layui.form.on('submit(' + submitID + ')', function (data) {
                        var field = data.field; //获取提交的字段
                        //确认密码
                        if (field.Pwd !== field.Repass) {
                            return layer.msg('两次密码输入不一致');
                        }
                        var resname = wooutil.UniqueValObj({
                            url: '/System/UserInfor/CheckInputValExist',
                            fieldName: 'Name',
                            inputVal: field.Name,
                            currId: field.Id
                        });
                        if (!resname) {
                            wooutil.OpenSubmitForm({
                                table: table,
                                url: "/System/UserInfor/UpdateSave",
                                tableId: 'NF-system-user',
                                data: field,
                                index: index
                            });
                        } else {
                            return layer.msg('当前用户已经存在！');
                        }

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
        else if (obj.event === 'detail') {
            var tr = $(obj.tr);
            layer.open({
                type: 2
                , title: '查看用户'
                , content: '/System/UserInfor/Detail?Id=' + obj.data.Id + "&rand=" + wooutil.getRandom()
                , maxmin: true
                , area: [window.screen.width / 2 + 'px', window.screen.height / 2 + 'px'] //宽高
                , success: function (layero, index) {
                    layer.full(index);
                    wooutil.openTip();
                }
            });
        } else if (obj.event === 'setrole') {
            layer.open({
                type: 2
                , title: '设置用户角色'
                , content: '/System/UserInfor/UserSetRoles?Id=' + obj.data.Id
                , maxmin: true
                , area: ['60%', '80%']
                , btn: ['确定', '取消']
                , btnAlign: 'c'
                , skin: "layer-ext-myskin"
                , yes: function (index, layero) {

                    var iframeWindow = window['layui-layer-iframe' + index]
                        , submitID = 'userRolesubmitForm'
                        , submit = layero.find('iframe').contents().find('#' + submitID);
                    //监听提交
                    iframeWindow.layui.form.on('submit(' + submitID + ')', function (data) {

                        var field = data.field; //获取提交的字段
                        var _uid = 0;
                        var _roleIds = [];
                        $.each(field, function (i) {

                            if (i == "Id") {
                                _uid = field[i];
                            }
                            else if (i.indexOf("uroles[") != -1) {
                                _roleIds.push(field[i]);
                            }
                        });
                        admin.req({
                            url: "/System/UserInfor/SetUserRoles",
                            data: { uId: _uid, roleIds: _roleIds.toString() },
                            type: 'POST',
                            success: function (res) {
                                layer.msg(res.Msg, { icon: 1, time: 500 }, function (msgindex) {

                                    layer.close(index);
                                });
                            }
                        });
                        //layer.alert(JSON.stringify(data.field));
                        return false;
                    })

                    submit.trigger('click');
                }

                //,
                //success: function (layero, index) {
                //    layer.full(index);
                //    wooutil.openTip();
                //}
            })

        }
    });

    exports('systemUserinfor', {})
});