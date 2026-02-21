/**
*收票查看
**/
layui.define(['form', 'laydate', 'commonnf', "viewPageEdit", 'appListHist','table'], function (exports) {
    var $ = layui.$
    , setter = layui.setter
    , admin = layui.admin
    , laydate = layui.laydate
    , commonnf = layui.commonnf
    , viewPageEdit = layui.viewPageEdit
    , form = layui.form
        , appListHist = layui.appListHist
        , table = layui.table;

   


    var Id = wooutil.getUrlVar('Id');
    //合同ID
    var contId = wooutil.getUrlVar('contId');
    $("#ContId").val(contId);
    //当前弹框索引
    var $index = parent.layer.getFrameIndex(window.name);
    //标识流程列表过来
    var isFlow = wooutil.getUrlVar('isFlow');
    //发票表单赋值
    if (Id !== "" && Id !== undefined && Id !== 0) {
        var suc = function (o) {
            
            if (isFlow != undefined && isFlow!=null) {
                $("#edit").addClass("layui-hide");
                $("#del").addClass("layui-hide");
               // $("#submit").addClass("layui-hide");
                $("#confirm").addClass("layui-hide");
                $("#back").addClass("layui-hide");
            }
            else if ((o.InState == 0 || o.InState == 4) && o.WfState!==1) {//未提交Or被打回
                $("#edit").removeClass("layui-hide");
                $("#del").removeClass("layui-hide");
               // $("#submit").removeClass("layui-hide");
               
            } else if (o.InState == 1) {//已提交
                $("#confirm").removeClass("layui-hide");
                $("#back").removeClass("layui-hide");
            }


        };
        wooutil.showView({
            objId: Id,
            url: '/Finance/ContInvoice/ShowView',
            formFilter: 'NF-ContInvoiceForm',
            form: form,
            success:suc
        });
    }

    //合同基本信息绑定
    admin.req({
        url: '/Contract/ContractPayment/ShowView',
        data: { Id: contId, rand: wooutil.getRandom() },
        done: function (res) {
            form.val("NF-ContInfoForm", res.Data);
        }
    });

    var updateFields = ["MakeOutDateTime", "InCode", "Reserve1", "Reserve2"];
    $.each(updateFields, function (index, fieldId) {
        switch (fieldId) {
            case "Reserve1":
            case "Reserve2":
            case "InCode":
                {//都是文本编辑框
                    viewPageEdit.render({
                        elem: '#' + fieldId,
                        edittype: 'text',
                        objid: Id,
                        fieldname: fieldId,
                        verify: 'required',
                        ckEl :'#InTypeName',
                        url: '/Finance/ContInvoice/UpdateField'

                    });
                }
                break;
            case "MakeOutDateTime"://开票日期
                {
                    viewPageEdit.render({
                        elem: '#' + fieldId,
                        edittype: 'date',
                        objid: Id,
                        fieldname: fieldId,
                        ckEl: '#InTypeName',
                        url: '/Finance/ContInvoice/UpdateField'

                    });
                }
                break;
        }
    });
    /***************************************查看页面相关按钮-begin***************************************************************/
    layui.use('table', function () {
        var table = layui.table;
        /**
        *编辑权限
        **/
        function getUpdatePermission(funcode){
            return wooutil.requestpremission({
                url: '/NfCommon/NfPermission/UpdatePermission?rand=' + wooutil.getRandom(),
                data: {
                    FuncCode: funcode,
                    ObjId: contId
                }
            });
        }
        /**
        *编辑
        **/
        function editFunc() {
            var ress = getUpdatePermission("addOrUpdateInvoice");
            if (ress.RetValue == 0) {
                parent.layer.title("修改发票", $index);
                parent.layer.iframeSrc($index, '/Finance/ContInvoice/BuildInvoice?Id=' + Id + "&contId=" + contId + "&rand=" + wooutil.getRandom());
            } else {
                return layer.alert(ress.Msg);
            }
        }
        /**
        *修改状态
        **/
        function updateState(state) {
            admin.req({
                url: '/Finance/ContInvoice/UpdateField'
                   , data: { Id: Id, fieldName: "InState", fieldVal: state }
                   , done: function (res) {
                       wootool.handlesucc({ msg: '操作成功！', time: 400, index: $index, tableId: 'NF-Invoice-Index' });
                   }
            });
        }
        /**
        *事件
        **/
        var active = {
            edit: function () {
                editFunc();
            }, del: function () {//删除
                var tempId = [];
                tempId.push(Id);
                wootool.del({ url: '/Finance/ContInvoice/Delete', Ids: tempId, index: $index, tableId: 'NF-Invoice-Index' });
            }, submit: function () {//提交
                var ress = getUpdatePermission("addOrUpdateInvoice");
                if (ress.RetValue == 0) {//int Id, string fieldName, string fieldVal
                    updateState(1);
                } else {
                    return layer.alert("无权限");
                }

            }, confirm: function () {//确认
                var ress = getUpdatePermission("ConfirmOrBackInvoice");
                if (ress.RetValue == 0) {
                    updateState(3);
                } else {
                    return layer.alert("无权限");
                }
            }, back: function () {//打回
                var ress = getUpdatePermission("ConfirmOrBackInvoice");
                if (ress.RetValue == 0) {
                    updateState(4);
                } else {
                    return layer.alert("无权限");
                }
            }

        };
        $('.layui-btn.nf-invoice-detail').on('click', function () {
            var type = $(this).data('type');
            active[type] ? active[type].call(this) : '';
        });
    });
    /******************************************按钮相关动作-end************************************************************/
    //审批历史
    appListHist.applistInit({ Id: Id, objType: setter.sysWf.flowType.ShouPiao });



    /***********************附件信息-begin***************************************************************************************************/
    table.render({
        elem: '#NF-FinceFileTable'
        , url: '/Finance/InvoFile/GetList?finceId=' + Id + '&rand=' + wooutil.getRandom()
        , toolbar: '#toolFinceFile'
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
            , { title: '操作', width: 220, align: 'center', fixed: 'right', toolbar: '#table-FinceFilebar' }
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
                , content: '/Finance/InvoFile/Build'
                // , maxmin: false
                , area: ['800px', '80%']
                , btn: ['确定', '取消']
                , btnAlign: 'c'
                , skin: "layer-ext-myskin"
                , yes: function (index, layero) {
                    var iframeWindow = window['layui-layer-iframe' + index]
                        , submitID = 'NF-FinceFileTable-FormSubmit'
                        , submit = layero.find('iframe').contents().find('#' + submitID);
                    var Projectfiled = layero.find('iframe').contents().find('#ActId');
                    Projectfiled.val(Id);
                    //监听提交
                    iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                        wooutil.OpenSubmitForm({
                            url: '/Finance/InvoFile/Save',
                            data: obj.field,
                            table: table,
                            index: index,
                            tableId: 'NF-FinceFileTable'
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
            wooutil.deleteDatas({ tableId: 'NF-FinceFileTable', table: table, url: '/Finance/InvoFile/Delete', nopage: true });
        },
        tooldownload: function (obj) {
            wooutil.download({
                Id: obj.data.Id,
                folder: 17//附件
            });
        },
        tooldel: function (obj) {
            /// <summary>列表操作栏-删除</summary>
            ///<param name='obj'>删除数据对象</param>
            wooutil.deleteInfo({ tableId: "NF-FinceFileTable", data: obj, url: '/Finance/InvoFile/Delete', nopage: true });

        },
        tooledit: function (obj) {
            ///<summary>修改</summary>
            ///<param name='obj'>修改数据对象</param>
            layer.open({
                type: 2
                , title: '修改附件'
                , content: '/Finance/InvoFile/Build?Id=' + obj.data.Id + "&rand=" + wooutil.getRandom()
                //, maxmin: true
                , area: ['800px', '80%']
                , btn: ['确定', '取消']
                , btnAlign: 'c'
                , skin: "layer-ext-myskin"
                , yes: function (index, layero) {
                    var iframeWindow = window['layui-layer-iframe' + index]
                        , submitID = 'NF-FinceFileTable-FormSubmit'
                        , submit = layero.find('iframe').contents().find('#' + submitID);
                    var Projectfiled = layero.find('iframe').contents().find('#ActId');
                    Projectfiled.val(Id);
                    //监听提交
                    iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                        wooutil.OpenSubmitForm({
                            url: '/Finance/InvoFile/UpdateSave',
                            data: obj.field,
                            table: table,
                            index: index,
                            tableId: 'NF-FinceFileTable'
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
    table.on('toolbar(NF-FinceFileTable)', function (obj) {
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
    table.on('tool(NF-FinceFileTable)', function (obj) {
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


    exports('continvoiceDetail', {});
});