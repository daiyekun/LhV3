/******************
 * Woorich Dev Team
 * By: Jnoodle
 *
 * 实际资金页（收款、付款）
 *
 ******************/

//初始化
woo.globalInitialize();

/**
 * 系统地址
 * @type {*}
 */
var sysUrl = woo.getData("systemurl");

/***********************************************************************************/

/**
 * 业务类型：0-收款；1-付款
 * @type {number}
 */
var businessType = woo.getParameterByName("type", location.href) === "1" ? 1 : 0;

/**
 * 业务类型显示文字，用于在页面中显示
 * @type {string}
 */
var businessTypeDisplayName = (businessType == 0 ? "实际收款" : "实际付款");

/**
 * 修改页面header标题
 * @param pageId 页面Id
 * @param title 标题
 */
var changeHeaderTitle = function (pageId, title) {
    $("#" + pageId).find("[data-role=header]").find("h1").text(title);
};
var getCategories = function (doneCallBack) {
    $.ajax({//companyDetail
        url: sysUrl + woo.url.companyDetail,
        // url: sysUrl + woo.url.category,
        type: "POST",
        dataType: "jsonp",
        crossDomain: "true",
        async: false
    }).done(doneCallBack)
        .fail(function () {
            console.log("获取全部类别信息失败!");
        });
};
//var getCategories = function (doneCallBack) {
//    $.ajax({//companyDetail
//        url: sysUrl + woo.url.financeActua_XX,
//        // url: sysUrl + woo.url.category,
//        type: "POST",
//        dataType: "jsonp",
//        crossDomain: "true",
//        async: false
//    }).done(doneCallBack)
//        .fail(function () {
//            console.log("获取全部类别信息失败!");
//        });
//};
/**
 * 获取列表数据
 * @param page
 * @param dataUrl
 * @param callback function(index, Element) return html  用于在给列表赋值时匹配数据，组装html
 */
var getListData = function (page, dataUrl, callback) {
    var _page = 1;
    if (typeof page !== 'undefined') {
        _page = page;
    }
    $.ajax({
        url: dataUrl,
        type: "POST",
        dataType: "jsonp",
        crossDomain: "true",
        data: {
            type: businessType,
            keyword: $("#search").val(),
            start: (_page - 1) * woo.pageItem,
            limit: woo.pageItem,
            limit: woo.pageItem,
            username: woo.getData("username"),
            userId: woo.getData("userId"),
            visibleFields: ",SkdConfirmor,CutomerCategory,SETTLEMENT_CATEGORY_ID,"
        }
    }).done(function (response) {
        //console.log(response);
        var totalCount = response.totalCount || response.TotalCount;
        var html = "";
        $.each(response.items, function (i, item) {
            html += callback(i, item);
        });
        //$.each((businessType == 0 ? response.SkdList : response.JkdList), function (i, item) {
        //    html += callback(i, item);
        //});
        $("#list-table").find("tbody").html(html);
        $("#list-table").table("refresh");

        //点击查看详细
        //    $("#list-table tbody a").click(function (e) {
        //        e.preventDefault();
        //        $("#detailId").val(this.id);
        //        //TODO
        //        //console.log($("#detailId").val());
        //        window.location.href = "contract.html?type=" + businessType + "&id=" + this.id + "#detail";
        //});
        //点击查看详细
        $("#list-table tbody a").click(function (e) {


            $("#detailId").val(this.id);
            //console.log($("#detailId").val());
            var url = $.mobile.path.parseUrl(location.href);
            window.location.href = url.filename + url.search + "&id=" + this.id + "#detail";

        });

        //初始化分页控件
        var wooMaxPage = Math.ceil(totalCount / woo.pageItem);
        if (wooMaxPage == 0)
            wooMaxPage = 1;
        $('.pagination').jqPagination("destroy"); //销毁后重建，防止循环刷新
        $('.pagination').jqPagination({
            max_page: wooMaxPage,
            paged: function (page) {
                getListData(page, dataUrl, getListDataCallBack);
            }
        });

    }).fail(function () {
        console.log(businessTypeDisplayName + "列表访问失败!");
    });
};

/**
 * 获取列表数据时回调函数，用于循环分析列表数据并转换成html
 * @param index 索引值
 * @param Element 数据项
 * @returns {string} 组装的列表html
 */
var getListDataCallBack = function (index, Element) {
    //    <th data-priority="1">序号</th>
    //    <th data-priority="persist">合同名称</th>
    //        <th data-priority="2">合同编号</th>
    //    <th data-priority="3">客户名称</th>
    //        <th data-priority="4">金额</th>
    //    <th data-priority="5">结算日期</th>
    //        <th data-priority="6">结算方式</th>
    //    <th data-priority="7">资金状态</th>
    //    <th data-priority="8">确认人</th>

    var html = '<tr>' +
        '<th>' + Element.Id + '</th>' +
        //    '<td class="title"><a href="#" id="' + (businessType == 0 ? Element.ContId : Element.ContId) + '">' + (businessType == 0 ? Element.ContName : Element.ContName) + '</a></td>' +
        '<td class="title"><a href="#" id="' + Element.ContId + '">' + Element.ContName + '</a></td>' +

        //'<td class="title"><a href="#" id="' + Element.ContId + '"  ' + (businessType == 0 ? Element.ContName : Element.ContName) + '</td>' +
        '<td>' + (businessType == 0 ? Element.ContCode : Element.ContCode) + '</td>' +
        '<td>' + (businessType == 0 ? Element.CompName : Element.CompName) + '</td>' +
        //  '<td>' + Element.Id + '</td>' +
        '<td>' + woo.formatMoney("#,##0.00", Element.AmountMoneyThod) + ' 元  </td>' +
        //     '<td class="title"><a href="#" id="' + (businessType == 0 ? Element.Id : Element.Id) + '">' + woo.formatMoney("#,##0.00", (businessType == 0 ? Element.AmountMoneyThod : Element.AmountMoneyThod)) + ' 元  </a></td>' +
        //'<td class="tr">' + woo.formatMoney("#,##0.00", (businessType == 0 ? Element.AmountMoneyThod : Element.AmountMoneyThod)) + ' 元 </td>' +
        '<td>' + (businessType == 0 ? Element.ActualSettlementDate : Element.ActualSettlementDate) + '</td>' +
        '<td>' + Element.SettlementMethodDic + '</td>' +
        '<td>' + (businessType == 0 ? Element.AstateDic : Element.AstateDic) + '</td>' +
        '<td>' + (businessType == 0 ? Element.ConfirmUserName : Element.ConfirmUserName) + '</td>' +
        '</tr>';
    return html;
};

/**
 * 列表页面是否已刷新：只刷新一次，不必每次载入页面都刷新
 * @type {boolean}
 */
var isListRefreshed = false;


/***********************************************************************************/


/***************************
 * 列表页（默认）
 **************************/

$(document).on("pageinit", "#index", function () {

    var dataUrl = sysUrl + woo.url.financeActualList;

    //更新标题
    changeHeaderTitle("index", businessTypeDisplayName);

    //初始化分页控件
    $('.pagination').jqPagination({
        max_page: $('#maxPage').val(),
        paged: function (page) {
            getListData(page, dataUrl, getListDataCallBack);
        }
    });

    //搜索提交后进行搜索
    $("#searchform").submit(function () {
        getListData(1, dataUrl, getListDataCallBack);
        return false;
    });

    //搜索框清空后进行搜索
    $("#searchform .ui-input-clear").click(function () {
        getListData(1, dataUrl, getListDataCallBack);
    });

    //只刷新一次，不必每次载入页面都刷新
    if (!isListRefreshed) {
        getListData(1, dataUrl, getListDataCallBack);
        isListRefreshed = true;
    }

    //新建
    $("#add_data").click(function (e) {
        e.preventDefault();
        $.mobile.changePage("#add");
        $("#addform")[0].reset(); //重置表单，防止上一次保存的数据滞留
    });

    //返回业务主页面
    $("#backtoindex").click(function (e) {
        e.preventDefault();
        window.location.href = "index.html#index";
    });
});



/***************************
 * 详情页
 * ************************/

var detailId = 0;
var categories;
$(document).on("pageshow", "#detail", function () {
    //判断如果是收款就隐藏银行和账号如果不是就显示 
    if (businessType == 0) {
        $(".ys").hide();
    };

    //更新标题
    changeHeaderTitle("detail", "查看" + businessTypeDisplayName);

    //详细信息Id


    detailId = $("#detailId").val() || woo.getParameterByName("id", $.mobile.path.parseUrl(location.href).search);

    console.log(detailId);
    if (detailId > 0) {
        //获取全部类别信息
        getCategories(function (response) {
            categories = response;

            //获取实际资金基本信息
            $.ajax({
                url: sysUrl + woo.url.financeActua_XX,
                type: "POST",
                dataType: "jsonp",
                crossDomain: "true",
                data: {
                    detailId: detailId
                }
            }).done(function (response) {
                var r = response;
                var d = $("#detail_basic");
                //赋值
                d.find('[name="AmountMoney"]').html(r.AmountMoney);// 金额
                d.find('[name="VoucherNo"]').html(r.VoucherNo);// 票据编号
                d.find('[name="SettlementMethodDic"]').html(r.SettlementMethodDic);// 结算方式
                d.find('[name="ActualSettlementDate"]').html(r.ActualSettlementDate);// 结算日期
                d.find('[name="Bank"]').html(r.Bank);// 开户银行
                d.find('[name="Account"]').html(r.Account);// 账号
                d.find('[name="Reserve1"]').html(r.Reserve1);// 备用1
                d.find('[name="Reserve2"]').html(r.Reserve2);//备用2
                d.find('[name="Remark"]').html(r.Remark);// 备注
            }).fail(function () {
                console.log("获取合同对方基本信息失败!");
            });

            //获取合同基本信息
            $.ajax({
                url: sysUrl + woo.url.financeActua_HT,
                type: "POST",
                dataType: "jsonp",
                crossDomain: "true",
                data: {
                    detailId: detailId
                }
            }).done(function (response) {
                var r = response;
                var d = $("#detail_HT");
                //$.ajax({
                //    url: sysUrl + woo.url.financeActua_HT,
                //    type: "POST",
                //    dataType: "jsonp",
                //    crossDomain: "true",
                //    data: {
                //        detailId: detailId
                //    }
                //}).done(function (response) {
                //    var r = response;
                //    var d = $("#detail_HT");

                //赋值
                d.find('[name="Name"]').html(r.Name);// 合同名称
                d.find('[name="Code"]').html(r.Code);// 合同编号
                d.find('[name="ContAmThod"]').html(r.ContAmThod);// 合同金额
                d.find('[name="CompName"]').html(r.CompName);// 合同对方
                d.find('[name="HtWcJeThod"]').html(r.HtWcJeThod);// 付款金额
                d.find('[name="FaPiaoThod"]').html(r.FaPiaoThod);// 收票金额
            }).fail(function () {
                console.log("获取合同对方基本信息失败!");
            });
            //获取计划资金
            $.ajax({
                url: sysUrl + woo.url.financeActua_JH,
                type: "POST",
                dataType: "jsonp",
                crossDomain: "true",
                data: {
                    detailId: detailId
                }
            }).done(function (response) {
                //console.log(response);
                var table = $("#detail_files_table");
                var html = "";

                $.each(response, function (i, d) {

                    html += '<tr>' +
                        '<th>' + d.Id + '</th>' +
                        '<td class="title">' + d.Name + '</td>' +// 名称
                        '<td>' + d.AmountMoney + '</td>' +//金额
                        '<td>' + d.ConfirmedAmountThod + '</td>' +//已完成金额
                        '<td>' + d.BalanceThod + '</td>' +//余额
                        '<td>' + d.CompRate + '</td>' +//完成比例 
                        '<td>' + d.PlanCompleteDateTime + '</td>' +//计划日期
                        '<td>' + d.SettlModelName + '</td>' +//结算方式
                        '</tr>';

                });
                table.find("tbody").html(html);
                table.table("refresh");
            }).fail(function () {
                console.log("获取合同对方基本信息失败!");
            });

        })

    }
    //返回
    $("#detail_back").click(function () {
        var url = $.mobile.path.parseUrl(location.href);
        var type = woo.getParameterByName("type", url.search);
        window.location.href = url.filename + "?type=" + type;
    });
});