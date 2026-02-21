/**
*项目工具类
***/
layui.define(['table', 'laydate', 'form'], function (exports) {
    var $ = layui.$
    ,admin = layui.admin
    ,table = layui.table
    ,laydate = layui.laydate
    , setter = layui.setter;
    var projectutility = {
        stateEvent:function(param,name) {
            /// <summary>注册状态流转按钮点击事件</summary>  
            /// <param name="param.tableId" type="string">列表ID</param>
            ///参看ViewModel下的ProjStateEnum 状态
            ///flowitem：流程事项参照ViewModel 下枚举FlowItemEnums
            ///tostate:修改后状态 ViewModel/ProjStateEnum
            //状态流转
            $(".downpanel").on("click", ".layui-select-title", function (e) {
                $(this).parents(".layui-form-select").removeClass("layui-form-selected");
                var checkStatus = table.checkStatus(param.tableId)
                      , checkData = checkStatus.data; //得到选中的数据
                if (checkData.length !== 1) {
                    return layer.msg('请选择一条数据！');
                } else if ((checkData[0].Pstate == 3 || checkData[0].Pstate == 4) && name != "SuperAdministrator") {//已终止Or已作废
                    return layer.msg('当前状态不允许修改！');
                }
                else {
                    if (name == "SuperAdministrator") {
                        $(".datastate").empty();
                      
                        $(".datastate").append('<dd lay-event="stateChange" flowitem="0" tostate="0"><i class="layui-icon layui-icon-edit"></i>未执行</dd>');
                        $(".datastate").append('<dd lay-event="stateChange" flowitem="1" tostate="1"><i class="layui-icon layui-icon-edit"></i>执行中</dd>');
                        $(".datastate").append('<dd lay-event="stateChange" flowitem="2" tostate="2"><i class="layui-icon layui-icon-edit"></i>已完成</dd>');
                        $(".datastate").append('<dd lay-event="stateChange" flowitem="3" tostate="3"><i class="layui-icon layui-icon-edit"></i>已终止</dd>');
                        $(".datastate").append('<dd lay-event="stateChange" flowitem="4" tostate="4"><i class="layui-icon layui-icon-edit"></i>已作废</dd>');
                        $(".datastate").append('<dd lay-event="stateChange" flowitem="5" tostate="5"><i class="layui-icon layui-icon-edit"></i>审批通过</dd>');

                        $(".layui-form-select").not($(this).parents(".layui-form-select")).removeClass("layui-form-selected");
                        $(this).parents(".layui-form-select").toggleClass("layui-form-selected");
                        e.stopPropagation();
                    } else {

                        $(".datastate").empty();
                        if (checkData[0].Pstate == 0) {//未执行
                            $(".datastate").append('<dd lay-event="stateChange" flowitem="1" tostate="1"><i class="layui-icon layui-icon-edit"></i>未执行-->执行中</dd>');
                            $(".datastate").append('<dd lay-event="stateChange" flowitem="2" tostate="4"><i class="layui-icon layui-icon-edit"></i>未执行-->已作废</dd>');


                        } else if (checkData[0].Pstate == 1) {//执行中
                            $(".datastate").append('<dd lay-event="stateChange" flowitem="3" tostate="2"><i class="layui-icon layui-icon-edit"></i>执行中-->已完成</dd>');
                            $(".datastate").append('<dd lay-event="stateChange" flowitem="4" tostate="3"><i class="layui-icon layui-icon-edit"></i>执行中-->已终止</dd>');
                        }
                        else if (checkData[0].Pstate == 5) {//审批通过
                            $(".datastate").append('<dd lay-event="stateChange" flowitem="5" tostate="1"><i class="layui-icon layui-icon-edit"></i>审批通过-->执行中</dd>');
                        } else {
                            return layer.msg('当前状态不允许修改->' + checkData[0].Pstate);
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
        updateSate:function(param) {
            /// <summary>修改状态</summary>  
            /// <param name="param.tableId" type="string">列表ID</param>
            /// <param name="param.url" type="string">修改时的URL路径</param>
            /// <param name="param.evtobj" type="Object">当前点击对象</param>
            var checkStatus = table.checkStatus(param.tableId)
              , checkData = checkStatus.data; //得到选中的数据
            var state = $(param.evtobj).attr("tostate");//修改状态
            var flowitem = $(param.evtobj).attr("flowitem");
            var _userId = layui.data(setter.tableName, { key: setter.NFData.userId });
            var _Id = checkData[0].Id;
            /**
            *去请求流程
            **/

            //否则弹框修改时间等
            projectutility.showHideField(flowitem);
            var _fields = new Array();
            _fields.push({
                Id: _Id,
                FieldName: 'Pstate',
                FieldValue: state,
                FieldType: 'int',
                CurrUserId: _userId

            });
            switch (flowitem) {
                case "1"://未执行-->执行中
                case 1:
                    {
                        layer.open({
                            type: 1
                            , title: '修改信息'
                            , area: ['370px', '220px']
                            , btn: ['确定', '取消']
                            , content: $('#updateprojdiv')
                            , yes: function (index, layero) {
                                $("#updateprojdiv").addClass("layui-hide");
                                var begdate = $("#ActualBeginDateTime").val();
                                _fields.push({
                                    Id: _Id,
                                    FieldName: 'ActualBeginDateTime',
                                    FieldValue: begdate,
                                    FieldType: 'date',
                                    CurrUserId: _userId

                                });
                                projectutility.requestUpdate(
                                    {
                                        url: param.url
                                       , tableId: param.tableId
                                       , fields: _fields
                                    });
                                layer.close(index);

                            }, btn2: function (index, layero) {
                                $("#updateprojdiv").addClass("layui-hide");
                            }, cancel: function () {
                                $("#updateprojdiv").addClass("layui-hide");
                            }
                        });
                    }
                    break;
                case "3":
                case 3://执行中-->已完成
                    {
                        layer.open({
                            type: 1
                            , title: '修改信息'
                            , area: ['370px', '220px']
                            , btn: ['确定', '取消']
                            , content: $('#updateprojdiv')
                            , yes: function (index, layero) {
                                $("#updateprojdiv").addClass("layui-hide");
                                var actcomdate = $("#ActualCompleteDateTime").val();
                                _fields.push({
                                    Id: _Id,
                                    FieldName: 'ActualCompleteDateTime',
                                    FieldValue: actcomdate,
                                    FieldType: 'date',
                                    CurrUserId: _userId

                                });
                                projectutility.requestUpdate(
                                    {
                                        url: param.url
                                       , tableId: param.tableId
                                       , fields: _fields
                                    });
                                layer.close(index);

                            }, btn2: function (index, layero) {
                                $("#updateprojdiv").addClass("layui-hide");
                            }, cancel: function () {
                                $("#updateprojdiv").addClass("layui-hide");
                            }
                        });
                    }
                    break;
                default:
                    {//其他直接修改状态
                             projectutility.requestUpdate(
                                 {
                                      url: param.url
                                    , tableId: param.tableId
                                    , fields: _fields
                                 });
                      
                    }
                    break;




            }


        },
        showHideField:function(flowitem) {
            if (flowitem == 1) {//未执行-执行中
                $("#updateprojdiv").removeClass("layui-hide");
                //实际完成时间隐藏
                $("#ActualCompleteDateTime").parent().parent().addClass("layui-hide");
                if ($("#ActualBeginDateTime").parent().parent().hasClass("layui-hide")) {
                    $("#ActualBeginDateTime").parent().parent().removeClass("layui-hide")
                }
                //实际开始日期
                laydate.render({
                    elem: '#ActualBeginDateTime',
                    trigger: 'click'
                });
            } else if (flowitem == 3) {//执行中-->已完成
                $("#updateprojdiv").removeClass("layui-hide");
                //实际开始时间隐藏
                $("#ActualBeginDateTime").parent().parent().addClass("layui-hide");
                if ($("#ActualCompleteDateTime").parent().parent().hasClass("layui-hide")) {
                    $("#ActualCompleteDateTime").parent().parent().removeClass("layui-hide")
                }
                //实际完成时间
                laydate.render({
                    elem: '#ActualCompleteDateTime'
                    , trigger: 'click'
                });
            } else {
                if (!$("#updateprojdiv").hasClass("layui-hide")) {
                    $("#updateprojdiv").addClass("layui-hide");
                }
            }
        },
        requestUpdate:function(param) {
            /// <summary>请求修改</summary>  
            /// <param name="param.tableId" type="string">列表ID</param>
            /// <param name="param.url" type="string">修改时的URL路径</param>
            /// <param name="param.fields" type="array">当前需要修改的字段集合</param>
            // layer.alert(JSON.stringify(param.fields));
            $.post(param.url, { fields: param.fields }, function (res) {
                layer.msg("操作成功", { time: 500, icon: 6 }, function () {
                    table.reload(param.tableId);
                });

            });

        }

    }
    exports('projectutility', projectutility);
});