/***
*收款合同标的
**/
layui.define(['table', 'nfBcTree', 'dynamicCondition', 'soulTable'], function (exports) {
    var $ = layui.$
   , table = layui.table
   , setter = layui.setter
   , admin = layui.admin
        , nfBcTree = layui.nfBcTree
        , dynamicCondition = layui.dynamicCondition
        , soulTable = layui.soulTable;;

    var loadindex = wooutil.loading();
    //列表
    table.render({
        elem: '#NF-CollSubMetIndex'
        , url: '/Contract/ContSubjectMatter/GetMainList?fType=0&rand=' + wooutil.getRandom()
        , toolbar: '#toolCollSubMet'
        , defaultToolbar: ['filter']
        , cellMinWidth: 80
        , overflow: {
            type: 'tips'//内容超过设置
            , color: 'black' // 字体颜色
            , bgColor: 'white' // 背景色
        }
        , cols: [[
            { type: 'numbers', fixed: 'left' }
            , { type: 'checkbox', fixed: 'left' }
            , { field: 'CatePath', title: '所属品类', minWidth: 160, width: 220, fixed: 'left' }
            , { field: 'SubName', title: '标的名称', width: 150, fixed: 'left', filter: true }
            , { field: 'ContName', title: '合同名称', minWidth: 160, width: 180, templet: '#nameTpl', filter: true }
            , { field: 'ContNo', title: '合同编号', width: 150, filter: true }
            , { field: 'Unit', title: '单位', width: 120 }
            , { field: 'PriceThod', title: '单价', width: 140, filter: true }
            , { field: 'Amountstr', title: '数量', width: 140 }
            , { field: 'TotalThod', title: '小计', width: 160, filter: true }
            , { field: 'SalePriceThod', title: '报价', width: 140 }
            , { field: 'Zkl', title: '折扣率', width: 140 }
            , { field: 'Remark', title: '备注', width: 150, hide: true }
            , { field: 'CompName', title: '合同对方', width: 180, filter: true }
            , { field: 'CreateDateTime', title: '标的建立日期', width: 150 }
            , { field: 'PlanDateTime', title: '计划交付日期', width: 150 }
            , { field: 'ComplateAmount', title: '已交付数量', width: 140 }
            , { field: 'NotDelNum', title: '未交付数量', width: 140 }
            , { field: 'JfBl', title: '交付比例', width: 140 }
            , { field: 'SjJfRq', title: '实际交付日期', width: 140 }
            , { field: 'CreateUserName', title: '建立人', width: 140 }
            , { field: 'ContStateDic', title: '合同状态', width: 130, templet: '#contractstateTpl' }
            , { field: 'Id', title: 'Id', width: 100, hide: true }
            , { title: '操作', width: 180, align: 'center', fixed: 'right', toolbar: '#table-CollSubMetIndex-bar' }
        ]]
        , page: true
        , loading: true
        , height: setter.table.height_4
        , limit: setter.table.limit
        , limits: setter.table.limits
        , filter: {
            //列表服务器缓存
            cache: true
            , bottom: false
        }
        , done: function (res) {   //返回数据执行回调函数
            soulTable.render(this)
            layer.close(loadindex);    //返回数据关闭loading
            //更多操作---begin
            $('#moreClick').hover(  //鼠标滑过导航栏目时

                function () {
                    $('#moreEncrypt').show();  //显示下拉列表
                    //设置导航栏目样式
                    //$(this).css({ 'color': 'red', 'background-color': 'orange' });
                },

                function () {
                    $('#moreEncrypt').hide();  //鼠标移开后隐藏下拉列表
                }
            );
            $('#moreEncrypt').hover(  //鼠标滑过下拉列表自身也要显示，防止无法点击下拉列表

                function () {
                    $('#moreEncrypt').show();
                },
                function () {
                    $('#moreEncrypt').hide();
                    //鼠标移开下拉列表后，导航栏目的样式也清除
                    //$('#moreClick').css({ 'color': 'white', 'background-color': 'blue' });
                }
            );


        }

    });
    //清除列表缓存
    //$('#clear').on('click', function () {
    //    soulTable.clearCache("NF-CollSubMetIndex")
    //    layer.msg('已还原！', { icon: 1, time: 1000 })
    //})
    //监听表格排序
    table.on('sort(NF-CollSubMetIndex)', function (obj) {
        table.reload('NF-CollSubMetIndex', { //testTable是表格容器id
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
            table.reload('NF-CollSubMetIndex', {
                page: { curr: 1 }
                , where: {
                    keyWord: $("input[name=keyWord]").val()

                }
            });
        },
        cateSearch: function () {//类别查询
            nfBcTree.showTree({ el: '#cateDiv', tableId: 'NF-CollSubMetIndex', treeId: 'bccateTree', hideFild: '#hdselCateIds' });
        },
        jiaoFuOpen: function (bcIds) {//打开交付页面
            layer.open({
                type: 2
            , title: '标的交付'
            , content: '/Contract/ContSubDelivery/JiaoFu?subIds=' + bcIds.toString()
            , maxmin: true
            , area: ['60%', '80%']
            , btn: ['确定', '取消']
            , btnAlign: 'c'
            , skin: "layer-ext-myskin"
            , yes: function (index, layero) {
                var iframeWindow = window['layui-layer-iframe' + index]
                    , submitID = 'NF-JiaoFu-FormSubmit'
                    , submit = layero.find('iframe').contents().find('#' + submitID);
                //监听提交
                iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
                    var datas = iframeWindow.layui.table.cache.SubDevList;
                    var postdata = [];
                    for (var i = 0; i < datas.length; i++) {
                        var cnum = parseFloat(datas[i].CurrDelNum);
                        if (!isNaN(cnum) && cnum !== 0) {
                            if (datas[i].CurrDelNum)
                                var currobj = new Object();
                            currobj.CurrNumber = datas[i].CurrDelNum;//当前交付数
                            currobj.NotNumber = datas[i].NotDelNum;//剩余未交付数
                            currobj.SubId = datas[i].Id;//标的ID
                            currobj.YanShiNum = datas[i].NotOldDelNum;//之前交付剩余数
                            postdata.push(currobj);
                        }
                    }
                    if (postdata.length <= 0) {
                        return layer.msg('没有交付数据行！', { icon: 5, time: 200 });
                    } else {
                        var _url = '/Contract/ContSubDelivery/BiaoDiJaioFu';

                        admin.req({
                            url: _url,
                            data: { info: obj.field, devDatas: postdata },
                            type: 'POST',
                            done: function (res) {
                                layer.msg("保存成功", { time: 500, icon: 6 }, function () {
                                    layer.close(index);
                                    table.reload("NF-CollSubMetIndex", {
                                        where: { rand: wooutil.getRandom() },
                                        page: { curr: 1 }
                                    });


                                });
                            }
                        });


                    }
                    return false;
                });

                submit.trigger('click');
            },
                success: function (layero, index) {
                    layer.full(index);
                    wooutil.openTip();
                }
            });
        },
        jiaoFu: function () {//交付
            var checkStatus = table.checkStatus('NF-CollSubMetIndex');
            if (checkStatus.data.length <= 0) {
                layer.msg('请选择数据！');
            } else {
                var bcIds = [];
                var notzxz = false;
                var bkyjf = false;//未交付数量0不能交付
                for (var i = 0; i < checkStatus.data.length; i++) {
                    if (checkStatus.data[i].ContState !== 1) {//合同状态不等于执行中
                        notzxz = true;
                    } else if (checkStatus.data[i].NotDelNum === 0) {
                        bkyjf = true;
                    }
                    bcIds.push(checkStatus.data[i].Id);
                }

                if (notzxz) {
                    return layer.msg("合同状态必须是执行中！", { time: 2000, icon: 5 });
                } else if (bkyjf) {
                    return layer.msg("存在已交付完毕标的！", { time: 2000, icon: 5 });
                } else {
                    active.jiaoFuOpen(bcIds);
                }

            }
        }, jiaofumx:function() {
            var checkStatus = table.checkStatus('NF-CollSubMetIndex');
            if (checkStatus.data.length <= 0) {
                layer.msg('请选择数据！');
            } else {
                var subIds = [];
                for (var i = 0; i < checkStatus.data.length; i++) {
                    subIds.push(checkStatus.data[i].Id);
                }
                active.mxrequest(subIds);
            }
        }, mxrequest: function (selIds) {
            parent.layui.index.openTabsPage("/Contract/ContSubDelivery/CollectionIndex?subIds=" + selIds, "收款标的交付明细");
        }
    };

    table.on('toolbar(NF-CollSubMetIndex)', function (obj) {
        switch (obj.event) {
            case 'search':
                active.search();
                break;
            case 'cateSearch'://类别查询
                active.cateSearch();
                break;
            case "exportexcel":
                wooutil.exportexcel(obj, { url: "/Contract/ContSubjectMatter/ExportExcel?FType=0&cateIds=" + $("#hdselCateIds").val() });
                break;
            case "jiaoFu"://交付
                active.jiaoFu();
                break;
            case "jiaoFuMx"://交付明细
                active.jiaofumx();
                break;
            case "clear":
                soulTable.clearCache("NF-CollSubMetIndex")
                layer.msg('已还原！', { icon: 1, time: 1000 })
                break;


        };
    });

    /***********************************************监听头部工具栏---end**************************************************************/

    /**********************************************************监听工具条-begin*********************************************************/
    /**
*打开客户查看页面
**/
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
    table.on('tool(NF-CollSubMetIndex)', function (obj) {
        switch (obj.event) {
            case "jiaofu":
                {

                    if (obj.data.ContState !== 1) {
                        return layer.msg("合同状态必须是执行中！", { time: 2000, icon: 5 });
                    }
                    else if (obj.data.NotDelNum === 0) {
                        return layer.msg("已交付完毕！", { time: 2000, icon: 5 });

                    } else {
                        active.jiaoFuOpen(obj.data.Id);
                    }

                }

                break;
            case "jiaofumx":
                active.mxrequest(obj.data.Id);
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
                layer.alert("当前事件没法识别：" + obj.event);
                break;

        }

    });
    /*********************************************************监听工具栏-end***************************************************************************/

    //类别菜单初始化
    nfBcTree.render({ el: '#cateDiv', treeEl: '#BcCategory_Tree', treeId: 'bccateTree', showf: true });

    exports('collContSubmetIndex', {});
});