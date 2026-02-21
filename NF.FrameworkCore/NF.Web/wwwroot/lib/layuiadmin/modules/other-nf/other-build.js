/**
*其他对方新建页面
*/
layui.define(['table', 'form'], function (exports) {
    var $ = layui.$
    , table = layui.table
    , setter = layui.setter
    , admin = layui.admin
    , form = layui.form;
    var companyId = wooutil.getUrlVar('Id');
    var Ctype = wooutil.getUrlVar('Ctype');//0其他对方

    /***********************其他联系人-begin***************************************************************************************************/
    table.render({
        elem: '#NF-other-build-otherContact'
           , url: '/Company/CompContact/GetList?companyId=' + companyId + '&rand=' + wooutil.getRandom()
           , toolbar: '#toolother_otherContact'
           , defaultToolbar: ['filter']
           , cols: [[
               { type: 'numbers', fixed: 'left' }
               ,{ type: 'checkbox', fixed: 'left' }
               , { field: 'Id', title: 'Id', width: 50, hide: true }
               , { field: 'Name', title: '姓名', width: 130, fixed: 'left' }
               , { field: 'Position', title: '职位', width: 150 }
               , { field: 'Tel', title: '办公电话', width: 145 }
               , { field: 'Mobile', title: '移动电话', width: 130 }
               , { field: 'Email', title: 'E-mail', width: 130 }
               , { field: 'Fax', title: '传真', width: 130 }
               , { field: 'CreateDateTime', title: '建立日期', width: 120, hide: true }
                , { field: 'CreateUserDisplyName', title: '建立人', width: 120, hide: true }
               , { field: 'Remark', title: '备注', width: 140, hide: true }

               , { title: '操作', width: 150, align: 'center', fixed: 'right', toolbar: '#table-other-otherContactbar' }
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
            , title: '新建联系人'
            , content: '/Company/CompContact/Build'
                // , maxmin: true
            , area: ['60%', '80%']
            , btn: ['确定', '取消']
            , btnAlign: 'c'
            , skin: "layer-ext-myskin"
            , yes: function (index, layero) {
                var iframeWindow = window['layui-layer-iframe' + index]
                    , submitID = 'NF-CompContact-formsubmit'
                    , submit = layero.find('iframe').contents().find('#' + submitID);
                var companyfiled = layero.find('iframe').contents().find('#build_CompanyId');
                companyfiled.val(companyId);
                //监听提交
                iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                    wooutil.OpenSubmitForm({
                        url: '/Company/CompContact/Save',
                        data: obj.field,
                        table: table,
                        index: index,
                        tableId: 'NF-other-build-otherContact'
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
            wooutil.deleteDatas({ tableId: 'NF-other-build-otherContact', url: '/Company/CompContact/Delete', nopage: true });
        },
        tooldel: function (obj) {
            /// <summary>列表操作栏-删除</summary>
            ///<param name='obj'>删除数据对象</param>
            wooutil.deleteInfo({ tableId: "NF-other-build-otherContact", data: obj, url: '/Company/CompContact/Delete', nopage: true });
          
        },
        tooledit: function (obj) {
            ///<summary>修改联系人</summary>
            ///<param name='obj'>修改数据对象</param>
            layer.open({
                type: 2
          , title: '修改联系人'
          , content: '/Company/CompContact/Build?Id=' + obj.data.Id + "&rand=" + wooutil.getRandom()
                //, maxmin: true
          , area: ['60%', '80%']
          , btn: ['确定', '取消']
          , btnAlign: 'c'
          , skin: "layer-ext-myskin"
          , yes: function (index, layero) {
              var iframeWindow = window['layui-layer-iframe' + index]
                  , submitID = 'NF-CompContact-formsubmit'
                  , submit = layero.find('iframe').contents().find('#' + submitID);
              var companyfiled = layero.find('iframe').contents().find('#build_CompanyId');
              companyfiled.val(companyId);
              //监听提交
              iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                  wooutil.OpenSubmitForm({
                      url: '/Company/CompContact/UpdateSave',
                      data: obj.field,
                      table: table,
                      index: index,
                      tableId: 'NF-other-build-otherContact'
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
    //其他联系人头部工具栏
    table.on('toolbar(NF-other-build-otherContact)', function (obj) {
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
    table.on('tool(NF-other-build-otherContact)', function (obj) {
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

    /***********************其他联系人-end***************************************************************************************************/

    /***********************附件信息-begin***************************************************************************************************/
    table.render({
        elem: '#NF-other-build-CompAttachment'
           , url: '/Company/CompAttachment/GetList?companyId=' + companyId + '&rand=' + wooutil.getRandom()
           , toolbar: '#toolother_CompAttachment'
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
               , { field: 'CreateUserDisplyName', title: '上传人', width: 120, hide: true }
               , { title: '操作', width: 220, align: 'center', fixed: 'right', toolbar: '#table-other-CompAttachmentbar' }
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
            , content: '/Company/CompAttachment/Build?Ctype=2'
                // , maxmin: false
            , area: ['800px', '80%']
            , btn: ['确定', '取消']
            , btnAlign: 'c'
            , skin: "layer-ext-myskin"
            , yes: function (index, layero) {
                var iframeWindow = window['layui-layer-iframe' + index]
                    , submitID = 'NF-CompAttachment-formsubmit'
                    , submit = layero.find('iframe').contents().find('#' + submitID);
                var companyfiled = layero.find('iframe').contents().find('#build_CompanyId');
                companyfiled.val(companyId);
                //监听提交
                iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                    wooutil.OpenSubmitForm({
                        url: '/Company/CompAttachment/Save',
                        data: obj.field,
                        table: table,
                        index: index,
                        tableId: 'NF-other-build-CompAttachment'
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
            wooutil.deleteDatas({ tableId: 'NF-other-build-CompAttachment', table: table, url: '/Company/CompAttachment/Delete', nopage: true });
        },
        tooldownload: function (obj) {
            wooutil.download({
                url: '/NfCommon/NfAttachment/Download',
                Id: obj.data.Id,
                folder: 2//标识其他对方附件


            });
        },
        tooldel: function (obj) {
            /// <summary>列表操作栏-删除</summary>
            ///<param name='obj'>删除数据对象</param>
            wooutil.deleteInfo({ tableId: "NF-other-build-CompAttachment", data: obj, url: '/Company/CompAttachment/Delete', nopage: true });

        },
        tooledit: function (obj) {
            ///<summary>修改</summary>
            ///<param name='obj'>修改数据对象</param>
            layer.open({
                type: 2
          , title: '修改附件'
          , content: '/Company/CompAttachment/Build?Id=' + obj.data.Id + "&Ctype=2&rand=" + wooutil.getRandom()
                //, maxmin: true
          , area: ['800px', '80%']
          , btn: ['确定', '取消']
          , btnAlign: 'c'
          , skin: "layer-ext-myskin"
          , yes: function (index, layero) {
              var iframeWindow = window['layui-layer-iframe' + index]
                  , submitID = 'NF-CompAttachment-formsubmit'
                  , submit = layero.find('iframe').contents().find('#' + submitID);
              var companyfiled = layero.find('iframe').contents().find('#build_CompanyId');
              companyfiled.val(companyId);
              //监听提交
              iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                  wooutil.OpenSubmitForm({
                      url: '/Company/CompAttachment/UpdateSave',
                      data: obj.field,
                      table: table,
                      index: index,
                      tableId: 'NF-other-build-CompAttachment'
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
    table.on('toolbar(NF-other-build-CompAttachment)', function (obj) {
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
    table.on('tool(NF-other-build-CompAttachment)', function (obj) {
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

    /***********************备忘录-begin***************************************************************************************************/

    table.render({
        elem: '#NF-other-build-CompDescription'
           , url: '/Company/CompDescription/GetList?companyId=' + companyId + '&rand=' + wooutil.getRandom()
           , toolbar: '#toolother_CompDescription'
           , defaultToolbar: ['filter']
           , cols: [[
               { type: 'numbers', fixed: 'left' }
               ,{ type: 'checkbox', fixed: 'left' }
               , { field: 'Id', title: 'Id', width: 50, hide: true }
               , { field: 'Item', title: '事项', width: 240 }
               , { field: 'ContentText', title: '内容', width: 460 }
               , { field: 'CreateDateTime', title: '建立日期', width: 120 }
                , { field: 'CreateUserDisplyName', title: '建立人', width: 120, hide: true }
               , { title: '操作', width: 150, align: 'center', fixed: 'right', toolbar: '#table-other-CompDescriptionbar' }
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
            , content: '/Company/CompDescription/Build'
                //, maxmin: true
            , area: ['60%', '80%']
            , btn: ['确定', '取消']
            , btnAlign: 'c'
            , skin: "layer-ext-myskin"
            , yes: function (index, layero) {
                var iframeWindow = window['layui-layer-iframe' + index]
                    , submitID = 'NF-CompDescription-formsubmit'
                    , submit = layero.find('iframe').contents().find('#' + submitID);
                var companyfiled = layero.find('iframe').contents().find('#build_CompanyId');
                companyfiled.val(companyId);
                //监听提交
                iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                    wooutil.OpenSubmitForm({
                        url: '/Company/CompDescription/Save',
                        data: obj.field,
                        table: table,
                        index: index,
                        tableId: 'NF-other-build-CompDescription'
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
            wooutil.deleteDatas({ tableId: 'NF-other-build-CompDescription', url: '/Company/CompDescription/Delete', nopage: true });
        },
        tooldel: function (obj) {
            /// <summary>列表操作栏-删除</summary>
            ///<param name='obj'>删除数据对象</param>
            wooutil.deleteInfo({ tableId: "NF-other-build-CompDescription", data: obj, url: '/Company/CompDescription/Delete', nopage: true });

        },
        tooledit: function (obj) {
            ///<summary>修改备忘录</summary>
            ///<param name='obj'>修改数据对象</param>
            layer.open({
                type: 2
          , title: '修改备忘录'
          , content: '/Company/CompDescription/Build?Id=' + obj.data.Id + "&rand=" + wooutil.getRandom()
                // , maxmin: true
          , area: ['60%', '80%']
          , btn: ['确定', '取消']
          , btnAlign: 'c'
          , skin: "layer-ext-myskin"
          , yes: function (index, layero) {
              var iframeWindow = window['layui-layer-iframe' + index]
                  , submitID = 'NF-CompDescription-formsubmit'
                  , submit = layero.find('iframe').contents().find('#' + submitID);
              var companyfiled = layero.find('iframe').contents().find('#build_CompanyId');
              companyfiled.val(companyId);
              //监听提交
              iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                  wooutil.OpenSubmitForm({
                      url: '/Company/CompDescription/UpdateSave',
                      data: obj.field,
                      table: table,
                      index: index,
                      tableId: 'NF-other-build-CompDescription'
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
    //其他联系人头部工具栏
    table.on('toolbar(NF-other-build-CompDescription)', function (obj) {
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
    table.on('tool(NF-other-build-CompDescription)', function (obj) {
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

    /***********************其他联系人-end***************************************************************************************************/

    exports('otherBuild', {});
});