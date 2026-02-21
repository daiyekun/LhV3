/**
 @Name：币种管理
 @Author：dyk
 */
layui.define(['table', 'form'], function (exports) {
    var $ = layui.$
   , table = layui.table
   , setter = layui.setter
   , admin = layui.admin
   , form = layui.form;
    var logdindex = layer.load(0, { shade: false });
    //列表
    table.render({
        elem: '#NF-CurrencyManager-index'
        , url: '/System/CurrencyManager/GetList?rand=' + wooutil.getRandom()
        , toolbar: '#toolCurrencyManager'
        , defaultToolbar: ['filter']
        , cellMinWidth: 80
        , cols: [[
            { type: 'numbers', fixed: 'left' }
            ,{ type: 'checkbox', fixed: 'left' }
            , { field: 'Name', title: '名称', minWidth: 160, fixed: 'left', sort: true }
            , { field: 'Rate', title: '汇率', width: 130, sort: true }
            , { field: 'ShortName', title: '简称', width: 130 }
            , { field: 'Abbreviation', title: '英文缩写', width: 130 }
            , { field: 'Code', title: '编码', width: 130 }
            , { field: 'Remark', title: '备注', width: 200 }
            , { field: 'Id', title: 'Id', width: 50, hide: true }
            , { title: '操作', width: 150, align: 'center', fixed: 'right', toolbar: '#table-CurrencyManager-bar' }
        ]]
        , page: true
        , loading: true
        , height: setter.table.height_4
        , limit: setter.table.limit
        , limits: setter.table.limits
        , done: function (res) {   //返回数据执行回调函数
            layer.close(logdindex);    //返回数据关闭loading
            $("input[name=keyWord]").val($("input[name=hide_keyWord]").val());
            $("input[name=hide_keyWord]").val("");

        }

    });
    //监听表格排序
    table.on('sort(NF-CurrencyManager-index)', function (obj) {
        table.reload('NF-CurrencyManager-index', { //testTable是表格容器id
            initSort: obj //记录初始排序，如果不设的话，将无法标记表头的排序状态。 layui 2.1.1 新增参数
            , where: { //请求参数（注意：这里面的参数可任意定义，并非下面固定的格式）
                orderField: obj.field //排序字段
            , orderType: obj.type //排序方式
            , keyWord: $("input[name=keyWord]").val()//查询关键字
            }
            , page: { curr: 1 }//重新从第 1 页开始
        });


    });

    /***********************************************监听头部工具栏---begin*****************************************************/
  
    var active = {
        add: function () {//新增
            layer.open({
                type: 2
             , title: '新增币种'
             , content: '/System/CurrencyManager/Build'
                // , maxmin: true
             , area: ['60%', '80%']
             , btn: ['确定', '取消']
             , btnAlign: 'c'
             , skin: "layer-ext-myskin"
             , yes: function (index, layero) {
                
                 var iframeWindow = window['layui-layer-iframe' + index]
                     , submitID = 'Nf-CurrencyManager-FormSubmit'
                     , submit = layero.find('iframe').contents().find('#' + submitID);
                 //监听提交
                 iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                     var field = obj.field; //获取提交的字段
                     var fieldval = obj.field.Name;
                     var fieldcode = obj.field.Code;
                    
                     var resname = wooutil.UniqueValObj({
                         url: '/System/CurrencyManager/CheckInputValExist',
                         fieldName: 'Name',
                         inputVal: fieldval,
                         currId: 0
                     });
                     if (resname) {
                         return layer.msg('此名称已经存在！');
                     }
                     var rescode = wooutil.UniqueValObj({
                         url: '/System/CurrencyManager/CheckInputValExist',
                         fieldName: 'Code',
                         inputVal: fieldcode,
                         currId: 0
                     });
                     if (rescode) {
                         return layer.msg('此编号已经存在！');
                     }
                     wooutil.OpenSubmitForm({
                         url: '/System/CurrencyManager/Save',
                         data: obj.field,
                         table: table,
                         index: index,
                         tableId: 'NF-CurrencyManager-index'
                     });
                     return false;
                 });

                 submit.trigger('click');
             },
                success: function (layero, index) {

                }
            });
        },
        batchdel: function () {//删除
            wooutil.deleteDatas({ tableId: 'NF-CurrencyManager-index', table: table, url: '/System/CurrencyManager/Delete' });
        },
        search: function () {//查询
            $("input[name=hide_keyWord]").val($("input[name=keyWord]").val());
            table.reload('NF-CurrencyManager-index', {
                page: { curr: 1 }
                , where: {
                    keyWord: $("input[name=keyWord]").val()

                }
            });
        }
    };

    table.on('toolbar(NF-CurrencyManager-index)', function (obj) {
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
         


        };
    });
    /***********************************************监听头部工具栏---end**************************************************************/

    /**********************************************************监听工具条-begin*********************************************************/
    function editFunc(obj, _success) {
        layer.open({
            type: 2
                , title: '修改信息'
                , content: '/System/CurrencyManager/Build?Id=' + obj.data.Id + "&rand=" + wooutil.getRandom()
               // , maxmin: true
                , area: ['60%', '80%']
            // , area: [window.screen.width / 2 + 'px', window.screen.height / 2 + 'px'] //宽高
                , btn: ['确定', '取消']
                , btnAlign: 'c'
                , yes: function (index, layero) {

                    var iframeWindow = window['layui-layer-iframe' + index]
                        , submitID = 'Nf-CurrencyManager-FormSubmit'
                        , submit = layero.find('iframe').contents().find('#' + submitID);
                    //监听提交
                    iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                        var field = obj.field; //获取提交的字段
                        var fieldval = field.Name;
                        var fieldcode = field.Code;
                       
                        var resname = wooutil.UniqueValObj({
                            url: '/System/CurrencyManager/CheckInputValExist',
                            fieldName: 'Name',
                            inputVal: fieldval,
                            currId: field.Id
                        });
                        if (resname) {
                            return layer.msg('此名称已经存在！');
                        }
                        var rescode = wooutil.UniqueValObj({
                            url: '/System/CurrencyManager/CheckInputValExist',
                            fieldName: 'Code',
                            inputVal: fieldcode,
                            currId: field.Id
                        });
                        if (rescode) {
                            return layer.msg('此编号已经存在！');
                        }
                        
                        wooutil.OpenSubmitForm({
                            url: '/System/CurrencyManager/UpdateSave',
                            table: table,
                            data: field,
                            tableId: 'NF-CurrencyManager-index',
                            msg: '保存成功',
                            index: index

                        });
                        return false;

                    });

                    submit.trigger('click');
                },
            success: function (layero, index) {
                
                if (typeof _success === 'function') {
                    setTimeout(function () {
                        _success();
                    }, 500)


                }
            }
        });
    }
    /**编辑带权限*/
    function customEdit(obj, success) {
        editFunc(obj, success);
    }
    //打开客户查看页面
    function openview(obj) {

        layer.open({
            type: 2
                , title: '查看详情'
                , content: '/System/CurrencyManager/Detail?Id=' + obj.data.Id + "&rand=" + wooutil.getRandom()
                , maxmin: true
                , area: ['60%', '80%']
                 , btnAlign: 'c'
                , btn: ['修改', '删除']// , area: [window.screen.width / 2 + 'px', window.screen.height / 2 + 'px'] //宽高
            , btn1: function (index, layero) {
                var success = function () {
                    layer.close(index);
                }
                customEdit(obj, success);
                // editFunc(obj, success);
            }, btn2: function (index, layero) {
                var suc = function () {
                    layer.close(index);
                }
                wooutil.deleteInfo({ tableId: "NF-CurrencyManager-index", data: obj, url: '/System/CurrencyManager/Delete', success: suc });
               
                return false;
            }
                , success: function (layero, index) {
                    layer.full(index);
                    wooutil.openTip();
                }
        });
    };

    table.on('tool(NF-CurrencyManager-index)', function (obj) {
        if (obj.event === 'del') {//删除
            wooutil.deleteInfo({ tableId: "NF-CurrencyManager-index", data: obj, url: '/System/CurrencyManager/Delete' });

        }
        else if (obj.event === 'edit') {//修改客户
            customEdit(obj);
        } 
    });
    /*********************************************************监听工具栏-end***************************************************************************/



    exports('currencymanagerIndex', {});
});