layui.define(["form","jquery"],function(exports){
    var form = layui.form,
    $ = layui.jquery,
    address = function(){};

    address.prototype.provinces = function() {
        //加一级数据
        var proHtml = '', that = this;
        //../../lib/layuiadmin/modules/address-plug/address.json
        $.get("/Others/Country/Getaddress", function (data) {
           
            for (var i = 0; i < data.length; i++) {
                proHtml += '<option value="' + data[i].code + '">' + data[i].name + '</option>';
            }
            //初始化一级
            $("select[name=CountryId]").append(proHtml);
            form.render();
            form.on('select(CountryId)', function (proData) {
                $("select[name=CityId]").html('<option value="">请选择..</option>');
                var value = proData.value;
                if (value > 0) {
                    that.citys(data[$(this).index() - 1].childs);
                } else {
                    $("select[name=ProvinceId]").attr("disabled", "disabled");
                }
            });
        })
    }

    //加耳二级数据
    address.prototype.citys = function(citys) {
        var cityHtml = '<option value="">请选择..</option>',that = this;
        for (var i = 0; i < citys.length; i++) {
            cityHtml += '<option value="' + citys[i].code + '">' + citys[i].name + '</option>';
        }
        $("select[name=ProvinceId]").html(cityHtml).removeAttr("disabled");
        form.render();
        form.on('select(ProvinceId)', function (cityData) {
            var value = cityData.value;
            if (value > 0) {
                that.areas(citys[$(this).index() - 1].childs);
            } else {
                $("select[name=CityId]").attr("disabled", "disabled");
            }
        });
    }

    //加三级数据
    address.prototype.areas = function(areas) {
        var areaHtml = '<option value="">请选择..</option>';
        for (var i = 0; i < areas.length; i++) {
            areaHtml += '<option value="' + areas[i].code + '">' + areas[i].name + '</option>';
        }
        $("select[name=CityId]").html(areaHtml).removeAttr("disabled");
        form.render();
    }

    var address = new address();
    exports("address",function(){
        address.provinces();
    });
})