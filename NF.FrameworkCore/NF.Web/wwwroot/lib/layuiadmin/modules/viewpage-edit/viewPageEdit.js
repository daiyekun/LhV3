layui.define(['layer', 'table', 'form', 'laydate'], function (exports) {
    var $ = layui.jquery;
    var layer = layui.layer;
    var form = layui.form;
    var laydate = layui.laydate;
    var currentval = "";

    var viewPageEdit = {
        /// <summary>获取URL参数</summary>
        ///<param name='elem'>编辑字段ID</param>
        ///<param name='parentWidth'>编辑字段容器div宽度样式，如果没有传值就使用默认值</param>
        ///<param name='edittype'>text:文本框，select：下拉框，textarea：文本域,date:时间字段,selTable:表格选择框</param>
        ///<param name='detailwidth'>查看状态下字段宽度,不传入给默认值</param>
        ///<param name='fieldname'>编辑字段的名称，用于传入到后台判断编辑那个字段</param>
        ///<param name='url'>编辑时发送的url</param>
        ///<param name='editwidth'>编辑状态下编辑框宽度</param>
        ///<param name='objid'>编辑时所在对象Id值</param>
         ///<param name='ckEl'>参考对象，没有编辑按钮的参考对象（#+XXX）</param>
        
        //渲染
        render: function (param) {
            setTimeout(function () {
                if (!viewPageEdit.checkParam(param)) {
                    return;
                }
                else {
                    viewPageEdit.init(param);
                }
            }, 2000);
        },
        noUpShow: function (fields) {//没权限时显示正常控件
            $.each(fields, function (index, fieldId) {
                $("#" + fieldId).parent('div.layui-input-inline').removeClass("layui-input-inline")
                    .addClass("layui-input-block");
            });
           
        },
        //初始化
        init: function (param) {
           
            $(param.elem).parent().removeClass("layui-input-block");
            $(param.elem).parent().addClass("layui-input-inline");
               
            var editEl = '<div class="layui-input-inline  layui-input-company nf-edit-div" id="a-div-' + param.elem.replace('#', '') + '"><a href="#" class="a-blue" rel="' + param.elem.replace('#', '') + '">编辑</a><input type="hidden" value="" name="h_' + param.elem.replace('#', '') + '" /></div>';
            var editbtn = '<div class="layui-form-mid layui-word-aux layui-hide" id="btn-div-' + param.elem.replace('#', '') + '"><a style="margin-top:-5px" class="layui-btn layui-btn-sm btn-ok" rel="' + param.elem.replace('#', '') + '">确定</a><a style="margin-top:-5px" class="layui-btn layui-btn-sm btn-cancel" rel="' + param.elem.replace('#', '') + '">取消</a></div>';
            var parentW = $(param.ckEl).parent().width();
            //var parentW4 = $(param.ckEl).parent().width();
            //var parentW3 = $(param.ckEl).parent().width();
            var parentW2 = $(param.ckEl).parent().parent().width();
            var parentW1 = $(param.ckEl).width();
            if ($(param.elem)) {
                param.parentWidth = parentW;
              
                $(param.elem).parent().parent().append(editEl).append(editbtn);
                $(param.elem).parent().css("width", param.parentWidth-28);
                $(param.elem).css("width", param.parentWidth);
                param.detailwidth = param.parentWidth;
                param.parentWidth = param.parentWidth - 28;
               

            }
            if (param.edittype === "textarea")
            {
                $(param.elem).parent().css("width", parentW*2 - 28);
                $(param.elem).css("width", parentW * 2);
                param.detailwidth = parentW * 2+100;
                param.parentWidth = parentW * 2 - 28;
            }
            
            if (param.edittype === "textarea") {
                
                if (param.detailwidth < 700) {
                    param.detailwidth = 700;
                }

            }

            $(param.elem).css("width", param.detailwidth);

            param.editwidth = param.detailwidth - 120;

            var datastr = JSON.stringify(param);
            $("input[type=hidden][name=h_" + param.elem.replace('#', '') + "]").val(datastr);


            form.render();

        },
        // 检查参数
        checkParam: function (param) {
            if (param.elem == "" || param.elem == undefined) {
                layer.msg('viewPageEdit控件参数elem不能为空！', { icon: 5 });
                return false;
            }
            if (param.edittype == "" || param.edittype == undefined) {
                layer.msg('viewPageEdit控件参数edittype不能为空！', { icon: 5 });
                return false;
            }
            if (param.url == "" || param.url == undefined) {
                layer.msg('viewPageEdit控件参数url不能为空！', { icon: 5 });
                return false;
            }
            if (param.objid == "" || param.objid == undefined) {
                layer.msg('viewPageEdit控件参数objId不能为空！', { icon: 5 });
                return false;
            }
            if (param.fieldname == "" || param.fieldname == undefined) {
                layer.msg('viewPageEdit控件参数fieldname不能为空！', { icon: 5 });
                return false;
            }
            if (param.edittype == "selTable") {//列表选择时
                if (param.selobjId == "" || param.selobjId == undefined) {
                    return layer.msg('选择Table时，viewPageEdit控件参数selobjId不能为空！', { icon: 5 });
                }


            }


            return true;
        },
        btnok: function (_this, param) {
          
            var $fieldval = "";
            var $fieldtext = "";//选择表格是显示值
            //确认
            if (param.edittype == "text" || param.edittype == "textarea" || param.edittype == "date") {//文本框
                $fieldval = $(param.elem).val();
                $fieldtext = $(param.elem).val();
                if ($fieldval == "" || $fieldval == undefined) {
                    layer.msg('此内容必填！');
                    return false;

                }
                //$fieldtext = $fieldtext.replace(/,/gi, '');
                   
                
                //var reg = /(^[1-9]([0-9]+)?(\.[0-9]{1,2})?$)|(^(0){1}$)|(^[0-9]\.[0-9]([0-9])?$)/;
                //var res1 = $fieldtext.match(reg);
                //var zbj = param.elem;
                //if (zbj == "#Zbjhis" || zbj == "#Zbjethis") {
                //    if (res1 != null) {

                //        if ($fieldval == "" || $fieldval == undefined) {
                //            layer.msg('此内容必填！');
                //            return false;
                //        }

                //    } else {
                //        layer.msg('请填写数字！');
                //        return false;
                //    }


                //}
            } else if (param.edittype == "select") {
                $fieldval = $('select[name="sel_' + param.fieldname + '"]').val();
                $fieldtext = $('select[name="sel_' + param.fieldname + '"]').find("option:selected").text();
                if ($fieldval == "" || $fieldval == undefined || $fieldval == -1) {
                    layer.msg('请选择！');
                    return false;

                }

            } else if (param.edittype == "selTable") {//table选择框
                $fieldval = $(param.selobjId).val();
                $fieldtext = $(param.elem).val();
                if ($fieldval == "" || $fieldval == undefined) {
                    layer.msg('请选择！');
                    return false;

                }
            } else if (param.edittype == "treeSelect") {//下拉选择树
                $fieldval = $(param.selobjId).val();
                if ($fieldval == "" || $fieldval == undefined) {
                    layer.msg('请选择！');
                    return false;
                }

            }

            $.ajax({
                url: param.url,
                data: { Id: param.objid, fieldVal: $fieldval, fieldName: param.fieldname, rand: wooutil.getRandom() },
                success: function (res) {
                    if (res.code == 5) {
                        return layer.alert("系统错误,请联系管理员");
                    } else {
                        cancelFunction(param, $, $fieldtext);
                    }

                },
                error: function (res) {
                    console.log("修改字段错误-看后台系统日志");
                    return layer.alert("系统错误,请联系管管理>error");
                }


            });

        },
        btncancel: function (_this, param) {
            cancelFunction(param, $, currentval);
        }
    };
    layui.link(layui.cache.base + 'viewpage-edit/viewPageEditing.css');
    //注册编辑按钮点击事件
    $('body').on('click', 'a.a-blue', function (e) {
        var rel = $(this).attr('rel');
        $('#' + rel).removeAttr("readonly");//移除自读属性
        currentval = $('#' + rel).val();
        var pmhval = JSON.parse($("input[type=hidden][name=h_" + rel + "]").val());
        e.preventDefault();
        if (pmhval.edittype == "text" || pmhval.edittype == "textarea" || pmhval.edittype == "date" || pmhval.edittype == "selTable") {//单行多行文本框
            $('#' + rel).removeClass('detail');
            if (pmhval.edittype == "date") {
                laydate.render({
                    elem: '#' + rel
                    , trigger: 'click'
                });
            }
        }
        else if (pmhval.edittype == "select") {//选择下拉框 
            $('#' + rel).addClass('layui-hide');
            if ($('select[name="sel_' + pmhval.fieldname + '"]').length > 0) {
                $('select[name="sel_' + pmhval.fieldname + '"]').next('div .layui-form-select').removeClass('layui-hide').addClass('layui-show');
            } else {
                var selecttmp = '<select name="sel_' + pmhval.fieldname + '" id="sel_' + pmhval.fieldname + '" lay-search="">';
                if (pmhval.editobj.empty) {
                    selecttmp += '<option value="">' + pmhval.editobj.emptyText + '</option>'
                }
                if (pmhval.editobj.url != undefined) {
                    $.ajax({
                        url: pmhval.editobj.url,
                        async: false,//必须
                        success: function (res) {
                            $(res.Data).each(function (i, n) {

                                selecttmp += '<option value="' + n.Id + '">' + n.Name + '</option>';
                            });
                        }
                    });

                } else {
                    $(pmhval.editobj.data).each(function (i, n) {
                        selecttmp += '<option value="' + n.Id + '">' + n.Name + '</option>';
                    });
                }
                selecttmp += '</select>'
                $(pmhval.elem).parent().append(selecttmp);
                form.render("select");
                //如果参数不必填就移除小笔头
                if (pmhval.verify != "required") {
                    $('select[name="sel_' + pmhval.fieldname + '"]').siblings("div").find("input").css("background", "url()").css("padding-left", "10px");
                }
            }
        } else if (pmhval.edittype == "treeSelect") {//下拉选择树
            $('#' + rel).addClass('layui-hide');
            $(pmhval.selobjId).parent().removeClass("layui-hide").addClass('layui-show');
        }

        $("#a-div-" + rel).removeClass('layui-show').addClass('layui-hide');
        

        $('#' + rel).css("width", pmhval.editwidth);
        $('#' + rel).parent().css("width", pmhval.editwidth)
        $("#btn-div-" + rel).removeClass('layui-hide').addClass('layui-show');
        //文本框加小笔头
        if (pmhval.verify == "required") {
            $('#' + rel).addClass('pen');
        }
    });
    //确认事件
    $('body').on('click', 'a.layui-btn.btn-ok', function () {
        var rel = $(this).attr('rel');
        var pmhval = JSON.parse($("input[type=hidden][name=h_" + rel + "]").val());
        viewPageEdit.btnok(this, pmhval);
        $(pmhval.elem).removeClass('pen');
    });
    //取消事件
    $('body').on('click', 'a.layui-btn.btn-cancel', function () {
        var rel = $(this).attr('rel');
        var pmhval = JSON.parse($("input[type=hidden][name=h_" + rel + "]").val());
        viewPageEdit.btncancel(this, pmhval)
    });

    function cancelFunction(param, $, currentval) {
        if (param.edittype == "text" || param.edittype == "textarea" || param.edittype == "date" || param.edittype == "selTable") { //文本框
            $(param.elem).addClass('detail');
            $(param.elem).val(currentval);
            if (param.edittype == "textarea") {
                $(param.elem).css("display", "layui-input-inline");
            }
        }
        else if (param.edittype == "select") {
            $(param.elem).val(currentval);
            $(param.elem).removeClass('layui-hide').addClass('layui-show');
            $('select[name="sel_' + param.fieldname + '"]').next('div .layui-form-select').addClass('layui-hide');
        } else if (param.edittype == "treeSelect") {//下拉选择树
            // $(param.elem).val(currentval);
            $(param.elem).removeClass('layui-hide').addClass('layui-show');
            $(param.selobjId).parent().removeClass("layui-show").addClass('layui-hide');
        }
        $("#a-div-" + param.elem.replace('#', '')).removeClass('layui-hide').addClass('layui-show');
        var pmhval = JSON.parse($("input[type=hidden][name=h_" + param.elem.replace('#', '') + "]").val());
        $(param.elem).parent().css("width", pmhval.parentWidth);
        $(param.elem).css("width", pmhval.detailwidth);

        $("#btn-div-" + param.elem.replace('#', '')).removeClass('layui-show').addClass('layui-hide');
        $(param.elem).removeClass('pen');

    }

    exports('viewPageEdit', viewPageEdit);

});


