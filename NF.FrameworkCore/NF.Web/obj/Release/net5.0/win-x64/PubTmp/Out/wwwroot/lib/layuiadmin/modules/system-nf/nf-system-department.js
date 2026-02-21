/**
 @Name：组织机构
 @Author：dyk 20180724
 */
layui.define(['table', 'form'], function (exports) {
        var $ = layui.$
        , table = layui.table
        , setter = layui.setter
        , admin = layui.admin
        , form = layui.form;
        var loadindex = wooutil.loading();
    //部门管理
    table.render({
        elem: '#NF-system-dept'
        , url:  '/System/Department/GetList'
        , cols: [[
              { type: 'numbers', fixed: 'left' }
              ,{ type: 'checkbox', fixed: 'left' }
            , { field: 'Name', title: '机构名称', minWidth: 160, templet: '#nameTpl', fixed: 'left', width: 180 }
            , { field: 'No', title: '机构编号', width: 110  }
            , { field: 'PName', title: '所属机构', width: 180 }
            , { field: 'CategoryName', title: '机构类型', width: 150 }
            , { field: 'ShortName', width: 130, title: '机构简称' }
            , { field: 'IsMain', width: 90, title: '签约主体', templet: '#IsMainTpl', unresize: true }
            , { field: 'IsSubCompany', width: 80, title: '子公司', templet: '#IsSubCompany', unresize: true }
            , { field: 'DStatus', width: 80, title: '状态', templet: '#deptstateTpl', unresize: true }
            , { title: '操作', width: 150, align: 'center', fixed: 'right', toolbar: '#table-system-dept' }
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
    table.on('tool(NF-system-dept)', function (obj) {
        var _data = obj.data;
        var tempId = [];
        if (obj.event === 'del') {
            layer.confirm('确定要删除吗？', { icon: 3, title: '提示信息' }, function (index) {
                tempId.push(_data.Id);
                admin.req({
                    url: '/System/Department/Delete',
                    data: { Ids: tempId.toString() },
                    done: function (res) {
                        wooutil.reloadTable({
                            tableId: 'NF-system-dept',
                            table: table
                        });
                        layer.msg('已删除');
                    }

                })
            })
        }
        else if (obj.event === 'edit') {
           
            layer.open({
                type: 2
                , title: '修改组织机构'
                , content: '/System/Department/Build?Id=' + obj.data.Id + "&rand=" + wooutil.getRandom()
                , maxmin: true
                // ,area: ['60%', '80%']   
                ,area: [window.screen.width / 2 + 'px', window.screen.height / 2 + 'px'] //宽高
                , btn: ['确定', '取消']
                , btnAlign: 'c'
                , yes: function (index, layero) {
                    var iframeWindow = window['layui-layer-iframe' + index]
                        , submitID = 'Nf-SystemAdmin-DeptFormSubmit'
                        , submit = layero.find('iframe').contents().find('#' + submitID);

                    //监听提交
                    iframeWindow.layui.form.on('submit(' + submitID + ')', function (data) {
                        var field = data.field; //获取提交的字段
                        //提交 Ajax 成功后
                        admin.req({
                            url: "/System/Department/UpdateSave",
                            data: field,
                            type: 'POST',
                            success: function (res) {
                                layer.msg('保存成功', { icon: 1, time: 500 }, function (msgindex) {

                                    table.reload('NF-system-dept', {
                                        where: {

                                            rand: new Date().toTimeString()

                                        },
                                        page: {
                                            curr: 1 //重新从第 1 页开始
                                        }
                                    }
                                    );
                                    layer.close(index); //关闭弹层
                                });


                            }
                        });
                        return false;
                       
                    });

                    submit.trigger('click');
                },
                 success: function (layero, index) {
                     layer.full(index);
                    setTimeout(function () {
                        layui.layer.tips('点击此处返回大列表', '.layui-layer-setwin .layui-layer-close', {
                            tips: 3
                        });
                    }, 500)



                }
            });
        }
        else if (obj.event === 'detail') {
            var tr = $(obj.tr);

            layer.open({
                type: 2
                , title: '查看组织机构'
                , content: '/System/Department/Detail?Id=' + obj.data.Id+ "&rand=" + wooutil.getRandom()
                , maxmin: true
                , area: [window.screen.width / 2 + 'px', window.screen.height / 2 + 'px'] //宽高
                , success: function (layero, index) {
                    layer.full(index);
                    setTimeout(function () {
                        layui.layer.tips('点击此处返回大列表', '.layui-layer-setwin .layui-layer-close', {
                            tips: 3
                        });
                    }, 500)
                }
            });
        }
    });
    
    exports('systemDepartment', {})
});