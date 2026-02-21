/**
*审批组
**/
layui.define(['table', 'form'], function (exports) {
    var $ = layui.$
   , table = layui.table
   , setter = layui.setter
   , admin = layui.admin
   , form = layui.form;
    var logdindex = layer.load(0, { shade: false });

    table.render({
        elem: '#NF-WorkFlow-Group'
       , url: '/WorkFlow/GroupInfo/GetList?rand=' + wooutil.getRandom()
       , toolbar: '#toolgroupinfo'
       , defaultToolbar: ['filter']
       , cellMinWidth: 80
       , cols: [[
             { type: 'numbers', fixed: 'left' }
           , { type: 'checkbox', fixed: 'left' }
           , { field: 'Name', title: '名称', width: 150, templet: '#nameTpl', fixed: 'left' }
           , { field: 'Remark', title: '说明', width: 260 }
           , { field: 'CreateUserName', title: '建立人', width: 120}
           , { field: 'CreateDateTime', title: '建立日期', width: 120}
           , { field: 'Id', title: 'Id', width: 50, hide: true }
           , { field: 'Gstate', width: 120, title: '状态', align: 'center', templet: '#GroupstateTpl', unresize: true }
           , { title: '操作', width: 150, align: 'center', fixed: 'right', toolbar: '#table-WorkFlow-Grouptbar' }
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

    var openAdd = function () {
        layer.open({
            type: 2
            , title: '新增组'
            , content: '/WorkFlow/GroupInfo/Build'
            , maxmin: true
            , area: ['60%', '80%']
            , btn: ['确定', '取消']
            , btnAlign: 'c'
            , skin: "layer-ext-myskin"
            , yes: function (index, layero) {
                var iframeWindow = window['layui-layer-iframe' + index]
                    , submitID = 'NF-WorkFlow-GroupFormSubmit'
                    , submit = layero.find('iframe').contents().find('#' + submitID);
                //监听提交
                iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                    var field = obj.field; //获取提交的字段
                    var fieldval = obj.field.Name;
                    var resname = wooutil.UniqueValObj({
                        url: '/WorkFlow/GroupInfo/CheckInputValExist',
                        fieldName: 'Name',
                        inputVal: fieldval,
                        currId: 0
                    });
                    if (resname) {
                        return layer.msg('此名称已经存在！');
                    }
                    
                    wooutil.OpenSubmitForm({
                        url: '/WorkFlow/GroupInfo/Save',
                        data: obj.field,
                        table: table,
                        index: index,
                        tableId: 'NF-WorkFlow-Group'
                    });
                    return false;
                });

                submit.trigger('click');
            },
            success: function (layero, index) {
               // layer.full(index);
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
            wooutil.deleteDatas({ tableId: 'NF-WorkFlow-Group', table: table, url: '/WorkFlow/GroupInfo/Delete' });
        },
        search: function () {//查询
            $("input[name=hide_keyWord]").val($("input[name=keyWord]").val());
            table.reload('NF-WorkFlow-Group', {
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
    table.on('toolbar(NF-WorkFlow-Group)', function (obj) {
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
            //default:
            //    layer.alert("你操作是什么鬼->" + obj.event);
            //    break;
        };
    });
    /**
    *修改信息
    **/
    function editFunc(obj, _success) {
        layer.open({
            type: 2
                , title: '修改信息'
                , content: '/WorkFlow/GroupInfo/Build?Id=' + obj.data.Id + "&rand=" + wooutil.getRandom()
                , maxmin: true
                , area: ['60%', '80%']
            // , area: [window.screen.width / 2 + 'px', window.screen.height / 2 + 'px'] //宽高
                , btn: ['确定', '取消']
                , btnAlign: 'c'
                , yes: function (index, layero) {

                    var iframeWindow = window['layui-layer-iframe' + index]
                        , submitID = 'NF-WorkFlow-GroupFormSubmit'
                        , submit = layero.find('iframe').contents().find('#' + submitID);
                    //监听提交
                    iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                        var field = obj.field; //获取提交的字段
                        var fieldval = field.Name;
                        var fieldcode = field.Code;

                        var resname = wooutil.UniqueValObj({
                            url: '/WorkFlow/GroupInfo/CheckInputValExist',
                            fieldName: 'Name',
                            inputVal: fieldval,
                            currId: field.Id
                        });
                        if (resname) {
                            return layer.msg('此名称已经存在！');
                        }
                        
                        wooutil.OpenSubmitForm({
                            url: '/WorkFlow/GroupInfo/UpdateSave',
                            table: table,
                            data: field,
                            tableId: 'NF-WorkFlow-Group',
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
                , content: '/WorkFlow/GroupInfo/Detail?Id=' + obj.data.Id + "&rand=" + wooutil.getRandom()
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
    *列工具栏
    **/
    table.on('tool(NF-WorkFlow-Group)', function (obj) {
        switch (obj.event) {
            case 'del':
                wooutil.deleteInfo({ tableId: "NF-WorkFlow-Group", data: obj, url: '/WorkFlow/GroupInfo/Delete' });
                break;
            case 'edit':
                editFunc(obj, null);
                break;
            case 'detail':
                openview(obj);
                break;
            default:
                layer.alert('操作的是什么鬼->'+obj.event);
                break;

        }
        
    });
    //监听状态操作
    form.on('switch(Gstate)', function (obj) {
        //layer.tips(this.value + ' ' + this.Dstatus + '：' + obj.elem.checked, obj.othis);
        var state = obj.elem.checked ? 1 : 0;//状态
        admin.req({
            url: '/WorkFlow/GroupInfo/UpdateField',
            data: { Id: this.value, FieldName: "Gstate", FieldValue: state },
            done: function (res) {
                layer.msg('修改成功！');

            }

        });
    });
    


    exports('groupInfoIndex', {});
});