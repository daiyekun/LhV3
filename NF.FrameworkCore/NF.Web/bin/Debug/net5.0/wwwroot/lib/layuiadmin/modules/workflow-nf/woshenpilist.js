/***
* 我审批通过-和我打回
* @author dyk 2019.4.4
***/
layui.define(['table', 'form',  'flowEventHandler'], function (exports) {
    var $ = layui.$
   , table = layui.table
   , setter = layui.setter
   , admin = layui.admin
   , form = layui.form
   , flowEventHandler = layui.flowEventHandler;
    var logdindex = layer.load(0, { shade: false });
    var searchType = wooutil.getUrlVar('seaType');
    var _reqUrl = '/WorkFlow/AppInst/GetWoShenPi?rand=' + wooutil.getRandom();
    if (searchType != undefined && searchType === "1") {
        _reqUrl = _reqUrl + "&search=" + searchType;
    }
    _reqUrl = _reqUrl + "&wosp=" + $("input[name=wshenpiType]").val()
    table.render({
        elem: '#NF-WoShenPiList'
       , url: _reqUrl
       , toolbar: '#toolWoShenPi'
       , defaultToolbar: ['filter']
       , cellMinWidth: 80
       , cols: [[
             { type: 'numbers', fixed: 'left' }
           , { type: 'checkbox', fixed: 'left' }
           , { field: 'ObjTypeDic', title: '审批对象', width: 140, fixed: 'left' }
           , { field: 'AppObjName', title: '对象名称', width: 230, templet: '#viewTpl', fixed: 'left' }
           , { field: 'AppObjNo', title: '对象编号', width: 150 }
           , { field: 'AppObjAmountThod', title: '对象金额', width: 120 }
           , { field: 'MissionDic', title: '审批事项', width: 150 }
           , { field: 'CurrentNodeName', title: '当前节点', width: 160 }
           , { field: 'StartUserName', title: '发起人', width: 160 }
           , { field: 'StartDateTime', title: '发起日期', width: 130 }
           , { field: 'Option', title: '我的审批意见', width: 160 }
           , { field: 'OptionDate', title: '审批时间', width: 130 }
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
    table.on('tool(NF-WoShenPiList)', function (obj) {
        switch (obj.event) {
            case "showView"://查看
                flowEventHandler.showView(obj);
                break;
        }


    });


    exports('woshenpiList', {});
});
