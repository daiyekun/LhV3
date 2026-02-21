/**
  数据字典
**/
layui.define(['layer', 'form', 'tree', 'table'], function (exports) {
    var layer = layui.layer
        , $ = layui.$
        , setter = layui.setter
        , form = layui.form
        , tree = layui.tree
        , admin = layui.admin
        , table = layui.table;

    //常规列
    var tempcol = [
        { type: 'numbers', fixed: 'left' },
        { type: "checkbox", fixed: "left", width: 50 },
        //{ field: 'Id', title: 'ID', width: 50, align: "center" },
        { field: 'Name', title: '名称', width: 180 },
        { field: 'Remark', title: '描述' },
        { fixed: 'right', width: 150, align: 'center', toolbar: '#layuiadmin-app-cont-tagsbar' }
    ];


    //字典列表
    var tableIns;
    //    table.render({
    //    elem: '#LAY-dicTable'
    //    , url: '/DataDictionary/GetList'
    //    , page: true
    //     ,loading: true
    //    , height: setter.table.height
    //    ,limit: setter.table.limit,
    //    limits: [10, 15, 20, 25, 50, 200],
    //    id: "dataDicListTable",
    //    cols: [
    //        tempcol
    //    ]
    //});
    //获取列
    function GetColumn(keyType) {
        var columns;
        switch (keyType) {
            case "1":
            case 1:
                columns = [
                    { type: "checkbox", fixed: "left", width: 50 },
                    //{ field: 'Id', title: 'ID', width: 50, align: "center" },
                    { field: 'Name', title: '名称', width: 200 },
                    { field: 'FundDic', title: '资金性质', width: 130 },
                    { field: 'ShortName', title: '简称', width: 130 },
                    { field: 'Remark', title: '描述' },
                    { fixed: 'right', width: 150, align: 'center', toolbar: '#layuiadmin-app-cont-tagsbar' }
                ]
                break;

            default:
                columns = tempcol;
                break;
        }
        return columns;
    }





    //字典类型树
    admin.req({
        url: '/System/DataDictionary/DataDicTypes'
        , success: function (res) {
            var treedata = res.Data;

            var $data = [{
                title: '字典'
               , id: -1
               , checked: true
              , spread: true
              , children: treedata
            }]
            tree.render(
                {
                    elem: '#dic_Tree',
                    height: "full-95",
                    data: $data
                    , click: function (obj) {
                        //设置类别
                        $("input[name='dataType']").val(obj.data.id);
                        //分类管理
                        table.render({
                            elem: '#LAY-dicTable'
                            , url: '/System/DataDictionary/GetList?dType=' + obj.data.id + "&rand=" + wooutil.getRandom()
                            , page: true
                            , loading: true
                            , height: setter.table.height
                            , limit: setter.table.limit
                            , limits: [10, 15, 20, 25, 50, 200]
                            , cols: [

                                GetColumn(obj.data.id)
                            ]
                            //,skin: 'line'

                        });
                    }

                });
        }
    });
    /**
     * 执行后台删除
     * @param {any} 删除Ids
     */
    function del(tempIds) {
        layer.confirm('确定删除选中的数据？', { icon: 3, title: '提示信息' }, function (index) {
            $.ajax({
                type: "POST",

                url: "/System/DataDictionary/Delete",
                data: { Ids: tempIds },
                success: function (obj) {
                    table.reload('LAY-dicTable', {
                        where: {

                            rand: wooutil.getRandom()

                        },
                        page: {
                            curr: 1 //重新从第 1 页开始
                        }
                    });
                    layer.close(index);
                },
                error: function (data) {
                    layer.msg("删除失败！");
                }
            })


        })
    }

    //批量删除
    $(".delAll_btn").click(function () {
        var checkStatus = table.checkStatus('LAY-dicTable'),
            data = checkStatus.data,
            tempIds = [];
        if (data.length > 0) {
            for (var i in data) {
                tempIds.push(data[i].Id);
            }
            del(tempIds);
        } else {
            layer.msg("请选择需要删除的数据!");
        }
    })


    //监听工具条
    table.on('tool(LAY-dicTable)', function (obj) {
        var data = obj.data;
        if (obj.event === 'del') {
            var tempIds = [];
            tempIds.push(data.Id);
            del(tempIds);
        } else if (obj.event === 'edit') {
            layer.open({
                type: 2
                , title: '编辑字典'
                , content: '/System/DataDictionary/Build?Id=' + data.Id
                , maxmin: true
                , area: ['60%', '80%']
                , btnAlign: 'c'
                , btn: ['确定', '取消']
                , yes: function (index, layero) {
                    var iframeWindow = window['layui-layer-iframe' + index]
                        , submitID = 'Nf-System-DataDicFormSubmit'
                        , submit = layero.find('iframe').contents().find('#' + submitID);
                    //监听提交
                    iframeWindow.layui.form.on('submit(' + submitID + ')', function (data) {
                        // wooutil.SaveForm(table, "/System/DataDictionary/Save", 'LAY-dicTable', data.field, '保存成功！', index);
                        wooutil.OpenSubmitForm({
                            table: table,
                            url: "/System/DataDictionary/Save",
                            tableId: 'LAY-dicTable',
                            data: data.field,
                            index: index
                        });

                        return false;
                    });
                    submit.trigger('click');
                },
                success: function (layero, index) {
                    //给iframe元素赋值

                }
            });

        }
    });

    //注意，这里是模块输出的核心，模块名必须和use时的模块名一致
    exports('systemDataDictionary', {});
});