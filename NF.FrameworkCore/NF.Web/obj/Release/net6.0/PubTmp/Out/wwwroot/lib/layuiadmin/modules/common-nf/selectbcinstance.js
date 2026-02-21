/**
*单品选择
**/
layui.define(['table', 'form', 'tree'], function (exports) {
    var $ = layui.$
    , table = layui.table
    , setter = layui.setter
    , admin = layui.admin
    , tree = layui.tree;
    //树
    function InitTree() {
        admin.req({
            url: '/Business/BusinessCategory/GetBcCateTreeData'
            , success: function (res) {
                        var treedata = res.Data;
                        tree.render({
                            elem: '#bc_Tree',
                            height: "full-95"
                             , spread: true, //展开
                            data: treedata
                                       , id: "bctree"
                                       , click: function (obj) {
                                           table.reload("NF-SelectBcInstance-Index", {
                                               page: { curr: 1 }
                                              , where: {
                                                  cateIds: obj.data.id
                                              }
                                           });

                                       }
                        });
                    }
        });
    }
  //  InitTree();
    TreeInit();
    function TreeInit() {
        admin.req({
            url: '/Business/BusinessCategory/GetBcCateTreeData?rand=' + wooutil.getRandom()
            , success: function (res) {
                tree.render(
                    {
                        elem: '#bc_Tree',
                        height: "full-95",
                        data: res.Data
                        , id: 'bctree'
                        , click: function (obj) {
                            //layer.alert(JSON.stringify(obj.data));
                            // layer.alert(obj.data.id + ":" + obj.data.title);
                            $("#selCateName").html(obj.data.title);
                            $("#cateId").val(obj.data.id);

                            //table.reload("NF-ContSub", {
                            //    page: { curr: 1 }
                            //    , where: { cateIds: obj.data.id }
                            //});

                        }
                    });
            }
        });
    };
    //单品列表
    var _reqUrl = '/Business/BcInstance/GetList?rand=' + wooutil.getRandom();
    table.render({
        elem: '#NF-SelectBcInstance-Index'
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
           //, { title: '操作', width: 150, align: 'center', fixed: 'right', toolbar: '#table-actfinance-bar' }
       ]]
       , page: true
       , loading: true
       , height: setter.table.height_4
       , limit: setter.table.limit
       , limits: setter.table.limits
       , done: function (res, curr, count) {   //返回数据执行回调函数
           //layer.close(logdindex);    //返回数据关闭loading
           $("input[name=keyWord]").val($("input[name=hide_keyWord]").val());
           $("input[name=hide_keyWord]").val("");


       }

    });
    /**工具栏事件**/
    table.on('toolbar(NF-SelectBcInstance-Index)', function (obj) {
        switch (obj.event) {
            case "search":
                active.search();
                break;
               
               }
     });

    /**相关操作**/
    var active = {
        search: function () {//查询
            $("input[name=hide_keyWord]").val($("input[name=keyWord]").val());
            table.reload('NF-SelectBcInstance-Index', {
                page: { curr: 1 }
                , where: {
                    keyWord: $("input[name=keyWord]").val()

                }
            });
        }
    }

    exports('selectBcInstance', {});
});