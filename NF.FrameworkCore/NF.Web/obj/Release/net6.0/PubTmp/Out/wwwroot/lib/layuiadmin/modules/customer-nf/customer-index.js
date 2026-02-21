/**
 @Name：客户
 @Author：dyk 20180918
 */
layui.define(['table', 'companyutility', 'form', 'appflowutility', 'dynamicCondition', 'soulTable'], function (exports) {
    var $ = layui.$
        , table = layui.table
        , setter = layui.setter
        , admin = layui.admin
        , form = layui.form
        , companyutility = layui.companyutility
        , appflowutility = layui.appflowutility
        , soulTable = layui.soulTable
        , dynamicCondition = layui.dynamicCondition;
    var loadindex = wooutil.loading();
    var $htcols = [
        { type: 'numbers', fixed: 'left' }
        , { type: 'checkbox', fixed: 'left' }
        , { field: 'Name', title: '名称', minWidth: 160, width: 220, templet: '#nameTpl', filter: true, fixed: 'left', sort: true }
        , { field: 'Code', title: '编号', width: 130, filter: true, sort: true }
        , { field: 'CompanyTypeClass', title: '类别', filter: true, width: 130 }
        , { field: 'FirstContact', title: '首要联系人', filter: true, width: 130 }
        , { field: 'FirstContactMobile', title: '移动电话', width: 130, hide: true }
        , { field: 'FirstContactTel', title: '办公电话', width: 130, hide: true }
        , { field: 'CstateDic', title: '状态', width: 120, templet: '#customerstateTpl', filter: true, unresize: true }
        , { field: 'CareditName', title: '信用等级', width: 130 }
        , { field: 'LevelName', title: '级别', width: 130 }
        , { field: 'Reserve1', title: '备用1', width: 130 }
        , { field: 'Reserve2', title: '备用2', width: 130 }
        , { field: 'PrincipalUserDisplayName', title: '负责人', filter: true, width: 130 }
        , { field: 'CountryName', title: '国家', width: 130, hide: true }
        , { field: 'ProvinceName', title: '省', width: 130, hide: true }
        , { field: 'CityName', title: '市', width: 130, hide: true }
        , { field: 'Trade', title: '行业', width: 130, hide: true }
        , { field: 'CreateDateTime', title: '建立日期', width: 130, filter: true, hide: true }
        , { field: 'Id', title: 'Id', width: 100, hide: true }
        , { field: 'WfStateDic', title: '流程状态', width: 130, templet: '#wfstateTpl' }
        , { field: 'WfCurrNodeName', title: '当前节点', width: 140, filter: true, }
        , { field: 'WfItemDic', title: '审批事项', width: 160 }
        , { title: '操作', width: 150, align: 'center', fixed: 'right', toolbar: '#table-customer-bar' }
    ];
    /**删除审批列**/
    appflowutility.SeCols({ htcols: $htcols });
 
    //列表
    table.render({
        elem: '#NF-customer-index'
        , url: '/Company/Customer/GetList?rand=' + wooutil.getRandom()
        , toolbar: '#toolcustomer'
        , defaultToolbar: ['filter']
        , cellMinWidth: 80
        , overflow: {
            type: 'tips'//内容超过设置
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
        , done: function (res) {   //返回数据执行回调函数
            soulTable.render(this)
            layer.close(loadindex);    //返回数据关闭loading
            $("input[name=keyWord]").val($("input[name=hide_keyWord]").val());
            $("input[name=hide_keyWord]").val("");
           
            var cate = layui.data(setter.tableName).userName;
            companyutility.stateEvent({ tableId: 'NF-customer-index' }, cate);//注册状态流转事件

        }

    });

    //监听表格排序
    table.on('sort(NF-customer-index)', function (obj) {
        table.reload('NF-customer-index', { //testTable是表格容器id
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
            , title: '新增客户'
            , content: '/Company/Customer/Build?Ctype=0'
            , maxmin: true
            , area: ['60%', '80%']
            , btn: ['确定', '取消']
            , btnAlign: 'c'
            , skin: "layer-ext-myskin"
            , yes: function (index, layero) {
                var iframeWindow = window['layui-layer-iframe' + index]
                    , submitID = 'NF-customer-formsubmit'
                    , submit = layero.find('iframe').contents().find('#' + submitID);
                //监听提交
                iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                    var field = obj.field; //获取提交的字段
                    var fieldval = obj.field.Name;
                    var fieldcode = obj.field.Code;

                    var resname = wooutil.UniqueValObj({
                        url: '/Company/Customer/CheckInputValExist',
                        fieldName: 'Name',
                        inputVal: fieldval,
                        currId: 0
                    });
                    if (resname) {
                        return layer.msg('此名称已经存在！');
                    }
                    var rescode = wooutil.UniqueValObj({
                        url: '/Company/Customer/CheckInputValExist',
                        fieldName: 'Code',
                        inputVal: fieldcode,
                        currId: 0
                    });
                    if (rescode) {
                        return layer.msg('此编号已经存在！');
                    }
                    wooutil.OpenSubmitForm({
                        url: '/Company/Customer/Save',
                        data: obj.field,
                        table: table,
                        index: index,
                        tableId: 'NF-customer-index'
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
                    FuncCode: 'addcustomer'
                }

            });
            if (ress.RetValue == 0) {
                openAdd();
            } else {
                return layer.alert(ress.Msg);
            }
        },
        batchdel: function () {//删除
            wooutil.deleteDatas({ tableId: 'NF-customer-index', table: table, url: '/Company/Customer/Delete' });
        },

         search: function () {//查询
            $("input[name=hide_keyWord]").val($("input[name=keyWord]").val());
             table.reload('NF-customer-index', {
                page: { curr: 1 }
                , where: {
                    keyWord: $("input[name=keyWord]").val()

                }
            });

        }
        //,
        //search: function () {//查询
        //    table.reload('NF-customer-index', {
        //        page: { curr: 1 }
        //        , where: {
        //            keyWord: $("input[name=keyWord]").val()

        //        }
        //    });
        //}

        , submitState: function (evtobj) {//提交状态
            if (setter.sysinfo.seversion == "SE") {
                var checkStatus = table.checkStatus("NF-customer-index")
                    , checkData = checkStatus.data; //得到选中的数据
                var resf = appflowutility.showFlow({
                    tableId: 'NF-customer-index'
                    , evtobj: evtobj
                    , objType: 0//客户
                    , deptId: checkData[0].UserDeptId
                    , objCateId: checkData[0].CompClassId//类别
                    , objName: checkData[0].Name
                    , objCode: checkData[0].Code
                });

                var ress = wooutil.requestpremission({
                    url: '/NfCommon/NfPermission/StateUpdate?rand=' + wooutil.getRandom(),
                    data: {
                        FuncCode: 'updatecustomerstate',
                        ObjId: checkStatus.data[0].Id
                    }
                });
                if (ress.RetValue == 0) {
                    if (resf === -1) {
                        layer.confirm('没有匹配上流程是否直接修改状态？', { icon: 3, title: '提示信息' }, function (cfindex) {
                            companyutility.updateSate({
                                tableId: 'NF-customer-index'
                                , url: '/Company/Customer/UpdateField'
                                , evtobj: evtobj
                            });
                            layer.close(cfindex);
                        });
                    } else {
                        //companyutility.updateSate({
                        //    tableId: 'NF-customer-index'
                        //    , url: '/Company/Customer/UpdateField'
                        //    , evtobj: evtobj
                        //});
                    }
                }
                else {
                    return layer.alert(ress.Msg);
                }
            } else {
                companyutility.updateSate({
                    tableId: 'NF-customer-index'
                    , url: '/Company/Customer/UpdateField'
                    , evtobj: evtobj
                });
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
                    table.reload("NF-customer-index", {
                        page: {
                            curr: 1 //重新加载当前页
                        }
                        , where: requestData
                    });
                    //layer.alert(JSON.stringify(requestData));

                }
                //    ,afterOpen: function (_dc) {
                //    //下拉框联动
                //        form.on('select(ProvinceId)', function (data) {
                //        var params = { province: data.value };
                //        $.post("/#(appName)/demo/dynamicCondition/getCityList", params, function (data) {
                //            if (data.code == 0) {
                //                var eleJq = _dc.getRowDivs("CityId")[0].eleJq;
                //                eleJq.html("<option value=''></option>");
                //                $.each(data.otherData.cityList, function (key, val) {
                //                    var option1 = $("<option>").val(val.value).text(val.label);
                //                    eleJq.append(option1);
                //                });
                //                form.render('select');
                //            }
                //        });
                //    })
                //    //如果省份有值，则需要初始化城市下拉框
                //        var provinceVal = _dc.getVal("ProvinceId");
                //     if (provinceVal) {
                //        var params = { province: provinceVal };
                //        $.post("/#(appName)/demo/dynamicCondition/getCityList", params, function (data) {
                //            if (data.code == 0) {
                //                var eleJq = _dc.getRowDivs("CityId")[0].eleJq;
                //                eleJq.html("<option value=''></option>");
                //                $.each(data.otherData.cityList, function (key, val) {
                //                    var option1 = $("<option>").val(val.value).text(val.label);
                //                    eleJq.append(option1);
                //                });
                //                eleJq.val(eleJq.attr("oldVal"));
                //                form.render('select');
                //            }
                //        });
                //    }

                //}

            });

            dynamicCondition.getInstance("complexInstance").open();


        }





    };

    table.on('toolbar(NF-customer-index)', function (obj) {

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
                wooutil.exportexcel(obj, { url: "/Company/Customer/ExportExcel", keyword: $("input[name=keyWord]").val() });
                break;
            case "clear":
                soulTable.clearCache("NF-customer-index")
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
            , content: '/Company/Customer/Build?Id=' + obj.data.Id + "&Ctype=0&rand=" + wooutil.getRandom()
            , maxmin: true
            , area: ['60%', '80%']
            // , area: [window.screen.width / 2 + 'px', window.screen.height / 2 + 'px'] //宽高
            , btn: ['确定', '取消']
            , btnAlign: 'c'
            , yes: function (index, layero) {

                var iframeWindow = window['layui-layer-iframe' + index]
                    , submitID = 'NF-customer-formsubmit'
                    , submit = layero.find('iframe').contents().find('#' + submitID);
                //监听提交
                iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                    var field = obj.field; //获取提交的字段
                    var fieldval = field.Name;
                    var fieldcode = field.Code;

                    var resname = wooutil.UniqueValObj({
                        url: '/Company/Customer/CheckInputValExist',
                        fieldName: 'Name',
                        inputVal: fieldval,
                        currId: field.Id
                    });
                    if (resname) {
                        return layer.msg('此名称已经存在！');
                    }
                    var rescode = wooutil.UniqueValObj({
                        url: '/Company/Customer/CheckInputValExist',
                        fieldName: 'Code',
                        inputVal: fieldcode,
                        currId: field.Id
                    });
                    if (rescode) {
                        return layer.msg('此编号已经存在！');
                    }
                    // wooutil.SaveForm(table,'/Company/Customer/UpdateSave','NF-customer-index', field, '保存成功', index);
                    wooutil.OpenSubmitForm({
                        url: '/Company/Customer/UpdateSave',
                        table: table,
                        data: field,
                        tableId: 'NF-customer-index',
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
                FuncCode: 'updatecustomer',
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
            , data: { perCode: "company", Id: obj.data.Id }
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
    *打开合同查看页面
    **/
    function openview(obj) {

        layer.open({
            type: 2
            , title: '查看详情'
            , content: '/Company/Customer/Detail?Id=' + obj.data.Id + "&rand=" + wooutil.getRandom()
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
                wooutil.deleteInfo({ tableId: "NF-customer-index", data: obj, url: '/Company/Customer/Delete', success: suc });

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

    table.on('tool(NF-customer-index)', function (obj) {
        if (obj.event === 'del') {//删除

            wooutil.deleteInfo({ tableId: "NF-customer-index", data: obj, url: '/Company/Customer/Delete' });
        }
        else if (obj.event === 'edit') {//修改客户
            customEdit(obj);
        } else if (obj.event === 'detail') {//查看详情
            var ress = wooutil.requestpremission({
                url: '/NfCommon/NfPermission/ViewPermission?rand=' + wooutil.getRandom(),
                data: {
                    FuncCode: 'querycustomerview',
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
    //form.on('select(CompClassId)', function (data) {
    //    $(document.body).trigger("click");
    //});
    //form.on('select(selectZt)', function (data) {
    //    $(document.body).trigger("click");
    //});


    exports('customerIndex', {});
});