/***
*收款合同标的交付明细
**/
layui.define(['table', 'nfBcTree', 'laydate', 'soulTable'], function (exports) {
    var $ = layui.$
   , table = layui.table
   , setter = layui.setter
   , admin = layui.admin
    ,laydate = layui.laydate
        , nfBcTree = layui.nfBcTree
        , soulTable = layui.soulTable;
    var initdate = function () {
        //绑定日期控件
        lay('.jf-laydate-item').each(function () {
            laydate.render({
                elem: this
                , trigger: 'click'
            });
        });
    }
    initdate();

    var loadindex = wooutil.loading();
    var subIds = wooutil.getUrlVar('subIds');//选择标的
    //列表
    table.render({
        elem: '#NF-CollSubMetMxIndex'
        , url: '/Contract/ContSubDelivery/GetMainList?fType=0&subIds=' + subIds + '&rand=' + wooutil.getRandom()
        , toolbar: '#toolCollSubMet'
        , defaultToolbar: ['filter']
        , cellMinWidth: 80
        , filter: {
            bottom: false
        }
        , cols: [[
            { type: 'numbers', fixed: 'left' }
            , { type: 'checkbox', fixed: 'left' }
            , { field: 'SubName', title: '标的名称', width: 160, fixed: 'left', filter: true }
            , { field: 'ContName', title: '合同名称', minWidth: 160, width: 180, templet: '#nameTpl', filter: true }
            , { field: 'ContCode', title: '合同编号', width: 150, filter: true}
            , { field: 'ActDate', title: '交付日期', width: 130, filter: true }
            , { field: 'DevNumber', title: '交付数量', width: 140, filter: true}
            , { field: 'DevMoneyThod', title: '交付金额', width: 140}
            , { field: 'DevDz', title: '交付地点', width: 150, filter: true }
            , { field: 'DevFs', title: '交付方式', width: 140, filter: true}
            , { field: 'DevUname', title: '交付人', width: 140}
            , { field: '', title: '附件', width: 120, templet: '#downloadTpl' }
            , { field: 'CompName', title: '合同对方', width: 160, templet: '#compTpl', filter: true }
            , { field: 'HtFzr', title: '合同负责人', width: 130 }
            , { field: 'JbJg', title: '经办机构', width: 140}
            , { field: 'HtZtDic', title: '合同状态', width: 130, templet: '#contractstateTpl', filter: true }
            , { field: 'Unit', title: '标的单位', width: 130, filter: true }
            , { field: 'DjThod', title: '标的单价', width: 140, filter: true }
            , { field: 'Bz1', title: '备用1', width: 140, filter: true }
            , { field: 'Bz2', title: '备用2', width: 140, filter: true }
            , { field: 'PlanDate', title: '计划交付日期', width: 140, filter: true }
            , { field: 'SubId', title: '标的Id', width: 100, hide: true }
            , { field: 'Id', title: 'Id', width: 100, hide: true }

        ]]
        , page: true
        , loading: true
        , height: setter.table.height_4
        , limit: setter.table.limit
        , limits: setter.table.limits
        , done: function (res) {   //返回数据执行回调函数
            soulTable.render(this)
            layer.close(loadindex);    //返回数据关闭loading
            //必须，否则日期控件不能使用
            initdate();


        }

    });
    //监听表格排序
    table.on('sort(NF-CollSubMetMxIndex)', function (obj) {
        table.reload('NF-CollSubMetMxIndex', { //testTable是表格容器id
            initSort: obj //记录初始排序，如果不设的话，将无法标记表头的排序状态。 layui 2.1.1 新增参数
            , where: { //请求参数（注意：这里面的参数可任意定义，并非下面固定的格式）
                orderField: obj.field //排序字段
            , orderType: obj.type //排序方式
            , keyWord: $("input[name=keyWord]").val()//查询关键字
            }
            , page: { curr: 1 }//重新从第 1 页开始
        });


    });

    /***********************************************监听头部工具栏---begin*****************************************************/

    var active = {
        search: function () {//查询
            table.reload('NF-CollSubMetMxIndex', {
                page: { curr: 1 }
                , where: {
                    keyWord: $("input[name=keyWord]").val()
                    , beginData: $("input[name=StratTime]").val()
                    , endData: $("input[name=EndTime]").val()

                }
            });
            subIds = "";
        },
        cateSearch: function () {//类别查询
            nfBcTree.showTree({ el: '#cateDiv', tableId: 'NF-CollSubMetMxIndex', treeId: 'bccateTree', hideFild: '#hdselCateIds' });
        }, download: function (obj) {
            wooutil.download({
                url: '/NfCommon/NfAttachment/Download',
                Id: obj.data.Id,
                folder: 9//交付附件
            });
        },
    };

    table.on('toolbar(NF-CollSubMetMxIndex)', function (obj) {
        switch (obj.event) {
            case 'search':
                active.search();
                break;
            case 'cateSearch'://类别查询
                active.cateSearch();
                break;
            case 'clear'://状态流转
                soulTable.clearCache("NF-CollSubMetMxIndex")
                layer.msg('已还原！', { icon: 1, time: 1000 })
                break;
            case "exportexcel":
                wooutil.exportexcel(obj, {
                    url: "/Contract/ContSubDelivery/ExportExcel?FType=0&cateIds="
                        + $("#hdselCateIds").val()
                        + "&KeyWord=" + $("input[name=keyWord]").val()
                        + "&beginData=" + $("input[name=StratTime]").val()
                        + "&endData=" + $("input[name=EndTime]").val()
                        + "&subIds=" + subIds
                });
                break;
           



        };
    });

    /***********************************************监听头部工具栏---end**************************************************************/
    function opencompview(obj) {
        layer.open({
            type: 2
            , title: '查看详情'
            , content: '/Company/Customer/Detail?Id=' + obj.data.CompId + "&rand=" + wooutil.getRandom()
            , maxmin: true
            , area: ['60%', '80%']
            , btnAlign: 'c'
            , skin: "layer-nf-nfskin"
            , btn: ['关闭']
            , success: function (layero, index) {
                layer.load(0, { shade: false, time: 1 * 1000 });
                layer.full(index);
                wooutil.openTip();

            }
        });
    };
    /**********************************************************监听工具条-begin*********************************************************/

    table.on('tool(NF-CollSubMetMxIndex)', function (obj) {
        switch (obj.event) {
            case "download"://下载
                active.download(obj);
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
            case "contdetail"://合同查看
                {

                    var ress = wooutil.requestpremission({
                        url: '/NfCommon/NfPermission/ViewPermission?rand=' + wooutil.getRandom(),
                        data: {
                            FuncCode: 'querycollcontview',
                            ObjId: obj.data.ContId
                        }

                    });
                    if (ress.RetValue == 0) {
                        wooutil.opencontview(obj);
                    } else {
                        return layer.alert(ress.Msg);
                    }

                }
                break;
            default:
                layer.alert("当前事件没法识别：" + obj.event);
                break;

        }

    });
    /*********************************************************监听工具栏-end***************************************************************************/

    //类别菜单初始化
    nfBcTree.render({ el: '#cateDiv', treeEl: '#BcCategory_Tree', treeId: 'bccateTree', showf: true });

    exports('collSubJiaoFundex', {});
});