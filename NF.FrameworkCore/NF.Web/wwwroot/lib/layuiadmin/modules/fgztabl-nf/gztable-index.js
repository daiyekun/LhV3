
layui.define(['table','index', 'gzdescutility', 'form', 'appflowutility', 'dynamicCondition', 'treeSelect', 'soulTable'], function (exports) {
    var $ = layui.$
        , table = layui.table
        , setter = layui.setter
        , admin = layui.admin
        , gzdescutility = layui.gzdescutility
        , form = layui.form
        , appflowutility = layui.appflowutility
        , dynamicCondition = layui.dynamicCondition
        , treeSelect = layui.treeSelect
        , soulTable = layui.soulTable;
    var logdindex = layer.load(0, { shade: false });
    var searchType = wooutil.getUrlVar('seaType');
    var _reqUrl = "/Workbench/Gzdesk/GetList?contId=" + 3 + "&rand=" + wooutil.getRandom();
    if (searchType != undefined && searchType === "1") {
        _reqUrl = _reqUrl + "&search=" + searchType;
    }
    var $htcols = [
        { type: 'numbers', fixed: 'left' }
        , { type: 'checkbox', fixed: 'left' }
        , { field: 'Jdname', title: '进度名称', width: 240, fixed: 'left', filter: true, sort: true }
        , { field: 'ScheduleName', title: '任务名称', width: 130, filter: true }
        , { field: 'ScheduleAttributionDic', title: '任务归属', width: 140, filter: true }
        , { field: 'ScheduleDuixiangName', title: '任务对象', width: 130}
        , { field: 'Description', title: '任务定义', width: 140, filter: true }
        , { field: 'Descriptionms', title: '任务描述', width: 130, filter: true }
        , { field: 'TixingName', title: '提醒人', hide: true, filter: true }
        , { field: 'DesigneeName', title: '执行人', width: 130, filter: true }
        , { field: 'StalkerName', title: '跟踪者', width: 130, filter: true }
        , { field: 'JdtataTime', title: '进度时间', width: 130, hide: true }
        , { field: 'MystateName', title: '状态', width: 130 }
        , { field: 'Mystate', title: '状态id', width: 130}
        , { field: 'CreateUserName', title: '创建人', width: 130, filter: true}
        , { field: 'CreateDateTime', title: '创建时间', width: 130, filter: true }
        , { field: 'ModifyUserName', title: '修改人', width: 130, filter: true }
        , { field: 'ModifyDateTime', title: '修改时间', width: 130, filter: true  }


    ];
    /**删除审批列**/
    appflowutility.SeCols({ htcols: $htcols });

    //列表
    table.render({
        elem: '#NF-Gztable-Index'
        , url: _reqUrl// '/Workbench/Gzdesk/GetList?contId=' + 3 + '&rand=' + wooutil.getRandom()
       // , url: '/Workbench/Gzdesk/GetList?&rand=' + wooutil.getRandom()
        , toolbar: '#toolWork3'
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
                    name: '查询实际收款',
                    click: function (obj) {
                        var contrId = obj.row.Id;
                        parent.layui.index.openTabsPage("/Finance/ContActualFinance/ActualFinanceCollIndex?contrId=" + contrId, "查询实际收款");
                    }
                },
                {
                    name: '查询开票',
                    click: function (obj) {
                        var contrId = obj.row.Id;
                        parent.layui.index.openTabsPage("/Finance/ContInvoice/InvoiceOutIndex?contrId=" + contrId, "查询开票");
                    }
                },
                {
                    name: '新建实际收款',
                    click: function (obj) {
                        if (obj.row.ContStateDic == "执行中") {
                            var contId = obj.row.Id;
                            parent.layui.index.openTabsPage("/Finance/ContActualFinance/ActualFinanceCollBuild?contId=" + contId, "新建实际收款");
                        } else {
                            layer.msg('当前状态不可新建！')
                        }

                    }

                },
                {
                    name: '新建开票',
                    click: function (obj) {
                        if (obj.row.ContStateDic == "执行中") {
                            var contId = obj.row.Id;
                            parent.layui.index.openTabsPage("/Finance/ContInvoice/BuildInvoiceOut?contId=" + contId, "新建开票");
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
            layer.close(logdindex);    //返回数据关闭loading
            var AmountMoney = 0;
            for (var i = 0; i < res.data.length; i++) {
                AmountMoney += res.data[i].AmountMoney
            }
            this.elem.next().find('.layui-table-total td[data-field="ContAmThod"] .layui-table-cell').text(AmountMoney.toLocaleString());//toLocaleString().style.color = "#ff0000"
            this.elem.next().find('.layui-table-total td[data-field="ContAmThod"] .layui-table-cell').css("color", "red");
            $("input[name=keyWord]").val($("input[name=hide_keyWord]").val());
            $("input[name=hide_keyWord]").val("");
            gzdescutility.stateEvent({ tableId: 'NF-Gztable-Index' });//注册状态流转事件

        }

    });

    //监听表格排序
    table.on('sort(NF-Gztable-Index)', function (obj) {
        table.reload('NF-Gztable-Index', { //testTable是表格容器id
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
 


    var active = {
       
        search: function () {//查询
            $("input[name=hide_keyWord]").val($("input[name=keyWord]").val());
            table.reload('NF-Gztable-Index', {
                page: { curr: 1 }
                , where: {
                    keyWord: $("input[name=keyWord]").val()

                }
            });

        },
        submitState: function (evtobj) {//提交状态
            var checkStatus = table.checkStatus("NF-Gztable-Index")
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
                        tableId: 'NF-Gztable-Index'
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
                            gzdescutility.updateSate({
                                tableId: 'NF-Gztable-Index'
                                , url: '/Contract/ContractCollection/UpdateMoreField'
                                , evtobj: evtobj
                            });
                            layer.close(cfindex);

                        });
                    }
                } else {
                    gzdescutility.updateSate({
                        tableId: 'NF-Gztable-Index'
                        , url: '/Contract/ContractCollection/UpdateMoreField'
                        , evtobj: evtobj
                    });
                }
            } else {
                return layer.alert(ress.Msg);
            }

        },
        updstatec: function (evtobj) {//提交状态
            var checkStatus = table.checkStatus("NF-Gztable-Index")
                , checkData = checkStatus.data; //得到选中的数据

            //var id = checkStatus.data[0].ScheduleId;
            var idlis = checkStatus.data[0].Id;
            gzdescutility.updateSate({
                tableId: 'NF-Gztable-Index'
                , url: '/Workbench/Gzdesk/Updstate?contId=' + idlis + '&ty=' + 1 +'&rand=' + wooutil.getRandom()
                , evtobj: evtobj
            });
          
        },
        updstateg: function (evtobj) {//提交状态
            var checkStatus = table.checkStatus("NF-Gztable-Index")
                , checkData = checkStatus.data; //得到选中的数据

            //var id = checkStatus.data[0].ScheduleId;
            var idlis = checkStatus.data[0].Id;
            gzdescutility.updateSate({
                tableId: 'NF-Gztable-Index'
                , url: '/Workbench/Gzdesk/Updstate?contId=' + idlis + '&ty=' + 2+'&rand=' + wooutil.getRandom()
                , evtobj: evtobj
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
                    table.reload("NF-Gztable-Index", {
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

    table.on('toolbar(NF-Gztable-Index)', function (obj) {
     
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
            case "updstatec"://重做
                active.updstatec(this);
                break;
            case "updstateg"://关闭
                active.updstateg(this)
                break;
            case "exportexcel":
                wooutil.exportexcel(obj, {
                    url: '/Workbench/Gzdesk/ExportExcel?contId=' + 3 + '&rand=' + wooutil.getRandom(),
                    keyword: $("input[name=keyWord]").val()
                });
                break;
            case "advQuery"://高级查询
                active.advQuery();
                break;
            case "clear"://高级查询
                soulTable.clearCache("NF-Gztable-Index")
                layer.msg('已还原！', { icon: 1, time: 1000 })
                break;

        };
    });
    /***********************************************监听头部工具栏---end**************************************************************/

    /**********************************************************监听工具条-begin*********************************************************/
 
    /**编辑带权限*/
    function customEdit(obj, success) {
        var ress = wooutil.requestpremission({
            url: '/NfCommon/NfPermission/UpdatePermission?rand=' + wooutil.getRandom(),
            data: {
                FuncCode: 'updatecollcont',
                ObjId: obj.data.Id
            }
        });
        if (ress.RetValue == 0) {
            editFunc(obj, success);
        } else {
            return layer.alert(ress.Msg);
        }
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



    table.on('tool(NF-Gztable-Index)', function (obj) {
        switch (obj.event) {
            case "del":
                {
                    if (obj.data.ContState === 0 && obj.data.ModificationTimes === 0) {
                        wooutil.deleteInfo({ tableId: "NF-Gztable-Index", data: obj, url: '/Contract/ContractCollection/Delete' });
                    } else {
                        layer.alert("只有未执行且没有变更过的合同才允许删除！");
                        return false;
                    }
                }
                break;
            case "edit":
                {
                    if (obj.data.ContState === 0 && obj.data.ModificationTimes === 0) {
                        customEdit(obj);
                    } else if (obj.data.ContState === 0 && obj.data.ModificationTimes > 0) {//变更修改
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
                            FuncCode: 'querycollcontview',
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




    exports('Gztable', {});
});