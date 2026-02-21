/**
 @Name：付款合同文本
 @Author：dyk 
 */
layui.define(['table', 'form', 'wordAddin'], function (exports) {
    var $ = layui.$
        , table = layui.table
        , setter = layui.setter
        , admin = layui.admin
        , form = layui.form
        , wordAddin = layui.wordAddin;
    var logdindex = layer.load(0, { shade: false });
    var $htcols = [
        { type: 'numbers', fixed: 'left' }
        , { type: 'checkbox', fixed: 'left' }
        , { field: 'Name', title: '文本名称', width: 200, fixed: 'left' }
        , { field: 'CategoryName', title: '文本类别', width: 150 }
        , { field: 'ContName', title: '合同名称', width: 200, templet: '#contnameTpl' }
        , { field: 'ContCode', title: '合同编号', width: 150 }
        , { field: 'CompName', title: '合同对方', width: 150, templet: '#compTpl'}
        , { field: 'Remark', title: '文本说明', width: 150 }
        , { field: 'IsFromTxt', title: '文本来源', width: 120 }
        , { field: 'ExtenName', title: '文件类型', width: 120 }
        , { field: 'ContStateDic', title: '合同状态', width: 120, templet: '#contractstateTpl', unresize: true }
        , { field: 'PrincipalUserName', title: '负责人', width: 130, hide: true }
        , { field: 'CreateUserName', title: '建立人', width: 130, hide: true }
        , { field: '', title: '下载', width: 120, templet: '#downloadTpl' }
        , { field: 'dwnpdf', title: '下载Pdf', width: 120, templet: '#downloadPdf' }
        , { field: 'SealStateDic', title: '用章状态', width: 130 }
        , { field: 'SealName', title: '印章名称', width: 130 }
        , { field: 'SealDate', title: '用章日期', width: 130 }
        , { field: 'ArchiveStateDic', title: '归档状态', width: 130 }
        , { field: 'ArchiveCode', title: '归档号', width: 130 }
        , { field: 'ArchiveCabCode', title: '归档柜号', width: 130 }
        //, { field: 'ArchiveEleFile', title: '归档文件电子版', width: 150 }
        , { field: 'ArchiveNumber', title: '归档份数', width: 130 }
        , { field: 'BorrowNumber', title: '已借份数', width: 130 }
        , { field: 'Surplus', title: '剩余份数', width: 130 }
        , { field: 'Id', title: 'Id', width: 80, hide: true }
        , { title: '操作', width: 220, align: 'center', fixed: 'right', toolbar: '#table-conttextcoll-bar' }
    ]
    if (setter.sysinfo.Mb !== "Mb") {
        for (var i = 0; i < $htcols.length; i++) {
            if ($htcols[i].field === "dwnpdf") {
                $htcols.splice(i, 1);
            }

        }

    }
    //列表
    table.render({
        elem: '#NF-ContTextPay-Index'
        , url: '/ContractDraft/ContText/GetMainList?requestType=1&rand=' + wooutil.getRandom()
        , toolbar: '#toolconttextcoll'
        , defaultToolbar: ['filter']
        , cellMinWidth: 80
        , cols: [$htcols]
        , page: true
        , loading: true
        , height: setter.table.height_4
        , limit: setter.table.limit
        , limits: setter.table.limits
        , done: function (res, curr, count) {   //返回数据执行回调函数
            layer.close(logdindex);    //返回数据关闭loading
            $("input[name=keyWord]").val($("input[name=hide_keyWord]").val());
            $("input[name=hide_keyWord]").val("");
        }
    });
    /*********************************公共方法-begin*********************************************************/
    /**
   *打开合同查看页面
   **/
    function opencontview(obj) {
        layer.open({
            type: 2
            , title: '查看合同详情'
            , content: '/contract/ContractCollection/Detail?Id=' + obj.data.ContId + "&rand=" + wooutil.getRandom()
            , maxmin: true
            , area: ['60%', '80%']
            , btnAlign: 'c'
            , skin: "layer-nf-nfskin"
            , btn: ['关闭']
            , success: function (layero, index) {
                layer.load(0, { shade: false, time: 2 * 1100 });//2秒自动关闭
                layer.full(index);
                wooutil.openTip();
            }
        });
    };
    /**
    *盖章
    **/
    var openseal = function (rowobj) {
        layer.open({
            type: 2
            , title: '合同文本-盖章'
            , content: '/ContractDraft/ContText/ContTextSeal?txtId=' + rowobj.data.Id + "&contId=" + rowobj.data.ContId
            , maxmin: true
            , area: ['70%', '80%']
            , btn: ['确定', '取消']
            , btnAlign: 'c'
            , skin: "layer-ext-myskin"
            , yes: function (index, layero) {
                var iframeWindow = window['layui-layer-iframe' + index]
                    , submitID = 'NF-ContTextSeal-FormSubmit'
                    , submit = layero.find('iframe').contents().find('#' + submitID);
                //监听提交
                iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                    wooutil.OpenSubmitForm({
                        url: '/ContractDraft/ContText/SaveSeal',
                        data: obj.field,
                        table: table,
                        index: index,
                        tableId: 'NF-ContTextPay-Index'
                    });
                    return false;
                });
                submit.trigger('click');
            },
            success: function (layero, index) {
                //layer.full(index);
                //wooutil.openTip();
            }
        });
    };
    /**
    *盖章查看页面
    **/
    var opensealview = function (rowobj) {
        layer.open({
            type: 2
            , title: '合同文本-盖章查看'
            , content: '/ContractDraft/ContText/ContTextViewSeal?txtId=' + rowobj.data.Id + "&contId=" + rowobj.data.ContId
            , maxmin: true
            , area: ['70%', '80%']
            , btn: ['取消']
            , btnAlign: 'c'
            , skin: "layer-ext-myskin"
            , success: function (layero, index) {
                //layer.full(index);
                //wooutil.openTip();
            }
        });
    };
    /**
    *归档
    **/
    var openarch = function (rowobj) {
        layer.open({
            type: 2
            , title: '合同文本-归档'
            , content: '/ContractDraft/ContText/ContTextArchive?txtId=' + rowobj.data.Id + "&contId=" + rowobj.data.ContId
            //, maxmin: true
            , area: ['70%', '80%']
            , btn: ['确定', '取消']
            , btnAlign: 'c'
            , skin: "layer-ext-myskin"
            , yes: function (index, layero) {
                var iframeWindow = window['layui-layer-iframe' + index]
                    , submitID = 'NF-ContTextArchive-FormSubmit'
                    , submit = layero.find('iframe').contents().find('#' + submitID);
                //监听提交
                iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                    wooutil.OpenSubmitForm({
                        url: '/ContractDraft/ContText/SaveArchive',
                        data: obj.field,
                        table: table,
                        index: index,
                        tableId: 'NF-ContTextPay-Index'
                    });
                    return false;
                });

                submit.trigger('click');
            },
            success: function (layero, index) {
                //layer.full(index);
                // wooutil.openTip();
            }
        });
    };
    /**
    *借阅
    **/
    var openborrow = function (rowobj) {
        layer.open({
            type: 2
            , title: '合同盖章-借阅'
            , content: '/ContractDraft/ContText/ContTextBorrow?txtId=' + rowobj.data.Id + "&contId=" + rowobj.data.ContId
            //, maxmin: true
            , area: ['70%', '80%']
            //, btn: ['确定','取消']
            //, btnAlign: 'c'
            //, skin: "layer-nf-nfskin"
            , yes: function (index, layero) {
                var iframeWindow = window['layui-layer-iframe' + index]
                    , submitID = 'NF-ContTextArchive-FormSubmit'
                    , submit = layero.find('iframe').contents().find('#' + submitID);
                //监听提交
                iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                    wooutil.OpenSubmitForm({
                        url: '/ContractDraft/ContText/SaveArchive',
                        data: obj.field,
                        table: table,
                        index: index,
                        tableId: 'NF-ContTextPay-Index'
                    });
                    return false;
                });
                submit.trigger('click');
            }
            , success: function (layero, index) {
                //layer.full(index);
                // wooutil.openTip();

            }
        });

    }
    /*********************************公共方法-end***********************************************************/
    //监听表格排序
    table.on('sort(NF-ContTextPay-Index)', function (obj) {
        table.reload('NF-ContTextPay-Index', { //testTable是表格容器id
            initSort: obj //记录初始排序，如果不设的话，将无法标记表头的排序状态。 layui 2.1.1 新增参数
            , where: { //请求参数（注意：这里面的参数可任意定义，并非下面固定的格式）
                orderField: obj.field //排序字段
                , orderType: obj.type //排序方式
                , keyWord: $("input[name=keyWord]").val()//查询关键字
            }
            , page: { curr: 1 }//重新从第 1 页开始
        });
    });
    /**
    *列表工具栏事件
    **/
    var toolEvent = {
        search: function () {//查询
            $("input[name=hide_keyWord]").val($("input[name=keyWord]").val());
            table.reload('NF-ContTextPay-Index', {
                page: { curr: 1 }
                , where: {
                    keyWord: $("input[name=keyWord]").val()

                }
            });

        }, OnLineView: function () {
            var checkStatus = table.checkStatus("NF-ContTextPay-Index")
                , checkData = checkStatus.data; //得到选中的数据
            if (checkData.length === 0) {
                return layer.msg('请选择数据');
            } else {
                if (checkData[0].ExtenName.toLowerCase().indexOf("pdf") >= 0) {

                    var fileurl = layui.cache.base + 'nf-plugs/pdfjs/web/viewer.html?file=' +
                        encodeURIComponent('/ContractDraft/ContText/GetFileBytes?Id=' + checkData[0].Id);
                    parent.layer.open({
                        type: 2
                        , maxmin: true
                        , title: '文件预览'
                        , content: fileurl
                        , area: ['70%', '80%']
                        , yes: function (index, layero) {

                        }
                        , success: function (layero, index) {

                        }
                    });

                } else if (checkData[0].ExtenName.toLowerCase().indexOf("png") >= 0
                    || checkData[0].ExtenName.toLowerCase().indexOf("jpg") >= 0
                    || checkData[0].ExtenName.toLowerCase().indexOf("bpm") >= 0
                    || checkData[0].ExtenName.toLowerCase().indexOf("tif") >= 0
                    || checkData[0].ExtenName.toLowerCase().indexOf("gif") >= 0
                    || checkData[0].ExtenName.toLowerCase().indexOf("svg") >= 0) {

                    //var pcurl = '/ContractDraft/ContText/GetFileBytesTuPian?Id=' + checkData[0].Id;
                    //$.getJSON(pcurl, function (res) {
                    //    layer.photos({
                    //        photos: res.Data
                    //        , anim: 5 //0-6的选择，指定弹出图片动画类型，默认随机（请注意，3.0之前的版本用shift参数）
                    //    });
                    //});
                    var pcurl = "/ContractDraft/ContText/PictureView?contId=" + checkData[0].ContId + "&viewType=1";
                    parent.parent.layer.open({
                        type: 2
                        , maxmin: true
                        , title: '图片预览'
                        , content: pcurl
                        , area: ['70%', '80%']
                        , yes: function (index, layero) {
                        }
                        , success: function (layero, index) {
                        }
                    });

                } else if (checkData[0].ExtenName.toLowerCase().indexOf("docx") >= 0) {
                    var fileurl = layui.cache.base + 'nf-plugs/pdfjs/web/viewer.html?file=' +
                        encodeURIComponent('/ContractDraft/ContText/WordView?Id=' + checkData[0].Id);
                    parent.layer.open({
                        type: 2
                        , maxmin: true
                        , title: '文件预览'
                        , content: fileurl
                        , area: ['70%', '80%']
                        , yes: function (index, layero) {

                        }
                        , success: function (layero, index) {

                        }
                    });
                }
                else {
                    return layer.msg('只支持PDF预览', { icon: 5 });
                }


            }
        }, openViewContract: function (obj) {
            /// <summary>打开合同查看页面</summary>
            ///<param name='obj' type='Object'>当前操作对象</param>
            var ress = wooutil.requestpremission({
                url: '/NfCommon/NfPermission/ViewPermission?rand=' + wooutil.getRandom(),
                data: {
                    FuncCode: 'querypaycontview',
                    ObjId: obj.data.ContId
                }

            });
            if (ress.RetValue == 0) {
                opencontview(obj);
            } else {
                return layer.alert(ress.Msg);
            }
        },
        openAddSeal: function (obj) {
            /// <summary>打开盖章</summary>
            ///<param name='obj' type='Object'>当前操作对象</param>
            var ress = wooutil.requestpremission({
                url: '/NfCommon/NfPermission/AddPermission?rand=' + wooutil.getRandom(),
                data: {
                    FuncCode: 'payconttextseal'//盖章权限
                }

            });
            if (ress.RetValue == 0) {
                if (obj.data.ContState !== 1) {
                    return layer.alert("只有执行中的合同才允许盖章！");
                } else {

                    if (obj.data.SealState !== -1
                    ) {
                        opensealview(obj)
                    } else {
                        openseal(obj);
                    }
                }
            } else {
                return layer.alert(ress.Msg);
            }

        },
        openArchShow: function (obj) {
            var ress = wooutil.requestpremission({
                url: '/NfCommon/NfPermission/AddPermission?rand=' + wooutil.getRandom(),
                data: {
                    FuncCode: 'payconttextarchive'
                }

            });
            if (ress.RetValue == 0) {
                if (obj.data.SealState < 0) {
                    return layer.alert("只有盖章的合同文本才允许归档！");
                } else {

                    openarch(obj);
                }

            } else {
                return layer.alert(ress.Msg);
            }

        },
        downloadContText: function (obj) {
            var dp = 0;
            if (setter.sysinfo.Mb !== "Mb") {
                wooutil.download({
                    url: '/NfCommon/NfAttachment/Download',
                    Id: obj.data.Id,
                    folder: 6,//合同文本
                    dtype: 0,
                    downType: dp
                });
            } else {
                if (obj.data.IsFromTemp !== 0) {
                    //dp = 1;
                    //wordAddin.downWord({
                    //    Id: obj.data.Id,
                    //    contTextId: obj.data.Id,
                    //    url: '/NfCommon/NfAttachment/Download',
                    //    dtype: 0,
                    //    downType: dp
                    //});
                    window.open("/ContractDraft/ContText/WordNewView?Id=" + obj.data.Id);
                } else {
                    wooutil.download({
                        url: '/NfCommon/NfAttachment/Download',
                        Id: obj.data.Id,
                        folder: 6,//合同文本
                        dtype: 0,
                        downType: dp
                    });
                }
            }
        },
        downloadPdf: function (obj) {//下载PDF
            var dp = 0;
            if (obj.data.ContState !== 1) {
                return layer.alert("只有执行中的合同才可以下载PDF");
            }
            else if (obj.data.IsPdf) {
                wooutil.download({
                    url: '/NfCommon/NfAttachment/Download',
                    Id: obj.data.Id,
                    folder: 6,//合同文本
                    dtype: 2,
                    downType: dp
                });
            }
            else {
                //wordAddin.wordSavePdf({
                //    Id: obj.data.Id,
                //    contTextId: obj.data.Id,
                //    url: '/NfCommon/NfAttachment/Download',
                //    dtype: 2,
                //    downType: dp
                //});
                window.open("/ContractDraft/ContText/WordView?Id=" + obj.data.Id);
            }

        },
        openborrowshow: function (obj) {//借阅
            openborrow(obj);

        }
    };

    table.on('toolbar(NF-ContTextPay-Index)', function (obj) {
        switch (obj.event) {
            case 'search':
                toolEvent.search();
                break;
            case "OnLineView"://在线预览
                toolEvent.OnLineView();
                break;
        };
    });
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
    table.on('tool(NF-ContTextPay-Index)', function (obj) {
        switch (obj.event) {
            case "contdetail":
                toolEvent.openViewContract(obj);
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
            case "download"://下载
                toolEvent.downloadContText(obj);
                break;
            case "downloadPdf"://下载PDF
                toolEvent.downloadPdf(obj);
                break;
            case "seal"://盖章
                toolEvent.openAddSeal(obj);
                break;
            case "archive"://归档
                toolEvent.openArchShow(obj)
                break;
            case "borrow"://借阅
                toolEvent.openborrowshow(obj);
                break;
            default:
                layer.alert("事件->" + obj.event + "不支持");
                break;
        }
    });

    exports('conttextPayIndex', {});
});