
/**
*收款合同查看
*/
layui.define(['form', 'tableSelect', 'selectnfitem'
    , "viewPageEdit", 'treeSelect', 'appListHist', 'subMetDetail', 'wordAddin'], function (exports) {
        var $ = layui.$
        , setter = layui.setter
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

    Fpdetail();

        /*******************************绑定值-begin*************************************************/
        layui.use('nfcontents', function () {
            var nfcontents = layui.nfcontents;
            //目录
            nfcontents.render({ content: '#customernva' });
            //绑定数据
            if (contId !== "" && contId !== undefined) {
                admin.req({
                    url: '/Contract/ContractCollection/ShowView',
                    data: { Id: contId, rand: wooutil.getRandom() },
                    done: function (res) {
                       
                        var x = ["title: x_one", " description: 1", " value: 4"]
                        var T = res.Data.CustomFields;
                        var obj = {};
                        for (var i = 0; i < T.length; i++) {
                            var split = T[i].split(',');
                            try {
                                obj[split[1].trim()] = split[0].trim();
                            } catch (e) {

                            }
                        }

                        CY(obj);
                        if (res.Data.ContPro == "框架合同") {
                            // alert("这是框架合同");
                            // $("#planfinancecheck").removeClass("layui-hide").addClass("layui-show");
                            $("#contractstatic").addClass("layui-hide");
                        }
                        if (res.Data.ContDivision != "1") {
                            $(".Lh-Zb").addClass("layui-hide");
                            //$("#F1").addClass("layui-hide");
                            //$("#F2").addClass("layui-hide");
                            //$("#F3").addClass("layui-hide");
                        }
                        form.val("NF-ContractCollection-Form", res.Data);
                        SetValueHand(res.Data);
                        contData = res.Data;
                        //修改次要字段
                        seteditsecfiled();




                    }
                });

            }

        });
    /*******************************绑定值-end**********************************************************/

    /////次要字段赋值
    //function CY(obj) {
    //    debugger;
    //    for (let i in obj) {
    //        var s = "Felid-" + i;
    //        $("#" +s + "").val(obj[i]);
    //    }
    //}
    ///次要字段赋值
    function CY(obj) {
        var sd = selectnull;
        var see = sd.split('+');
         // var ids = "";
        var date = "";

        for (var i = 1; i < see.length; i++) {
            var ids = "Felid-"+sd.split('+')[i].split('-')[1];
            if (date == "") {
                date = ids + "?" + sd.split('+')[i].split('-')[0];
            } else {
                date = date + "+" + ids + "?" + sd.split('+')[i].split('-')[0];
            }
        }
        var FName = "";
        var rs = date.split('-');
        var seid = "";
        var w = "";
        for (let i in obj) {
           
            var seids = Number(obj[i]);
            var s = "Felid-" + i;
            if (isNaN(seids)) {
                $("#" + s + "").val(obj[i]);
            } else {
              
                var g = date.split('+');
                for (var j = 0; j < g.length; j++) {
                    var t = date.split('+')[j].split('?')[0];
                    if (t == s) {
                        $("#" + s + "").val(date.split('+')[j].split('?')[1].split(';')[obj[i]]);
                    } else {
                        $("#" + s + "").val(obj[i]);
                    }
                }
                var w = date.split('-');
                //var t = rs.split('+');
                //    $("#" + s + "").val(t);
               
            }
        }
        //for (let i in obj) {
        //    debugger;
        //    for (var j = 0; j < rs.length; j++) {
        //        FName = "Felid-" + rs[j].split('+')[0];
        //           }
        //        var s = "Felid-" + i;

        //    if (FName ==s) {
        //            var seid = Number(obj[i]);
        //            var t = rs[seid];
        //            $("#" + s + "").val(t);
        //        } else {
        //            $("#" + s + "").val(obj[i]);
        //        }
           

        //}
    }

    ///添加次要字段
    //function Fpdetail() {

    //    var $urls = "/Contract/ContractCollection/ContractXtZdyzd?rand=" + wooutil.getRandom();
    //    $.ajax({
    //        type: 'Get',
    //        url: $urls,
    //        data:
    //        {
              
    //        },
    //        dataType: 'json',

    //        success: function (data) {
    //            debugger;
    //            var length = data.Data.data.length;
    //            var resultstr2 = "";
    //            var Tys = "";
    //            if (length > 0) {
    //                for (var i = 0; i < length; i++) {
                      
    //                    var Felid = "Felid-" + data.Data.data[i].Id;
    //                    if (data.Data.data[i].FieldType == 0) {
    //                        //data.Data.data[1].Fname
    //                        resultstr2 += '  <div class="layui-col-lg6 layui-col-md6 layui-col-xs12 layui-col-sm12">'
    //                            + '      <label class="layui-form-label">' + data.Data.data[i].Lable + '</label>'
    //                            + '      <div class="layui-input-block">'
    //                            + '          <input type="' + data.Data.data[i].FieldTypeName + '" name="' + Felid + '" id="' + Felid + '" class="layui-input detail nf-customfield" autocomplete="off" />'
    //                            + '      </div>'
    //                            + '  </div>'
    //                    }
    //                    else if (data.Data.data[i].FieldType == 1) {
    //                        debugger;
    //                        Tys += '<div class="layui-col-lg6 layui-col-md6 layui-col-xs12 layui-col-sm12">'
    //                            + '      <label class="layui-form-label">' + data.Data.data[i].Lable + '</label>'
    //                            + '      <div class="layui-input-block">'
    //                            + '          <select name="' + Felid + '" class="nf-customfield" id="' + Felid + '" lay-filter="' + Felid + '" lay-verify="required" lay-search>'
    //                        var er = data.Data.data[i].SelData.split(';');
    //                        for (var i = 0; i < er.length; i++) {
    //                            Tys += ' <option value="' + i + '" >' + er[i] + '</option>';
    //                        }
                         

    //                        Tys += +'          </select>'
    //                            + '   </div>'
    //                            + ' </div>'
    //                    } else if (data.Data.data[i].FieldType == 2) {
    //                        debugger;
    //                        resultstr2 += '  <div class="layui-col-lg6 layui-col-md6 layui-col-xs12 layui-col-sm12">'
    //                            + '      <label class="layui-form-label">' + data.Data.data[i].Lable + '</label>'
    //                            + '      <div class="layui-input-block">'
    //                            + '          <input type="text" name="' + Felid + '" id="' + Felid + '" class="layui-input detail nf-customfield" autocomplete="off" />'
    //                            + '      </div>'
    //                            + '  </div>'
    //                    }

    //                }
    //            } else {
    //                resultstr2 += "<span class='f-red'>没有数据</span>";
    //            }
    //            $("#Cyzd").html(resultstr2 + Tys);
    //        }
    //    });
    //}


    ///发票内容
    function Fpdetail() {

        var $urls = "/Contract/ContractCollection/ContractXtZdyzd?rand=" + wooutil.getRandom();
        $.ajax({
            type: 'Get',
            url: $urls,
            data:
            {
            },
            dataType: 'json',

            success: function (data) {

                var length = data.Data.data.length;
                var resultstr2 = "";
                var Tys = "";
                if (length > 0) {
                    for (var i = 0; i < length; i++) {
                        var Felid = "Felid-" + data.Data.data[i].Id;
                        if (data.Data.data[i].FieldType == 0) {
                            //data.Data.data[1].Fname
                            resultstr2 += '  <div class="layui-col-lg6 layui-col-md6 layui-col-xs12 layui-col-sm12">'
                                + '      <label class="layui-form-label">' + data.Data.data[i].Lable + '</label>'
                                + '      <div class="layui-input-block">'
                                + '          <input type="text" name="' + Felid + '" id="' + Felid + '" class="layui-input detail  nf-customfield" autocomplete="off" />'
                                + '      </div>'
                                + '  </div>'
                        }
                        else if (data.Data.data[i].FieldType == 1) {
                            selectnull = selectnull +"+"+ data.Data.data[i].SelData + "-" + data.Data.data[i].Id;
                            Tys += '<div class="layui-col-lg6 layui-col-md6 layui-col-xs12 layui-col-sm12">'
                                + '      <label class="layui-form-label">' + data.Data.data[i].Lable + '</label>'
                                + '      <div class="layui-input-block">'
                                + '          <input type="text" name="' + Felid + '" id="' + Felid + '" class="layui-input detail nf-customfield" autocomplete="off" />'
                                + '   </div>'
                                + ' </div>'
                        } else if (data.Data.data[i].FieldType == 2) {
                            resultstr2 += '  <div class="layui-col-lg6 layui-col-md6 layui-col-xs12 layui-col-sm12">'
                                + '      <label class="layui-form-label">' + data.Data.data[i].Lable + '</label>'
                                + '      <div class="layui-input-block">'
                                + '          <input type="text" name="' + Felid + '" id="' + Felid + '" class="layui-input detail nf-customfield" autocomplete="off" />'
                                + '      </div>'
                                + '  </div>'
                        }

                    }
                } else {
                    resultstr2 += "<span class='f-red'>没有数据</span>";
                }
                $("#Cyzd").html(resultstr2 + Tys);
            }
        });
    }

        /***********************合同文本信息-begin***************************************************************************************************/
        layui.use('table', function () {
            var table = layui.table;
            function renderTxt() {
                /// <summary>加载合同文本</summary>
                table.render({
                    elem: '#NF-conttxt'
                               , url: '/ContractDraft/ContText/GetList?contId=' + contId + '&rand=' + wooutil.getRandom()
                               , toolbar: '#toolconttxt'
                               , defaultToolbar: ['filter']
                               , cols: [[
                                   { type: 'numbers', fixed: 'left' }
                                   , { type: 'checkbox', fixed: 'left' }
                                   , { field: 'Name', title: '文件名称', width: 180, fixed: 'left' }
                                   , { field: 'ContTxtType', title: '文件类别', width: 140 }
                                   , { field: 'TempName', title: '模板名称', width: 130 }
                                   , { field: 'IsFromTxt', title: '文本来源', width: 140, hide: true}
                                   , { field: 'ExtenName', title: '文件类型', width: 140, hide: true}
                                   , { field: 'Stagetxt', title: '阶段', width: 140, hide: true}
                                   , { field: 'CreateUserName', title: '建立人', width: 120 }
                                   , { field: 'CreateDateTime', title: '建立日期', width: 120 }
                                   , { field: 'Remark', title: '文本说明', width: 120, hide: true}
                                   , { field: 'Versions', title: '版本', width: 120 }
                                   , { field: 'ModifyDateTime', title: '变更日期', width: 120, hide: true }
                                   , { field: 'Id', title: 'Id', width: 50, hide: true }
                                   , { title: '操作', width: 120, align: 'center', fixed: 'right', toolbar: '#table-conttxtbar' }
                               ]]
                               , page: false
                               , loading: true
                               , height: setter.table.height_tab
                               , limit: setter.table.limit_tab
                               , done: function (res, curr, count) {
                                   $("#txtshowHistory").html('<i class="layui-icon layui-icon-read"></i>查看其他版本');
                                   $("#txtshowHistory").attr("lay-event", "showHistory");
                                   $("#htYuLan").attr("lh-hist", "0");
                                   $("button.compareView").addClass("layui-hide");//对比
                               }
                    // , limits: setter.table.limits

                });
            }
            renderTxt();//初始化加载

            function renderTxtHist(txtId) {
                /// <summary>加载合同历史文本</summary>
                table.render({
                    elem: '#NF-conttxt'
              , url: '/ContractDraft/ContText/GetHistList?contId=' + contId + '&txtId=' + txtId + '&rand=' + wooutil.getRandom()
              , toolbar: '#toolconttxt'
              , defaultToolbar: ['filter']
              , cols: [[
                  { type: 'numbers', fixed: 'left' }
                  , { type: 'checkbox', fixed: 'left' }
                  , { field: 'Name', title: '文件名称', width: 180, fixed: 'left' }
                  , { field: 'ContTxtType', title: '文件类别', width: 140 }
                  , { field: 'TempName', title: '模板名称', width: 130 }
                  , { field: 'IsFromTxt', title: '文本来源', width: 140 }
                  , { field: 'ExtenName', title: '文件类型', width: 140 }
                  , { field: 'Stagetxt', title: '阶段', width: 140 }
                  , { field: 'CreateUserName', title: '建立人', width: 120 }
                  , { field: 'CreateDateTime', title: '建立日期', width: 120 }
                  , { field: 'Remark', title: '文本说明', width: 120 }
                  , { field: 'Versions', title: '版本', width: 120 }
                  , { field: 'ModifyDateTime', title: '变更日期', width: 120 }
                  , { field: 'Id', title: 'Id', width: 50, hide: true }
                  , { title: '操作', width: 120, align: 'center', fixed: 'right', toolbar: '#table-conttxtbar' }
              ]]
              , page: false
              , loading: true
              , height: setter.table.height_tab
              , limit: setter.table.limit_tab
               , done: function (res, curr, count) {
                   $("#txtshowHistory").html('<i class="layui-icon layui-icon-read"></i>查看最新版本');
                   $("#txtshowHistory").attr("lay-event", "showFirst");
                   $("#htYuLan").attr("lh-hist", "1");
                   $("button.compareView").removeClass("layui-hide");

               }
                    // , limits: setter.table.limits

                });
            }

            var contTextEvent = {

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
                    , title: '新建合同文本'
                    , content: '/ContractDraft/ContText/Build'
                        // , maxmin: false
                        , area: ['800px', '80%']
                    , btn: ['确定', '取消']
                    , btnAlign: 'c'
                    , skin: "layer-ext-myskin"
                    , yes: function (index, layero) {
                        var iframeWindow = window['layui-layer-iframe' + index]
                         , submitID = 'NF-ContText-FormSubmit'
                        , submit = layero.find('iframe').contents().find('#' + submitID);
                        var Contractfiled = layero.find('iframe').contents().find('#ContId');
                        Contractfiled.val(contId);
                        //监听提交
                        iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                            wooutil.OpenSubmitForm({
                                url: '/ContractDraft/ContText/Save',
                                data: obj.field,
                                table: table,
                                index: index,
                                tableId: 'NF-conttxt'
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
                    wooutil.deleteDatas({ tableId: 'NF-conttxt', table: table, url: '/ContractDraft/ContText/Delete', nopage: true });
                },
                YuLan: function () {
                    /// <summary>预览</summary>
                    var checkData = this.seldata();
                    var txtId = checkData[0].Id;
                    var IsHist = $("#htYuLan").attr("lh-hist");
                    var histId = -1;
                    if (IsHist === "1") {//历史版本
                        histId = txtId;
                    }
                    if (checkData[0].IsFromTemp == 1) {//模板起草
                        
                        /*wordAddin.contractTextPreview(txtId, histId);*/
                        if (checkData[0].ExtenName.toLowerCase().indexOf("pdf") >= 0) {
                            contTextEvent.OnLineView();
                        } else {
                            wordAddin.contractTextPreview(txtId, histId);
                        }
                    } else {
                        contTextEvent.OnLineView();
                    }
                    


                },
                OnLineView: function () {
                    var checkStatus = table.checkStatus("NF-conttxt")
                        , checkData = checkStatus.data; //得到选中的数据
                    if (checkData.length === 0) {
                        return layer.msg('请选择数据');
                    } else {
                        if (checkData[0].ExtenName.toLowerCase().indexOf("pdf") >= 0) {
                            var fileurl = layui.cache.base + 'nf-plugs/pdfjs/web/viewer.html?file=' +
                                encodeURIComponent('/ContractDraft/ContText/GetFileBytes?Id=' + checkData[0].Id);
                            parent.parent.layer.open({
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
                            || checkData[0].ExtenName.toLowerCase().indexOf("svg") >= 0
                            || checkData[0].ExtenName.toLowerCase().indexOf("jpeg") >= 0
                        ) {
                            //var pcurl = '/ContractDraft/ContText/GetFileBytesTuPian?Id=' + checkData[0].Id;
                            //$.getJSON(pcurl, function (res) {
                            //    parent.parent.layer.photos({
                            //        photos: res.Data
                            //        , anim: 5 //0-6的选择，指定弹出图片动画类型，默认随机（请注意，3.0之前的版本用shift参数）
                            //    });
                            //});

                            var pcurl = "/ContractDraft/ContText/PictureView?contId=" + checkData[0].ContId + "&viewType=0";
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
                            parent.parent.layer.open({
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
                },
                seldata: function () {
                    /// <summary>选择文本数据</summary>
                    var checkStatus = table.checkStatus("NF-conttxt")
                  , checkData = checkStatus.data; //得到选中的数据
                    if (checkData.length === 0) {
                       // return layer.msg('请选择数据！7');
                    } else if (checkData.length > 1) {
                        return layer.msg('只允许选择一条数据！');
                    }
                    return checkData;
                },
                htReview: function () {
                    /// <summary>审阅</summary>
                    /// <summary>审阅</summary>
                    var checkData = this.seldata();
                    var seldata = checkData[0];
                    var txtId = seldata.Id;
                    var isfromtemp = parseInt(seldata.IsFromTemp);
                    if (checkData.length === 0) {
                        return layer.msg('请选择数据！');
                    } else if (checkData.length > 1) {
                        return layer.msg('只允许选择一条数据！');
                    }
                    if (seldata.IsFromTxt == "模板起草") {
                        if (!isNaN(isfromtemp)) {
                            switch (isfromtemp) {
                                case 1://模板起草
                                case 2://自由起草
                                case 0://本地上传
                                    {
                                        var isFreeDoc = false;
                                        if (isfromtemp === 2 || isfromtemp === 0) {
                                            isFreeDoc = true;
                                            isFreeDoc = true;
                                        }

                                        wordAddin.drfopenWord(txtId, false, false, isFreeDoc, true);
                                    }
                                    break;
                                default:
                                    layer.alert("合同文本来源不清楚，没法提供操作！" + isfromtemp);
                                    break;
                            }
                        } else {
                            layer.alert("合同文本来源不清楚，没法提供操作！");
                        }
                    }
                    else if (seldata.IsFromTxt == "本地上传") {
                        layer.open({
                            title: '审阅文本',
                            content: '请选择！'
                            , btn: ['上传', '审阅', '取消']
                            , yes: function (index, layero) {
                                //按钮【按钮一】的回调
                                /// <summary>列表头部-新增按钮</summary>
                                layer.open({
                                    type: 2
                                    , title: '新建合同文本'
                                    , content: '/ContractDraft/ContText/Build'
                                    // , maxmin: false
                                    , area: ['800px', '80%']
                                    , btn: ['确定', '取消']
                                    , btnAlign: 'c'
                                    , skin: "layer-ext-myskin"
                                    , yes: function (index, layero) {
                                        var iframeWindow = window['layui-layer-iframe' + index]
                                            , submitID = 'NF-ContText-FormSubmit'
                                            , submit = layero.find('iframe').contents().find('#' + submitID);
                                        var Contractfiled = layero.find('iframe').contents().find('#ContId');
                                        Contractfiled.val(contId);
                                        //监听提交
                                        iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                                            wooutil.OpenSubmitForm({
                                                url: '/ContractDraft/ContText/Save',
                                                data: obj.field,
                                                table: table,
                                                index: index,
                                                tableId: 'NF-conttxt'
                                            });
                                            return false;
                                        });

                                        submit.trigger('click');
                                    },
                                    success: function (layero, index) {

                                    }
                                });
                            }
                            , btn2: function (index, layero) {
                                if (!isNaN(isfromtemp)) {
                                    switch (isfromtemp) {
                                        case 1://模板起草
                                        case 2://自由起草
                                        case 0://本地上传
                                            {
                                                var isFreeDoc = false;
                                                if (isfromtemp === 2 || isfromtemp === 0) {
                                                    isFreeDoc = true;
                                                    isFreeDoc = true;
                                                }

                                                wordAddin.drfopenWord(txtId, false, false, isFreeDoc, true);
                                            }
                                            break;
                                        default:
                                            layer.alert("合同文本来源不清楚，没法提供操作！" + isfromtemp);
                                            break;
                                    }
                                } else {
                                    layer.alert("合同文本来源不清楚，没法提供操作！");
                                }
                            }, btn3: function (index, layero) {
                                //按钮【按钮3】的回调
                                //return false 开启该代码可禁止点击该按钮关闭
                            }

                        })
                    }
                }, showHistory: function () {
                    /// <summary>查看其他版本</summary>
                    var checkData = this.seldata();
                    var txtId = checkData[0].Id;
                    renderTxtHist(txtId);
                },
                showFirst: function () {
                    /// <summary>查看最新版本</summary>
                    renderTxt();
                },
                compareTxt: function () {
                    /// <summary>比对文本</summary>
                    var checkStatus = table.checkStatus("NF-conttxt")
                  , checkData = checkStatus.data; //得到选中的数据
                    if (checkData.length !==2) {
                        return layer.msg('请选择数据！');
                    }
                    var id0 = checkData[0].Id;
                    var id1 = checkData[1].Id;
                    if (id0 > id1) {
                        var temp = id0;
                        id0 = id1;
                        id1 = temp;
                    }

                    wordAddin.compareContractText(id0, id1);
                }
                , tooldownload: function (obj) {
                    var $dtype = $("#htYuLan").attr("lh-hist") === "1" ? 1 : 0;
                    //历史文本下载
                    var dp = 0;
                    if (setter.sysinfo.Mb !== "Mb") {
                        wooutil.download({
                            url: '/NfCommon/NfAttachment/Download',
                            Id: obj.data.Id,
                            folder: 6,//合同文本
                            dtype: $dtype,
                            downType: dp
                        });
                    } else {
                    if (obj.data.IsFromTemp !== 0){
                        //dp = 1;
                        //wordAddin.downWord({
                        //    Id: obj.data.Id,
                        //    contTextId: obj.data.Id,
                        //    url:'/NfCommon/NfAttachment/Download',
                        //    dtype: $dtype,
                        //    downType: dp
                        //});
                        window.open("/ContractDraft/ContText/WordNewView?Id=" + obj.data.Id);
                    } else {
                            wooutil.download({
                                url: '/NfCommon/NfAttachment/Download',
                                Id: obj.data.Id,
                                folder: 6,//合同文本
                                dtype: $dtype,
                                downType: dp
                            });
                        }
                    }
                },
                tooldel: function (obj) {
                    /// <summary>列表操作栏-删除</summary>
                    ///<param name='obj'>删除数据对象</param>
                    wooutil.deleteInfo({ tableId: "NF-conttxt", data: obj, url: '/ContractDraft/ContText/Delete', nopage: true });

                },
                tooledit: function (obj) {
                    ///<summary>修改</summary>
                    ///<param name='obj'>修改数据对象</param>
                    layer.open({
                        type: 2
                  , title: '修改合同文本'
                  , content: '/ContractDraft/ContText/Build?Id=' + obj.data.Id + "&rand=" + wooutil.getRandom()
                        //, maxmin: true
                   , area: ['800px', '80%']
                  , btn: ['确定', '取消']
                  , btnAlign: 'c'
                  , skin: "layer-ext-myskin"
                  , yes: function (index, layero) {
                      var iframeWindow = window['layui-layer-iframe' + index]
                          , submitID = 'NF-ContText-FormSubmit'
                          , submit = layero.find('iframe').contents().find('#' + submitID);
                      var Contractfiled = layero.find('iframe').contents().find('#ContId');
                      Contractfiled.val(contId);
                      //监听提交
                      iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                          wooutil.OpenSubmitForm({
                              url: '/ContractDraft/ContText/UpdateSave',
                              data: obj.field,
                              table: table,
                              index: index,
                              tableId: 'NF-conttxt'
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
            table.on('toolbar(NF-conttxt)', function (obj) {
                switch (obj.event) {
                    case 'add'://审阅
                        contTextEvent.add();
                        break;
                    case 'htReview'://审阅
                        contTextEvent.htReview();
                        break;
                    case 'htYuLan'://预览
                        contTextEvent.YuLan();
                        break;
                    //case "OnLineView"://在线预览
                    //    contTextEvent.OnLineView();
                    //    break;
                    case 'showHistory'://查看其他版本
                        contTextEvent.showHistory();
                        break;
                    case "showFirst"://查看最新版本
                        contTextEvent.showFirst();
                        break;
                    case "compareView"://合同文本比对
                        contTextEvent.compareTxt();
                        break;
                    case 'LAYTABLE_COLS'://选择列-系统默认不管
                        break;
                    default:
                        layer.alert("暂不支持（" + obj.event + "）");
                        break;

                };
            });
            //列表操作栏
            table.on('tool(NF-conttxt)', function (obj) {
                var _data = obj.data;
                switch (obj.event) {
                    case 'del':
                        contTextEvent.tooldel(obj);
                        break;
                    case 'edit':
                        contTextEvent.tooledit(obj);
                        break;
                    case 'download'://下载
                        contTextEvent.tooldownload(obj);
                        break;
                    default:
                        layer.alert("暂不支持（" + obj.event + "）");
                        break;
                }
            });

            /***********************文本信息-end***************************************************************************************************/

            /***********************合同备忘-begin***************************************************************************************************/
            table.render({
                elem: '#NF-ContDescription'
                   , url: '/Contract/ContDescription/GetList?contId=' + contId + '&rand=' + wooutil.getRandom()
                   , toolbar: '#toolcontdesc'
                   , defaultToolbar: ['filter']
                   , cols: [[
                       { type: 'numbers', fixed: 'left' }
                       , { type: 'checkbox', fixed: 'left' }
                        , { field: 'Id', title: 'Id', width: 50, hide: true }
                       , { field: 'Citem', title: '说明事项', width: 200, fixed: 'left' }
                       , { field: 'Ccontent', title: '内容', width: 350 }
                        , { field: 'CreateUserName', title: '创建人', width: 120 }
                       , { field: 'CreateDateTime', title: '创建时间', width: 120 }
                       , { title: '操作', width: 150, align: 'center', fixed: 'right', toolbar: '#tabl-contdescbar' }
                   ]]
                   , page: false
                   , loading: true
                   , height: setter.table.height_tab
                   , limit: setter.table.limit_tab
                // , limits: setter.table.limits

            });
            //按钮
            var submitID = 'NF-ContDescription-FormSubmit';
            var contacdestEvent = {
                add: function () {
                    /// <summary>列表头部-新增按钮</summary>
                    layer.open({
                        type: 2
                    , title: '新建合同备忘'
                    , content: '/Contract/ContDescription/Build'
                        // , maxmin: true
                    , area: ['60%', '80%']
                    , btn: ['确定', '取消']
                    , btnAlign: 'c'
                    , skin: "layer-ext-myskin"
                    , yes: function (index, layero) {
                        var iframeWindow = window['layui-layer-iframe' + index]
                            , submit = layero.find('iframe').contents().find('#' + submitID);
                        var Contractfiled = layero.find('iframe').contents().find('#ContId');
                        Contractfiled.val(contId);
                        //监听提交
                        iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                            wooutil.OpenSubmitForm({
                                url: '/Contract/ContDescription/Save',
                                data: obj.field,
                                table: table,
                                index: index,
                                tableId: 'NF-ContDescription'
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
                    wooutil.deleteDatas({ tableId: 'NF-ContDescription', url: '/Contract/ContDescription/Delete', nopage: true });
                },
                tooldel: function (obj) {
                    /// <summary>列表操作栏-删除</summary>
                    ///<param name='obj'>删除数据对象</param>
                    wooutil.deleteInfo({ tableId: "NF-ContDescription", data: obj, url: '/Contract/ContDescription/Delete', nopage: true });

                },
                tooledit: function (obj) {
                    ///<summary>修合同备忘</summary>
                    ///<param name='obj'>修改数据对象</param>
                    layer.open({
                        type: 2
                  , title: '修合同备忘'
                  , content: '/Contract/ContDescription/Build?Id=' + obj.data.Id + "&rand=" + wooutil.getRandom()
                        //, maxmin: true
                  , area: ['60%', '80%']
                  , btn: ['确定', '取消']
                  , btnAlign: 'c'
                  , skin: "layer-ext-myskin"
                  , yes: function (index, layero) {
                      var iframeWindow = window['layui-layer-iframe' + index]
                          , submit = layero.find('iframe').contents().find('#' + submitID);
                      var Contractfiled = layero.find('iframe').contents().find('#ContId');
                      Contractfiled.val(contId);
                      //监听提交
                      iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                          wooutil.OpenSubmitForm({
                              url: '/Contract/ContDescription/UpdateSave',
                              data: obj.field,
                              table: table,
                              index: index,
                              tableId: 'NF-ContDescription'
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
            //合同备忘头部工具栏
            table.on('toolbar(NF-ContDescription)', function (obj) {
                switch (obj.event) {
                    case 'add':
                        contacdestEvent.add();
                        break;
                    case 'batchdel':
                        contacdestEvent.batchdel();
                        break
                    case 'LAYTABLE_COLS'://选择列-系统默认不管
                        break;
                    default:
                        layer.alert("暂不支持（" + obj.event + "）");
                        break;

                };
            });
            //列表操作栏
            table.on('tool(NF-ContDescription)', function (obj) {
                var _data = obj.data;
                switch (obj.event) {
                    case 'del':
                        contacdestEvent.tooldel(obj);
                        break;
                    case 'edit':
                        contacdestEvent.tooledit(obj);
                        break;
                    default:
                        layer.alert("暂不支持（" + obj.event + "）");
                        break;
                }
            });

            /***********************合同备忘-end***************************************************************************************************/

            /***********************计划资金-begin***************************************************************************************************/

            table.render({
                elem: '#NF-ContPlanFinace'
                   , url: '/Finance/ContPlanFinance/GetList?contId=' + contId + '&rand=' + wooutil.getRandom()
                   , toolbar: '#toolcontplanfinace'
                   , defaultToolbar: ['filter']
                   , cols: [[
                       { type: 'numbers', fixed: 'left' }
                       , { type: 'checkbox', fixed: 'left' }
                       , { field: 'Id', title: 'Id', width: 50, hide: true }
                       , { field: 'Name', title: '名称', width: 200 }
                       , { field: 'AmountMoneyThod', title: '金额', width: 160 }
                       , { field: 'SettlModelName', title: '结算方式', width: 160 }
                       , { field: 'Remark', title: '备注', width: 280 }
                       , { title: '操作', width: 150, align: 'center', fixed: 'right', toolbar: '#table-contplanfinacebar' }
                   ]]
                   , page: false
                   , loading: true
                   , height: setter.table.height_tab
                , limit: setter.table.limit_tab
                , done: function (res, curr, count) {   //返回数据执行回调函数
                    if (contData!=null)
                    SetValueHand(contData);

                }
                // , limits: setter.table.limits

            });
            var planfinanceEvent = {
                add: function () {
                    /// <summary>列表头部-新增按钮</summary>
                    layer.open({
                        type: 2
                    , title: '新建计划资金'
                    , content: '/Finance/ContPlanFinance/Build'
                        //, maxmin: true
                    , area: ['60%', '80%']
                    , btn: ['确定', '取消']
                    , btnAlign: 'c'
                    , skin: "layer-ext-myskin"
                    , yes: function (index, layero) {
                        var iframeWindow = window['layui-layer-iframe' + index]
                            , submitID = 'NF-ContPlanFinance-FormSubmit'
                            , submit = layero.find('iframe').contents().find('#' + submitID);
                        layero.find('iframe').contents().find('#ContId').val(contId);//合同ID
                        layero.find('iframe').contents().find('#IsFramework').val(1);//表示是框合同执行新增计划资金
                        layero.find('iframe').contents().find('#Ftype').val(Ftype);//计划资金类型
                        
                        //监听提交
                        iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                            wooutil.OpenSubmitForm({
                                url: '/Finance/ContPlanFinance/Save',
                                data: obj.field,
                                table: table,
                                index: index,
                                tableId: 'NF-ContPlanFinace'
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
                    wooutil.deleteDatas({ tableId: 'NF-ContPlanFinace', url: '/Finance/ContPlanFinance/Delete?isFra=true', nopage: true });
                },
                tooldel: function (obj) {
                    /// <summary>列表操作栏-删除</summary>
                    ///<param name='obj'>删除数据对象</param>
                    wooutil.deleteInfo({ tableId: "NF-ContPlanFinace", data: obj, url: '/Finance/ContPlanFinance/Delete?isFra=true', nopage: true });

                },
                tooledit: function (obj) {
                    ///<summary>修改计划资金</summary>
                    ///<param name='obj'>修改数据对象</param>
                    layer.open({
                        type: 2
                  , title: '修改计划资金'
                  , content: '/Finance/ContPlanFinance/Build?Id=' + obj.data.Id + "&rand=" + wooutil.getRandom()
                        // , maxmin: true
                  , area: ['60%', '80%']
                  , btn: ['确定', '取消']
                  , btnAlign: 'c'
                  , skin: "layer-ext-myskin"
                  , yes: function (index, layero) {
                      var iframeWindow = window['layui-layer-iframe' + index]
                          , submitID = 'NF-ContPlanFinance-FormSubmit'
                          , submit = layero.find('iframe').contents().find('#' + submitID);
                      layero.find('iframe').contents().find('#ContId').val(contId);
                      layero.find('iframe').contents().find('#IsFramework').val(contId);
                      //监听提交
                      iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                          wooutil.OpenSubmitForm({
                              url: '/Finance/ContPlanFinance/UpdateSave',
                              data: obj.field,
                              table: table,
                              index: index,
                              tableId: 'NF-ContPlanFinace'
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
            table.on('toolbar(NF-ContPlanFinace)', function (obj) {
                switch (obj.event) {
                    case 'add':
                        planfinanceEvent.add();
                        break;
                    case 'batchdel':
                        planfinanceEvent.batchdel();
                        break;
                    case 'LAYTABLE_COLS'://选择列-系统默认不管
                        break;
                    default:
                        layer.alert("暂不支持（" + obj.event + "）");
                        break;

                };
            });
            //列表操作栏
            table.on('tool(NF-ContPlanFinace)', function (obj) {
                var _data = obj.data;
                switch (obj.event) {
                    case 'del':
                        planfinanceEvent.tooldel(obj);
                        break;
                    case 'edit':
                        planfinanceEvent.tooledit(obj);
                        break;
                    default:
                        layer.alert("暂不支持（" + obj.event + "）");
                        break;
                }
            });

            /***********************计划资金-end***************************************************************************************************/

            /***********************合同附件信息-begin***************************************************************************************************/
            table.render({
                elem: '#NF-ContAttachment'
                   , url: '/Contract/ContAttachment/GetList?contId=' + contId + '&rand=' + wooutil.getRandom()
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
                       , { title: '操作', width: 220, align: 'center', fixed: 'right', toolbar: '#tabl-contattachmentbar' }
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
                    , content: '/Contract/ContAttachment/Build'
                        // , maxmin: false
                        , area: ['800px', '80%']
                    , btn: ['确定', '取消']
                    , btnAlign: 'c'
                    , skin: "layer-ext-myskin"
                    , yes: function (index, layero) {
                        var iframeWindow = window['layui-layer-iframe' + index]
                            , submitID = 'NF-ContAttachment-FormSubmit'
                            , submit = layero.find('iframe').contents().find('#' + submitID);
                        var Contractfiled = layero.find('iframe').contents().find('#ContId');
                        Contractfiled.val(contId);
                        //监听提交
                        iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                            wooutil.OpenSubmitForm({
                                url: '/Contract/ContAttachment/Save',
                                data: obj.field,
                                table: table,
                                index: index,
                                tableId: 'NF-ContAttachment'
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
                    wooutil.deleteDatas({ tableId: 'NF-ContAttachment', table: table, url: '/Contract/ContAttachment/Delete', nopage: true });
                },
                tooldownload: function (obj) {
                    wooutil.download({
                        Id: obj.data.Id,
                        folder: 5//标识合同附件
                    });
                },
                tooldel: function (obj) {
                    /// <summary>列表操作栏-删除</summary>
                    ///<param name='obj'>删除数据对象</param>
                    wooutil.deleteInfo({ tableId: "NF-ContAttachment", data: obj, url: '/Contract/ContAttachment/Delete', nopage: true });
                },
                tooledit: function (obj) {
                    ///<summary>修改</summary>
                    ///<param name='obj'>修改数据对象</param>
                    layer.open({
                        type: 2
                  , title: '修改附件'
                  , content: '/Contract/ContAttachment/Build?Id=' + obj.data.Id + "&rand=" + wooutil.getRandom()
                        //, maxmin: true
                        , area: ['800px', '80%']
                  , btn: ['确定', '取消']
                  , btnAlign: 'c'
                  , skin: "layer-ext-myskin"
                  , yes: function (index, layero) {
                      var iframeWindow = window['layui-layer-iframe' + index]
                          , submitID = 'NF-ContAttachment-FormSubmit'
                          , submit = layero.find('iframe').contents().find('#' + submitID);
                      var Contractfiled = layero.find('iframe').contents().find('#ContId');
                      Contractfiled.val(contId);
                      //监听提交
                      iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                          wooutil.OpenSubmitForm({
                              url: '/Contract/ContAttachment/UpdateSave',
                              data: obj.field,
                              table: table,
                              index: index,
                              tableId: 'NF-ContAttachment'
                          });
                          return false;
                      });

                      submit.trigger('click');
                  },
                        success: function (layero, index) {

                        }
                    });
                }
                ,
                OnLineView: function () {
                    var checkStatus = table.checkStatus("NF-ContAttachment")
                        , checkData = checkStatus.data; //得到选中的数据
                    if (checkData.length === 0) {
                        return layer.msg('请选择数据');
                    } else {
                        if (checkData[0].FileName.toLowerCase().indexOf("pdf") >= 0) {
                            var fileurl = layui.cache.base + 'nf-plugs/pdfjs/web/viewer.html?file=' +
                                encodeURIComponent('/Contract/ContAttachment/GetFileBytes?Id=' + checkData[0].Id);
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
                                encodeURIComponent('/Contract/ContAttachment/WordView?Id=' + checkData[0].Id);
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
                }
            };
            //附件头部工具栏
            table.on('toolbar(NF-ContAttachment)', function (obj) {
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
            table.on('tool(NF-ContAttachment)', function (obj) {
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

            /***********************合同附件信息-end***************************************************************************************************/

            /***********************合同查阅人-begin***************************************************************************************************/

            var succ = function (rd) {
                var userIds = [];
                $.each(rd, function (i, d) {
                    userIds.push(d.Id);
                });
                admin.req({
                    url: "/Contract/ContConsult/Save"
             , type: 'POST'
             , data: {
                 contId: contId,
                 userIds: userIds.toString()
             },
                    done: function (res) {
                        layer.msg("操作成功", { icon: 1, time: 500 }, function () {
                            table.reload("NF-ContConsult", {
                                where: { rand: wooutil.getRandom() }

                            });

                        });

                    }
                });
            }
            selectnfitem.selectUserItem(
                   {
                       tableSelect: tableSelect,
                       elem: '#consultAdd',
                       //hide_elem: '',
                       seltype: 'checkbox',
                       suc: succ,
                       noval: 'true'
                   });
            table.render({
                elem: '#NF-ContConsult'
                   , url: '/Contract/ContConsult/GetList?contId=' + contId + '&rand=' + wooutil.getRandom()
                   , toolbar: '#toolcontconsult'
                   , defaultToolbar: ['filter']
                   , cols: [[
                       { type: 'numbers', fixed: 'left' }
                       , { type: 'checkbox', fixed: 'left' }
                       , { field: 'Id', title: 'Id', width: 50, hide: true }
                       , { field: 'UserName', title: '登录名称', width: 150, fixed: 'left' }
                       , { field: 'DisplayName', title: '显示名称', width: 150 }
                       , { field: 'DeptName', title: '所属部门', width: 280 }
                       , { title: '操作', width: 150, align: 'center', fixed: 'right', toolbar: '#tabl-contconsultbar' }
                   ]]
                   , page: false
                   , loading: true
                   , height: setter.table.height_tab
                   , limit: setter.table.limit_tab
                // , limits: setter.table.limits

            });


            var contconsultEvent = {
                add: function () {
                    $("#consultAdd").click();
                },
                batchdel: function () {
                    /// <summary>列表头部-批量删除</summary>
                    wooutil.deleteDatas({ tableId: 'NF-ContConsult', url: '/Contract/ContConsult/Delete', nopage: true });
                },
                tooldel: function (obj) {
                    /// <summary>列表操作栏-删除</summary>
                    ///<param name='obj'>删除数据对象</param>
                    wooutil.deleteInfo({ tableId: "NF-ContConsult", data: obj, url: '/Contract/ContConsult/Delete', nopage: true });
                }
            };
            //合同备忘头部工具栏
            table.on('toolbar(NF-ContConsult)', function (obj) {
                switch (obj.event) {
                    case 'add':
                        contconsultEvent.add();
                        break;
                    case 'batchdel':
                        contconsultEvent.batchdel();
                        break
                    case 'LAYTABLE_COLS'://选择列-系统默认不管
                        break;
                    default:
                        layer.alert("暂不支持（" + obj.event + "）");
                        break;
                };
            });
            //列表操作栏
            table.on('tool(NF-ContConsult)', function (obj) {
                var _data = obj.data;
                switch (obj.event) {
                    case 'del':
                        contconsultEvent.tooldel(obj);
                        break;
                    default:
                        layer.alert("暂不支持（" + obj.event + "）");
                        break;
                }
            });

            /***********************合同查阅人-end***************************************************************************************************/

            /************************合同实际资金-begin*****************************************************************************************/
            table.render({
                elem: '#NF-ContActfinance'
                   , url: '/Contract/ContractCollection/GetActListByContId?contId=' + contId + '&rand=' + wooutil.getRandom()
                //, toolbar: '#toolcontconsult'
                   , defaultToolbar: ['filter']
                   , cols: [[
                       { type: 'numbers', fixed: 'left' }
                       , { type: 'checkbox', fixed: 'left' }
                       , { field: 'Id', title: 'Id', width: 50, hide: true }
                       , { field: 'AmountMoneyThod', title: '金额', width: 150, fixed: 'left' }
                       , { field: 'SettlementMethodDic', title: '结算方式', width: 150 }
                       , { field: 'VoucherNo', title: '票据号码', width: 150 }
                       , { field: 'ActualSettlementDate', title: '结算日期', width: 140 }
                       , { field: 'AstateDic', title: '资金状态', width: 140 }
                       , { field: 'ConfirmUserName', title: '确认人', width: 140 }
                       , { field: 'ConfirmDateTime', title: '确认日期', width: 150 }
                       , { field: 'CreateUserName', title: '建立人', width: 140 }
                       , { field: 'CreateDateTime', title: '建立日期', width: 140 }

                   ]]
                   , page: false
                   , loading: true
                   , height: setter.table.height_tab
                   , limit: setter.table.limit_tab
                // , limits: setter.table.limits

            });
            /*************************合同实际资金-end*******************************************************************************************/
            /************************发票-begin*****************************************************************************************/
            table.render({
                elem: '#NF-ContInvoice'
                   , url: '/Contract/ContractCollection/GetInvoiceListByContId?contId=' + contId + '&rand=' + wooutil.getRandom()
                //, toolbar: '#toolcontconsult'
                   , defaultToolbar: ['filter']
                   , cols: [[
                       { type: 'numbers', fixed: 'left' }
                       , { type: 'checkbox', fixed: 'left' }
                       , { field: 'Id', title: 'Id', width: 50, hide: true }
                       , { field: 'InTypeName', title: '发票类型', width: 140, fixed: 'left' }
                       , { field: 'AmountMoneyThod', title: '发票金额', width: 140 }
                       , { field: 'MakeOutDateTime', title: '开票日期', width: 140 }
                       , { field: 'InCode', title: '发票号', width: 150 }
                       , { field: 'InStateDic', title: '发票状态', width: 140 }
                       , { field: 'ConfirmUserName', title: '确认人', width: 140 }
                       , { field: 'ConfirmDateTime', title: '确认日期', width: 140 }
                       , { field: 'CreateUserName', title: '创建人', width: 140 }
                       , { field: 'CreateDateTime', title: '创建日期', width: 140 }

                   ]]
                   , page: false
                   , loading: true
                   , height: setter.table.height_tab
                   , limit: setter.table.limit_tab
                // , limits: setter.table.limits

            });
            /*************************发票-end*******************************************************************************************/

            /***********************分包合同-begin*****************************************************************************************/
            function renderConts(tabId, contId, finctype) {
                table.render({
                    elem: '#' + tabId
                    , url: '/Contract/ContractCollection/GetContsByZbContId?contId=' + contId + '&fincType=' + finctype + '&rand=' + wooutil.getRandom()
                    //, toolbar: '#toolcustomer_CompDescription'
                    , defaultToolbar: ['filter']
                    , cols: [[
                        { type: 'numbers', fixed: 'left' }
                        , { type: 'checkbox', fixed: 'left' }
                        , { field: 'Name', title: '合同名称', width: 220, templet: '#nameTpl' }
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
            
            renderConts('NF-proj-FkContracts', contId, 1);//付款

            /**
            *打开合同查看页面
            **/
            function openview(obj) {
                parent.layer.open({
                    type: 2
                    , title: '查看详情'
                    , content: '/contract/ContractPayment/Detail?Id=' + obj.data.Id + "&Ftype=" + obj.data.FinanceType + "&rand=" + wooutil.getRandom()
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
            /**
           *打开客户查看页面
           **/
            function opencompview(obj) {
                parent.layer.open({
                    type: 2
                    , title: '查看详情'
                    , content: '/Company/Supplier/Detail?Id=' + obj.data.CompId + "&rand=" + wooutil.getRandom()
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
    /***********************分包合同-end********************************************************************************************/

      /***********************合同变更-begin***************************************************************************************************/
            table.render({
                elem: '#NF-ContChangeList'
                , url: '/Contract/ContractChange/GetChangeList?contId=' + contId + '&rand=' + wooutil.getRandom()
                , toolbar: '#toolcontchangelist'
                , defaultToolbar: ['filter']
                , cols: [[
                    { type: 'numbers', fixed: 'left' }
                    , { type: 'checkbox', fixed: 'left' }
                    , { field: 'Id', title: 'Id', width: 50, hide: true }
                    , { field: 'Name', title: '合同名称', width: 260, fixed: 'left' }
                    , { field: 'ChangeVersions', title: '变更版本', width: 120 }
                    , { field: 'ChangeDate', title: '变更日期', width: 120 }
                    , { field: 'ChangePerson', title: '变更人', width: 120 }
                    , { field: 'ChageDesc', title: '变更报告', width: 300 }

                ]]
                , page: false
                , loading: true
                , height: setter.table.height_tab
                , limit: setter.table.limit_tab
                // , limits: setter.table.limits

            });
            //按钮
            var contchangelistEvent = {
                selview: function (obj) {
                    /// <summary>列表头部-选中查看按钮</summary>
                    if (obj.data.length != 1) {
                        layer.alert("请选择一个合同");
                    } else {
                        layer.open({
                            type: 2
                        , title: '选中合同查看'
                        , content: '/Contract/ContractChange/SelContView?Id=' + obj.data[0].Id
                            // , maxmin: true
                        , area: ['60%', '80%']
                        , skin: "layer-ext-myskin"
                        });
                    }
                },
                compview: function (obj) {

                    /// <summary>列表头部-选中合同比较</summary>
                    if (obj.data.length != 2) {
                        layer.alert("请选择两条对比合同");
                    } else {
                        layer.open({
                            type: 2
                       , title: '选择合同对比查看'
                       , content: '/Contract/ContractChange/SelContCompareView?Id=' + obj.data[0].Id + '&Id2=' + obj.data[1].Id
                            // , maxmin: true
                       , area: ['60%', '80%']
                       , skin: "layer-ext-myskin"
                        });
                    }
                }
            };
            //合同变更头部工具栏
            table.on('toolbar(NF-ContChangeList)', function (obj) {

                var checkStatus = table.checkStatus(obj.config.id);
                switch (obj.event) {
                    case 'selview'://查看选中合同
                        contchangelistEvent.selview(checkStatus);
                        break;
                    case 'compview'://比较选中合同
                        contchangelistEvent.compview(checkStatus);
                        break
                    case 'LAYTABLE_COLS'://选择列-系统默认不管
                        break;
                    default:
                        layer.alert("暂不支持（" + obj.event + "）");
                        break;

                };
            });
            /***********************合同变更-end***************************************************************************************************/
        });

        /*******************************编辑次要字段-begin***********************************************/
        //经办机构
        function IniDept(el, vel, selVal) {
            treeSelect.render(
              {
                  elem: "#" + el,
                  data: '/System/Department/GetTreeSelectDept',
                  method: "GET",
                  search: true,
                  click: function (d) {
                      $("input[name=" + el + "]").val(d.current.id);
                      $("input[name=" + vel + "]").val(d.current.name);
                  },
                  success: function (d) {
                      if (selVal!=null){
                          treeSelect.checkNode(el, selVal);
                      }

                  }
              });
        }
        /**次要字段编辑**/
        function seteditsecfiled() {
            var ress = wooutil.requestpremission({
                url: '/NfCommon/NfPermission/SecFieldUpatePremission?rand=' + wooutil.getRandom(),
                data: {
                    FuncCode: 'updatecollcontminor',
                    ObjId: contId
                }

            });
            var updateFields =
                    ["OtherCode", "PerformanceDateTime", "ContSName", "ContTypeName"
                   , "PrincipalUserName", "StampTaxThod", "DeptName", "ProjName"
                   , "Reserve1", "Reserve2"];
            if (ress.RetValue == 0) {//有权限
                $.each(updateFields, function (index, fieldId) {

                    switch (fieldId) {
                        case "OtherCode":
                        case 'StampTaxThod':
                        case "Reserve1":
                        case 'Reserve2':
                            {//都是文本编辑框
                                viewPageEdit.render({
                                    elem: '#' + fieldId,
                                    edittype: 'text',
                                    objid: contId,
                                    fieldname: fieldId,
                                    verify: 'required',
                                    ckEl: '#Name',
                                    url: '/Contract/ContractCollection/UpdateField'

                                });
                            }

                            break;
                        case "PrincipalUserName"://负责人
                            {//都是文本编辑框
                                viewPageEdit.render({
                                    elem: '#' + fieldId,
                                    edittype: 'selTable',
                                    objid: contId,
                                    fieldname: fieldId,
                                    verify: 'required',
                                    selobjId: "#PrincipalUserId",
                                    ckEl: '#Name',
                                    url: '/Contract/ContractCollection/UpdateField'

                                });
                                //负责人编辑
                                selectnfitem.selectUserItem(
                                {
                                    tableSelect: tableSelect,
                                    elem: '#PrincipalUserName',
                                    hide_elem: '#PrincipalUserId'

                                });
                            }
                            break;
                        case "ProjName"://所属项目
                            {
                                viewPageEdit.render({
                                    elem: '#' + fieldId,
                                    edittype: 'selTable',
                                    objid: contId,
                                    fieldname: fieldId,
                                    verify: 'required',
                                    selobjId: "#ProjectId",
                                    ckEl: '#Name',
                                    url: '/Contract/ContractCollection/UpdateField'

                                });
                                //所属项目
                                selectnfitem.selectProjItem(
                                {
                                    tableSelect: tableSelect,
                                    elem: '#ProjName',
                                    hide_elem: '#ProjectId'

                                });
                            }
                            break;
                        case "ContSName"://合同来源
                            {
                                var objdata = {};
                                objdata.url = '/System/DataDictionary/GetListByDataEnumType?dataEnum=' + 15;
                                objdata.empty = true;
                                objdata.emptyText = "请选择";
                                viewPageEdit.render({
                                    elem: '#ContSName',
                                    edittype: 'select',
                                    objid: contId,
                                    fieldname: "ContSName",
                                    ckEl: '#Name',
                                    url: '/Contract/ContractCollection/UpdateField',
                                    editobj: objdata
                                });
                            }
                            break;
                        case "ContTypeName"://合同类别
                            {
                                var objdata = {};
                                objdata.url = '/System/DataDictionary/GetListByDataEnumType?dataEnum=' + 1;
                                objdata.empty = true;
                                objdata.emptyText = "请选择";
                                viewPageEdit.render({
                                    elem: '#ContTypeName',
                                    edittype: 'select',
                                    objid: contId,
                                    fieldname: "ContTypeName",
                                    ckEl: '#Name',
                                    url: '/Contract/ContractCollection/UpdateField',
                                    editobj: objdata
                                });
                            }
                            break;
                        case "PerformanceDateTime"://履行时间
                            {
                                viewPageEdit.render({
                                    //elem: '#PerformanceDateTime',
                                    elem: '#' + fieldId,
                                    edittype: 'date',
                                    objid: contId,
                                    fieldname: fieldId,
                                    //fieldname: "PerformanceDateTime",
                                    ckEl: '#Name',
                                    url: '/Contract/ContractCollection/UpdateField'

                                });
                            }
                            break;
                        case "DeptName"://经办机构
                            {

                                viewPageEdit.render({
                                    elem: '#' + fieldId,
                                    edittype: 'treeSelect',
                                    objid: contId,
                                    fieldname: fieldId,
                                    verify: 'required',
                                    selobjId: "#DeptId",
                                    ckEl: '#Name',
                                    url: '/Contract/ContractCollection/UpdateField'

                                });

                                IniDept("DeptId", "DeptName", null);

                            }
                            break;



                    }
                });

            } else {
                viewPageEdit.noUpShow(updateFields);
            }
        }

        /********************************编辑次要字段-end*******************************************************/
        /**绑定值以后的一些条件判断**/
        function SetValueHand(objval) {
            if (objval.IsFramework == 1 && objval.ContState == 1) {
                //不是框架合同->隐藏计划资金操作按钮
                $(".financebtn").removeClass("layui-hide");
                $(".financebar").removeClass("layui-hide");
                //隐藏标的操作按钮
                $(".subbtn").removeClass("layui-hide");
                $(".httextadd").removeClass("layui-hide");

               /* $(".htweb").removeClass("layui-hide");*/
            }
            else if (objval.IsFramework == 0 && objval.ContState == 1) {
                $(".financebtn").addClass("layui-hide");
                $(".financebar").addClass("layui-hide");
                //隐藏标的操作按钮
                $(".subbtn").addClass("layui-hide");
                $(".httextadd").addClass("layui-hide");
               /* $(".htweb").addClass("layui-hide");*/
            }

            if (IsSp === 1) {
                //判断如果是审批就显示审阅按钮
                $(".htReview").removeClass("layui-hide");

                //打开合同文本标签
                var seldiv = $("#conttxt").children("div");
                if (seldiv.length > 0) {
                    seldiv.addClass("layui-show")
                }
            }


            if (objval.ContDivision==1) {
               /* $("#li_fkcontracts").removeClass("layui-hide");*/
                $("#fkcontracts").removeClass("layui-hide"); 

            } else {
                $('#li_fkcontracts').remove();
              /*  $("#li_fkcontracts").addClass("layui-hide");*/
                $("#fkcontracts").addClass("layui-hide");

            }




        }

        
        //资金统计
        admin.req({
            url: '/Contract/ContractCollection/ContractStatic',
            data: { Id: contId, rand: wooutil.getRandom() },
            done: function (res) {

                form.val("NF-ContStaticForm", res.Data);
               

            }
        });

        //标的
    setTimeout(function () {
     
        subMetDetail.render({ contId: contId });
    }, 2000)
       // subMetDetail.render({ contId: contId });
        //审批历史
        appListHist.applistInit({ Id: contId, objType: setter.sysWf.flowType.Hetong });
        exports('CollectionContractDetail', {});
    });