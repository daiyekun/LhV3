/**
  系统功能
**/
layui.define(['layer', 'form', 'tree', 'table'], function (exports) {
    var layer = layui.layer
        , $ = layui.$
        , setter = layui.setter
        , form = layui.form
        , tree = layui.tree
        , admin = layui.admin
        , table = layui.table;
    var loadindex = wooutil.loading();
    //定义列
    var tempcol = [
        { type: 'numbers', fixed: 'left' }
        //{ type: 'checkbox', fixed: 'left' }
        ,{ field: 'Name', title: '功能名称', width: 300 }
        , { field: 'FunIdentify', title: '功能标识', width: 200 }
        , { field: 'Remark', title: '备注', width: 220 }
        //, { title: '操作', width: 150, align: 'center', fixed: 'right', toolbar: '#table-sysfunctiontbar' }
    ];
    //表格设置
    var tableoption = {
        elem: '#NF-sysfunctionTable'
        , url: '/System/SysFunction/GetList?rand=' + wooutil.getRandom()
        , cols: [tempcol]
        , page: true
        , loading: true
        , height: setter.table.height
        , limit:50 //setter.table.limit
        , limits: setter.table.limits
        , done: function (res) {
            layer.close(loadindex);
        }

    };
    //渲染表格
    var tableInce = table.render(tableoption);

    //菜单树
    admin.req({
        url: '/System/SysFunction/GetMenuTree'
        , success: function (res) {
            var treedata = res.Data;
            tree.render({
                elem: '#sysleft_Tree',
                height: "full-95",
                data: treedata,
                click: function (obj) {
                    $("input[name='dataType']").val(item.data.id);
                            tableoption.url = '/System/SysFunction/GetList?modeId=' + item.data.id + '&rand=' + wooutil.getRandom()
                            tableInce.reload(tableoption);

                }

               })
           

        }
    });


    exports('systemSysfunction', {});
});