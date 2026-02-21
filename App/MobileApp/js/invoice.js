/******************
 * Woorich Dev Team
 * By: Jnoodle
 *
 * 发票页（收款、付款）
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
 * 业务类型：0-开票；1-收票
 * @type {number}
 */
var businessType = woo.getParameterByName("type", location.href) === "1" ? 1 : 0;

/**
 * 业务类型显示文字，用于在页面中显示
 * @type {string}
 */
var businessTypeDisplayName = (businessType == 0 ? "已开发票" : "已收发票");

/**
 * 修改页面header标题
 * @param pageId 页面Id
 * @param title 标题
 */
var changeHeaderTitle = function (pageId, title) {
    $("#" + pageId).find("[data-role=header]").find("h1").text(title);
};

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
            fType: businessType,
            keyword: $("#search").val(),
            start: (_page - 1) * woo.pageItem,
            limit: woo.pageItem,
            userId: woo.getData("userId"),
        }
    }).done(function (response) {
            //console.log(response);
            var totalCount = response.totalCount || response.TotalCount;
        var html = "";
            $.each(response.items, function (i, item) {
                html += callback(i, item);
            });
            $("#list-table").find("tbody").html(html);
            $("#list-table").table("refresh");

            //点击查看详细
        $("#list-table tbody a").click(function (e) {
            e.preventDefault();
            $("#IncoicedetailId").val(this.id);
                //TODO
                //console.log($("#detailId").val());
            window.location.href = "invoice.html?type=" + businessType + "&id=" + this.id + "&contrid=" + this.contrid + "#Invoicedetail";
            
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
//        <th data-priority="4">发票类型</th>
//    <th data-priority="5">发票号</th>
//        <th data-priority="6">金额</th>
//    <th data-priority="7">状态</th>
//    <th data-priority="8">确认人</th>

    var html = '<tr>' +
        '<th>' + Element.Id + '</th>' +
        '<td class="title"><a href="#" id="' + Element.Id + '">' + Element.ContractnName + '</a></td>' +
        '<td>' + Element.contractNO + '</td>' +
        '<td>' + Element.CompayName + '</td>' +
        '<td>' + Element.FapiaoType + '</td>' +
        '<td>' + Element.FapiaoNO + '</td>' +
        '<td class="tr">' + woo.formatMoney("#,##0.00", Element.AmountMoney) + ' 元 </td>' +
        '<td>' + Element.FapiaoState + '</td>' +
        '<td>' + Element.ConfirmUserName + '</td>' +
        '</tr>';
    return html;
};

/**
 * 列表页面是否已刷新：只刷新一次，不必每次载入页面都刷新
 * @type {boolean}
 */
var isListRefreshed = false;
/***************************
 * 列表页（默认）
 **************************/

$(document).on("pageinit", "#index", function () {

    var dataUrl = sysUrl + woo.url.appfinanceInvoiceList;
    
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

var detailId = 0;
var historyId = 0;
var categories;
$(document).on("pageshow", "#Invoicedetail", function () {
  
    //更新标题
    changeHeaderTitle("Invoicedetail", businessTypeDisplayName);
    //详细信息Id
    detailId = $("#IncoicedetailId").val() || woo.getParameterByName("id", $.mobile.path.parseUrl(location.href).search);
    console.log(detailId);
    if (detailId > 0) {
        //获取基本信息
        $.ajax({
            url: sysUrl + woo.url.aPPfinanceInvoiceDert ,
            type: "POST",
            dataType: "jsonp",
            crossDomain: "true",
            data: {
                detailId: detailId
            }
        }).done(function (response) {
                        var r = response;
            var d = $("#detail_Invoicebasic");
            //赋值
            d.find('[name="FapiaoType"]').html(r.FapiaoType);//发票类别
            d.find('[name="AmountMoney"]').html(woo.formatMoney("#,##0.00", r.AmountMoney) + ' 元');//发票金额
            d.find('[name="InCode"]').html(r.InCode);//发票号
            d.find('[name="MakeOutDateTime"]').html(r.MakeOutDateTime);//开票时间
            d.find('[name="CreateUserName"]').html(r.CreateUserName);//建立人
            d.find('[name="ConfirmUserName"]').html(r.PrincipalUserName);//确认人
            d.find('[name="FapiaoState"]').html(r.FapiaoState);//状态
        })

        //获取合同基本信息
        $.ajax({
            url: sysUrl + woo.url.aPPfinanceInvoiceCONT,
            type: "POST",
            dataType: "jsonp",
            crossDomain: "true",
            data: {
                detailId: detailId
            }
        }).done(function (response) {
            var r = response;
            var E = $("#detail_Contractbasic");
            E.find('[name="name"]').html(r.Name);//合同名称
            E.find('[name="no"]').html(r.Code);//合同编号
            E.find('[name="amount"]').html(woo.formatMoney("#,##0.00", r.ContAmThod) + ' 元');//合同金额
            E.find('[name="company"]').html(r.CompName);//合同对方 
            E.find('[name="SKam"]').html(woo.formatMoney("#,##0.00", r.ContAmThod) + ' 元');//付款金额
            E.find('[name="SPam"]').html(woo.formatMoney("#,##0.00", r.ContAmThod) + ' 元');//收票金额
        })
        }

    //返回
    $("#invoicedetail_back").click(function () {
        
        var url = $.mobile.path.parseUrl(location.href);
        var type = woo.getParameterByName("type", url.search);
        window.location.href = url.filename + "?type=" + type;
    });
    //$("#invoicedetail_back").click(function (e) {

    //    e.preventDefault();
    //    window.location.href = "index.html#index";
    //});

});
