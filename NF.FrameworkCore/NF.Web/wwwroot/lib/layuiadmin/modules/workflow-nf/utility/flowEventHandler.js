/**审批列表事件操作相关**/

layui.define(['table', 'form'], function (exports) {
    var $ = layui.$
         , admin = layui.admin
         , table = layui.table
         , setter = layui.setter
    form = layui.form;
    //流程相关事件
    var flowEventHandler = {
        showView: function (obj) {
            /// <summary>列表页面查看详情</summary>
            ///<param name='obj'>当前点击行对象</param>
            var $url = '';
            //layer.alert(obj.data.AppObjId + ">>" + obj.data.AppSecObjId + ">ID>>" + obj.data.Id);
            switch (obj.data.ObjType) {
               
                case setter.sysWf.flowType.Kh://客户
                    $url = '/Company/Customer/Detail?Id=' + obj.data.AppObjId + "&rand=" + wooutil.getRandom();
                    break;
                case setter.sysWf.flowType.Gys://供应商
                    $url = '/Company/Supplier/Detail?Id=' + obj.data.AppObjId + "&rand=" + wooutil.getRandom();
                    break;
                case setter.sysWf.flowType.Qtdf://其他对方
                    $url = '/Company/Other/Detail?Id=' + obj.data.AppObjId + "&rand=" + wooutil.getRandom();
                    break;
                case setter.sysWf.flowType.Xm://项目
                    $url = '/Project/ProjectManager/Detail?Id=' + obj.data.AppObjId + "&rand=" + wooutil.getRandom();
                    break;
                case setter.sysWf.flowType.Hetong://合同
                    {
                        if (obj.data.FinceType === 0) {//收款
                            $url = '/contract/ContractCollection/Detail?Id=' + obj.data.AppObjId + "&rand=" + wooutil.getRandom();
                        } else if (obj.data.FinceType === 1) {//付款
                            $url = '/contract/ContractPayment/Detail?Id=' + obj.data.AppObjId + "&rand=" + wooutil.getRandom();
                        } else {
                            layer.alert("合同资金性质未确定，给不了你答案！");

                        }


                    }
                    break;
                case setter.sysWf.flowType.Fukuan://付款
                    $url = '/Finance/ContActualFinance/ActualFinancePayDetail?Id=' + obj.data.AppObjId + '&contId=' + obj.data.AppSecObjId + "&isFlow=1&rand=" + wooutil.getRandom();
                    break;
                case setter.sysWf.flowType.ShouPiao://收票
                    $url = '/Finance/ContInvoice/DetailInvoice?Id=' + obj.data.AppObjId + '&contId=' + obj.data.AppSecObjId + "&isFlow=1&rand=" + wooutil.getRandom();
                    break;
                case setter.sysWf.flowType.KaiPiao://开票
                    $url = '/Finance/ContInvoice/DetailInvoiceOut?Id=' + obj.data.AppObjId + '&contId=' + obj.data.AppSecObjId + "&isFlow=1&rand=" + wooutil.getRandom();
                    break;


            }
            wfoption.openView({ tlp: '查看详情', url: $url });

        }, editObject: function (obj) {
            switch (obj.data.ObjType) {
                /// <summary>修改数据信息</summary>
                ///<param name='obj'>当前点击行对象</param>
                case setter.sysWf.flowType.Kh:
                    wfoption.openCustomerEdit({ tlp: '修改客户', objId: obj.data.AppObjId });
                    break;
                case setter.sysWf.flowType.Gys:
                    wfoption.openEditSupplier({ tlp: '修改供应商', objId: obj.data.AppObjId });
                    break;
                case setter.sysWf.flowType.Qtdf:
                    wfoption.openEditOther({ tlp: '修改其他对方', objId: obj.data.AppObjId });
                    break;
                case setter.sysWf.flowType.Xm:
                    wfoption.openEditProject({ tlp: '修改项目', objId: obj.data.AppObjId });
                    break;
                case setter.sysWf.flowType.Hetong:
                    if (obj.data.FinceType === 0) {//收款合同
                        wfoption.openEditCollContract({ tlp: '修改收款合同', objId: obj.data.AppObjId });
                    }
                    else if (obj.data.FinceType === 1) {//付款付款合同
                        wfoption.openEditPayContract({ tlp: '修改付款合同', objId: obj.data.AppObjId });
                    } else {
                        layer.alert("开玩笑，非法数据操作。");

                    }
                    break;
                case setter.sysWf.flowType.Fukuan:
                    wfoption.openEditPayFinance({ tlp: '修改付款单', objId: obj.data.AppObjId, appSecId: obj.data.AppSecObjId });
                    break;
                case setter.sysWf.flowType.ShouPiao:
                    wfoption.openEditInvoice({ tlp: '修改收票', objId: obj.data.AppObjId, appSecId: obj.data.AppSecObjId });
                    break;
                case setter.sysWf.flowType.KaiPiao:
                    wfoption.openEditInvoiceOut({ tlp: '修改开票', objId: obj.data.AppObjId, appSecId: obj.data.AppSecObjId });
                    break;
                default:
                    layer.alert("未知审批类型：" + obj.data.ObjType);
                    break;

            }

        }


    };

    var wfoption = {
        openView: function (param) {
            layer.open({
                type: 2
                , title: param.tlp
                , content: param.url
                , maxmin: true
                , area: ['60%', '80%']
                , btnAlign: 'c'
                , skin: "layer-nf-nfskin"
                , btn: ['关闭']
            , btn1: function (index, layero) {
                layer.close(index);
            }, success: function (layero, index) {
                layer.full(index);
                wooutil.openTip();
            }
            });

        }, openCustomerEdit: function (param) {
            /// <summary>修改客户</summary>
            layer.open({
                type: 2
                , title: param.tlp
                , content: '/Company/Customer/Build?Id=' + param.objId + "&Ctype=0&rand=" + wooutil.getRandom()
                , maxmin: true
                , area: ['60%', '80%']
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

                        wooutil.OpenSubmitForm({
                            url: '/Company/Customer/UpdateSave',
                            table: table,
                            data: field,
                            tableId: 'NF-customer-index',
                            msg: '保存成功',
                            index: index,
                            showtab: true,
                            taburl: '/Company/Customer/Index',
                            tabtip: '客户'

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
         , openEditSupplier: function (param) {
             /// <summary>修改供应商</summary>
             layer.open({
                 type: 2
                 , title: param.tlp
                 , content: '/Company/Supplier/Build?Id=' + param.objId + "&Ctype=1&rand=" + wooutil.getRandom()
                 , maxmin: true
                 , area: ['60%', '80%']
                 , btn: ['确定', '取消']
                 , btnAlign: 'c'
                 , yes: function (index, layero) {
                     var iframeWindow = window['layui-layer-iframe' + index]
                         , submitID = 'NF-supplier-formsubmit'
                         , submit = layero.find('iframe').contents().find('#' + submitID);
                     //监听提交
                     iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                         var field = obj.field; //获取提交的字段
                         var fieldval = field.Name;
                         var fieldcode = field.Code;

                         var resname = wooutil.UniqueValObj({
                             url: '/Company/Supplier/CheckInputValExist',
                             fieldName: 'Name',
                             inputVal: fieldval,
                             currId: field.Id
                         });
                         if (resname) {
                             return layer.msg('此名称已经存在！');
                         }
                         var rescode = wooutil.UniqueValObj({
                             url: '/Company/Supplier/CheckInputValExist',
                             fieldName: 'Code',
                             inputVal: fieldcode,
                             currId: field.Id
                         });
                         if (rescode) {
                             return layer.msg('此编号已经存在！');
                         }
                         wooutil.OpenSubmitForm({
                             url: '/Company/Supplier/UpdateSave',
                             table: table,
                             data: field,
                             tableId: 'NF-supplier-index',
                             msg: '保存成功',
                             index: index,
                             showtab: true,
                             taburl: '/Company/Supplier/Index',
                             tabtip: '供应商'

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
         },
        openEditOther: function (param) {
            /// <summary>修改其他对方</summary>
            layer.open({
                type: 2
                 , title: param.tlp
                 , content: '/Company/Other/Build?Id=' + param.objId + "&Ctype=2&rand=" + wooutil.getRandom()
                 , maxmin: true
                 , area: ['60%', '80%']
                 , btn: ['确定', '取消']
                 , btnAlign: 'c'
                 , yes: function (index, layero) {

                     var iframeWindow = window['layui-layer-iframe' + index]
                         , submitID = 'NF-other-formsubmit'
                         , submit = layero.find('iframe').contents().find('#' + submitID);
                     //监听提交
                     iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                         var field = obj.field; //获取提交的字段
                         var fieldval = field.Name;
                         var fieldcode = field.Code;

                         var resname = wooutil.UniqueValObj({
                             url: '/Company/Other/CheckInputValExist',
                             fieldName: 'Name',
                             inputVal: fieldval,
                             currId: field.Id
                         });
                         if (resname) {
                             return layer.msg('此名称已经存在！');
                         }
                         var rescode = wooutil.UniqueValObj({
                             url: '/Company/Other/CheckInputValExist',
                             fieldName: 'Code',
                             inputVal: fieldcode,
                             currId: field.Id
                         });
                         if (rescode) {
                             return layer.msg('此编号已经存在！');
                         }
                         wooutil.OpenSubmitForm({
                             url: '/Company/Other/UpdateSave',
                             table: table,
                             data: field,
                             tableId: 'NF-other-index',
                             msg: '保存成功',
                             index: index,
                             showtab: true,
                             taburl: '/Company/Other/Index',
                             tabtip: '其他对方'

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
        },
        openEditProject: function (param) {
            /// <summary>修改项目</summary>
            layer.open({
                type: 2
                    , title: param.tlp
                    , content: '/Project/ProjectManager/Build?Id=' + param.objId + "&rand=" + wooutil.getRandom()
                    , maxmin: true
                    , area: ['60%', '80%']
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
                            wooutil.OpenSubmitForm({
                                url: '/Project/ProjectManager/UpdateSave',
                                table: table,
                                data: field,
                                tableId: 'NF-project-index',
                                msg: '保存成功',
                                index: index,
                                showtab: true,
                                taburl: '/Project/ProjectManager/Index',
                                tabtip: '项目'

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
        },
        openEditCollContract: function (param) {
            /// <summary>修改收款合同</summary>
            layer.open({
                type: 2
                    , title: param.tlp
                    , content: '/Contract/ContractCollection/Build?Id=' + param.objId + "&rand=" + wooutil.getRandom()
                    , maxmin: true
                    , area: ['60%', '80%']
                    , btn: ['确定', '取消']
                    , btnAlign: 'c'
                    , yes: function (index, layero) {

                        var iframeWindow = window['layui-layer-iframe' + index]
                           , submitID = 'NF-ContractCollection-FormSubmit'
                           , submit = layero.find('iframe').contents().find('#' + submitID);
                        //监听提交
                        iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                            var field = obj.field; //获取提交的字段
                            var fieldval = field.Name;
                            var fieldcode = field.Code;

                            var resname = wooutil.UniqueValObj({
                                url: '/Contract/ContractCollection/CheckInputValExist',
                                fieldName: 'Name',
                                inputVal: fieldval,
                                currId: field.Id
                            });
                            if (resname) {
                                return layer.msg('此名称已经存在！');
                            }
                            var rescode = wooutil.UniqueValObj({
                                url: '/Contract/ContractCollection/CheckInputValExist',
                                fieldName: 'Code',
                                inputVal: fieldcode,
                                currId: field.Id
                            });
                            if (rescode) {
                                return layer.msg('此编号已经存在！');
                            }
                            wooutil.OpenSubmitForm({
                                url: '/Contract/ContractCollection/UpdateSave',
                                table: table,
                                data: field,
                                tableId: 'NF-ContractCollection-Index',
                                msg: '保存成功',
                                index: index,
                                showtab: true,
                                taburl: '/Contract/ContractCollection/Index',
                                tabtip: '收款合同'

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
        },
        openEditPayContract: function (param) {
            /// <summary>修改付款合同</summary>
            layer.open({
                type: 2
                    , title: param.tlp
                    , content: '/Contract/ContractPayment/Build?Id=' + param.objId + "&rand=" + wooutil.getRandom()
                    , maxmin: true
                    , area: ['60%', '80%']
                    , btn: ['确定', '取消']
                    , btnAlign: 'c'
                    , yes: function (index, layero) {
                        var iframeWindow = window['layui-layer-iframe' + index]
                            , submitID = 'NF-ContractPayment-FormSubmit'
                            , submit = layero.find('iframe').contents().find('#' + submitID);
                        //监听提交
                        iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                            var field = obj.field; //获取提交的字段
                            var fieldval = field.Name;
                            var fieldcode = field.Code;

                            var resname = wooutil.UniqueValObj({
                                url: '/Contract/ContractPayment/CheckInputValExist',
                                fieldName: 'Name',
                                inputVal: fieldval,
                                currId: field.Id
                            });
                            if (resname) {
                                return layer.msg('此名称已经存在！');
                            }
                            var rescode = wooutil.UniqueValObj({
                                url: '/Contract/ContractPayment/CheckInputValExist',
                                fieldName: 'Code',
                                inputVal: fieldcode,
                                currId: field.Id
                            });
                            if (rescode) {
                                return layer.msg('此编号已经存在！');
                            }
                            wooutil.OpenSubmitForm({
                                url: '/Contract/ContractPayment/UpdateSave',
                                table: table,
                                data: field,
                                tableId: 'NF-ContractPayment-Index',
                                msg: '保存成功',
                                index: index,
                                showtab: true,
                                taburl: '/Contract/ContractPayment/Index',
                                tabtip: '付款合同'

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
        },
        openEditPayFinance: function (param) {
            /// <summary>修改实际付款</summary>
            layer.open({
                type: 2
                    , title: param.tlp
                    , content: '/Finance/ContActualFinance/ActualFinancePayBuild?Id=' + param.objId + "&contId=" + param.appSecId +"&isFlow=1" +"&rand=" + wooutil.getRandom()
                    , maxmin: true
                    , area: ['60%', '80%']
                  , success: function (layero, index) {
                      layer.full(index);
                      wooutil.openTip();
                      if (typeof _success === 'function') {
                          setTimeout(function () {
                              _success();
                          }, 500)


                      }
                  }
            });
        },
        openEditInvoice: function (param) {
            /// <summary>修改收票</summary>
            layer.open({
                type: 2
                    , title: param.tlp
                    , content: '/Finance/ContInvoice/BuildInvoice?Id=' + param.objId + "&contId=" + param.appSecId + "&rand=" + wooutil.getRandom()
                    , maxmin: true
                    , area: ['60%', '80%']
                    , success: function (layero, index) {
                        layer.full(index);
                        wooutil.openTip();
                        if (typeof _success === 'function') {
                            setTimeout(function () {
                                _success();
                            }, 500)


                        }
                    }
            });
        },
        openEditInvoiceOut:function(param) {
            /// <summary>修改开票</summary>
            layer.open({
                type: 2
                     , title: param.tlp
                    , content: '/Finance/ContInvoice/BuildInvoiceOut?Id=' + param.objId + "&contId=" + param.appSecId + "&rand=" + wooutil.getRandom()
                    , maxmin: true
                    , area: ['60%', '80%']
                  , success: function (layero, index) {
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

    }


    exports('flowEventHandler', flowEventHandler);
});