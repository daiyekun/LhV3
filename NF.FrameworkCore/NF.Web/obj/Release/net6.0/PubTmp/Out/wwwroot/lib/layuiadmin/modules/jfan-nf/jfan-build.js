/**
*收款合同新建
*/
layui.define(['table', 'form', 'subMetBuild', 'wordAddin'], function (exports) {
    var $ = layui.$
        , table = layui.table
        , setter = layui.setter
        , admin = layui.admin
        , form = layui.form
        , subMetBuild = layui.subMetBuild
        , wordAddin = layui.wordAddin
        ;
    var contId = wooutil.getUrlVar('Id');
    if (contId === undefined)
        contId = 0;



   /****************************选择表格注册区域-合同对方、项目、签约主体等选择-begin**************************************************************/
    layui.use(['selectnfitem', 'tableSelect'], function () {
        var tableSelect = layui.tableSelect
            , selectnfitem = layui.selectnfitem;
        //负责人
        selectnfitem.selectUserItem(
            {
                tableSelect: tableSelect,
                elem: '#HandlerIdName',
                hide_elem: '#HandlerId'

            });
        //案件纠纷专用 客户和供应商
        selectnfitem.selectKhGys(
            {
                tableSelect: tableSelect,
                elem: '#CompName',
                hide_elem: '#CompId',
                ctype:0
            });
        //案件纠纷专用 收款合同预付款合同
        selectnfitem.selectSkFk(
            {
                tableSelect: tableSelect,
                elem: '#ContIdName',
                hide_elem: '#ContId',
            });
        
      
    });
    /****************************选择表格注册区域-合同对方、项目、签约主体等选择-end**************************************************************/




    /*****************************日期、导航、字典注册-begin************************************************************/
    layui.use(['laydate', 'nfcontents', 'commonnf', 'treeSelect'], function () {
        var laydate = layui.laydate
            , nfcontents = layui.nfcontents
            , commonnf = layui.commonnf
            , treeSelect = layui.treeSelect;
      
        laydate.render({ elem: '#HandDate', value: new Date(), trigger: 'click' });//生效日期
       //HandDate 开庭事件
        laydate.render({ elem: '#HandDate', value: new Date(), trigger: 'click' });//开庭事件
     
      
       
        commonnf.getdatadic({ dataenum: 39, selectEl: "#Urgent" });
        commonnf.getdatadic({ dataenum: 40, selectEl: "#DisputeType" });
        //目录
        nfcontents.render({ content: '#customernva' });


        //千分位字段
        var thodfields = ['AmountMoney', 'StampTax', 'EstimateAmount', 'AdvanceAmount'];
      
        /**
        *修改
        **/
        if (contId !== "" && contId !== undefined && contId !== 0) {
            admin.req({
                url: '/CaseManagement/Jfsb/ShowView',
                data: { Id: contId, rand: wooutil.getRandom() },
                done: function (res) {
                    form.val("NF-ContractCollection-Form", res.Data);
                    if (res.Data.IsFramework == 1) {//框架合同
                        $(".IsFramework").removeClass("layui-hide").addClass("layui-show");
                    }
                    SetValueHand(res.Data);
                    $("input[name=DeptId]").attr(readonly = "readonly");
                    //经办机构
                    InitDeptTree(res.Data.DeptId);
                    //类别
                    InitDataTree(res.Data.ContTypeId, 1);


                    if (res.Data.IsFramework == 1) {

                        $("#ContAmThod").attr("readonly", "readonly");
                        $("#ContAmThod").css('background-color', ' #e2e2e2');
                    }
                    var T = res.Data.CustomFields;

                    var obj = {};
                    for (var i = 0; i < T.length; i++) {
                        var split = T[i].split(',');
                        obj[split[1].trim()] = split[0].trim();
                    }
                   
                }
            });
        } else {//新建时
            $("input[name=DeptId]").attr(readonly = "readonly");
            //InitDeptTree(null);
            //InitDataTree(null, 1);
        }
        //清除部分下拉小笔头
        wooutil.selpen();
    });
    /*****************************日期、导航、字典注册-end************************************************************/

   
  

    if (setter.sysinfo.Mb !== "Mb") {
        $(".mb").addClass("layui-hide");
    }

    exports('CollectionContractBuild', {});
});