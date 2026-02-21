/**
 @Name：实际付款查看
 @Author：dyk 
 */
layui.define(['table', 'form', 'appListHist'], function (exports) {
    var $ = layui.$
    , table = layui.table
    , setter = layui.setter
    , admin = layui.admin
    , form = layui.form
    , appListHist = layui.appListHist;
    //合同ID
    var contId = wooutil.getUrlVar('contId');
    if (contId === undefined)
        contId = 0;
    //修改时使用的ID
    var Id =parseInt(wooutil.getUrlVar('Id')) ;
    if (isNaN(Id))Id = 0;
    //当前弹框索引
    var $index = parent.layer.getFrameIndex(window.name);
    $("#ContId").val(contId);
    //1：表示待处理列表进入
    var $isFlow = parseInt(wooutil.getUrlVar('isFlow'));
    

    var contAmount = 0;//合同金额
    var currActMoney = 0;//当前实际资金
    /***********************票款核销-begin***************************************************************************************************/
    function renderInvoiceCheck() {
        table.render({
            elem: '#NF-ActualBuild-InvoiceCheck'
              , url: '/Finance/ContActualFinance/GetInvoiceByContId?contId=' + contId + '&actId=' + Id + '&rand=' + wooutil.getRandom()
            //, toolbar: '#toolconttxt'
            // , defaultToolbar: ['filter']
               , cols: [[
                   { type: 'numbers', fixed: 'left' }
                   //,{ type: 'checkbox', fixed: 'left' }
                   , { field: 'ConfirmedAmountThod', title: '已付款金额', width: 150, fixed: 'left' }
                   , { field: 'AmountMoneyThod', title: '已收票金额', width: 140 }
                   , { field: 'FareWorseThod', title: '票款差额', width: 130 }
                   , { field: 'AccountsThod', title: '应付款金额', width: 140 }
                   , { field: 'CheckAmountThod', title: '本次付款金额', width: 150 }
                   , { field: 'Id', title: 'Id', width: 50, hide: true }
                  // , { title: '操作', width: 220, align: 'center', fixed: 'right', toolbar: '#table-invoicecheckbar' }
               ]]
               , page: false
               , loading: true
               , height: setter.table.height_tab
               , limit: setter.table.limit_tab
            // , limits: setter.table.limits

        });
       
    }

    /***********************票款核销-end***************************************************************************************************/

    /***********************计划资金核销-begin***************************************************************************************************/
    table.render({
        elem: '#NF-ActualBuild-PlanFinanceCheck'
           , url: '/Finance/ContActualFinance/GetPlanCheckList?contId=' + contId + '&actId=' + Id + '&rand=' + wooutil.getRandom()
        //, toolbar: '#toolcontdesc'
        //, defaultToolbar: ['filter']
           , cols: [[
               { type: 'numbers', fixed: 'left' }
               , { field: 'Id', title: 'Id', width: 50, hide: true }
               , { field: 'Name', title: '名称', width: 180, fixed: 'left' }
               , { field: 'AmountMoneyThod', title: '金额', width: 140 }
               , { field: 'ConfirmedAmountThod', title: '已付款金额', width: 130 }
               , { field: 'SurplusAmountThod', title: '可核核销', width: 130 }
               , { field: 'CheckAmountThod', title: '本次核销', width: 150 }
               //, { title: '操作', width: 150, align: 'center', fixed: 'right', toolbar: '#table-planfinancecheckbar' }
           ]]
           , page: false
           , loading: true
           , height: setter.table.height_tab
           , limit: setter.table.limit_tab
          , done: function (res, curr, count) {
              //layer.alert("条数：" + count);
              if (count <= 0) {
                  renderInvoiceCheck();
                  $("#invoicecheck").removeClass("layui-hide");
                  $("li.planfinancecheck").hide();//隐藏计划资金核销目录
                  $("li.planfinance").hide();//计划资金
                  $("#CheckType").val(1);
              } else {
                  $("#planfinancecheck").removeClass("layui-hide");
                  $("#planfinance").removeClass("layui-hide");
                  $("li.invoicecheck").hide();//隐藏票核销目录
                  renderPlanFinance();//计划资金
                  $("#CheckType").val(0);
              }

          }


    });

    /***********************计划资金核销-end***************************************************************************************************/
    /***********************核销明细-begin***************************************************************************************************/
    table.render({
        elem: '#NF-ActualBuild-CheckItem'
           , url: '/Finance/ContActualFinance/GetChkDetail?contId=' + contId + '&rand=' + wooutil.getRandom()
           , cols: [[
               { type: 'numbers', fixed: 'left' }
               , { field: 'AmountMoneyThod', title: '金额', width: 140, fixed: 'left' }
               , { field: 'SettlementMethodDic', title: '结算方式', width: 120 }
               , { field: 'ActualSettlementDate', title: '结算日期', width: 120 }
               , { field: 'ConfirmUserName', title: '确认人', width: 120 }
               , { field: 'ConfirmDateTime', title: '确认时间', width: 120 }
               , { field: 'AstateDic', title: '状态', width: 120 }
               , { field: 'WfStateDic', title: '流程状态', width: 120 },
              , { field: 'Id', title: 'Id', width: 50, hide: true }
              
           ]]
           , page: true
           , loading: true
           , height: setter.table.height_tab
           , limit: setter.table.limit_tab
           , limits: setter.table.limits
         , done: function (res, curr, count) {
            
            
         }

    });
    /***********************核销明细-end***************************************************************************************************/
    /***********************计划资金-begin***************************************************************************************************/
    function renderPlanFinance() {
        table.render({
            elem: '#NF-ActualBuild-PlanFinance'
               , url: '/Finance/ContActualFinance/GetPlFinanceByContId?contId=' + contId + '&rand=' + wooutil.getRandom()
               , cols: [[
                   { type: 'numbers', fixed: 'left' }
                   , { field: 'Id', title: 'Id', width: 50, hide: true }
                   , { field: 'Name', title: '名称', width: 160, fixed: 'left' }
                   , { field: 'AmountMoneyThod', title: '金额', width: 130 }
                   , { field: 'ConfirmedAmountThod', title: '已完成金额', width: 130 }
                   , { field: 'BalanceThod', title: '余额', width: 130 }
                   , { field: 'CompRate', title: '完成比例', width: 130 }
                   , { field: 'PlanCompleteDateTime', title: '计划日期', width: 130 }
                   , { field: 'SettlModelName', title: '结算方式', width: 160 }
                   , { field: 'Id', title: 'Id', width: 50, hide: true }
               ]]
               , page: false
               , loading: true
               , height: setter.table.height_tab
               , limit: setter.table.limit_tab




        });
    }
    /***********************计划资金-end***************************************************************************************************/

    /*************************************按钮--begin*********************************************************************/
    /**
        *编辑权限
        **/
    function getUpdatePermission(funcode) {
        return wooutil.requestpremission({
            url: '/NfCommon/NfPermission/UpdatePermission?rand=' + wooutil.getRandom(),
            data: {
                FuncCode: funcode,
                ObjId: Id,
                ObjHtId: contId
            }
        });
    }
    /**
       *编辑
       **/
    function editFunc() {
        var ress = getUpdatePermission("addOrUpdateActFinancePay");
        if (ress.RetValue == 0) {
            parent.layer.title("修改实际资金", $index);
            parent.layer.iframeSrc($index, '/Finance/ContActualFinance/ActualFinancePayBuild?Id=' + Id + "&contId=" + contId + "&rand=" + wooutil.getRandom());
        } else {
            return layer.alert(ress.Msg);
        }
    }
    /**
    *修改状态
    **/
    function updateState(state) {
        admin.req({
            url: '/Finance/ContActualFinance/UpdateField'
               , data: { Id: Id, OtherId: contId, FieldName: 'Astate', FieldValue: state, UpdateMoney: currActMoney }
               , done: function (res) {
                   wootool.handlesucc({ msg: '操作成功！', time: 400, index: $index, tableId: 'NF-ActualFinancePay-Index' });
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
            wootool.del({ url: '/Finance/ContActualFinance/Delete', Ids: tempId, index: $index, tableId: 'NF-ActualFinanceColl-Index' });
        }, submit: function () {//提交
            var ress = getUpdatePermission("addOrUpdateActFinancePay");
            if (ress.RetValue == 0) {
                updateState(1);
            } else {
                return layer.alert("无权限");
            }

        }, confirm: function () {//确认
            var ress = getUpdatePermission("confirmOrBackActFinancePay");
            if (ress.RetValue == 0) {
                updateState(2);
            } else {
                return layer.alert("无权限");
            }
        }, back: function () {//打回
            var ress = getUpdatePermission("confirmOrBackActFinancePay");
            if (ress.RetValue == 0) {
                updateState(3);
            } else {
                return layer.alert("无权限");
            }
        }

    };

    $('.layui-btn.nf-actfinance-detail').on('click', function () {
        var type = $(this).data('type');
        active[type] ? active[type].call(this) : '';
    });
    /*********************************************按钮-end*************************************************************/




    /*****************************日期、导航、字典注册-begin************************************************************/
    layui.use([ 'nfcontents', 'viewPageEdit'], function () {
        
        var nfcontents = layui.nfcontents
        , commonnf = layui.commonnf
        , viewPageEdit = layui.viewPageEdit;
        
        //目录
        nfcontents.render({ content: '#customernva' });
        

        //修改赋值
        if (Id !== "" && Id !== undefined && Id !== 0) {
            var suc = function (o) {

                if ((o.Astate == 0 || o.Astate == 3)  && o.WfState!==1) {//未提交Or被打回
                    $("#edit").removeClass("layui-hide");
                    $("#del").removeClass("layui-hide");
                    //$("#submit").removeClass("layui-hide");

                }
                else if (o.Astate == 0 && o.WfState === 1) {
                    $("#edit").addClass("layui-hide");
                    $("#del").addClass("layui-hide");
                    //$("#submit").addClass("layui-hide");
                }
                else if (o.Astate == 1) {//已提交
                    $("#confirm").removeClass("layui-hide");
                    $("#back").removeClass("layui-hide");
                }
                currActMoney = o.AmountMoney;
            };
            wooutil.showView({
                objId: Id,
                url: '/Finance/ContActualFinance/ShowView',
                formFilter: 'NF-ContActFinanceForm',
                form: form,
                success: suc
            });
        }

        //合同基本信息绑定
        var $url = "/Contract/ContractPayment/ShowView";
        var $pdata = { Id: contId, rand: wooutil.getRandom() };
        if ($isFlow === 1) {//待处理
            $url = "/Finance/ContActualFinance/ShowContView";
            $pdata = { Id: Id, rand: wooutil.getRandom() }
        }
        admin.req({
            url: $url,
            data: $pdata,
            done: function (res) {
                form.val("NF-ContInfoForm", res.Data);
                contAmount = parseFloat(res.Data.AmountMoney);
               
            }
        });

        /**
        编辑字段
        **/
        var updateFields = ["ActualSettlementDate", "Reserve1", "Reserve2"];
        $.each(updateFields, function (index, fieldId) {
            switch (fieldId) {
                case "Reserve1":
                case "Reserve2":
                    {//都是文本编辑框
                        viewPageEdit.render({
                            elem: '#' + fieldId,
                            edittype: 'text',
                            objid: Id,
                            fieldname: fieldId,
                            verify: 'required',
                            ckEl: '#AmountMoneyThod',
                            url: '/Finance/ContActualFinance/UpdateField'

                        });
                    }
                    break;
                case "ActualSettlementDate"://结算日期
                    {
                        viewPageEdit.render({
                            elem: '#' + fieldId,
                            edittype: 'date',
                            objid: Id,
                            fieldname: fieldId,
                            ckEl: '#AmountMoneyThod',
                            url: '/Finance/ContActualFinance/UpdateField'

                        });
                    }
                    break;
            }
        });



        //清除部分下拉小笔头
       // wooutil.selpen();
    });
    /*****************************日期、导航、字典注册-end************************************************************/



    //审批历史
    appListHist.applistInit({ Id: Id, objType: setter.sysWf.flowType.Fukuan });


    /***********************附件信息-begin***************************************************************************************************/
    table.render({
        elem: '#NF-FinceFileTable'
        , url: '/Finance/ActFinceFile/GetList?finceId=' + Id + '&rand=' + wooutil.getRandom()
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
                , content: '/Finance/ActFinceFile/Build'
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
                            url: '/Finance/ActFinceFile/Save',
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
            wooutil.deleteDatas({ tableId: 'NF-FinceFileTable', table: table, url: '/Finance/ActFinceFile/Delete', nopage: true });
        },
        tooldownload: function (obj) {
            wooutil.download({
                Id: obj.data.Id,
                folder: 16//附件
            });
        },
        tooldel: function (obj) {
            /// <summary>列表操作栏-删除</summary>
            ///<param name='obj'>删除数据对象</param>
            wooutil.deleteInfo({ tableId: "NF-FinceFileTable", data: obj, url: '/Finance/ActFinceFile/Delete', nopage: true });

        },
        tooledit: function (obj) {
            ///<summary>修改</summary>
            ///<param name='obj'>修改数据对象</param>
            layer.open({
                type: 2
                , title: '修改附件'
                , content: '/Finance/ActFinceFile/Build?Id=' + obj.data.Id + "&rand=" + wooutil.getRandom()
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
                            url: '/Finance/ActFinceFile/UpdateSave',
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


    exports('actualfinancePayDetail', {});
});