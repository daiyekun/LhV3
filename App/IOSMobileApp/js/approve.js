/******************
 * Woorich Dev Team
 * By: Jnoodle
 *
 * 审批页
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
 * 业务类型：0-已发起；1-待处理；2-已处理
 * @type {number}
 */
var businessType = 1; //默认显示待处理
var business = "";
var wftype = "";//审批类型

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
            wftype : wftype,
            fType: businessType,
            keyword: $("#search").val(),
            start: (_page - 1) * woo.pageItem,
            limit: woo.pageItem,
            userId: woo.getData("userId")
        }
    }).done(function (response) {
        //console.log(response);
        var totalCount = response.totalCount || response.TotalCount;
        var html = "";

        $.each(response.WFInstanceList, function (i, item) {
            html += callback(i, item);
        });
        $("#list-table").find("tbody").html(html);
        $("#list-table").table("refresh");
        var a = wftype;
        //点击查看详细
        $("#list-table tbody a").click(function (e) {
            e.preventDefault();
            $("#objectId").val($(this).attr("oid"));
            $("#workflowId").val($(this).attr("wid"));
            $("#workflowType").val($(this).attr("wtype"));
            //审批
            $("#workfappstate").val($(this).attr("appstate"));
            //console.log($("#detailId").val());
            var url = $.mobile.path.parseUrl(location.href);
            window.location.href = url.filename + (url.search ? url.search + "&" : "?")
                + "&workfappstate=" + $("#workfappstate").val() + "&workflowType=" + $("#workflowType").val() + "&objectId=" + $("#objectId").val() + "&workflowId=" + $("#workflowId").val() + "#detail";
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
    //    <th data-priority="persist">审批对象名称</th>
    //        <th data-priority="2">审批对象</th>
    //    <th data-priority="3">审批对象编号</th>
    //        <th data-priority="4">审批对象金额</th>
    //    <th data-priority="5">审批事项</th>
    //        <th data-priority="6">收到日期</th>
    //    <th data-priority="7">发起人</th>
    var html = '<tr>' +
        '<th>' + Element.Id + '</th>' +
        '<td class="title"><a href="#" appstate="' + Element.AppState+'"wid="' + Element.AppObjId + '" oid="' + Element.Id + '" wtype="' + Element.ObjType + '">' + Element.AppObjName + '</a></td>' +
        '<td>' + Element.ObjTypeDic + '</td>' +
        '<td>' + Element.AppObjNo + '</td>' +
        '<td class="tr">' + woo.formatMoney("#,##0.00", Element.AppObjAmountThod) + ' 元 </td>' +
        '<td>' + Element.MissionDic + '</td>' +
        '<td>' + (Element.StartDateTime ? Element.StartDateTime : "") + '</td>' +
        '<td>' + Element.StartUserName + '</td>' +
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

$(document).on("pagecreate", "#index", function () {

    var dataUrl = sysUrl + woo.url.APPworkflowlist;

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
    //点击按钮替换审批列表
    $('#listtype_form').find("input:radio").change(function () {
        businessType = $(this).val();
        business = $(this).val();
        getListData(1, dataUrl, getListDataCallBack);
    });

    //点击下拉框选择审批类型
    $('#selectWfType').on("change", function () {
        if ($(this).val()) {
            var selectText = $(this).find('option:selected').text();
            var index = selectText.indexOf('-');
            wftype = selectText.substring(index + 1);
            getListData(1, dataUrl, getListDataCallBack);
        }      
    })
});

/***************************
 * 查看页
 **************************/
var objectId = 0;
var workflowId = 0;
var workfappstate = 0;
$(document).on("pageshow", "#detail", function () {
    //更新标题
    changeHeaderTitle("detail", "查看审批单");
    $(this).find("[data-role=content]").html("")
        .html('<iframe frameborder="0" width="100%" id="detailWorkflowIframe" height="90%"></iframe>');
    //详细信息Id
    objectId = $("#objectId").val() || woo.getParameterByName("objectId", $.mobile.path.parseUrl(location.href).search);
    workfappstate = $("#workfappstate").val() || woo.getParameterByName("workfappstate", $.mobile.path.parseUrl(location.href).search);
    workflowId = $("#workflowId").val() || woo.getParameterByName("workflowId", $.mobile.path.parseUrl(location.href).search);
    //Index
    var workflowType = $("#workflowType").val() || woo.getParameterByName("workflowType", $.mobile.path.parseUrl(location.href).search);
    var pageUrl = "BLL/Restful/Workflow/DetailsPages/ContractDetails.aspx";
    //根据审批类型
    switch (workflowType) {
        //合同
        case "3":
            pageUrl = "MobileApp/WorkInfo/Index"; 
            break;
        //客户
        case "0":
            pageUrl = "MobileApp/WorkInfo/CustomerDetails";
            break;
        //供应商
        case "1":
            pageUrl = "MobileApp/WorkInfo/CompanyDetails";
            break;
        //其他
        case "2":
            pageUrl = "MobileApp/WorkInfo/ComQitaDetails";
            break;
        //收票
        case "4":
            pageUrl = "MobileApp/WorkInfo/ShoupiaoDetails";
            break;
        //开票
        case "5":
            pageUrl = "MobileApp/WorkInfo/KaipiaoDetails";
            break;
        //付款
        case "6":
            pageUrl = "MobileApp/WorkInfo/FukuanDetails";
            break;
        //项目
        case "7":
            pageUrl = "MobileApp/WorkInfo/PRROJDetails";
            break;
    }
    //alert(Url + pageUrl + "?workflowId=" + workflowId)
    var url = sysUrl + pageUrl;
    //var url1 = "htp://localhost:8081/" +"BLL/Restful/Workflow/DetailsPages/ContractDetails.aspx";
    $("#detailWorkflowIframe").attr("src", url + "?workflowId=" + workflowId
        ).load(function () {
            //$("#detailWorkflowIframe").co0ntents().find("a").click(function (e) {
            //    e.preventDefault();
            //    if (this.id == "FinanceAttachmentPreviewText") {
            //        window.appRootDirName = "download_woorich";
            //        window.requestFileSystem(LocalFileSystem.PERSISTENT, 0, gotFS, fail);
            //        var fileTransfer = new FileTransfer();
            //        var endName = this.href.substr(this.href.lastIndexOf("."), (this.href.length - this.href.lastIndexOf(".")));
            //        var filePath = "";
            //        switch (endName) {
            //            case ".docx":
            //                filePath = window.appRootDir.fullPath + "/docx/temp.docx";
            //                break;
            //            case ".doc":
            //                filePath = window.appRootDir.fullPath + "/doc/temp.doc";
            //                break;
            //            case ".txt":
            //                filePath = window.appRootDir.fullPath + "/txt/temp.txt";
            //                break;
            //            case ".xlsx":
            //                filePath = window.appRootDir.fullPath + "/xlsx/temp.xlsx";
            //                break;
            //            case ".xls":
            //                filePath = window.appRootDir.fullPath + "/xls/temp.xls";
            //                break;
            //            case ".jpg":
            //                filePath = window.appRootDir.fullPath + "/jpg/temp.jpg";
            //                break;
            //            case ".jpeg":
            //                filePath = window.appRootDir.fullPath + "/jpeg/temp.jpeg";
            //                break;
            //            case ".pdf":
            //                filePath = window.appRootDir.fullPath + "/pdf/temp.pdf";
            //                break;
            //            case ".gif":
            //                filePath = window.appRootDir.fullPath + "/gif/temp.gif";
            //                break;
            //            default:
            //                alert("此格式不支持预览，请下载文件!");
            //                return;
            //        }
            //        fileTransfer.download(
            //    		this.href,
            //    		filePath,
            //    		function (entry) {
            //    		    //此处调用打开文件方法 
            //    		    OpenFile(entry.fullPath);
            //    		    //window.location.href = window.appRootDir.fullPath; 
            //    		    console.log("download complete: " + entry.fullPath);
            //    		},
            //    		function (error) {
            //    		    console.log("download error source " + error.source);
            //    		    console.log("download error target " + error.target);
            //    		    console.log("upload error code" + error.code);
            //    		}
            //    	);
            //    } else {
            //        //window.open(this.href, "_system");
            //        //navigator.app.loadUrl(this.href, { openExternal:true});
                                                                  
            //        window.open(this.href, "_system");
            //    }
            //    function gotFS(fileSystem) {
            //        console.log("filesystem got");
            //        window.fileSystem = fileSystem;
            //        fileSystem.root.getDirectory(window.appRootDirName, {
            //            create: true,
            //            exclusive: false
            //        }, dirReady, fail);
            //    }
            //    function dirReady(entry) {
            //        window.appRootDir = entry;
            //        console.log("application dir is ready");
            //    }
            //    function fail() {
            //        console.log("failed to get filesystem");
            //    }
            //    function OpenFile(path) {
            //        try {
            //            var fullPath = [];
            //            fullPath[0] = path;
            //            cordova.exec(function (message) { }, null, 'OpenFilePlugin', 'haha', fullPath);
            //        } catch (e) {
            //            console.log("failed to open file");
            //        }
            //    }
            //});
        });

    //返回
    $("#detailback").click(function () {
        $("#detailWorkflowIframe").remove();
        var url = $.mobile.path.parseUrl(location.href);
        window.location.href = url.filename;
    });
    //审批
    $("#doworkflow").click(function () {
        //var inputs = document.getElementsByName("radio-choice");
        //var a = $('input[name^="radio-choice"]').map(function () { return { value: this.value, checked: this.checked } }).get();
        workfappstate = $("#workfappstate").val() || woo.getParameterByName("workfappstate", $.mobile.path.parseUrl(location.href).search);
        if (workfappstate==1) {
            $.mobile.changePage("#workflow", {
                changeHash: true
            });
        } else {
            alert("当前状态不可审批!");
        }
        });
});


/***************************
 * 填写审批意见页
 **************************/

$(document).on("pagecreate", "#workflow", function () {
    var form = $("#workflow_form");

    var search = $.mobile.path.parseUrl(location.href).search;

    form.find("[name=type]").val(0); 
    form.find("[name=InstId]").val($("#objectId").val() || woo.getParameterByName("objectId", search));
    form.find("[name=ObjId]").val($("#workflowId").val() || woo.getParameterByName("workflowId", search));
    var state = "2"; //4:一票通过;6:一票否决;2:同意;5:不同意
    var clicked = false; //审批按钮是否被点击
    var workflowType = $("#workflowType").val() || woo.getParameterByName("workflowType", $.mobile.path.parseUrl(location.href).search);
     //同意
    $("#agree").click(function () {

        if (!clicked) {
            ObjType = workflowType;
            state = "2";
            submit();
        }
    });
               

//打回
$("#disagree").click(function () {
    
        if (!clicked) {
            state = "5";
            submit();
        }
 
    });

    $("#cancel").click(function () {
                    
    var url = $.mobile.path.parseUrl(location.href);
    window.location.href = url.hrefNoHash + "#detail";
    });

    //提交审批
     
    var submit = function () {
        var suburl = sysUrl + woo.url.APPsubmitOpinion
        
       clicked = true;
        $.ajax({
            url: suburl,
            type: "POST",
            dataType: "jsonp",
            crossDomain: "true",
            data: form.serialize() + "&state=" + state + "&ObjType=" + ObjType + "&SubmitUserId=" + woo.getData("userId")
        }).done(function () {
                
            clicked = false;
            alert("审批成功!", function () {
                var url = $.mobile.path.parseUrl(location.href);
                window.location.href = url.filename;
            });
        }).fail(function () {
            clicked = false;
            alert("提交审批失败!");
            console.log("提交审批失败!");
        });
    };
               

}
);
