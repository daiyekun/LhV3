layui.define(['layer', 'table', 'form'], function (exports) {
    var $ = layui.jquery;
    var layer = layui.layer;
    var nfcontents = {
        //渲染
        render: function (param) {
            if (!nfcontents.checkParam(param)) {
                return;
            }
            else {
                nfcontents.init(param);
            }
        },
        init: function (param) {
            $(param.content).removeClass("layui-hide");
            var bar = '<div class="showl" style="cursor:pointer;"><a data-type="showml"><i class="layui-icon layui-icon-console" style="font-size: 30px; color: #FFFFFF;"></i ></a></div>';
            layer.ready(function () {
                layer.open({
                    title: param.title,// '目录',
                    offset: param.offset,// 'r',
                    area: ['140px', '390px'], //param.area,// ['180px', '300px'],
                    type: 1,
                    shade: 0,
                    shadeClose: true,
                    content: $(param.content),
                    cancel: function (index, layero)
                    {
                         layer.open({
                            title: false,
                            closeBtn: 0,
                            offset: 'rb',
                             area: ['40px','40px'],
                             type: 1,
                             shade: 0,
                             skin: 'layui-layer-currbg',
                             shadeClose: true,
                             content: bar,
                             success: function (layero, index) {
                                 $("div[class=showl]").on("click", function () {
                                     $(param.content).removeClass("layui-hide");
                                     nfcontents.init(param);
                                     layer.close(index);
                                 });
                                 $(param.content).addClass("layui-hide");
                             }

                        });
                        
                    }
                    

                });
            });
            var $navs = $('ul[id=' + param.content.replace('#', '')+'] li a');
            $navs.on('click', function (e) {
                e.preventDefault();
                //debugger;
                //定位
                $($(this).attr('href'))[0].scrollIntoView();
                //去掉所有颜色
                $.each($navs, function (index, item) {
                    $(item).css('color', '#000000');
                });
                //设置当前点击对象颜色
                $(this).css('color', '#01AAED');
                //layui-colla-content html 页面这个样式有空格导致查找失败
                //var seldiv = $($($(this).attr('href'))[0]).children("div[class='layui-colla-content']");
                var seldiv = $($($(this).attr('href'))[0]).children("div");
                if (seldiv.length > 0) {
                    seldiv.addClass("layui-show")
                }
               
            });

        },
        checkParam: function (param) {
            if (param.content == "" || param.content == undefined) {
                layer.msg('nfcontents目录控件参数content不能为空', { icon: 5 });
                return false;
            }
            if (param.title == "" || param.title == undefined)
            {
                param.title = "目录";

            }
            if (param.area == "" || param.area == undefined) {
                param.area = ['180px', '300px'];
            }
            if (param.offset == "" || param.offset == undefined) {
                param.offset = 'r';
            }
            return true;
        }
    }


    layui.link(layui.cache.base + 'contents-nf/nfcontents.css');
    exports('nfcontents', nfcontents);
});
