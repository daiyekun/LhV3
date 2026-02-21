/**
 @Name：计划收款
 @Author：dyk 
 */
layui.define(['table', 'form', 'dynamicCondition', 'soulTable'], function (exports) {
    var $ = layui.$
   , table = layui.table
   , setter = layui.setter
   , admin = layui.admin
        , form = layui.form
        , soulTable = layui.soulTable
        , dynamicCondition = layui.dynamicCondition;
    var logdindex = layer.load(0, { shade: false });
    var searchType = wooutil.getUrlVar('seaType');
    var _reqUrl = '/Finance/ContPlanFinance/GetMainList?requestType=0';
    if (searchType != undefined && searchType==="1") {
        _reqUrl =_reqUrl+ "&search=" + searchType;
    }
    _reqUrl=_reqUrl+"&rand=" + wooutil.getRandom();
   
    //列表
    table.render({
        elem: '#NF-PlanFinanceColl-Index'
       , url:_reqUrl
       , toolbar: '#toolplanfinancecell'
       , defaultToolbar: ['filter']
        , cellMinWidth: 80
        , overflow: {
            type: 'tips'//内容超过设置
            , color: 'black' // 字体颜色
            , bgColor: 'white' // 背景色
        }
       , cols: [[
           { type: 'numbers', fixed: 'left' }
           ,{ type: 'checkbox', fixed: 'left' }
           , { field: 'Id', title: 'Id', width: 50, hide: true }
           , { field: 'Name', title: '名称', width: 150, fixed: 'left', filter: true, totalRowText: '合计' }
           , { field: 'ContName', title: '合同名称', width: 200, templet: '#contnameTpl', filter: true, fixed: 'left' }
           , { field: 'ContCode', title: '合同编号', width: 150, sort: true, filter: true }
           , { field: 'ContCategoryName', title: '合同类别', width: 140 }
           , { field: 'CompName', title: '客户名称', width: 150, templet: '#compnameTpl' }
           , { field: 'AmountMoneyThod', title: '计划收款金额', width: 140, filter: true, }
           , { field: 'CompAmountThod', title: '已完成金额', width: 130, filter: true,}
           , { field: 'BalanceThod', title: '余额', width: 130, sort: true, filter: true,}
           , { field: 'ContActBl', title: '完成比例', width: 130 }
           , { field: 'PlanCompleteDateTime', title: '计划完成日期', width: 130, hide: true }
           , { field: 'CompTypeName', title: '客户类别', width: 130 }
           , { field: 'DeptName', title: '经办机构', width: 130, hide: true }
           , { field: 'CurrencyName', title: '币种', width: 130 }
           , { field: 'SettlModelName', title: '结算方式', width: 130 }
           , { field: 'Remark', title: '备注', width: 130 }
           , { field: 'ProjectName', title: '所属项目', width: 150, templet: '#projnameTpl', hide: true }
           , { field: 'ActSettlementDate', title: '实际完成日期', width: 130, hide: true }
           , { field: 'PrincipalUserName', title: '负责人', width: 130, hide: true }
           , { field: 'Id', title: 'Id', width: 100, hide: true }
       ]]
       , page: true
        , loading: true
        , totalRow: true
       , height: setter.table.height_4
       , limit: setter.table.limit
        , limits: setter.table.limits
        , filter: {
            //列表服务器缓存
            cache: true
            , bottom: false
        }
        , done: function (res, curr, count) {   //返回数据执行回调函数
            soulTable.render(this)
           layer.close(logdindex);    //返回数据关闭loading
           $("input[name=keyWord]").val($("input[name=hide_keyWord]").val());
            $("input[name=hide_keyWord]").val("");

            var AmountMoney = 0;//计划资金金额
            var CompAmount = 0;//已完成金额
            var Balance = 0;//余额
            
            for (var i = 0; i < res.data.length; i++) {
               
                AmountMoney += res.data[i].AmountMoney;
                CompAmount += res.data[i].CompAmount;
                Balance += res.data[i].Balance;
               
            }
            this.elem.next().find('.layui-table-total td[data-field="AmountMoneyThod"] .layui-table-cell').text(AmountMoney.toLocaleString('zh', { style: 'currency', currency: 'CNY' }));
            this.elem.next().find('.layui-table-total td[data-field="AmountMoneyThod"] .layui-table-cell').css("color", "red");

            this.elem.next().find('.layui-table-total td[data-field="CompAmountThod"] .layui-table-cell').text(CompAmount.toLocaleString('zh', { style: 'currency', currency: 'CNY' }));
            this.elem.next().find('.layui-table-total td[data-field="CompAmountThod"] .layui-table-cell').css("color", "red");

            this.elem.next().find('.layui-table-total td[data-field="BalanceThod"] .layui-table-cell').text(Balance.toLocaleString('zh', { style: 'currency', currency: 'CNY' }));
            this.elem.next().find('.layui-table-total td[data-field="BalanceThod"] .layui-table-cell').css("color", "red");

       }

    });
    //清除列表缓存
    $('#clear').on('click', function () {
        soulTable.clearCache("NF-PlanFinanceColl-Index")
        layer.msg('已还原！', { icon: 1, time: 1000 })
    })
    //监听表格排序
    table.on('sort(NF-PlanFinanceColl-Index)', function (obj) {
        table.reload('NF-PlanFinanceColl-Index', { //testTable是表格容器id
            initSort: obj //记录初始排序，如果不设的话，将无法标记表头的排序状态。 layui 2.1.1 新增参数
            , where: { //请求参数（注意：这里面的参数可任意定义，并非下面固定的格式）
                orderField: obj.field //排序字段
            , orderType: obj.type //排序方式
            , keyWord: $("input[name=keyWord]").val()//查询关键字
            }
            , page: { curr: 1 }//重新从第 1 页开始
        });
    });
    var active = {
        search: function () {//查询
            $("input[name=hide_keyWord]").val($("input[name=keyWord]").val());
            table.reload('NF-PlanFinanceColl-Index', {
                page: { curr: 1 }
                , where: {
                    keyWord: $("input[name=keyWord]").val()

                }
            });

        }
    };

    table.on('toolbar(NF-PlanFinanceColl-Index)', function (obj) {
        switch (obj.event) {
            case 'search':
                active.search();
                break;
            case "exportexcel":
                wooutil.exportexcel(obj, {
                    url: "/Finance/ContPlanFinance/ExportExcel?FType=0",
                    keyword: $("input[name=keyWord]").val()
                });
                break;
            case 'clear'://状态流转
                soulTable.clearCache("NF-PlanFinanceColl-Index")
                layer.msg('已还原！', { icon: 1, time: 1000 })
                break;


        };
    });
    table.on('tool(NF-PlanFinanceColl-Index)', function (obj) {
        switch (obj.event) {
            case "contdetail":
                {
                    var ress = wooutil.requestpremission({
                        url: '/NfCommon/NfPermission/ViewPermission?rand=' + wooutil.getRandom(),
                        data: {
                            FuncCode: 'querycollcontview',
                            ObjId: obj.data.ContId
                        }

                    });
                    if (ress.RetValue == 0) {
                        wooutil.opencontview(obj);
                    } else {
                        return layer.alert(ress.Msg);
                    }

                }
                break;
            case "compdetail"://合同对方
                {
                var ress = wooutil.requestpremission({
                    url: '/NfCommon/NfPermission/ViewPermission?rand=' + wooutil.getRandom(),
                    data: {
                        FuncCode: 'querycustomerview',
                        ObjId: obj.data.CompId
                    }
                });
                if (ress.RetValue == 0) {
                    wooutil.opencompview(obj);
                } else {
                    return layer.alert(ress.Msg);
                }
                }
                break;
            case "projdetail"://项目
                {
                    var ress = wooutil.requestpremission({
                        url: '/NfCommon/NfPermission/ViewPermission?rand=' + wooutil.getRandom(),
                        data: {
                            FuncCode: 'queryprojectview',
                            ObjId: obj.data.ProjId
                        }

                    });
                    if (ress.RetValue == 0) {
                        wooutil.openprojview(obj);
                    } else {
                        return layer.alert(ress.Msg);
                    }
                }
                break;

         }
        
         
    });

    exports('planFinanCecollIndex', {});
    });