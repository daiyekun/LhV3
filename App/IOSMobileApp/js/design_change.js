/******************
 * Woorich Dev Team
 * By: Jnoodle
 *
 * 设计变更单
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
 * 业务类型显示文字，用于在页面中显示
 * @type {string}
 */
var businessTypeDisplayName = "设计变单";

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
            keyword: $("#search").val(),
            start: (_page - 1) * woo.pageItem,
            limit: woo.pageItem
        }
    }).done(function (response) {
        //console.log(response);
        var totalCount = response.totalCount || response.TotalCount;
        var html = "";

        $.each(response.List, function (i, item) {
            html += callback(i, item);
        });
        $("#list-table").find("tbody").html(html);
        $("#list-table").table("refresh");

        //点击查看详细
        $("#list-table tbody a").click(function (e) {
            e.preventDefault();
            $("#detailId").val(this.id);
            //console.log($("#detailId").val());
            window.location.href = "design_change.html?id=" + this.id + "#detail";
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
    //<th data-priority="1">序号</th>
    //<th data-priority="persist">编号</th>
    //<th data-priority="2">工程名称</th>
    //<th data-priority="3">合同编号</th>
    //<th data-priority="4">创建人</th>
    //<th data-priority="5">创建时间</th>
    //<th data-priority="6">当前节点</th>
    //<th data-priority="7">状态</th>

    var html = '<tr>' +
        '<th>' + Element.ID + '</th>' +
        '<td class="title"><a href="#" id="' + Element.ID + '">' + Element.NO + '</a></td>' +
        '<td>' + Element.ContName + '</td>' +
        '<td>' + Element.ContNO + '</td>' +
        '<td>' + Element.CreateUserName + '</td>' +
        '<td>' + Element.CreateDateTime + '</td>' +
        '<td>' + Element.CurrentNodeName + '</td>' +
        '<td>' + Element.DesState + '</td>' +
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

    var dataUrl = sysUrl + woo.url.designChangeList;

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
 * 查看页
 **************************/

var detailId = 0;
$(document).on("pageshow", "#detail", function () {
    //更新标题
    changeHeaderTitle("detail", "查看" + businessTypeDisplayName);

    //详细信息Id
    detailId = $("#detailId").val() || woo.getParameterByName("id", $.mobile.path.parseUrl(location.href).search);

    console.log(detailId);

    if (detailId > 0) {
        //获取基本信息
        $.ajax({
            url: sysUrl + woo.url.designChangeDetail,
            type: "POST",
            dataType: "jsonp",
            crossDomain: "true",
            data: {
                detailId: detailId
            }
        }).done(function (response) {
            //console.log(response);
            var r = response[0];
            var d = $("#detail_basic");
            //赋值
            d.find('[name="no"]').html(r.no);
            d.find('[name="gcname"]').html(r.gcname);
            d.find('[name="htno"]').html(r.htno);
            d.find('[name="lxdw"]').html(r.lxdw);
            d.find('[name="bgyy"]').html(r.bgyy);
            d.find('[name="bgnr"]').html(r.bgnr);           
            d.find('[name="cjr"]').html(r.cjr);
            d.find('[name="cjsj"]').html(woo.GetDateFormat(r.cjsj));
            d.find('[name="state"]').html(r.state);

            historyId = parseInt(r.historyId);

            if (r.wfstate && r.wfstate.length > 1) {
                //在审批中，隐藏提交审批
                $("#detail_menu").find("#detail_menu_commitWorkflow").hide();
            }

            //判断修改状态权限
            $.ajax({
                //url: sysUrl + woo.url.permission,
                //type: "POST",
                //dataType: "jsonp",
                //crossDomain: "true",
                //data: {
                //    permissionname: "合同状态修改",
                //    id: detailId
                //}
            }).done(function (response) {
                if (!response.success) {
                    $("#detail_menu").find("#detail_menu_commitWorkflow").hide();
                }
            })
                .fail(function () {
                    console.log("获取权限信息失败!");
                });

        }).fail(function () {
            console.log("获取设计变更单基本信息失败!");
        });

        //获取设计变更单附件信息
        $.ajax({
            url: sysUrl + woo.url.designChangeDetailFiles,
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
                var type = "";
                html += '<tr>' +
                    '<th>' + d.id + '</th>' +
                    '<td class="title"><a href="' + sysUrl + d.path + '" target="_blank" onclick="window.open(this.href,\'_system\'); return false;">' + d.name + '</a></td>' +
                    '<td>' + type + '</td>' +
                    '<td>' + d.remark + '</td>' +
                    '</tr>';
            });
            table.find("tbody").html(html);
            table.table("refresh");
        }).fail(function () {
            console.log("获取设计变更单附件信息失败!");
        });

        //获取设计变更单审批记录信息
        $.ajax({
            url: sysUrl + woo.url.designChangeDetailWorkflow,
            type: "POST",
            dataType: "jsonp",
            crossDomain: "true",
            data: {
                detailId: detailId
            }
        }).done(function (response) {
            //console.log(response);
            var table = $("#detail_workflow_table");
            var html = "";
            $.each(response.WfHistoryList, function (i, d) {
                html += '<tr>' +
                    '<th>' + d.ID + '</th>' +
                    '<td class="title">' + d.MISSION + '</td>' +
                    '<td>' + d.START_DATETIME + '</td>' +
                    '<td>' + d.COMPLETE_DATETIME + '</td>' +
                    '<td>' + d.CURRENT_NODE_NAME + '</td>' +
                    '<td>' + d.STATE + '</td>' +
                    '</tr>';
            });
            table.find("tbody").html(html);
            table.table("refresh");
        }).fail(function () {
            console.log("获取设计变更单审批记录信息失败!");
        });
    }

    //右侧菜单项查看功能项点击处理
    $("#detail_menu a[data-collapsible-id]").click(function (e) {
        e.preventDefault();
        $("#detail_menu").panel("close");
        $("#detail").find("[data-role=collapsible]").collapsible("collapse");
        $("#detail").find("#" + $(this).attr("data-collapsible-id")).collapsible("expand");
    });


    //返回
    $("#detail_back").click(function () {
        var url = $.mobile.path.parseUrl(location.href);
        window.location.href = url.filename;
    });
})

/**
 * 返回查看页面
 */
var backToDetail = function () {
    var url = $.mobile.path.parseUrl(location.href);
    window.location.href = url.hrefNoHash + "#detail";
}

/***************************
 * 查看页修改状态
 **************************/

$(document).on("pageshow", "#detail_updateState", function () {

    var form = $("#detail_updateState_form");

    //根据现在状态赋值
    var select = form.find("select");
    var state = $("#detail_basic").find("[name=state]").text();
    select.html('<option value="审核通过" ' + (state == "审核通过" ? "selected" : "") + '>审核通过</option>' +
        '<option value="未执行" ' + (state == "未执行" ? "selected" : "") + '>未执行</option>' +
        '<option value="执行中" ' + (state == "执行中" ? "selected" : "") + '>执行中</option>');
    select.selectmenu("refresh");

    //保存状态
    form.unbind('submit').submit(function (e) {
        e.preventDefault();
        $.ajax({
            url: sysUrl + woo.url.designChangeState,
            type: "POST",
            dataType: "jsonp",
            crossDomain: "true",
            data: $(this).serialize() + "&detailId=" + detailId
        }).done(function () {
            //$("#detail_changeState").dialog("close");
            backToDetail();
        }).fail(function () {
            console.log("查看页保存沟通记录失败!");
        });

        return false;
    });

    //取消
    form.find("a.cancel").unbind('click').click(function (e) {
        backToDetail();
    });
});