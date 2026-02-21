
/**
*收款合同查看
*/
layui.define(['form', 'table', 'tableSelect', 'selectnfitem'
    , "viewPageEdit", 'treeSelect', 'appListHist', 'subMetDetail', 'wordAddin'], function (exports) {
        var $ = layui.$
            , setter = layui.setter
            , table = layui.table
            , admin = layui.admin
            , tableSelect = layui.tableSelect
            , selectnfitem = layui.selectnfitem
            , viewPageEdit = layui.viewPageEdit
            , treeSelect = layui.treeSelect
            , form = layui.form
            , subMetDetail = layui.subMetDetail
            , appListHist = layui.appListHist
            , wordAddin = layui.wordAddin;
        var contId = wooutil.getUrlVar('Id');
        var Ftype = wooutil.getUrlVar('Ftype');
        var IsSp = parseInt(wooutil.getUrlVar('IsSp'));//是不是审批
        if (isNaN(IsSp)) {
            IsSp = 0;
        }
        var contData = null;
        var selectnull = "";

        //  Fpdetail();
    /***********************案件管理附件信息-begin***************************************************************************************************/
    table.render({
        elem: '#NF-Ajglfj'
        , url: '/CaseManagement/Ajfj/GetList?contId=' + contId + '&rand=' + wooutil.getRandom()
        , toolbar: '#toolcontattachment'
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
            //, { title: '操作', width: 220, align: 'center', fixed: 'right', toolbar: '#tabl-contattachmentbar' }
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
                , content: '/CaseManagement/Ajfj/Build'
                // , maxmin: false
                , area: ['800px', '80%']
                , btn: ['确定', '取消']
                , btnAlign: 'c'
                , skin: "layer-ext-myskin"
                , yes: function (index, layero) {
                    var iframeWindow = window['layui-layer-iframe' + index]
                        , submitID = 'NF-Ajglfj-FormSubmit'
                        , submit = layero.find('iframe').contents().find('#' + submitID);
                    var Contractfiled = layero.find('iframe').contents().find('#ContId');
                    Contractfiled.val(contId);
                    //监听提交
                    iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                        wooutil.OpenSubmitForm({
                            url: '/CaseManagement/Ajfj/Save',
                            data: obj.field,
                            table: table,
                            index: index,
                            tableId: 'NF-Ajglfj'
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
            wooutil.deleteDatas({ tableId: 'NF-Ajglfj', table: table, url: '/CaseManagement/Ajfj/Delete', nopage: true });
        },
        tooldownload: function (obj) {
            wooutil.download({
                Id: obj.data.Id,
                folder: 18//标识合同附件
            });
        },
        tooldel: function (obj) {
            /// <summary>列表操作栏-删除</summary>
            ///<param name='obj'>删除数据对象</param>
            wooutil.deleteInfo({ tableId: "NF-Ajglfj", data: obj, url: '/CaseManagement/Ajfj/Delete', nopage: true });
        },
        tooledit: function (obj) {
            ///<summary>修改</summary>
            ///<param name='obj'>修改数据对象</param>
            layer.open({
                type: 2
                , title: '修改附件'
                , content: '/CaseManagement/Ajfj/Build?Id=' + obj.data.Id + "&rand=" + wooutil.getRandom()
                //, maxmin: true
                , area: ['800px', '80%']
                , btn: ['确定', '取消']
                , btnAlign: 'c'
                , skin: "layer-ext-myskin"
                , yes: function (index, layero) {
                    var iframeWindow = window['layui-layer-iframe' + index]
                        , submitID = 'NF-Ajglfj-FormSubmit'
                        , submit = layero.find('iframe').contents().find('#' + submitID);
                    var Contractfiled = layero.find('iframe').contents().find('#ContId');
                    Contractfiled.val(contId);
                    //监听提交
                    iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                        wooutil.OpenSubmitForm({
                            url: '/CaseManagement/Ajfj/UpdateSave',
                            data: obj.field,
                            table: table,
                            index: index,
                            tableId: 'NF-Ajglfj'
                        });
                        return false;
                    });

                    submit.trigger('click');
                },
                success: function (layero, index) {

                }
            });
        },
        OnLineView: function () {
            var checkStatus = table.checkStatus("NF-Ajglfj")
                , checkData = checkStatus.data; //得到选中的数据
            if (checkData.length === 0) {
                return layer.msg('请选择数据');
            } else {
                if (checkData[0].FileName.toLowerCase().indexOf("pdf") >= 0) {
                    var fileurl = layui.cache.base + 'nf-plugs/pdfjs/web/viewer.html?file=' +
                        encodeURIComponent('/CaseManagement/Ajfj/GetFileBytes?Id=' + checkData[0].Id);
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

                } else if (checkData[0].FileName.toLowerCase().indexOf("png") >= 0
                    || checkData[0].FileName.toLowerCase().indexOf("jpg") >= 0
                    || checkData[0].FileName.toLowerCase().indexOf("bpm") >= 0
                    || checkData[0].FileName.toLowerCase().indexOf("tif") >= 0
                    || checkData[0].FileName.toLowerCase().indexOf("gif") >= 0
                    || checkData[0].FileName.toLowerCase().indexOf("svg") >= 0
                    || checkData[0].FileName.toLowerCase().indexOf("jpeg") >= 0
                ) {

                    var pcurl = "/ContractDraft/ContText/PictureView?contId=" + checkData[0].ContId + "&viewType=3";
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


                } else if (checkData[0].FileName.toLowerCase().indexOf("docx") >= 0) {
                    //var url = '/ContractDraft/ContText/WordView?Id=' + checkData[0].Id
                    var fileurl = layui.cache.base + 'nf-plugs/pdfjs/web/viewer.html?file=' +
                        encodeURIComponent('/CaseManagement/Ajfj/WordView?Id=' + checkData[0].Id);
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
                    return layer.msg('只支持PDF、PNG、JPG、docx预览', { icon: 5 });
                }


            }
        }


    };
    //附件头部工具栏
    table.on('toolbar(NF-Ajglfj)', function (obj) {
        switch (obj.event) {
            case 'add':
                attachmentEvent.add();
                break;
            case 'batchdel':
                attachmentEvent.batchdel();
                break;
            case 'htYuLan'://预览
                attachmentEvent.OnLineView();
                break;
            case 'LAYTABLE_COLS'://选择列-系统默认不管
                break;
            default:
                layer.alert("暂不支持（" + obj.event + "）");
                break;

        };
    });
    //列表操作栏
    table.on('tool(NF-Ajglfj)', function (obj) {
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

    /***********************案件管理附件信息-end***************************************************************************************************/

    /***********************审判文书附件信息-begin***************************************************************************************************/
    table.render({
        elem: '#NF-Ajglfj-Sqws'
        , url: '/CaseManagement/AjglSpws/GetList?contId=' + contId + '&rand=' + wooutil.getRandom()
        , toolbar: '#toolcontattachment-Sqws'
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
            //, { title: '操作', width: 220, align: 'center', fixed: 'right', toolbar: '#tabl-contattachmentbar-Sqws' }
        ]]
        , page: false
        , loading: true
        , height: setter.table.height_tab
        , limit: setter.table.limit_tab
        // , limits: setter.table.limits

    });
    var SpattachmentEvent = {
        mydownload: function (url, method, filedir, filename) {
            $('<form action="' + url + '" method="' + (method || 'post') + '">' +  // action请求路径及推送方法
                '<input type="text" name="filedir" value="' + filedir + '"/>' + // 文件路径
                '<input type="text" name="filename" value="' + filename + '"/>' + // 文件名称
                '</form>')
                .appendTo('body').submit().remove();
        },
        Spadd: function () {
            /// <summary>列表头部-新增按钮</summary>
            layer.open({
                type: 2
                , title: '新建附件'
                , content: '/CaseManagement/AjglSpws/Build'
                // , maxmin: false
                , area: ['800px', '80%']
                , btn: ['确定', '取消']
                , btnAlign: 'c'
                , skin: "layer-ext-myskin"
                , yes: function (index, layero) {
                    var iframeWindow = window['layui-layer-iframe' + index]
                        , submitID = 'NF-Ajglfj-FormSubmit-Sqws'
                        , submit = layero.find('iframe').contents().find('#' + submitID);
                    var Contractfiled = layero.find('iframe').contents().find('#ContId');
                    Contractfiled.val(contId);
                    //监听提交
                    iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                        wooutil.OpenSubmitForm({
                            url: '/CaseManagement/AjglSpws/Save',
                            data: obj.field,
                            table: table,
                            index: index,
                            tableId: 'NF-Ajglfj-Sqws'
                        });
                        return false;
                    });

                    submit.trigger('click');
                },
                success: function (layero, index) {

                }
            });
        },
        Spbatchdel: function () {
            /// <summary>列表头部-批量删除</summary>
            wooutil.deleteDatas({ tableId: 'NF-Ajglfj-Sqws', table: table, url: '/CaseManagement/AjglSpws/Delete', nopage: true });
        },
        Sptooldownload: function (obj) {
            wooutil.download({
                Id: obj.data.Id,
                folder: 19//标识合同附件
            });
        },
        Sptooldel: function (obj) {
            /// <summary>列表操作栏-删除</summary>
            ///<param name='obj'>删除数据对象</param>
            wooutil.deleteInfo({ tableId: "NF-Ajglfj-Sqws", data: obj, url: '/CaseManagement/AjglSpws/Delete', nopage: true });
        },
        Sptooledit: function (obj) {
            ///<summary>修改</summary>
            ///<param name='obj'>修改数据对象</param>
            layer.open({
                type: 2
                , title: '修改附件'
                , content: '/CaseManagement/AjglSpws/Build?Id=' + obj.data.Id + "&rand=" + wooutil.getRandom()
                //, maxmin: true
                , area: ['800px', '80%']
                , btn: ['确定', '取消']
                , btnAlign: 'c'
                , skin: "layer-ext-myskin"
                , yes: function (index, layero) {
                    var iframeWindow = window['layui-layer-iframe' + index]
                        , submitID = 'NF-Ajglfj-FormSubmit-Sqws'
                        , submit = layero.find('iframe').contents().find('#' + submitID);
                    var Contractfiled = layero.find('iframe').contents().find('#ContId');
                    Contractfiled.val(contId);
                    //监听提交
                    iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                        wooutil.OpenSubmitForm({
                            url: '/CaseManagement/AjglSpws/UpdateSave',
                            data: obj.field,
                            table: table,
                            index: index,
                            tableId: 'NF-Ajglfj-Sqws'
                        });
                        return false;
                    });

                    submit.trigger('click');
                },
                success: function (layero, index) {

                }
            });
        },
        SpOnLineView: function () {
            var checkStatus = table.checkStatus("NF-Ajglfj-Sqws")
                , checkData = checkStatus.data; //得到选中的数据
            if (checkData.length === 0) {
                return layer.msg('请选择数据');
            } else {
                if (checkData[0].FileName.toLowerCase().indexOf("pdf") >= 0) {
                    var fileurl = layui.cache.base + 'nf-plugs/pdfjs/web/viewer.html?file=' +
                        encodeURIComponent('/CaseManagement/AjglSpws/GetFileBytes?Id=' + checkData[0].Id);
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

                } else if (checkData[0].FileName.toLowerCase().indexOf("png") >= 0
                    || checkData[0].FileName.toLowerCase().indexOf("jpg") >= 0
                    || checkData[0].FileName.toLowerCase().indexOf("bpm") >= 0
                    || checkData[0].FileName.toLowerCase().indexOf("tif") >= 0
                    || checkData[0].FileName.toLowerCase().indexOf("gif") >= 0
                    || checkData[0].FileName.toLowerCase().indexOf("svg") >= 0
                    || checkData[0].FileName.toLowerCase().indexOf("jpeg") >= 0
                ) {

                    var pcurl = "/ContractDraft/ContText/PictureView?contId=" + checkData[0].ContId + "&viewType=3";
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


                } else if (checkData[0].FileName.toLowerCase().indexOf("docx") >= 0) {
                    //var url = '/ContractDraft/ContText/WordView?Id=' + checkData[0].Id
                    var fileurl = layui.cache.base + 'nf-plugs/pdfjs/web/viewer.html?file=' +
                        encodeURIComponent('/CaseManagement/AjglSpws/WordView?Id=' + checkData[0].Id);
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
                    return layer.msg('只支持PDF、PNG、JPG、docx预览', { icon: 5 });
                }


            }
        }


    };
    //附件头部工具栏
    table.on('toolbar(NF-Ajglfj-Sqws)', function (obj) {
        switch (obj.event) {
            case 'add':
                SpattachmentEvent.Spadd();
                break;
            case 'batchdel':
                SpattachmentEvent.Spbatchdel();
                break;
            case 'htYuLan'://预览
                SpattachmentEvent.SpOnLineView();
                break;
            case 'LAYTABLE_COLS'://选择列-系统默认不管
                break;
            default:
                layer.alert("暂不支持（" + obj.event + "）");
                break;

        };
    });
    //列表操作栏
    table.on('tool(NF-Ajglfj-Sqws)', function (obj) {
        var _data = obj.data;
        switch (obj.event) {
            case 'del':
                SpattachmentEvent.Sptooldel(obj);
                break;
            case 'edit':
                SpattachmentEvent.Sptooledit(obj);
                break;
            case 'download'://下载
                SpattachmentEvent.Sptooldownload(obj);
                break;
            default:
                layer.alert("暂不支持（" + obj.event + "）");
                break;
        }
    });

    /***********************审判文书附件信息-end***************************************************************************************************/

    /***********************诉讼保全附件信息-begin***************************************************************************************************/
    table.render({
        elem: '#NF-Ajglfj-Ssbq'
        , url: '/CaseManagement/AjglSsBqFile/GetList?contId=' + contId + '&rand=' + wooutil.getRandom()
        , toolbar: '#toolcontattachment-Ssbq'
        , defaultToolbar: ['filter']
        , cols: [[
            { type: 'numbers', fixed: 'left' }
            , { type: 'checkbox', fixed: 'left' }
            , { field: 'Id', title: 'Id', width: 50, hide: true }
            , { field: 'Name', title: '附件名称', width: 180, fixed: 'left' }
            , { field: 'CategoryName', title: '保全类型', width: 140 }
            , { field: 'Remark', title: '文件说明', width: 200 }
            , { field: 'FileName', title: '文件名', width: 180 }
            , { field: 'CreateDateTime', title: '上传日期', width: 120 }
            //, { title: '操作', width: 220, align: 'center', fixed: 'right', toolbar: '#tabl-contattachmentbar-Ssbq' }
        ]]
        , page: false
        , loading: true
        , height: setter.table.height_tab
        , limit: setter.table.limit_tab
        // , limits: setter.table.limits

    });
    var SsattachmentEvent = {
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
                , content: '/CaseManagement/AjglSsBqFile/Build'
                // , maxmin: false
                , area: ['800px', '80%']
                , btn: ['确定', '取消']
                , btnAlign: 'c'
                , skin: "layer-ext-myskin"
                , yes: function (index, layero) {
                    var iframeWindow = window['layui-layer-iframe' + index]
                        , submitID = 'NF-Ajglfj-FormSubmit-Ssbq'
                        , submit = layero.find('iframe').contents().find('#' + submitID);
                    var Contractfiled = layero.find('iframe').contents().find('#ContId');
                    Contractfiled.val(contId);
                    //监听提交
                    iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                        wooutil.OpenSubmitForm({
                            url: '/CaseManagement/AjglSsBqFile/Save',
                            data: obj.field,
                            table: table,
                            index: index,
                            tableId: 'NF-Ajglfj-Ssbq'
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
            wooutil.deleteDatas({ tableId: 'NF-Ajglfj-Ssbq', table: table, url: '/CaseManagement/AjglSsBqFile/Delete', nopage: true });
        },
        tooldownload: function (obj) {
            wooutil.download({
                Id: obj.data.Id,
                folder: 20//标识合同附件
            });
        },
        tooldel: function (obj) {
            /// <summary>列表操作栏-删除</summary>
            ///<param name='obj'>删除数据对象</param>
            wooutil.deleteInfo({ tableId: "NF-Ajglfj-Ssbq", data: obj, url: '/CaseManagement/AjglSsBqFile/Delete', nopage: true });
        },
        tooledit: function (obj) {
            ///<summary>修改</summary>
            ///<param name='obj'>修改数据对象</param>
            layer.open({
                type: 2
                , title: '修改附件'
                , content: '/CaseManagement/AjglSsBqFile/Build?Id=' + obj.data.Id + "&rand=" + wooutil.getRandom()
                //, maxmin: true
                , area: ['800px', '80%']
                , btn: ['确定', '取消']
                , btnAlign: 'c'
                , skin: "layer-ext-myskin"
                , yes: function (index, layero) {
                    var iframeWindow = window['layui-layer-iframe' + index]
                        , submitID = 'NF-Ajglfj-FormSubmit-Ssbq'
                        , submit = layero.find('iframe').contents().find('#' + submitID);
                    var Contractfiled = layero.find('iframe').contents().find('#ContId');
                    Contractfiled.val(contId);
                    //监听提交
                    iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                        wooutil.OpenSubmitForm({
                            url: '/CaseManagement/AjglSsBqFile/UpdateSave',
                            data: obj.field,
                            table: table,
                            index: index,
                            tableId: 'NF-Ajglfj-Ssbq'
                        });
                        return false;
                    });

                    submit.trigger('click');
                },
                success: function (layero, index) {

                }
            });
        },
        OnLineView: function () {
            var checkStatus = table.checkStatus("NF-Ajglfj-Ssbq")
                , checkData = checkStatus.data; //得到选中的数据
            if (checkData.length === 0) {
                return layer.msg('请选择数据');
            } else {
                if (checkData[0].FileName.toLowerCase().indexOf("pdf") >= 0) {
                    var fileurl = layui.cache.base + 'nf-plugs/pdfjs/web/viewer.html?file=' +
                        encodeURIComponent('/CaseManagement/AjglSsBqFile/GetFileBytes?Id=' + checkData[0].Id);
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

                } else if (checkData[0].FileName.toLowerCase().indexOf("png") >= 0
                    || checkData[0].FileName.toLowerCase().indexOf("jpg") >= 0
                    || checkData[0].FileName.toLowerCase().indexOf("bpm") >= 0
                    || checkData[0].FileName.toLowerCase().indexOf("tif") >= 0
                    || checkData[0].FileName.toLowerCase().indexOf("gif") >= 0
                    || checkData[0].FileName.toLowerCase().indexOf("svg") >= 0
                    || checkData[0].FileName.toLowerCase().indexOf("jpeg") >= 0
                ) {

                    var pcurl = "/ContractDraft/ContText/PictureView?contId=" + checkData[0].ContId + "&viewType=3";
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


                } else if (checkData[0].FileName.toLowerCase().indexOf("docx") >= 0) {
                    //var url = '/ContractDraft/ContText/WordView?Id=' + checkData[0].Id
                    var fileurl = layui.cache.base + 'nf-plugs/pdfjs/web/viewer.html?file=' +
                        encodeURIComponent('/CaseManagement/AjglSsBqFile/WordView?Id=' + checkData[0].Id);
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
                    return layer.msg('只支持PDF、PNG、JPG、docx预览', { icon: 5 });
                }


            }
        }


    };
    //附件头部工具栏
    table.on('toolbar(NF-Ajglfj-Ssbq)', function (obj) {
        switch (obj.event) {
            case 'add':
                SsattachmentEvent.add();
                break;
            case 'batchdel':
                SsattachmentEvent.batchdel();
                break;
            case 'htYuLan'://预览
                SsattachmentEvent.OnLineView();
                break;
            case 'LAYTABLE_COLS'://选择列-系统默认不管
                break;
            default:
                layer.alert("暂不支持（" + obj.event + "）");
                break;

        };
    });
    //列表操作栏
    table.on('tool(NF-Ajglfj-Ssbq)', function (obj) {
        var _data = obj.data;
        switch (obj.event) {
            case 'del':
                SsattachmentEvent.tooldel(obj);
                break;
            case 'edit':
                SsattachmentEvent.tooledit(obj);
                break;
            case 'download'://下载
                SsattachmentEvent.tooldownload(obj);
                break;
            default:
                layer.alert("暂不支持（" + obj.event + "）");
                break;
        }
    });

    /***********************诉讼保全附件信息-end***************************************************************************************************/


        /*******************************绑定值-begin*************************************************/
        layui.use('nfcontents', function () {
            var nfcontents = layui.nfcontents;
            //目录
            nfcontents.render({ content: '#customernva' });
            //绑定数据
            if (contId !== "" && contId !== undefined) {
                admin.req({
                    url: '/CaseManagement/Ajgl/ShowView',
                    data: { Id: contId, rand: wooutil.getRandom() },
                    done: function (res) {
                        form.val("NF-Ajgl-Form", res.Data);
                        //SetValueHand(res.Data);
                        contData = res.Data;
                        //修改次要字段
                        //  seteditsecfiled();
                    }
                });

            }
            
        });
        /*******************************绑定值-end**********************************************************/
        exports('CollectionContractDetail', {});
    });