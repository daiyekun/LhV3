/***
*发票帮助类
***/
layui.define(['table', 'form'], function (exports) {
    var $ = layui.$
         , admin = layui.admin
    table = layui.table
    form = layui.form;
    var continvoiceutility = {
        stateEvent: function (param,name) {
            /// <summary>注册状态流转按钮点击事件</summary>  
            /// <param name="param.tableId" type="string">列表ID</param>
            //状态流转
            $(".downpanel").on("click", ".layui-select-title", function (e) {
                $(this).parents(".layui-form-select").removeClass("layui-form-selected");
                var checkStatus = table.checkStatus(param.tableId)
                      , checkData = checkStatus.data; //得到选中的数据
                if (checkData.length !== 1) {
                    return layer.msg('请选择一条数据！');
                } else if ((checkData[0].InState == 2 || checkData[0].InState == 3) && name != "SuperAdministrator") {//已收到、已开出
                    return layer.msg('当前状态不允许修改！');
                }
                else {
                    if (name == "SuperAdministrator") {
                        $(".datastate").empty();
                        $(".datastate").append('<dd lay-event="stateChange" flowitem="0" tostate="0"><i class="layui-icon layui-icon-edit"></i>未提交</dd>');
                        $(".datastate").append('<dd lay-event="stateChange" flowitem="1" tostate="1"><i class="layui-icon layui-icon-edit"></i>已提交</dd>');
                        $(".datastate").append('<dd lay-event="stateChange" flowitem="2" tostate="2"><i class="layui-icon layui-icon-edit"></i>已开出</dd>');
                        $(".datastate").append('<dd lay-event="stateChange" flowitem="3" tostate="3"><i class="layui-icon layui-icon-edit"></i>已收到</dd>');
                        $(".datastate").append('<dd lay-event="stateChange" flowitem="4" tostate="4"><i class="layui-icon layui-icon-edit"></i>被打回</dd>');
                        $(".layui-form-select").not($(this).parents(".layui-form-select")).removeClass("layui-form-selected");
                        $(this).parents(".layui-form-select").toggleClass("layui-form-selected");
                        e.stopPropagation();

                    } else {
                    $(".datastate").empty();
                    if (checkData[0].InState == 0) {//未提交
                        $(".datastate").append('<dd lay-event="stateChange" flowitem="1" tostate="1"><i class="layui-icon layui-icon-edit"></i>未提交-->已提交</dd>');

                    } else if (checkData[0].InState == 1) {//已提交
                        if (checkData[0].FinanceType == 0) {//收款合同开票
                            $(".datastate").append('<dd lay-event="stateChange" flowitem="2" tostate="2"><i class="layui-icon layui-icon-edit"></i>已提交-->已确认</dd>');
                        } else {//付款合同收票
                            $(".datastate").append('<dd lay-event="stateChange" flowitem="2" tostate="3"><i class="layui-icon layui-icon-edit"></i>已提交-->已确认</dd>');
                        }
                        
                        $(".datastate").append('<dd lay-event="stateChange" flowitem="4" tostate="4"><i class="layui-icon layui-icon-edit"></i>已提交-->被打回</dd>');
                    }
                    else {
                        return layer.msg('当前状态不允许修改！');
                    }

                   

                    $(".layui-form-select").not($(this).parents(".layui-form-select")).removeClass("layui-form-selected");
                    $(this).parents(".layui-form-select").toggleClass("layui-form-selected");
                    e.stopPropagation();
                    }
                }
            });
            //点击其他区域时
            $(document).mouseup(function (e) {
                var userSet_con = $('.datastate');
                if (!userSet_con.is(e.target) && userSet_con.has(e.target).length === 0) {
                    if ($(".layui-form-select").hasClass("layui-form-selected")) {
                        $(".layui-form-select").toggleClass("layui-form-selected");
                    }
                    if ($(".datastate").parent().hasClass("layui-form-selected")) {
                        $(".datastate").parent().removeClass("layui-form-selected")
                    }
                }
            });
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
            //更多操作---end
        },
        updateSate: function (param) {
            /// <summary>修改状态</summary>  
            /// <param name="param.tableId" type="string">列表ID</param>
            /// <param name="param.url" type="string">修改时的URL路径</param>
            var checkStatus = table.checkStatus(param.tableId)
              , checkData = checkStatus.data; //得到选中的数据
            var state = $(param.evtobj).attr("tostate");//修改状态
            var flowitem = $(param.evtobj).attr("flowitem");
            if (state == "1") {
                admin.req({
               url: param.url
             , data: { Id: checkData[0].Id, OtherId: checkData[0].ContId, FieldName: 'InState', FieldValue: state, UpdateMoney: checkData[0].AmountMoney }
             , done: function (res) {
                 layer.msg("操作成功", { time: 500, icon: 6 }, function () {
                     table.reload(param.tableId);

                 });
             }
                });
            } else {
                var reslt = wooutil.requestpremission({
                    url: '/NfCommon/NfPermission/UpdatePermission?rand=' + wooutil.getRandom(),
                    data: {
                        FuncCode: param.funcode,
                        ObjId: checkData[0].ContId
                    }
                });
                if (reslt.RetValue == 0) {
                    admin.req({
                        url: param.url
                   , data: { Id: checkData[0].Id, OtherId: checkData[0].ContId, FieldName: 'InState', FieldValue: state, UpdateMoney: checkData[0].AmountMoney }
                   , done: function (res) {
                       layer.msg("操作成功", { time: 500, icon: 6 }, function () {
                           table.reload(param.tableId);

                       });
                   }
                    });
                } else {
                    return layer.alert("无权限");
                }
            }
        }
    }
    exports('continvoiceutility', continvoiceutility);
});