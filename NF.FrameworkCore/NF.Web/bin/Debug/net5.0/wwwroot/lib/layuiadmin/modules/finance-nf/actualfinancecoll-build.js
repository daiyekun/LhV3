/**
 @Name：实际收款建立
 @Author：dyk 
 */
layui.define(['table', 'form'], function (exports) {
    var $ = layui.$
        , table = layui.table
        , setter = layui.setter
        , admin = layui.admin
        , form = layui.form;
    var _IsFramework = 0;
    //合同ID
    var contId = wooutil.getUrlVar('contId');
    if (contId === undefined)
        contId = 0;
    //修改时使用的ID
    var Id = parseInt(wooutil.getUrlVar('Id'));
    if (isNaN(Id)) Id = 0;

    var cyId = wooutil.getUrlVar('cyId');//币种ID
    var rate = wooutil.getUrlVar('rate');//汇率
    $("#ContId").val(contId);

    $("#CurrencyId").val(cyId);
    $("#CurrencyRate").val(rate);
    var totalamount = 0;//已经创建实际资金
    var checklist = [];
    var contAmount = 0;//合同金额
    var DcontAmount = 0;//单笔计划资金金额
    var syje = 0;

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
                , { field: 'ConfirmedAmountThod', title: '已收款金额', width: 150, fixed: 'left' }
                , { field: 'AmountMoneyThod', title: '已开票金额', width: 140 }
                , { field: 'FareWorseThod', title: '票款差额', width: 130 }
                , { field: 'AccountsThod', title: '应收款金额', width: 140 }
                , { field: 'CheckAmountThod', title: '<i class="layui-icon  layui-icon-edit"></i>本次收款金额', width: 150, edit: 'text' }
                , { field: 'Id', title: 'Id', width: 50, hide: true }
                , { title: '操作', width: 220, align: 'center', fixed: 'right', toolbar: '#table-invoicecheckbar' }
            ]]
            , page: false
            , loading: true
            , height: setter.table.height_tab
            , limit: setter.table.limit_tab
            , done: function (res, curr, count) {
                if (count > 0) {
                    //var currmonery = parseFloat(wooutil.parseThousandsSeparator($("input[name=AmountMoney]").val()));
                    for (var i = 0; i < count; i++) {
                        var hxje = person(res.data[i].CheckAmount);
                        if (!isNaN(hxje) && hxje > 0)
                            checklist.push({ ChkId: res.data[i].Id, ChkMonery: res.data[i].CheckAmount });
                    }

                }
            }
            // , limits: setter.table.limits

        });
        var invoiceCheckEvent = {

            checkInvoice: function (obj) {
                /// <summary>发票核销</summary>
                var _val = $("input[name=AmountMoney]").val();
                obj.update({
                    CheckAmountThod: _val
                });
                for (var i = 0; i < checklist.length; i++) {
                    if (checklist[i].ChkId === obj.data.Id) {
                        checklist.splice(i, 1);
                    }
                }
                checklist.push({ ChkId: obj.data.Id, ChkMonery: _val });
            }


        };

        //列表操作栏
        table.on('tool(NF-ActualBuild-InvoiceCheck)', function (obj) {
            var _data = obj.data;
            switch (obj.event) {
                case 'checkinvoice':
                    invoiceCheckEvent.checkInvoice(obj);
                    break;
                default:
                    layer.alert("暂不支持（" + obj.event + "）");
                    break;
            }
        });
        //编辑监听
        table.on('edit(NF-ActualBuild-InvoiceCheck)', function (obj) {
            var value = obj.value //得到修改后的值
                , data = obj.data; //得到所在行所有键值
            //, field = obj.field; //得到字段
            //layer.msg('[ID: ' + data.Id + '] ' + field + ' 字段更改为：' + value);
            //去掉重复修改内容
            for (var i = 0; i < checklist.length; i++) {
                if (checklist[i].ChkId === data.Id) {
                    checklist.splice(i, 1);
                }
            }
            checklist.push({ ChkId: data.Id, ChkMonery: value });
            checkplanfinanceEvent.chkplanfinanceYe();
            //layer.alert(JSON.stringify(checklist));
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
            , { field: 'ConfirmedAmountThod', title: '已收款金额', width: 130 }
            , { field: 'SurplusAmountThod', title: '可核核销', width: 130 }
            , { field: 'CheckAmountThod', title: '<i class="layui-icon  layui-icon-edit"></i>本次核销', width: 150, edit: 'text' }
            , { field: 'SyPlanAmountThod', title: '计划余额', width: 130 }
            , { title: '操作', width: 150, align: 'center', fixed: 'right', toolbar: '#table-planfinancecheckbar' }
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

                for (var i = 0; i < count; i++) {
                    var hxje = person(res.data[i].CheckAmount);
                    if (!isNaN(hxje) && hxje > 0)
                        checklist.push({ ChkId: res.data[i].Id, ChkMonery: res.data[i].CheckAmount });
                }

            }

        }


    });
    function accMul(arg1, arg2) {
        var m = 0, s1 = arg1.toString(), s2 = arg2.toString();
        try { m += s1.split(".")[1].length } catch (e) { }
        try { m += s2.split(".")[1].length } catch (e) { }
        return Number(s1.replace(".", "")) * Number(s2.replace(".", "")) / Math.pow(10, m)
    }
    //相加
    function dcmAdd(arg1, arg2) {
        var r1, r2, m;
        try { r1 = arg1.toString().split(".")[1].length; } catch (e) { r1 = 0; }
        try { r2 = arg2.toString().split(".")[1].length; } catch (e) { r2 = 0; }
        m = Math.pow(10, Math.max(r1, r2));
        return (accMul(arg1, m) + accMul(arg2, m)) / m;
    }
    //相减
    function dcmSub(arg1, arg2) {
        return dcmAdd(arg1, -arg2);
    }
    var _va1 = 0;
    var checkplanfinanceEvent = {

        toolchkplanfinance: function (obj) {
            var _va2 = 0;

            ///<summary>计划资金核销</summary>
            ///<param name='obj'>当前对象</param>
            var ssd = person($("input[name=SYHXJE]").val());
            var DcontAmount = person(obj.data.SyPlanAmountThod);//计划余额
            if (DcontAmount === 0) {
                return layer.alert("计划资金剩余金额为0不能核销该条计划资金！");
            } else {
                //var DcontAmount =obj.data.AmountMoney;
                //_va1 = (ssd - DcontAmount);//剩余核销额-可核核销
                _va1 = dcmSub(ssd, DcontAmount);
                if (_va1 < 0) {
                    _va1 = Math.abs(_va1);
                    syje = 0;
                    obj.update({
                        CheckAmountThod: ssd
                    });
                    for (var i = 0; i < checklist.length; i++) {
                        if (checklist[i].ChkId === obj.data.Id) {
                            checklist.splice(i, 1);
                        }
                    }
                    checklist.push({ ChkId: obj.data.Id, ChkMonery: ssd, syjr: syje });
                    checkplanfinanceEvent.chkplanfinanceYe();
                } else {
                    //_va2 = (person(ssd) - _va1);
                    _va2 = dcmSub(person(ssd), _va1);
                    if (_va2 < DcontAmount) {
                        _va2 = 0;
                    }
                    obj.update({
                        CheckAmountThod: _va2
                    });
                    for (var i = 0; i < checklist.length; i++) {
                        if (checklist[i].ChkId === obj.data.Id) {
                            checklist.splice(i, 1);
                        }
                    }
                    checklist.push({ ChkId: obj.data.Id, ChkMonery: _va2, syjr: _va1 });
                    checkplanfinanceEvent.chkplanfinanceYe();
                }

            }

        }
        , chkplanfinanceYe: function () {
            ///<summary>计算本次输入核销金额以后剩余金额</summary>
            var currentamount = wooutil.parseThousandsSeparator($("input[name=AmountMoney]").val());
            var syje = 0;
            var totalamount = 0;

            for (var i = 0; i < checklist.length; i++) {

                var curraM = person(checklist[i].ChkMonery);
                if (!isNaN(curraM)) {
                    //totalamount = totalamount + curraM;
                    totalamount = dcmAdd(totalamount, curraM);
                }
            }
           // syje = currentamount - totalamount;
            syje = dcmSub(currentamount, totalamount);
            var currmonery = DcontAmount;// person(wooutil.parseThousandsSeparator($("input[name=AmountMoney]").val()));
            // $("#SYHXJE").val(wooutil.numThodFormat(person(  totalamount-currmonery).toFixed(2).toString()));
            $("#SYHXJE").val(syje);
            //票款核销剩余金额
            //$("#HXFPSYHXJE").val(wooutil.numThodFormat(parseFloat(currmonery - totalamount).toFixed(2).toString()));

        }
    };
    //列表操作栏
    table.on('tool(NF-ActualBuild-PlanFinanceCheck)', function (obj) {
        var _data = obj.data;
        switch (obj.event) {
            case 'checkplanfinance':
                checkplanfinanceEvent.toolchkplanfinance(obj);
                break;

            default:
                layer.alert("暂不支持（" + obj.event + "）");
                break;
        }
    });
    //编辑监听
    table.on('edit(NF-ActualBuild-PlanFinanceCheck)', function (obj) {
        var value = obj.value //得到修改后的值
            , data = obj.data; //得到所在行所有键值
        //, field = obj.field; //得到字段
        //layer.alert('[ID: ' + data.Id + ']  字段更改为：' + value);
        //去掉重复修改内容
        var jhye = person(data.SyPlanAmountThod);//计划余额
        var srje = 0;
        srje = parseFloat(value);
        if (isNaN(srje)) {
            srje = 0;
        }
        if (srje > jhye) {//大于的时候更改为0
            $(this).val("0");
            return layer.alert("本次核销金额不能大于计划余额！");

        } else {

            for (var i = 0; i < checklist.length; i++) {
                if (checklist[i].ChkId === data.Id) {
                    checklist.splice(i, 1);
                }
            }

            checklist.push({ ChkId: data.Id, ChkMonery: value });
            checkplanfinanceEvent.chkplanfinanceYe();
        }
    });

    /***********************计划资金核销-end***************************************************************************************************/
    /***********************核销明细-begin***************************************************************************************************/
    table.render({
        elem: '#NF-ActualBuild-CheckItem'
        , url: '/Finance/ContActualFinance/GetChkDetail?contId=' + contId + '&rand=' + wooutil.getRandom()
        //, toolbar: '#toolcontsub'
        //, defaultToolbar: ['filter']
        , cols: [[
            { type: 'numbers', fixed: 'left' }
            //{ type: 'checkbox', fixed: 'left' },     
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
            for (var i = 0; i < count; i++) {
                totalamount = person(totalamount + person(res.data[i].AmountMoney)).toFixed(2);
            }


        }

    });
    /***********************核销明细-end***************************************************************************************************/
    /***********************计划资金-begin***************************************************************************************************/
    function renderPlanFinance() {
        table.render({
            elem: '#NF-ActualBuild-PlanFinance'
            , url: '/Finance/ContActualFinance/GetPlFinanceByContId?contId=' + contId + '&rand=' + wooutil.getRandom()
            //, toolbar: '#toolcontplanfinace'
            //, defaultToolbar: ['filter']
            , cols: [[
                { type: 'numbers', fixed: 'left' }
                //,{ type: 'checkbox', fixed: 'left' }
                , { field: 'Id', title: 'Id', width: 50, hide: true }
                , { field: 'Name', title: '名称', width: 160, fixed: 'left' }
                , { field: 'AmountMoneyThod', title: '金额', width: 130 }
                , { field: 'ConfirmedAmountThod', title: '已完成金额', width: 130 }
                , { field: 'BalanceThod', title: '余额', width: 130 }
                , { field: 'CompRate', title: '完成比例', width: 130 }
                //, { field: 'SubAmountThod', title: '未确认', width: 130 }
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

    function person(str) {
        str = new String(str);
        while (str.indexOf(',') > 0) {
            str = str.replace(',', '');
        }
        var num = parseFloat(str);
        if (isNaN(num))
            return 0;
        return num;
    }
    /*************************************按钮--begin*********************************************************************/
    var $index = parent.layer.getFrameIndex(window.name);
    var active = {

        save: function () {
            var submit = $("#NF-ActFinance-FormSubmit");

            form.on('submit(NF-ActFinance-FormSubmit)', function (obj) {

                var dataId = parseInt(Id);
                var _url = '/Finance/ContActualFinance/Save';
                if (!isNaN(dataId) && dataId > 0) {
                    _url = "/Finance/ContActualFinance/UpdateSave";
                }
                var TotalcurrentMonery = 0;//此次核心金额总和
                var currmonery = person(wooutil.parseThousandsSeparator($("input[name=AmountMoney]").val()));
                //核销计划资金剩余金额
                var ShenYHeXiaoJinE = person(wooutil.parseThousandsSeparator($("input[name=SYHXJE]").val()));
                //已收金额
                var totalActMoney = person(person(totalamount) + currmonery).toFixed(2)
                if (_IsFramework === 0) {
                    var CheckType = $("#CheckType").val();
                    var totalmonery = 0;
                    for (var i = 0; i < checklist.length; i++) {
                        var curchk = wooutil.parseThousandsSeparator(checklist[i].ChkMonery);
                        totalmonery = totalmonery + curchk;

                    }
                    totalmonery = totalmonery.toFixed(2);

                    if (CheckType === "0" && currmonery !== parseFloat(totalmonery)) {
                        return layer.alert("实际资金必须与核销金额总和相等！");
                    }
                    else if (CheckType === "0" && ShenYHeXiaoJinE !== 0) {
                        return layer.alert("此次核销计划资金剩余金额必须等于0！");
                    }

                    else {
                        // layer.alert(JSON.stringify(checklist));
                        wootool.submit({
                            url: _url, data: {
                                info: obj.field, chkData: checklist
                            }, msg: '保存成功', index: $index, tableId: 'NF-ActualFinanceColl-Index'
                        });
                    }
                }

                else {
                    if (_IsFramework === 1) {
                        wootool.submit({
                            url: _url, data: {
                                info: obj.field, chkData: checklist
                            }, msg: '保存成功', index: $index, tableId: 'NF-ActualFinanceColl-Index'
                        });
                    } else {
                        if (totalActMoney <= contAmount) {
                            var CheckType = $("#CheckType").val();
                            var totalmonery = 0;

                            for (var i = 0; i < checklist.length; i++) {
                                totalmonery = person(totalmonery + person(checklist[i].ChkMonery)).toFixed(2);

                            }
                            if (CheckType === "0" && currmonery !== totalmonery) {
                                return layer.alert("实际资金必须与核销金额总和相等！");
                            } else if (CheckType === "0" && ShenYHeXiaoJinE !== 0) {
                                return layer.alert("此次核销计划资金剩余金额必须等于0！");
                            }
                            else {

                                //layer.alert(JSON.stringify(checklist));
                                wootool.submit({
                                    url: _url, data: {
                                        info: obj.field, chkData: checklist
                                    }, msg: '保存成功', index: $index, tableId: 'NF-ActualFinanceColl-Index'
                                });
                            }
                        }
                        else {
                            return layer.alert("建立实际资金不能超合同金额");
                        }
                    }
                }

            });
            submit.trigger('click');
        }
        , cancel: function () {
            if (isNaN($index)) {
                parent.layui.admin.events.closeThisTabs();
            } else {
                parent.layer.close($index);
            }
            
        }
    };

    $('.layui-btn.nf-actfinance-build').on('click', function () {
        var type = $(this).data('type');
        active[type] ? active[type].call(this) : '';
    });
    /*********************************************按钮-end*************************************************************/




    /*****************************日期、导航、字典注册-begin************************************************************/
    layui.use(['laydate', 'nfcontents', 'commonnf'], function () {
        var laydate = layui.laydate
            , nfcontents = layui.nfcontents
            , commonnf = layui.commonnf
        laydate.render({ elem: '#ActualSettlementDate', value: new Date() });//结算日期
        //结算方式
        commonnf.getdatadic({ dataenum: 17, selectEl: "#SettlementMethod" });
        //目录
        nfcontents.render({ content: '#customernva' });
        //千分位字段
        var thodfields = ['AmountMoney'];
        $.each(thodfields, function (i, v) {
            $("input[name=" + v + "]").blur(function () {
                var _this = $(this);
                var temp = _this.val();
                _this.val(wooutil.numThodFormat(temp));
                if (v == "AmountMoney") {//金额
                    $("#SYHXJE").val(wooutil.numThodFormat(temp));
                    //票款核销
                    //$("#HXFPSYHXJE").val(wooutil.numThodFormat(temp));
                }
            })
        });

        //修改赋值
        if (Id !== "" && Id !== undefined && Id !== 0) {
            wooutil.showView({
                objId: Id,
                url: '/Finance/ContActualFinance/ShowView',
                formFilter: 'NF-ContActFinanceForm',
                form: form
            });
        }
        //合同基本信息绑定
        admin.req({
            url: '/Contract/ContractCollection/ShowView',
            data: { Id: contId, rand: wooutil.getRandom() },
            done: function (res) {
                if (res.Data.ContPro == "框架合同") {
                    // alert("这是框架合同");
                    // $("#planfinancecheck").removeClass("layui-hide").addClass("layui-show");
                    $("#planfinancecheck").addClass("layui-hide");
                }
                form.val("NF-ContInfoForm", res.Data);
                _IsFramework = res.Data.IsFramework;
                contAmount = parseFloat(res.Data.AmountMoney);


            }
        });



        //清除部分下拉小笔头
        wooutil.selpen();
    });
    /*****************************日期、导航、字典注册-end************************************************************/


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





    exports('actualfinancecollBuild', {});
});