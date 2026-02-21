/******************
 * Woorich Dev Team
 * By: Jnoodle
 *
 * 计划资金页（收款、付款）
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
var businessTypeDisplayName = (businessType == 0 ? "计划收款" : "计划付款");

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
            type: businessType,
            keyword: $("#search").val(),
            start: (_page - 1) * woo.pageItem,
            limit: woo.pageItem,
            username: woo.getData("username"),
            userId: woo.getData("userId"),
        },
    }).done(function (response) {
        var totalCount = response.totalCount;
        var html = "";

        $.each(response.items, function (i, item) {
            html += callback(i, item);
        });
        $("#list-table").find("tbody").html(html);
        $("#list-table").table("refresh");
        //console.log(response);
        //    var totalCount = response.totalCount || response.TotalCount;
        //    var html = "";

        //    $.each((businessType == 0 ? response.SkdList : response.JkdList), function (i, item) {
        //        html += callback(i, item);
        //});

        //$("#list-table").find("tbody").html(html);
        //$("#list-table").table("refresh");


        //点击查看详细
        $("#list-table tbody a").click(function (e) {
            e.preventDefault();
            $("#detailId").val(this.id);
            //TODO
            //console.log($("#detailId").val());
            window.location.href = "contract.html?type=" + businessType + "&id=" + this.id + "#detail";
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
    //var html = '<tr>' +
    //    '<th>' + Element.Id + '</th>' +
    //    '<td class="title"><a href="#" id="' + Element.Id + '" name="' + Element.Name + '">' + Element.Name + '</a></td>' +
    //      '<td class="title"><a href="#" id="' + Element.ID + '" name="' + Element.NAME + '">' + Element.NAME + '</a></td>' +
    //    '<td>' + Element.Code + '</td>' +
    //    '<td>' + Element.FirstContact + '</td>' +
    //    '<td>' + Element.FirstContactTel + '</td>' +
    //    '</tr>';
    //return html;
    //合同名称 ContName
    //合同编号 ContCode
    // 客户名称 CompName
    // 计划资金名称 Name
    // 金额 AmountMoney
    //计划完成日期 PlanCompleteDateTime
    //余额 BalanceThod
    // 完成比例 ContActBl
    var html = '<tr>' +
        '<th>' + Element.Id + '</th>' +
        '<td  style="background:#b7c2cb; color:#FFF">' + Element.ContName +'</td>' +
      //  '<td class="title"><a href="#" id="' + Element.Id + '" name="' + Element.ContName + '">' + Element.ContName + '</a></td>' +
        //  '<td class="title"><a href="#" id="' + (businessType == 0 ? Element.SkdHtid : Element.JkdHtid) + '">' + (businessType == 0 ? Element.SkdHt : Element.JkdHt) + '</a></td>' +
        '<td>' + (businessType == 0 ? Element.ContCode : Element.ContCode) + '</td>' +
        '<td>' + (businessType == 0 ? Element.CompName : Element.CompName) + '</td>' +
        '<td>' + Element.Name + '</td>' +
        '<td class="tr">' + woo.formatMoney("#,##0.00", (businessType == 0 ? Element.AmountMoney : Element.AmountMoney)) + ' 元 </td>' +
        '<td>' + (businessType == 0 ? Element.PlanCompleteDateTime : Element.PlanCompleteDateTime) + '</td>' +
        '<td class="tr">' + woo.formatMoney("#,##0.00", Element.BalanceThod) + ' 元 </td>' +
        '<td>' + Element.ContActBl + ' </td>' +

        '</tr>';
    return html;
};

/**
 * 列表页面是否已刷新：只刷新一次，不必每次载入页面都刷新
 * @type {boolean}
 */
var isListRefreshed = false;


/***********************************************************************************/
var detailId = 0;
var categories;
$(document).on("pageshow", "#detail", function () {
    //更新标题
    changeHeaderTitle("detail", "查看" + businessTypeDisplayName);
    //详细信息Id
    detailId = $("#detailId").val() || woo.getParameterByName("id", $.mobile.path.parseUrl(location.href).search);
    console.log(detailId);
    if (detailId > 0) {
        getCategories(function (response) {
            categories = response;
            $.ajax({
                url: sysUrl + woo.url.companyDetail,
                type: "POST",
                dataType: "jsonp",
                crossDomain: "true",
                data: {
                    detailId: detailId
                }
            }).done(function (response) {
                //console.log(response); 
                var r = response;
                var d = $("#detail_basic");
                //赋值
                d.find('[name="name"]').html(r.Name);// 客户名称
                d.find('[name="no"]').html(r.Code);// 客户编号
                d.find('[name="category"]').html(r.CompanyTypeClass);// 公司类型
                d.find('[name="level"]').html(r.LevelName);// 单位级别
                d.find('[name="clevel"]').html(r.CareditName);// 信用等级
                d.find('[name="state"]').html(r.WfStateDic);// 流程状态
                d.find('[name="principalUser"]').html(r.PrincipalUserDisplayName);// 负责人
                d.find('[name="buildUser"]').html(r.CreateUserDisplayName);//创建人
                d.find('[name="contactname"]').html(r.FirstContact);// 联系人
                d.find('[name="contactjob"]').html(r.FirstContactPosition);// 职位
                d.find('[name="contacttel"]').html(r.FirstContactTel);// 联系人办公电话
                d.find('[name="contactmobile"]').html(r.FirstContactMobile);// 联系人移动电话
                d.find('[name="contactemail"]').html(r.FirstContactEmail);// 邮箱
            })
        })
    }
});

/***************************
 * 列表页（默认）
 **************************/

$(document).on("pageinit", "#index", function () {

    var dataUrl = sysUrl + woo.url.APPfinance_planlist;

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