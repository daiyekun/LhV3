/***
* 审批历史记录
* @author dyk 2019.4.11
***/
layui.define(['table', 'form'], function (exports) {
    var $ = layui.$
   , table = layui.table
   , setter = layui.setter
   , admin = layui.admin
        , form = layui.form;
    var logdindex = layer.load(0, { shade: false });
    var wftype = -1;//流程类型
    var appListHist = {
        
        applistInit: function (parm) {
            wftype = parm.objType;
            var $url = '/WorkFlow/AppInst/GetAppHistList?rand=' + wooutil.getRandom();
            $url = $url + '&appObjId=' + parm.Id + '&objType=' + parm.objType
            table.render({
        
                elem: '#NF-workFlow-approveList'
               , url: $url
               //, toolbar: '#toolAppPending'
               , defaultToolbar: ['filter']
               , cellMinWidth: 80
               , cols: [[
                     { type: 'numbers', fixed: 'left' }
                   , { type: 'checkbox', fixed: 'left' }
                   , { field: 'MissionDic', title: '审批事项', width: 150 }
                   , { field: 'StartUserName', title: '发起人', width: 130 }
                   , { field: 'StartDateTime', title: '发起日期', width: 130 }
                   , { field: 'CurrentNodeName', title: '当前节点', width: 160 }
                   , { field: 'AppStateDic', title: '流程状态', width: 130 }
                   , { field: 'CompleteDateTime', title: '完成日期', width: 130 }
                   , { title: '操作', width: 170, align: 'center', fixed: 'right', toolbar: '#applisthist-bar' }
               ]]
               , page: true
               , loading: true
               , height: setter.table.height_4
               , limit: setter.table.limit
               , limits: setter.table.limits
               , done: function (res, curr, count) {   //返回数据执行回调函数
                   layer.close(logdindex);    //返回数据关闭loading



               }

            });


        }


      
    }
    /***查看流程图**/
    function viewflow(obj) {
        var $url = '/WorkFlow/AppInst/ViewFlow?instId=' + obj.data.Id + '&wftype=' + wftype;
        parent.parent.parent.layui.index.openTabsPage($url, '查看流程')
    }
    
  /**
   *审批历史列表
  **/
    table.on('tool(NF-workFlow-approveList)', function (obj) {
        switch (obj.event) {
            case "viewapp"://查看
                viewflow(obj);
                break;
            case "printapp"://审批单
                
                //parent.parent.parent.layui.index.openTabsPage(opurl, '打印审批单')
                
                if (obj.data.AppState == 2) {
                    var opurl = '/WorkFlow/FlowDocToPdf/FlowinceToPdf?WfInceId=' + obj.data.Id;
                    window.open(opurl);
                } else {
                    layer.alert("只有审批通过才允许打印");
                }
                break;
        }
    });
   
    layer.close(logdindex);
    exports('appListHist', appListHist);
});