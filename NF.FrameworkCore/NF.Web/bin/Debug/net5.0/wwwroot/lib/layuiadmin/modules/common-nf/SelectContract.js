/**
*选择合同
**/
layui.define(['table', 'form'], function (exports) {
    var $ = layui.$
        , table = layui.table
        , setter = layui.setter
        , admin = layui.admin;
    //0:收票、1：开票、2：实际收款、3：实际付款
    var selType = wooutil.getUrlVar('selType');
    $("#selType").val(selType);
    var logdindex = layer.load(0, { shade: false });
    //列表
    table.render({
        elem: '#NF-SelectContract-Index'
        , url: '/NfCommon/SelectItem/GetList?selType=' + selType + '&rand=' + wooutil.getRandom()
        , toolbar: '#toolcontract'
        , defaultToolbar: []
        , cellMinWidth: 80
        , cols: [[
            { type: 'numbers', fixed: 'left' },
            { type: 'radio', fixed: 'left' },
            , { field: 'Id', title: 'Id', width: 50, hide: true }
            , { field: 'Name', title: '名称', width: 200, fixed: 'left' }
            , { field: 'Code', title: '编号', width: 150 }
            , { field: 'ContTypeName', title: '类别', width: 140 }
            , { field: 'ContPro', title: '合同属性', width: 130 }
            , { field: 'CompName', title: '合同对方', width: 140 }
            , { field: 'BankName', title: '开户行', width: 140 }
            , { field: 'BankAccount', title: '账号', width: 140 }
            , { field: 'ContAmThod', title: '合同金额', width: 130 }
            , { field: 'DeptName', title: '经办机构', width: 130 }
            , { field: 'ProjName', title: '所属项目', width: 130, hide: true }
            , { field: 'CreateUserName', title: '建立人', width: 130, hide: true }
            , { field: 'CreateDateTime', title: '建立日期', width: 130, hide: true }
            , { field: 'MdeptName', title: '签约主体', width: 130, hide: true }
            , { field: 'PrincUserName', title: '负责人', width: 130, hide: true }
            , { field: 'ContStateDic', title: '状态', width: 120, templet: '#contractstateTpl', unresize: true }


        ]]
        , page: true
        , loading: true
        , height: '450px'//setter.table.height_tab
        , limit: setter.table.limit
        , limits: setter.table.limits
        , done: function (res, curr, count) {   //返回数据执行回调函数
            layer.close(logdindex);    //返回数据关闭loading
            $("input[name=keyWord]").val($("input[name=hide_keyWord]").val());
            $("input[name=hide_keyWord]").val("");

        }

    });
    var event = {
        search: function () {//查询
            $("input[name=hide_keyWord]").val($("input[name=keyWord]").val());
            table.reload('NF-SelectContract-Index', {
                page: { curr: 1 }
                , where: {
                    keyWord: $("input[name=keyWord]").val()

                }
            });

        },
        iframeSrc: function (obj) {
            /// <summary>页面跳转</summary>
            parent.layer.iframeSrc(obj.index, obj.url);
            parent.layer.title(obj.title, obj.index);
            parent.layer.full(obj.index);
        },
        createInvoice: function (obj) {
            /// <summary>建立收票</summary>
            var ress = wooutil.requestpremission({
                url: '/NfCommon/NfPermission/ViewPermission?rand=' + wooutil.getRandom(),
                data: {
                    FuncCode: 'addOrUpdateInvoice',
                    ObjHtId: obj.contId
                }

            });
            if (ress.RetValue == 0) {
                event.iframeSrc(obj);
            } else {
                layer.alert("无权限！");

            }

        },
        createInvoiceOut: function (obj) {
            /// <summary>建立开票</summary>
            var ress = wooutil.requestpremission({
                url: '/NfCommon/NfPermission/ViewPermission?rand=' + wooutil.getRandom(),
                data: {
                    FuncCode: 'addOrUpdateInvoiceOut',
                    ObjHtId: obj.contId
                }

            });
            if (ress.RetValue == 0) {
                event.iframeSrc(obj);
            } else {
                layer.alert("无权限！");

            }

        },
        createActFinanceColl: function (obj) {
            /// <summary>建立实际收款</summary>
            var ress = wooutil.requestpremission({
                url: '/NfCommon/NfPermission/ViewPermission?rand=' + wooutil.getRandom(),
                data: {
                    FuncCode: 'addOrUpdateActFinanceColl',
                    ObjHtId: obj.contId
                }

            });
            if (ress.RetValue == 0) {
                event.iframeSrc(obj);
            } else {
                layer.alert("无权限！");

            }

        },
        createActFinancePay: function (obj) {

            /// <summary>建立实际付款</summary>
            var ress = wooutil.requestpremission({
                url: '/NfCommon/NfPermission/ViewPermission?rand=' + wooutil.getRandom(),
                data: {
                    FuncCode: 'addOrUpdateActFinancePay',
                    ObjHtId: obj.contId
                }

            });
            if (ress.RetValue == 0) {
                event.iframeSrc(obj);
            } else {
                layer.alert("无权限！");

            }

        }

    };
    table.on('toolbar(NF-SelectContract-Index)', function (obj) {
        switch (obj.event) {
            case 'search':
                event.search();
                break;

        }
    });
    //table.on('row(test)', function (obj) {
    table.on('row(NF-SelectContract-Index)', function (obj) {
        var _index = $("#currIndex").val();
        switch (selType) {
            case 0://收票
            case "0":
                event.createInvoice({ index: _index, url: '/Finance/ContInvoice/BuildInvoice?contId=' + obj.data.Id, title: '建立发票', contId: obj.data.Id });
                break;
            case 1://开票
            case "1":
                event.createInvoiceOut({ index: _index, url: '/Finance/ContInvoice/BuildInvoiceOut?contId=' + obj.data.Id + '&compId=' + obj.data.CompId, title: '建立发票', contId: obj.data.Id });
                break;
            case 2:
            case "2"://实际收款
                event.createActFinanceColl({ index: _index, url: '/Finance/ContActualFinance/ActualFinanceCollBuild?contId=' + obj.data.Id + "&cyId=" + obj.data.CurrencyId + "&rate=" + obj.data.Rate, title: '建立收款', contId: obj.data.Id });
                break;
            case 3:
            case "3"://实际付款
                event.createActFinancePay({ index: _index, url: '/Finance/ContActualFinance/ActualFinancePayBuild?contId=' + obj.data.Id + "&cyId=" + obj.data.CurrencyId + "&rate=" + obj.data.Rate + "&yhName=" + encodeURI(obj.data.BankName) + "&yhzh=" + obj.data.BankAccount, title: '建立付款', contId: obj.data.Id });
                break;
            default:
                layer.alert('未知类型>>selType:' + selType);
                break;

        }



    });

    exports('SelectContract', {});
});