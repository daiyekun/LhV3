/**
 @Name：收款合同列表
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
    var _reqUrl = "/CaseManagement/Jfsb/GetList?rand=" + wooutil.getRandom();
    if (searchType != undefined && searchType === "1") {
        _reqUrl = _reqUrl + "&search=" + searchType;
    }
    var $htcols = [
        { type: 'numbers', fixed: 'left' }
        , { type: 'checkbox', fixed: 'left' }
        , { field: 'Name', title: '名称', width: 240,  fixed: 'left', filter: true, sort: true, totalRowText: '合计' }
        , { field: 'Code', title: '编号', width: 130, filter: true, sort: true }
        , { field: 'Title', title: '标题', width: 140, templet: '#compTpl', filter: true}
        , { field: 'UrgentName', title: '紧急程度', width: 130, filter: true }
        , { field: 'CompName', title: '经办单位', width: 140, filter: true }
        , { field: 'HandlerIdName', title: '经办人', filter: true, sort: true, width: 130 }
        , { field: 'HandDate', title: '经办日期', width: 130, filter: true }
        , { field: 'ContId', title: '相关合同', width: 140, filter: true }
        , { field: 'DisputeName', title: '纠纷类别', width: 140, filter: true }
        , { field: 'Amount', title: '涉及金额(万元)', width: 140, filter: true }
        , { field: 'CompInfo', title: '向对方情况', width: 140, filter: true }
        , { field: 'Remark', title: '纠纷简述及建议', width: 130, filter: true  }
        , { field: 'CName', title: '新建人', width: 130, filter: true }
        , { field: 'CreateDateTime', title: '新建日期', width: 130, filter: true }
       
        , { title: '操作', width: 150, align: 'center', fixed: 'right', toolbar: '#table-contract-bar' }
    ];
    /**删除审批列**/
    appflowutility.SeCols({ htcols: $htcols });

    //列表
    table.render({
        elem: '#NF-Jfgl-Index'
        , url: _reqUrl
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
                //{
                //    name: '查询实际收款',
                //    click: function (obj) {
                //        var contrId = obj.row.Id;
                //        parent.layui.index.openTabsPage("/Finance/ContActualFinance/ActualFinanceCollIndex?contrId=" + contrId, "查询实际收款");
                //    }
                //}
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
            layer.close(logdindex);    //返回数据关闭loading
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
            $("input[name=keyWord]").val($("input[name=hide_keyWord]").val());
            $("input[name=hide_keyWord]").val("");
            var cate = layui.data(setter.tableName).userName;
            contractutility.stateEvent({ tableId: 'NF-Jfgl-Index' }, cate);//注册状态流转事件

        }

    });

    //监听表格排序
    table.on('sort(NF-Jfgl-Index)', function (obj) {
        table.reload('NF-Jfgl-Index', { //testTable是表格容器id
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
    var openAdd = function () {
        layer.open({
            type: 2
            , title: '新增纠纷'
            , content: '/CaseManagement/Jfsb/Build'
            , maxmin: true
            , area: ['60%', '80%']
            , btn: ['确定', '取消']
            , btnAlign: 'c'
            , skin: "layer-ext-myskin"
            , yes: function (index, layero) {
                var iframeWindow = window['layui-layer-iframe' + index]
                    , submitID = 'NF-ContractCollection-FormSubmit'
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
                    //var resname = wooutil.UniqueValObj({
                    //    url: '/Contract/ContractCollection/CheckInputValExist',
                    //    fieldName: 'Name',
                    //    inputVal: fieldval,
                    //    currId: $currId
                    //});
                    //if (resname) {
                    //    return layer.msg('此名称已经存在！');
                    //}
                    //var rescode = wooutil.UniqueValObj({
                    //    url: '/Contract/ContractCollection/CheckInputValExist',
                    //    fieldName: 'Code',
                    //    inputVal: fieldcode,
                    //    currId: $currId
                    //});
                    //if (rescode) {
                    //    return layer.msg('此编号已经存在！');
                    //}
                    var $url = "/CaseManagement/Jfsb/Save";
                    if ($currId > 0) {
                        $url = "/Contract/ContractCollection/UpdateSave";
                    }
                    wooutil.OpenSubmitForm({
                        url: $url,
                        data: obj.field,
                        table: table,
                        msg: '保存成功',
                        index: index,
                        tableId: 'NF-Jfgl-Index'
                    });
                    return false;
                });

                submit.trigger('click');
            },
            success: function (layero, index) {
                layer.full(index);
                wooutil.openTip();
            }
        });
    };



    var active = {
        add: function () {//新增
            var ress = wooutil.requestpremission({
                url: '/NfCommon/NfPermission/AddPermission?rand=' + wooutil.getRandom(),
                data: {
                    FuncCode: 'AddJfsb'
                }

            });
            if (ress.RetValue == 0) {
                openAdd();
            } else {
                return layer.alert(ress.Msg);
            }
        },
        adds: function () {//新增

            Fpdetail();

        },
        batchdel: function () {//删除
            wooutil.deleteDatas({ tableId: 'NF-Jfgl-Index', table: table, url: '/CaseManagement/Jfsb/Delete' });
        },
        search: function () {//查询
            $("input[name=hide_keyWord]").val($("input[name=keyWord]").val());
            table.reload('NF-Jfgl-Index', {
                page: { curr: 1 }
                , where: {
                    keyWord: $("input[name=keyWord]").val()

                }
            });

        },
        submitState: function (evtobj) {//提交状态

            var checkStatus = table.checkStatus("NF-Jfgl-Index")
                , checkData = checkStatus.data; //得到选中的数据
            var ress = wooutil.requestpremission({
                url: '/NfCommon/NfPermission/StateUpdate?rand=' + wooutil.getRandom(),
                data: {
                    FuncCode: 'updatecollcontstate',
                    ObjId: checkStatus.data[0].Id
                }
            });
            if (ress.RetValue == 0) {
                if (setter.sysinfo.seversion == "SE") {
                    var resf = appflowutility.showFlow({
                        tableId: 'NF-Jfgl-Index'
                        , evtobj: evtobj
                        , objType: 3//合同
                        , deptId: checkData[0].DeptId //合同所属部门ID经办机构
                        , objCateId: checkData[0].ContTypeId//类别
                        , objName: checkData[0].Name //合同名称
                        , objCode: checkData[0].Code // 合同编号
                        , objamt: checkData[0].AmountMoney//合同金额
                        , finceType: 0//收款
                    });
                    if (resf === -1) {
                        layer.confirm('没有匹配上流程是否直接修改状态？', { icon: 3, title: '提示信息' }, function (cfindex) {
                            contractutility.updateSate({
                                tableId: 'NF-Jfgl-Index'
                                , url: '/Contract/ContractCollection/UpdateMoreField'
                                , evtobj: evtobj
                            });
                            layer.close(cfindex);

                        });
                    }
                } else {
                    contractutility.updateSate({
                        tableId: 'NF-Jfgl-Index'
                        , url: '/Contract/ContractCollection/UpdateMoreField'
                        , evtobj: evtobj
                    });
                }
            } else {
                return layer.alert(ress.Msg);
            }

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
                    table.reload("NF-Jfgl-Index", {
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

    table.on('toolbar(NF-Jfgl-Index)', function (obj) {
        switch (obj.event) {
            case 'add':
                active.add();
                break;
            case 'adds':
                active.adds();
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
                    url: "/CaseManagement/Jfsb/ExportExcel",
                    keyword: $("input[name=keyWord]").val()
                });
                break;
            case "advQuery"://高级查询
                active.advQuery();
                break;
            case "clear"://高级查询
                soulTable.clearCache("NF-Jfgl-Index")
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
            , content: '/CaseManagement/Jfsb/Build?Id=' + obj.data.Id + "&rand=" + wooutil.getRandom()
            , maxmin: true
            , area: ['60%', '80%']
            // , area: [window.screen.width / 2 + 'px', window.screen.height / 2 + 'px'] //宽高
            , btn: ['确定', '取消']
            , btnAlign: 'c'
            , yes: function (index, layero) {
                var iframeWindow = window['layui-layer-iframe' + index]
                    , submitID = 'NF-ContractCollection-FormSubmit'
                    , submit = layero.find('iframe').contents().find('#' + submitID);
                //监听提交
                iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                    var custfiledjson = "";
                    var arrfiled = [];
                    var rrty = obj.form;
                   
                    
                    var field = obj.field; //获取提交的字段
                    var fieldval = field.Name;
                    var fieldcode = field.Code;
                    
                    //var resname = wooutil.UniqueValObj({
                    //    url: '/Contract/ContractCollection/CheckInputValExist',
                    //    fieldName: 'Name',
                    //    inputVal: fieldval,
                    //    currId: field.Id
                    //});
                    //if (resname) {
                    //    return layer.msg('此名称已经存在！');
                    //}
                    //var rescode = wooutil.UniqueValObj({
                    //    url: '/Contract/ContractCollection/CheckInputValExist',
                    //    fieldName: 'Code',
                    //    inputVal: fieldcode,
                    //    currId: field.Id
                    //});
                    //if (rescode) {
                    //    return layer.msg('此编号已经存在！');
                    //}
                    wooutil.OpenSubmitForm({
                        url: '/CaseManagement/Jfsb/UpdateSave',
                        table: table,
                        data: field,
                        tableId: 'NF-Jfgl-Index',
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
    /**合同变更**/
    function changeFunc(obj, _success) {
        layer.open({
            type: 2
            , title: '合同变更'
            , content: '/Contract/ContractCollection/Change?Id=' + obj.data.Id + "&rand=" + wooutil.getRandom()
            , maxmin: true
            , area: ['60%', '80%']
            // , area: [window.screen.width / 2 + 'px', window.screen.height / 2 + 'px'] //宽高
            , btn: ['确定', '取消']
            , btnAlign: 'c'
            , yes: function (index, layero) {
                var iframeWindow = window['layui-layer-iframe' + index]
                    , submitID = 'NF-ContractCollection-FormSubmit'
                    , submit = layero.find('iframe').contents().find('#' + submitID);
                //监听提交
                iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                    var field = obj.field; //获取提交的字段

                    wooutil.OpenSubmitForm({
                        url: '/Contract/ContractCollection/ChangeSave',
                        table: table,
                        data: field,
                        tableId: 'NF-Jfgl-Index',
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
                FuncCode: 'JfsbUpdate',
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
            , content: '/CaseManagement/Jfsb/Detail?Id=' + obj.data.Id +  "&rand=" + wooutil.getRandom()
            , maxmin: true
            , area: ['60%', '80%']
            , btnAlign: 'c'
            , skin: "layer-nf-nfskin"
            , btn: ['修改', '删除']
            , btn1: function (index, layero) {
                var success = function () {
                    layer.close(index);
                }
                    customEdit(obj, success);
              

            }, btn2: function (index, layero) {
                if (obj.data.ContState === 0 && obj.data.ModificationTimes === 0) {
                    var suc = function () {
                        layer.close(index);
                    }
                    wooutil.deleteInfo({ tableId: "NF-Jfgl-Index", data: obj, url: '/CaseManagement/Jfsb/Delete', success: suc });

                    return false;
                } else {
                    layer.alert("只有未执行且没有变更过的合同才允许删除！");
                    return false;
                }
            }
           
            , success: function (layero, index) {
                layer.load(0, { shade: false, time: 1 * 1000 });//2秒自动关闭
                layer.full(index);
                wooutil.openTip();
                SetBtnBgColor(obj);
                DetailBtnShowAndHide(obj);

            }
        });
    };
    /**
   *打开客户查看页面
   **/
    function opencompview(obj) {
        layer.open({
            type: 2
            , title: '查看详情'
            , content: '/Company/Customer/Detail?Id=' + obj.data.CompId + "&rand=" + wooutil.getRandom()
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

    table.on('tool(NF-Jfgl-Index)', function (obj) {
        switch (obj.event) {
            case "del":
                {
                    var cate = layui.data(setter.tableName).userName;
                    //if ((obj.data.ContState === 0 && obj.data.ModificationTimes === 0) || cate == "SuperAdministrator") {
                        wooutil.deleteInfo({ tableId: "NF-Jfgl-Index", data: obj, url: '/CaseManagement/Jfsb/Delete' });
                    //} else {
                    //    layer.alert("当前数据不允许删除！");
                    //    return false;
                    //}
                }
                break;
            case "edit":
                {
                    var cate = layui.data(setter.tableName).userName;
                    if (obj.data.IsDelete==0) {
                        customEdit(obj);
                    } else if (obj.data.ContState === 0 && obj.data.ModificationTimes > 0) {//变更修改
                        contractChange(obj, null);
                    } else if (cate == "SuperAdministrator") {
                        customEdit(obj);
                    }
                    else {
                        layer.alert("只有未执行且没有变更过的合同才允许修改！");
                        return false;
                    }
                }
                break;
            case "detail":
                {
                    //var ress = wooutil.requestpremission({
                    //    url: '/NfCommon/NfPermission/ViewPermission?rand=' + wooutil.getRandom(),
                    //    data: {
                    //        FuncCode: 'querycollcontview',
                    //        ObjId: obj.data.Id
                    //    }

                    //});
                    //if (ress.RetValue == 0) {
                        openview(obj);
                    //} else {
                    //    return layer.alert(ress.Msg);
                    //}
                }
                break;
            case "compdetail":
                {
                    var ress = wooutil.requestpremission({
                        url: '/NfCommon/NfPermission/ViewPermission?rand=' + wooutil.getRandom(),
                        data: {
                            FuncCode: 'querycustomerview',
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




    exports('collectionContractIndex', {});
});