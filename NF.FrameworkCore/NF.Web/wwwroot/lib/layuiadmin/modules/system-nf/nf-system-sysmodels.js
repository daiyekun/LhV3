/**
 @Name：菜单管理
 @Author：dyk 20180816
 */
layui.define(['table', 'form'], function (exports) {
    var $ = layui.$
        , table = layui.table
        , setter = layui.setter
        , admin = layui.admin
        , form = layui.form
    var loadindex = wooutil.loading();
    table.render({
        elem: '#NF-system-sysmodel'
        , url: '/System/sysmodel/GetList?rand=' + wooutil.getRandom()
        , cols: [[
            { type: 'numbers', fixed: 'left' },
            { type: 'checkbox', fixed: 'left' }
            , { field: 'Name', title: '名称', minWidth: 140, templet: '#nameTpl', fixed: 'left' }
            , { field: 'No', title: '编号', width: 130 }
            , { field: 'Ico', title: '图标', width: 100, templet: '#IcoTpl', }
            , { field: 'ControllerName', title: '控制器名称', width: 130 }
            , { field: 'ActionName', title: '方法名称', width: 130 }
            , { field: 'RequestUrl', title: 'URL', width: 140 }
            , { field: 'AreaName', title: '区域名称', width: 140 }
            , { field: 'IsShow', title: '是否显示', width: 130, templet: '#sysmodelIsshowTpl' }
            , { field: 'Sort', title: '排序', width: 150, align: 'center', toolbar: '#table-system-sysmodelordertbar' }
            , { field: 'CreateUserName', title: '创建人', width: 120 }
            , { field: 'CreateDatetime', width: 110, title: '创建时间' }
            , { title: '操作', width: 150, align: 'center', fixed: 'right', toolbar: '#table-system-sysmodeltbar' }
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
    table.on('tool(NF-system-sysmodel)', function (obj) {
        var _data = obj.data;
        if (obj.event === 'del') {
            wooutil.deleteInfo({ tableId: "NF-system-sysmodel", data: obj, url: '/System/Sysmodel/Delete' });

        }
        else if (obj.event === 'edit') {
            layer.open({
                type: 2
                , title: '修改菜单'
                , content: '/System/sysmodel/Build?Id=' + obj.data.Id + "&rand=" + wooutil.getRandom()
                , maxmin: true
                // ,area: ['60%', '80%']   
                , area: [window.screen.width / 2 + 'px', window.screen.height / 2 + 'px'] //宽高
                , btn: ['确定', '取消']
                , btnAlign: 'c'
                , yes: function (index, layero) {

                    var iframeWindow = window['layui-layer-iframe' + index]
                        , submitID = 'Nf-SystemAdmin-sysmodelFormSubmit'
                        , submit = layero.find('iframe').contents().find('#' + submitID);
                    //监听提交
                    iframeWindow.layui.form.on('submit(' + submitID + ')', function (data) {
                        var field = data.field; //获取提交的字段
                        var resname = wooutil.UniqueValObj({
                            url: '/System/sysmodel/CheckInputValExist',
                            fieldName: 'Name',
                            inputVal: field.Name,
                            currId: field.Id
                        });
                        if (!resname) {
                            wooutil.OpenSubmitForm({
                                table: table,
                                url: "/System/sysmodel/UpdateSave",
                                tableId: 'NF-system-sysmodel',
                                data: field,
                                index: index
                            });

                        } else {
                            layer.msg('当前菜单已经存在！');

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
                , title: '查看菜单'
                , content: '/System/sysmodel/Detail?Id=' + obj.data.Id + "&rand=" + wooutil.getRandom()
                , maxmin: true
                , area: [window.screen.width / 2 + 'px', window.screen.height / 2 + 'px'] //宽高
                , success: function (layero, index) {
                    layer.full(index);
                    wooutil.openTip();
                }
            });
        }
        else if (obj.event === 'up')
        {
            var _tempobj = { Id: obj.data.Id, fieldName: "Sort", fieldValue: 1 };
            var success = function () {
                table.reload("NF-system-sysmodel");
            }
            wooutil.adminreq('/System/Role/UpdateField', _tempobj, '操作成功', success);
        } else if (obj.event === 'down') {


        }
    });

  

    exports('systemSysmodels', {})
});