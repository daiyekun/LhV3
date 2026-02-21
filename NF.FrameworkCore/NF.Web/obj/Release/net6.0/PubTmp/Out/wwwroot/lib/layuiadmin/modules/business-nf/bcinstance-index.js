/**
*单品管理
**/
layui.define(['table', 'form', 'nfBcTree'], function (exports) {
    var $ = layui.$
   , table = layui.table
   , setter = layui.setter
   , admin = layui.admin
   , form = layui.form
   , nfBcTree = layui.nfBcTree
    ;
   
    var logdindex = layer.load(0, { shade: false });
    var _reqUrl = '/Business/BcInstance/GetList?rand='+ wooutil.getRandom();
   
    //列表
    table.render({
        elem: '#NF-BcInstance-Index'
       , url: _reqUrl
       , toolbar: '#toolbcinstance'
       , defaultToolbar: ['filter']
       , cellMinWidth: 80
       , cols: [[
             { type: 'numbers', fixed: 'left' }
           , { type: 'checkbox', fixed: 'left' }
           , { field: 'CatePath', title: '所属类别', width: 180, fixed: 'left' }
           , { field: 'Name', title: '单品名称', width: 220, templet: '#nameTpl', fixed: 'left' }
           , { field: 'Code', title: '单品编号', width: 150, sort: true, fixed: 'left' }
           , { field: 'Unit', title: '单位', width: 120 }
           , { field: 'PriceThod', title: '报价', width: 150 }
           , { field: 'ProDic', title: '属性', width: 150 }
           , { field: 'CreateUserName', title: '建立人', width: 130 }
           , { field: 'CreateDateTime', title: '建立时间', width: 130, hide: true, sort: true }
           , { field: 'Id', title: 'Id', width: 100, hide: true }
           , { field: 'Remark', title: '备注', width: 180, hide: true }
           , { title: '操作', width: 150, align: 'center', fixed: 'right', toolbar: '#table-actfinance-bar' }
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
    /**定义方法**/
    var eventMethod = {
         openAdd:function () {
            layer.open({
                type: 2
             , title: '新增单品'
             , content: '/Business/BcInstance/Build'
             , maxmin: true
             , area: ['60%', '80%']
             , btn: ['确定', '取消']
             , btnAlign: 'c'
             , skin: "layer-ext-myskin"
             , yes: function (index, layero) {
                 var iframeWindow = window['layui-layer-iframe' + index]
                     , submitID = 'NF-BcInstance-FormSubmit'
                     , submit = layero.find('iframe').contents().find('#' + submitID);
                 //监听提交
                 iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                     var field = obj.field; //获取提交的字段
                     var fieldval = obj.field.Name;
                     var fieldcode = obj.field.Code;

                     var resname = wooutil.UniqueValObj({
                         url: '/Business/BcInstance/CheckInputValExist',
                         fieldName: 'Name',
                         inputVal: fieldval,
                         currId: 0
                     });
                     if (resname) {
                         return layer.msg('此名称已经存在！');
                     }
                     var rescode = wooutil.UniqueValObj({
                         url: '/Business/BcInstance/CheckInputValExist',
                         fieldName: 'Code',
                         inputVal: fieldcode,
                         currId: 0
                     });
                     if (rescode) {
                         return layer.msg('此编号已经存在！');
                     }
                     wooutil.OpenSubmitForm({
                         url: '/Business/BcInstance/Save',
                         data: obj.field,
                         table: table,
                         index: index,
                         tableId: 'NF-BcInstance-Index'
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
    };
    /**
    *相关事件
    **/
    var actEvent = {
        add: function () {
            var ress = wooutil.requestpremission({
                url: '/NfCommon/NfPermission/AddPermission?rand=' + wooutil.getRandom(),
                data: {
                    FuncCode: 'addBcInstance'
                }
            });
            if (ress.RetValue == 0) {
                eventMethod.openAdd();
            } else {
                return layer.alert(ress.Msg);
            }
        },
        batchdel: function () {//删除
            wooutil.deleteDatas({ tableId: 'NF-BcInstance-Index', table: table, url: '/Business/BcInstance/Delete' });
        },
        search: function () {//查询
            table.reload('NF-BcInstance-Index', {
                page: { curr: 1 }
                , where: {
                    keyWord: $("input[name=keyWord]").val()

                }
            });
        },
        cateSearch: function () {//类别查询 
            nfBcTree.showTree({ el: '#cateDiv', tableId: 'NF-BcInstance-Index', treeId: 'bccateTree', hideFild: '#hdselCateIds' });
        }
    };
    /**头部工具栏监听**/
    table.on('toolbar(NF-BcInstance-Index)', function (obj) {
        switch (obj.event) {
            case 'add':
                actEvent.add();
                break;
            case 'batchdel':
                actEvent.batchdel();
                break;
            case 'search':
                actEvent.search();
                break;
            case 'stateChange'://状态流转
                actEvent.submitState(this);
                break;
            case 'cateSearch'://类别查询
                actEvent.cateSearch();
                break;
            case "exportexcel":
                wooutil.exportexcel(obj, { url: "/Business/BcInstance/ExportExcel?cateIds=" + $("#hdselCateIds").val() });
                break;


        };
    });
    function editFunc(obj, _success) {
        layer.open({
            type: 2
                , title: '修改信息'
                , content: '/Business/BcInstance/Build?Id=' + obj.data.Id + "&Ctype=0&rand=" + wooutil.getRandom()
                , maxmin: true
                , area: ['60%', '80%']
            // , area: [window.screen.width / 2 + 'px', window.screen.height / 2 + 'px'] //宽高
                , btn: ['确定', '取消']
                , btnAlign: 'c'
                , yes: function (index, layero) {

                    var iframeWindow = window['layui-layer-iframe' + index]
                        , submitID = 'NF-BcInstance-FormSubmit'
                        , submit = layero.find('iframe').contents().find('#' + submitID);
                    //监听提交
                    iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                        var field = obj.field; //获取提交的字段
                        var fieldval = field.Name;
                        var fieldcode = field.Code;

                        var resname = wooutil.UniqueValObj({
                            url: '/Business/BcInstance/CheckInputValExist',
                            fieldName: 'Name',
                            inputVal: fieldval,
                            currId: field.Id
                        });
                        if (resname) {
                            return layer.msg('此名称已经存在！');
                        }
                        var rescode = wooutil.UniqueValObj({
                            url: '/Business/BcInstance/CheckInputValExist',
                            fieldName: 'Code',
                            inputVal: fieldcode,
                            currId: field.Id
                        });
                        if (rescode) {
                            return layer.msg('此编号已经存在！');
                        }
                        
                        wooutil.OpenSubmitForm({
                            url: '/Business/BcInstance/Save',
                            table: table,
                            data: field,
                            tableId: 'NF-BcInstance-Index',
                            msg: '保存成功',
                            index: index

                        });
                        return false;

                    });

                    submit.trigger('click');
                },
            success: function (layero, index) {
                layer.full(index);
                wooutil.openTip();
                if (typeof _success === 'function') {
                    setTimeout(function () {
                        _success();
                    }, 500)


                }
            }
        });
    }
    /**编辑带权限*/
    function bcInstanceEdit(obj, success) {
        var cate = layui.data(setter.tableName).userName;
        var ress = wooutil.requestpremission({
            url: '/NfCommon/NfPermission/AddPermission?rand=' + wooutil.getRandom(),
            data: {
                FuncCode: 'addBcInstance',
                ObjId: obj.data.Id
            }
        });
        if (ress.RetValue == 0) {
            editFunc(obj, success);
        } else if (cate == "SuperAdministrator") {
            editFunc(obj, success);
        } else {
            return layer.alert(ress.Msg);
        }
    }

    /**
    *打开合同查看页面
    **/
    function openview(obj) {

        layer.open({
            type: 2
                , title: '查看详情'
                , content: '/Business/BcInstance/Detail?Id=' + obj.data.Id + "&rand=" + wooutil.getRandom()
                , maxmin: true
                , area: ['60%', '80%']
                , btnAlign: 'c'
                , skin: "layer-nf-nfskin"
                , btn: ['关闭']// , area: [window.screen.width / 2 + 'px', window.screen.height / 2 + 'px'] //宽高
                , yes: function (index, layero) {
                    layer.close(index);
              
                
            }
           , success: function (layero, index) {
               layer.load(0, { shade: false, time: 1 * 1000 });//2秒自动关闭
               layer.full(index);
               wooutil.openTip();
           
           }
        });
    };

    /**操作列**/
    table.on('tool(NF-BcInstance-Index)', function (obj) {
        if (obj.event === 'del') {//删除

            wooutil.deleteInfo({ tableId: "NF-BcInstance-Index", data: obj, url: '/Business/BcInstance/Delete' });
        }
        else if (obj.event === 'edit') {//修改客户
            bcInstanceEdit(obj);
        } else if (obj.event === 'detail') {//查看详情
            var ress = wooutil.requestpremission({
                url: '/NfCommon/NfPermission/ViewPermission?rand=' + wooutil.getRandom(),
                data: {
                    FuncCode: 'detailBcInstance',
                    ObjId: obj.data.Id
                }

            });
            if (ress.RetValue == 0) {
                openview(obj);
            } else {
                return layer.alert(ress.Msg);
            }


        }
    });
    //类别菜单初始化
    nfBcTree.render({ el: '#cateDiv', treeEl: '#BcCategory_Tree', treeId:'bccateTree' });


    exports('bcInstanceIndex', {});
});