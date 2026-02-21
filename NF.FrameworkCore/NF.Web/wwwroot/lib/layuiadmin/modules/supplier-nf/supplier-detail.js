/**
*供应商查看页面
*/
layui.define(['table', 'form', 'viewPageEdit', 'tableSelect', 'selectnfitem'], function (exports) {
    var $ = layui.$
    , table = layui.table
    , setter = layui.setter
    , admin = layui.admin
    , form = layui.form
    , viewPageEdit = layui.viewPageEdit
    , tableSelect = layui.tableSelect
    , selectnfitem = layui.selectnfitem
    ;
    var companyId = wooutil.getUrlVar('Id');
    var Ctype = wooutil.getUrlVar('Ctype');//0供应商
    var tabtoolvisable = true;
    /***
查看页面按钮根据状态显示隐藏
**/
    function DetailBtnShowAndHide(obj) {
        admin.req({
            url: "/NfCommon/NfPermission/DetailBtnPermission"
            , data: { perCode: "contract", Id: obj.data.Id }
            , done: function (res) {
                if (res.Data.Delete == 0) {
                    //删除按钮
                    $(".layer-nf-nfskin .layui-layer-btn .layui-layer-btn1").addClass("layui-hide");
                }
                if (res.Data.Update == 0) {
                    //修改按钮
                    $(".layer-nf-nfskin .layui-layer-btn .layui-layer-btn0").addClass("layui-hide");
                }
                if (res.Data.Change == 0) {
                    //变更按钮
                    $(".layer-nf-nfskin .layui-layer-btn .layui-layer-btn2").addClass("layui-hide");
                }
            }
        });
    }
    /**
    *设置背景颜色
    **/
    function SetBtnBgColor(obj) {
        $(".layer-nf-nfskin .layui-layer-btn .layui-layer-btn1").addClass("layui-bg-blue");
        $(".layer-nf-nfskin .layui-layer-btn .layui-layer-btn0").addClass("layui-bg-blue");
        $(".layer-nf-nfskin .layui-layer-btn .layui-layer-btn2").addClass("layui-bg-blue");
        if (obj.data.ContState == 0 && obj.data.ModificationTimes > 0) {
            $(".layer-nf-nfskin .layui-layer-btn .layui-layer-btn2").text("修改");
        }
    }
    if (companyId !== "" && companyId !== undefined) {
        admin.req({
            url: '/Company/Supplier/ShowView',
            async: false,//取消异步
            data: { Id: companyId, rand: wooutil.getRandom() },
            done: function (res) {
                if (res.Data.WfState === 1) {//审批中
                    tabtoolvisable = false;
                }
                form.val("Nf-Company-SupplierBuildForm", res.Data);
                //页面绑定完毕以后延迟执行修改次要字段
                seteditsecfiled();
            }
        });

    }

    /***********************其他联系人-begin***************************************************************************************************/
    var otherContacttbr = (tabtoolvisable ? '#toolsupplier_otherContact' : tabtoolvisable);
    var otherContactcoltbr = (tabtoolvisable ? '#table-supplier-otherContactbar' : tabtoolvisable);
    table.render({
        elem: '#NF-supplier-detail-otherContact'
           , url: '/Company/CompContact/GetList?companyId=' + companyId + '&rand=' + wooutil.getRandom()
           , toolbar: otherContacttbr
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
               , { title: '操作', width: 150, align: 'center', fixed: 'right', toolbar: otherContactcoltbr }
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
                        tableId: 'NF-supplier-detail-otherContact'
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
            wooutil.deleteDatas({ tableId: 'NF-supplier-detail-otherContact', url: '/Company/CompContact/Delete', nopage: true });
        },
        tooldel: function (obj) {
            /// <summary>列表操作栏-删除</summary>
            ///<param name='obj'>删除数据对象</param>
            wooutil.deleteInfo({ tableId: "NF-supplier-detail-otherContact", data: obj, url: '/Company/CompContact/Delete', nopage: true });

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
                      tableId: 'NF-supplier-detail-otherContact'
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
    table.on('toolbar(NF-supplier-detail-otherContact)', function (obj) {
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
    table.on('tool(NF-supplier-detail-otherContact)', function (obj) {
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
    var CompAttachmenttbr = (tabtoolvisable ? '#toolsupplier_CompAttachment' : tabtoolvisable);
    var CompAttachmentcoltbr = (tabtoolvisable ? '#table-supplier-CompAttachmentbar' : tabtoolvisable);
    table.render({
        elem: '#NF-supplier-detail-CompAttachment'
           , url: '/Company/CompAttachment/GetList?companyId=' + companyId + '&rand=' + wooutil.getRandom()
           , toolbar: CompAttachmenttbr
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
               , { title: '操作', width: 220, align: 'center', fixed: 'right', toolbar: CompAttachmentcoltbr }
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
            , content: '/Company/CompAttachment/Build?Ctype=1'
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
                        tableId: 'NF-supplier-detail-CompAttachment'
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
            wooutil.deleteDatas({ tableId: 'NF-supplier-detail-CompAttachment', url: '/Company/CompAttachment/Delete', nopage: true });
        },
        tooldownload: function (obj) {
            wooutil.download({
                url: '/NfCommon/NfAttachment/Download',
                Id: obj.data.Id,
                folder: 1//标识供应商附件


            });
        },
        tooldel: function (obj) {
            /// <summary>列表操作栏-删除</summary>
            ///<param name='obj'>删除数据对象</param>
            wooutil.deleteInfo({ tableId: "NF-supplier-detail-CompAttachment", data: obj, url: '/Company/CompAttachment/Delete', nopage: true });

        },
        tooledit: function (obj) {
            ///<summary>修改</summary>
            ///<param name='obj'>修改数据对象</param>
            layer.open({
                type: 2
          , title: '修改附件'
          , content: '/Company/CompAttachment/Build?Id=' + obj.data.Id + "&Ctype=1&rand=" + wooutil.getRandom()
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
                      tableId: 'NF-supplier-detail-CompAttachment'
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
    table.on('toolbar(NF-supplier-detail-CompAttachment)', function (obj) {
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
    table.on('tool(NF-supplier-detail-CompAttachment)', function (obj) {
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
    var CompDescriptiontbr = (tabtoolvisable ? '#toolsupplier_CompDescription' : tabtoolvisable);
    var CompDescriptioncoltbr = (tabtoolvisable ? '#table-supplier-CompDescriptionbar' : tabtoolvisable);
    table.render({
        elem: '#NF-supplier-detail-CompDescription'
           , url: '/Company/CompDescription/GetList?companyId=' + companyId + '&rand=' + wooutil.getRandom()
           , toolbar: CompDescriptiontbr
           , defaultToolbar: ['filter']
           , cols: [[
               { type: 'numbers', fixed: 'left' }
               ,{ type: 'checkbox', fixed: 'left' }
               , { field: 'Id', title: 'Id', width: 50, hide: true }
               , { field: 'Item', title: '事项', width: 240 }
               , { field: 'ContentText', title: '内容', width: 460 }
               , { field: 'CreateDateTime', title: '建立日期', width: 120 }
                , { field: 'CreateUserDisplyName', title: '建立人', width: 120, hide: true }
               , { title: '操作', width: 150, align: 'center', fixed: 'right', toolbar: CompDescriptioncoltbr }
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
                        tableId: 'NF-supplier-detail-CompDescription'
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
            wooutil.deleteDatas({ tableId: 'NF-supplier-detail-CompDescription', url: '/Company/CompDescription/Delete', nopage: true });
        },
        tooldel: function (obj) {
            /// <summary>列表操作栏-删除</summary>
            ///<param name='obj'>删除数据对象</param>
            wooutil.deleteInfo({ tableId: "NF-supplier-detail-CompDescription", data: obj, url: '/Company/CompDescription/Delete', nopage: true });

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
                      tableId: 'NF-supplier-detail-CompDescription'
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
    table.on('toolbar(NF-supplier-detail-CompDescription)', function (obj) {
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
    table.on('tool(NF-supplier-detail-CompDescription)', function (obj) {
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
    /***********************签约合同-begin*****************************************************************************************/
    table.render({
        elem: '#NF-supplier-detail-Contracts'
            , url: '/Company/Customer/GetContsByComId?companyId=' + companyId + '&rand=' + wooutil.getRandom()
        //, toolbar: '#toolcustomer_CompDescription'
            , defaultToolbar: ['filter']
            , cols: [[
                { type: 'numbers', fixed: 'left' }
                , { type: 'checkbox', fixed: 'left' }
                , { field: 'Name', title: '合同名称', width: 260, templet: '#nameTpl' }
                , { field: 'Code', title: '合同编号', width: 150 }
                , { field: 'ContAmThod', title: '合同金额', width: 140 }
                , { field: 'ContTypeName', title: '合同类别', width: 140 }
                , { field: 'ContStateDic', title: '合同状态', width: 140 }
                , { field: 'Id', title: 'Id', width: 50, hide: true }

            ]]
            , page: false
            , loading: true
            , height: setter.table.height_tab
            , limit: setter.table.limit_tab


    });
    /***********************签约合同-end********************************************************************************************/
    table.on('tool(NF-supplier-detail-Contracts)', function (obj) {
        switch (obj.event) {
            case "detail":
                {
                    var ress = wooutil.requestpremission({
                        url: '/NfCommon/NfPermission/ViewPermission?rand=' + wooutil.getRandom(),
                        data: {
                            FuncCode: 'querycollcontview',
                            ObjId: obj.data.Id
                        }
                    });
                    if (ress.RetValue == 0) {
                        openview(obj);
                    } else {
                        return layer.alert(ress.Msg);
                    }
                }
                break;

            default:
                layer.alert("未知操作（obj.event）");
                break;
        }
    });

    function openview(obj) {
        parent.layer.open({
            type: 2
            , title: '查看详情'
            , content: '/contract/ContractCollection/Detail?Id=' + obj.data.Id + "&Ftype=" + obj.data.FinanceType + "&rand=" + wooutil.getRandom()
            , maxmin: true
            , area: ['60%', '80%']
            , btnAlign: 'c'
            , skin: "layer-nf-nfskin"
            , btn: ['修改', '删除', '变更']
            , btn1: function (index, layero) {

                if (obj.data.ContState === 0 && obj.data.ModificationTimes === 0) {
                    var success = function () {
                        layer.close(index);
                    }
                    customEdit(obj, success);
                } else {
                    layer.alert("只有未执行且没有变更过的合同才允许修改！");
                    return false;

                }

            },
            btn2: function (index, layero) {
                if (obj.data.ContState === 0 && obj.data.ModificationTimes === 0) {
                    var suc = function () {
                        layer.close(index);
                    }
                    wooutil.deleteInfo({ tableId: "NF-supplier-detail-Contracts", data: obj, url: '/Contract/ContractCollection/Delete', success: suc });

                    return false;
                } else {
                    layer.alert("只有未执行且没有变更过的合同才允许删除！");
                    return false;
                }
            },
            btn3: function (index, layero) {
                if (obj.data.ContState === 1 || (obj.data.ContState == 0 && obj.data.ModificationTimes > 0)) {
                    contractChange(obj, index);
                } else {
                    layer.alert("只有执行中的合同才允许变更！");
                    return false;//阻止关闭
                }
            }
            , success: function (layero, index) {
                parent.layer.load(0, { shade: false, time: 1 * 1000 });//2秒自动关闭
                parent.layer.full(index);
                wooutil.openTip();
                SetBtnBgColor(obj);
                DetailBtnShowAndHide(obj);

            }
        });
    };
/***********************修改次要字段begin******************************************************************************************************/
    function seteditsecfiled() {
        var ress = wooutil.requestpremission({
            url: '/NfCommon/NfPermission/SecFieldUpatePremission?rand=' + wooutil.getRandom(),
            data: {
                FuncCode: 'updatesupplierminor',
                ObjId: companyId
            }

        });
        if (ress.RetValue == 0) {//有权限
            var updateFields =
           ["Address", "Trade", "PostCode", "WebSite", "FirstContact", "FirstContactTel", "FirstContactEmail", "FirstContactPosition", "FirstContactMobile", "Fax", "FirstContactQq", "Reserve1", "Reserve2", "InvoiceTitle", "TaxIdentification", "InvoiceTel", "InvoiceAddress", "BankName", "BankAccount", "PrincipalUserDisplayName"];
            $.each(updateFields, function (index, fieldId) {

                switch (fieldId) {
                    case "Address":
                    case 'Trade':
                    case 'PostCode':
                    case 'WebSite':
                    case 'FirstContact':
                    case 'FirstContactTel':
                    case 'FirstContactPosition':
                    case 'FirstContactMobile':
                    case "FirstContactEmail":
                    case 'Fax':
                    case 'FirstContactQq':
                    case 'Reserve1':
                    case 'Reserve2':
                    case 'InvoiceTitle':
                    case 'TaxIdentification':
                    case "InvoiceTel":
                    case "InvoiceAddress":
                    case "BankName":
                    case "BankAccount":
                        {//都是文本编辑框
                            viewPageEdit.render({
                                elem: '#' + fieldId,
                                edittype: 'text',
                                objid: companyId,
                                fieldname: fieldId,
                                verify: 'required',
                                ckEl: '#Name',
                                url: '/Company/Supplier/UpdateField'

                            });
                        }

                        break;
                    case "PrincipalUserDisplayName"://赋值人
                        {//都是文本编辑框
                            viewPageEdit.render({
                                elem: '#' + fieldId,
                                edittype: 'selTable',
                                objid: companyId,
                                fieldname: fieldId,
                                verify: 'required',
                                selobjId: "#PrincipalUserId",
                                ckEl: '#Name',
                                url: '/Company/Supplier/UpdateField'

                            });
                        }

                        break;

                }
            });
            //负责人编辑
            selectnfitem.selectUserItem(
          {
              tableSelect: tableSelect,
              elem: '#PrincipalUserDisplayName',
              hide_elem: '#PrincipalUserId'

          });
        } else {
            viewPageEdit.noUpShow(updateFields);
        }
    }
    /***********************修改次要字段-end******************************************************************************************************/


    /*******************************资金统计-begin******************************************************************************************************************/
    if (companyId !== "" && companyId !== undefined) {
        admin.req({
            url: '/Company/Supplier/GetFundStatistics',
            //async: false,//取消异步
            data: { compId: companyId, rand: wooutil.getRandom() },
            done: function (res) {

                form.val("Nf-Company-TongJiForm", res.Data);



            }
        });

    }

    /******************************资金统计-end**************************************************************************************/

    /***********************相关项目-begin*****************************************************************************************/
    table.render({
        elem: '#NF-detail-projctlist'
        , url: '/Company/Supplier/GetProjList?otherwh=' + companyId + '&rand=' + wooutil.getRandom()
        //, toolbar: '#toolcustomer_CompDescription'
        , defaultToolbar: ['filter']
        , cols: [[
            { type: 'numbers', fixed: 'left' }
            , { type: 'checkbox', fixed: 'left' }
            , { field: 'Name', title: '项目名称', templet: '#projctTpls',width: 260 }
            , { field: 'Code', title: '项目编号', width: 150 }
            , { field: 'ProjTypeName', title: '项目类别', width: 140 }
            , { field: 'BugetCollectAmountMoneyThod', title: '计划预算收款', width: 140 }
            , { field: 'BudgetPayAmountMoneyThod', title: '计划预算付款', width: 140 }
            , { field: 'PriUserName', title: '项目负责人', width: 140 }
            , { field: 'PstateDic', title: '项目状态', width: 140 }
            , { field: 'Id', title: 'Id', width: 50, hide: true }

        ]]
        , page: false
        , loading: true
        , height: setter.table.height_tab
        , limit: setter.table.limit_tab


    });
    /***********************相关项目-end********************************************************************************************/
    table.on('tool(NF-detail-projctlist)', function (obj) {
        switch (obj.event) {
            case "projctdetails":
                {
                    var ress = wooutil.requestpremission({
                        url: '/NfCommon/NfPermission/ViewPermission?rand=' + wooutil.getRandom(),
                        data: {
                            FuncCode: 'queryprojectview',
                            ObjId: obj.data.Id
                        }
                    });
                    if (ress.RetValue == 0) {
                        Projectopenview(obj);
                    } else {
                        return layer.alert(ress.Msg);
                    }
                }
                break;

            default:
                layer.alert("未知操作（obj.event）");
                break;
        }
    });
    /**
*打开查看页面
**/
    function Projectopenview(obj) {

        parent.layer.open({
            type: 2
            , title: '查看详情'
            , content: '/Project/ProjectManager/Detail?Id=' + obj.data.Id + "&rand=" + wooutil.getRandom()
            , maxmin: true
            , area: ['60%', '80%']
            , btnAlign: 'c'
            , skin: "layer-nf-nfskin"
            , btn: ['修改', '删除']// , area: [window.screen.width / 2 + 'px', window.screen.height / 2 + 'px'] //宽高
            , btn1: function (index, layero) {
                var success = function () {
                    layer.close(index);
                }
                customEdit(obj, success);
                // editFunc(obj, success);
            }, btn2: function (index, layero) {
                var suc = function () {
                    layer.close(index);
                }
                wooutil.deleteInfo({ tableId: "NF-project-index", data: obj, url: '/Project/ProjectManager/Delete', success: suc });

                return false;
            }
            , success: function (layero, index) {
                parent.layer.load(0, { shade: false, time: 1 * 1000 });//2秒自动关闭
                parent.layer.full(index);
                wooutil.openTip();
                SetBtnBgColor();
                DetailBtnShowAndHide(obj);
            }
        });
    };
    /***********************相关标的-begin*****************************************************************************************/
    table.render({
        elem: '#NF-detail-submitlist'
        , url: '/Company/Supplier/GetSubmitList?otherwh=' + companyId + '&rand=' + wooutil.getRandom()
        //, toolbar: '#toolcustomer_CompDescription'
        , defaultToolbar: ['filter']
        , cols: [[
            { type: 'numbers', fixed: 'left' }
            , { type: 'checkbox', fixed: 'left' }
            , { field: 'ContName', title: '合同名称1', minWidth: 160, width: 180, templet: '#nameTpls', fixed: 'left' }
            , { field: 'SubName', title: '标的名称', width: 150, fixed: 'left' }
            , { field: 'ContNo', title: '合同编号', width: 130 }
            , { field: 'ContStateDic', title: '合同状态', width: 130 }
            , { field: 'Unit', title: '单位', width: 120 }
            , { field: 'BcName', title: '单品名称', width: 140 }
            , { field: 'BcCode', title: '单品编号', width: 130 }
            , { field: 'PriceThod', title: '单价', width: 140 }
            , { field: 'Amountstr', title: '数量', width: 140 }
            , { field: 'TotalThod', title: '小计', width: 140 }
            , { field: 'SalePriceThod', title: '报价', width: 140 }
            , { field: 'Zkl', title: '折扣率', width: 140 }
            , { field: 'JfBl', title: '交付比例', width: 140 }

            , { field: 'Id', title: 'Id', width: 50, hide: true }

        ]]
        , page: true
        , loading: true
        , height: setter.table.height_tab
        , limit: setter.table.limit_tab


    });
    /***********************相关标的-end********************************************************************************************/

    table.on('tool(NF-detail-submitlist)', function (obj) {
      
        switch (obj.event) {
            case "details":
                {
                    var ress = wooutil.requestpremission({
                        url: '/NfCommon/NfPermission/ViewPermission?rand=' + wooutil.getRandom(),
                        data: {
                            FuncCode: 'querycollcontview',
                            ObjId: obj.data.Id
                        }
                    });
                    if (ress.RetValue == 0) {
                        openview(obj);
                    } else {
                        return layer.alert(ress.Msg);
                    }
                }
                break;

            default:
                layer.alert("未知操作（obj.event）");
                break;
        }
    });

    exports('supplierdetail', {});
});