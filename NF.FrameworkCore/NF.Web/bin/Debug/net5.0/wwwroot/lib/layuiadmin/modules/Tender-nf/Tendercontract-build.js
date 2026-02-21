/**
*招标新建
*/
layui.define(['table', 'form', 'subMetBuild', 'wordAddin', "tableSelect","treeSelect"], function (exports) {
    var $ = layui.$
        , table = layui.table
        , setter = layui.setter
        , admin = layui.admin
        , form = layui.form
        , subMetBuild = layui.subMetBuild
        , wordAddin = layui.wordAddin
        , tableSelect = layui.tableSelect
        , treeSelect = layui.treeSelect
        ;

    wootool.wooMoneyThod();
   // var updatedata = [];//修改的数据
    var contId = wooutil.getUrlVar('Id');
    if (contId === undefined)
        contId = 0;
    var updatedata = [];
    /***********************开标情况-begin***************************************************************************************************/

    table.render({
        elem: '#NF-contplanKbqk'
        , url: '/Tender/TenderInfo/GetKbqkList?contId=' + contId + '&rand=' + wooutil.getRandom()
        , toolbar: '#toolcontplanKbqk'
        , defaultToolbar: ['filter']
        , cols: [[
            { type: 'numbers', fixed: 'left' }
            , { type: 'checkbox', fixed: 'left' }
            , { field: 'Id', title: 'Id', width: 50, hide: true }
            , { field: 'OpenSituationName', title: '名称', fixed: 'left', edit: 'text' }
            //, { field: 'UnitName', title: '单位', fixed: 'left', edit: 'text', event: 'SelectPJname' }
            , { field: 'UnitName', title: '单位', fixed: 'left'/*, edit: 'text'*/, event: 'SelectPJname' }
            , { field: 'Unit', title: 'ID', width: 120, hide: true }
            , { field: 'TotalPricethis', title: '总价', width: 200, edit: 'text', class:"lhmoney" }
            , { field: 'Uitpricethis', title: '单价', width: 160, edit: 'text', class: "lhmoney" }
            , { field: 'UserName', title: '人员', width: 160, edit: 'text', event: 'SelectUSname' }
            , { field: 'UserId', hide: true}
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
            case "OpenSituationName"://名称
                {
                    newdata[filed] = obj.value;
                    obj.update(newdata);
                    obj.data.OpenSituationName = obj.value;
                }
                break;
            case "TotalPricethis"://总价
                {
                        newdata[filed] = obj.value;
                        obj.update(newdata);
                    obj.data.TotalPricethis = obj.value;
                }
                break;
            case "Uitpricethis"://单价
                {
                    newdata[filed] = obj.value;
                    obj.update(newdata);
                    obj.data.Uitpricethis = obj.value;
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
    var planQbqkEvent = {
        add: function () {
                        wooutil.OpenSubmitForm({
                            url: '/Tender/TenderInfo/SaveKbqk?contId=' + contId,
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
                    url: '/Tender/TenderInfo/SaveData?contId=' + contId,
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
            wooutil.deleteDatas({ tableId: 'NF-contplanKbqk', url: '/Tender/TenderInfo/Deletekb', nopage: true });
        },
        tooldel: function (obj) {
          
            /// <summary>列表操作栏-删除</summary>
            ///<param name='obj'>删除数据对象</param>
            wooutil.deleteInfo({ tableId: "NF-contplanKbqk", data: obj, url: '/Tender/TenderInfo/Deletekb', nopage: true });

        }, toolsava: function (obj) {
            $("#Zbdw").val(obj.data.Unit);//ZBDWID
            $("#Zje").val(obj.data.TotalPricethis);//ZJ
            admin.req({
                url: '/Tender/SuccessfulBidderLable/SaveBZData?contId=' + obj.data.Id,
                type: 'POST',
                done: function (res) {
                    //清空变量，防止重复提交
                    isupdate = false;
                    //return layer.msg('保存成功', { icon: 1 });
                    layer.msg("操作成功", { icon: 1, time: 500 }, function (msgindex) {
                        table.reload("NF-contplanZbdw", {
                                where: { rand: wooutil.getRandom() }
                            });
                    });
                   // wooutil.reloadTable({ msg: "保存成功", tableId: 'NF-contplanZbdw' });
                }
            });

        }
    };
    //招标头部
    table.on('toolbar(NF-contplanKbqk)', function (obj) {
        switch (obj.event) {
            case 'add':
                planQbqkEvent.add();
                break;
            case 'batchdel':
                planQbqkEvent.batchdel();
                break;
            case 'SaveAll':
                planQbqkEvent.SaveAll();

                break;
            case 'del':
                planQbqkEvent.tooldel(obj);
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
                planQbqkEvent.tooldel(obj);
                break;
            case 'Save':
                planQbqkEvent.toolsava(obj);
                break;
            case 'edit':
                planQbqkEvent.tooledit(obj);
                break;
            case "SelectPJname":
                {
                    var elem = obj;
                    var field = $(this).data('field');
                    var _url = '/Company/Supplier/GetList?selitem='+ true;
                    tableSelect.render({
                        elem: this.lastChild,
                        searchKey: 'keyWord',
                        searchPlaceholder: '关键词搜索',
                        table: {
                            url: _url + '&rand=' + wooutil.getRandom(),
                            cols: [[
                                { type: 'numbers', fixed: 'left' }
                                , { type: 'radio', fixed: 'left' }
                                , { field: 'Id', title: 'ID', width: 130 }//hide: true
                                , { field: 'Name', title: '名称', minWidth: 150, fixed: 'left' }
                                , { field: 'Code', title: '编号', width: 130 }
                                , { field: 'CompanyTypeClass', title: '类别', width: 120 }
                                , { field: 'FirstContact', width: 120, title: '主要联系人' }
                            ]]
                        },
                        done: function (elem, data) {
                            if (data.data.length !== 0) {
                                isupdate = true;
                                newdata[field] = data.data[0].Name;
                                newdata["Unit"] = data.data[0].Id;
                                obj.update(newdata);
                                for (var i = 0; i < updatedata.length; i++) {
                                    if (updatedata[i].Id === obj.data.Id) {
                                        updatedata.splice(i, 1);
                                    }
                                }
                                obj.data.OpenId = data.data[0].Id;
                                obj.data.UnitName = data.data[0].Name;
                                obj.data.Unit = data.data[0].Id;
                                updatedata.push(obj.data);
                            }
                        }
                    });
                    $(this.lastChild).trigger("click");

                }
                break;
            case "SelectUSname":
                {
                    var field = $(this).data('field');
                    var _url = '/System/UserInfor/GetList?ISQy=' + 1 + '&selitem=' + true;
                    tableSelect.render({
                        elem: this.lastChild,
                        searchKey: 'keyWord',
                        searchPlaceholder: '关键词搜索',
                        table: {
                            url: _url + '&rand=' + wooutil.getRandom(),
                            cols: [[
                                { type: 'numbers', fixed: 'left' }
                                , { type: 'radio', fixed: 'left' }
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
                            if (data.data.length !== 0) {
                                isupdate = true;
                                newdata[field] = data.data[0].Name;
                                newdata["UserId"] = data.data[0].Id;
                                obj.update(newdata);
                                for (var i = 0; i < updatedata.length; i++) {
                                    if (updatedata[i].Id === obj.data.Id) {
                                        updatedata.splice(i, 1);
                                    }
                                }
                                obj.data.SuccessUntiName = data.data[0].Name;
                                obj.data.SuccessUntiId = data.data[0].Id;
                                updatedata.push(obj.data);

                            }
                            //isupdate = true;
                            //newdata[field] = data.data[0].Name;
                            //newdata["UserId"] = data.data[0].Id;
                            //obj.update(newdata);
                            //for (var i = 0; i < updatedata.length; i++) {
                            //    if (updatedata[i].Id === obj.data.Id) {
                            //        updatedata.splice(i, 1);
                            //    }
                            //}
                            //obj.data.UserName = data.data[0].Name;
                            //obj.data.UserId = data.data[0].Id;
                            //updatedata.push(obj.data);

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
    /***********************招标人-begin***************************************************************************************************/
    table.render({
        elem: '#NF-contplanZbr'
        , url: '/Tender/TendererNameLabel/GetKbqkList?contId=' + contId + '&rand=' + wooutil.getRandom()
        , toolbar: '#toolcontplanZbr'
        , defaultToolbar: ['filter']
        ,cols: [[
            { type: 'numbers', fixed: 'left' }
            , { type: 'checkbox', fixed: 'left' }
            , { field: 'Id', title: 'Id', width: 50, hide: true }
            , { field: 'TeNameLabel', title: '名称', width: 200, edit: 'text', fixed: 'left' }
            , { field: 'Psition', title: '职位', edit: 'text', width: 200 }
            , { field: 'UserName', title: '招标人', width: 200, event: 'SelectzbUSname' }
            , { field: 'UserId', title: 'Id', width: 50, hide: true }
            , { field: 'TeDartmentName', title: '部门', width: 200, event:'SelectzbPJname'}
            , { field: 'TeDartment', title: 'Id', width: 50, hide: true }
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
    var planZBREvent = {
        add: function () {
            wooutil.OpenSubmitForm({
                url: '/Tender/TendererNameLabel/SaveKbr?contId=' + contId,
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
            wooutil.deleteDatas({ tableId: 'NF-contplanZbr', url: '/Tender/TendererNameLabel/Deletekb', nopage: true });
           // wooutil.deleteDatas({ tableId: 'NF-contplanZbr', url: '/Tender/TendererNameLabel/Deletekb', nopage: true });
        },
        tooldel: function (obj) {
            /// <summary>列表操作栏-删除</summary>
            ///<param name='obj'>删除数据对象</param>
            wooutil.deleteInfo({ tableId: "NF-contplanZbr", data: obj, url: '/Tender/TendererNameLabel/Deletekb', nopage: true });
        }
    };
    //合同计划资金头部工具栏
    table.on('toolbar(NF-contplanZbr)', function (obj) {
        switch (obj.event) {
            case 'add':
                planZBREvent.add();
                break;
            case 'batchdel':
                planZBREvent.batchdel();
                break;
            case 'SaveAll':
                planZBREvent.SaveAll();
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
                planZBREvent.tooldel(obj);
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
                                var Conid= ContractEnforId.val();
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
                    var _url = '/System/UserInfor/GetList?ISQy=' + 1 + '&selitem=' + true;
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
                layer.alert("暂不支持（" + obj.event + "）");
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
            , { field: 'SuccessUntiName', title: '开标情况单位', width: 200, edit: 'text',event: 'SelectzbPJname'}
            , { field: 'SuccessUntiId', title: 'SuccessUntiId', width: 50, hide: true }
            , { field: 'SuccTotalPricethis', title: '中标总价', edit: 'text', width: 200 }
            , { field: 'SuccUitpricethis', title: '中标单价', edit: 'text', width: 200 }
            , { field: 'Zbdwid', title: '中标单位', edit: 'text', width: 200,hide: true }
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
               
                planZbdwEvent.tooldel(obj);
                break;
            case "SelectzbPJname":
                {
                    var field = $(this).data('field');
                    var _url = '/Tender/SuccessfulBidderLable/GetZbList?contId=' + contId+'&selitem=' + true;
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
                    });
                    $(this.lastChild).trigger("click");
                }
                break;
            case "SelectzbUSname":
                {
                    var field = $(this).data('field');
                    var _url = '/System/UserInfor/GetList?ISQy=' + 1 + '&selitem=' + true;
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
    /***********************数据导入-begin***************************************************************************************************/

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
            , { title: '操作', width: 150, align: 'center', fixed: 'right', toolbar: '#table-contplanfinacebar' }
        ]]
        , page: false
        , loading: true
        , height: setter.table.height_tab
        , limit: setter.table.limit_tab
    });
    var attacCargoEvent = {
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
                , content: '/Tender/WinningItems/Build'
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
                        // table.reload('/Inquiry/Winning/ShowView', options)
                        table.reload('NF-Cargo', {
                            url: '/Tender/WinningItems/ShowView1',//+ '&rand=' + wooutil.getRandom(),
                        });
                        return false;
                    });
                    submit.trigger('click');
                },
                success: function (layero, index) {
                  //  layer.close(param.index);
                }, SaveAll: function () {
                    var isupdate = true;
                    if (isupdate) {
                        admin.req({
                            url: '/Tender/Winning/SaveData?contId=' + contId,
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
            });
        },
        batchdel: function () {
            /// <summary>列表头部-批量删除</summary>
            wooutil.deleteDatas({ tableId: 'NF-Cargo', table: table, url: '/Tender/WinningItems/Delete', nopage: true });
        },
        tooldownload: function (obj) {
            wooutil.download({
                Id: obj.data.Id,
                folder: 12//标识合同附件
            });
        },
        tooldel: function (obj) {
            /// <summary>列表操作栏-删除</summary>
            ///<param name='obj'>删除数据对象</param>
            wooutil.deleteInfo({ tableId: "NF-Cargo", data: obj, url: '/Tender/WinningItems/Delete', nopage: true });
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
                attacCargoEvent.add();
                break;
            case 'batchdel':
                attacCargoEvent.batchdel();
                break;
            case 'SaveAll':
                attachmentEvent.SaveAll();
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
                attacCargoEvent.tooldel(obj);
                break;
            case 'edit':
                planfinanceEvent.tooledit(obj);
                break;
            default:
                layer.alert("暂不支持（" + obj.event + "）");
                break;
        }
    });

/***********************数据导入-end***************************************************************************************************/

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
            , { field: 'CreateDateTime', title: '上传日期', width: 120 }
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
                , area: ['800px', '80%']
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
                , area: ['800px', '80%']
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
    /****************************选择表格注册区域-合同对方、项目、签约主体等选择-begin**************************************************************/
    layui.use(['selectnfitem', 'tableSelect'], function () {
        var tableSelect = layui.tableSelect
            , selectnfitem = layui.selectnfitem;
        //负责人
        selectnfitem.selectUserItemQ(
            {
                tableSelect: tableSelect,
                elem: '#RecorderName',
                hide_elem: '#RecorderId'

            });
        //项目
        selectnfitem.selectProjZBItem(
            {
                tableSelect: tableSelect,
                elem: '#ProjectName',
                hide_elem: '#ProjectId',
                hide_noelem:'#ProjectNO',
                selitem: true
            });
    });
    /****************************选择表格注册区域-合同对方、项目、签约主体等选择-end**************************************************************/

    /*****************************日期、导航、字典注册-begin************************************************************/
    layui.use(['laydate', 'nfcontents', 'commonnf', 'treeSelect'], function () {
        var laydate = layui.laydate
            , nfcontents = layui.nfcontents
            , commonnf = layui.commonnf
            , treeSelect = layui.treeSelect;
        laydate.render({ elem: '#TenderDate', trigger: 'click' });//时间
        laydate.render({ elem: '#TenderExpirationDate',trigger: 'click' });//有效期
        //合同类别
        commonnf.getdatadic({ dataenum: 33, selectEl: "#TenderType" });
        //目录
        nfcontents.render({ content: '#custoZBmernva' });
        /**
       *合同执行部门
         **/
        function InitenforTree(tval) {
            treeSelect.render(
                {
                    elem: "#ContractEnforcementDepId",
                    data: '/System/Department/GetTreeSelectDept',
                    method: "GET",
                   
                    verify: true,
                    click: function (d) {
                        $("input[name=ContractEnforcementDepId]").val(d.current.id);
                    },
                    success: function (d) {
                        if (tval != null) {
                            treeSelect.checkNode("ContractEnforcementDepId", tval);
                        }


                    }

                });
        }
     
        /**
        *经办机构
        **/
        function InitDeptTree(tval) {
            treeSelect.render(
                {
                    elem: "#NF-PDept",
                    data: '/System/Department/GetTreeSelectDept',
                    method: "GET",
                   
                    verify: true,
                    click: function (d) {
                        $("input[name=TenderUserId]").val(d.current.id);
                    },
                    success: function (d) {
                        if (tval != null) {
                            treeSelect.checkNode("NF-PDept", tval);
                        }


                    }

                });
        }



        /**
        *修改
        **/
        if (contId !== "" && contId !== undefined && contId !== 0) {
            admin.req({
                url: '/Tender/TenderInfo/ShowView',
                data: { Id: contId, rand: wooutil.getRandom() },
                done: function (res) {
                    form.val("NF-TenderInfo-Form", res.Data);
                    //SetValueHand(res.Data);
                    //经办机构
                    InitenforTree(res.Data.ContractEnforcementDepId);
                    InitDeptTree(res.Data.TenderUserId);
                }
            });
        } else {//新建时
          //  InitenforTree(res.Data.ContractEnforcementDepId);
            InitenforTree(null);
            InitDeptTree(null);
        }
        //清除部分下拉小笔头
        wooutil.selpen();
    });
    /*****************************日期、导航、字典注册-end************************************************************/

    //标的
    subMetBuild.render({ contId: contId });

    if (setter.sysinfo.Mb !== "Mb") {
        $(".mb").addClass("layui-hide");
    }

    exports('CollectionContractBuild', {});
});