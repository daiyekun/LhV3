/**
*招标详情查看
*/
layui.define(['form', 'tableSelect', 'selectnfitem'
    , "viewPageEdit", 'treeSelect', 'appListHist', 'subMetDetail', 'wordAddin'], function (exports) {
        var $ = layui.$
            , setter = layui.setter
            , admin = layui.admin
            , tableSelect = layui.tableSelect
            , selectnfitem = layui.selectnfitem
            , viewPageEdit = layui.viewPageEdit
            , treeSelect = layui.treeSelect
            , form = layui.form
            , subMetDetail = layui.subMetDetail
            , appListHist = layui.appListHist
            , wordAddin = layui.wordAddin;
        var contId = wooutil.getUrlVar('Id');
        var IsSp = parseInt(wooutil.getUrlVar('IsSp'));//是不是审批
        if (isNaN(IsSp)) {
            IsSp = 0;
        }
        var updatedata = [];

        /*******************************绑定值-begin*************************************************/
        layui.use('nfcontents', function () {
            var nfcontents = layui.nfcontents;
            //目录
            nfcontents.render({ content: '#customernva' });
            //绑定数据
            if (contId !== "" && contId !== undefined) {
                admin.req({
                    url: '/Tender/TenderInfo/ShowView',
                    data: { Id: contId, rand: wooutil.getRandom() },
                    done: function (res) {
                        form.val("NF-TenderinfoCollection-Form", res.Data);
                        SetValueHand(res.Data);
                        //修改次要字段
                        seteditsecfiled();

                    }
                });

            }

        });
        /*******************************绑定值-end**********************************************************/

 
        layui.use('table', function () {
            var table = layui.table;
            /**********************开标情况-begin***************************************************************************************************/
            table.render({
                elem: '#NF-ZBplanfinace'
                , url: '/Tender/TenderInfo/GetKbqkList?contId=' + contId + '&rand=' + wooutil.getRandom()
                , toolbar: '#toolcontplanKbqk'
                , defaultToolbar: ['filter']
                , cols: [[
                    { type: 'numbers', fixed: 'left' }
                    , { type: 'checkbox', fixed: 'left' }
                    , { field: 'Id', title: 'Id', width: 50, hide: true }
                    , { field: 'OpenSituationName', title: '名称', fixed: 'left', edit: 'text' }
                    , { field: 'UnitName', title: '单位', fixed: 'left',templet: '#compTpl' }
                    , { field: 'Unit', title: 'ID', width: 120, hide: true }
                    , { field: 'TotalPricethis', title: '总价', width: 200, edit: 'text' }
                    , { field: 'Uitpricethis', title: '单价', width: 160, edit: 'text' }
                    , { field: 'UserName', title: '人员', width: 160, edit: 'text', event: 'SelectUSname' }
                    , { field: 'UserId', hide: true }
                    , { title: '操作', width: 150, align: 'center', fixed: 'right', toolbar: '#table-contplanKbqkbar' }
                ]]
                , page: false
                , loading: true
                , height: setter.table.height_tab
                , limit: setter.table.limit_tab

            });
            var planfinanceEvent = {
                add: function () {
                    /// <summary>列表头部-新增按钮</summary>
                    layer.open({
                        type: 2
                        , title: '新建计划资金'
                        , content: '/Finance/ContPlanFinance/Build'
                        //, maxmin: true
                        , area: ['60%', '80%']
                        , btn: ['确定', '取消']
                        , btnAlign: 'c'
                        , skin: "layer-ext-myskin"
                        , yes: function (index, layero) {
                            var iframeWindow = window['layui-layer-iframe' + index]
                                , submitID = 'NF-ContPlanFinance-FormSubmit'
                                , submit = layero.find('iframe').contents().find('#' + submitID);
                            layero.find('iframe').contents().find('#ContId').val(contId);//合同ID
                            layero.find('iframe').contents().find('#IsFramework').val(1);//表示是框合同执行新增计划资金

                            //监听提交
                            iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                                wooutil.OpenSubmitForm({
                                    url: '/Finance/ContPlanFinance/Save',
                                    data: obj.field,
                                    table: table,
                                    index: index,
                                    tableId: 'NF-ZBplanfinace'
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
                    wooutil.deleteDatas({ tableId: 'NF-ZBplanfinace', url: '/Finance/ContPlanFinance/Delete?isFra=true', nopage: true });
                },
                tooldel: function (obj) {
                    /// <summary>列表操作栏-删除</summary>
                    ///<param name='obj'>删除数据对象</param>
                    wooutil.deleteInfo({ tableId: "NF-ZBplanfinace", data: obj, url: '/Finance/ContPlanFinance/Delete?isFra=true', nopage: true });

                },
                tooledit: function (obj) {
                    ///<summary>修改计划资金</summary>
                    ///<param name='obj'>修改数据对象</param>
                    layer.open({
                        type: 2
                        , title: '修改计划资金'
                        , content: '/Finance/ContPlanFinance/Build?Id=' + obj.data.Id + "&rand=" + wooutil.getRandom()
                        // , maxmin: true
                        , area: ['60%', '80%']
                        , btn: ['确定', '取消']
                        , btnAlign: 'c'
                        , skin: "layer-ext-myskin"
                        , yes: function (index, layero) {
                            var iframeWindow = window['layui-layer-iframe' + index]
                                , submitID = 'NF-ContPlanFinance-FormSubmit'
                                , submit = layero.find('iframe').contents().find('#' + submitID);
                            layero.find('iframe').contents().find('#ContId').val(contId);
                            layero.find('iframe').contents().find('#IsFramework').val(contId);
                            //监听提交
                            iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                                wooutil.OpenSubmitForm({
                                    url: '/Finance/ContPlanFinance/UpdateSave',
                                    data: obj.field,
                                    table: table,
                                    index: index,
                                    tableId: 'NF-ContPlanFinace'
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
            //合同计划资金头部工具栏
            table.on('toolbar(NF-ZBplanfinace)', function (obj) {
                switch (obj.event) {
                    case 'add':
                        planfinanceEvent.add();
                        break;
                    case 'batchdel':
                        planfinanceEvent.batchdel();
                        break;
                    case 'LAYTABLE_COLS'://选择列-系统默认不管
                        break;
                    default:
                        layer.alert("暂不支持（" + obj.event + "）");
                        break;

                };
            });
            //列表操作栏
            table.on('tool(NF-ZBplanfinace)', function (obj) {
                var _data = obj.data;
                switch (obj.event) {
                    case 'del':
                        planfinanceEvent.tooldel(obj);
                        break;
                    case 'edit':
                        planfinanceEvent.tooledit(obj);
                        break;
                    default:
                        layer.alert("暂不支持（" + obj.event + "）");
                        break;
                }
            });

            /***********************开标情况-end***************************************************************************************************/
            /***********************招标人-begin***************************************************************************************************/

            table.render({
                elem: '#NF-ZBRplanfinace'
                , url: '/Tender/TendererNameLabel/GetKbqkList?contId=' + contId + '&rand=' + wooutil.getRandom()
                , toolbar: '#toolcontplanZbr'
                , defaultToolbar: ['filter']
                , cols: [[
                    { type: 'numbers', fixed: 'left' }
                    , { type: 'checkbox', fixed: 'left' }
                    , { field: 'Id', title: 'Id', width: 50, hide: true }
                    , { field: 'TeNameLabel', title: '名称', width: 200, edit: 'text', fixed: 'left' }
                    , { field: 'Psition', title: '职位', edit: 'text', width: 200 }
                    , { field: 'UserName', title: '招标人', width: 200, event: 'SelectzbUSname' }
                    , { field: 'UserId', title: 'Id', width: 50, hide: true }
                    , { field: 'TeDartmentName', title: '部门', width: 200, event: 'SelectzbPJname' }
                    , { field: 'TeDartment', title: 'Id', width: 50, hide: true }
                    , { title: '操作', width: 150, align: 'center', fixed: 'right', toolbar: '#table-contplanZbrbar' }
                ]]
                , page: false
                , loading: true
                , height: setter.table.height_tab
                , limit: setter.table.limit_tab
                // , limits: setter.table.limits

            });
            table.on('edit(NF-ZBRplanfinace)', function (obj) {
                isupdate = true;
                var data = obj.data; //得到所在行所有键值
                var filed = obj.field;
                var newdata = {};
                switch (filed) {
                    case "TeNameLabel"://名称
                        {
                            newdata[filed] = obj.value;
                            obj.update(newdata);
                            obj.data.PlanDateTime = obj.value;
                        }
                        break;
                    case "Psition"://职位
                        {
                            newdata[filed] = obj.value;
                            obj.update(newdata);
                            obj.data.TotalPrice = obj.value;
                        }
                        break;
                }
                for (var i = 0; i < updatedata.length; i++) {
                    if (updatedata[i].Id === obj.data.Id) {
                        updatedata.splice(i, 1);
                    }
                }

                updatedata.push(obj.data);

            });
            var planfinanceEvent = {
                add: function () {
                    wooutil.OpenSubmitForm({
                        url: '/Tender/TendererNameLabel/SaveKbr?contId=' + contId,
                        //data: obj.field,
                        table: table,
                        index: 10,
                        tableId: 'NF-ZBRplanfinace'
                    });
                    return false;
                    submit.trigger('click');
                }, SaveAll: function () {
                    if (isupdate) {
                        admin.req({
                            url: '/Tender/TendererNameLabel/SaveData?contId=' + contId,
                            data: { subs: updatedata },
                            type: 'POST',
                            done: function (res) {
                                //清空变量，防止重复提交
                                updatedata = [];
                                isupdate = false;
                                return layer.msg('保存成功', { icon: 1 });
                            }
                        });
                    } else {
                        return layer.msg('数据没有任何修改！', { icon: 5 });
                    }
                },
                batchdel: function () {
                    /// <summary>列表头部-批量删除</summary>
                    wooutil.deleteDatas({ tableId: 'NF-contplanKbqk', url: '/Tender/TendererNameLabel/Deletekb', nopage: true });
                },
                tooldel: function (obj) {
                    /// <summary>列表操作栏-删除</summary>
                    ///<param name='obj'>删除数据对象</param>
                    wooutil.deleteInfo({ tableId: "NF-contplanKbqk", data: obj, url: '/Tender/TendererNameLabel/Deletekb', nopage: true });
                }
            };
            //合同计划资金头部工具栏
            table.on('toolbar(NF-ZBRplanfinace)', function (obj) {
                switch (obj.event) {
                    case 'add':
                        planfinanceEvent.add();
                        break;
                    case 'batchdel':
                        planfinanceEvent.batchdel();
                        break;
                    case 'SaveAll':
                        planfinanceEvent.SaveAll();
                        break;
                    case 'LAYTABLE_COLS'://选择列-系统默认不管
                        break;
                    default:
                        layer.alert("暂不支持1（" + obj.event + "）");
                        break;

                };
            });
            //列表操作栏
            table.on('tool(NF-ZBRplanfinace)', function (obj) {
                var _data = obj.data;
                var newdata = {};
                switch (obj.event) {
                    case 'del':
                        planfinanceEvent.tooldel(obj);
                        break;
                    case "SelectzbPJname":
                        {
                            var field = $(this).data('field');
                            function NEWOPEN() {
                                layer.open({
                                    type: 2
                                    , title: '修改部门'
                                    , content: '/Tender/TendererNameLabel/Build?Id=' + obj.data.Id + "&rand=" + wooutil.getRandom()
                                    //, maxmin: true
                                    , area: ['60%', '80%']
                                    , btn: ['确定', '取消']
                                    , btnAlign: 'c'
                                    , skin: "layer-ext-myskin"
                                    , yes: function (index, layero) {
                                        var iframeWindow = window['layui-layer-iframe' + index]
                                        var ContractEnforId = layero.find('iframe').contents().find('#ContractEnforcementDepId');
                                        var ContractEnforName = layero.find('iframe').contents().find('#ContractEnforcementDepName');
                                        var Conid = ContractEnforId.val();
                                        var ConName = ContractEnforName.val();
                                        isupdate = true;
                                        newdata[field] = ConName;
                                        newdata["TeDartment"] = Conid;
                                        obj.update(newdata);
                                        for (var i = 0; i < updatedata.length; i++) {
                                            if (updatedata[i].Id === obj.data.Id) {
                                                updatedata.splice(i, 1);
                                            }
                                        }
                                        obj.data.TeDartmentName = ConName;
                                        obj.data.TeDartment = Conid;
                                        updatedata.push(obj.data);
                                        layer.closeAll()
                                    },
                                    success: function (layero, index) {

                                    }
                                });
                                $(this.lastChild).trigger("click");
                            }
                            NEWOPEN();
                        }
                        break;
                    case "SelectzbUSname":
                        {
                            var field = $(this).data('field');
                            var _url = '/System/UserInfor/GetList?selitem=' + true;
                            tableSelect.render({
                                elem: this.lastChild,
                                searchKey: 'keyWord',
                                searchPlaceholder: '关键词搜索',
                                table: {
                                    url: _url + '&rand=' + wooutil.getRandom(),
                                    cols: [[
                                        { type: 'numbers', fixed: 'left' }
                                        , { field: 'Id', width: 100, hide: true }
                                        , { field: 'Name', title: '用户名称', minWidth: 150, fixed: 'left' }
                                        , { field: 'DeptName', title: '所属机构', width: 130 }
                                        , { field: 'DisplyName', title: '显示名称', width: 110 }
                                        , { field: 'SexDic', width: 100, title: '性别' }
                                        , { field: 'Age', width: 100, title: '年龄', hide: true }
                                        , { field: 'Tel', width: 100, title: '电话' }
                                        , { field: 'Mobile', width: 120, title: '手机' }
                                        , { field: 'Email', width: 120, title: 'E-Mail', hide: true }
                                    ]]
                                },
                                done: function (elem, data) {
                                    isupdate = true;
                                    newdata[field] = data.data[0].Name;
                                    newdata["UserId"] = data.data[0].Id;
                                    obj.update(newdata);
                                    for (var i = 0; i < updatedata.length; i++) {
                                        if (updatedata[i].Id === obj.data.Id) {
                                            updatedata.splice(i, 1);
                                        }
                                    }
                                    obj.data.UserName = data.data[0].Name;
                                    obj.data.UserId = data.data[0].Id;
                                    updatedata.push(obj.data);

                                }

                            });
                            $(this.lastChild).trigger("click");

                        }
                        break;
                    default:
                        layer.alert("暂不支持2（" + obj.event + "）");
                        break;
                }
            });

          /***********************招标人-end***************************************************************************************************/

            /***********************中标单位-begin***************************************************************************************************/

            table.render({
                elem: '#NF-contplanZbdw'
                , url: '/Tender/SuccessfulBidderLable/GetKbqkList?contId=' + contId + '&rand=' + wooutil.getRandom()
                , toolbar: '#toolcontplanZbdw'
                , defaultToolbar: ['filter']
                , cols: [[
                    { type: 'numbers', fixed: 'left' }
                    , { type: 'checkbox', fixed: 'left' }
                    , { field: 'Id', title: 'Id', width: 50, hide: true }
                    , { field: 'SuccessName', title: '名称', width: 200, edit: 'text', fixed: 'left' }
                    , { field: 'SuccessUntiName', title: '开标情况单位', width: 200, templet: '#compTpls' }
                    , { field: 'SuccessUntiId', title: 'SuccessUntiId', width: 50, hide: true }
                    , { fineid: 'SuccessUntiIds', title: '合同对方id', hide: true}
                    , { field: 'SuccTotalPricethis', title: '中标总价', edit: 'text', width: 200 }
                    , { field: 'SuccUitpricethis', title: '中标单价', edit: 'text', width: 200 }
                    , { field: 'SuccName', title: '中标人员', width: 200, event: 'SelectzbUSname' }
                    , { field: 'SuccId', title: 'Id', width: 50, hide: true }
                    , { title: '操作', width: 150, align: 'center', fixed: 'right', toolbar: '#table-contplanZbdwbar' }
                ]]
                , page: false
                , loading: true
                , height: setter.table.height_tab
                , limit: setter.table.limit_tab
                // , limits: setter.table.limits

            });
            table.on('edit(NF-contplanZbdw)', function (obj) {
                isupdate = true;
                var data = obj.data; //得到所在行所有键值
                var filed = obj.field;
                var newdata = {};
                switch (filed) {
                    case "SuccessName"://名称
                        {
                            newdata[filed] = obj.value;
                            obj.update(newdata);
                            obj.data.PlanDateTime = obj.value;
                        }
                        break;
                    case "SuccTotalPrice"://职位
                        {
                            newdata[filed] = obj.value;
                            obj.update(newdata);
                            obj.data.TotalPrice = obj.value;
                        }
                        break;
                    case "SuccUitprice"://职位
                        {
                            newdata[filed] = obj.value;
                            obj.update(newdata);
                            obj.data.TotalPrice = obj.value;
                        }
                        break;
                }
                for (var i = 0; i < updatedata.length; i++) {
                    if (updatedata[i].Id === obj.data.Id) {
                        updatedata.splice(i, 1);
                    }
                }

                updatedata.push(obj.data);

            });
            var planZbdwEvent = {
                add: function () {
                    wooutil.OpenSubmitForm({
                        url: '/Tender/SuccessfulBidderLable/SaveKbr?contId=' + contId,
                        //data: obj.field,
                        table: table,
                        index: 10,
                        tableId: 'NF-contplanZbdw'
                    });
                    return false;
                    submit.trigger('click');
                }, SaveAll: function () {
                    if (isupdate) {
                        admin.req({
                            url: '/Tender/SuccessfulBidderLable/SaveData?contId=' + contId,
                            data: { subs: updatedata },
                            type: 'POST',
                            done: function (res) {
                                //清空变量，防止重复提交
                                updatedata = [];
                                isupdate = false;
                                return layer.msg('保存成功', { icon: 1 });
                            }
                        });
                    } else {
                        return layer.msg('数据没有任何修改！', { icon: 5 });
                    }
                },
                batchdel: function () {
                    /// <summary>列表头部-批量删除</summary>
                    wooutil.deleteDatas({ tableId: 'NF-contplanZbdw', url: '/Tender/SuccessfulBidderLable/Deletekb', nopage: true });
                },
                tooldel: function (obj) {
                    /// <summary>列表操作栏-删除</summary>
                    ///<param name='obj'>删除数据对象</param>
                    wooutil.deleteInfo({ tableId: "NF-contplanZbdw", data: obj, url: '/Tender/SuccessfulBidderLable/Deletekb', nopage: true });
                }
            };
            //合同计划资金头部工具栏
            table.on('toolbar(NF-contplanZbdw)', function (obj) {
                switch (obj.event) {
                    case 'add':
                        planZbdwEvent.add();
                        break;
                    case 'batchdel':
                        planZbdwEvent.batchdel();
                        break;
                    case 'SaveAll':
                        planZbdwEvent.SaveAll();
                        break;
                    case 'LAYTABLE_COLS'://选择列-系统默认不管
                        break;
                    default:
                        layer.alert("暂不支持3（" + obj.event + "）");
                        break;

                };
            });
            //列表操作栏
            table.on('tool(NF-contplanZbdw)', function (obj) {
                var _data = obj.data;
                var newdata = {};
                switch (obj.event) {
                    case 'del':
                        planZbdwEvent.tooldel(obj);
                        break;
                    case "SelectzbPJname":
                        {
                            var field = $(this).data('field');
                            var _url = '/Tender/SuccessfulBidderLable/GetZbList?contId=' + contId + '&selitem=' + true;
                            tableSelect.render({
                                elem: this.lastChild,
                                searchKey: 'keyWord',
                                searchPlaceholder: '关键词搜索',
                                table: {
                                    url: _url + '&rand=' + wooutil.getRandom(),
                                    cols: [[
                                        { type: 'numbers', fixed: 'left' }
                                        , { type: 'checkbox', fixed: 'left' }
                                        , { field: 'Id', width: 100, hide: true }
                                        , { field: 'Unit', width: 100, hide: true }
                                        , { field: 'UnitName', title: '单位名称', minWidth: 150, fixed: 'left' }
                                    ]]
                                },
                                done: function (elem, data) {
                                    if (data.data.length !== 0) {
                                    isupdate = true;
                                    newdata[field] = data.data[0].UnitName;
                                    newdata["SuccessUntiId"] = data.data[0].Id;
                                    obj.update(newdata);
                                    for (var i = 0; i < updatedata.length; i++) {
                                        if (updatedata[i].Id === obj.data.Id) {
                                            updatedata.splice(i, 1);
                                        }
                                    }
                                    obj.data.SuccessUntiName = data.data[0].UnitName;
                                    obj.data.SuccessUntiId = data.data[0].Id;
                                        updatedata.push(obj.data);

                                    }
                                }
                            });
                            $(this.lastChild).trigger("click");
                        }
                        break;
                    case "SelectzbUSname":
                        {
                            var field = $(this).data('field');
                            var _url = '/System/UserInfor/GetList?selitem=' + true;
                            tableSelect.render({
                                elem: this.lastChild,
                                searchKey: 'keyWord',
                                searchPlaceholder: '关键词搜索',
                                table: {
                                    url: _url + '&rand=' + wooutil.getRandom(),
                                    cols: [[
                                        { type: 'numbers', fixed: 'left' }
                                        , { field: 'Id', width: 100, hide: true }
                                        , { field: 'Name', title: '用户名称', minWidth: 150, fixed: 'left' }
                                        , { field: 'DeptName', title: '所属机构', width: 130 }
                                        , { field: 'DisplyName', title: '显示名称', width: 110 }
                                        , { field: 'SexDic', width: 100, title: '性别' }
                                        , { field: 'Age', width: 100, title: '年龄', hide: true }
                                        , { field: 'Tel', width: 100, title: '电话' }
                                        , { field: 'Mobile', width: 120, title: '手机' }
                                        , { field: 'Email', width: 120, title: 'E-Mail', hide: true }
                                    ]]
                                },
                                done: function (elem, data) {
                                    isupdate = true;
                                    newdata[field] = data.data[0].Name;
                                    newdata["SuccId"] = data.data[0].Id;
                                    obj.update(newdata);
                                    for (var i = 0; i < updatedata.length; i++) {
                                        if (updatedata[i].Id === obj.data.Id) {
                                            updatedata.splice(i, 1);
                                        }
                                    }
                                    obj.data.SuccName = data.data[0].Name;
                                    obj.data.SuccId = data.data[0].Id;
                                    updatedata.push(obj.data);

                                }

                            });
                            $(this.lastChild).trigger("click");

                        }
                        break;
                    default:
                        layer.alert("暂不支持4（" + obj.event + "）");
                        break;
                }
            });
         
         
        /***********************中标单位-end***************************************************************************************************/
            table.on('tool(NF-contplanZbdw)', function (obj) {
                switch (obj.event) {
                    case "compdetails":
                        {
                            var ress = wooutil.requestpremission({
                                url: '/NfCommon/NfPermission/ViewPermission?rand=' + wooutil.getRandom(),
                                data: {
                                    FuncCode: 'querycustomerview',
                                    ObjId: obj.data.SuccessUntiIds
                                }
                            });
                            if (ress.RetValue == 0) {
                                zbdwopencompview(obj);
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
             * 中标单位
  *打开客户查看页面
  **/
            function zbdwopencompview(obj) {
                parent.layer.open({
                    type: 2
                    , title: '查看详情'
                    , content: '/Company/Customer/Detail?Id=' + obj.data.SuccessUntiIds + "&rand=" + wooutil.getRandom()
                    , maxmin: true
                    , area: ['60%', '80%']
                    , btnAlign: 'c'
                    , skin: "layer-nf-nfskin"
                    , btn: ['关闭']
                    , success: function (layero, index) {
                        parent.layer.load(0, { shade: false, time: 1 * 1000 });
                        parent.layer.full(index);
                        wooutil.openTip();

                    }
                });
            };

            table.on('tool(NF-ZBplanfinace)', function (obj) {

                switch (obj.event) {
                    case "compdetail":
                        {
                            var ress = wooutil.requestpremission({
                                url: '/NfCommon/NfPermission/ViewPermission?rand=' + wooutil.getRandom(),
                                data: {
                                    FuncCode: 'querycustomerview',
                                    ObjId: obj.data.Unit
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
        /********************************中标货物清单-begin****************************************************/
            table.render({
                elem: '#NF-Cargo',
                url: '/Tender/WinningItems/GetActListByContId?contId=' + contId + '&rand=' + wooutil.getRandom()
                , toolbar: '#toolcontplanfinaceHw'
                , defaultToolbar: ['filter']
                , cols: [[
                    { type: 'numbers', fixed: 'left' }
                    , { type: 'checkbox', fixed: 'left' }
                    , { field: 'Id', title: 'Id', width: 50, hide: true }
                    , { field: 'WinningName', title: '品名', width: 200 }
                    , { field: 'WinningUntiId', title: '品牌', width: 160 }
                    , { field: 'WinningModel', title: '型号', width: 160 }
                    , { field: 'WinningQuantity', title: '数量', width: 280 }
                    , { field: 'WinningTotalPrice', title: '单价', width: 280 }
                    , { field: 'WinningUitprice', title: '总价', width: 280 }
                    , { title: '操作', width: 150, align: 'center', fixed: 'right', toolbar: '#table-X' }
                ]]
                , page: false
                , loading: true
                , height: setter.table.height_tab
                , limit: setter.table.limit_tab
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
                        , title: '新建'
                        , content: 'Tender/WinningItems/Build'
                        , area: ['60%', '80%']
                        , btn: ['确定', '取消']
                        , btnAlign: 'c'
                        , skin: "layer-ext-myskin"
                        , yes: function (index, layero) {
                            var iframeWindow = window['layui-layer-iframe' + index]
                                , submitID = 'NF-InqAttachment-FormSubmit'
                                , submit = layero.find('iframe').contents().find('#' + submitID);
                            var Contractfiled = layero.find('iframe').contents().find('#ContId');
                            Contractfiled.val(contId);
                            //监听提交
                            iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                                wooutil.OpenSubmitForm({
                                    url: 'Tender/WinningItems/ShowView1',
                                    data: obj.field,
                                    table: table,
                                    index: index,
                                    tableId: 'NF-Cargo'
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
                    wooutil.deleteDatas({ tableId: 'NF-ContAttachment', table: table, url: 'Tender/WinningItemsDelete', nopage: true });
                },
                tooldownload: function (obj) {
                    wooutil.download({
                        Id: obj.data.Id,
                        folder: 5//标识合同附件
                    });
                },
                tooldel: function (obj) {
                    /// <summary>列表操作栏-删除</summary>
                    ///<param name='obj'>删除数据对象</param>
                    wooutil.deleteInfo({ tableId: "NF-ContAttachment", data: obj, url: 'Tender/WinningItems/Delete', nopage: true });
                },
                tooledit: function (obj) {
                    ///<summary>修改</summary>
                    ///<param name='obj'>修改数据对象</param>
                    layer.open({
                        type: 2
                        , title: '修改附件'
                        , content: '/Contract/ContAttachment/Build?Id=' + obj.data.Id + "&rand=" + wooutil.getRandom()
                        //, maxmin: true
                        , area: ['60%', '80%']
                        , btn: ['确定', '取消']
                        , btnAlign: 'c'
                        , skin: "layer-ext-myskin"
                        , yes: function (index, layero) {
                            var iframeWindow = window['layui-layer-iframe' + index]
                                , submitID = 'NF-ContAttachment-FormSubmit'
                                , submit = layero.find('iframe').contents().find('#' + submitID);
                            var Contractfiled = layero.find('iframe').contents().find('#ContId');
                            Contractfiled.val(contId);
                            //监听提交
                            iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                                wooutil.OpenSubmitForm({
                                    url: '/Contract/ContAttachment/UpdateSave',
                                    data: obj.field,
                                    table: table,
                                    index: index,
                                    tableId: 'NF-ContAttachment'
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
            var planfinanceEvent = {

                tooledit: function (obj) {
                    ///<summary>修改计划资金</summary>
                    ///<param name='obj'>修改数据对象</param>
                    layer.open({
                        type: 2
                        , title: '修改中标货物'
                        , content: '/Finance/ContPlanFinance/Build?Id=' + obj.data.Id + "&rand=" + wooutil.getRandom()
                        // , maxmin: true
                        , area: ['60%', '80%']
                        , btn: ['确定', '取消']
                        , btnAlign: 'c'
                        , skin: "layer-ext-myskin"
                        , yes: function (index, layero) {
                            var iframeWindow = window['layui-layer-iframe' + index]
                                , submitID = 'NF-ContPlanFinance-FormSubmit'
                                , submit = layero.find('iframe').contents().find('#' + submitID);
                            layero.find('iframe').contents().find('#ContId').val(contId);
                            //监听提交
                            iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                                wooutil.OpenSubmitForm({
                                    url: '/Finance/ContPlanFinance/UpdateSave',
                                    data: obj.field,
                                    table: table,
                                    index: index,
                                    tableId: 'NF-Cargo'
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
            //合同计划资金头部工具栏
            table.on('toolbar(NF-Cargo)', function (obj) {
                switch (obj.event) {
                    case 'add':
                        attachmentEvent.add();
                        break;
                    case 'batchdel':
                        planfinanceEvent.batchdel();
                        break;
                    case 'LAYTABLE_COLS'://选择列-系统默认不管
                        break;
                    default:
                        layer.alert("暂不支持（" + obj.event + "）");
                        break;

                };
            });
            //列表操作栏
            table.on('tool(NF-Cargo)', function (obj) {
                var _data = obj.data;
                switch (obj.event) {
                    case 'del':
                        planfinanceEvent.tooldel(obj);
                        break;
                    case 'edit':
                        planfinanceEvent.tooledit(obj);
                        break;
                    default:
                        layer.alert("暂不支持（" + obj.event + "）");
                        break;
                }
            });
            /********************************中标货物清单-end****************************************************/

            /***********************招标附件信息-begin***************************************************************************************************/
            table.render({
                elem: '#NF-ContAttachment'
                , url: '/Tender/TenderAttachment/GetList?contId=' + contId + '&rand=' + wooutil.getRandom()
                , toolbar: '#toolcontattachment'
                , defaultToolbar: ['filter']
                , cols: [[
                    { type: 'numbers', fixed: 'left' }
                    , { type: 'checkbox', fixed: 'left' }
                    , { field: 'Id', title: 'Id', width: 50, hide: true }
                    , { field: 'Name', title: '附件名称', width: 180, fixed: 'left' }
                    , { field: 'CategoryName', title: '附件类别', width: 140 }
                    , { field: 'Remark', title: '文件说明', width: 200 }
                    , { field: 'FileName', title: '文件名', width: 180 }
                    , { field: 'CreateDateTime', title: '上传日期', width: 120 }//tabl-contattachmentbar
                    , { title: '操作', width: 220, align: 'center', fixed: 'right', toolbar: '#tabl-contattachmentbar' }
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
                        , content: '/Tender/TenderAttachment/Build'
                        // , maxmin: false
                        , area: ['60%', '80%']
                        , btn: ['确定', '取消']
                        , btnAlign: 'c'
                        , skin: "layer-ext-myskin"
                        , yes: function (index, layero) {
                            var iframeWindow = window['layui-layer-iframe' + index]
                                , submitID = 'NF-ContAttachment-FormSubmit'
                                , submit = layero.find('iframe').contents().find('#' + submitID);
                            var Contractfiled = layero.find('iframe').contents().find('#ContId');
                            Contractfiled.val(contId);
                            //监听提交
                            iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                                wooutil.OpenSubmitForm({
                                    url: '/Tender/TenderAttachment/Save',
                                    data: obj.field,
                                    table: table,
                                    index: index,
                                    tableId: 'NF-ContAttachment'
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
                    wooutil.deleteDatas({ tableId: 'NF-ContAttachment', table: table, url: '/Tender/TenderAttachment/Delete', nopage: true });
                },
                tooldownload: function (obj) {
                    wooutil.download({
                        Id: obj.data.Id,
                        folder: 12//标识招标附件
                    });
                },
                tooldel: function (obj) {
                    /// <summary>列表操作栏-删除</summary>
                    ///<param name='obj'>删除数据对象</param>
                    wooutil.deleteInfo({ tableId: "NF-ContAttachment", data: obj, url: '/Tender/TenderAttachment/Delete', nopage: true });
                },
                tooledit: function (obj) {
                    ///<summary>修改</summary>
                    ///<param name='obj'>修改数据对象</param>
                    layer.open({
                        type: 2
                        , title: '修改附件'
                        , content: '/Tender/TenderAttachment/Build?Id=' + obj.data.Id + "&rand=" + wooutil.getRandom()
                        //, maxmin: true
                        , area: ['60%', '80%']
                        , btn: ['确定', '取消']
                        , btnAlign: 'c'
                        , skin: "layer-ext-myskin"
                        , yes: function (index, layero) {
                            var iframeWindow = window['layui-layer-iframe' + index]
                                , submitID = 'NF-ContAttachment-FormSubmit'
                                , submit = layero.find('iframe').contents().find('#' + submitID);
                            var Contractfiled = layero.find('iframe').contents().find('#ContId');
                            Contractfiled.val(contId);
                            //监听提交
                            iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                                wooutil.OpenSubmitForm({
                                    url: '/Tender/TenderAttachment/UpdateSave',
                                    data: obj.field,
                                    table: table,
                                    index: index,
                                    tableId: 'NF-ContAttachment'
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
            table.on('toolbar(NF-ContAttachment)', function (obj) {
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
            table.on('tool(NF-ContAttachment)', function (obj) {
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

        /***********************合同附件信息-end***************************************************************************************************/

        });
        /**
   * 开标情况
*  打开客户查看页面
**/
        function opencompview(obj) {
            parent.layer.open({
                type: 2
                , title: '查看详情'
                , content: '/Company/Customer/Detail?Id=' + obj.data.Unit + "&rand=" + wooutil.getRandom()
                , maxmin: true
                , area: ['60%', '80%']
                , btnAlign: 'c'
                , skin: "layer-nf-nfskin"
                , btn: ['关闭']
                , success: function (layero, index) {
                    parent.layer.load(0, { shade: false, time: 1 * 1000 });
                    parent.layer.full(index);
                    wooutil.openTip();

                }
            });
        };

        /*******************************编辑次要字段-begin***********************************************/
        //经办机构
        function IniDept(el, vel, selVal) {
            treeSelect.render(
                {
                    elem: "#" + el,
                    data: '/System/Department/GetTreeSelectDept',
                    method: "GET",
                    search: true,
                    click: function (d) {
                        $("input[name=" + el + "]").val(d.current.id);
                        $("input[name=" + vel + "]").val(d.current.name);
                    },
                    success: function (d) {
                        if (selVal != null) {
                            treeSelect.checkNode(el, selVal);
                        }

                    }
                });
        }
        /**次要字段编辑**/
        function seteditsecfiled() {
            //var ress = wooutil.requestpremission({
            //    url: '/NfCommon/NfPermission/SecFieldUpatePremission?rand=' + wooutil.getRandom(),
            //    data: {
            //        FuncCode: 'updatecollcontminor',
            //        ObjId: contId
            //    }

            //});
            var updateFields =
                ["OtherCode", "PerformanceDateTime", "ContSName", "ContTypeName"
                    , "PrincipalUserName", "StampTaxThod", "DeptName", "ProjName"
                    , "Reserve1", "Reserve2"];
            //if (ress.RetValue == 0) {//有权限
                $.each(updateFields, function (index, fieldId) {

                    switch (fieldId) {
                        case "OtherCode":
                        case 'StampTaxThod':
                        case "Reserve1":
                        case 'Reserve2':
                            {//都是文本编辑框
                                viewPageEdit.render({
                                    elem: '#' + fieldId,
                                    edittype: 'text',
                                    objid: contId,
                                    fieldname: fieldId,
                                    verify: 'required',
                                    url: '/Contract/ContractCollection/UpdateField'

                                });
                            }

                            break;
                        case "PrincipalUserName"://负责人
                            {//都是文本编辑框
                                viewPageEdit.render({
                                    elem: '#' + fieldId,
                                    edittype: 'selTable',
                                    objid: contId,
                                    fieldname: fieldId,
                                    verify: 'required',
                                    selobjId: "#PrincipalUserId",
                                    url: '/Contract/ContractCollection/UpdateField'

                                });
                                //负责人编辑
                                selectnfitem.selectUserItem(
                                    {
                                        tableSelect: tableSelect,
                                        elem: '#PrincipalUserName',
                                        hide_elem: '#PrincipalUserId'

                                    });
                            }
                            break;
                        case "ProjName"://所属项目
                            {
                                viewPageEdit.render({
                                    elem: '#' + fieldId,
                                    edittype: 'selTable',
                                    objid: contId,
                                    fieldname: fieldId,
                                    verify: 'required',
                                    selobjId: "#ProjectId",
                                    url: '/Contract/ContractCollection/UpdateField'

                                });
                                //所属项目
                                selectnfitem.selectProjItem(
                                    {
                                        tableSelect: tableSelect,
                                        elem: '#ProjName',
                                        hide_elem: '#ProjectId'

                                    });
                            }
                            break;
                        case "ContSName"://合同来源
                            {
                                var objdata = {};
                                objdata.url = '/System/DataDictionary/GetListByDataEnumType?dataEnum=' + 15;
                                objdata.empty = true;
                                objdata.emptyText = "请选择";
                                viewPageEdit.render({
                                    elem: '#ContSName',
                                    edittype: 'select',
                                    objid: contId,
                                    fieldname: "ContSName",
                                    url: '/Contract/ContractCollection/UpdateField',
                                    editobj: objdata
                                });
                            }
                            break;
                        case "ContTypeName"://合同类别
                            {
                                var objdata = {};
                                objdata.url = '/System/DataDictionary/GetListByDataEnumType?dataEnum=' + 1;
                                objdata.empty = true;
                                objdata.emptyText = "请选择";
                                viewPageEdit.render({
                                    elem: '#ContTypeName',
                                    edittype: 'select',
                                    objid: contId,
                                    fieldname: "ContTypeName",
                                    url: '/Contract/ContractCollection/UpdateField',
                                    editobj: objdata
                                });
                            }
                            break;
                        case "PerformanceDateTime"://履行时间
                            {
                                viewPageEdit.render({
                                    elem: '#PerformanceDateTime',
                                    edittype: 'date',
                                    objid: contId,
                                    fieldname: "PerformanceDateTime",
                                    url: '/System/ContractCollection/UpdateField'

                                });
                            }
                            break;
                        case "DeptName"://经办机构
                            {

                                viewPageEdit.render({
                                    elem: '#' + fieldId,
                                    edittype: 'treeSelect',
                                    objid: contId,
                                    fieldname: fieldId,
                                    verify: 'required',
                                    selobjId: "#DeptId",
                                    url: '/Contract/ContractCollection/UpdateField'

                                });

                                IniDept("DeptId", "DeptName", null);

                            }
                            break;



                    }
                });

            //} else {
            //    viewPageEdit.noUpShow(updateFields);
            //}
        }

        /********************************编辑次要字段-end*******************************************************/
        /**绑定值以后的一些条件判断**/
        function SetValueHand(objval) {
            if (objval.IsFramework == 1 && objval.ContState == 1) {
                //不是框架合同->隐藏计划资金操作按钮
                $(".financebtn").removeClass("layui-hide");
                $(".financebar").removeClass("layui-hide");
                //隐藏标的操作按钮
                $(".subbtn").removeClass("layui-hide");


            }
            if (IsSp === 1) {
                //判断如果是审批就显示审阅按钮
                $(".htReview").removeClass("layui-hide");
            }




        }

        //资金统计
        //admin.req({
        //    url: '/Contract/ContractCollection/ContractStatic',
        //    data: { Id: contId, rand: wooutil.getRandom() },
        //    done: function (res) {
        //        form.val("NF-ContStaticForm", res.Data);


        //    }
        //});

        //标的
        subMetDetail.render({ contId: contId });
        //审批历史
        appListHist.applistInit({ Id: contId, objType: setter.sysWf.flowType.Hetong });
        exports('CollectionContractDetail', {});
    });