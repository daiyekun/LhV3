var ddr = $("#Wx").val();
if (ddr == null || ddr == "") {
    $("#wxnamenull").click();
}


var $url = woowx.constant.APIBaseURL + "/api/ContInvoice/GetInvoce";
var pagesize = woowx.constant.page;//每页数据条数
var page = 1;
var maxpage;
var Itype = $("#Itype").val();
var Wxz =$('#Wx').val();

var flag = true;
var $kekword = "";
/**
 * 分页
 * @param {any} spage 开始页
 */
function ajaxpage1(spage) {
    $.ajax({
        type: "GET",
        url: $url,
        data: { "page": spage, "limit": pagesize, "keyWord": $kekword, "wxzh": Wxz, "Itype": Itype },
        dataType: 'json',
        contentType: 'application/json;charset=utf-8',
        timeout: 16000,
        beforeSend: function (xhr) {
            $('#loading').show();
        },
        success: function (rs) {
          

            var datas = JSON.parse(rs);
            $('#loading').hide();
            if (datas.data == null) {
                $("#getmore").html("没有更多数据了");
                return false
            }
            if (datas.data.length < pagesize) {
                flag = false//设置没有数据了标记
                $("#getmore").html("没有更多数据了");
            }
            var result = '';
            for (var i = 0; i < datas.data.length; i++) {
                result += '<div class="weui-cells">'
                    + '<a class="weui-cell  weui-cell_access" href="Detail?Id=' + datas.data[i].Id + "&Htid=" + datas.data[i].ContId + "&wxzh=" + Wxz + "&Itype=" + Itype + '">'
                    + '<div class="weui-cell__bd">'//ContName
                    + ' <p>发票类型</p>'
                    + '</div>'
                    + '<div class="weui-cell__ft list-title">' + datas.data[i].InTypeName + '</div>'
                    + '</a>'
                    + '<div class="weui-cell " >'
                    + '<div class="weui-cell__bd">'
                    + '<p>合同名称</p>'
                    + '</div>'
                    + '<div class="weui-cell__ft">' + datas.data[i].ContName + '</div>'
                    + '</div>'
                    + '<div class="weui-cell wetitle">'
                    + '<div class="weui-cell__bd">'
                    + '<p>发票金额</p>'
                    + '</div>'
                    + '<div class="weui-cell__ft">' + datas.data[i].AmountMoneyThod + '</div>'
                    + '</div>'
                    + '<div class="weui-cell  weui-cell_access" href="javascript:">'
                    + '<div class="weui-cell__bd">'
                    + ' <p>发票状态</p>'
                    + '</div>'
                    + '<div class="weui-cell__ft">' + datas.data[i].InStateDic + '</div>'
                    + '</div>'
                    + '</div>'
            }
            $("#rank-list").append(result);
        },
        error: function (xhr) {
            alert('ajax出错');
        },
    });
}
//初始化数据从第一页数据开始请求
ajaxpage1(page);
//更多数据
$('#getmore').on('click', function () {
    if (flag) {
        page = page + 1;//如果可以就是页码加1
        ajaxpage1(page);
    }
});
//回车搜索
$('#searchInput').bind('keypress', function (event) {
    if (event.keyCode == "13") {
        $("#rank-list").html("");
        $kekword = $('#searchInput').val();
        page = 1;
        ajaxpage1(page);
    }
});
//搜索按钮
$("#btnsearch").on("click", function () {
    $("#rank-list").html("");
    $kekword = $('#searchInput').val();
    page = 1;
    ajaxpage1(page);
})








