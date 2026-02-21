/**
*收款合同查看
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
        /*******************************绑定值-begin*************************************************/
        layui.use('nfcontents', function () {
            var nfcontents = layui.nfcontents;
            //目录
            nfcontents.render({ content: '#customernva' });
            //绑定数据
            if (contId !== "" && contId !== undefined) {
                admin.req({
                    url: '/Inquiry/Inquiry/ShowView',
                    data: { Id: contId, rand: wooutil.getRandom() },
                    done: function (res) {
                        form.val("NF-ContractCollection-Form", res.Data);
                    }
                });

            }

        });
        layui.use('table', function () {
            var table = layui.table;
            /***********************开标情况-begin***************************************************************************************************/

            table.render({
                elem: '#NF-contplanKbqk'
                , url: '/Inquiry/OpenTenderCondition/GetKbqkList?contId=' + contId + '&rand=' + wooutil.getRandom()
                , toolbar: '#toolcontplanKbqk'
                , defaultToolbar: ['filter']
                , cols: [[
                    { type: 'numbers', fixed: 'left' }
                    , { type: 'checkbox', fixed: 'left' }
                    , { field: 'Id', title: 'Id', width: 50, hide: true }
                    , { field: 'Name', title: '名称', fixed: 'left', edit: 'text' }
                    , { field: 'UnitName', title: '单位', fixed: 'left', templet: '#compTpl' }
                    , { fineld:'UnitId',title:'单位id',hide:true}
                    , { field: 'Unit', title: 'ID', width: 120, hide: true }
                    , { field: 'TotalPricesthis', title: '总价', width: 200, edit: 'text' }
                    , { field: 'UnitPricethis', title: '单价', width: 160, edit: 'text' }
                    , { field: 'PersonneName', title: '人员', width: 160, edit: 'text', event: 'SelectUSname' }
                    , { field: 'Personnel', hide: true }
                    , { title: '操作', width: 150, align: 'center', fixed: 'right', toolbar: '#table-contplanKbqkbar' }
                ]]
                , page: false
                , loading: true
                , height: setter.table.height_tab
                , limit: setter.table.limit_tab
                // , limits: setter.table.limits

            });
            table.on('edit(NF-contplanKbqk)', function (obj) {
                isupdate = true;
                var data = obj.data; //得到所在行所有键值
                var filed = obj.field;
                var newdata = {};
                switch (filed) {
                    case "Name"://名称
                        {
                            newdata[filed] = obj.value;
                            obj.update(newdata);
                            obj.data.Name = obj.value;
                        }
                        break;
                    case "TotalPrices"://总价
                        {
                            newdata[filed] = obj.value;
                            obj.update(newdata);
                            obj.data.TotalPrices = obj.value;
                        }
                        break;
                    case "UnitPrice"://单价
                        {
                            newdata[filed] = obj.value;
                            obj.update(newdata);
                            obj.data.UnitPrice = obj.value;
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
            var planXJQKEvent = {
                add: function () {
                    wooutil.OpenSubmitForm({
                        url: '/Inquiry/OpenTenderCondition/SaveKbqk?contId=' + contId,
                        //data: obj.field,
                        table: table,
                        index: 10,
                        tableId: 'NF-contplanKbqk'
                    });
                    return false;
                    submit.trigger('click');
                }, SaveAll: function () {
                    if (isupdate) {
                        admin.req({
                            url: '/Inquiry/OpenTenderCondition/SaveData?contId=' + contId,
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
                    wooutil.deleteDatas({ tableId: 'NF-contplanKbqk', url: '/Inquiry/OpenTenderCondition/Deletekb', nopage: true });
                },
                tooldel: function (obj) {
                    /// <summary>列表操作栏-删除</summary>
                    ///<param name='obj'>删除数据对象</param>
                    wooutil.deleteInfo({ tableId: "NF-contplanKbqk", data: obj, url: '/Inquiry/OpenTenderCondition/Deletekb', nopage: true });

                }
            };
            //合同计划资金头部工具栏
            table.on('toolbar(NF-contplanKbqk)', function (obj) {
                switch (obj.event) {
                    case 'add':
                        planXJQKEvent.add();
                        break;
                    case 'batchdel':
                        planXJQKEvent.batchdel();
                        break;
                    case 'SaveAll':
                        planXJQKEvent.SaveAll();
                        break;
                    case 'LAYTABLE_COLS'://选择列-系统默认不管
                        break;
                    default:
                        layer.alert("暂不支持（" + obj.event + "）");
                        break;

                };
            });
            //列表操作栏
            table.on('tool(NF-contplanKbqk)', function (obj) {
                var _data = obj.data;
                var newdata = {};
                switch (obj.event) {
                    case 'del':
                        planXJQKEvent.tooldel(obj);
                        break;
                    case 'edit':
                        planXJQKEvent.tooledit(obj);
                        break;
                    case "SelectPJname":
                        {
                            var field = $(this).data('field');
                            var _url = '/Company/Supplier/GetList?selitem=' + true;
                            tableSelect.render({
                                elem: this.lastChild,
                                searchKey: 'keyWord',
                                searchPlaceholder: '关键词搜索',
                                table: {
                                    url: _url + '&rand=' + wooutil.getRandom(),
                                    cols: [[
                                        { type: 'numbers', fixed: 'left' }
                                        , { type: 'radio', fixed: 'left' }
                                        , { field: 'Name', title: '名称', minWidth: 150, fixed: 'left' }
                                        , { field: 'Code', title: '编号', width: 130 }
                                        , { field: 'CompanyTypeClass', title: '类别', width: 120 }
                                        , { field: 'FirstContact', width: 120, title: '主要联系人' }

                                    ]]
                                },
                                done: function (elem, data) {
                                    isupdate = true;
                                    newdata[field] = data.data[0].Name;
                                    newdata["Unit"] = data.data[0].Id;
                                    obj.update(newdata);
                                    for (var i = 0; i < updatedata.length; i++) {
                                        if (updatedata[i].Id === obj.data.Id) {
                                            updatedata.splice(i, 1);
                                        }
                                    }
                                    obj.data.UnitName = data.data[0].Name;
                                    obj.data.Unit = data.data[0].Id;
                                    updatedata.push(obj.data);

                                }

                            });
                            $(this.lastChild).trigger("click");

                        }
                        break;
                    case "SelectUSname":
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
                                    newdata["Personnel"] = data.data[0].Id;
                                    obj.update(newdata);
                                    for (var i = 0; i < updatedata.length; i++) {
                                        if (updatedata[i].Id === obj.data.Id) {
                                            updatedata.splice(i, 1);
                                        }
                                    }
                                    obj.data.PersonneName = data.data[0].Name;
                                    obj.data.Personnel = data.data[0].Id;
                                    updatedata.push(obj.data);

                                }

                            });
                            $(this.lastChild).trigger("click");

                        }
                        break;
                    default:
                        layer.alert("暂不支持（" + obj.event + "）");
                        break;
                }
            });

            /***********************开标情况-end***************************************************************************************************/
            table.on('tool(NF-contplanKbqk)', function (obj) {
                switch (obj.event) {
                    case "compdetail":
                        {
                            var ress = wooutil.requestpremission({
                                url: '/NfCommon/NfPermission/ViewPermission?rand=' + wooutil.getRandom(),
                                data: {
                                    FuncCode: 'querycustomerview',
                                    ObjId: obj.data.UnitId
                                }
                            });
                            if (ress.RetValue == 0) {
                                Kbqkopencompview(obj);
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
  *打开客户查看页面
  **/
            function Kbqkopencompview(obj) {
                parent.layer.open({
                    type: 2
                    , title: '查看详情'
                    , content: '/Company/Customer/Detail?Id=' + obj.data.UnitId + "&rand=" + wooutil.getRandom()
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

        /***********************招标人-begin***************************************************************************************************/
            table.render({
                elem: '#NF-contplanZbr'
                , url: '/Inquiry/Inquirer/GetKbqkList?contId=' + contId + '&rand=' + wooutil.getRandom()
                , toolbar: '#toolcontplanZbr'
                , defaultToolbar: ['filter']
                , cols: [[
                    { type: 'numbers', fixed: 'left' }
                    , { type: 'checkbox', fixed: 'left' }
                    , { field: 'Id', title: 'Id', width: 50, hide: true }
                    , { field: 'Name', title: '名称', width: 200, edit: 'text', fixed: 'left' }
                    , { field: 'Position', title: '职位', edit: 'text', width: 200 }
                    , { field: 'InqName', title: '询价人', width: 200, event: 'SelectzbUSname' }
                    , { field: 'InqId', title: 'Id', width: 50, hide: true }
                    , { field: 'DepartmentName', title: '部门', width: 200, event: 'SelectzbPJname' }
                    , { field: 'Department', title: 'Id', width: 50, hide: true }
                    , { title: '操作', width: 150, align: 'center', fixed: 'right', toolbar: '#table-contplanZbrbar' }
                ]]
                , page: false
                , loading: true
                , height: setter.table.height_tab
                , limit: setter.table.limit_tab
                // , limits: setter.table.limits

            });
            table.on('edit(NF-contplanZbr)', function (obj) {
                isupdate = true;
                var data = obj.data; //得到所在行所有键值
                var filed = obj.field;
                var newdata = {};
                switch (filed) {
                    case "Name"://名称
                        {
                            newdata[filed] = obj.value;
                            obj.update(newdata);
                            obj.data.Name = obj.value;
                        }
                        break;
                    case "Position"://职位
                        {
                            newdata[filed] = obj.value;
                            obj.update(newdata);
                            obj.data.Position = obj.value;
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
            var planXJZBREvent = {
                add: function () {
                    wooutil.OpenSubmitForm({
                        url: '/Inquiry/Inquirer/SaveKbr?contId=' + contId,
                        //data: obj.field,
                        table: table,
                        index: 10,
                        tableId: 'NF-contplanZbr'
                    });
                    return false;
                    submit.trigger('click');
                }, SaveAll: function () {
                    if (isupdate) {
                        admin.req({
                            url: '/Inquiry/Inquirer/SaveData?contId=' + contId,
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
                    wooutil.deleteDatas({ tableId: 'NF-contplanKbqk', url: '/Inquiry/Inquirer/Deletekb', nopage: true });
                },
                tooldel: function (obj) {
                    /// <summary>列表操作栏-删除</summary>
                    ///<param name='obj'>删除数据对象</param>
                    wooutil.deleteInfo({ tableId: "NF-contplanKbqk", data: obj, url: '/Inquiry/Inquirer/Deletekb', nopage: true });
                }
            };
            //合同计划资金头部工具栏
            table.on('toolbar(NF-contplanZbr)', function (obj) {
                switch (obj.event) {
                    case 'add':
                        planXJZBREvent.add();
                        break;
                    case 'batchdel':
                        planXJZBREvent.batchdel();
                        break;
                    case 'SaveAll':
                        planXJZBREvent.SaveAll();
                        break;
                    case 'LAYTABLE_COLS'://选择列-系统默认不管
                        break;
                    default:
                        layer.alert("暂不支持（" + obj.event + "）");
                        break;

                };
            });
            //列表操作栏
            table.on('tool(NF-contplanZbr)', function (obj) {
                var _data = obj.data;
                var newdata = {};
                switch (obj.event) {
                    case 'del':
                        planXJZBREvent.tooldel(obj);
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
                                        newdata["Department"] = Conid;
                                        obj.update(newdata);
                                        for (var i = 0; i < updatedata.length; i++) {
                                            if (updatedata[i].Id === obj.data.Id) {
                                                updatedata.splice(i, 1);
                                            }
                                        }
                                        obj.data.DepartmentName = ConName;
                                        obj.data.Department = Conid;
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
                                    newdata["InqId"] = data.data[0].Id;
                                    obj.update(newdata);
                                    for (var i = 0; i < updatedata.length; i++) {
                                        if (updatedata[i].Id === obj.data.Id) {
                                            updatedata.splice(i, 1);
                                        }
                                    }
                                    obj.data.InqName = data.data[0].Name;
                                    obj.data.InqId = data.data[0].Id;
                                    updatedata.push(obj.data);

                                }

                            });
                            $(this.lastChild).trigger("click");

                        }
                        break;
                    default:
                        layer.alert("暂不支持（" + obj.event + "）");
                        break;
                }
            });

            /***********************招标人-end***************************************************************************************************/
            /***********************中标单位-begin***************************************************************************************************/
            table.render({
                elem: '#NF-contplanZbdw'
                , url: '/Inquiry/TheWinningUnit/GetKbqkList?contId=' + contId + '&rand=' + wooutil.getRandom()
                , toolbar: '#toolcontplanZbdw'
                , defaultToolbar: ['filter']
                , cols: [[
                    { type: 'numbers', fixed: 'left' }
                    , { type: 'checkbox', fixed: 'left' }
                    , { field: 'Id', title: 'Id', width: 50, hide: true }
                    , { field: 'Name', title: '名称', width: 200, edit: 'text', fixed: 'left' }
                    , { field: 'WinningUnitName', title: '询价情况单位', width: 200,  templet: '#compTpls' }
                    , { field: 'WinningUnit', title: 'SuccessUntiId', width: 50, hide: true }
                    , { field: 'BidPricesthis', title: '询价总价', edit: 'text', width: 200 }
                    , { field: 'BidPricethis', title: '询价单价', edit: 'text', width: 200 }
                    , { field: 'BidUserName', title: '询价人员', width: 200, event: 'SelectzbUSname' }
                    , { field: 'BidUser', title: 'Id', width: 50, hide: true }
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
                    case "Name"://名称
                        {
                            newdata[filed] = obj.value;
                            obj.update(newdata);
                            obj.data.Name = obj.value;
                        }
                        break;
                    case "BidPricesthis"://职位
                        {
                            newdata[filed] = obj.value;
                            obj.update(newdata);
                            obj.data.BidPricesthis = obj.value;
                        }
                        break;
                    case "BidPricethis"://职位
                        {
                            newdata[filed] = obj.value;
                            obj.update(newdata);
                            obj.data.BidPricethis = obj.value;
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
            var planxjZbdwEvent = {
                add: function () {
                    wooutil.OpenSubmitForm({
                        url: '/Inquiry/TheWinningUnit/SaveKbr?contId=' + contId,
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
                            url: '/Inquiry/TheWinningUnit/SaveData?contId=' + contId,
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
                    wooutil.deleteDatas({ tableId: 'NF-contplanZbdw', url: '/Inquiry/TheWinningUnit/Deletekb', nopage: true });
                },
                tooldel: function (obj) {
                    /// <summary>列表操作栏-删除</summary>
                    ///<param name='obj'>删除数据对象</param>
                    wooutil.deleteInfo({ tableId: "NF-contplanZbdw", data: obj, url: '/Inquiry/TheWinningUnit/Deletekb', nopage: true });
                }
            };
            //合同计划资金头部工具栏
            table.on('toolbar(NF-contplanZbdw)', function (obj) {
                switch (obj.event) {
                    case 'add':
                        planxjZbdwEvent.add();
                        break;
                    case 'batchdel':
                        planxjZbdwEvent.batchdel();
                        break;
                    case 'SaveAll':
                        planxjZbdwEvent.SaveAll();
                        break;
                    case 'LAYTABLE_COLS'://选择列-系统默认不管
                        break;
                    default:
                        layer.alert("暂不支持（" + obj.event + "）");
                        break;

                };
            });
            //列表操作栏
            table.on('tool(NF-contplanZbdw)', function (obj) {
                var _data = obj.data;
                var newdata = {};
                switch (obj.event) {
                    case 'del':
                        planxjZbdwEvent.tooldel(obj);
                        break;
                    case "SelectzbPJname":
                        {
                            var field = $(this).data('field');
                            var _url = '/Inquiry/TheWinningUnit/GetZbList?contId=' + contId + '&selitem=' + true;
                            tableSelect.render({
                                elem: this.lastChild,
                                searchKey: 'keyWord',
                                searchPlaceholder: '关键词搜索',
                                table: {
                                    url: _url + '&rand=' + wooutil.getRandom(),
                                    cols: [[
                                        { type: 'numbers', fixed: 'left' }
                                        , { field: 'Id', width: 100, hide: true }
                                        , { field: 'Unit', width: 100, hide: true }
                                        , { field: 'UnitName', title: '单位名称', minWidth: 150, fixed: 'left' }
                                    ]]
                                },
                                done: function (elem, data) {
                                    isupdate = true;
                                    newdata[field] = data.data[0].UnitName;
                                    newdata["WinningUnit"] = data.data[0].Id;
                                    obj.update(newdata);
                                    for (var i = 0; i < updatedata.length; i++) {
                                        if (updatedata[i].Id === obj.data.Id) {
                                            updatedata.splice(i, 1);
                                        }
                                    }
                                    obj.data.WinningUnitName = data.data[0].UnitName;
                                    obj.data.WinningUnit = data.data[0].Id;
                                    updatedata.push(obj.data);
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
                        layer.alert("暂不支持（" + obj.event + "）");
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
                                    ObjId: obj.data.WinningUnit
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
            /**
  *打开客户查看页面
  **/
            function opencompview(obj) {
                parent.layer.open({
                    type: 2
                    , title: '查看详情'
                    , content: '/Company/Customer/Detail?Id=' + obj.data.WinningUnit + "&rand=" + wooutil.getRandom()
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
        /***********************招标附件信息-begin***************************************************************************************************/
            table.render({
                elem: '#NF-ContAttachment'
                , url: '/Inquiry/InquiryAttachment/GetList?contId=' + contId + '&rand=' + wooutil.getRandom()
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
                    , { field: 'CreateDateTime', title: '上传日期', width: 120 }
                    , { title: '操作', width: 220, align: 'center', fixed: 'right', toolbar: '#tabl-contattachmentbar' }
                ]]
                , page: false
                , loading: true
                , height: setter.table.height_tab
                , limit: setter.table.limit_tab
                // , limits: setter.table.limits

            });
            var attacXJFJEvent = {
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
                        , content: '/Inquiry/InquiryAttachment/Build'
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
                                    url: '/Inquiry/InquiryAttachment/Save',
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
                    wooutil.deleteDatas({ tableId: 'NF-ContAttachment', table: table, url: '/Inquiry/InquiryAttachment/Delete', nopage: true });
                },
                tooldownload: function (obj) {
                    wooutil.download({
                        Id: obj.data.Id,
                        folder: 13//标识招标附件
                    });
                },
                tooldel: function (obj) {
                    /// <summary>列表操作栏-删除</summary>
                    ///<param name='obj'>删除数据对象</param>
                    wooutil.deleteInfo({ tableId: "NF-ContAttachment", data: obj, url: '/Inquiry/InquiryAttachment/Delete', nopage: true });
                },
                tooledit: function (obj) {
                    ///<summary>修改</summary>
                    ///<param name='obj'>修改数据对象</param>
                    layer.open({
                        type: 2
                        , title: '修改附件'
                        , content: '/Inquiry/InquiryAttachment/Build?Id=' + obj.data.Id + "&rand=" + wooutil.getRandom()
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
                                    url: '/Inquiry/InquiryAttachment/UpdateSave',
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
                        attacXJFJEvent.add();
                        break;
                    case 'batchdel':
                        attacXJFJEvent.batchdel();
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
                        attacXJFJEvent.tooldel(obj);
                        break;
                    case 'edit':
                        attacXJFJEvent.tooledit(obj);
                        break;
                    case 'download'://下载
                        attacXJFJEvent.tooldownload(obj);
                        break;
                    default:
                        layer.alert("暂不支持（" + obj.event + "）");
                        break;
                }
            });

        /***********************合同附件信息-end***************************************************************************************************/

            /********************************中标货物清单-begin****************************************************/
            table.render({
                elem: '#NF-Cargo',
                url: '/Inquiry/Winning/GetActListByContId?contId=' + contId + '&rand=' + wooutil.getRandom()

                //  , url: '/Inquiry/Winning/ShowView1'
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
                    , { title: '操作', width: 150, align: 'center', fixed: 'right', toolbar: '#table-contplanfinacebarHw' }
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
                        , content: '/Inquiry/InqAttachment/Build'
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
                                    url: '/Inquiry/Winning/ShowView',
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
                    wooutil.deleteDatas({ tableId: 'NF-ContAttachment', table: table, url: '/Contract/ContAttachment/Delete', nopage: true });
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
                    wooutil.deleteInfo({ tableId: "NF-ContAttachment", data: obj, url: '/Contract/ContAttachment/Delete', nopage: true });
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
        });
    /*******************************绑定值-end**********************************************************/

   
        //标的
        subMetDetail.render({ contId: contId });
        //审批历史
        appListHist.applistInit({ Id: contId, objType: setter.sysWf.flowType.Hetong });
        exports('CollectionContractDetail', {});
    });