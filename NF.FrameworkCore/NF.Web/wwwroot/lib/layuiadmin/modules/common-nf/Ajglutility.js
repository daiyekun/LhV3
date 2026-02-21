/**
*案件管理工具类
***/
layui.define(['table', 'laydate', 'form'], function (exports) {
    var $ = layui.$
        , admin = layui.admin
        , table = layui.table
        , laydate = layui.laydate
        , setter = layui.setter;
    var contractutility = {
        stateEvent: function (param, name) {
            /// <summary>注册状态流转按钮点击事件</summary>  
            /// <param name="param.tableId" type="string">列表ID</param>
            ///参看ViewModel下的ContractEnum 状态
            ///flowitem：流程事项参照ViewModel 下枚举FlowItemEnums
            ///tostate:修改后状态 ViewModel/ContractEnum
            //状态流转
            $(".downpanel").on("click", ".layui-select-title", function (e) {
                $(this).parents(".layui-form-select").removeClass("layui-form-selected");
                var checkStatus = table.checkStatus(param.tableId)
                    , checkData = checkStatus.data; //得到选中的数据
                if (checkData.length !== 1) {
                    return layer.msg('请选择一条数据！');
                } else if (checkData[0].Dstate == 4 && name != "SuperAdministrator") {//已终止Or已作废Or已完成
                    return layer.msg('当前状态不允许修改！');
                }
                else {
                    if (name == "SuperAdministrator") {

                        $(".datastate").empty();
                        $(".datastate").append('<dd lay-event="stateChange" flowite+m="0" tostate="0"><i class="layui-icon layui-icon-edit"></i>未执行</dd>');
                        $(".datastate").append('<dd lay-event="stateChange" flowitem="1" tostate="1"><i class="layui-icon layui-icon-edit"></i>执行中</dd>');
                        $(".datastate").append('<dd lay-event="stateChange" flowitem="2" tostate="2"><i class="layui-icon layui-icon-edit"></i>已终止</dd>');
                        $(".datastate").append('<dd lay-event="stateChange" flowitem="3" tostate="3"><i class="layui-icon layui-icon-edit"></i>已作废</dd>');
                        $(".datastate").append('<dd lay-event="stateChange" flowitem="4" tostate="4"><i class="layui-icon layui-icon-edit"></i>审批中</dd>');
                        $(".datastate").append('<dd lay-event="stateChange" flowitem="5" tostate="5"><i class="layui-icon layui-icon-edit"></i>被打回</dd>');
                        $(".datastate").append('<dd lay-event="stateChange" flowitem="6" tostate="6"><i class="layui-icon layui-icon-edit"></i>已完成</dd>');
                        $(".datastate").append('<dd lay-event="stateChange" flowitem="8" tostate="8"><i class="layui-icon layui-icon-edit"></i>审批通过</dd>');

                        $(".layui-form-select").not($(this).parents(".layui-form-select")).removeClass("layui-form-selected");
                        $(this).parents(".layui-form-select").toggleClass("layui-form-selected");
                        e.stopPropagation();
                    } else {
                        $(".datastate").empty();
                        switch (checkData[0].Dstate) {
                            
                            case 0:
                            case "0"://当前状态“未执行”
                                {
                                   
                                        $(".datastate").append('<dd lay-event="stateChange" flowitem="1" tostate="1"><i class="layui-icon layui-icon-edit"></i>已立项-->一审中</dd>');
                                }
                                break;
                            case 1:
                            case "1":  //当前状态“执行中”
                                {
                                    $(".datastate").append('<dd lay-event="stateChange" flowitem="2" tostate="2"><i class="layui-icon layui-icon-edit"></i>一审中-->二审中</dd>');
                                  
                                }
                                break;
                            case 2:
                            case "2":  //当前状态“执行中”
                                {
                                    $(".datastate").append('<dd lay-event="stateChange" flowitem="3" tostate="3"><i class="layui-icon layui-icon-edit"></i>二审中-->执行中</dd>');

                                }
                                break;
                            case 3:
                            case "3":  //当前状态“执行中”
                                {
                                    $(".datastate").append('<dd lay-event="stateChange" flowitem="4" tostate="4"><i class="layui-icon layui-icon-edit"></i>执行中-->已结案</dd>');

                                }
                                break;
                           
                            default:
                                return layer.msg('当前状态不允许修改->' + checkData[0].Dstate);
                                break;

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
          //  contractutility.showHideField(flowitem);
            var _fields = new Array();
            _fields.push({
                Id: _Id,
                FieldName: 'Dstate',
                FieldValue: state,
                FieldType: 'int',
                CurrUserId: _userId

            });

            switch (flowitem) {
                case "1"://未执行-->执行中
                case 1:
         
                case "3":
                case 3://执行中-->已完成

                default:
                    {//其他直接修改状态
                        contractutility.requestUpdate(
                            {
                                url: param.url
                                , tableId: param.tableId
                                , fields: _fields
                            });
                    }
                    break
            }
        },
        showHideField: function (flowitem) {
           

        },
           
        requestUpdate: function (param) {
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
    exports('contractutility', contractutility);
});