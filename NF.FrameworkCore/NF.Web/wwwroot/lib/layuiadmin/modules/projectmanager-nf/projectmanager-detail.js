/**
*项目查看
*/
layui.define(['table', 'form', 'viewPageEdit'], function (exports) {
    var $ = layui.$
        , table = layui.table
        , setter = layui.setter
        , admin = layui.admin
        , form = layui.form
        , viewPageEdit = layui.viewPageEdit
        ;
    var ProjectId = wooutil.getUrlVar('Id');
    var tabtoolvisable = true;
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
    /**绑定主信息***/
    if (ProjectId !== "" && ProjectId !== undefined) {
        admin.req({
            url: '/Project/ProjectManager/ShowView',
            async: false,//取消异步
            data: { Id: ProjectId, rand: wooutil.getRandom() },
            done: function (res) {
                if (res.Data.WfState === 1) {//审批中
                    tabtoolvisable = false;

                }
                form.val("NF-Project-DetailForm", res.Data);
                //页面绑定完毕以后延迟执行修改次要字段
                seteditsecfiled();


            }
        });

    }
    /***********************项目备忘-begin***************************************************************************************************/
    var projdesctbr = (tabtoolvisable ? '#toolprojDescription' : tabtoolvisable);
    var projdesccoltbr = (tabtoolvisable ? '#table-projDescriptionbar' : tabtoolvisable);

    table.render({
        elem: '#NF-projDescription'
        , url: '/Project/ProjDescription/GetList?projectId=' + ProjectId + '&rand=' + wooutil.getRandom()
        , toolbar: projdesctbr
        , defaultToolbar: ['filter']
        , cols: [[
            { type: 'numbers', fixed: 'left' }
            , { type: 'checkbox', fixed: 'left' }
            , { field: 'Id', title: 'Id', width: 50, hide: true }
            , { field: 'Pitem', title: '说明事项', width: 200, fixed: 'left' }
            , { field: 'ProjContent', title: '内容', width: 350 }
            , { field: 'CreateUserName', title: '提交人', width: 120 }
            , { field: 'CreateDateTime', title: '提交日期', width: 120 }
            , { title: '操作', width: 150, align: 'center', fixed: 'right', toolbar: projdesccoltbr }
        ]]
        , page: false
        , loading: true
        , height: setter.table.height_tab
        , limit: setter.table.limit_tab
        // , limits: setter.table.limits

    });
    var contactEvent = {
        add: function () {
            /// <summary>列表头部-新增按钮</summary>
            layer.open({
                type: 2
                , title: '新建项目备忘'
                , content: '/Project/ProjDescription/Build'
                // , maxmin: true
                , area: ['60%', '80%']
                , btn: ['确定', '取消']
                , btnAlign: 'c'
                , skin: "layer-ext-myskin"
                , yes: function (index, layero) {
                    var iframeWindow = window['layui-layer-iframe' + index]
                        , submitID = 'NF-ProjDescription-FormSubmit'
                        , submit = layero.find('iframe').contents().find('#' + submitID);
                    var Projectfiled = layero.find('iframe').contents().find('#ProjectId');
                    Projectfiled.val(ProjectId);
                    //监听提交
                    iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                        wooutil.OpenSubmitForm({
                            url: '/Project/ProjDescription/Save',
                            data: obj.field,
                            table: table,
                            index: index,
                            tableId: 'NF-projDescription'
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
            wooutil.deleteDatas({ tableId: 'NF-projDescription', url: '/Project/ProjDescription/Delete', nopage: true });
        },
        tooldel: function (obj) {
            /// <summary>列表操作栏-删除</summary>
            ///<param name='obj'>删除数据对象</param>
            wooutil.deleteInfo({ tableId: "NF-projDescription", data: obj, url: '/Project/ProjDescription/Delete', nopage: true });

        },
        tooledit: function (obj) {
            ///<summary>修项目备忘</summary>
            ///<param name='obj'>修改数据对象</param>
            layer.open({
                type: 2
                , title: '修项目备忘'
                , content: '/Project/ProjDescription/Build?Id=' + obj.data.Id + "&rand=" + wooutil.getRandom()
                //, maxmin: true
                , area: ['60%', '80%']
                , btn: ['确定', '取消']
                , btnAlign: 'c'
                , skin: "layer-ext-myskin"
                , yes: function (index, layero) {
                    var iframeWindow = window['layui-layer-iframe' + index]
                        , submitID = 'NF-ProjDescription-FormSubmit'
                        , submit = layero.find('iframe').contents().find('#' + submitID);
                    var Projectfiled = layero.find('iframe').contents().find('#ProjectId');
                    Projectfiled.val(ProjectId);
                    //监听提交
                    iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                        wooutil.OpenSubmitForm({
                            url: '/Project/ProjDescription/UpdateSave',
                            data: obj.field,
                            table: table,
                            index: index,
                            tableId: 'NF-projDescription'
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
    //项目备忘头部工具栏
    table.on('toolbar(NF-projDescription)', function (obj) {
        switch (obj.event) {
            case 'add':
                contactEvent.add();
                break;
            case 'batchdel':
                contactEvent.batchdel();
                break
            case 'LAYTABLE_COLS'://选择列-系统默认不管
                break;
            default:
                layer.alert("暂不支持（" + obj.event + "）");
                break;

        };
    });
    //列表操作栏
    table.on('tool(NF-projDescription)', function (obj) {
        var _data = obj.data;
        switch (obj.event) {
            case 'del':
                contactEvent.tooldel(obj);
                break;
            case 'edit':
                contactEvent.tooledit(obj);
                break;
            default:
                layer.alert("暂不支持（" + obj.event + "）");
                break;
        }
    });

    /***********************项目备忘-end***************************************************************************************************/

    /***********************项目时间表-begin***************************************************************************************************/
    var projschebr = (tabtoolvisable ? '#toolprojSchedule' : tabtoolvisable);
    var projschecoltbr = (tabtoolvisable ? '#table-projSchedulebar' : tabtoolvisable);
    table.render({
        elem: '#NF-projSchedule'
        , url: '/Project/ProjSchedule/GetList?projectId=' + ProjectId + '&rand=' + wooutil.getRandom()
        , toolbar: projschebr
        , defaultToolbar: ['filter']
        , cols: [[
            { type: 'numbers', fixed: 'left' },
            { type: 'checkbox', fixed: 'left' },
            , { field: 'Id', title: 'Id', width: 50, hide: true }
            , { field: 'Pitem', title: '事项', width: 400 }
            , { field: 'PlanBeginDateTime', title: '计划开始时间', width: 140 }
            , { field: 'PlanCompleteDateTime', title: '计划开始时间', width: 140 }
            , { field: 'Remark', title: '备注', width: 150 }
            , { title: '操作', width: 150, align: 'center', fixed: 'right', toolbar: projschecoltbr }
        ]]
        , page: false
        , loading: true
        , height: setter.table.height_tab
        , limit: setter.table.limit_tab
        // , limits: setter.table.limits

    });
    var descEvent = {
        add: function () {
            /// <summary>列表头部-新增按钮</summary>
            layer.open({
                type: 2
                , title: '新建备忘录'
                , content: '/Project/ProjSchedule/Build'
                //, maxmin: true
                , area: ['60%', '80%']
                , btn: ['确定', '取消']
                , btnAlign: 'c'
                , skin: "layer-ext-myskin"
                , yes: function (index, layero) {
                    var iframeWindow = window['layui-layer-iframe' + index]
                        , submitID = 'NF-ProjSchedule-FormSubmit'
                        , submit = layero.find('iframe').contents().find('#' + submitID);
                    var Projectfiled = layero.find('iframe').contents().find('#ProjectId');
                    Projectfiled.val(ProjectId);
                    //监听提交
                    iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                        wooutil.OpenSubmitForm({
                            url: '/Project/ProjSchedule/Save',
                            data: obj.field,
                            table: table,
                            index: index,
                            tableId: 'NF-projSchedule'
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
            wooutil.deleteDatas({ tableId: 'NF-projSchedule', url: '/Project/ProjSchedule/Delete', nopage: true });
        },
        tooldel: function (obj) {
            /// <summary>列表操作栏-删除</summary>
            ///<param name='obj'>删除数据对象</param>
            wooutil.deleteInfo({ tableId: "NF-projSchedule", data: obj, url: '/Project/ProjSchedule/Delete', nopage: true });

        },
        tooledit: function (obj) {
            ///<summary>修改备忘录</summary>
            ///<param name='obj'>修改数据对象</param>
            layer.open({
                type: 2
                , title: '修改备忘录'
                , content: '/Project/ProjSchedule/Build?Id=' + obj.data.Id + "&rand=" + wooutil.getRandom()
                // , maxmin: true
                , area: ['60%', '80%']
                , btn: ['确定', '取消']
                , btnAlign: 'c'
                , skin: "layer-ext-myskin"
                , yes: function (index, layero) {
                    var iframeWindow = window['layui-layer-iframe' + index]
                        , submitID = 'NF-ProjSchedule-FormSubmit'
                        , submit = layero.find('iframe').contents().find('#' + submitID);
                    var Projectfiled = layero.find('iframe').contents().find('#ProjectId');
                    Projectfiled.val(ProjectId);
                    //监听提交
                    iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                        wooutil.OpenSubmitForm({
                            url: '/Project/ProjSchedule/UpdateSave',
                            data: obj.field,
                            table: table,
                            index: index,
                            tableId: 'NF-projSchedule'
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
    //项目备忘头部工具栏
    table.on('toolbar(NF-projSchedule)', function (obj) {
        switch (obj.event) {
            case 'add':
                descEvent.add();
                break;
            case 'batchdel':
                descEvent.batchdel();
                break;
            case 'LAYTABLE_COLS'://选择列-系统默认不管
                break;
            default:
                layer.alert("暂不支持（" + obj.event + "）");
                break;

        };
    });
    //列表操作栏
    table.on('tool(NF-projSchedule)', function (obj) {
        var _data = obj.data;
        switch (obj.event) {
            case 'del':
                descEvent.tooldel(obj);
                break;
            case 'edit':
                descEvent.tooledit(obj);
                break;
            default:
                layer.alert("暂不支持（" + obj.event + "）");
                break;
        }
    });

    /***********************项目时间表-end***************************************************************************************************/

    /***********************附件信息-begin***************************************************************************************************/

    var projattach = (tabtoolvisable ? '#toolprojAttachment' : tabtoolvisable);
    var projattachcoltbr = (tabtoolvisable ? '#table-projAttachmentbar' : tabtoolvisable);
    table.render({
        elem: '#NF-projAttachment'
        , url: '/Project/ProjAttachment/GetList?ProjectId=' + ProjectId + '&rand=' + wooutil.getRandom()
        , toolbar: projattach
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
            , { title: '操作', width: 220, align: 'center', fixed: 'right', toolbar: projattachcoltbr }
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
                , content: '/Project/ProjAttachment/Build'
                // , maxmin: false
                , area: ['800px', '80%']
                , btn: ['确定', '取消']
                , btnAlign: 'c'
                , skin: "layer-ext-myskin"
                , yes: function (index, layero) {
                    var iframeWindow = window['layui-layer-iframe' + index]
                        , submitID = 'NF-ProjAttachment-FormSubmit'
                        , submit = layero.find('iframe').contents().find('#' + submitID);
                    var Projectfiled = layero.find('iframe').contents().find('#ProjectId');
                    Projectfiled.val(ProjectId);
                    //监听提交
                    iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                        wooutil.OpenSubmitForm({
                            url: '/Project/ProjAttachment/Save',
                            data: obj.field,
                            table: table,
                            index: index,
                            tableId: 'NF-projAttachment'
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
            wooutil.deleteDatas({ tableId: 'NF-projAttachment', table: table, url: '/Project/ProjAttachment/Delete', nopage: true });
        },
        tooldownload: function (obj) {
            wooutil.download({
                url: '/NfCommon/NfAttachment/Download',
                Id: obj.data.Id,
                folder: 4//标识客户附件


            });
        },
        tooldel: function (obj) {
            /// <summary>列表操作栏-删除</summary>
            ///<param name='obj'>删除数据对象</param>
            wooutil.deleteInfo({ tableId: "NF-projAttachment", data: obj, url: '/Project/ProjAttachment/Delete', nopage: true });

        },
        tooledit: function (obj) {
            ///<summary>修改</summary>
            ///<param name='obj'>修改数据对象</param>
            layer.open({
                type: 2
                , title: '修改附件'
                , content: '/Project/ProjAttachment/Build?Id=' + obj.data.Id + "&rand=" + wooutil.getRandom()
                //, maxmin: true
                , area: ['800px', '80%']
                , btn: ['确定', '取消']
                , btnAlign: 'c'
                , skin: "layer-ext-myskin"
                , yes: function (index, layero) {
                    var iframeWindow = window['layui-layer-iframe' + index]
                        , submitID = 'NF-ProjAttachment-FormSubmit'
                        , submit = layero.find('iframe').contents().find('#' + submitID);
                    var Projectfiled = layero.find('iframe').contents().find('#ProjectId');
                    Projectfiled.val(ProjectId);
                    //监听提交
                    iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                        wooutil.OpenSubmitForm({
                            url: '/Project/ProjAttachment/UpdateSave',
                            data: obj.field,
                            table: table,
                            index: index,
                            tableId: 'NF-projAttachment'
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
    table.on('toolbar(NF-projAttachment)', function (obj) {
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
    table.on('tool(NF-projAttachment)', function (obj) {
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

    /***********************附件信息-end***************************************************************************************************/
    /**************************修改次要字段begin*******************************************************/
    function seteditsecfiled() {
        var ress = wooutil.requestpremission({
            url: '/NfCommon/NfPermission/SecFieldUpatePremission?rand=' + wooutil.getRandom(),
            data: {
                FuncCode: 'updateprojectminor',
                ObjId: ProjectId
            }

        });
        if (ress.RetValue == 0) {//有权限
            var updateFields =
                ["Reserve1", "Reserve2"];
            $.each(updateFields, function (index, fieldId) {
                switch (fieldId) {
                    case "Reserve1":
                    case "Reserve2":
                        {//都是文本编辑框
                            viewPageEdit.render({
                                elem: '#' + fieldId,
                                edittype: 'text',
                                objid: ProjectId,
                                fieldname: fieldId,
                                verify: 'required',
                                ckEl: '#Name',
                                url: '/Project/ProjectManager/UpdateField'

                            });
                        }

                        break;
                }
            });

        } else {
            viewPageEdit.noUpShow(updateFields);
        }
    }
    /************************************修改次要字段-end*************************************************/

    /*******************************资金统计-begin******************************************************************************************************************/
    if (ProjectId !== "" && ProjectId !== undefined) {
        admin.req({
            url: '/Project/ProjectManager/GetFundStatistics',
            //async: false,//取消异步
            data: { projId: ProjectId, rand: wooutil.getRandom() },
            done: function (res) {

                form.val("Nf-Proj-TongJiForm", res.Data);



            }
        });

    }

    /******************************资金统计-end**************************************************************************************/


    /***********************相关收款合同-begin*****************************************************************************************/
    function renderConts(tabId, ProjectId, finctype) {

        table.render({
            elem: '#' + tabId
            , url: '/Project/ProjectManager/GetContsByProjId?projId=' + ProjectId + '&fincType=' + finctype + '&rand=' + wooutil.getRandom()
            //, toolbar: '#toolcustomer_CompDescription'
            , defaultToolbar: ['filter']
            , cols: [[
                { type: 'numbers', fixed: 'left' }
                , { type: 'checkbox', fixed: 'left' }
                , { field: 'Name', title: '合同名称', width: 220, templet: '#nameTpl'}
                , { field: 'Code', title: '合同编号', width: 130 }
                , { field: 'ContAmThod', title: '合同金额', width: 140 }
                , { field: 'CompName', title: '合同对方', width: 160, templet: '#compTpl' }
                , { field: 'CurrName', title: '币种', width: 130 }
                , { field: 'ContTypeName', title: '合同类别', width: 140 }
                , { field: 'ContStateDic', title: '合同状态', width: 140 }
                , { field: 'FinceTypeName', title: '资金性质', width: 140 }
                , { field: 'Id', title: 'Id', width: 50, hide: true }

            ]]
            , page: false
            , loading: true
            , height: setter.table.height_tab
            , limit: setter.table.limit_tab


        });
    }
    renderConts('NF-proj-SkContracts', ProjectId, 0);//收款
    renderConts('NF-proj-FkContracts', ProjectId, 1);//付款
    /***********************相关收款合同-end********************************************************************************************/
    /**
 *打开查看页面
 **/
    function openview(obj) {
        parent.layer.open({
            type: 2
            , title: '查看详情'
            , content: '/contract/ContractCollection/Detail?Id=' + obj.data.Id + "&Ftype=" + obj.data.FinanceType + "&rand=" + wooutil.getRandom()
            , maxmin: true
            , area: ['60%', '80%']
            , btnAlign: 'c'
            , skin: "layer-nf-nfskin"
            , btn: ['修改', '删除', '变更']
            , btn1: function (index, layero) {

                if (obj.data.ContState === 0 && obj.data.ModificationTimes === 0) {
                    var success = function () {
                        layer.close(index);
                    }
                    customEdit(obj, success);
                } else {
                    layer.alert("只有未执行且没有变更过的合同才允许修改！");
                    return false;

                }

            }, btn2: function (index, layero) {
                if (obj.data.ContState === 0 && obj.data.ModificationTimes === 0) {
                    var suc = function () {
                        layer.close(index);
                    }
                    wooutil.deleteInfo({ tableId: "NF-ContractCollection-Index", data: obj, url: '/Contract/ContractCollection/Delete', success: suc });

                    return false;
                } else {
                    layer.alert("只有未执行且没有变更过的合同才允许删除！");
                    return false;
                }
            },
            btn3: function (index, layero) {
                if (obj.data.ContState === 1 || (obj.data.ContState == 0 && obj.data.ModificationTimes > 0)) {
                    contractChange(obj, index);
                } else {
                    layer.alert("只有执行中的合同才允许变更！");
                    return false;//阻止关闭
                }
            }
            , success: function (layero, index) {
                parent.layer.load(0, { shade: false, time: 1 * 1000 });//2秒自动关闭
                parent.layer.full(index);
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
        parent.layer.open({
            type: 2
            , title: '查看详情'
            , content: '/Company/Customer/Detail?Id=' + obj.data.CompId + "&rand=" + wooutil.getRandom()
            , maxmin: true
            , area: ['60%', '80%']
            , btnAlign: 'c'
            , skin: "layer-nf-nfskin"
            , btn: ['关闭']
            , success: function (layero, index) {
                parent.layer.load(0, { shade: false, time: 1 * 1000 });
                parent.layer.full(index);
                wooutil.openTip();

            }
        });
    };

    table.on('tool(NF-proj-SkContracts)', function (obj) {
        switch (obj.event) {
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
    table.on('tool(NF-proj-FkContracts)', function (obj) {
        switch (obj.event) {
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

    exports('projectManagerDetail', {});
});