/**
*合同变更
*/
layui.define(['table', 'form'], function (exports) {
    var $ = layui.$
    , table = layui.table
    , setter = layui.setter
    , admin = layui.admin
    , form = layui.form;
    var contId = wooutil.getUrlVar('Id');

    /***********************标的历史-begin***************************************************************************************************/
    table.render({
        elem: '#NF-ContSubHistory'
            , url: '/Contract/ContractChange/GetSubMatterHistoryList?contId=' + contId + '&rand=' + wooutil.getRandom()
       
            , defaultToolbar: ['filter']
            , cols: [[
                { type: 'numbers', fixed: 'left' },
                { type: 'checkbox', fixed: 'left' },
                , { field: 'Id', title: 'Id', width: 50, hide: true }
                , { field: 'Name', title: '标的名称', width: 140 }
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
                //, { title: '操作', width: 150, align: 'center', fixed: 'right', toolbar: '#tabl-contsubbar' }
            ]]
            , page: true
            , loading: true
            , height: setter.table.height_tab
            , limit: setter.table.limit_tab
            , limits: setter.table.limits
    });
    /***********************************标的历史-end***********************************************************/
    /**************************************计划资金-begin******************************************************/
    table.render({
        elem: '#NF-ContPlanceHistory'
           , url: '/Contract/ContractChange/GetPlanceHistoryList?contId=' + contId + '&rand=' + wooutil.getRandom()
           , defaultToolbar: ['filter']
           , cols: [[
               { type: 'numbers', fixed: 'left' },
               { type: 'checkbox', fixed: 'left' },
               , { field: 'Id', title: 'Id', width: 50, hide: true }
               , { field: 'Name', title: '名称', width: 200 }
               , { field: 'AmountMoneyThod', title: '金额', width: 160 }
               , { field: 'SettlModelName', title: '结算方式', width: 160 }
               , { field: 'Remark', title: '备注', width: 280 }
              // , { title: '操作', width: 150, align: 'center', fixed: 'right', toolbar: '#table-contplanfinacebar' }
           ]]
           , page: false
           , loading: true
           , height:setter.table.height_tab
           , limit: setter.table.limit_tab
        // , limits: setter.table.limits

    });
    /***************************************计划资金-end***********************************************************/
    /***********************************************合同文本历史-begin*****************************************************************/
    /***********************合同文本信息-begin***************************************************************************************************/
    table.render({
        elem: '#NF-ContTextHistory'
           , url: '/Contract/ContractChange/GetContTextHistoryList?contId=' + contId + '&rand=' + wooutil.getRandom()
      
           , defaultToolbar: ['filter']
           , cols: [[
               { type: 'numbers', fixed: 'left' },
               { type: 'checkbox', fixed: 'left' },
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
               , { title: '操作', width: 120, align: 'center', fixed: 'right', toolbar: '#table-conttxthistorybar' }
           ]]
           , page: false
           , loading: true
           , height: setter.table.height_tab
           , limit: setter.table.limit_tab
        // , limits: setter.table.limits

    });
    var contTextHisEvent = {
        mydownload: function (url, method, filedir, filename) {
            $('<form action="' + url + '" method="' + (method || 'post') + '">' +  // action请求路径及推送方法
                             '<input type="text" name="filedir" value="' + filedir + '"/>' + // 文件路径
                             '<input type="text" name="filename" value="' + filename + '"/>' + // 文件名称
                         '</form>')
                 .appendTo('body').submit().remove();
        },
        tooldownload: function (obj) {
            wooutil.download({
                url: '/NfCommon/NfAttachment/Download',
                Id: obj.data.Id,
                folder: 6//合同文本


            });
        }
    };
    //列表操作栏
    table.on('tool(NF-ContTextHistory)', function (obj) {
        var _data = obj.data;
        switch (obj.event) {
            case 'download'://下载
                contTextHisEvent.tooldownload(obj);
                break;
            default:
                layer.alert("暂不支持（" + obj.event + "）");
                break;
        }
    });
    /************************************************合同文本历史-end*****************************************************************************/
    exports('contractChange', {});
});