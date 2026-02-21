var ddr = $("#Wx").val();
if (ddr == null || ddr == "") {
    $("#wxnamenull").click();
}
var $url = woowx.constant.APIBaseURL + "/api/WorkFlow/WxYcl";
var pagesize = woowx.constant.page;//每页数据条数
var page = 1;
var maxpage;
var HtType = $("#HtType").val();
var Wxz = $('#Wx').val();
var flag = true;
var $kekword = "";
/**
 * 分页
 * @param {any} spage 开始页
 */
function ajaxpage1(spage) {
    // alert("page:" + spage + "，limit=" + pagesize + ",keyWord=" + $kekword + ",Wxzh=" + Wxz + ",FinanceType=" + HtType);
    $.ajax({
        type: "GET",
        url: $url,
        data: { "page": page, "limit": pagesize, "keyWord": $kekword, "Wxzh": Wxz },
        // data: { "page": spage, "limit": pagesize, "keyWord": $kekword, "wxzh": Wxz, "FinanceType": HtType },
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
                var Utl = "";
                /// 客户  供应商  其他对方
                if (datas.data[i].ObjType == 0 || datas.data[i].ObjType == 1 || datas.data[i].ObjType == 2) {
                    Utl = "/Company/Detail?Id=" + datas.data[i].AppObjId + "&FinanceType=" + datas.data[i].ObjType + "&T=6";
                }//合同
                else if (datas.data[i].ObjType == 3) {
                    Utl = "/Contract/Detail?Id=" + datas.data[i].AppObjId + "&T=6";
                }//项目
                else if (datas.data[i].ObjType == 7) {
                    Utl = "/Project/Detail?Id=" + datas.data[i].AppObjId + "&Wxz=" + Wxz + "&T=6";
                }//付款
                else if (datas.data[i].ObjType == 6) {
                    Utl = "/ActualFinance/Detail?Id=" + datas.data[i].AppObjId + "&wxzh=" + Wxz + "&Ftype=1" + "&T=6";
                }//收票    开票
                else if (datas.data[i].ObjType == 4 || datas.data[i].ObjType == 5) {
                    var It = -1;
                    if (datas.data[i].ObjType == 4) {
                        It = 1
                    } else if (datas.data[i].ObjType == 5) {
                        It = 0
                    }
                    Utl = "/ContInvoice/Detail?Id=" + datas.data[i].AppObjId + "&Htid=" + 0 + "&wxzh=" + Wxz + "&Itype=" + It + "&T=6";
                }
                result += '<div class="weui-cells">'
                    //+ '<div class="weui-cells__title">' + datas.data[i].AppObjName + ':' + i + '</div>'
                    + '<div class="weui-cells">'
                    + '<a class="weui-cell  weui-cell_access" href=' + Utl + '">'
                    + '<div class="weui-cell__bd">'
                    + '<p>对象名称</p>'
                    + '</div>'
                    + '<div class="weui-cell__ft list-title">' + datas.data[i].AppObjName + '</div>'
                    + '</a>'
                    + '<div class="weui-cell  " >'
                    + '<div class="weui-cell__bd">'
                    + '<p>对象编号</p>'
                    + '</div>'
                    + '<div class="weui-cell__ft">' + datas.data[i].AppObjNo + '</div>'
                    + '</div>'
                    + '<div class="weui-cell  " >'
                    + '<div class="weui-cell__bd">'
                    + '<p>对象金额</p>'
                    + '</div>'
                    + '<div class="weui-cell__ft">' + datas.data[i].AppObjAmountThod + '</div>'
                    + '</div>'
                    + '<div class="weui-cell  " >'
                    + '<div class="weui-cell__bd">'
                    + '<p>审批事项</p>'
                    + '</div>'
                    + '<div class="weui-cell__ft">' + datas.data[i].MissionDic + '</div>'
                    + '</div>'
                    + '<div class="weui-cell  " >'
                    + '<div class="weui-cell__bd">'
                    + '<p>发起日期</p>'
                    + '</div>'
                    + '<div class="weui-cell__ft">' + datas.data[i].StartDateTime + '</div>'
                    + '</div>'
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


//var pagesize = woowx.constant.page;//每页数据条数
//var page = 1;
//var maxpage;
/////============================搜索-开始=======================///
//$(function () {
//    var ttp = $('#searchInput').val();
//    var ty = $('#searchInput').val();

//    var $searchBar = $('#searchBar'),
//        $searchResult = $('#searchResult'),
//        $searchText = $('#searchText'),
//        $searchInput = $('#searchInput'),
//        $searchClear = $('#searchClear'),
//        $searchCancel = $('#searchCancel');

//    function hideSearchResult() {

//        $searchResult.hide();
//        $searchInput.val('');
//    }
//    function cancelSearch() {

//        hideSearchResult();
//        $searchBar.removeClass('weui-search-bar_focusing');
//        $searchText.show();
//    }

//    $searchText.on('click', function () {

//        $searchBar.addClass('weui-search-bar_focusing');
//        $searchInput.focus();
//    });
//    $searchInput
//        .on('blur', function () {
//            $('#keyWord').val(this.value)
//            //if (this.value.length>0) {
//            $('#Pag').val(0);
//            $('#Ts').val(0);
//            Aj(page, $url)
//            //}
//            if (!this.value.length) cancelSearch();

//        })
//        //.on('input', function () {

//        //    
//        //    if (this.value.length) {

//        //        $searchResult.show();
//        //    } else {
//        //    
//        //        $searchResult.hide();
//        //    }
//        //})
//        ;
//    $searchClear.on('click', function () {

//        hideSearchResult();
//        $searchInput.focus();
//    });
//    $searchCancel.on('click', function () {

//        cancelSearch();
//        $searchInput.blur();
//    });
//});
/////============================搜索-结束=======================///
/////=============================下拉分页-开始==============================///
//$(function () {
//    Aj(page, $url); //初始化数据从第一页数据开始请求
//});
//function del(a, b, c) {
//    var removeObj = "";
//    for (var i in c) {
//        var thisNode = document.getElementById("Re");
//        if (thisNode != null && thisNode.parentElement != null) {
//            thisNode.parentElement.removeChild(thisNode);
//        }
//    }
//}
//function Aj() {

//    var thisNode = document.getElementsByName("Rt")
//    if (thisNode.length > 0) {

//        del('rank-list', 'Rt', thisNode);


//    }
//    var $we = $('#keyWord').val();

//    if ($we == "") {
//        var G = $('#Ts').val();
//        if (G != "" && G != null) {
//            pagesize = 10;
//            page = 1
//            pagesize = parseInt($('#Ts').val()) + pagesize;
//            //  page = parseInt($('#Pag').val()) + page;
//        }
//    }
//    else {
//        pagesize = 10;
//        page = 1
//        var G = $('#Ts').val();
//        if (G != "" && G != null) {
//            pagesize = parseInt($('#Ts').val()) + pagesize;
//            // page = parseInt($('#Pag').val()) + page;
//        }
//    }

//    var HtType = $("#HtType").val();
//    var Wxz = $('#Wx').val();
//    var rre = document.getElementById('searchInput');
//    var ds = { "page": page, "limit": pagesize, "keyWord": $we, "Wxzh": Wxz };

//    function ajaxpage(page) {
//        $.ajax({
//            url: $url
//            , type: "GET"
//            , dataType: "json"
//            , contentType: 'application/json;charset=utf-8'
//            , timeout: 16000,
//            data: ds,//{ "page": page, "limit": pagesize, "keyWord": $we, "Wxzh": Wxz},
//            beforeSend: function () {
//                //$("#more").show();
//            },
//            success: function (datax) {

//                $('#Pag').val(page);
//                $('#Ts').val(pagesize);
//                var result = '';
//                var results = '';
//                var datas = JSON.parse(datax);
//                for (var i = 0; i < datas.data.length; i++) {
//                    result += '<div class="page" id="Re" name="Rt" > '
//                        + '<div class="weui-cells__title">' + datas.data[i].AppObjName + '</div>'
//                        + '<div class="weui-cells">'
//                        + '<a class="weui-cell  weui-cell_access" href="/Contract/Detail?Id=' + datas.data[i].AppObjId + '">'
//                        + '<div class="weui-cell__bd">'
//                        + '<p>对象名称</p>'
//                        + '</div>'
//                        + '<div class="weui-cell__ft">' + datas.data[i].AppObjName + '</div>'
//                        + '</a>'
//                        + '<a class="weui-cell  weui-cell_access" href="javascript:">'
//                        + '<div class="weui-cell__bd">'
//                        + '<p>对象编号</p>'
//                        + '</div>'
//                        + '<div class="weui-cell__ft">' + datas.data[i].AppObjNo + '</div>'
//                        + '</a>'
//                        + '<a class="weui-cell  weui-cell_access" href="javascript:">'
//                        + '<div class="weui-cell__bd">'
//                        + '<p>对象金额</p>'
//                        + '</div>'
//                        + '<div class="weui-cell__ft">' + datas.data[i].AppObjAmountThod + '</div>'
//                        + '</a>'
//                        + '<a class="weui-cell  weui-cell_access" href="javascript:">'
//                        + '<div class="weui-cell__bd">'
//                        + '<p>审批事项</p>'
//                        + '</div>'
//                        + '<div class="weui-cell__ft">' + datas.data[i].MissionDic + '</div>'
//                        + '</a>'
//                        + '<a class="weui-cell  weui-cell_access" href="javascript:">'
//                        + '<div class="weui-cell__bd">'
//                        + '<p>发起日期</p>'
//                        + '</div>'
//                        + '<div class="weui-cell__ft">' + datas.data[i].StartDateTime + '</div>'
//                        + '</a>'
//                        + '</div>'
//                        + '</div>'
//                }

//                if (pagesize < datas.count) {

//                    results += '<div id="Re" class="page">'
//                        + '<a  onclick="Aj()">更多数据</a>'
//                        + '</div>'
//                }
//                else {
//                    results += '<div id="Re" class="page">'
//                        + '<a">加载完毕！</a>'
//                        + '</div>'
//                }
//                $("#rank-list").append(result + results);
//            },
//            timeout: 15000
//        });
//    }


//    $(function () {

//        var l = sessionStorage.getItem('index_list');
//        if (null !== l && "" !== l) {
//            $("#rank-list").html(l);
//            $(window).scrollTop(sessionStorage.getItem('index_scroll'));
//            ajaxpage(1);
//        } else {
//            ajaxpage(1);
//        }
//    })
//}
/////=============================下拉分页-结束==============================///
