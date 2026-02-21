layui.define(['table', 'form'], function (exports) {
    var $ = layui.$
        //, table = layui.table
        //, setter = layui.setter
        , admin = layui.admin
        , form = layui.form;

    var commonnf = {

        getdatadic: function (param) {
            /// <summary>数据字典下拉框赋值</summary>  
            /// <param name="param" type="Object">selectEl:select的ID带#，dataenum数据字典类别的enum值。</param>
                admin.req({
                    url: '/System/DataDictionary/GetListByDataEnumType?rand=' + Math.round(Math.random() * (10000 - 1)).toString(),
                    data: { dataEnum: param.dataenum }
                    , async: false//取消异步
                    , done: function (res) {
                       
                        if (param.script === "script") {
                            var slhtml = '<select name="' + param.selectEl + '" id="' + param.selectEl + '" lay-filter="' + param.selectEl + '">'
                            $(res.Data).each(function (i, n) {
                                slhtml = slhtml + "<option value='" + n.Id + "'>" + n.Name + "</option>";
                                // $($("#selectLb").html()).append("<option value='" + n.Id + "'>" + n.Name + "</option>");
                            });
                            slhtml = slhtml + ' </select>';
                            $("#" + param.scriptEl).html(slhtml)
                            //form.render("select");//必须

                        }
                        else {
                            $(res.Data).each(function (i, n) {
                                $(param.selectEl).append("<option value='" + n.Id + "'>" + n.Name + "</option>");
                            });
                        }
                        form.render("select");//必须
                        if (param.wooverify != undefined && param.wooverify) {
                            $(param.selectEl).next("div.layui-form-select").children("div").children("input").addClass("pen");
                            //$(param.selectEl).next("div.layui-form-select").children("div").children("input").addClass("pen");
                        }
                    }
                });
            

        },
       
        setSelectVal: function (param) {
            /// <summary>手动赋值LayUI select控件，并且触发form('select()',function(){})事件</summary>  
            /// <param name="param" type="Object">name:select名称，val值</param>
            $('select[name="' + param.name + '"]').next().find('.layui-anim').children('dd[lay-value="' + param.val + '"]').click();
        },
        getCurrency: function (param) {
            /// <summary>获取币种下拉框</summary> 
            ///<param name="selectEls" type="array">需要赋值的数组</param>
            admin.req({
                url: '/System/CurrencyManager/GetSelectData?rand=' + Math.round(Math.random() * (10000 - 1)).toString(),
                done: function (res) {
                    for (var k = 0; k < param.selectEls.length; k++) {

                        $(res.Data).each(function (i, n) {

                            $(param.selectEls[k]).append("<option value='" + n.Id + "'>" + n.Name + "</option>");

                        });

                    }

                    form.render("select");//必须
                }

            });

        }



    }

    exports('commonnf', commonnf);
});