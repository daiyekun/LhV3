/**
 @Name：收款合同列表
 @Author：dyk 
 */
layui.define(['table', 'tenderutility', 'form', 'appflowutility', 'dynamicCondition', 'treeSelect', 'soulTable'], function (exports) {
    var $ = layui.$
        , table = layui.table
        , setter = layui.setter
        , admin = layui.admin
        , tenderutility = layui.tenderutility
        , form = layui.form
        , appflowutility = layui.appflowutility
        , dynamicCondition = layui.dynamicCondition
        , treeSelect = layui.treeSelect
     , soulTable = layui.soulTable;
    var logdindex = layer.load(0, { shade: false });
    var _reqUrl = "/Tender/TenderInfo/GetList?rand=" + wooutil.getRandom();
    var $htcols = [
        { type: 'numbers', fixed: 'left' }
        , { type: 'checkbox', fixed: 'left' }
        , { field: 'TenderUserNAME', title: '招标部门', width: 240, templet: '#ProjectNAMETpl', fixed: 'left'}
        , { field: 'ProjectName', title: '项目名称', width: 140, templet:'#ProjectTpl', filter: true }
        , { field: 'ProjectNO', title: '项目编号', width: 130, sort: true, filter: true }
        , { field: 'Iocation', title: '地点', width: 140, filter: true }
        , { field: 'TenderDate', title: '时间', width: 130, filter: true }
        , { field: 'ContractEnforcementDepName', title: '合同执行部门', width: 140}
        , { field: 'RecorderName', title: '记录人', width: 130, filter: true }
        , { field: 'TenderExpirationDate', title: '有效期', width: 130, filter: true }
        , { field: 'TenderStatus', title: '招标状态', width: 130, templet:'#stateTpl', filter: true }
        , { field: 'TenderStatusId', title: '招标ID', hide: true, width: 130 }//ZbswName
        , { field: 'Zbdw', title: '中标单位id', hide: true}
        , { field: 'ZbswName', title: '中标单位', width: 130, filter: true ,templet: '#compTpl', }
        , { title: '操作', width: 150, align: 'center', fixed: 'right', toolbar: '#table-Tender-bar' }
    ];
    /**删除审批列**/
    appflowutility.SeCols({ htcols: $htcols });

    //列表
    table.render({
        elem: '#NF-TenderCollection-Index'
        , url: _reqUrl
        , toolbar: '#toolTender'
        , defaultToolbar: ['filter']
        , cellMinWidth: 80
        , cols: [$htcols]
        , page: true
        , loading: true
        , height: setter.table.height_4
        , limit: setter.table.limit
        , limits: setter.table.limits
        , filter: {
            bottom: false
        }
        , done: function (res, curr, count) {   //返回数据执行回调函数
            soulTable.render(this)
            tenderutility.stateEvent({ tableId: 'NF-TenderCollection-Index' });//注册状态流转事件
            layer.close(logdindex);    //返回数据关闭loading
            $("input[name=keyWord]").val($("input[name=hide_keyWord]").val());
            $("input[name=hide_keyWord]").val("");
            

        }

    });
    //监听表格排序
    table.on('sort(NF-TenderCollection-Index)', function (obj) {
        table.reload('NF-TenderCollection-Index', { //testTable是表格容器id
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
            , title: '新增招标'
            , content: '/Tender/TenderInfo/Build'
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
                    var fieldval = obj.field.Name;
                    var $currId = !isNaN($htId) && $htId > 0 ? $htId : 0;
                    var $url = "/Tender/TenderInfo/Save";
                    if ($currId > 0) {
                        $url = "/Contract/ContractCollection/UpdateSave";
                    }
                    wooutil.OpenSubmitForm({
                        url: $url,
                        data: obj.field,
                        table: table,
                        msg: '保存成功',
                        index: index,
                        tableId: 'NF-TenderCollection-Index'
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
                    FuncCode: 'addzbcollcont'
                }

            });
            if (ress.RetValue == 0) {
                openAdd();
            } else {
                return layer.alert(ress.Msg);
            }
        },
        batchdel: function () {//删除
            wooutil.deleteDatas({ tableId: 'NF-TenderCollection-Index', table: table, url: '/Tender/TenderInfo/Delete' });
        },
        search: function () {//查询
            $("input[name=hide_keyWord]").val($("input[name=keyWord]").val());
            table.reload('NF-TenderCollection-Index', {
                page: { curr: 1 }
                , where: {
                    keyWord: $("input[name=keyWord]").val()

                }
            });

        },
        submitState: function (evtobj) {//提交状态
            var checkStatus = table.checkStatus("NF-TenderCollection-Index")
                , checkData = checkStatus.data; //得到选中的数据
            var ress = wooutil.requestpremission({
                url: '/NfCommon/NfPermission/StateUpdate?rand=' + wooutil.getRandom(),
                data: {
                    FuncCode: 'updatezbcollcontstate',
                    ObjId: checkStatus.data[0].Id
                }
            });
            if (ress.RetValue == 0) {
                if (setter.sysinfo.seversion == "SE") {
                    var resf = appflowutility.showFlow({
                        tableId: 'NF-TenderCollection-Index'
                        , evtobj: evtobj
                        , objType: 10//询价
                        , deptId: checkData[0].CreateUserId
                        , objCateId: checkData[0].TenderType//类别
                        , objName: checkData[0].Id
                        , objCode: checkData[0].ProjectNO
                    });

                    if (resf === -1) {
                        layer.confirm('没有匹配上流程是否直接修改状态？', { icon: 3, title: '提示信息' }, function (cfindex) {
                            tenderutility.updateSate({
                                tableId: 'NF-TenderCollection-Index'
                                , url: '/Tender/TenderInfo/UpdateField'
                                , evtobj: evtobj
                            });
                            layer.close(cfindex);

                        });
                    }
                } else {
                    tenderutility.updateSate({
                        tableId: 'NF-TenderCollection-Index'
                        , url: '/Tender/TenderInfo/UpdateField'
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
                    table.reload("NF-TenderCollection-Index", {
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

    table.on('toolbar(NF-TenderCollection-Index)', function (obj) {
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
                    url: "/Tender/TenderInfo/ExportExcel",
                    keyword: $("input[name=keyWord]").val()
                });
                break;
            case 'clear'://状态流转
                soulTable.clearCache("NF-TenderCollection-Index")
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
            , content: '/Tender/TenderInfo/Build?Id=' + obj.data.Id + "&rand=" + wooutil.getRandom()
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
                        url: '/Tender/TenderInfo/UpdateSave',
                        table: table,
                        data: field,
                        tableId: 'NF-TenderCollection-Index',
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
        //        FuncCode: 'updatecollcont',
        //        ObjId: obj.data.Id
        //    }
        //});
        //if (ress.RetValue == 0) {
        if (0 == 0) {
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
        if (obj.data.TenderStatusId == 0) {
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
            , content: '/Tender/TenderInfo/Detail?Id=' + obj.data.Id + "&rand=" + wooutil.getRandom()
            , maxmin: true
            , area: ['60%', '80%']
            , btnAlign: 'c'
            , skin: "layer-nf-nfskin"
            , btn: ['修改', '删除']
            , btn1: function (index, layero) {
                if (obj.data.tenTenderStatus === 0) {
                    var success = function () {
                        layer.close(index);
                    }
                    customEdit(obj, success);
                } else {
                    layer.alert("只有未执的合同才允许修改！");
                    return false;

                }

            }
            , btn2: function (index, layero) {
                if (obj.data.TenderStatusId === 0) {
                    var suc = function () {
                        layer.close(index);
                    }
                    wooutil.deleteInfo({ tableId: "NF-TenderCollection-Index", data: obj, url: '/Contract/ContractCollection/Delete', success: suc });

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
 *打开项目查看页面
 **/
    //function openPorjview(obj) {
    //    layer.open({
    //        type: 2
    //        , title: '查看详情'
    //        , content: '/Project/ProjectManager/Detail?Id=' + obj.data.ProjectId + "&rand=" + wooutil.getRandom()
    //       // , content: '/Company/Customer/Detail?Id=' + obj.data. + "&rand=" + wooutil.getRandom()
    //        , maxmin: true
    //        , area: ['60%', '80%']
    //        , btnAlign: 'c'
    //        , skin: "layer-nf-nfskin"
    //        , btn: ['关闭']
    //        , success: function (layero, index) {
    //            layer.load(0, { shade: false, time: 1 * 1000 });//2秒自动关闭
    //            layer.full(index);
    //            wooutil.openTip();
    //            SetBtnBgColor(obj);
    //           // DetailBtnShowAndHide(obj);

    //        }
    //    });
    //};
    function openPorjview(obj) {

        layer.open({
            type:2
            , title: '查看详情'
            , content: '/Project/ProjectManager/Detail?Id=' + obj.data.ProjectId + "&rand=" + wooutil.getRandom()
            , maxmin: true
            , area: ['60%', '80%']
            , btnAlign: 'c'
            , skin: "layer-nf-nfskin"
            , btn: ['关闭']// , area: [window.screen.width / 2 + 'px', window.screen.height / 2 + 'px'] //宽高
           
            , success: function (layero, index) {
                layer.load(0, { shade: false, time: 1 * 1000 });//2秒自动关闭
                layer.full(index);
                wooutil.openTip();
                //SetBtnBgColor();
              //  DetailBtnShowAndHide(obj);
            }
        });
    };
    table.on('tool(NF-TenderCollection-Index)', function (obj) {
     
        switch (obj.event) {
            case "del":
                {
                    var cate = layui.data(setter.tableName).userName;
                    if (obj.data.TenderStatusId === 0 || cate == "SuperAdministrator") {
                        wooutil.deleteInfo({ tableId: "NF-TenderCollection-Index", data: obj, url: '/Tender/TenderInfo/Delete' });
                    } else {
                        layer.alert("执行中数据无法修改！");
                        return false;
                    }
                }
                break;
            case "edit":
                {

                    if (obj.data.TenderStatusId ===0) {
                        customEdit(obj);
                    } else if (obj.data.TenderStatusId === 0) {//变更修改
                        contractChange(obj, null);
                    }
                    else {
                        layer.alert("执行中数据无法修改");
                        return false;
                    }
                }
                break;
            case "detail":
                {
                    var ress = wooutil.requestpremission({
                        url: '/NfCommon/NfPermission/ViewPermission?rand=' + wooutil.getRandom(),
                        data: {
                            FuncCode: 'queryInquirylist',
                            ObjId: obj.data.Id
                        }
                    });
                    if (ress.RetValue == 0) {
                        openview(obj);
                    } else {
                        return layer.alert(ress.Msg);
                    }
                    openview(obj);
                }
                break;
            case "Projectdetail":
                {
                    var ress = wooutil.requestpremission({
                        url: '/NfCommon/NfPermission/ViewPermission?rand=' + wooutil.getRandom(),
                        data: {
                            FuncCode: 'queryprojectview',
                            ObjId: obj.data.Id
                        }
                    });
                    if (ress.RetValue == 0) {
                        openPorjview(obj);
                    } else {
                        return layer.alert(ress.Msg);
                    }
                    openPorjview(obj);
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
    /**
*打开客户查看页面
**/
    function opencompview(obj) {
        layer.open({
            type: 2
            , title: '查看详情'
            , content: '/Company/Customer/Detail?Id=' + obj.data.Zbdw + "&rand=" + wooutil.getRandom()
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
    exports('collectionContractIndex', {});
});