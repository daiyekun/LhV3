/**
 @Name：实际收款
 @Author：dyk 
 */
layui.define(['table', 'form', 'actfinaniceutility', 'dynamicCondition', 'soulTable'], function (exports) {
    var $ = layui.$
   , table = layui.table
   , setter = layui.setter
   , admin = layui.admin
   , form = layui.form
        , actfinaniceutility = layui.actfinaniceutility
        , dynamicCondition = layui.dynamicCondition
        , soulTable = layui.soulTable;
    var logdindex = layer.load(0, { shade: false });
    var searchType = wooutil.getUrlVar('seaType');
    var contrId = wooutil.getUrlVar('contrId');
    var _reqUrl = '/Finance/ContActualFinance/GetList?fType=0';
    if (searchType != undefined && searchType === "1") {
        _reqUrl = _reqUrl + "&search=" + searchType;
    }
    if (contrId != null && contrId!="") {
        _reqUrl = _reqUrl + "&otherwh=" + contrId;
    }
    _reqUrl = _reqUrl + "&rand=" + wooutil.getRandom();
    //列表
    table.render({
        elem: '#NF-ActualFinanceColl-Index'
       , url: _reqUrl
       , toolbar: '#toolactfinance'
       , defaultToolbar: ['filter']
        , cellMinWidth: 80
        , overflow: {
            type: 'tips'//内容超过设置
            , color: 'black' // 字体颜色
            , bgColor: 'white' // 背景色
        }
        , totalRow: true
       , cols: [[
             { type: 'numbers', fixed: 'left' }
           , { type: 'checkbox', fixed: 'left', totalRowText: '合计' }
           , { field: 'AmountMoneyThod', title: '实际金额', width: 130, sort: true, templet: '#nameTpl', fixed: 'left', filter: true}
           , { field: 'ContName', title: '合同名称', width: 260, templet: '#contnameTpl', fixed: 'left', filter: true }
           , { field: 'ContCode', title: '合同编号', width: 150, sort: true, fixed: 'left', filter: true }
           , { field: 'AmountMoney', title: '实际金额', hide: true, width: 130, filter: true}
           , { field: 'ContCategoryName', title: '合同类别', width: 140, filter: true}
           , { field: 'CompCategoryName', title: '客户类别', width: 140, filter: true}
           , { field: 'CompName', title: '客户名称', width: 150, templet: '#compnameTpl', filter: true }
           , { field: 'ActualSettlementDate', title: '结算日期', width: 130, sort: true, filter: true }
           , { field: 'VoucherNo', title: '票据码', width: 130, hide: true, filter: true}
           , { field: 'AstateDic', title: '资金状态', width: 130, templet: '#stateTpl', filter: true }
           , { field: 'SettlementMethodDic', title: '结算方式', width: 140, hide: true, filter: true}
           , { field: 'DeptName', title: '经办机构', width: 140, hide: true, filter: true}
           , { field: 'ConfirmUserName', title: '确认人', width: 120, filter: true }
           , { field: 'ConfirmDateTime', title: '确认时间', width: 120, filter: true}
           , { field: 'CreateUserName', title: '建立人', width: 120, hide: true, filter: true}
           , { field: 'CreateDateTime', title: '建立日期', width: 120, sort: true, hide: true, filter: true }
           , { field: 'Remark', title: '备注', width: 140, hide: true, filter: true}
           , { field: 'Reserve1', title: '备用1', width: 130, filter: true}
           , { field: 'Reserve2', title: '备用2', width: 130, filter: true}
           , { field: 'Id', title: 'Id', width: 50, hide: true }
           , { title: '操作', width: 150, align: 'center', fixed: 'right', toolbar: '#table-actfinance-bar' }
       ]]
       , page: true
       , loading: true
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
           var AmountMoney = 0;
           for (var i = 0; i < res.data.length; i++) {
               AmountMoney += res.data[i].AmountMoney
           }

            this.elem.next().find('.layui-table-total td[data-field="AmountMoneyThod"] .layui-table-cell').text(AmountMoney.toLocaleString('zh', { style: 'currency', currency: 'CNY' }));
            this.elem.next().find('.layui-table-total td[data-field="AmountMoneyThod"] .layui-table-cell').css("color", "red");
           $("input[name=keyWord]").val($("input[name=hide_keyWord]").val());
            $("input[name=hide_keyWord]").val("");
            var cate = layui.data(setter.tableName).userName;
            actfinaniceutility.stateEvent({ tableId: 'NF-ActualFinanceColl-Index' }, cate);//注册状态流转事件

       }

    });
    //监听表格排序
    table.on('sort(NF-ActualFinanceColl-Index)', function (obj) {
        table.reload('NF-ActualFinanceColl-Index', { //testTable是表格容器id
            initSort: obj //记录初始排序，如果不设的话，将无法标记表头的排序状态。 layui 2.1.1 新增参数
            , where: { //请求参数（注意：这里面的参数可任意定义，并非下面固定的格式）
                orderField: obj.field //排序字段
            , orderType: obj.type //排序方式
            , keyWord: $("input[name=keyWord]").val()//查询关键字
            }
            , page: { curr: 1 }//重新从第 1 页开始
        });


    });

    /***********************************************监听头部工具栏---begin*****************************************************/

    var opencontract = function () {
        layer.open({
            type: 2
            , title: '选择合同'
            , content: '/NfCommon/SelectItem/SelectContract?selType=2'
            , maxmin: true
            , area: ['75%', '500px']
           // , btn: ['确定', '取消']
            , btnAlign: 'c'
            , skin: "layer-ext-myskin"
            , success: function (layero, index) {
                // layer.full(index);
                layero.find('iframe').contents().find('#currIndex').val(index);
                wooutil.openTip();
            }
        });
    };
    var active = {
        add: function () { opencontract() }
        , batchdel: function () {//删除
            wooutil.deleteDatas({ tableId: 'NF-ActualFinanceColl-Index', table: table, url: '/Finance/ContActualFinance/Delete' });
        },
        search: function () {//查询
            $("input[name=hide_keyWord]").val($("input[name=keyWord]").val());
            table.reload('NF-ActualFinanceColl-Index', {
                page: { curr: 1 }
                , where: {
                    keyWord: $("input[name=keyWord]").val()

                }
            });

        },
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
                    table.reload("NF-ActualFinanceColl-Index", {
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

    table.on('toolbar(NF-ActualFinanceColl-Index)', function (obj) {
        switch (obj.event) {
            case 'add':
                active.add();
                break;
            case 'batchdel':
                var cate = layui.data(setter.tableName).userName;
                if (cate =="SuperAdministrator") {
                    layer.alert("超级管理员不能批量删除");
                } else {
  active.batchdel();
                }
              
                break;
            case 'search':
                active.search();
                break;
            case 'stateChange'://状态流转
                {
                    
                    actfinaniceutility.updateSate({
                    tableId: 'NF-ActualFinanceColl-Index'
                    , url: '/Finance/ContActualFinance/UpdateField'
                    , evtobj: this
                    , funcode: 'confirmOrBackActFinanceColl'
                   
                });
                }
                break;
            case "exportexcel":
                wooutil.exportexcel(obj, {
                    url: "/Finance/ContActualFinance/ExportExcel?FType=0",
                    keyword: $("input[name=keyWord]").val()
                });
                break;
            case 'clear'://状态流转
                soulTable.clearCache("NF-ActualFinanceColl-Index")
                layer.msg('已还原！', { icon: 1, time: 1000 })
                break;
            case "advQuery"://高级查询
                active.advQuery();
                break;


        };
    });
    /***********************************************监听头部工具栏---end**************************************************************/

    /**********************************************************监听工具条-begin*********************************************************/


    /**
    *打开查看页面
    **/
    function openview(obj) {
        layer.open({
            type: 2
                , title: '查看详情'
                , content: '/Finance/ContActualFinance/ActualFinanceCollDetail?Id=' + obj.data.Id + "&contId=" + obj.data.ContId + "&rand=" + wooutil.getRandom()
                , maxmin: true
                , area: ['60%', '80%']
                , btnAlign: 'c'
                , skin: "layer-nf-nfskin"
                , success: function (layero, index) {
                    layer.load(0, { shade: false, time: 500 });
                    layer.full(index);
                    wooutil.openTip();
                }
        });
    };
    /**
    *实际资金查看权限
    */
    function getViewPremission(obj) {
        return wooutil.requestpremission({
            url: '/NfCommon/NfPermission/ViewPermission?rand=' + wooutil.getRandom(),
            data: {
                FuncCode: 'querycollcontview',
                ObjId: obj.data.ContId
            }

        });
    }
    /**
    *编辑权限
    **/
    function getUpdatePermission(funcode, obj) {
        return wooutil.requestpremission({
            url: '/NfCommon/NfPermission/UpdatePermission?rand=' + wooutil.getRandom(),
            data: {
                FuncCode: funcode,
                ObjHtId: obj.data.ContId,
                ObjId: obj.data.Id
            }
        });
    }
    /**
    *修改
    **/
    function editFunc(obj, _success) {
        layer.open({
            type: 2
                , title: '修改实际收款'
                , content: '/Finance/ContActualFinance/ActualFinanceCollBuild?Id=' + obj.data.Id + "&contId=" + obj.data.ContId + "&rand=" + wooutil.getRandom()
                , maxmin: true
                , area: ['60%', '80%']
            //, btnAlign: 'c'
              , success: function (layero, index) {
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

    table.on('tool(NF-ActualFinanceColl-Index)', function (obj) {
        switch (obj.event) {
            case "del":
                {
                    var cate = layui.data(setter.tableName).userName;
                    if (obj.data.Astate === 0 || cate =="SuperAdministrator") {
                        wooutil.deleteInfo({ tableId: "NF-ActualFinanceColl-Index", data: obj, url: '/Finance/ContActualFinance/Delete' });
                    } else {
                        layer.alert("只有未提交的才能删除！");
                        return false;
                    }
                }
                break;
            case "edit":
                {
                    var cate = layui.data(setter.tableName).userName;
                    if (obj.data.Astate === 0 || obj.data.Astate == 3) {//未提交Or被打回
                        var ress = getUpdatePermission("addOrUpdateActFinanceColl", obj);
                        if (ress.RetValue == 0) {
                            editFunc(obj, null);
                        } else {
                            return layer.alert("无权限");
                        }
                    } else if (cate == "SuperAdministrator") {
                        editFunc(obj, null);
                    }
                    else {
                        layer.alert("只有未提交与被打回的实际资金才允许修改");
                        return false;
                    }

                }
                break;
            case "detail"://详情
                {
                    var ress = getViewPremission(obj);
                    if (ress.RetValue == 0) {
                        openview(obj);
                    } else {
                        return layer.alert(ress.Msg);
                    }
                }
                break;
            case "contdetail":
                {
                    var ress = getViewPremission(obj);
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

        }

    });
/*********************************************************监听工具栏-end***************************************************************************/
    //解决高级查询选择下拉影响页面错乱问题
    //合同类别
    //form.on('select(ContTypeId)', function (data) {
    //    $(document.body).trigger("click");
    //});
    ////合同对方类别
    //form.on('select(CompClassId)', function (data) {
    //    $(document.body).trigger("click");
    //});
    ////资金状态
    //form.on('select(selectZt)', function (data) {
    //    $(document.body).trigger("click");
    //});
    ////结算方式
    //form.on('select(SettlementMethod)', function (data) {
    //    $(document.body).trigger("click");
    //});


    exports('actualFinanceCollIndex', {});
});