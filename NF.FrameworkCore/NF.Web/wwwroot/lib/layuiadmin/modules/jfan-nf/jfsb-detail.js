
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

      //  Fpdetail();

        /*******************************绑定值-begin*************************************************/
        layui.use('nfcontents', function () {
            var nfcontents = layui.nfcontents;
            //目录
            nfcontents.render({ content: '#customernva' });
            //绑定数据
            if (contId !== "" && contId !== undefined) {
                admin.req({
                    url: '/CaseManagement/Jfsb/ShowView',
                    data: { Id: contId, rand: wooutil.getRandom() },
                    done: function (res) {
                        form.val("NF-Jfsb-Form", res.Data);
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