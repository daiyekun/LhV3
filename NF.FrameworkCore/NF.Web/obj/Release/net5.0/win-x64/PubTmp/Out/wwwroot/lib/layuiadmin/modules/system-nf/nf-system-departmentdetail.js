/**
*经办机构查看页面
*/
layui.define(['table', 'form'], function (exports) {
    var $ = layui.$
    , table = layui.table
    , setter = layui.setter
    , admin = layui.admin
    , form = layui.form
    var mainDeptId = wooutil.getUrlVar('Id');
    if (mainDeptId === undefined)
        mainDeptId = 0;
    /***********************合同文本信息-begin***************************************************************************************************/
    table.render({
        elem: '#NF-DeptSeal'
           , url: '/System/SealManager/GetList?mainDeptId=' + mainDeptId + '&rand=' + wooutil.getRandom()
           , toolbar: '#toolseal'
           , defaultToolbar: ['filter']
           , cols: [[
               { type: 'numbers', fixed: 'left' }
               ,{ type: 'checkbox', fixed: 'left' }
               , { field: 'Id', title: 'Id', width: 50, hide: true }
               , { field: 'SealName', title: '印章名称', width: 180, fixed: 'left' }
               , { field: 'SealCode', title: '印章编号', width: 140 }
               , { field: 'KeeperUserName', title: '保管人', width: 130 }
               , { field: 'DeptName', title: '保管部门', width: 140 }
               , { field: 'EnabledDate', title: '启用日期', width: 140 }
               , { field: 'SealState', title: '状态', width: 140, templet: '#sealstateTpl', unresize: true }
               , { field: 'Remark', title: '说明', width: 140 }
               , { field: 'CreateUserName', title: '建立人', width: 120 }
               , { field: 'CreateDateTime', title: '建立日期', width: 120 }
               , { title: '操作', width: 220, align: 'center', fixed: 'right', toolbar: '#table-sealbar' }
           ]]
           , page: false
           , loading: true
           , height: setter.table.height_tab
           , limit: setter.table.limit_tab
        // , limits: setter.table.limits

    });

    //监听状态操作
    form.on('switch(SealState)', function (obj) {
        //layer.tips(this.value + ' ' + this.Dstatus + '：' + obj.elem.checked, obj.othis);
        var state = obj.elem.checked ? 1 : 0;//状态
        admin.req({
            url: '/System/SealManager/UpdateField',
            data: { Id: this.value, fieldName: "SealState", fieldValue: state },
            done: function (res) {
                layer.msg('修改成功！');

            }

        });
    });

    var sealEvent = {
        add: function () {
            /// <summary>列表头部-新增按钮</summary>
            layer.open({
                type: 2
            , title: '新建印章'
            , content: '/System/SealManager/Build'
                //, maxmin: true
            , area: ['60%', '80%']
            , btn: ['确定', '取消']
            , btnAlign: 'c'
            , skin: "layer-ext-myskin"
            , yes: function (index, layero) {
                var iframeWindow = window['layui-layer-iframe' + index]
                    , submitID = 'NF-SealManager-FormSubmit'
                    , submit = layero.find('iframe').contents().find('#' + submitID);
                layero.find('iframe').contents().find('#MainDeptId').val(mainDeptId);//合同ID

                //监听提交
                iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                    wooutil.OpenSubmitForm({
                        url: '/System/SealManager/Save',
                        data: obj.field,
                        table: table,
                        index: index,
                        tableId: 'NF-DeptSeal'
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
            wooutil.deleteDatas({ tableId: 'NF-DeptSeal', url: '/System/SealManager/Delete', nopage: true });
        },
        tooldel: function (obj) {
            /// <summary>列表操作栏-删除</summary>
            ///<param name='obj'>删除数据对象</param>
            wooutil.deleteInfo({ tableId: "NF-DeptSeal", data: obj, url: '/System/SealManager/Delete', nopage: true });

        },
        tooledit: function (obj) {
            ///<summary>修改印章</summary>
            ///<param name='obj'>修改数据对象</param>
            layer.open({
                type: 2
          , title: '修改印章'
          , content: '/System/SealManager/Build?Id=' + obj.data.Id + "&rand=" + wooutil.getRandom()
                // , maxmin: true
          , area: ['60%', '80%']
          , btn: ['确定', '取消']
          , btnAlign: 'c'
          , skin: "layer-ext-myskin"
          , yes: function (index, layero) {
              var iframeWindow = window['layui-layer-iframe' + index]
                  , submitID = 'NF-SealManager-FormSubmit'
                  , submit = layero.find('iframe').contents().find('#' + submitID);
              layero.find('iframe').contents().find('#MainDeptId').val(mainDeptId);
              //监听提交
              iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                  wooutil.OpenSubmitForm({
                      url: '/System/SealManager/UpdateSave',
                      data: obj.field,
                      table: table,
                      index: index,
                      tableId: 'NF-DeptSeal'
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
    //合同印章头部工具栏
    table.on('toolbar(NF-DeptSeal)', function (obj) {
        switch (obj.event) {
            case 'add':
                sealEvent.add();
                break;
            case 'batchdel':
                sealEvent.batchdel();
                break;
            case 'LAYTABLE_COLS'://选择列-系统默认不管
                break;
            default:
                layer.alert("暂不支持（" + obj.event + "）");
                break;

        };
    });
    //列表操作栏
    table.on('tool(NF-DeptSeal)', function (obj) {
        var _data = obj.data;
        switch (obj.event) {
            case 'del':
                sealEvent.tooldel(obj);
                break;
            case 'edit':
                sealEvent.tooledit(obj);
                break;
            default:
                layer.alert("暂不支持（" + obj.event + "）");
                break;
        }
    });
    exports('systemDepartDetail', {});
});