/**
 @Name：项目
 @Author：dyk 
 */
layui.define(['table', 'projectutility', 'form', 'appflowutility', 'dynamicCondition', 'soulTable'], function (exports) {
    var $ = layui.$
   , table = layui.table
   , setter = layui.setter
   , admin = layui.admin
   , form = layui.form
   , projectutility = layui.projectutility
        , appflowutility = layui.appflowutility
        , soulTable = layui.soulTable
        , dynamicCondition = layui.dynamicCondition;
    var logdindex = layer.load(0, { shade: false });
    var $htcols = [
        { type: 'numbers', fixed: 'left' }
        , { type: 'checkbox', fixed: 'left' }
        , { field: 'Name', title: '名称', minWidth: 160, width: 220, templet: '#nameTpl', fixed: 'left', filter: true, sort: true }
        , { field: 'Code', title: '编号', width: 130, sort: true }
        , { field: 'ProjTypeName', title: '类别', width: 130, filter: true}
        , { field: 'BugetCollectAmountMoneyThod', title: '预算收款', width: 130, filter: true, sort: true }
        , { field: 'BudgetPayAmountMoneyThod', title: '预算付款', width: 130, filter: true, sort: true }
        , { field: 'PriUserName', title: '项目负责人', width: 130, filter: true, hide: true }
        , { field: 'PlanBeginDateTime', title: '计划开始时间', width: 130, filter: true,sort: true }
        , { field: 'PlanCompleteDateTime', title: '计划结束时间', width: 130, filter: true,sort: true }
        , { field: 'Pstate', title: '状态', width: 120, templet: '#projectstateTpl', filter: true, unresize: true }
        , { field: 'CreateUserName', title: '建立人', width: 130, filter: true, hide: true }
        , { field: 'CreateDateTime', title: '建立日期', width: 130, filter: true, hide: true }
        , { field: 'Id', title: 'Id', width: 50, hide: true }
        , { field: 'WfStateDic', title: '流程状态', width: 130, templet: '#wfstateTpl' }
        , { field: 'WfCurrNodeName', title: '当前节点', width: 140 ,filter: true}
        , { field: 'WfItemDic', title: '审批事项', width: 160 }
        , { title: '操作', width: 150, align: 'center', fixed: 'right', toolbar: '#table-project-bar' }
    ];
    /**删除审批列**/
    appflowutility.SeCols({ htcols: $htcols });
    //列表
    table.render({
        elem: '#NF-project-index'
       , url: '/Project/ProjectManager/GetList?rand=' + wooutil.getRandom()
       , toolbar: '#toolproject'
       , defaultToolbar: ['filter']
        , cellMinWidth: 80
        //, totalRow: true
        , overflow: {
            type: 'tips'//内容超过设置
            //, hoverTime: 300 // 悬停时间，单位ms, 悬停 hoverTime 后才会显示，默认为 0
            , color: 'black' // 字体颜色
            , bgColor: 'white' // 背景色
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
           $("input[name=keyWord]").val($("input[name=hide_keyWord]").val());
            $("input[name=hide_keyWord]").val("");

            var cate = layui.data(setter.tableName).userName;
            projectutility.stateEvent({ tableId: 'NF-project-index' }, cate);//注册状态流转事件

       }

    });
    //清除列表缓存
    //$('#clear').on('click', function () {
    //    soulTable.clearCache("NF-project-index")
    //    layer.msg('已还原！', { icon: 1, time: 1000 })
    //})
    //监听表格排序
    table.on('sort(NF-project-index)', function (obj) {
        table.reload('NF-project-index', { //testTable是表格容器id
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
            , title: '新增项目'
            , content: '/Project/ProjectManager/Build'
            , maxmin: true
            , area: ['60%', '80%']
            , btn: ['确定', '取消']
            , btnAlign: 'c'
            , skin: "layer-ext-myskin"
            , yes: function (index, layero) {
                var iframeWindow = window['layui-layer-iframe' + index]
                    , submitID = 'NF-project-formsubmit'
                    , submit = layero.find('iframe').contents().find('#' + submitID);
                //监听提交
                iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                    var field = obj.field; //获取提交的字段
                    var fieldval = obj.field.Name;
                    var fieldcode = obj.field.Code;
                    var jhkssj = obj.field.PlanBeginDateTime;//计划开始时间
                    var jhwcsj = obj.field.PlanCompleteDateTime;//计划完成时间
                    var ressjbj = ((new Date(jhkssj.replace(/-/g, "\/"))) > (new Date(jhwcsj.replace(/-/g, "\/"))));
                    if (ressjbj) {
                        return layer.alert("计划开始时间不能大于计划完成时间！");
                    }
                    var resname = wooutil.UniqueValObj({
                        url: '/Project/ProjectManager/CheckInputValExist',
                        fieldName: 'Name',
                        inputVal: fieldval,
                        currId: 0
                    });
                    if (resname) {
                        return layer.msg('此名称已经存在！');
                    }
                    var rescode = wooutil.UniqueValObj({
                        url: '/Project/ProjectManager/CheckInputValExist',
                        fieldName: 'Code',
                        inputVal: fieldcode,
                        currId: 0
                    });
                    if (rescode) {
                        return layer.msg('此编号已经存在！');
                    }
                    wooutil.OpenSubmitForm({
                        url: '/Project/ProjectManager/Save',
                        data: obj.field,
                        table: table,
                        index: index,
                        tableId: 'NF-project-index'
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
                    FuncCode: 'addproject'
                }

            });
            if (ress.RetValue == 0) {
                openAdd();
            } else {
                return layer.alert(ress.Msg);
            }
        },
        batchdel: function () {//删除
            wooutil.deleteDatas({ tableId: 'NF-project-index', table: table, url: '/Project/ProjectManager/Delete' });
        },
        search: function () {//查询
            $("input[name=hide_keyWord]").val($("input[name=keyWord]").val());
            table.reload('NF-project-index', {
                page: { curr: 1 }
                , where: {
                    keyWord: $("input[name=keyWord]").val()

                }
            });

        }, submitState: function (evtobj) {//提交状态
            var checkStatus = table.checkStatus("NF-project-index")
                , checkData = checkStatus.data; //得到选中的数据
            var ress = wooutil.requestpremission({
                url: '/NfCommon/NfPermission/StateUpdate?rand=' + wooutil.getRandom(),
                data: {
                    FuncCode: 'updateprojectstate',
                    ObjId: checkStatus.data[0].Id
                }
            });
            if (ress.RetValue == 0) {
                if (setter.sysinfo.seversion == "SE") {

                    var resf = appflowutility.showFlow({
                        tableId: 'NF-project-index'
                        , evtobj: evtobj
                        , objType: 7//项目
                        , deptId: checkData[0].UserDeptId
                        , objCateId: checkData[0].CategoryId//类别
                        , objName: checkData[0].Name
                        , objCode: checkData[0].Code
                    });
                    if (resf === -1) {
                        layer.confirm('没有匹配上流程是否直接修改状态？', { icon: 3, title: '提示信息' }, function (cfindex) {
                            projectutility.updateSate({
                                tableId: 'NF-project-index'
                                , url: '/Project/ProjectManager/UpdateMoreField'
                                , evtobj: evtobj
                            });
                            layer.close(cfindex);

                        });
                    }
                }
                else {
                    projectutility.updateSate({
                        tableId: 'NF-project-index'
                        , url: '/Project/ProjectManager/UpdateMoreField'
                        , evtobj: evtobj
                    });
                }
            } else {
                return layer.alert(ress.Msg);
            }

        }, advQuery: function () {
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
                    table.reload("NF-project-index", {
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

    table.on('toolbar(NF-project-index)', function (obj) {
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
                    url: "/Project/ProjectManager/ExportExcel",
                    keyword: $("input[name=keyWord]").val()
                });
                break;
            case "advQuery"://高级查询
                active.advQuery();
                break;
            case "clear":
                soulTable.clearCache("NF-project-index")
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
                , content: '/Project/ProjectManager/Build?Id=' + obj.data.Id + "&rand=" + wooutil.getRandom()
                , maxmin: true
                , area: ['60%', '80%']
            // , area: [window.screen.width / 2 + 'px', window.screen.height / 2 + 'px'] //宽高
                , btn: ['确定', '取消']
                , btnAlign: 'c'
                , yes: function (index, layero) {

                    var iframeWindow = window['layui-layer-iframe' + index]
                        , submitID = 'NF-project-formsubmit'
                        , submit = layero.find('iframe').contents().find('#' + submitID);
                    //监听提交
                    iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                        var field = obj.field; //获取提交的字段
                        var fieldval = field.Name;
                        var fieldcode = field.Code;

                        var resname = wooutil.UniqueValObj({
                            url: '/Project/ProjectManager/CheckInputValExist',
                            fieldName: 'Name',
                            inputVal: fieldval,
                            currId: field.Id
                        });
                        if (resname) {
                            return layer.msg('此名称已经存在！');
                        }
                        var rescode = wooutil.UniqueValObj({
                            url: '/Project/ProjectManager/CheckInputValExist',
                            fieldName: 'Code',
                            inputVal: fieldcode,
                            currId: field.Id
                        });
                        if (rescode) {
                            return layer.msg('此编号已经存在！');
                        }
                        // wooutil.SaveForm(table,'/Project/ProjectManager/UpdateSave','NF-project-index', field, '保存成功', index);
                        wooutil.OpenSubmitForm({
                            url: '/Project/ProjectManager/UpdateSave',
                            table: table,
                            data: field,
                            tableId: 'NF-project-index',
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
                FuncCode: 'updateproject',
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
           , data: { perCode: "project", Id: obj.data.Id }
           , done: function (res) {
               if (res.Data.Delete == 0) {
                   //删除按钮
                   $(".layer-nf-nfskin .layui-layer-btn .layui-layer-btn1").addClass("layui-hide");
               }
               if (res.Data.Update == 0) {
                   //修改按钮
                   $(".layer-nf-nfskin .layui-layer-btn .layui-layer-btn0").addClass("layui-hide");
               }

           }
        });
    }
    /**
    *设置背景颜色
    **/
    function SetBtnBgColor() {
        $(".layer-nf-nfskin .layui-layer-btn .layui-layer-btn1").addClass("layui-bg-blue");
        $(".layer-nf-nfskin .layui-layer-btn .layui-layer-btn0").addClass("layui-bg-blue");

    }
    /**
    *打开查看页面
    **/
    function openview(obj) {

        layer.open({
            type: 2
                , title: '查看详情'
                , content: '/Project/ProjectManager/Detail?Id=' + obj.data.Id + "&rand=" + wooutil.getRandom()
                , maxmin: true
                , area: ['60%', '80%']
                , btnAlign: 'c'
                , skin: "layer-nf-nfskin"
                , btn: ['修改', '删除']// , area: [window.screen.width / 2 + 'px', window.screen.height / 2 + 'px'] //宽高
            , btn1: function (index, layero) {
                var success = function () {
                    layer.close(index);
                }
                customEdit(obj, success);
                // editFunc(obj, success);
            }, btn2: function (index, layero) {
                var suc = function () {
                    layer.close(index);
                }
                wooutil.deleteInfo({ tableId: "NF-project-index", data: obj, url: '/Project/ProjectManager/Delete', success: suc });

                return false;
            }
           , success: function (layero, index) {
               layer.load(0, { shade: false, time: 1 * 1000 });//2秒自动关闭
               layer.full(index);
               wooutil.openTip();
               SetBtnBgColor();
               DetailBtnShowAndHide(obj);
           }
        });
    };

    table.on('tool(NF-project-index)', function (obj) {
        if (obj.event === 'del') {//删除

            wooutil.deleteInfo({ tableId: "NF-project-index", data: obj, url: '/Project/ProjectManager/Delete' });
        }
        else if (obj.event === 'edit') {//修改客户
            customEdit(obj);
        } else if (obj.event === 'detail') {//查看详情
            var ress = wooutil.requestpremission({
                url: '/NfCommon/NfPermission/ViewPermission?rand=' + wooutil.getRandom(),
                data: {
                    FuncCode: 'queryprojectview',
                    ObjId: obj.data.Id
                }

            });
            if (ress.RetValue == 0) {
                openview(obj);
            } else {
                return layer.alert(ress.Msg);
            }


        }
    });
    /*********************************************************监听工具栏-end***************************************************************************/

    //解决高级查询选择下拉影响页面错乱问题
    form.on('select(CategoryId)', function (data) {
        $(document.body).trigger("click");
    });
    form.on('select(selectZt)', function (data) {
        $(document.body).trigger("click");
    });

    exports('projectmanagerIndex', {});
});