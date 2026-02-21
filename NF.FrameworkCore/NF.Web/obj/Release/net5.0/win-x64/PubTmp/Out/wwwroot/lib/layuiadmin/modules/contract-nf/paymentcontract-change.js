/**
*付款合同变更
*/
layui.define(['table', 'form', 'subMetChange', 'wordAddin'], function (exports) {
    var $ = layui.$
    , table = layui.table
    , setter = layui.setter
    , admin = layui.admin
    , subMetChange = layui.subMetChange
    , form = layui.form
    , wordAddin = layui.wordAddin
    ;
    var contId = wooutil.getUrlVar('Id');

    /***********************合同文本信息-begin***************************************************************************************************/
    table.render({
        elem: '#NF-conttxt'
           , url: '/ContractDraft/ContText/GetList?contId=' + contId + '&rand=' + wooutil.getRandom()
           , toolbar: '#toolconttxt'
           , defaultToolbar: ['filter']
           , cols: [[
               { type: 'numbers', fixed: 'left' }
               ,{ type: 'checkbox', fixed: 'left' }
               , { field: 'Id', title: 'Id', width: 50, hide: true }
               , { field: 'Name', title: '文件名称', width: 180, fixed: 'left' }
               , { field: 'ContTxtType', title: '文件类别', width: 140 }
               , { field: 'TempName', title: '模板名称', width: 130 }
               , { field: 'IsFromTxt', title: '文本来源', width: 140 }
               , { field: 'FileName', title: '文件类型', width: 140 }
               , { field: 'Stagetxt', title: '阶段', width: 140 }
               , { field: 'CreateUserName', title: '建立人', width: 120 }
               , { field: 'CreateDateTime', title: '建立日期', width: 120 }
               , { field: 'Remark', title: '文本说明', width: 120 }
               , { field: 'Versions', title: '版本', width: 120 }
               , { field: 'ModifyDateTime', title: '变更日期', width: 120 }
               , { title: '操作', width: 220, align: 'center', fixed: 'right', toolbar: '#table-conttxtbar' }
           ]]
           , page: false
           , loading: true
           , height: setter.table.height_tab
           , limit: setter.table.limit_tab
        // , limits: setter.table.limits

    });
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
        tooldownload: function (obj) {

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
                dp = 1;
                wordAddin.downWord({
                    Id: obj.data.Id,
                    contTextId: obj.data.Id,
                    url: '/NfCommon/NfAttachment/Download',
                    dtype: 0,
                    downType: dp
                });
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
        tooldel: function (obj) {
            /// <summary>列表操作栏-删除</summary>
            ///<param name='obj'>删除数据对象</param>
            wooutil.deleteInfo({ tableId: "NF-conttxt", data: obj, url: '/ContractDraft/ContText/Delete', nopage: true });

        },
        tooledit: function (obj) {
            ///<summary>修改</summary>
            ///<param name='obj'>修改数据对象</param>
            if (obj.data.IsFromTemp === 0) {//本地上传
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
            } else if (obj.data.IsFromTemp === 1) {//模板起草
                contTextEvent.tempDraft(obj.data.Id);
            }
        },
        tempDraft: function (txtId) {
            /// <summary>模板起草</summary>
            var logdindex = layer.load(0, { shade: false });
            var submitID = 'NF-ContractPayment-FormSubmit';
            var submit = $('#' + submitID);
            var $txtId = parseInt(txtId);
            $txtId = (!isNaN($txtId) && $txtId > 0) ? $txtId : 0;


            form.on('submit(' + submitID + ')', function (obj) {
                var fieldval = obj.field.Name;
                var fieldcode = obj.field.Code;
                var contId = $("#Id").val();
                var $currId = 0;
                var $url = "/Contract/ContractPayment/Save";
                if (contId > 0) {
                    $currId = contId;
                    $url = "/Contract/ContractPayment/UpdateSave";
                }
                var resname = wooutil.UniqueValObj({
                    url: '/Contract/ContractPayment/CheckInputValExist',
                    fieldName: 'Name',
                    inputVal: fieldval,
                    currId: $currId
                });
                if (resname) {
                    return layer.msg('此名称已经存在！');
                }
                var rescode = wooutil.UniqueValObj({
                    url: '/Contract/ContractPayment/CheckInputValExist',
                    fieldName: 'Code',
                    inputVal: fieldcode,
                    currId: $currId
                });
                if (rescode) {
                    return layer.msg('此编号已经存在！');
                }
                admin.req({
                    url: $url,
                    data: obj.field,
                    type: 'POST',
                    success: function (res) {
                        //console.log(res.Data.Id);
                        $("#Id").val(res.Data.Id);
                        layer.close(logdindex);
                        obj.field.Id = res.Data.Id;
                        contTextEvent.selTxtTemp(obj, $txtId);
                    }
                });


            });
            submit.trigger('click');


        }, selTxtTemp: function (ctobj, txtId) {
            layer.open({
                type: 2
           , title: '选择合同模板'
           , content: '/ContractDraft/ContText/DraftTxtInfo?htlb=' + ctobj.field.ContTypeId + "&deptId=" + ctobj.field.DeptId + "&txtId=" + txtId
           , maxmin: true
           , area: ['75%', '90%']
           , btn: ['编辑文本', '取消']
           , btnAlign: 'c'
           , skin: "layer-ext-myskin"
           , yes: function (index, layero) {
               var $htId = ctobj.field.Id;
               // console.log($htId);
               var txtUrl = "/ContractDraft/ContText/Save";
               if (txtId > 0) {
                   txtUrl = "/ContractDraft/ContText/UpdateSave";
               }
               var iframeWindow = window['layui-layer-iframe' + index]
                  , submitID = 'NF-ContTextDrft-FormSubmit'
                  , submit = layero.find('iframe').contents().find('#' + submitID);
               iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                   admin.req({
                       url: txtUrl,
                       data: obj.field,
                       type: 'POST',
                       success: function (res) {
                           layer.close(index);
                           table.reload("NF-conttxt",

                               {
                                   url: '/ContractDraft/ContText/GetList?contId=' + $htId
                               , where: { rand: wooutil.getRandom() }

                               });
                           var baseDoc = false;
                           wordAddin.drfopenWord(res.Data.Id, false, false, false, false, baseDoc);

                       }
                   });


                   return false;
               });
               submit.trigger('click');
           }
           , success: function (layero, index) {
               layero.find('iframe').contents().find('#currIndex').val(index);
               layero.find('iframe').contents().find('#Name').val(ctobj.field.Name);
               //合同ID
               layero.find('iframe').contents().find('#ContId').val(ctobj.field.Id);
               //wooutil.openTip();
           }
            });


        },
        YuLan: function () {
            /// <summary>预览</summary> 
            var checkStatus = table.checkStatus("NF-conttxt")
                , checkData = checkStatus.data; //得到选中的数据
            if (checkData.length === 0) {
                return layer.msg('请选择数据！');
            } else if (checkData.length > 1) {
                return layer.msg('只允许选择一条数据！');
            }

            if (checkData[0].IsFromTemp == 1) {//模板起草
                var txtId = checkData[0].Id;
                wordAddin.contractTextPreview(txtId, null);
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
                    || checkData[0].ExtenName.toLowerCase().indexOf("svg") >= 0
                    || checkData[0].ExtenName.toLowerCase().indexOf("jpeg") >= 0
                ) {
                    //var pcurl = '/ContractDraft/ContText/GetFileBytesTuPian?Id=' + checkData[0].Id;
                    //$.getJSON(pcurl, function (res) {
                    //    layer.photos({
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
    table.on('toolbar(NF-conttxt)', function (obj) {
        switch (obj.event) {
            case 'add':
                contTextEvent.add();
                break;
            case 'batchdel':
                contTextEvent.batchdel();
                break;
            case "tempDraft"://模板起草
                contTextEvent.tempDraft();
                break;
            case "htYuLan"://预览
                contTextEvent.YuLan();
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
               ,{ type: 'checkbox', fixed: 'left' }
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
               ,{ type: 'checkbox', fixed: 'left' }
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
            wooutil.deleteDatas({ tableId: 'NF-ContPlanFinace', url: '/Finance/ContPlanFinance/Delete', nopage: true });
        },
        tooldel: function (obj) {
            /// <summary>列表操作栏-删除</summary>
            ///<param name='obj'>删除数据对象</param>
            wooutil.deleteInfo({ tableId: "NF-ContPlanFinace", data: obj, url: '/Finance/ContPlanFinance/Delete', nopage: true });

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

    /***********************合同标的-begin***************************************************************************************************/
    table.render({
        elem: '#NF-ContSub'
           , url: '/Contract/ContSubjectMatter/GetList?contId=' + contId + '&rand=' + wooutil.getRandom()
           , toolbar: '#toolcontsub'
           , defaultToolbar: ['filter']
           , cols: [[
               { type: 'numbers', fixed: 'left' }
               ,{ type: 'checkbox', fixed: 'left' }
               , { field: 'Id', title: 'Id', width: 50, hide: true, fixed: 'left' }
               , { field: 'Name', title: '标的名称', width: 140, fixed: 'left'  }
               , { field: 'Spec', title: '规格', width: 120 }
               , { field: 'Stype', title: '型号', width: 120 }
               , { field: 'Unit', title: '单位', width: 120 }
               , { field: 'PriceThod', title: '单价', width: 120 }
               , { field: 'Amount', title: '数量', width: 120 }
               , { field: 'SubTotalThod', title: '小计', width: 120 }
               , { field: 'SalePriceThod', title: '报价', width: 120 }
               , { field: 'DiscountRate', title: '折扣率', width: 120 }
               , { field: 'PlanDateTime', title: '计划交付日期', width: 130 }
               , { field: 'Remark', title: '备注', width: 130 }
               , { title: '操作', width: 150, align: 'center', fixed: 'right', toolbar: '#tabl-contsubbar' }
           ]]
           , page: true
           , loading: true
           , height: setter.table.height_tab
           , limit: setter.table.limit_tab
           , limits: setter.table.limits
    });
    var submatterEvent = {
        add: function () {
            /// <summary>列表头部-新增按钮</summary>
            layer.open({
                type: 2
            , title: '新建合同标的'
            , content: '/Contract/ContSubjectMatter/Build'
                //, maxmin: true
            , area: ['60%', '80%']
            , btn: ['确定', '取消']
            , btnAlign: 'c'
            , skin: "layer-ext-myskin"
            , yes: function (index, layero) {
                var iframeWindow = window['layui-layer-iframe' + index]
                    , submitID = 'NF-ContSubMatter-FormSubmit'
                    , submit = layero.find('iframe').contents().find('#' + submitID);
                layero.find('iframe').contents().find('#ContId').val(contId);//合同ID

                //监听提交
                iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                    wooutil.OpenSubmitForm({
                        url: '/Contract/ContSubjectMatter/Save',
                        data: obj.field,
                        table: table,
                        index: index,
                        tableId: 'NF-ContSub'
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
            wooutil.deleteDatas({ tableId: 'NF-ContSub', url: '/Contract/ContSubjectMatter/Delete' });
        },
        tooldel: function (obj) {
            /// <summary>列表操作栏-删除</summary>
            ///<param name='obj'>删除数据对象</param>
            wooutil.deleteInfo({ tableId: "NF-ContSub", data: obj, url: '/Contract/ContSubjectMatter/Delete' });

        },
        tooledit: function (obj) {
            ///<summary>修改标的</summary>
            ///<param name='obj'>修改数据对象</param>
            layer.open({
                type: 2
          , title: '修改合同标的'
          , content: '/Contract/ContSubjectMatter/Build?Id=' + obj.data.Id + "&rand=" + wooutil.getRandom()
                // , maxmin: true
          , area: ['60%', '80%']
          , btn: ['确定', '取消']
          , btnAlign: 'c'
          , skin: "layer-ext-myskin"
          , yes: function (index, layero) {
              var iframeWindow = window['layui-layer-iframe' + index]
                  , submitID = 'NF-ContSubMatter-FormSubmit'
                  , submit = layero.find('iframe').contents().find('#' + submitID);
              layero.find('iframe').contents().find('#ContId').val(contId);
              //监听提交
              iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                  wooutil.OpenSubmitForm({
                      url: '/Contract/ContSubjectMatter/UpdateSave',
                      data: obj.field,
                      table: table,
                      index: index,
                      tableId: 'NF-ContSub'
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
    table.on('toolbar(NF-ContSub)', function (obj) {
        switch (obj.event) {
            case 'add':
                submatterEvent.add();
                break;
            case 'batchdel':
                submatterEvent.batchdel();
                break;
            case 'LAYTABLE_COLS'://选择列-系统默认不管
                break;
            default:
                layer.alert("暂不支持（" + obj.event + "）");
                break;

        };
    });
    //列表操作栏
    table.on('tool(NF-ContSub)', function (obj) {
        var _data = obj.data;
        switch (obj.event) {
            case 'del':
                submatterEvent.tooldel(obj);
                break;
            case 'edit':
                submatterEvent.tooledit(obj);
                break;
            default:
                layer.alert("暂不支持（" + obj.event + "）");
                break;
        }
    });

    /***********************合同标的-end***************************************************************************************************/
    /***********************合同附件信息-begin***************************************************************************************************/
    table.render({
        elem: '#NF-ContAttachment'
           , url: '/Contract/ContAttachment/GetList?contId=' + contId + '&rand=' + wooutil.getRandom()
           , toolbar: '#toolcontattachment'
           , defaultToolbar: ['filter']
           , cols: [[
               { type: 'numbers', fixed: 'left' }
               ,{ type: 'checkbox', fixed: 'left' }
               ,{ field: 'Id', title: 'Id', width: 50, hide: true }
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
        },
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
                    return layer.msg('只支持PDF、PNG、JPG、docx预览', { icon: 5 });
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
    /****************************选择表格注册区域-合同对方、项目、签约主体等选择-begin**************************************************************/
    layui.use(['selectnfitem', 'tableSelect'], function () {
        var tableSelect = layui.tableSelect
       , selectnfitem = layui.selectnfitem;
        //负责人
        selectnfitem.selectUserItem(
            {
                tableSelect: tableSelect,
                elem: '#PrincipalUserName',
                hide_elem: '#PrincipalUserId'

            });
        //合同对方
        selectnfitem.selectCompItem(
           {
               tableSelect: tableSelect,
               elem: '#CompName',
               hide_elem: '#CompId',
               selitem: true,
               ctype: 1
           });
        //第三方
        selectnfitem.selectCompItem(
           {
               tableSelect: tableSelect,
               elem: '#Comp3Name',
               hide_elem: '#CompId3',
               selitem: true,
               ctype: 2
           });
        //第四方
        selectnfitem.selectCompItem(
           {
               tableSelect: tableSelect,
               elem: '#Comp4Name',
               hide_elem: '#CompId4',
               selitem: true,
               ctype: 2
           });
        //项目
        selectnfitem.selectProjItem(
           {
               tableSelect: tableSelect,
               elem: '#ProjName',
               hide_elem: '#ProjectId',
               selitem: true
           });
        //签约主体
        selectnfitem.selectMainDeptItem(
                  {
                      tableSelect: tableSelect,
                      elem: '#MdeptName',
                      hide_elem: '#MainDeptId',
                      selitem: true
                  });
    });
    /****************************选择表格注册区域-合同对方、项目、签约主体等选择-end**************************************************************/

    /*****************************日期、导航、字典注册-begin************************************************************/
    layui.use(['laydate', 'nfcontents', 'commonnf', 'treeSelect'], function () {
        var laydate = layui.laydate
          , nfcontents = layui.nfcontents
          , commonnf = layui.commonnf
        treeSelect = layui.treeSelect;
        laydate.render({ elem: '#SngnDateTime', value: new Date() });//签订日期
        laydate.render({ elem: '#EffectiveDateTime', value: new Date() });//生效日期
        laydate.render({ elem: '#PlanCompleteDateTime' });//计划完成日期
        //币种
        commonnf.getCurrency({ selectEls: ["#CurrencyId"] });
        //合同类别
       // commonnf.getdatadic({ dataenum: 1, selectEl: "#ContTypeId" });
        //合同来源
        commonnf.getdatadic({ dataenum: 15, selectEl: "#ContSourceId" });
        //目录
        nfcontents.render({ content: '#customernva' });

        var treeSelect = layui.treeSelect;
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

        /**
      *初始数据字典树
      **/
        function InitDataTree(tvl, dtype) {
            treeSelect.render(
                {
                    elem: "#ContTypeId",
                    data: '/System/DataDictionary/GetTreeSelectData?dataType=' + dtype,
                    method: "GET",
                    verify: true,
                    click: function (d) {
                        $("input[name=ContTypeId]").val(d.current.id);
                    },
                    success: function (d) {
                        if (tvl != null) {
                            treeSelect.checkNode("ContTypeId", tvl);
                        }


                    }

                });
        }
        /**
       *经办机构
       **/
        function InitDeptTree(tval) {
            treeSelect.render(
                {
                    elem: "#DeptId",
                    data: '/System/Department/GetTreeSelectDept',
                    method: "GET",
                    search: true,
                    verify: true,
                    click: function (d) {
                        $("input[name=DeptId]").val(d.current.id);
                    },
                    success: function (d) {
                        if (tval != null) {
                            treeSelect.checkNode("DeptId", tval);
                        }


                    }

                });
        }

        if (contId !== "" && contId !== undefined && contId !== 0) {
            admin.req({
                url: '/Contract/ContractPayment/ShowView',
                data: { Id: contId, rand: wooutil.getRandom() },
                done: function (res) {
                    form.val("NF-ContractPayment-Form", res.Data);
                    if (res.Data.IsFramework == 1) {//框架合同
                        $(".IsFramework").removeClass("layui-hide").addClass("layui-show");
                    }
                    //经办机构
                    InitDeptTree(res.Data.DeptId);
                    //类别
                    InitDataTree(res.Data.ContTypeId, 1);
                    if (res.Data.IsFramework == 1) {
                        //$(".IsFramework").removeClass("layui-hide").addClass("layui-show");
                        //$("#ContAmThod").val("0.00");
                        $("#ContAmThod").attr("readonly", "readonly");
                        $("#ContAmThod").css('background-color', ' #e2e2e2');
                    }

                }
            });
        } else {//新建时
            InitDeptTree(null);
            InitDataTree(null, 1);
        }
        //清除部分下拉小笔头
        wooutil.selpen();
    });
    //标的
    subMetChange.render({ contId: contId });
    if (setter.sysinfo.Mb !== "Mb") {
        $(".mb").addClass("layui-hide");
    }
    exports('paymentContractChange', {});
});