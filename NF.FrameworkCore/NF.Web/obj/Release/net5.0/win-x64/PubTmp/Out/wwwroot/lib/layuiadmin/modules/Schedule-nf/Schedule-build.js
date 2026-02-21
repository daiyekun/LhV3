/**
*询价新建
*/
layui.define(['table', 'form', 'wordAddin', "tableSelect", "treeSelect"], function (exports) {
    var $ = layui.$
        , table = layui.table
        , setter = layui.setter
        , admin = layui.admin
        , form = layui.form
        , tableSelect = layui.tableSelect
        , treeSelect = layui.treeSelect
        // , subMetBuild = layui.subMetBuild
        , wordAddin = layui.wordAddin;
    var updatedata = [];//修改的数据
    var contId = wooutil.getUrlVar('Id');
    if (contId === undefined)
        contId = 0;



    /****************************选择表格注册区域-合同对方、项目、签约主体等选择-begin**************************************************************/
    layui.use(['selectnfitem', 'tableSelect'], function () {
        var tableSelect = layui.tableSelect
            , selectnfitem = layui.selectnfitem;
        selectnfitem.selectUserItemQ(
            {
                tableSelect: tableSelect,
                elem: '#TixingName',
                hide_elem: '#Tixing'

            });
        //指派给
        selectnfitem.selectUserItemQ(
            {
                tableSelect: tableSelect,
                elem: '#DesigneeName',
                hide_elem: '#Designee'

            });

        //跟踪者
        selectnfitem.selectUserItemQ(
            {
                tableSelect: tableSelect,
                elem: '#StalkerName',
                hide_elem: '#Stalker'

            });
        //项目
        //selectnfitem.selectProjZBItem(
        //    {
        //        tableSelect: tableSelect,
        //        elem: '#ScheduleDuixiangNames',
        //        hide_elem: '#ScheduleDuixiang',
        //        ctype: document.getElementById("ScheduleAttribution").value,
        //        selitem: true
        //    });
        //selectnfitem.selectProjZBItem(
        //    {
        //        tableSelect: tableSelect,
        //        elem: '#ScheduleDuixiangNames',
        //        hide_elem: '#ScheduleDuixiang',
        //        selitem: true
        //    });


    });
    /****************************选择表格注册区域-合同对方、项目、签约主体等选择-end**************************************************************/

    /*****************************日期、导航、字典注册-begin************************************************************/
    layui.use(['laydate', 'nfcontents', 'commonnf', 'treeSelect'], function () {
        var laydate = layui.laydate
            , nfcontents = layui.nfcontents
            , commonnf = layui.commonnf
            , treeSelect = layui.treeSelect;
        laydate.render({ elem: '#JhCreateDateTime',  trigger: 'click' });//时间
        laydate.render({ elem: '#JhCompleteDateTime', trigger: 'click' });//有效期

        //优先级
        commonnf.getdatadic({ dataenum: 35, selectEl: "#Priority" });
        //任务归属
        commonnf.getdatadic({ dataenum: 36, selectEl: "#ScheduleAttribution" });
        
        //目录
        nfcontents.render({ content: '#customernva' });


        //千分位字段
        var thodfields = ['AmountMoney', 'StampTax', 'EstimateAmount', 'AdvanceAmount'];
        $.each(thodfields, function (i, v) {
            $("input[name=" + v + "]").blur(function () {
                var _this = $(this);
                var temp = _this.val();
                _this.val(wooutil.numThodFormat(temp));
            })
        });
        //合同属性
        form.on('select(IsFramework)', function (data) {
            if (data.value == 1) {
                $(".IsFramework").removeClass("layui-hide").addClass("layui-show");
            } else {//标准合同
                $("input[name=AdvanceAmount]").val(0);
                $("input[name=EstimateAmount]").val(0);
                $(".IsFramework").removeClass("layui-show").addClass("layui-hide");
            }

        });
        //预收金额
        $("input[name=AdvanceAmount]").blur(function () {
            $("input[name=AmountMoney]").val(wooutil.numThodFormat($(this).val()));
        });
        /**绑定值以后的一些条件判断**/
        function SetValueHand(objval) {
            if (objval.IsFramework == 1) {//框架合同
                $(".IsFramework").removeClass("layui-hide").addClass("layui-show");
            }
            if (objval.ContDivision === "2") {//分包合同
                $("input[name=SumContName]").removeClass("layui-bg-gray").removeClass("layui-disabled");
            }

        }


        /**
        *修改
        **/
        if (contId !== "" && contId !== undefined && contId !== 0) {
            admin.req({
                url: '/Schedule/ScheduleManagement/ShowView',
                data: { Id: contId, rand: wooutil.getRandom() },
                done: function (res) {
                    form.val("NF-ContractCollection-Form", res.Data);
                }
            });
        } else {//新建时
            //BranchTree(null);
        }
        //清除部分下拉小笔头
        wooutil.selpen();
    });
    /*****************************日期、导航、字典注册-end************************************************************/

    /***********************任务列表-begin***************************************************************************************************/

    table.render({
        elem: '#NF-ScheduleList'
        , url: '/Schedule/ScheduleList/GetList?contId=' + contId + '&rand=' + wooutil.getRandom()
        , toolbar: '#toolScheduleList'
        , defaultToolbar: ['filter']
        , cols: [[
            { type: 'numbers', fixed: 'left' }
            , { type: 'checkbox', fixed: 'left' }
            , { field: 'Id', title: 'Id', width: 50, hide: true }
            , { field: 'ScheduleName', title: '任务名称', fixed: 'left', width: 200,}
            , { field: 'ScheduleAttributionDic', title: '任务归属', width: 120, }
            , { field: 'ScheduleDuixiangName', title: '任务对象', width: 120, }
            , { field: 'Description', title: '任务定义', width: 120, }
            , { field: 'Descriptionms', title: '任务描述', width: 120, }
            , { field: 'TixingName', title: '提醒人', width: 120, }
            , { field: 'DesigneeName', title: '执行人', width: 120, }
            , { field: 'StalkerName', title: '跟踪者', width: 120, }
            , { field: 'CreateUserName', title: '创建人', width: 120, }
            , { title: '操作', width: 150, align: 'center', fixed: 'right', toolbar: '#table-ScheduleListbar' }
        ]]
        , page: false
        , loading: true
        , height: setter.table.height_tab
        , limit: setter.table.limit_tab
    });

    var planXJQKEvent = {
        add: function () {
            /// <summary>列表头部-新增按钮</summary>
            layer.open({
                type: 2
                , title: '新建任务列表'
                , content: '/Schedule/ScheduleList/Build'
                // , maxmin: false
                , area: ['800px', '80%']
                , btn: ['确定', '取消']
                , btnAlign: 'c'
                , skin: "layer-ext-myskin"
                , yes: function (index, layero) {
                    var iframeWindow = window['layui-layer-iframe' + index]
                        , submitID = 'NF-ScheduleList-FormSubmit'
                        , submit = layero.find('iframe').contents().find('#' + submitID);
                    var Contractfiled = layero.find('iframe').contents().find('#ScheduleId');
                    Contractfiled.val(contId);
                    //监听提交
                    iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                        wooutil.OpenSubmitForm({
                            url: '/Schedule/ScheduleList/Save',
                            data: obj.field,
                            table: table,
                            index: index,
                            tableId: 'NF-ScheduleList'
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
            wooutil.deleteDatas({ tableId: 'NF-ScheduleList', url: '/Schedule/ScheduleList/Delete', nopage: true });
        },
        tooldel: function (obj) {
            /// <summary>列表操作栏-删除</summary>
            ///<param name='obj'>删除数据对象</param>
            wooutil.deleteInfo({ tableId: "NF-ScheduleList", data: obj, url: '/Schedule/ScheduleList/Delete', nopage: true });

        },
        tooledit: function (obj) {
            ///<summary>修改</summary>
            ///<param name='obj'>修改数据对象</param>
            layer.open({
                type: 2
                , title: '修改任务'
                , content: '/Schedule/ScheduleList/Build?Id=' + obj.data.Id + "&rand=" + wooutil.getRandom()
                //, maxmin: true
                , area: ['800px', '80%']
                , btn: ['确定', '取消']
                , btnAlign: 'c'
                , skin: "layer-ext-myskin"
                , yes: function (index, layero) {
                    var iframeWindow = window['layui-layer-iframe' + index]
                        , submitID = 'NF-ScheduleList-FormSubmit'
                        , submit = layero.find('iframe').contents().find('#' + submitID);
                    var Contractfiled = layero.find('iframe').contents().find('#ScheduleId');
                    Contractfiled.val(contId);
                    //监听提交
                    iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                        wooutil.OpenSubmitForm({
                            url: '/Schedule/ScheduleList/UpdateSave',
                            data: obj.field,
                            table: table,
                            index: index,
                            tableId: 'NF-ScheduleList'
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
    //合同计划资金头部工具栏
    table.on('toolbar(NF-ScheduleList)', function (obj) {
        switch (obj.event) {
            case 'add':
                planXJQKEvent.add();
                break;
            case 'batchdel':
                planXJQKEvent.batchdel();
                break;
            default:
                layer.alert("暂不支持（" + obj.event + "）");
                break;

        };
    });
    //列表操作栏
    table.on('tool(NF-ScheduleList)', function (obj) {
        var _data = obj.data;
        var newdata = {};
        switch (obj.event) {
            case 'del':
                planXJQKEvent.tooldel(obj);
                break;
            case 'edit':
                planXJQKEvent.tooledit(obj);
                break;
            default:
                layer.alert("暂不支持（" + obj.event + "）");
                break;
        }
    });

    /***********************任务列表-end***************************************************************************************************/

    /***********************进度管理附件信息-begin***************************************************************************************************/
    table.render({
        elem: '#NF-ScheduleAttachment'
        , url: '/Schedule/ScheduleManagementAttachment/GetList?contId=' + contId + '&rand=' + wooutil.getRandom()
        , toolbar: '#toolScheduleAttachment'
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
            , { title: '操作', width: 220, align: 'center', fixed: 'right', toolbar: '#tabl-ScheduleAttachmentbar' }
        ]]
        , page: false
        , loading: true
        , height: setter.table.height_tab
        , limit: setter.table.limit_tab
        // , limits: setter.table.limits

    });
    var attacXJFJEvent = {
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
                , content: '/Schedule/ScheduleManagementAttachment/Build'
                // , maxmin: false
                , area: ['800px', '80%']
                , btn: ['确定', '取消']
                , btnAlign: 'c'
                , skin: "layer-ext-myskin"
                , yes: function (index, layero) {
                    var iframeWindow = window['layui-layer-iframe' + index]
                        , submitID = 'NF-ScheduleAttachment-FormSubmit'
                        , submit = layero.find('iframe').contents().find('#' + submitID);
                    var Contractfiled = layero.find('iframe').contents().find('#SchedulemId');
                    Contractfiled.val(contId);
                    //监听提交
                    iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                        wooutil.OpenSubmitForm({
                            url: '/Schedule/ScheduleManagementAttachment/Save',
                            data: obj.field,
                            table: table,
                            index: index,
                            tableId: 'NF-ScheduleAttachment'
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
            wooutil.deleteDatas({ tableId: 'NF-ScheduleAttachment', table: table, url: '/Schedule/ScheduleManagementAttachment/Delete', nopage: true });
        },
        tooldownload: function (obj) {
            wooutil.download({
                Id: obj.data.Id,
                folder: 13//标识招标附件
            });
        },
        tooldel: function (obj) {
            /// <summary>列表操作栏-删除</summary>
            ///<param name='obj'>删除数据对象</param>
            wooutil.deleteInfo({ tableId: "NF-ScheduleAttachment", data: obj, url: '/Schedule/ScheduleManagementAttachment/Delete', nopage: true });
        },
        tooledit: function (obj) {
            ///<summary>修改</summary>
            ///<param name='obj'>修改数据对象</param>
            layer.open({
                type: 2
                , title: '修改附件'
                , content: '/Schedule/ScheduleManagementAttachment/Build?Id=' + obj.data.Id + "&rand=" + wooutil.getRandom()
                //, maxmin: true
                , area: ['800px', '80%']
                , btn: ['确定', '取消']
                , btnAlign: 'c'
                , skin: "layer-ext-myskin"
                , yes: function (index, layero) {
                    var iframeWindow = window['layui-layer-iframe' + index]
                        , submitID = 'NF-ScheduleAttachment-FormSubmit'
                        , submit = layero.find('iframe').contents().find('#' + submitID);
                    var Contractfiled = layero.find('iframe').contents().find('#SchedulemId');
                    Contractfiled.val(contId);
                    //监听提交
                    iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                        wooutil.OpenSubmitForm({
                            url: '/Schedule/ScheduleManagementAttachment/UpdateSave',
                            data: obj.field,
                            table: table,
                            index: index,
                            tableId: 'NF-ScheduleAttachment'
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
    table.on('toolbar(NF-ScheduleAttachment)', function (obj) {
        switch (obj.event) {
            case 'add':
                attacXJFJEvent.add();
                break;
            case 'batchdel':
                attacXJFJEvent.batchdel();
                break
            case 'LAYTABLE_COLS'://选择列-系统默认不管
                break;
            default:
                layer.alert("暂不支持（" + obj.event + "）");
                break;

        };
    });
    //列表操作栏
    table.on('tool(NF-ScheduleAttachment)', function (obj) {
        var _data = obj.data;
        switch (obj.event) {
            case 'del':
                attacXJFJEvent.tooldel(obj);
                break;
            case 'edit':
                attacXJFJEvent.tooledit(obj);
                break;
            case 'download'://下载
                attacXJFJEvent.tooldownload(obj);
                break;
            default:
                layer.alert("暂不支持（" + obj.event + "）");
                break;
        }
    });

    /***********************合同附件信息-end***************************************************************************************************/

    form.on('select(ScheduleAttribution)', function (data) {

        layui.use(['selectnfitem', 'tableSelect'], function () {
            var tableSelect = layui.tableSelect
                , selectnfitem = layui.selectnfitem;
                $('.tableSelect').remove();
            $(".tableSelect").removeClass();
           

            //selectnfitem.render('tableSelect');
            //var name = document.getElementById("ScheduleAttribution");
            var name = $("#ScheduleAttribution option:selected").text();
          
         
            var rurl = '';
            switch (name) {                               
                case "客户":
                    selectnfitem.selectCompItem(
                        {
                            tableSelect: tableSelect,
                            elem: '#ScheduleDuixiangName',
                            hide_elem: '#ScheduleDuixiang',
                            ctype: 0,
                            lhide: 0
                        });
                    rurl = '/Company/Customer/GetList?selitem=false';
                    break;
                case "供应商":
                    selectnfitem.selectCompItem(
                        {
                            tableSelect: tableSelect,
                            elem: '#ScheduleDuixiangName',
                            hide_elem: '#ScheduleDuixiang',
                            ctype: 1,
                            lhide: 0
                        });
                    rurl = '/Company/Supplier/GetList?selitem=false';
                    break;
                case "其他对方":
                    selectnfitem.selectCompItem(
                        {
                            tableSelect: tableSelect,
                            elem: '#ScheduleDuixiangName',
                            hide_elem: '#ScheduleDuixiang',
                            ctype: 2,
                            lhide: 0
                        });
                    rurl = '/Company/Other/GetList?selitem=false';
                    break;
                case "项目":
                    selectnfitem.selectProjZBItem(
                        {
                            tableSelect: tableSelect,
                            elem: '#ScheduleDuixiangName',
                            hide_elem: '#ScheduleDuixiang',
                        });
                    rurl = '/Project/ProjectManager/GetList1?selitem=false';
                    break;
                case "收款合同":
                    selectnfitem.selectColleCtionItem(
                        {
                            tableSelect: tableSelect,
                            elem: '#ScheduleDuixiangName',
                            hide_elem: '#ScheduleDuixiang',
                        });
                    rurl = '/Contract/ContractCollection/GetSelectList?selitem=false';
                    break;
                case "付款合同":
                    selectnfitem.selectPayCtionItem(
                        {
                            tableSelect: tableSelect,
                            elem: '#ScheduleDuixiangName',
                            hide_elem: '#ScheduleDuixiang',
                        });
                    rurl = '/Contract/ContractPayment/GetSelectList?selitem=false';
                    break;
                case "招标":
                    selectnfitem.selectZbItem(
                        {
                            tableSelect: tableSelect,
                            elem: '#ScheduleDuixiangName',
                            hide_elem: '#ScheduleDuixiang',
                        });
                    rurl = '/Tender/TenderInfo/GetList?selitem=false';
                    break;
                case "询价":
                    selectnfitem.selectXjItem(
                        {
                            tableSelect: tableSelect,
                            elem: '#ScheduleDuixiangName',
                            hide_elem: '#ScheduleDuixiang',
                        });
                    rurl = '/Inquiry/Inquiry/GetList?selitem=false';
                    break;
                case "洽谈":
                    selectnfitem.selectYtItem(
                        {
                            tableSelect: tableSelect,
                            elem: '#ScheduleDuixiangName',
                            hide_elem: '#ScheduleDuixiang',
                        });
                    rurl = '/Questioning/Questioning/GetList?selitem=false';
                    break;
                case "收票":
                    selectnfitem.selectSPItem(
                        {
                            tableSelect: tableSelect,
                            elem: '#ScheduleDuixiangName',
                            hide_elem: '#ScheduleDuixiang',
                        });
                    rurl = '/Finance/ContInvoice/GetList?fType=1&selitem=false';
                    break;
                case "开票":
                    selectnfitem.selectKPItem(
                        {
                            tableSelect: tableSelect,
                            elem: '#ScheduleDuixiangName',
                            hide_elem: '#ScheduleDuixiang',
                        });
                    rurl = '/Finance/ContInvoice/GetList?fType=0&selitem=false';
                    break;
                case "实际收款":
                    selectnfitem.selectSJSKItem(
                        {
                            tableSelect: tableSelect,
                            elem: '#ScheduleDuixiangName',
                            hide_elem: '#ScheduleDuixiang',
                        });
                    rurl = '/Finance/ContActualFinance/GetList?fType=0&selitem=false';
                    break;
                case "实际付款":
                    selectnfitem.selectSJFKItem(
                        {
                            tableSelect: tableSelect,
                            elem: '#ScheduleDuixiangName',
                            hide_elem: '#ScheduleDuixiang',
                        });
                    rurl = '/Finance/ContActualFinance/GetList?fType=1&selitem=false';
                    break;
                case "收款合同文本":
                    selectnfitem.selectSKcontextItem(
                        {
                            tableSelect: tableSelect,
                            elem: '#ScheduleDuixiangName',
                            hide_elem: '#ScheduleDuixiang',
                        });
                    rurl = '/ContractDraft/ContText/GetMainList?requestType=0&selitem=false';
                    break;
                case "付款合同文本":
                    selectnfitem.selectFKcontextItem(
                        {
                            tableSelect: tableSelect,
                            elem: '#ScheduleDuixiangName',
                            hide_elem: '#ScheduleDuixiang',
                        });
                    rurl = '/ContractDraft/ContText/GetMainList?requestType=1&selitem=false';
                    break;
                //case "收款合同标的":
                //    selectnfitem.selectSKbdItem(
                //        {
                //            tableSelect: tableSelect,
                //            elem: '#ScheduleDuixiangName',
                //            hide_elem: '#ScheduleDuixiang',
                //        });
                //    break;
                //case "付款合同标的":
                //    selectnfitem.selectFKbdItem(
                //        {
                //            tableSelect: tableSelect,
                //            elem: '#ScheduleDuixiangName',
                //            hide_elem: '#ScheduleDuixiang',
                //        });
                //    break;
                //case "收款标的交付明细":
                //    selectnfitem.selectCompItem(
                //        {
                //            tableSelect: tableSelect,
                //            elem: '#ScheduleDuixiangName',
                //            hide_elem: '#ScheduleDuixiang',
                //        });
                //   // rurl = '/ContractDraft/ContText/GetMainList?requestType=1&selitem=false';
                //    break;
                //case "付款标的交付明细":
                //    selectnfitem.selectCompItem(
                //        {
                //            tableSelect: tableSelect,
                //            elem: '#ScheduleDuixiangName',
                //            hide_elem: '#ScheduleDuixiang',
                //        });
                //    break;
                default:
                    layer.alert("暂不支持（" + name + "）");
                    break;
                    
                    
            }

            $("#ScheduleDuixiangName").attr("selrw", rurl);
           
            //项目
            //selectnfitem.selectProjZBItem(
            //    {
            //        tableSelect: tableSelect,
            //        elem: '#ScheduleDuixiangNames',
            //        hide_elem: '#ScheduleDuixiang',
            //        ctype: document.getElementById("ScheduleAttribution").value,
            //        selitem: true
            //    });


        });
        

    })

    if (setter.sysinfo.Mb !== "Mb") {
        $(".mb").addClass("layui-hide");
    }
    exports('ScheduleBuild', {});
});