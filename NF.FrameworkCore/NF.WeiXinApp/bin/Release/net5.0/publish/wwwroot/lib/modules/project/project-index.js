var ddr = $("#Wx").val();
if (ddr == null || ddr == "") {
    $("#wxnamenull").click();
}

var $url = woowx.constant.APIBaseURL + "/api/Project/Prlist";

var page = 1;
var maxpage;
var pagesize = woowx.constant.page;//每页数据条数
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
                result += '<div class="weui-cells">'
                    + '<a class="weui-cell  weui-cell_access" href="Detail?Id=' + datas.data[i].Id + "&Wxz=" + Wxz + '">'
                   // + '<a class="weui-cell  weui-cell_access" href="Detail?Id=' + datas.data[i].Id + '">'
                    + '<div class="weui-cell__bd">'
                    + ' <p>项目名称</p>'
                    + '</div>'
                    + '<div class="weui-cell__ft list-title">' + datas.data[i].Name + '</div>'
                    + '</a>'
                    + '<div class="weui-cell  " >'
                    + '<div class="weui-cell__bd">'
                    + '<p>项目编号</p>'
                    + '</div>'
                    + '<div class="weui-cell__ft">' + datas.data[i].Code + '</div>'
                    + '</div>'
                    + '<div class="weui-cell">'
                    + '<div class="weui-cell__bd">'
                    + ' <p>项目类别</p>'
                    + '</div>'
                    + '<div class="weui-cell__ft">' + datas.data[i].ProjTypeName + '</div>'
                    + '</div>'
                    + '<div class="weui-cell  " >'
                    + '<div class="weui-cell__bd">'
                    + ' <p>项目状态</p>'
                    + '</div>'
                    + '<div class="weui-cell__ft">' + datas.data[i].PstateDic + '</div>'
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
//            pagesize = 1;
//            page = 1
//            pagesize = parseInt($('#Ts').val()) + pagesize;
//            page = parseInt($('#Pag').val()) + page;
//        }
//    }
//    else {
//        pagesize = 1;
//        page = 1
//        var G = $('#Ts').val();
//        if (G != "" && G != null) {
//            pagesize = parseInt($('#Ts').val()) + pagesize;
//            page = parseInt($('#Pag').val()) + page;
//        }
//    }

//    var HtType = $("#HtType").val();
//    var Wxz = $('#Wx').val();
//    var rre = document.getElementById('searchInput');
//    var ds = { "page": page, "limit": pagesize };
//    function ajaxpage(page) {
//        $.ajax({
//            url: $url
//            , type: "GET"
//            , dataType: "json"
//            , contentType: 'application/json;charset=utf-8'
//            , timeout: 16000,
//            data: { "page": page, "limit": pagesize, "keyWord": $we, "Wxzh": Wxz },

//            beforeSend: function () {
//                //$("#more").show();
//            },
//            success: function (datax) {
//                $('#Pag').val(page);
//                $('#Ts').val(pagesize);
//                var result = '';
//                var datas = JSON.parse(datax);
//                for (var i = 0; i < datas.data.length; i++) {
//                    result += '<div class="page" id="Re" name="Rt" > '
//                        + '<div class="weui-cells__title">' + datas.data[i].Name + '</div>'
//                        + '<div class="weui-cells">'
//                        + '<a class="weui-cell  weui-cell_access" href="Detail?Id=' + datas.data[i].Id + "&Wxz="+Wxz + '">'
//                        + '<div class="weui-cell__bd">'
//                        + '<p>项目名称</p>'
//                        + '</div>'
//                        + '<div class="weui-cell__ft">' + datas.data[i].Name + '</div>'
//                        + '</a>'
//                        + '<a class="weui-cell  weui-cell_access" href="javascript:">'
//                        + '<div class="weui-cell__bd">'
//                        + '<p>项目编号</p>'
//                        + '</div>'
//                        + '<div class="weui-cell__ft">' + datas.data[i].Code + '</div>'
//                        + '</a>'
//                        + '<a class="weui-cell  weui-cell_access" href="javascript:">'
//                        + '<div class="weui-cell__bd">'
//                        + '<p>项目类别</p>'
//                        + '</div>'
//                        + '<div class="weui-cell__ft">' + datas.data[i].ProjTypeName + '</div>'
//                        + '</a>'
//                        + '<a class="weui-cell  weui-cell_access" href="javascript:">'
//                        + '<div class="weui-cell__bd">'
//                        + '<p>项目状态</p>'
//                        + '</div>'
//                        + '<div class="weui-cell__ft">' + datas.data[i].PstateDic + '</div>'
//                        + '</a>'
//                        + '</div>'
//                        + '</div>'
//                }

//                if (pagesize < datas.count) {
//                    result += '<div id="Re" class="page">'
//                        + '<a  onclick="Aj()">更多数据</a>'
//                        + '</div>'
//                    $('#more').hide();
//                } else {
//                    result += '<div class="page">'
//                        + '<p>数据加载完毕</p>'
//                        + '</div>'
//                }

//                $("#rank-list").append(result);
//                maxpage = Math.ceil(datas.length / pagesize);
//                sessionStorage['maxpage'] = maxpage;

//            },
//            timeout: 15000
//        });
//    }

//    $(window).scroll(

//        function () {
//            var scrollTop = $(this).scrollTop();
//            var scrollHeight = $(document).height();
//            var windowHeight = $(this).height();
//            if (scrollTop + windowHeight == scrollHeight) {
//                maxpage = sessionStorage['maxpage'];
//                if (page < maxpage) {
//                    page++;
//                    ajaxpage(page);
//                } else {
//                    //$("#more").html("没有更多数据了");return false;
//                }
//            }

//        });
  
//    $(function () {

//        var l = sessionStorage.getItem('index_list');
//        if (null !== l && '' !== l) {
//            $("#rank-list").html(l);
//            $(window).scrollTop(sessionStorage.getItem('index_scroll'));
//            page = sessionStorage.getItem('index_page');
//            sessionStorage.removeItem('index_list');
//            sessionStorage.removeItem('index_page');
//            sessionStorage.removeItem('index_scroll');
//        } else {
//            ajaxpage(1);
//        }
//    })
//}
/////=============================下拉分页-结束==============================///
