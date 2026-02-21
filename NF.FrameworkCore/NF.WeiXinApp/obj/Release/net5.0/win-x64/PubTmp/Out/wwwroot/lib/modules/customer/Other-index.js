var ddr = $("#Wxid").val();
if (ddr == null || ddr == "") {
    $("#wxnamenull").click();
}

var $url = woowx.constant.APIBaseURL + "/api/Company/Khlist";
var pagesize = woowx.constant.page;//每页数据条数
var page = 1;
var maxpage;
///============================搜索-开始=======================///
$(function () {
    var ttp = $('#searchInput').val();
    var ty = $('#searchInput').val();

    var $searchBar = $('#searchBar'),
        $searchResult = $('#searchResult'),
        $searchText = $('#searchText'),
        $searchInput = $('#searchInput'),
        $searchClear = $('#searchClear'),
        $searchCancel = $('#searchCancel');

    function hideSearchResult() {

        $searchResult.hide();
        $searchInput.val('');
    }
    function cancelSearch() {

        hideSearchResult();
        $searchBar.removeClass('weui-search-bar_focusing');
        $searchText.show();
    }

    $searchText.on('click', function () {

        $searchBar.addClass('weui-search-bar_focusing');
        $searchInput.focus();
    });
    $searchInput
        .on('blur', function () {
            $('#keyWord').val(this.value)
            //if (this.value.length>0) {
            $('#Pag').val(0);
            $('#Ts').val(0);
            Aj(page, $url)
            //}
            if (!this.value.length) cancelSearch();

        })
        //.on('input', function () {

        //    
        //    if (this.value.length) {

        //        $searchResult.show();
        //    } else {
        //    
        //        $searchResult.hide();
        //    }
        //})
        ;
    $searchClear.on('click', function () {

        hideSearchResult();
        $searchInput.focus();
    });
    $searchCancel.on('click', function () {

        cancelSearch();
        $searchInput.blur();
    });
});
///============================搜索-结束=======================///


///=============================下拉分页-开始==============================///





$(function () {
    Aj(page, $url); //初始化数据从第一页数据开始请求
});

function del(a, b, c) {
    var removeObj = "";
    for (var i in c) {
        var thisNode = document.getElementById("Re");
        if (thisNode != null && thisNode.parentElement != null) {
            thisNode.parentElement.removeChild(thisNode);
        }
    }
}
function Aj() {

    var thisNode = document.getElementsByName("Rt")
    if (thisNode.length > 0) {

        del('rank-list', 'Rt', thisNode);


    }
    var $we = $('#keyWord').val();

    if ($we == "") {
        var G = $('#Ts').val();
        if (G != "" && G != null) {
            pagesize = 1;
            page = 1
            pagesize = parseInt($('#Ts').val()) + pagesize;
            page = parseInt($('#Pag').val()) + page;
        }
    }
    else {
        pagesize = 1;
        page = 1
        var G = $('#Ts').val();
        if (G != "" && G != null) {
            pagesize = parseInt($('#Ts').val()) + pagesize;
            page = parseInt($('#Pag').val()) + page;
        }
    }
    var Wxz = $('#Wx').val();
    var rre = document.getElementById('searchInput');
    var ds = { "page": page, "limit": pagesize };
    function ajaxpage(page) {
        $.ajax({
            url: $url
            , type: "GET"
            , dataType: "json"
            , contentType: 'application/json;charset=utf-8'
            , timeout: 16000,
            data: { "page": page, "limit": pagesize, "keyWord": $we, "Wxzh": Wxz, "FinanceType": HtType },

            beforeSend: function () {
                //$("#more").show();
            },
            success: function (datax) {
                $('#Pag').val(page);
                $('#Ts').val(pagesize);
                var result = '';
                var datas = JSON.parse(datax);
                for (var i = 0; i < datas.data.length; i++) {
                    result += '<div class="page" id="Re" name="Rt" > '
                        + '<div class="weui-cells__title">' + datas.data[i].Name + '</div>'
                        + '<div class="weui-cells">'
                        + '<a class="weui-cell  weui-cell_access" href="Detail?Id=' + datas.data[i].Id + '">'
                        + '<div class="weui-cell__bd">'
                        + '<p>客户名称</p>'
                        + '</div>'
                        + '<div class="weui-cell__ft list-title">' + datas.data[i].Name + '</div>'
                        + '</a>'
                        + '<div class="weui-cell  " >'
                        + '<div class="weui-cell__bd">'
                        + '<p>客户编号</p>'
                        + '</div>'
                        + '<div class="weui-cell__ft">' + datas.data[i].Code + '</div>'
                        + '</div>'
                        + '<div class="weui-cell " >'
                        + '<div class="weui-cell__bd">'
                        + '<p>类别</p>'
                        + '</div>'
                        + '<div class="weui-cell__ft">' + datas.data[i].CompanyTypeClass + '</div>'
                        + '</div>'
                        + '<div class="weui-cell  " >'
                        + '<div class="weui-cell__bd">'
                        + '<p>状态</p>'
                        + '</div>'
                        + '<div class="weui-cell__ft">' + datas.data[i].CstateDic + '</div>'
                        + '</div>'
                        + '</div>'
                        + '</div>'
                }

                if (pagesize < datas.count) {
                    result += '<div id="Re" class="page">'
                        + '<a  onclick="Aj()">更多数据</a>'
                        + '</div>'
                    $('#more').hide();
                } else {
                    result += '<div class="page">'
                        + '<p>数据加载完毕</p>'
                        + '</div>'
                }

                $("#rank-list").append(result);
                maxpage = Math.ceil(datas.length / pagesize);
                sessionStorage['maxpage'] = maxpage;

            },
            timeout: 15000
        });
    }

    $(window).scroll(

        function () {
            var scrollTop = $(this).scrollTop();
            var scrollHeight = $(document).height();
            var windowHeight = $(this).height();
            if (scrollTop + windowHeight == scrollHeight) {
                maxpage = sessionStorage['maxpage'];
                if (page < maxpage) {
                    page++;
                    ajaxpage(page);
                } else {
                    //$("#more").html("没有更多数据了");return false;
                }
            }

        });
    const goto = (id) => {
        sessionStorage.setItem('index_list', $("#rank-list").html());//存储列表数据
        sessionStorage.setItem('index_page', page);//存储页码
        sessionStorage.setItem('index_scroll', $(window).scrollTop());//存储滚动条位置
        location.href = "Detail.cshtml?id=" + id;
    }
    $(document).on('tap click', "#rank-list .weui-cell", function () {

        id = $(this).datas('id');
        goto(id);
    })
    $(function () {

        var l = sessionStorage.getItem('index_list');
        if (null !== l && '' !== l) {
            $("#rank-list").html(l);
            $(window).scrollTop(sessionStorage.getItem('index_scroll'));
            page = sessionStorage.getItem('index_page');
            sessionStorage.removeItem('index_list');
            sessionStorage.removeItem('index_page');
            sessionStorage.removeItem('index_scroll');
        } else {
            ajaxpage(1);
        }
    })
}
///=============================下拉分页-结束==============================///
