/**
*项目新建
*/
layui.define(['table', 'form'], function (exports) {
    var $ = layui.$
    , table = layui.table
    , setter = layui.setter
    , admin = layui.admin
    , form = layui.form;
    var ProjectId = wooutil.getUrlVar('Id');
    /***********************项目备忘-begin***************************************************************************************************/
    table.render({
        elem: '#NF-projDescription'
           , url: '/Project/ProjDescription/GetList?projectId=' + ProjectId + '&rand=' + wooutil.getRandom()
           , toolbar: '#toolprojDescription'
           , defaultToolbar: ['filter']
           , cols: [[
               { type: 'numbers', fixed: 'left' }
               ,{ type: 'checkbox', fixed: 'left' }
               , { field: 'Id', title: 'Id', width: 50, hide: true }
               , { field: 'Pitem', title: '说明事项', width: 200, fixed: 'left' }
               , { field: 'ProjContent', title: '内容', width: 350 }
               , { field: 'CreateUserName', title: '提交人', width: 120 }
               , { field: 'CreateDateTime', title: '提交日期', width: 120}
               , { title: '操作', width: 150, align: 'center', fixed: 'right', toolbar: '#table-projDescriptionbar' }
           ]]
           , page: false
           , loading: true
           , height: setter.table.height_tab
           , limit: setter.table.limit_tab
        // , limits: setter.table.limits

    });
    var contactEvent = {
        add: function () {
            /// <summary>列表头部-新增按钮</summary>
            layer.open({
                type: 2
            , title: '新建项目备忘'
            , content: '/Project/ProjDescription/Build'
                // , maxmin: true
            , area: ['60%', '80%']
            , btn: ['确定', '取消']
            , btnAlign: 'c'
            , skin: "layer-ext-myskin"
            , yes: function (index, layero) {
                var iframeWindow = window['layui-layer-iframe' + index]
                    , submitID = 'NF-ProjDescription-FormSubmit'
                    , submit = layero.find('iframe').contents().find('#' + submitID);
                var Projectfiled = layero.find('iframe').contents().find('#ProjectId');
                Projectfiled.val(ProjectId);
                //监听提交
                iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                    wooutil.OpenSubmitForm({
                        url: '/Project/ProjDescription/Save',
                        data: obj.field,
                        table: table,
                        index: index,
                        tableId: 'NF-projDescription'
                    });
                    return false;
                });

                submit.trigger('click');
            },
                success: function (layero, index) {

                }
            });
        },
        batchdel: function () {
            /// <summary>列表头部-批量删除</summary>
            wooutil.deleteDatas({ tableId: 'NF-projDescription', url: '/Project/ProjDescription/Delete', nopage: true });
        },
        tooldel: function (obj) {
            /// <summary>列表操作栏-删除</summary>
            ///<param name='obj'>删除数据对象</param>
            wooutil.deleteInfo({ tableId: "NF-projDescription", data: obj, url: '/Project/ProjDescription/Delete', nopage: true });

        },
        tooledit: function (obj) {
            ///<summary>修项目备忘</summary>
            ///<param name='obj'>修改数据对象</param>
            layer.open({
                type: 2
          , title: '修项目备忘'
          , content: '/Project/ProjDescription/Build?Id=' + obj.data.Id + "&rand=" + wooutil.getRandom()
                //, maxmin: true
          , area: ['60%', '80%']
          , btn: ['确定', '取消']
          , btnAlign: 'c'
          , skin: "layer-ext-myskin"
          , yes: function (index, layero) {
              var iframeWindow = window['layui-layer-iframe' + index]
                  , submitID = 'NF-ProjDescription-FormSubmit'
                  , submit = layero.find('iframe').contents().find('#' + submitID);
              var Projectfiled = layero.find('iframe').contents().find('#ProjectId');
              Projectfiled.val(ProjectId);
              //监听提交
              iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                  wooutil.OpenSubmitForm({
                      url: '/Project/ProjDescription/UpdateSave',
                      data: obj.field,
                      table: table,
                      index: index,
                      tableId: 'NF-projDescription'
                  });
                  return false;
              });

              submit.trigger('click');
          },
                success: function (layero, index) {

                }
            });
        }
    };
    //项目备忘头部工具栏
    table.on('toolbar(NF-projDescription)', function (obj) {
        switch (obj.event) {
            case 'add':
                contactEvent.add();
                break;
            case 'batchdel':
                contactEvent.batchdel();
                break
            case 'LAYTABLE_COLS'://选择列-系统默认不管
                break;
            default:
                layer.alert("暂不支持（" + obj.event + "）");
                break;

        };
    });
    //列表操作栏
    table.on('tool(NF-projDescription)', function (obj) {
        var _data = obj.data;
        switch (obj.event) {
            case 'del':
                contactEvent.tooldel(obj);
                break;
            case 'edit':
                contactEvent.tooledit(obj);
                break;
            default:
                layer.alert("暂不支持（" + obj.event + "）");
                break;
        }
    });

    /***********************项目备忘-end***************************************************************************************************/

    /***********************项目时间表-begin***************************************************************************************************/

    table.render({
        elem: '#NF-projSchedule'
           , url: '/Project/ProjSchedule/GetList?projectId=' + ProjectId + '&rand=' + wooutil.getRandom()
           , toolbar: '#toolprojSchedule'
           , defaultToolbar: ['filter']
           , cols: [[
               { type: 'numbers', fixed: 'left' }
               ,{ type: 'checkbox', fixed: 'left' }
               , { field: 'Id', title: 'Id', width: 50, hide: true }
               , { field: 'Pitem', title: '事项', width: 400 }
               , { field: 'PlanBeginDateTime', title: '计划开始时间', width: 140 }
               , { field: 'PlanCompleteDateTime', title: '计划开始时间', width: 140 }
               , { field: 'Remark', title: '备注', width: 150 }
               , { title: '操作', width: 150, align: 'center', fixed: 'right', toolbar: '#table-projSchedulebar' }
           ]]
           , page: false
           , loading: true
           , height: setter.table.height_tab
           , limit: setter.table.limit_tab
        // , limits: setter.table.limits

    });
    var descEvent = {
        add: function () {
            /// <summary>列表头部-新增按钮</summary>
            layer.open({
                type: 2
            , title: '新建备忘录'
            , content: '/Project/ProjSchedule/Build'
                //, maxmin: true
            , area: ['60%', '80%']
            , btn: ['确定', '取消']
            , btnAlign: 'c'
            , skin: "layer-ext-myskin"
            , yes: function (index, layero) {
                var iframeWindow = window['layui-layer-iframe' + index]
                    , submitID = 'NF-ProjSchedule-FormSubmit'
                    , submit = layero.find('iframe').contents().find('#' + submitID);
                var Projectfiled = layero.find('iframe').contents().find('#ProjectId');
                Projectfiled.val(ProjectId);
                //监听提交
                iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                    wooutil.OpenSubmitForm({
                        url: '/Project/ProjSchedule/Save',
                        data: obj.field,
                        table: table,
                        index: index,
                        tableId: 'NF-projSchedule'
                    });
                    return false;
                });

                submit.trigger('click');
            },
                success: function (layero, index) {

                }
            });
        },
        batchdel: function () {
            /// <summary>列表头部-批量删除</summary>
            wooutil.deleteDatas({ tableId: 'NF-projSchedule', url: '/Project/ProjSchedule/Delete', nopage: true });
        },
        tooldel: function (obj) {
            /// <summary>列表操作栏-删除</summary>
            ///<param name='obj'>删除数据对象</param>
            wooutil.deleteInfo({ tableId: "NF-projSchedule", data: obj, url: '/Project/ProjSchedule/Delete', nopage: true });

        },
        tooledit: function (obj) {
            ///<summary>修改备忘录</summary>
            ///<param name='obj'>修改数据对象</param>
            layer.open({
                type: 2
          , title: '修改备忘录'
          , content: '/Project/ProjSchedule/Build?Id=' + obj.data.Id + "&rand=" + wooutil.getRandom()
                // , maxmin: true
          , area: ['60%', '80%']
          , btn: ['确定', '取消']
          , btnAlign: 'c'
          , skin: "layer-ext-myskin"
          , yes: function (index, layero) {
              var iframeWindow = window['layui-layer-iframe' + index]
                  , submitID = 'NF-ProjSchedule-FormSubmit'
                  , submit = layero.find('iframe').contents().find('#' + submitID);
              var Projectfiled = layero.find('iframe').contents().find('#ProjectId');
              Projectfiled.val(ProjectId);
              //监听提交
              iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                  wooutil.OpenSubmitForm({
                      url: '/Project/ProjSchedule/UpdateSave',
                      data: obj.field,
                      table: table,
                      index: index,
                      tableId: 'NF-projSchedule'
                  });
                  return false;
              });

              submit.trigger('click');
          },
                success: function (layero, index) {

                }
            });
        }
    };
    //项目备忘头部工具栏
    table.on('toolbar(NF-projSchedule)', function (obj) {
        switch (obj.event) {
            case 'add':
                descEvent.add();
                break;
            case 'batchdel':
                descEvent.batchdel();
                break;
            case 'LAYTABLE_COLS'://选择列-系统默认不管
                break;
            default:
                layer.alert("暂不支持（" + obj.event + "）");
                break;

        };
    });
    //列表操作栏
    table.on('tool(NF-projSchedule)', function (obj) {
        var _data = obj.data;
        switch (obj.event) {
            case 'del':
                descEvent.tooldel(obj);
                break;
            case 'edit':
                descEvent.tooledit(obj);
                break;
            default:
                layer.alert("暂不支持（" + obj.event + "）");
                break;
        }
    });

    /***********************项目时间表-end***************************************************************************************************/

    /***********************附件信息-begin***************************************************************************************************/
    table.render({
        elem: '#NF-projAttachment'
           , url: '/Project/ProjAttachment/GetList?ProjectId=' + ProjectId + '&rand=' + wooutil.getRandom()
           , toolbar: '#toolprojAttachment'
           , defaultToolbar: ['filter']
           , cols: [[
               { type: 'numbers', fixed: 'left' }
               ,{ type: 'checkbox', fixed: 'left' }
               , { field: 'Id', title: 'Id', width: 50, hide: true }
               , { field: 'Name', title: '附件名称', width: 180, fixed: 'left' }
               , { field: 'CategoryName', title: '附件类别', width: 140 }
               , { field: 'Remark', title: '文件说明', width: 200 }
               , { field: 'FileName', title: '文件名', width: 180 }
               , { field: 'CreateDateTime', title: '上传日期', width: 120 }
               , { title: '操作', width: 220, align: 'center', fixed: 'right', toolbar: '#table-projAttachmentbar' }
           ]]
           , page: false
           , loading: true
           , height: setter.table.height_tab
           , limit: setter.table.limit_tab
        // , limits: setter.table.limits

    });
    var attachmentEvent = {
        mydownload: function (url, method, filedir, filename) {
            $('<form action="' + url + '" method="' + (method || 'post') + '">' +  // action请求路径及推送方法
                             '<input type="text" name="filedir" value="' + filedir + '"/>' + // 文件路径
                             '<input type="text" name="filename" value="' + filename + '"/>' + // 文件名称
                         '</form>')
                 .appendTo('body').submit().remove();
        },
        add: function () {
            /// <summary>列表头部-新增按钮</summary>
            layer.open({
                type: 2
            , title: '新建附件'
            , content: '/Project/ProjAttachment/Build'
                // , maxmin: false
                , area: ['800px', '80%']
            , btn: ['确定', '取消']
            , btnAlign: 'c'
            , skin: "layer-ext-myskin"
            , yes: function (index, layero) {
                var iframeWindow = window['layui-layer-iframe' + index]
                    , submitID = 'NF-ProjAttachment-FormSubmit'
                    , submit = layero.find('iframe').contents().find('#' + submitID);
                var Projectfiled = layero.find('iframe').contents().find('#ProjectId');
                Projectfiled.val(ProjectId);
                //监听提交
                iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                    wooutil.OpenSubmitForm({
                        url: '/Project/ProjAttachment/Save',
                        data: obj.field,
                        table: table,
                        index: index,
                        tableId: 'NF-projAttachment'
                    });
                    return false;
                });

                submit.trigger('click');
            },
                success: function (layero, index) {

                }
            });
        },
        batchdel: function () {
            /// <summary>列表头部-批量删除</summary>
            wooutil.deleteDatas({ tableId: 'NF-projAttachment', table: table, url: '/Project/ProjAttachment/Delete', nopage: true });
        },
        tooldownload: function (obj) {
            wooutil.download({
                Id: obj.data.Id,
                folder: 4//项目附件
            });
        },
        tooldel: function (obj) {
            /// <summary>列表操作栏-删除</summary>
            ///<param name='obj'>删除数据对象</param>
            wooutil.deleteInfo({ tableId: "NF-projAttachment", data: obj, url: '/Project/ProjAttachment/Delete', nopage: true });

        },
        tooledit: function (obj) {
            ///<summary>修改</summary>
            ///<param name='obj'>修改数据对象</param>
            layer.open({
                type: 2
          , title: '修改附件'
          , content: '/Project/ProjAttachment/Build?Id=' + obj.data.Id + "&rand=" + wooutil.getRandom()
                //, maxmin: true
                , area: ['800px', '80%']
          , btn: ['确定', '取消']
          , btnAlign: 'c'
          , skin: "layer-ext-myskin"
          , yes: function (index, layero) {
              var iframeWindow = window['layui-layer-iframe' + index]
                  , submitID = 'NF-ProjAttachment-FormSubmit'
                  , submit = layero.find('iframe').contents().find('#' + submitID);
              var Projectfiled = layero.find('iframe').contents().find('#ProjectId');
              Projectfiled.val(ProjectId);
              //监听提交
              iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                  wooutil.OpenSubmitForm({
                      url: '/Project/ProjAttachment/UpdateSave',
                      data: obj.field,
                      table: table,
                      index: index,
                      tableId: 'NF-projAttachment'
                  });
                  return false;
              });

              submit.trigger('click');
          },
                success: function (layero, index) {

                }
            });
        }
    };
    //附件头部工具栏
    table.on('toolbar(NF-projAttachment)', function (obj) {
        switch (obj.event) {
            case 'add':
                attachmentEvent.add();
                break;
            case 'batchdel':
                attachmentEvent.batchdel();
                break

            case 'LAYTABLE_COLS'://选择列-系统默认不管
                break;
            default:
                layer.alert("暂不支持（" + obj.event + "）");
                break;

        };
    });
    //列表操作栏
    table.on('tool(NF-projAttachment)', function (obj) {
        var _data = obj.data;
        switch (obj.event) {
            case 'del':
                attachmentEvent.tooldel(obj);
                break;
            case 'edit':
                attachmentEvent.tooledit(obj);
                break;
            case 'download'://下载
                attachmentEvent.tooldownload(obj);
                break;
            default:
                layer.alert("暂不支持（" + obj.event + "）");
                break;
        }
    });

    /***********************附件信息-end***************************************************************************************************/
    exports('projectManagerBuild', {});
});