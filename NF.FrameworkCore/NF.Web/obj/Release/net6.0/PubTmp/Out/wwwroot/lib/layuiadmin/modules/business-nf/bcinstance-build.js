/**
*单品管理
**/
layui.define(['table', 'form', 'treeSelect'], function (exports) {
    var $ = layui.$
    , table = layui.table
    , setter = layui.setter
    , admin = layui.admin
    , form = layui.form
    , treeSelect = layui.treeSelect;
    var $Id = wooutil.getUrlVar('Id');

    /*********************基本信息-begin******************************************************************************************************************/
    /**
       *初始化类别树
       **/
    function InitCateTree(tvl) {
        treeSelect.render(
                  {
                      elem: "#NF-LbId",
                      data: '/Business/BusinessCategory/GetCateTree',
                      method: "GET",
                      verify: true,
                      click: function (d) {
                          $("input[name=LbId]").val(d.current.id);
                      },
                      success: function (d) {
                          if (tvl != null) {
                              treeSelect.checkNode("NF-LbId", tvl);
                          }


                      }

                  });
    }
    //判断绑定界面
    if ($Id !== "" && $Id !== undefined) {
        admin.req({
            url: '/Business/BcInstance/ShowView',
            data: { Id: $Id, rand: wooutil.getRandom() },
            done: function (res) {
                form.val("NF-BcInstanceForm", res.Data);
                //设置类别
                InitCateTree(res.Data.LbId);
             
            }
        });

    } else {
        InitCateTree(null);
    }
    /*********************基本信息-end******************************************************************************************************************/
    /***********************附件信息-begin***************************************************************************************************/
    table.render({
        elem: '#NF-BcAttachment'
           , url: '/Business/BcAttachment/GetList?bId=' + $Id + '&rand=' + wooutil.getRandom()
           , toolbar: '#toolBcAttachment'
           , defaultToolbar: ['filter']
           , cols: [[
               { type: 'numbers', fixed: 'left' }
               , { type: 'checkbox', fixed: 'left' }
               , { field: 'Id', title: 'Id', width: 50, hide: true }
               , { field: 'Name', title: '附件名称', width: 180, fixed: 'left' }
               , { field: 'CategoryName', title: '附件类别', width: 140 }
               , { field: 'Remark', title: '文件说明', width: 200 }
               , { field: 'FileName', title: '文件名', width: 180 }
               , { field: 'CreateDateTime', title: '上传日期', width: 120 }
               , { field: 'CreateUserName', title: '上传人', width: 120, hide: true }
               , { title: '操作', width: 220, align: 'center', fixed: 'right', toolbar: '#tabl-bcAttachmentbar' }
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
            , content: '/Business/BcAttachment/Build'
                // , maxmin: false
            , area: ['800px', '80%']
            , btn: ['确定', '取消']
            , btnAlign: 'c'
            , skin: "layer-ext-myskin"
            , yes: function (index, layero) {
                var iframeWindow = window['layui-layer-iframe' + index]
                    , submitID = 'NF-BcAttachment-FormSubmit'
                    , submit = layero.find('iframe').contents().find('#' + submitID);
                var bcfiled = layero.find('iframe').contents().find('#BcId');
                bcfiled.val($Id);
                //监听提交
                iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                    wooutil.OpenSubmitForm({
                        url: '/Business/BcAttachment/Save',
                        data: obj.field,
                        table: table,
                        index: index,
                        tableId: 'NF-BcAttachment'
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
            wooutil.deleteDatas({ tableId: 'NF-BcAttachment', table: table, url: '/Business/BcAttachment/Delete', nopage: true });
        },
        tooldownload: function (obj) {
            wooutil.download({
                url: '/NfCommon/NfAttachment/Download',
                Id: obj.data.Id,
                folder: 8//指定附件文件夹


            });
        },
        tooldel: function (obj) {
            /// <summary>列表操作栏-删除</summary>
            ///<param name='obj'>删除数据对象</param>
            wooutil.deleteInfo({ tableId: "NF-BcAttachment", data: obj, url: '/Business/BcAttachment/Delete', nopage: true });

        },
        tooledit: function (obj) {
            ///<summary>修改</summary>
            ///<param name='obj'>修改数据对象</param>
            layer.open({
                type: 2
          , title: '修改附件'
          , content: '/Business/BcAttachment/Build?Id=' + obj.data.Id + "&rand=" + wooutil.getRandom()
                //, maxmin: true
                , area: ['800px', '80%']
          , btn: ['确定', '取消']
          , btnAlign: 'c'
          , skin: "layer-ext-myskin"
          , yes: function (index, layero) {
              var iframeWindow = window['layui-layer-iframe' + index]
                  , submitID = 'NF-BcAttachment-FormSubmit'
                  , submit = layero.find('iframe').contents().find('#' + submitID);
              var bcfiled = layero.find('iframe').contents().find('#BcId');
              bcfiled.val($Id);
              //监听提交
              iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                  wooutil.OpenSubmitForm({
                      url: '/Business/BcAttachment/UpdateSave',
                      data: obj.field,
                      table: table,
                      index: index,
                      tableId: 'NF-BcAttachment'
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
    table.on('toolbar(NF-BcAttachment)', function (obj) {
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
    table.on('tool(NF-BcAttachment)', function (obj) {
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




    exports('bcInstanceBuild', {});
});