/**
 @Name：付款合同列表
 @Author：dyk 
 */
layui.define(['table', 'contractutility', 'form', 'appflowutility', 'dynamicCondition', 'treeSelect', 'soulTable'], function (exports) {
    var $ = layui.$
        , table = layui.table
        , setter = layui.setter
        , admin = layui.admin
        , contractutility = layui.contractutility
        , form = layui.form
        , appflowutility = layui.appflowutility
        , dynamicCondition = layui.dynamicCondition
        , treeSelect = layui.treeSelect
        , soulTable = layui.soulTable;
    var logdindex = layer.load(0, { shade: false });
    var searchType = wooutil.getUrlVar('seaType');
    var _reqUrl = "/Contract/ContractPayment/GetList?rand=" + wooutil.getRandom();
    if (searchType != undefined && searchType === "1") {
        _reqUrl = _reqUrl + "&search=" + searchType;
    }
    var $htcols = [
        { type: 'numbers', fixed: 'left' }
        , { type: 'checkbox', fixed: 'left' }
        , { field: 'Name', title: '名称', width: 240, templet: '#nameTpl', fixed: 'left', filter: true, sort: true, totalRowText: '合计' }
        , { field: 'Code', title: '编号', width: 130, filter: true, sort: true }
        , {
            field: 'ContTypeName', title: '类别', width: 140, filter: true
        }
        , { field: 'ContPro', title: '合同属性', width: 130, filter: true }
        , { field: 'CompName', title: '合同对方', width: 140, templet: '#compTpl', filter: true }
        , { field: 'ContAmThod', title: '合同金额', width: 130, filter: true, sort: true }
        //, { field: 'AmountMoney', title: '合同金额', sort: true, hide: true, width: 130, filter: true }
        , { field: 'ContAmRmbThod', title: '折合本币', width: 130, filter: true }
        , { field: 'CompActAmThod', title: '已完成金额', width: 140, filter: true }
        , { field: 'CompInAmThod', title: '发票金额', width: 140, filter: true }
        , { field: 'CompRatioStr', title: '完成%', width: 140, filter: true }
        , { field: 'BalaTickThod', title: '票款差额', width: 140, filter: true }
        , { field: 'CurrName', title: '币种', width: 130, hide: true, filter: true }
        , { field: 'Rate', title: '汇率', width: 130, hide: true, filter: true }
        , { field: 'ContSum', title: '总包', width: 130, hide: true, filter: true }
        , { field: 'DeptName', title: '执行部门', width: 130, filter: true }
        , { field: 'ProjName', title: '所属项目', width: 130, hide: true, filter: true }
        , { field: 'CreateUserName', title: '建立人', width: 130, filter: true }
        , { field: 'CreateDateTime', title: '建立日期', width: 130, sort: true, filter: true }
        , { field: 'MdeptName', title: '签约主体', width: 130, hide: true, filter: true }
        , { field: 'SngDate', title: '签订日期', width: 130, hide: true, sort: true, filter: true }
        , { field: 'EfDate', title: '生效日期', width: 130, hide: true, sort: true, filter: true }
        , { field: 'PlanDate', title: '计划完成时间', width: 130, hide: true, filter: true, sort: true }
        , { field: 'ActDate', title: '实际完成时间', width: 130, hide: true, filter: true, sort: true }
        , { field: 'PrincUserName', title: '负责人', width: 130, filter: true }
        , { field: 'ContStateDic', title: '状态', width: 120, templet: '#contractstateTpl', unresize: true, filter: true }
        , { field: 'OtherCode', title: '对方编号', width: 130, filter: true }
        , { field: 'ContSName', title: '合同来源', width: 130, filter: true }
        , { field: 'Reserve1', title: '备用1', width: 130, filter: true }
        , { field: 'Reserve2', title: '备用2', width: 130, filter: true }
        , { field: 'Id', title: 'Id', width: 50, hide: true }
        , { field: 'WfStateDic', title: '流程状态', width: 130, templet: '#wfstateTpl', filter: true }
        , { field: 'WfCurrNodeName', title: '当前节点', width: 140, filter: true }
        , { field: 'Dqjdspr', title: '审批人', width: 140, filter: true }
        , { field: 'WfItemDic', title: '审批事项', width: 160, filter: true }
        , { title: '操作', width: 150, align: 'center', fixed: 'right', toolbar: '#table-contract-bar' }
    ];

    /**删除审批列**/
    appflowutility.SeCols({ htcols: $htcols });
    //列表
    table.render({
        elem: '#NF-ContractPayment-Index'
        , url: _reqUrl //'/Contract/ContractPayment/GetList?rand=' + wooutil.getRandom()
        , toolbar: '#toolcontract'
        , defaultToolbar: ['filter']
        , cellMinWidth: 80
        , totalRow: true
        , overflow: {
            type: 'tips'//内容超过设置
            //, hoverTime: 300 // 悬停时间，单位ms, 悬停 hoverTime 后才会显示，默认为 0
            , color: 'black' // 字体颜色
            , bgColor: 'white' // 背景色
        }
        //列头
        , contextmenu: {
            header: [
                {
                    name: '复制',
                    icon: 'layui-icon layui-icon-template',
                    click: function (obj) {
                        soulTable.copy(obj.text)
                        layer.msg('复制成功！')
                    }
                },
                {
                    name: '导出excel',
                    click: function () {
                        soulTable.export(this.id)
                    }
                }],
            // 表格内容右键菜单配置
            body: [
                {
                    name: '查询实际付款',
                    click: function (obj) {
                        var contrId = obj.row.Id;
                        parent.layui.index.openTabsPage("/Finance/ContActualFinance/ActualFinancePayIndex?contrId=" + contrId, "查询实际付款");
                    }
                },
                {
                    name: '查询收票',
                    click: function (obj) {
                        var contrId = obj.row.Id;
                        parent.layui.index.openTabsPage("/Finance/ContInvoice/InvoiceIndex?contrId=" + contrId, "查询收票");
                    }
                },
                {
                    name: '新建实际付款',
                    click: function (obj) {
                        if (obj.row.ContStateDic == "执行中") {
                            var contId = obj.row.Id;
                            parent.layui.index.openTabsPage("/Finance/ContActualFinance/ActualFinancePayBuild?contId=" + contId, "新建实际付款");
                        } else {
                            layer.msg('当前状态不可新建！')
                        }

                    }
                },
                {
                    name: '新建收票',
                    click: function (obj) {
                        if (obj.row.ContStateDic == "执行中") {
                            var contId = obj.row.Id;
                            parent.layui.index.openTabsPage("/Finance/ContInvoice/BuildInvoice?contId=" + contId, "新建收票");
                        } else {
                            layer.msg('当前状态不可新建！')
                        }
                    }
                }
            ],
            // 合计栏右键菜单配置
            total: [
                {
                    name: '背景黄色',
                    click: function (obj) {
                        obj.elem.css('background', '#FFB800')
                    }
                }]
        }
        , cols: [$htcols]
        , page: true
        , loading: true
        , height: setter.table.height_4
        , limit: setter.table.limit
        , limits: setter.table.limits
        , filter: {
            //列表服务器缓存
            //items: ['column', 'data', 'condition', 'editCondition', 'excel', 'clearCache'],
            cache: true
            , bottom: false
        }
        , done: function (res, curr, count) {   //返回数据执行回调函数
            soulTable.render(this)
            var AmountMoney = 0;//合同金额
            var CompInAm = 0;//发票
            var CompActAm = 0;//已完成金额
            var ContAmRmb = 0;//折合本币
            for (var i = 0; i < res.data.length; i++) {
                AmountMoney += res.data[i].AmountMoney;
                CompInAm += res.data[i].CompInAm;
                CompActAm += res.data[i].CompActAm;
                ContAmRmb += res.data[i].ContAmRmb
            }
            this.elem.next().find('.layui-table-total td[data-field="ContAmThod"] .layui-table-cell').text(AmountMoney.toLocaleString('zh', { style: 'currency', currency: 'CNY' }));//toLocaleString().style.color = "#ff0000"
            this.elem.next().find('.layui-table-total td[data-field="ContAmThod"] .layui-table-cell').css("color", "red");

            this.elem.next().find('.layui-table-total td[data-field="CompInAmThod"] .layui-table-cell').text(CompInAm.toLocaleString('zh', { style: 'currency', currency: 'CNY' }));//toLocaleString().style.color = "#ff0000"
            this.elem.next().find('.layui-table-total td[data-field="CompInAmThod"] .layui-table-cell').css("color", "red");

            this.elem.next().find('.layui-table-total td[data-field="CompActAmThod"] .layui-table-cell').text(CompActAm.toLocaleString('zh', { style: 'currency', currency: 'CNY' }));//toLocaleString().style.color = "#ff0000"
            this.elem.next().find('.layui-table-total td[data-field="CompActAmThod"] .layui-table-cell').css("color", "red");

            this.elem.next().find('.layui-table-total td[data-field="ContAmRmbThod"] .layui-table-cell').text(ContAmRmb.toLocaleString('zh', { style: 'currency', currency: 'CNY' }));//toLocaleString().style.color = "#ff0000"
            this.elem.next().find('.layui-table-total td[data-field="ContAmRmbThod"] .layui-table-cell').css("color", "red");
            layer.close(logdindex);    //返回数据关闭loading
            $("input[name=keyWord]").val($("input[name=hide_keyWord]").val());
            $("input[name=hide_keyWord]").val("");
            var cate = layui.data(setter.tableName).userName;
            contractutility.stateEvent({ tableId: 'NF-ContractPayment-Index' }, cate);//注册状态流转事件

        }

    });

    //监听表格排序
    table.on('sort(NF-ContractPayment-Index)', function (obj) {
        table.reload('NF-ContractPayment-Index', { //testTable是表格容器id
            initSort: obj //记录初始排序，如果不设的话，将无法标记表头的排序状态。 layui 2.1.1 新增参数
            , where: { //请求参数（注意：这里面的参数可任意定义，并非下面固定的格式）
                orderField: obj.field //排序字段
                , orderType: obj.type //排序方式
                , keyWord: $("input[name=keyWord]").val()//查询关键字
            }
            , page: { curr: 1 }//重新从第 1 页开始
        });


    });
    function AddLC(index, layero) {
        var iframeWindow = window['layui-layer-iframe' + index]
            , submitID = 'NF-ContractPayment-FormSubmit'
            , submit = layero.find('iframe').contents().find('#' + submitID);
        //由于模板起草时候会保存合同基本信息
        var $htId = parseInt(layero.find('iframe').contents().find('#Id').val());
        //监听提交
        iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {

            var custfiledjson = "";
            var arrfiled = [];
            var rrty = obj.form;
            var Q1 = layero.find('iframe').contents().find(".nf-customfield");
            //var Q2 = layero.find('iframe').contents().find(".nf-customfield")[0];
            var Q3 = $(layero.find('iframe').contents().find(".nf-customfield")[0]).val();
            var cufileds = $(".nf-customfield");
            for (var i = 0; i < Q1.length; i++) {
                var objfield = new Object();
                var tty = $(layero.find('iframe').contents().find(".nf-customfield")[i]).val();
                var Q2 = layero.find('iframe').contents().find(".nf-customfield")[i].id;
                var ids = Q2.split('-');
                var rtid = ids[1];
                arrfiled.push(tty + ";" + Q2);
            }


            var field = obj.field; //获取提交的字段
            var fieldval = obj.field.Name;
            var fieldcode = obj.field.Code;
            var $currId = !isNaN($htId) && $htId > 0 ? $htId : 0;
            obj.field.CustField = arrfiled.toString();
            var resname = wooutil.UniqueValObj({
                url: '/Contract/ContractPayment/CheckInputValExist',
                fieldName: 'Name',
                inputVal: fieldval,
                currId: $currId
            });
            if (resname) {
                return layer.msg('此名称已经存在！');
            }
            var rescode = wooutil.UniqueValObj({
                url: '/Contract/ContractPayment/CheckInputValExist',
                fieldName: 'Code',
                inputVal: fieldcode,
                currId: $currId
            });
            if (rescode) {
                return layer.msg('此编号已经存在！');
            }
            if (obj.field.ContTypeId == "0") {
                return layer.msg('请选择类别！');
            }
            if (obj.field.DeptId == "0") {
                return layer.msg('请选择执行部门！');
            }
            obj.field.ContState = 0;
            var $url = "/Contract/ContractPayment/Save02";
            if ($currId > 0) {
                $url = "/Contract/ContractPayment/UpdateSave01";
            }
            admin.req({
                url: $url,
                data: obj.field,
                type: 'POST',
                success: function (res) {
                    var z = "0";
                    //  alert("标记01" + res.RetValue)
                    layer.close(index);
                    layer.close(logdindex);
                    if (res.RetValue === 0) {
                        if ('NF-ContractPayment-Index' === "" && 'NF-ContractPayment-Index' === undefined) {

                        } else {
                            //状态流转
                            //====================================================
                            var ress = wooutil.requestpremission({
                                url: '/NfCommon/NfPermission/StateUpdate?rand=' + wooutil.getRandom(),
                                data: {
                                    FuncCode: 'updatepaycontstate',
                                    ObjId: res.Data[0].Id
                                }
                            });
                            if (ress.RetValue == 0) {
                                if (res.Data[0].ModificationTimes > 0) {
                                    z = "2";
                                } else {
                                    z = "1";
                                }
                                if (setter.sysinfo.seversion == "SE") {
                                    var resf = appflowutility.showFlow01({
                                        tableId: 'NF-ContractPayment-Index'
                                        , evtobj: z
                                        , objType: 3//合同
                                        , deptId: res.Data[0].DeptId //合同所属部门ID经办机构
                                        , objCateId: res.Data[0].ContTypeId//类别
                                        , objName: res.Data[0].Name //合同名称
                                        , objCode: res.Data[0].Code // 合同编号
                                        , objamt: res.Data[0].AmountMoney//合同金额
                                        , objIds: res.Data[0].Id
                                        , finceType: 0//收款
                                    });
                                    if (resf === -1) {
                                        layer.confirm('没有匹配上流程是否直接修改状态？', { icon: 3, title: '提示信息' }, function (cfindex) {
                                            contractutility.updateSate01({
                                                tableId: 'NF-ContractCollection-Index'
                                                , url: '/Contract/ContractPayment/UpdateMoreField'
                                                , sZT: z
                                                , Sid: res.Data[0].Id
                                            });
                                            layer.close(cfindex);

                                        });
                                    }
                                } else {
                                    contractutility.updateSate01({
                                        tableId: 'NF-ContractCollection-Index'
                                        , url: '/Contract/ContractPayment/UpdateMoreField'
                                        , sZT: z
                                        , Sid: res.Data[0].Id
                                    });
                                }
                            } else {
                                return layer.alert(ress.Msg);
                            }
                        }

                    } else {
                        layer.msg(res.Msg,
                            {
                                offset: '15px'
                                , icon: 5
                            });
                    }
                }
            });

            return false;
        });

        submit.trigger('click');

    }
    function SetBtnBgColordde() {

        $(" .layui-layer-btn2").addClass("layui-bg-red");
    }
    /***********************************************监听头部工具栏---begin*****************************************************/
    var openAdd = function () {
        layer.open({
            type: 2
            , title: '新增合同'
            , content: '/Contract/ContractPayment/Build'
            , maxmin: true
            , area: ['60%', '80%']
            , btn: ['确定', '取消', '提交流程']
            , btnAlign: 'c'
            , skin: "layer-ext-myskin"
            , yes: function (index, layero) {
                var iframeWindow = window['layui-layer-iframe' + index]
                    , submitID = 'NF-ContractPayment-FormSubmit'
                    , submit = layero.find('iframe').contents().find('#' + submitID);
                //由于模板起草时候会保存合同基本信息
                var $htId = parseInt(layero.find('iframe').contents().find('#Id').val());
                //监听提交
                iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {

                    var field = obj.field; //获取提交的字段
                    var fieldval = obj.field.Name;
                    var fieldcode = obj.field.Code;
                    var $currId = !isNaN($htId) && $htId > 0 ? $htId : 0;
                    var resname = wooutil.UniqueValObj({
                        url: '/Contract/ContractPayment/CheckInputValExist',
                        fieldName: 'Name',
                        inputVal: fieldval,
                        currId: $currId
                    });
                    if (resname) {
                        return layer.msg('此名称已经存在！');
                    }
                    var rescode = wooutil.UniqueValObj({
                        url: '/Contract/ContractPayment/CheckInputValExist',
                        fieldName: 'Code',
                        inputVal: fieldcode,
                        currId: $currId
                    });
                    if (rescode) {
                        return layer.msg('此编号已经存在！');
                    }
                    if (obj.field.ContTypeId == "0") {
                        return layer.msg('请选择类别！');
                    }
                    if (obj.field.DeptId == "0") {
                        return layer.msg('请选择执行部门！');
                    }
                    obj.field.ContState = 0;//
                    var $url = "/Contract/ContractPayment/Save";
                    if ($currId > 0) {
                        $url = "/Contract/ContractPayment/UpdateSave";
                      
                    } 

                    wooutil.OpenSubmitForm({
                        url: $url,
                        data: obj.field,
                        table: table,
                        index: index,
                        tableId: 'NF-ContractPayment-Index'
                    });
                    return false;
                });

                submit.trigger('click');
            }
            , btn3: function (index, layero) {
                AddLC(index, layero)
            },
            success: function (layero, index) {
                layer.full(index);
                wooutil.openTip();
                SetBtnBgColordde();
            }
        });
    };




    var active = {
        add: function () {//新增
            var ress = wooutil.requestpremission({
                url: '/NfCommon/NfPermission/AddPermission?rand=' + wooutil.getRandom(),
                data: {
                    FuncCode: 'addpaycont'
                }

            });
            if (ress.RetValue == 0) {
                openAdd();
            } else {
                return layer.alert(ress.Msg);
            }
        },
        batchdel: function () {//删除
            wooutil.deleteDatas({ tableId: 'NF-ContractPayment-Index', table: table, url: '/Contract/ContractPayment/Delete' });
        },
        search: function () {//查询
            $("input[name=hide_keyWord]").val($("input[name=keyWord]").val());
            table.reload('NF-ContractPayment-Index', {
                page: { curr: 1 }
                , where: {
                    keyWord: $("input[name=keyWord]").val()

                }
            });

        }
        ,
        submitState: function (evtobj) {//提交状态
            var checkStatus = table.checkStatus("NF-ContractPayment-Index")
                , checkData = checkStatus.data; //得到选中的数据
            var ress = wooutil.requestpremission({
                url: '/NfCommon/NfPermission/StateUpdate?rand=' + wooutil.getRandom(),
                data: {
                    FuncCode: 'updatepaycontstate',
                    ObjId: checkStatus.data[0].Id
                }
            });
            if (ress.RetValue == 0) {
                if (setter.sysinfo.seversion == "SE") {

                    var resf = appflowutility.showFlow({
                        tableId: 'NF-ContractPayment-Index'
                        , evtobj: evtobj
                        , objType: 3//合同
                        , deptId: checkData[0].DeptId
                        , objCateId: checkData[0].ContTypeId//类别
                        , objName: checkData[0].Name
                        , objCode: checkData[0].Code
                        , objamt: checkData[0].AmountMoney//合同金额
                        , finceType: 1//付款
                    });

                    if (resf === -1) {
                        layer.confirm('没有匹配上流程是否直接修改状态？', { icon: 3, title: '提示信息' }, function (cfindex) {
                            contractutility.updateSate({
                                tableId: 'NF-ContractPayment-Index'
                                , url: '/Contract/ContractPayment/UpdateMoreField'
                                , evtobj: evtobj
                            });
                            layer.close(cfindex);

                        });
                    }
                } else {
                    contractutility.updateSate({
                        tableId: 'NF-ContractPayment-Index'
                        , url: '/Contract/ContractPayment/UpdateMoreField'
                        , evtobj: evtobj
                    });
                }
            } else {
                return layer.alert(ress.Msg);
            }

        }
        ,
        advQuery: function () {
            var complexDc = dynamicCondition.create({
                elem: "#dynamicCondition"
                //, tableId:'NF-customer-index'
                , type: "complex"
                , requestDataType: 'json'
                //当有多个动态条件查询实例时，定义instanceName属性可以通过dynamicCondition.getInstance(instanceName)获取对应的实例
                , instanceName: "complexInstance"
                , popupBtnsWidth: 350
                , popupShowQueryBtn: true
                , unpopupBtnswidth: 410
                , unpopupShowAddBtn: true
                //, nestedQuery: true
                , queryCallBack: function (requestData) {

                    $("input[name=advwhere]").val(JSON.stringify(JSON.parse(requestData.jsonStr)).replace(/^\"|\"$/g, ''));
                    table.reload("NF-ContractCollection-Index", {
                        page: {
                            curr: 1 //重新加载当前页
                        }
                        , where: requestData
                    });


                }


            });

            dynamicCondition.getInstance("complexInstance").open();


        }
    };

    table.on('toolbar(NF-ContractPayment-Index)', function (obj) {
        switch (obj.event) {
            case 'add':
                active.add();
                break;
            case 'batchdel':
                active.batchdel();
                break;
            case 'search':
                active.search();
                break;
            case 'stateChange'://状态流转
                active.submitState(this);
                break;
            case "exportexcel":
                wooutil.exportexcel(obj, {
                    url: "/Contract/ContractPayment/ExportExcel",
                    keyword: $("input[name=keyWord]").val()
                });
                break;
            case "advQuery"://高级查询
                active.advQuery();
                break;
            case "skclear"://高级查询
                soulTable.clearCache("NF-ContractCollection-Index")
                layer.msg('已还原！', { icon: 1, time: 1000 })
                break;

        };
    });
    /***********************************************监听头部工具栏---end**************************************************************/

    /**********************************************************监听工具条-begin*********************************************************/
    function editFunc(obj, _success) {
        layer.open({
            type: 2
            , title: '修改信息'
            , content: '/Contract/ContractPayment/Build?Id=' + obj.data.Id + "&rand=" + wooutil.getRandom()
            , maxmin: true
            , area: ['60%', '80%']
            // , area: [window.screen.width / 2 + 'px', window.screen.height / 2 + 'px'] //宽高
            , btn: ['确定', '取消', '提交流程']
            , btnAlign: 'c'
            , btn3: function (index, layero) {
                // active.submitState(this);
                var Dqid = "";
                var HtBmid = "";
                var HtType = "";
                var Htname = "";
                var Htcode = "";
                var Htje = "";
                var iframeWindow = window['layui-layer-iframe' + index]
                    , submitID = 'NF-ContractPayment-FormSubmit'
                    , submit = layero.find('iframe').contents().find('#' + submitID);
                //监听提交
                iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                    var custfiledjson = "";
                    var arrfiled = [];
                    var rrty = obj.form;
                    var Q1 = layero.find('iframe').contents().find(".nf-customfield");
                    //var Q2 = layero.find('iframe').contents().find(".nf-customfield")[0];
                    var Q3 = $(layero.find('iframe').contents().find(".nf-customfield")[0]).val();
                    var cufileds = $(".nf-customfield");
                    for (var i = 0; i < Q1.length; i++) {
                        var objfield = new Object();
                        var tty = $(layero.find('iframe').contents().find(".nf-customfield")[i]).val();
                        var Q2 = layero.find('iframe').contents().find(".nf-customfield")[i].id;
                        arrfiled.push(tty + ";" + Q2);
                    }
                    var field = obj.field; //获取提交的字段
                    var fieldval = field.Name;
                    var fieldcode = field.Code;
                    obj.field.CustField = arrfiled.toString();
                    //当前合同id
                    Dqid = field.Id;
                    HtBmid = field.DeptId; //合同所属部门ID经办机构
                    HtType = field.ContTypeId; //类别
                    Htname = field.Name; //合同名称
                    Htcode = field.Code; // 合同编号
                    Htje = field.AmountMoney; //合同金额
                    var resname = wooutil.UniqueValObj({
                        url: '/Contract/ContractPayment/CheckInputValExist',
                        fieldName: 'Name',
                        inputVal: fieldval,
                        currId: field.Id
                    });
                    if (resname) {
                        return layer.msg('此名称已经存在！');
                    }
                    var rescode = wooutil.UniqueValObj({
                        url: '/Contract/ContractPayment/CheckInputValExist',
                        fieldName: 'Code',
                        inputVal: fieldcode,
                        currId: field.Id
                    });
                    if (rescode) {
                        return layer.msg('此编号已经存在！');
                    }
                    if (obj.field.ContTypeId == "0") {
                        return layer.msg('请选择类别！');
                    }
                    if (obj.field.DeptId == "0") {
                        return layer.msg('请选择执行部门！');
                    }
                    admin.req({
                        url: '/Contract/ContractPayment/UpdateSave01',
                        data: field,
                        type: 'POST',
                        success: function (res) {
                            var z = "0";
                            //  alert("标记01" + res.RetValue)
                            layer.close(index);
                            layer.close(logdindex);
                            if (res.RetValue === 0) {
                                if ('NF-ContractPayment-Index' === "" && 'NF-ContractPayment-Index' === undefined) {
                                } else {
                                    //状态流转
                                    //====================================================

                                    var ress = wooutil.requestpremission({
                                        url: '/NfCommon/NfPermission/StateUpdate?rand=' + wooutil.getRandom(),
                                        data: {
                                            FuncCode: 'updatepaycontstate',
                                            ObjId: res.Data[0].Id
                                        }
                                    });
                                    if (ress.RetValue == 0) {
                                        if (res.Data[0].ModificationTimes > 0) {
                                            z = "2";
                                        } else {
                                            z = "1";
                                        }
                                        if (setter.sysinfo.seversion == "SE") {
                                            var resf = appflowutility.showFlow01({
                                                tableId: 'NF-ContractPayment-Index'
                                                , evtobj: z
                                                , objType: 3//合同
                                                , deptId: res.Data[0].DeptId //合同所属部门ID经办机构
                                                , objCateId: res.Data[0].ContTypeId//类别
                                                , objName: res.Data[0].Name //合同名称
                                                , objCode: res.Data[0].Code // 合同编号
                                                , objamt: res.Data[0].AmountMoney//合同金额
                                                , objIds: res.Data[0].Id
                                                , finceType: 1//收款
                                            });
                                            if (resf === -1) {
                                                //debugger;
                                                //alert("返回状态" + ress.RetValue);
                                                layer.confirm('没有匹配上流程是否直接修改状态？', { icon: 3, title: '提示信息' }, function (cfindex) {
                                                    contractutility.updateSate01({
                                                        tableId: 'NF-ContractPayment-Index'
                                                        , url: '/Contract/ContractPayment/UpdateMoreField'
                                                        // , evtobj: evtobj
                                                        , sZT: z
                                                        , Sid: res.Data[0].Id
                                                    });
                                                    layer.close(cfindex);
                                                });
                                            }
                                        } else {
                                            contractutility.updateSate01({
                                                tableId: 'NF-ContractPayment-Index'
                                                , url: '/Contract/ContractPayment/UpdateMoreField'
                                                , sZT: z
                                                , Sid: res.Data[0].Id
                                            });
                                         
                                            layer.close(cfindex);
                                        }
                                    } else {
                                        return layer.alert(ress.Msg);
                                    }
                                }
                            } else {
                                layer.msg(res.Msg,
                                    {
                                        offset: '15px'
                                        , icon: 5
                                    });
                            }
                        }
                    });
                    return false;
                });
                submit.trigger('click');
            }
            , yes: function (index, layero) {
                var iframeWindow = window['layui-layer-iframe' + index]
                    , submitID = 'NF-ContractPayment-FormSubmit'
                    , submit = layero.find('iframe').contents().find('#' + submitID);
                //监听提交
                iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                    var field = obj.field; //获取提交的字段
                    var fieldval = field.Name;
                    var fieldcode = field.Code;

                    var resname = wooutil.UniqueValObj({
                        url: '/Contract/ContractPayment/CheckInputValExist',
                        fieldName: 'Name',
                        inputVal: fieldval,
                        currId: field.Id
                    });
                    if (resname) {
                        return layer.msg('此名称已经存在！');
                    }
                    var rescode = wooutil.UniqueValObj({
                        url: '/Contract/ContractPayment/CheckInputValExist',
                        fieldName: 'Code',
                        inputVal: fieldcode,
                        currId: field.Id
                    });
                    if (rescode) {
                        return layer.msg('此编号已经存在！');
                    }
                    if (obj.field.ContTypeId == "0") {
                        return layer.msg('请选择类别！');
                    }
                    if (obj.field.DeptId == "0") {
                        return layer.msg('请选择执行部门！');
                    }
                    wooutil.OpenSubmitForm({
                        url: '/Contract/ContractPayment/UpdateSave',
                        table: table,
                        data: field,
                        tableId: 'NF-ContractPayment-Index',
                        msg: '保存成功',
                        index: index

                    });
                    return false;

                });

                submit.trigger('click');
            },
            success: function (layero, index) {
                layer.full(index);
                wooutil.openTip();
                SetBtnBgColordde();
                if (typeof _success === 'function') {
                    setTimeout(function () {
                        _success();
                    }, 500)


                }
            }
        });
    }
    /**合同变更**/
    function changeFunc(obj, _success) {
        layer.open({
            type: 2
            , title: '合同变更'
            , content: '/Contract/ContractPayment/Change?Id=' + obj.data.Id + "&rand=" + wooutil.getRandom()
            , maxmin: true
            , area: ['60%', '80%']
            // , area: [window.screen.width / 2 + 'px', window.screen.height / 2 + 'px'] //宽高
            , btn: ['确定', '取消']
            , btnAlign: 'c'
            , yes: function (index, layero) {
                var iframeWindow = window['layui-layer-iframe' + index]
                    , submitID = 'NF-ContractPayment-FormSubmit'
                    , submit = layero.find('iframe').contents().find('#' + submitID);
                //监听提交
                iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                    var field = obj.field; //获取提交的字段

                    wooutil.OpenSubmitForm({
                        url: '/Contract/ContractPayment/ChangeSave',
                        table: table,
                        data: field,
                        tableId: 'NF-ContractPayment-Index',
                        msg: '保存成功',
                        index: index

                    });
                    return false;

                });

                submit.trigger('click');
            },
            success: function (layero, index) {
                layer.full(index);
                wooutil.openTip();
                if (typeof _success === 'function') {
                    setTimeout(function () {
                        _success();
                    }, 500)


                }


            }
        });
    }
    /**编辑带权限*/
    function customEdit(obj, success) {
        var ress = wooutil.requestpremission({
            url: '/NfCommon/NfPermission/UpdatePermission?rand=' + wooutil.getRandom(),
            data: {
                FuncCode: 'updatepaycont',
                ObjId: obj.data.Id
            }
        });
        if (ress.RetValue == 0) {
            editFunc(obj, success);
        } else {
            return layer.alert(ress.Msg);
        }
    }
    /**chan合同变更ge*/
    function contractChange(obj, index) {
        var success = function () {
            if (index != null) {
                layer.close(index);
            }
        }
        changeFunc(obj, success);
    }
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

    /**
    *打开查看页面
    **/
    function openview(obj) {
        layer.open({
            type: 2
            , title: '查看详情'
            , content: '/contract/ContractPayment/Detail?Id=' + obj.data.Id + "&Ftype=" + obj.data.FinanceType + "&rand=" + wooutil.getRandom()
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

            }, btn2: function (index, layero) {
                if (obj.data.ContState === 0 && obj.data.ModificationTimes === 0) {
                    var suc = function () {
                        layer.close(index);
                    }
                    wooutil.deleteInfo({ tableId: "NF-ContractPayment-Index", data: obj, url: '/Contract/ContractPayment/Delete', success: suc });

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
                layer.load(0, { shade: false, time: 2 * 1100 });//2秒自动关闭
                layer.full(index);
                wooutil.openTip();
                SetBtnBgColor(obj);
                DetailBtnShowAndHide(obj);

            }
        });
    };
    /**
    *打开查看供应商页面
    **/
    function opencompview(obj) {
        layer.open({
            type: 2
            , title: '查看详情'
            , content: '/Company/Supplier/Detail?Id=' + obj.data.CompId + "&rand=" + wooutil.getRandom()
            , maxmin: true
            , area: ['60%', '80%']
            , btnAlign: 'c'
            , skin: "layer-nf-nfskin"
            , btn: ['关闭']
            , success: function (layero, index) {
                layer.load(0, { shade: false, time: 1 * 1000 });
                layer.full(index);
                wooutil.openTip();

            }
        });
    };

    table.on('tool(NF-ContractPayment-Index)', function (obj) {
        switch (obj.event) {
            case "del"://删除
                {
                    var cate = layui.data(setter.tableName).userName;
                    if ((obj.data.ContState === 0 && obj.data.ModificationTimes === 0) || cate == "SuperAdministrator") {
                        wooutil.deleteInfo({ tableId: "NF-ContractPayment-Index", data: obj, url: '/Contract/ContractPayment/Delete' });
                    } else {
                        layer.alert("只有未执行且没有变更过的合同才允许删除！");
                        return false;
                    }
                }
                break;
            case "edit":
                {
                    var cate = layui.data(setter.tableName).userName;
                    if (obj.data.ContState === 0 && obj.data.ModificationTimes === 0) {
                        customEdit(obj);
                    } else if (cate == "SuperAdministrator") {
                        customEdit(obj);
                    }
                    else if (obj.data.ContState === 0 && obj.data.ModificationTimes > 0) {//变更修改
                        contractChange(obj, null);
                    }
                    else {
                        layer.alert("只有未执行且没有变更过的合同才允许修改！");
                        return false;
                    }
                }
                break;
            case "detail":
                {
                    var ress = wooutil.requestpremission({
                        url: '/NfCommon/NfPermission/ViewPermission?rand=' + wooutil.getRandom(),
                        data: {
                            FuncCode: 'querypaycontview',
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
            case "compdetail"://对方查看
                {
                    var ress = wooutil.requestpremission({
                        url: '/NfCommon/NfPermission/ViewPermission?rand=' + wooutil.getRandom(),
                        data: {
                            FuncCode: 'querysupplierview',
                            ObjId: obj.data.CompId
                        }

                    });
                    if (ress.RetValue == 0) {
                        opencompview(obj);
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
    /*********************************************************监听工具栏-end***************************************************************************/
    //解决高级查询选择下拉影响页面错乱问题
    //合同类别
    //form.on('select(ContTypeId)', function (data) {
    //    $(document.body).trigger("click");
    //});
    ////合同来源
    //form.on('select(ContSourceId)', function (data) {
    //    $(document.body).trigger("click");
    //});
    ////合同状态
    //form.on('select(selectZt)', function (data) {
    //    $(document.body).trigger("click");
    //});
    ////合同状态
    //form.on('select(IsZb)', function (data) {
    //    $(document.body).trigger("click");
    //});


    exports('paymentContractIndex', {});
});