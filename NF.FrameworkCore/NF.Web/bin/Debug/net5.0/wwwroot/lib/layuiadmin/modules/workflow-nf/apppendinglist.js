/***
* 待处理
* @author dyk 2019.4.4
***/
layui.define(['table', 'form'], function (exports) {
    var $ = layui.$
   , table = layui.table
   , setter = layui.setter
   , admin = layui.admin
   , form = layui.form;
    var logdindex = layer.load(0, { shade: false });

    table.render({
        elem: '#NF-AppPendingList'
       , url: '/WorkFlow/AppInst/GetAppPendingList?rand=' + wooutil.getRandom()
       , toolbar: '#toolAppPending'
       , defaultToolbar: ['filter']
       , cellMinWidth: 80
       , cols: [[
             { type: 'numbers', fixed: 'left' }
           , { type: 'checkbox', fixed: 'left' }
           , { field: 'ObjTypeDic', title: '审批对象', width: 140, fixed: 'left' }
           , { field: 'AppObjName', title: '对象名称', width: 230, templet: '#approveTpl', fixed: 'left' }
           , { field: 'AppObjNo', title: '对象编号', width: 150 }
           , { field: 'AppObjAmountThod', title: '对象金额', width: 120 }
           , { field: 'MissionDic', title: '审批事项', width: 150 }
           , { field: 'CurrentNodeName', title: '当前节点', width: 160 }
           , { field: 'StartUserName', title: '发起人', width: 130 }
           , { field: 'StartDateTime', title: '发起日期', width: 130 }
           , { field: 'AppStateDic', title: '流程状态', width: 130 }
         
          
       ]]
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

    /**工具栏操作**/
    table.on('tool(NF-AppPendingList)', function (obj) {
        switch (obj.event) {
            case "appingdetail":
                openView(obj);
                break;
        }


    });

    /**
    *点击查看
    **/
    function openView(obj) {
        var $url = '';
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
            case 8://询价
                $url = '/Inquiry/Inquiry/Detail?Id=' + obj.data.AppObjId + "&isFlow=1&rand=" + wooutil.getRandom();
                break;
            case 9://约谈
                $url = '/Questioning/Questioning/Detail?Id=' + obj.data.AppObjId + "&isFlow=1&rand=" + wooutil.getRandom();
                break;
            case 10://招标
                $url = '/Tender/TenderInfo/Detail?Id=' + obj.data.AppObjId + "&isFlow=1&rand=" + wooutil.getRandom();
                break;
            case setter.sysWf.flowType.Hetong://合同
                {
                    if (obj.data.FinceType===0) {//收款
                        $url = '/contract/ContractCollection/Detail?Id=' + obj.data.AppObjId +"&IsSp=1&rand=" + wooutil.getRandom();
                    } else if (obj.data.FinceType === 1) {//付款
                        $url = '/contract/ContractPayment/Detail?Id=' + obj.data.AppObjId + "&IsSp=1&rand=" + wooutil.getRandom();
                    } else {
                        layer.alert("合同资金性质未确定，给不了你答案！");

                    }

                    
                }
                break;
            case setter.sysWf.flowType.Fukuan://付款
                $url = '/Finance/ContActualFinance/ActualFinancePayDetail?Id=' + obj.data.AppObjId +"&isFlow=1&rand=" + wooutil.getRandom();
                break;
            case setter.sysWf.flowType.ShouPiao://收票
                $url = '/Finance/ContInvoice/DetailInvoice?Id=?Id=' + obj.data.AppObjId + "&isFlow=1&rand=" + wooutil.getRandom();
                break;
            case setter.sysWf.flowType.KaiPiao://开票
                $url = '/Finance/ContInvoice/DetailInvoiceOut?Id=?Id=' + obj.data.AppObjId + "&isFlow=1&rand=" + wooutil.getRandom();
                break;
          
            default:
                layer.alert("无法解析您当前操作");
                break;

        };
        layer.open({
            type: 2
               , title: '查看详情'
               , content: $url
               , maxmin: true
               , area: ['60%', '80%']
               , btnAlign: 'c'
               , skin: "layer-nf-nfskin"
               , btn: ['审批','取消']
               , yes: function (index, layero) {
                   option.openAppPage({
                       ObjId: obj.data.AppObjId,
                       InstId: obj.data.Id,
                       ObjType: obj.data.ObjType,
                       ObjMoney: obj.data.AppObjAmount,
                       viewIndex:index
                   });

               }, success: function (layero, index) {
                   layer.full(index);
                   wooutil.openTip();
               }
        });
    }
    /**
    *审批意见相关
    **/
    var option = {
        openAppPage: function (param) {//跳转审批框
            var $url = '/WorkFlow/AppInst/ApprovePage';
            layer.open({
                type: 2
                   , title: '审批'
                   , content: $url
                   , maxmin: true
                   , area: ['680px', '300px']
                   , btnAlign: 'c'
                   , skin: "layer-nf-nfskin"
                   , btn: ['同意', '不同意']
                   , yes: function (index, layero) {
                       var iframeWindow = window['layui-layer-iframe' + index]
                       , submitID = 'NF-Flow-FormSubmit'
                       , submit = layero.find('iframe').contents().find('#' + submitID);
                       iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                           var $option = obj.field.AppOption;//意见
                           var opdata = {
                               ObjId: param.ObjId,
                               InstId: param.InstId,
                               ObjType: param.ObjType,
                               ObjMoney:param.ObjMoney,
                               Option: $option,
                               OptRes: 1//同意
                           };
                           
                           option.agreeSubmit({
                               url: '/WorkFlow/AppInstOption/SubmitAgreeOption',
                               data: opdata,
                               currindex: index,
                               viewIndex:param.viewIndex,
                               tableId: 'NF-AppPendingList'
                           });
                           return false;
                       });
                       submit.trigger('click');

                   }, btn2: function (index, layero) {//不同意
                       var iframeWindow = window['layui-layer-iframe' + index]
                        , submitID = 'NF-Flow-FormSubmit'
                        , submit = layero.find('iframe').contents().find('#' + submitID);
                       iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                           var $option = obj.field.AppOption;//意见
                           var opdata = {
                               ObjId: param.ObjId,
                               InstId: param.InstId,
                               ObjType: param.ObjType,
                               ObjMoney: param.ObjMoney,
                               Option: $option,
                               OptRes: 0//不同意
                           };
                           option.disagreeSubmit({
                               url: '/WorkFlow/AppInstOption/SubmitDisagreeOption',
                               data: opdata,
                               currindex: index,
                               viewIndex: param.viewIndex,
                               tableId: 'NF-AppPendingList'
                           });
                           return false;
                       });
                       submit.trigger('click');
                   },success: function (layero, index) {

                   }
            });
        },
        agreeSubmit: function (param) {//同意审批意见提交
            layer.confirm('你的意见是同意？', {
                btn: ['确定', '取消'] 
            }, function (firmindex) {
                option.submitOption(param);
                layer.close(firmindex);
            }, function () {
              
            });
           
        },
        disagreeSubmit: function (param) {//不同意审批意见提交
            layer.confirm('你的意见是不同意？', {
                btn: ['确定', '取消']
            }, function (firmindex) {
                option.submitOption(param);
                layer.close(firmindex);
            }, function () {

            });

        }, submitOption:function(param) {//提交审批意见
            var logdindex = layer.load(0, { shade: false });
            admin.req({
                url: param.url,
                data: param.data,
                type: 'POST',
                success: function (res) {
                    layer.msg('意见提交成功',
                        { time: 1000, icon: 6 }
                        , function () {
                            layer.close(logdindex);
                            layer.close(param.currindex);
                            layer.close(param.viewIndex);
                            table.reload(param.tableId, {
                                where: { rand: wooutil.getRandom() }

                            });
                        });
                   
                    
                }
            });
        }
      

    }



    exports('appPendingList', {});
});
