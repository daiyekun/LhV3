/**
*审批模板列表
**/
layui.define(['table', 'form'], function (exports) {
    var $ = layui.$
   , table = layui.table
   , setter = layui.setter
   , admin = layui.admin
   , form = layui.form;
    var logdindex = layer.load(0, { shade: false });

    table.render({
        elem: '#NF-WorkFlow-Temp'
       , url: '/WorkFlow/FlowTemp/GetList?rand=' + wooutil.getRandom()
       , toolbar: '#toolWorkFlowTemp'
       , defaultToolbar: ['filter']
       , cellMinWidth: 80
       , cols: [[
             { type: 'numbers', fixed: 'left' }
           , { type: 'checkbox', fixed: 'left' }
           , { field: 'ObjTypeDic', title: '流程对象', width: 140, fixed: 'left' }
           , { field: 'Name', title: '名称', width: 150, fixed: 'left' }
           , { field: 'CategorysName', title: '对象类别', width: 150 }
           , {
               field: 'Version', title: '版本', width: 100, templet: function (d) {
                   return d.Version + ".0";
               }
           }
           , { field: 'DeptsName', title: '所属机构', width: 150 }
           , { field: 'FlowItemsDic', title: '审批事项', width: 150 }
           , { field: 'CreateUserName', title: '建立人', width: 120 }
           , { field: 'CreateDateTime', title: '建立日期', width: 120 }
           , { field: 'Id', title: 'Id', width: 50, hide: true }
           , { field: 'IsValid', width: 120, title: '状态', align: 'center', templet: '#IsValidTpl', unresize: true }
           , { title: '操作', width: 240, align: 'center', fixed: 'right', toolbar: '#table-WorkFlowTemp-tbar' }
       ]]
       , page: true
       , loading: true
       , height: setter.table.height_4
       , limit: setter.table.limit
       , limits: setter.table.limits
       , done: function (res, curr, count) {   //返回数据执行回调函数
           layer.close(logdindex);    //返回数据关闭loading
           $("input[name=keyWord]").val($("input[name=hide_keyWord]").val());
           $("input[name=hide_keyWord]").val("");


       }

    });

    /**
    *判断流程是否唯一
    **/
    function UniqueFlow(param) {
        var result = "";
        admin.req({
            async: false,
            url: param.url,
            data: param.data,
            type: 'POST',
            done: function (res) {
                if (res.Tag === 1) {
                    result= res.RetValue;
                }

            }
        });
        return result;
    }

    var openAdd = function () {
        layer.open({
            type: 2
            , title: '新增'
            , content: '/WorkFlow/FlowTemp/Build'
            , maxmin: true
            , area: ['60%', '80%']
            , btn: ['确定', '取消']
            , btnAlign: 'c'
            , skin: "layer-ext-myskin"
            , yes: function (index, layero) {
                var iframeWindow = window['layui-layer-iframe' + index]
                    , submitID = 'NF-FlowTemp-FormSubmit'
                    , submit = layero.find('iframe').contents().find('#' + submitID);
                //监听提交
                iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                    var field = obj.field; //获取提交的字段
                    var fieldval = obj.field.Name;
                    var resname = wooutil.UniqueValObj({
                        url: '/WorkFlow/FlowTemp/CheckInputValExist',
                        fieldName: 'Name',
                        inputVal: fieldval,
                        currId: 0
                    });
                    if (resname) {
                        return layer.msg('此名称已经存在！');
                    }
                    //判断流程是否唯一
                    var soltstr = UniqueFlow({ url: '/WorkFlow/FlowTemp/CheckFlowUnique', data: obj.field });
                    if (soltstr.length!= 0) {
                        return layer.alert("与流程【" + soltstr + "】相冲突");
                    }

                    wooutil.OpenSubmitForm({
                        url: '/WorkFlow/FlowTemp/Save',
                        data: obj.field,
                        table: table,
                        index: index,
                        tableId: 'NF-WorkFlow-Temp'
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
    };
    /**
    *事件
    **/
    var active = {
        add: function () {//新增
            openAdd();
        },
        batchdel: function () {//删除
            wooutil.deleteDatas({ tableId: 'NF-WorkFlow-Temp', table: table, url: '/WorkFlow/FlowTemp/Delete' });
        },
        search: function () {//查询
            $("input[name=hide_keyWord]").val($("input[name=keyWord]").val());
            table.reload('NF-WorkFlow-Temp', {
                page: { curr: 1 }
                , where: {
                    keyWord: $("input[name=keyWord]").val()

                }
            });
        }
    };
    /**
    *头部工具栏
    **/
    table.on('toolbar(NF-WorkFlow-Temp)', function (obj) {
        switch (obj.event) {
            case 'add':
                active.add();
                break;
            case 'batchdel':
                active.batchdel();
                break;
            case 'search':
                active.search();
                break;
            case "LAYTABLE_COLS":
                break;
            default:
                layer.alert("你操作是什么鬼->" + obj.event);
                break;
        };
    });
    /**
    *修改信息
    **/
    function editFunc(obj, _success) {
        layer.open({
            type: 2
                , title: '修改信息'
                , content: '/WorkFlow/FlowTemp/Build?Id=' + obj.data.Id + "&rand=" + wooutil.getRandom()
                , maxmin: true
                , area: ['60%', '80%']
            // , area: [window.screen.width / 2 + 'px', window.screen.height / 2 + 'px'] //宽高
                , btn: ['确定', '取消']
                , btnAlign: 'c'
                , yes: function (index, layero) {

                    var iframeWindow = window['layui-layer-iframe' + index]
                        , submitID = 'NF-FlowTemp-FormSubmit'
                        , submit = layero.find('iframe').contents().find('#' + submitID);
                    //监听提交
                    iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                        var field = obj.field; //获取提交的字段
                        var fieldval = field.Name;
                        var fieldcode = field.Code;

                        var resname = wooutil.UniqueValObj({
                            url: '/WorkFlow/FlowTemp/CheckInputValExist',
                            fieldName: 'Name',
                            inputVal: fieldval,
                            currId: field.Id
                        });
                        if (resname) {
                            return layer.msg('此名称已经存在！');
                        }
                        //判断流程是否唯一
                        var soltstr = UniqueFlow({ url: '/WorkFlow/FlowTemp/CheckFlowUnique', data: obj.field });
                        if (soltstr.length != 0) {
                            return layer.alert("与流程【" + soltstr + "】相冲突");
                        }

                        wooutil.OpenSubmitForm({
                            url: '/WorkFlow/FlowTemp/UpdateSave',
                            table: table,
                            data: field,
                            tableId: 'NF-WorkFlow-Temp',
                            msg: '保存成功',
                            index: index

                        });
                        return false;

                    });

                    submit.trigger('click');
                },
            success: function (layero, index) {
                layer.full(index);
                // wooutil.openTip();
                if (typeof _success === 'function') {
                    setTimeout(function () {
                        _success();
                    }, 500)


                }
            }
        });
    }
    /**
    *打开看页面
    **/
    function openview(obj) {
        layer.open({
            type: 2
                , title: '查看详情'
                , content: '/WorkFlow/FlowTemp/Detail?Id=' + obj.data.Id + "&rand=" + wooutil.getRandom()
                , maxmin: true
                , area: ['60%', '80%']
                , btnAlign: 'c'
                , skin: "layer-nf-nfskin"
               , success: function (layero, index) {
                   layer.full(index);
                   wooutil.openTip();
               }
        });
    }
    /**
    *流程节点设置
    **/
    function SetFlowNode(obj) {
        var _furl='/WorkFlow/FlowTemp/SetFlowNode?Id=' 
            + obj.data.Id + '&ftitle=' + encodeURI(encodeURI(obj.data.Name)) 
            +'&vs='+obj.data.Version
            +'&ftype='+encodeURI(encodeURI(obj.data.ObjTypeDic))
            + "&rand=" + wooutil.getRandom();
        layer.open({
            type: 2
                , title: '查看/编辑流程图'
                , content: _furl
                , maxmin: true
                , area: ['60%', '80%']
                , cancel: function (index, layero) {
                    admin.req({
                        async: false,
                        url: "/WorkFlow/FlowTemp/ChekAppFlowData",
                        data: { tempId: obj.data.Id },
                        type: 'POST',
                        done: function (res) {
                            if (res.RetValue !== 0) {
                                layer.confirm('有节点设置信息未设置正确,是否强制关闭？', { icon: 3, title: '提示信息' }, function (index2) {
                                    layer.close(index);
                                    layer.close(index2);
                                });
                            } else {
                                layer.close(index);
                                layer.close(index2);
                            }
                            //if (res.RetValue === 1) {
                            //    return layer.alert("请设置节点信息！");
                            //    return false;
                            //} else if (res.RetValue === 2) {
                            //    return layer.alert("请设置节点审批人！");
                            //    return false;
                            //} else {
                            //layer.close(index);
                            //}

                        }
                    });
                    return false;
                    
                }
                //, btn: ['确定', '取消']
                //, btnAlign: 'c'
                //, yes: function (index, layero) {

                //    var iframeWindow = window['layui-layer-iframe' + index]
                //        , submitID = 'NF-FlowTemp-FormSubmit'
                //        , submit = layero.find('iframe').contents().find('#' + submitID);
                //    //监听提交
                //    iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                //        var field = obj.field; //获取提交的字段
                //        var fieldval = field.Name;
                //        var fieldcode = field.Code;

                //        var resname = wooutil.UniqueValObj({
                //            url: '/WorkFlow/FlowTemp/CheckInputValExist',
                //            fieldName: 'Name',
                //            inputVal: fieldval,
                //            currId: field.Id
                //        });
                //        if (resname) {
                //            return layer.msg('此名称已经存在！');
                //        }
                //        //判断流程是否唯一
                //        var soltstr = UniqueFlow({ url: '/WorkFlow/FlowTemp/CheckFlowUnique', data: obj.field });
                //        if (soltstr.length != 0) {
                //            return layer.alert("与流程【" + soltstr + "】相冲突");
                //        }

                //        wooutil.OpenSubmitForm({
                //            url: '/WorkFlow/FlowTemp/UpdateSave',
                //            table: table,
                //            data: field,
                //            tableId: 'NF-WorkFlow-Temp',
                //            msg: '保存成功',
                //            index: index

                //        });
                //        return false;

                //    });

                //    submit.trigger('click');
                //},
            ,success: function (layero, index) {
                layer.full(index);
                //wooutil.openTip();
                
            }
        });
    }
    /**
    *列工具栏
    **/
    table.on('tool(NF-WorkFlow-Temp)', function (obj) {
        switch (obj.event) {
            case 'del':
                wooutil.deleteInfo({ tableId: "NF-WorkFlow-Temp", data: obj, url: '/WorkFlow/FlowTemp/Delete' });
                break;
            case 'edit':
                editFunc(obj, null);
                break;
            case 'detail':
               // openview(obj);
                break;
            case "setflow":
                SetFlowNode(obj);
                break;
            default:
                layer.alert('操作的是什么鬼->' + obj.event);
                break;

        }

    });
    //监听状态操作
    form.on('switch(IsValid)', function (obj) {
        var state = obj.elem.checked ? 1 : 0;//状态
        admin.req({
            url: '/WorkFlow/FlowTemp/UpdateField',
            data: { Id: this.value, FieldName: "IsValid", FieldValue: state },
            done: function (res) {
                layer.msg('修改成功！');

            }

        });
    });



    exports('workFlowTempIndex', {});
});