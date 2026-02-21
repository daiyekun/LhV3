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
    var _reqUrl = "/Schedule/ScheduleManagement/GetList?rand=" + wooutil.getRandom();
    if (searchType != undefined && searchType === "1") {
        _reqUrl = _reqUrl + "&search=" + searchType;
    }
    var $htcols = [
        { type: 'numbers', fixed: 'left' }
        , { type: 'checkbox', fixed: 'left' }
        , { field: 'ScheduleName', title: '进度名称', width: 240, templet: '#nameTpl', fixed: 'left', sort: true, filter: true  }
        , { field: 'ScheduleSer', title: '进度关键字', width: 130, sort: true, filter: true }
        , { field: 'PriorityDic', title: '优先级', width: 140, filter: true }
        , { field: 'ScheduleAttributionDic', title: '进度归属', width: 130, filter: true }
        , { field: 'ScheduleDuixiangName', title: '进度对象', width: 140 }
        , { field: 'Description', title: '描述', width: 130, sort: true, filter: true }
        , { field: 'DesigneeName', title: '指派给', width: 130, filter: true }
        , { field: 'StalkerName', title: '跟踪者', width: 130, filter: true }
        , { field: 'Wancheng', title: '完成%', width: 130, filter: true }
        , { field: 'JhCreateDateTime', title: '计划开始时间', width: 130, filter: true }
        , { field: 'JhCompleteDateTime', title: '计划完成时间', width: 130, hide: true, filter: true }
        , { field: 'SjCreateDateTime', title: '实际开始时间', width: 130, filter: true }
        , { field: 'SjCompleteDateTime', title: '实际完成时间', width: 130, hide: true, filter: true }
        , { field: 'CreateName', title: '创建人', width: 130, filter: true }
        , { field: 'CreateDateTime', title: '创建时间', width: 130, hide: true, filter: true }
        , { field: 'ModifyName', title: '修改人', width: 130, filter: true }
        , { field: 'ModifyDateTime', title: '修改时间', width: 130, hide: true, filter: true }
        , { field: 'State', title: '状态', width: 130, templet: '#stateTpl', filter: true }
        , { field: 'Id', title: 'ID', width: 130, hide: true }
        , { title: '操作', width: 150, align: 'center', fixed: 'right', toolbar: '#table-contract-bar' }
    ];
    /**删除审批列**/
    appflowutility.SeCols({ htcols: $htcols });

    //列表
    table.render({
        elem: '#NF-ScheduleManagementCollection-Index'
        , url: _reqUrl
        , toolbar: '#toolcontract'
        , defaultToolbar: ['filter']
        , cellMinWidth: 80
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
            $("input[name=keyWord]").val($("input[name=hide_keyWord]").val());
            $("input[name=hide_keyWord]").val("");
            contractutility.stateEvent({ tableId: 'NF-ScheduleManagementCollection-Index' });//注册状态流转事件

        }

    });
    //监听表格排序
    table.on('sort(NF-ScheduleManagementCollection-Index)', function (obj) {
        table.reload('NF-ScheduleManagementCollection-Index', { //testTable是表格容器id
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
            , title: '新增进度管理'
            , content: '/Schedule/ScheduleManagement/Build'
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
                    var field = obj.field; //获取提交的字段
                    var $currId = !isNaN($htId) && $htId > 0 ? $htId : 0;

                    var $url = "/Schedule/ScheduleManagement/Save";
                    if ($currId > 0) {
                        $url = "/Schedule/ScheduleManagement/UpdateSave";
                    }
                    wooutil.OpenSubmitForm({
                        url: $url,
                        data: obj.field,
                        table: table,
                        msg: '保存成功',
                        index: index,
                        tableId: 'NF-ScheduleManagementCollection-Index'
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
            //var ress = wooutil.requestpremission({
            //    url: '/NfCommon/NfPermission/AddPermission?rand=' + wooutil.getRandom(),
            //    data: {
            //        FuncCode: 'addQuestioning'
            //    }
            //});
            //if (ress.RetValue == 0) {
                openAdd();
            //} else {
            //    return layer.alert(ress.Msg);
            //}
        },
        batchdel: function () {//删除
            wooutil.deleteDatas({ tableId: 'NF-ScheduleManagementCollection-Index', table: table, url: '/Schedule/ScheduleManagement/Delete' });
        },
        search: function () {//查询
            $("input[name=hide_keyWord]").val($("input[name=keyWord]").val());
            table.reload('NF-ScheduleManagementCollection-Index', {
                page: { curr: 1 }
                , where: {
                    keyWord: $("input[name=keyWord]").val()
                }
            });
        },
        submitState: function (evtobj) {//提交状态
            var checkStatus = table.checkStatus("NF-ScheduleManagementCollection-Index")
                , checkData = checkStatus.data; //得到选中的数据
            //var ress = wooutil.requestpremission({
            //    url: '/Schedule/ScheduleManagement/StateUpdate?rand=' + wooutil.getRandom(),
            //    data: {
            //        ObjId: checkStatus.data[0].Id
            //    }
            //});
           // if (ress.RetValue == 0) {
                if (setter.sysinfo.seversion == "SE") {

                    var resf = appflowutility.showFlow({
                        tableId: 'NF-ScheduleManagementCollection-Index'
                        , evtobj: evtobj
                        , objType: 9//约谈
                        , deptId: checkData[0].CreateUserId
                        , objCateId: checkData[0].QueType//类别
                        , objName: checkData[0].Id
                        , objCode: checkData[0].ProjectNumber
                    });
                    if (resf === -1) {
                        layer.confirm('没有匹配上流程是否直接修改状态？', { icon: 3, title: '提示信息' }, function (cfindex) {
                            contractutility.updateSate({
                                tableId: 'NF-ScheduleManagementCollection-Index'
                                , url: '/Schedule/ScheduleManagement/UpdateField'
                                , evtobj: evtobj
                            });
                            layer.close(cfindex);
                        });
                    }
                } else {
                    contractutility.updateSate({
                        tableId: 'NF-ScheduleManagementCollection-Index'
                        , url: '/Schedule/ScheduleManagement/UpdateField'
                        , evtobj: evtobj
                    });
                }
            //} else {
            //    return layer.alert(ress.Msg);
            //}
        },
        advQuery: function () {
            var complexDc = dynamicCondition.create({
                elem: "#dynamicCondition"
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
                    table.reload("NF-ScheduleManagementCollection-Index", {
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
    table.on('toolbar(NF-ScheduleManagementCollection-Index)', function (obj) {
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
                    url: "/Schedule/ScheduleManagement/ExportExcel",
                    keyword: $("input[name=keyWord]").val()
                });
                break;
            case "clear":
                soulTable.clearCache("NF-ScheduleManagementCollection-Index")
                layer.msg('已还原！', { icon: 1, time: 1000 })
                break;
            case "advQuery"://高级查询
                active.advQuery();
                break;
        };
    });
    /***********************************************监听头部工具栏---end**************************************************************/

    /**********************************************************监听工具条-begin*********************************************************/
    function editFunc(obj, _success) {
        layer.open({
            type: 2
            , title: '修改信息'
            , content: '/Schedule/ScheduleManagement/Build?Id=' + obj.data.Id + "&rand=" + wooutil.getRandom()
            , maxmin: true
            , area: ['60%', '80%']
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
                        url: '/Schedule/ScheduleManagement/UpdateSave',
                        table: table,
                        data: field,
                        tableId: 'NF-ScheduleManagementCollection-Index',
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
            , content: '/Questioning/Questioning/Change?Id=' + obj.data.Id + "&rand=" + wooutil.getRandom()
            , maxmin: true
            , area: ['60%', '80%']
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
                        url: '/Questioning/Questioning/ChangeSave',
                        table: table,
                        data: field,
                        tableId: 'NF-ScheduleManagementCollection-Index',
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

        //var ress = wooutil.requestpremission({
        //    url: '/NfCommon/NfPermission/UpdatePermission?rand=' + wooutil.getRandom(),
        //    data: {
        //        FuncCode: 'updateQuestioning',
        //        ObjId: obj.data.Id
        //    }
        //});
        //if (ress.RetValue == 0) {
            editFunc(obj, success);
        //} else {
        //    return layer.alert(ress.Msg);
        //}
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
            , data: { perCode: "Questioning", Id: obj.data.Id }
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
        if (obj.data.State == 0) {
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
            , content: '/Schedule/ScheduleManagement/Detail?Id=' + obj.data.Id + "&rand=" + wooutil.getRandom()
            , maxmin: true
            , area: ['60%', '80%']
            , btnAlign: 'c'
            , skin: "layer-nf-nfskin"
            , btn: ['修改', '删除']
            , btn1: function (index, layero) {
                if (obj.data.State === 0) {
                    var success = function () {
                        layer.close(index);
                    }
                    customEdit(obj, success);
                } else {
                    layer.alert("执行中数据无法修改！");
                    return false;

                }

            }, btn2: function (index, layero) {
                if (obj.data.State === 0) {
                    var suc = function () {
                        layer.close(index);
                    }
                    wooutil.deleteInfo({ tableId: "NF-ScheduleManagementCollection-Index", data: obj, url: '/Schedule/ScheduleManagement/Delete', success: suc });

                    return false;
                } else {
                    layer.alert("当前状态不允许删除！");
                    return false;
                }
            },
            btn3: function (index, layero) {
                if (obj.data.State === 1 || obj.data.State == 0) {
                    contractChange(obj, index);
                } else {
                    layer.alert("当前状态不允许修改！");
                    return false;//阻止关闭
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


    table.on('tool(NF-ScheduleManagementCollection-Index)', function (obj) {

        switch (obj.event) {

            case "del":
                {
                    if (obj.data.State === 0 || obj.State === null) {
                        wooutil.deleteInfo({ tableId: "NF-ScheduleManagementCollection-Index", data: obj, url: '/Schedule/ScheduleManagement/Delete' });
                    } else {
                        layer.alert("已完成数据无法修改");
                        return false;
                    }
                }
                break;
            case "edit":
                {
                    var cate = layui.data(setter.tableName).userName;
                    if (obj.data.State === 0) {
                        customEdit(obj);
                    } else if (obj.data.State === 0) {//变更修改
                        contractChange(obj, null);
                    } else if (cate == "SuperAdministrator") {
                        customEdit(obj);
                    }
                    else {
                        layer.alert("当前状态不允许修改！");
                        return false;
                    }
                }
                break;
            case "detail":
                {

                    //var ress = wooutil.requestpremission({
                    //    url: '/NfCommon/NfPermission/ViewPermission?rand=' + wooutil.getRandom(),
                    //    data: {
                    //        FuncCode: 'queryInquirylist',
                    //        ObjId: obj.data.Id
                    //    }

                    //});
                    //if (ress.RetValue == 0) {
                    //    openview(obj);
                    //} else {
                    //    return layer.alert(ress.Msg);
                    //}
                    openview(obj);
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





    exports('collectionScheduleIndex', {});
});