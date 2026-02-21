/**
 @Name：菜单管理-TreeTable
 @Author：dyk 20180816
 */
layui.define(['table', 'form', 'treetable'], function (exports) {
    var $ = layui.$
        , table = layui.table
        , setter = layui.setter
        , admin = layui.admin
        , form = layui.form
        , treetable = layui.treetable;
    var loadindex = wooutil.loading();
    // 渲染表格
    var renderTable = function () {
        
        treetable.render({
            treeColIndex: 1,          // treetable新增参数
            treeSpid: -1,             // treetable新增参数
            treeIdName: 'd_Id',       // treetable新增参数
            treePidName: 'd_Pid',     // treetable新增参数
            treeDefaultClose: false,   // treetable新增参数
            treeLinkage: true,        // treetable新增参数
            elem: '#NF-system-sysmodelTree',
            url: '/System/sysmodel/GetListAll?rand=' + wooutil.getRandom()
            , height: setter.table.height_2,
            cols: [[
                { type: 'numbers' },
                //{ type: 'checkbox' },
                //{ field: 'Id', title: 'ID' },
                { field: 'Name', title: '名称', width: 180, minWidth: 180 },
                { field: 'No', title: '编号', width: 120, minWidth: 80, },
                { field: 'Ico', title: '图标', width: 100, templet: '#IcoTpl' }
                , { field: 'RequestUrl', title: 'URL', width: 140 }
                , { field: 'AreaName', title: '区域名称', width: 140 }
                , { field: 'IsShow', title: '是否显示', width: 130, templet: '#sysmodelIsshowTpl' }
                , { field: 'Sort', title: '排序', width: 150, align: 'center', toolbar: '#table-system-sysmodelordertbar' }
                , { title: '操作', width: 150, align: 'center', templet: '#table-system-sysmodeltbar' }
            ]],
            done: function () {
                layer.closeAll(loadindex);
            }
        });
    }
    renderTable();

    exports('nf-system-sysmodels_treetable', {})
});
