/**
*收票建立
**/
layui.define(['form', 'laydate', 'commonnf','table'], function (exports) {
    var $ = layui.$
    , setter = layui.setter
    , admin = layui.admin
    , laydate = layui.laydate
    , commonnf = layui.commonnf
        , form = layui.form
        , table = layui.table;
    var Id = wooutil.getUrlVar('Id');
    //合同ID
    var contId = wooutil.getUrlVar('contId');
    $("#ContId").val(contId);
    var isFlow = wooutil.getUrlVar('isFlow');//是流程列表过来修改:1
    //发票类型
    commonnf.getdatadic({ dataenum: 19, selectEl: "#InType" });
    //开票时间
    laydate.render({ elem: '#MakeOutDateTime' });

    //输入千分位
    $("input[name=AmountMoney]").blur(function () {

        $(this).val(wooutil.numThodFormat($(this).val()));
    });
    //修改赋值
    if (Id !== "" && Id !== undefined && Id !== 0) {
       
        admin.req({
            url: '/Finance/ContInvoice/ShowView',
            data: { Id: Id, rand: wooutil.getRandom() },
            done: function (res) {
              
                form.val("NF-ContInvoiceForm", res.Data);
                $("input[name=AmountMoney]").val(res.Data.AmountMoneyThod);
                $("input[name=InState]").val(res.Data.InState);
            }
        });
     
    }

    //合同基本信息绑定
    admin.req({
        url: '/Contract/ContractPayment/ShowView',
        data: { Id: contId, rand: wooutil.getRandom() },
        done: function (res) {
            form.val("NF-ContInfoForm", res.Data);
        }
    });
    //按钮相关事件
    layui.use('table', function () {
        var $index = parent.layer.getFrameIndex(window.name);
        var table = layui.table;
        var active = {
            save: function () {
                var submit = $("#NF-ContInvoice-FormSubmit");
                form.on('submit(NF-ContInvoice-FormSubmit)', function (obj) {
                    var result = 0;
                    admin.req({
                        async: false,//取消异步
                        url: "/Finance/ContInvoice/CheckContJe",
                        data: obj.field,
                        type: 'POST',
                        done: function (res) {
                            result = res.RetValue;

                        }

                    });
                    if (result === 1)
                    {
                        return layer.alert('已建立发票金额超出合同金额！', { icon: 5 });
                   
                    }
                    var dataId = parseInt(Id);
                    var _url = '/Finance/ContInvoice/Save';
                    if (!isNaN(dataId) && dataId > 0) {
                        _url = "/Finance/ContInvoice/UpdateSave";
                    }
                    var $showtab = false;
                    if (isFlow != undefined && parseInt(isFlow) == 1) {
                        $showtab = true;
                    }

                    
                    wootool.submit(
                        {
                            url: _url,
                            data: obj.field,
                            msg: '保存成功',
                            index: $index,
                            tableId: 'NF-Invoice-Index'
                            ,showtab: $showtab,
                            taburl: '/Finance/ContInvoice/InvoiceIndex',
                            tabtip: '收票'
                        });
                    });

                submit.trigger('click');
            }
            , cancel: function () {
                parent.layer.close($index);
            }
        };

        $('.layui-btn.nf-invoice-build').on('click', function () {
            var type = $(this).data('type');
            active[type] ? active[type].call(this) : '';
        });

    });

   


    /***********************附件信息-begin***************************************************************************************************/
    table.render({
        elem: '#NF-FinceFileTable'
        , url: '/Finance/InvoFile/GetList?finceId=' + Id + '&rand=' + wooutil.getRandom()
        , toolbar: '#toolFinceFile'
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
            , { title: '操作', width: 220, align: 'center', fixed: 'right', toolbar: '#table-FinceFilebar' }
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
                , content: '/Finance/InvoFile/Build'
                // , maxmin: false
                , area: ['800px', '80%']
                , btn: ['确定', '取消']
                , btnAlign: 'c'
                , skin: "layer-ext-myskin"
                , yes: function (index, layero) {
                    var iframeWindow = window['layui-layer-iframe' + index]
                        , submitID = 'NF-FinceFileTable-FormSubmit'
                        , submit = layero.find('iframe').contents().find('#' + submitID);
                    var Projectfiled = layero.find('iframe').contents().find('#ActId');
                    Projectfiled.val(Id);
                    //监听提交
                    iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                        wooutil.OpenSubmitForm({
                            url: '/Finance/InvoFile/Save',
                            data: obj.field,
                            table: table,
                            index: index,
                            tableId: 'NF-FinceFileTable'
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
            wooutil.deleteDatas({ tableId: 'NF-FinceFileTable', table: table, url: '/Finance/InvoFile/Delete', nopage: true });
        },
        tooldownload: function (obj) {
            wooutil.download({
                Id: obj.data.Id,
                folder: 17//附件
            });
        },
        tooldel: function (obj) {
            /// <summary>列表操作栏-删除</summary>
            ///<param name='obj'>删除数据对象</param>
            wooutil.deleteInfo({ tableId: "NF-FinceFileTable", data: obj, url: '/Finance/InvoFile/Delete', nopage: true });

        },
        tooledit: function (obj) {
            ///<summary>修改</summary>
            ///<param name='obj'>修改数据对象</param>
            layer.open({
                type: 2
                , title: '修改附件'
                , content: '/Finance/InvoFile/Build?Id=' + obj.data.Id + "&rand=" + wooutil.getRandom()
                //, maxmin: true
                , area: ['800px', '80%']
                , btn: ['确定', '取消']
                , btnAlign: 'c'
                , skin: "layer-ext-myskin"
                , yes: function (index, layero) {
                    var iframeWindow = window['layui-layer-iframe' + index]
                        , submitID = 'NF-FinceFileTable-FormSubmit'
                        , submit = layero.find('iframe').contents().find('#' + submitID);
                    var Projectfiled = layero.find('iframe').contents().find('#ActId');
                    Projectfiled.val(Id);
                    //监听提交
                    iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                        wooutil.OpenSubmitForm({
                            url: '/Finance/InvoFile/UpdateSave',
                            data: obj.field,
                            table: table,
                            index: index,
                            tableId: 'NF-FinceFileTable'
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
    table.on('toolbar(NF-FinceFileTable)', function (obj) {
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
    table.on('tool(NF-FinceFileTable)', function (obj) {
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

    //添加小笔头
    wooutil.selpen();
    exports('continvoiceBuild', {});
});