/******************
 * Woorich Dev Team
 * By: Jnoodle
 *
 * 合同页（收款合同、付款合同）
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
var businessTypeDisplayName = (businessType == 0 ? "收款合同" : "付款合同"); //TODO:根据业务需要修改

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
        }
    }).done(function (response) {
            //console.log(response);
            var totalCount = response.totalCount;
            var html = "";

            $.each(response.items, function (i, item) {
                html += callback(i, item);
            });
            $("#list-table").find("tbody").html(html);
            $("#list-table").table("refresh");
               
            //点击查看详细
        $("#list-table tbody a").click(function (e) {
                e.preventDefault();
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
    //TODO:根据业务需要修改
    var html = '<tr>' +
        '<th>' + Element.Id + '</th>' +
        '<td class="title"><a href="#" id="' + Element.Id + '">' + Element.Name + '</a></td>' +
        '<td>' + Element.Code + '</td>' +
        '<td>' + Element.CompName + '</td>' +
        '<td class="tr">' + woo.formatMoney("#,##0.00", Element.ContAmThod) + ' 元 </td>' +
        '<td>' + Element.ContTypeName + '</td>' +
        '<td>' + Element.ContStateDic + '</td>' +
        '</tr>';
    return html;
};

/**
 * 列表页面是否已刷新：只刷新一次，不必每次载入页面都刷新
 * @type {boolean}
 */
var isListRefreshed = false;

/**
 * 初始化select控件中的选项值
 * @param dataUrl 后台地址
 * @param callback function(response) 执行成功后处理函数
 */
var initSelectItems = function (dataUrl, callback) {
    $.ajax({
        url: dataUrl,
        type: "POST",
        dataType: "jsonp",
        crossDomain: "true",
        data: {
            type: businessType
        }
    }).done(callback)
        .fail(function () {
            console.log("初始化select控件选项值失败");
        });
};

/**
 * 获取WOO_CATEGORY表中所有类别信息，用于给字典字段赋值
 * @param doneCallBack function(response) 获取成功后的动作
 */
var getCategories = function (doneCallBack) {
    $.ajax({
        url: sysUrl + woo.url.category,
        type: "POST",
        dataType: "jsonp",
        crossDomain: "true",
        async: false
    }).done(doneCallBack)
        .fail(function () {
            console.log("获取全部类别信息失败!");
        });
}

/**
 * 隐藏合同对方选择页面，在合同对方选择页的iframe中调用
 */
var hideCompanySelect = function () {
    $.mobile.changePage("#add");
};


/***********************************************************************************/


/***************************
 * 激活PhoneGap
 **************************/

document.addEventListener("deviceready", function () {
    console.log("onDeviceReady");
}, false);


/***************************
 * 列表页（默认）
 **************************/

$(document).on("pageinit", "#index", function () {
    //var dataUrl = sysUrl + woo.url.contractList;applogincontractList
    var dataUrl = sysUrl + woo.url.appContList; 
    //更新标题
    changeHeaderTitle("index", businessTypeDisplayName);

    //初始化分页控件
    var wooMaxPage = $('#maxPage').val();
    if (wooMaxPage == 0)
        wooMaxPage = 1;
    $('.pagination').jqPagination({
        max_page: wooMaxPage,
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
        //判断权限
        $.ajax({
            url: sysUrl + woo.url.permission,
            type: "POST",
            dataType: "jsonp",
            crossDomain: "true",
            data: {
                permissionname: "合同建立"
            }
        }).done(function (response) {
                if (!response.success) {
                    alert("没有权限！");
                } else {
                    $.mobile.changePage("#add");
                    $("#addform")[0].reset(); //重置表单，防止上一次保存的数据滞留
                }
            })
            .fail(function () {
                console.log("获取权限信息失败!");
            });
    });

    //返回业务主页面
    $("#backtoindex").click(function (e) {
        e.preventDefault();
        window.location.href = "index.html#index";
    });
});

/***************************
 * 选择合同对方
 **************************/

$(document).on("pageshow", "#selectCompanyPage", function () {
    $("#selectCompanyIframe").attr("src", "company.html?select=true&type=" + businessType);
});


/***************************
 * 新建/修改页
 **************************/

$(document).on("pageinit", "#add", function () {

    var form = $("#addform");

    //更新标题
    changeHeaderTitle("add", "新建" + businessTypeDisplayName);

    //初始化合同类别选项
    initSelectItems(sysUrl + woo.url.contractSelectType, function (response) {
        var options = "";
        $.each(response, function (i, item) {
            options += '<option value="' + item.ID + '">' + item.NAME + '</option>';
        });
        form.find("#category").empty().append(options).selectmenu("refresh");
    });

    //初始化签约主体选项
    initSelectItems(sysUrl + woo.url.contractSelectMainDept, function (response) {
        var options = "";
        $.each(response, function (i, item) {
            options += '<option value="' + item.ID + '">(' + item.NO + ')' + item.NAME + '</option>';
        });
        form.find("#maindept").empty().append(options).selectmenu("refresh");
    });

    //初始化经办机构选项
    initSelectItems(sysUrl + woo.url.contractSelectDept, function (response) {
        var options = "";
        $.each(response, function (i, item) {
            options += '<option value="' + item.ID + '">(' + item.NO + ')' + item.NAME + '</option>';
        });
        form.find("#dept").empty().append(options).selectmenu("refresh");
    });

    //初始化合同文本类别选项
    var fileTypeItems1 = "";
    initSelectItems(sysUrl + woo.url.contractSelectFile1, function (response) {
        $.each(response, function (i, item) {
            fileTypeItems1 += '<option value="' + item.ID + '">' + item.NAME + '</option>';
        });
    });

    //初始化合同附件类别选项
    var fileTypeItems2 = "";
    initSelectItems(sysUrl + woo.url.contractSelectFile2, function (response) {
        $.each(response, function (i, item) {
            fileTypeItems2 += '<option value="' + item.ID + '">' + item.NAME + '</option>';
        });
    });

    //选择合同对方
    $("#selectCompany").click(function (e) {
        e.preventDefault();
        $.mobile.changePage("#selectCompanyPage", {
            reloadPage: false
        });
    });

    /**
     * 是否是自动增加
     * @type {boolean}
     */
    var isAutoFinance = false;

    /**
     * 增加资金计划
     * @param id 在什么元素后增加
     * @returns {number} count 资金计划表单索引
     */
    var addFinance = function (id) {

        isAutoFinance = false;

        var collapsibles = $("#collapsible_finance").find("[data-role=collapsible]");
        var count = collapsibles.length + 1;

        for (var k = 0; k < collapsibles.length; k++) {
            if (collapsibles[k].attr("data-count") == (count + "")) {
                count++;
            }
        }

        var html = '<div data-role="collapsible" data-collapsed="false" data-count="' + count + '">' +
            '<h4>计划资金' + count + '</h4>' +
            '<a href="#" class="ui-btn ui-icon-delete ui-btn-icon-left ui-corner-all delete">删除</a>' +
            '<div class="ui-field-contain">' +
            '<label for="finance_name_' + count + '">名称:</label>' +
            '<input type="text" name="finance_name_' + count + '" id="finance_name_' + count + '" placeholder="名称" required/>' +
            '</div><div class="ui-field-contain">' +
            '<label for="finance_date_' + count + '">计划完成日期:</label>' +
            '<input type="text" name="finance_date_' + count + '" id="finance_date_' + count + '" placeholder="计划完成日期" class="dt" required/>' +
            '</div><div class="ui-field-contain">' +
            '<label for="finance_amount_' + count + '">金额:</label>' +
            '<input type="number" name="finance_amount_' + count + '" id="finance_amount_' + count + '" placeholder="金额" required/>' +
            '</div><div class="ui-field-contain">' +
            '<label for="finance_remark_' + count + '">备注:</label>' +
            '<input type="text" name="finance_remark_' + count + '" id="finance_remark_' + count + '" placeholder="备注"/>' +
            '</div></div>';

        $("#" + id).after(html);
        var collapsibles = $("#collapsible_finance").find("[data-role=collapsible]");
        collapsibles.collapsible(); //初始化collapsible样式
        collapsibles.find("input").textinput(); //初始化input样式

        woo.setDatetimeInput();

        //collapsible点击按钮默认是删除自己
        try {
            collapsibles.find("a.delete").click(function (e) {
                e.preventDefault();
                $($(this).parents("[data-role=collapsible]")[0]).remove();
            });
        } catch (e) {
        }

        return count;
    }

    /**
     * 自动增加资金计划
     */
    var autoAddFinance = function () {

        $("#addfinance_auto_form")[0].reset();

        //确认
        $("#addfinance_auto_form").unbind('submit').submit(function () {

            //对计划资金格式的验证
            $.validator.addMethod("autoFinance", function (value, element, param) {
                if (this.optional(element) || value.match(param)) {
                    var array = value.split(":");
                    var num = 0;
                    for (var i = 0; i < array.length; i++) {
                        num += parseInt(array[i]);
                    }
                    return num == 10;
                } else {
                    return false;
                }
            }, "格式不对或总和不正确");

            var validator = $("#addfinance_auto_form").validate({
                rules: {
                    auto: {
                        required: true,
                        autoFinance: /^[1-9](([:][1-9]){1,})$/
                    }
                }
            });

            if (validator.form()) {
                //清除所有现有的计划资金
                $("#collapsible_finance").find("[data-role=collapsible]").remove();

                if (!$("#addform").find("#amount").val()) {
                    $.mobile.changePage("#add");
                    return false;
                }

                var contractAmount = parseFloat($("#addform").find("#amount").val()); //合同金额

                console.log(contractAmount);

                var count = 1;
                var html = "";
                var allMoney = 0.00;

                var array = $(this).find("#auto").val().split(":");
                for (var i = 0; i < array.length; i++) {

                    var money = 0.00;
                    if (i == (array.length - 1)) {
                        //如果是最后一条，为了防止金额不对，变成合同总额减去之前的条目的金额
                        money = contractAmount - allMoney;
                    } else {
                        money = contractAmount * array[i] / 10;
                    }
                    allMoney += money;
                    console.log(money);
                    console.log(allMoney);

                    var m = woo.formatMoney("0.0000", money);

                    html += '<div data-role="collapsible" data-collapsed="false">' +
                        '<h4>计划资金' + count + '</h4>' +
                        '<div class="ui-field-contain">' +
                        '<label for="finance_name_' + count + '">名称:</label>' +
                        '<input type="text" name="finance_name_' + count + '" id="finance_name_' + count + '" value="第' + count + '笔" required/>' +
                        '</div><div class="ui-field-contain">' +
                        '<label for="finance_date_' + count + '">计划完成日期:</label>' +
                        '<input type="text" name="finance_date_' + count + '" id="finance_date_' + count + '" placeholder="计划完成日期" class="dt" required/>' +
                        '</div><div class="ui-field-contain">' +
                        '<label for="finance_amount_' + count + '">金额:</label>' +
                        '<input type="number" name="finance_amount_' + count + '" id="finance_amount_' + count + '" value="' + m + '" required readonly/>' +
                        '</div><div class="ui-field-contain">' +
                        '<label for="finance_remark_' + count + '">备注:</label>' +
                        '<input type="text" name="finance_remark_' + count + '" id="finance_remark_' + count + '" placeholder="备注"/>' +
                        '</div></div>';

                    count++;
                }

                $("#addFinance").after(html);
                var collapsibles = $("#collapsible_finance").find("[data-role=collapsible]");
                collapsibles.collapsible(); //初始化collapsible样式
                collapsibles.find("input").textinput(); //初始化input样式

                woo.setDatetimeInput();
                isAutoFinance = true;

                $.mobile.changePage("#add");
                return false;
            } else {
                return false;
            }

        });

        //取消
        $("#addfinance_auto_form .cancel").on("click.canc", function () {
            $.mobile.changePage("#add");
        });

        $.mobile.changePage("#addfinance_auto");
    }

    //增加计划资金
    $("#addFinance").click(function (e) {
        e.preventDefault();

        if (isAutoFinance && $("#collapsible_finance").find("[data-role=collapsible]").length > 0) {
            //确认
            $("#addfinance_confirm .confirm").on("click.conf", function () {

                //清除所有现有的计划资金
                $("#collapsible_finance").find("[data-role=collapsible]").remove();

                addFinance("addFinance");
                $(this).off("click.conf");
                $.mobile.changePage("#add");
            });

            //取消
            $("#addfinance_confirm .cancel").on("click.canc", function () {
                $(this).off("click.canc");
                $.mobile.changePage("#add");
            });

            $.mobile.changePage("#addfinance_confirm", {
                role: "dialog"
            });
        } else {
            addFinance("addFinance");
        }
    });

    //批量增加计划资金
    $("#generateFinance").click(function (e) {
        e.preventDefault();
        if (isAutoFinance && $("#collapsible_finance").find("[data-role=collapsible]").length == 0) {
            autoAddFinance();
        } else {
            if ($("#collapsible_finance").find("[data-role=collapsible]").length > 0) {
                //确认
                $("#addfinance_confirm .confirm").on("click.conf", function () {
                    autoAddFinance();
                });

                //取消
                $("#addfinance_confirm .cancel").on("click.canc", function () {
                    $.mobile.changePage("#add");
                });

                $.mobile.changePage("#addfinance_confirm", {
                    role: "dialog"
                });
            } else {
                autoAddFinance();
            }
        }
    });


    //本地上传合同文本
    $("#selectFile1").click(function (e) {
        e.preventDefault();
        var count = $("#collapsible_file1").find("[data-role=collapsible]").length + 1;

        var html = '<div data-role="collapsible" data-collapsed="false">' +
            '<h4>合同文本' + count + '</h4>' +
            '<a href="#" class="ui-btn ui-icon-delete ui-btn-icon-left ui-corner-all delete">删除</a>' +
            '<div class="ui-field-contain">' +
            '<a href="#" class="ui-btn ui-corner-all" id="choose1_upload_' + count + '">选择文件</a>' +
            '<input type="file" name="file1_upload_' + count + '" id="file1_upload_' + count + '" data-role="none"/>' +
            //'<label for="file1_name_' + count + '">文件名:</label>' +
            '<input type="hidden" name="file1_name_' + count + '" id="file1_name_' + count + '" required/>' +
            '<input type="hidden" name="file1_pre_' + count + '" id="file1_pre_' + count + '"/>' +
            '</div><div class="ui-field-contain">' +
            '<label for="file1_displayname_' + count + '">名称:</label>' +
            '<input type="text" name="file1_displayname_' + count + '" id="file1_displayname_' + count + '" placeholder="名称"/>' +
            '</div><div class="ui-field-contain">' +
            '<label for="file1_type_' + count + '">类别:</label>' +
            '<select name="file1_type_' + count + '" id="file1_type_' + count + '" placeholder="类别"></select>' +
            '</div><div class="ui-field-contain">' +
            '<label for="file1_remark_' + count + '">说明:</label>' +
            '<input type="text" name="file1_remark_' + count + '" id="file1_remark_' + count + '" placeholder="说明"/>' +
            '</div></div>';

        $(this).after(html);
        var collapsibles = $("#collapsible_file1").find("[data-role=collapsible]");
        collapsibles.collapsible(); //初始化collapsible样式
        collapsibles.find("input:not(:file,:hidden)").textinput(); //初始化input样式
        collapsibles.find("select").empty().append(fileTypeItems1).selectmenu(); //初始化select样式

        //设置文件名随机前缀种子值，防止上传文件重名覆盖
        var pre = "MAT" + Math.floor(Math.random() * 100000) + "-";
        $("#file1_pre_" + count).val(pre);

        //collapsible点击按钮默认是删除自己
        collapsibles.find("a.delete").click(function (e) {
            e.preventDefault();
            $($(this).parents("[data-role=collapsible]")[0]).remove();
        });

        //uploadify不支持ios系统，所以需要选择更好地ajax upload组件 blueimp / jQuery-File-Upload
        if (device.platform == 'android' || device.platform == 'Android') {
            $("#choose1_upload_" + count).css("display", "none");
            $('#file1_upload_' + count).fileupload({
                url: sysUrl + woo.url.contractUploadFile + "&type=1&pre=" + pre,
                forceIframeTransport: true,
                done: function (e, data) {
                    try {
                        //做判断防止android浏览器出错
                        if (data && data.files && data.files[0].name) {
                            $('#file1_name_' + count).val(data.files[0].name + "");
                            $('#file1_displayname_' + count).val(data.files[0].name + "");
                        }
                    } catch (e) {
                    }

                    return false;
                },
                fail: function (e, data) {
                    $('#uploadfailure').popup("open");
                    return false;
                }
            });
        } else {
            //IOS
            $("#file1_upload_" + count).css("display", "none");
            $('#choose1_upload_' + count).click(function () {
                event.preventDefault();

                function uploadPhoto(imageURI) {
                    var options = new FileUploadOptions();
                    options.fileKey = "file";
                    options.fileName = imageURI.substr(imageURI.lastIndexOf('/') + 1);
                    options.mimeType = "image/jpeg";
                    options.chunkedMode = false;

                    var ft = new FileTransfer();
                    ft.upload(imageURI, sysUrl + woo.url.contractUploadFile + "&type=1&pre=" + pre,
                        function (r) {
                            console.log("Code = " + r.responseCode);
                            console.log("Response = " + r.response);
                            console.log("Sent = " + r.bytesSent);
                            try {
                                $('#file1_name_' + count).val(r.response + "");
                                $('#file1_displayname_' + count).val(r.response + "");
                            } catch (e) {
                            }
                        }, function (error) {
                            console.log("An error has occurred: Code = " + error.code);
                        }, options);
                }


                navigator.camera.getPicture(uploadPhoto, function (message) {
                        console.log('获取文件失败！');
                    }, {
                        quality: 80,
                        destinationType: navigator.camera.DestinationType.FILE_URI,
                        sourceType: navigator.camera.PictureSourceType.PHOTOLIBRARY
                    }
                );
            });
        }
    });


    //本地上传合同附件
    $("#selectFile2").click(function (e) {
        e.preventDefault();
        var count = $("#collapsible_file2").find("[data-role=collapsible]").length + 1;

        var html = '<div data-role="collapsible" data-collapsed="false">' +
            '<h4>合同附件' + count + '</h4>' +
            '<a href="#" class="ui-btn ui-icon-delete ui-btn-icon-left ui-corner-all delete">删除</a>' +
            '<div class="ui-field-contain">' +
            '<a href="#" class="ui-btn ui-corner-all" id="choose2_upload_' + count + '">选择文件</a>' +
            '<input type="file" name="file2_upload_' + count + '" id="file2_upload_' + count + '" data-role="none"/>' +
            //'<label for="file1_name_' + count + '">文件名:</label>' +
            '<input type="hidden" name="file2_name_' + count + '" id="file2_name_' + count + '" required/>' +
            '<input type="hidden" name="file2_pre_' + count + '" id="file2_pre_' + count + '"/>' +
            '</div><div class="ui-field-contain">' +
            '<label for="file2_displayname_' + count + '">名称:</label>' +
            '<input type="text" name="file2_displayname_' + count + '" id="file2_displayname_' + count + '" placeholder="名称"/>' +
            '</div><div class="ui-field-contain">' +
            '<label for="file2_type_' + count + '">类别:</label>' +
            '<select name="file2_type_' + count + '" id="file2_type_' + count + '" placeholder="类别"></select>' +
            '</div><div class="ui-field-contain">' +
            '<label for="file2_remark_' + count + '">说明:</label>' +
            '<input type="text" name="file2_remark_' + count + '" id="file2_remark_' + count + '" placeholder="说明"/>' +
            '</div></div>';

        $(this).after(html);
        var collapsibles = $("#collapsible_file2").find("[data-role=collapsible]");
        collapsibles.collapsible(); //初始化collapsible样式
        collapsibles.find("input:not(:file,:hidden)").textinput(); //初始化input样式
        collapsibles.find("select").empty().append(fileTypeItems2).selectmenu(); //初始化select样式

        //设置文件名随机前缀种子值，防止上传文件重名覆盖
        var pre = "MAT" + Math.floor(Math.random() * 100000) + "-";
        $("#file2_pre_" + count).val(pre);

        //collapsible点击按钮默认是删除自己
        collapsibles.find("a.delete").click(function (e) {
            e.preventDefault();
            $($(this).parents("[data-role=collapsible]")[0]).remove();
        });

        //uploadify不支持ios系统，所以需要选择更好地ajax upload组件 blueimp / jQuery-File-Upload
        if (device.platform == 'android' || device.platform == 'Android') {
            $("#choose2_upload_" + count).css("display", "none");
            $('#file2_upload_' + count).fileupload({
                url: sysUrl + woo.url.contractUploadFile + "&type=2&pre=" + pre,
                forceIframeTransport: true,
                done: function (e, data) {
                    try {
                        //做判断防止android浏览器出错
                        if (data && data.files && data.files[0].name) {
                            $('#file2_name_' + count).val(data.files[0].name + "");
                            $('#file2_displayname_' + count).val(data.files[0].name + "");
                        }
                    } catch (e) {
                    }

                    return false;
                },
                fail: function (e, data) {
                    $('#uploadfailure').popup("open");
                    return false;
                }
            });
        } else {
            //IOS
            $("#file2_upload_" + count).css("display", "none");
            $('#choose2_upload_' + count).click(function () {
                event.preventDefault();

                function uploadPhoto(imageURI) {
                    var options = new FileUploadOptions();
                    options.fileKey = "file";
                    options.fileName = imageURI.substr(imageURI.lastIndexOf('/') + 1);
                    options.mimeType = "image/jpeg";
                    options.chunkedMode = false;

                    var ft = new FileTransfer();
                    ft.upload(imageURI, sysUrl + woo.url.contractUploadFile + "&type=2&pre=" + pre,
                        function (r) {
                            console.log("Code = " + r.responseCode);
                            console.log("Response = " + r.response);
                            console.log("Sent = " + r.bytesSent);
                            try {
                                $('#file2_name_' + count).val(r.response + "");
                                $('#file2_displayname_' + count).val(r.response + "");
                            } catch (e) {
                            }
                        }, function (error) {
                            console.log("An error has occurred: Code = " + error.code);
                        }, options);
                }


                navigator.camera.getPicture(uploadPhoto, function (message) {
                        console.log('获取文件失败！');
                    }, {
                        quality: 80,
                        destinationType: navigator.camera.DestinationType.FILE_URI,
                        sourceType: navigator.camera.PictureSourceType.PHOTOLIBRARY
                    }
                );
            });
        }
    });

    //提交表单
    $("#addform_submit").unbind('click').click(function (e) {
        e.preventDefault();

        var validator = $("#addform").validate();  //表单验证

        if (validator.form()) {
            //console.log($("#addform").serialize());

            //计划资金总和与合同金额判断

            var contractAmount = parseFloat($("#amount").val());

            var plansAmount = 0;

            var plans = $("#collapsible_finance").find("[name^=finance_amount_]");

            for (var i = 0; i < plans.length; i++) {
                plansAmount += parseFloat($(plans[i]).val());
            }

            if (contractAmount != plansAmount) {
                $("#notequal").popup("open");
                return false;
            } else {


                $("#submiting").popup("open");

                $.ajax({
                    url: sysUrl + woo.url.contractAdd + "&type=" + businessType,
                    type: "POST",
                    dataType: "jsonp",
                    crossDomain: "true",
                    data: $("#addform").serialize()
                }).done(function (response) {
                        if (response.success) {
                            console.log("addform_submit成功!");

                            $("#submiting").popup("close");
                            window.setTimeout(function () {
                                $.mobile.changePage("#index", {
                                    changeHash: true
                                });
                                
                                getListData(1, sysUrl + woo.url.contractList, getListDataCallBack);//重新刷新数据
                            }, 500);

                        } else {
                            console.log("addform_submit失败!");
                            $("#submiting").popup("close");
                            //解决一个popup关闭后另一个popup无法打开问题，https://github.com/jquery/jquery-mobile/issues/4968
                            window.setTimeout(function () {
                                $('#submiterror').popup('open');
                            }, 1000);
                        }

                    }).fail(function () {
                        console.log("合同新建地址访问失败!");
                        $("#submiting").popup("close");
                        window.setTimeout(function () {
                            $('#submiterror').popup('open');
                        }, 1000);
                    });
            }

            return false;
        }
    });

    //取消保存,返回首页
    $("#addform_cancel").click(function (e) {
        e.preventDefault();
        $.mobile.changePage("#index", {
            changeHash: true
        });
    });

});


/***************************
 * 查看页
 **************************/

var detailId = 0;
var historyId = 0;
var categories;
$(document).on("pageshow", "#detail", function () {

    //更新标题
    changeHeaderTitle("detail", "查看" + businessTypeDisplayName);

    //详细信息Id
    detailId = $("#detailId").val() || woo.getParameterByName("id", $.mobile.path.parseUrl(location.href).search);

    console.log(detailId);

    if (detailId > 0) {

        //获取全部类别信息
        //getCategories(function (response) {
            categories = null;
            //判断权限                
                    //获取基本信息
                    $.ajax({
                        url: sysUrl + woo.url.APPcontractDetail,
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
                        d.find('[name="category"]').html(r.ContTypeName);//合同类别
                        d.find('[name="name"]').html(r.Name);//合同名称
                        d.find('[name="no"]').html(r.Code);//合同编号
                        d.find('[name="amount"]').html(woo.formatMoney("#,##0.00", r.ContAmThod) + ' 元');//合同金额
                        d.find('[name="company"]').html(r.CompName);//合同对方
                        d.find('[name="maindept"]').html(r.MdeptName);//签约主体
                        d.find('[name="dept"]').html(r.DeptName);//经办机构 
                        d.find('[name="state"]').html(r.StateDic);//状态
                        //d.find('[name="wfstate"]').html(r.wfstate);
                        d.find('[name="buildUser"]').html(r.CreateUserName);//建立人
                        d.find('[name="principalUser"]').html(r.PrincipalUserName);//负责人

                        historyId = parseInt(r.historyId);

                        if (r.wfstate && r.wfstate.length > 1) {
                            //在审批中，隐藏提交审批
                            $("#detail_menu").find("#detail_menu_commitWorkflow").hide();
                        }

                        //判断修改状态权限
                        $.ajax({
                            url: sysUrl + woo.url.permission,
                            type: "POST",
                            dataType: "jsonp",
                            crossDomain: "true",
                            data: {
                                permissionname: "合同状态修改",
                                id: detailId
                            }
                        }).done(function (response) {
                            if (!response.success) {
                                $("#detail_menu").find("#detail_menu_commitWorkflow").hide();
                            }
                        })
                            .fail(function () {
                                console.log("获取权限信息失败!");
                            });

                    }).fail(function () {
                        console.log("获取合同基本信息失败!");
                    });

                    //获取合同文本信息
                    $.ajax({
                        url: sysUrl + woo.url.appcontractDetailFile1,
                        type: "POST",
                        dataType: "jsonp",
                        crossDomain: "true",
                        data: {
                            detailId: detailId
                        }
                    }).done(function (response) {
                      
                        var table = $("#detail_files1_table");
                        var html = "";
                        $.each(response, function (i, d) {
                         
                            html += '<tr>' +
                                '<th>' + d.Id + '</th>' +
                                '<td class="title"><a href="' + sysUrl + d.Path+ '" target="_blank" onclick="window.open(this.href,\'_system\'); return false;">' + d.Name + '</a></td>' +
                                '<td>' + d.CategoryName + '</td>' +
                                '<td>' + d.Remark + '</td>' +
                                '</tr>';
                        });
                        table.find("tbody").html(html);
                        table.table("refresh");
                    }).fail(function () {
                        console.log("获取合同文本信息失败!");
                    });

                    //获取合同附件信息
                    $.ajax({
                        url: sysUrl + woo.url.appcontractDetailFile2,
                        type: "POST",
                        dataType: "jsonp",
                        crossDomain: "true",
                        data: {
                            detailId: detailId
                        }
                    }).done(function (response) {
                        //console.log(response);
                        var table = $("#detail_files2_table");
                        var html = "";
                         $.each(response, function (i, d) {
                            //alert(sysUrl + d.Path)
                            html += '<tr>' +
                                '<th>' + d.Id + '</th>' +
                                '<td class="title"><a href="' + sysUrl + d.Path + '" target="_blank" onclick="window.open(this.href,\'_system\'); return false;">' + d.Name + '</a ></td>' +
                                //'<td class="title"><a href="' + sysUrl + d.Path + '" target="_blank">' + d.Name + '</a ></td>' +
                                '<td>' + d.CategoryName + '</td>' +
                                '<td>' + d.Remark + '</td>' +
                                '</tr>';
                        });
                        table.find("tbody").html(html);
                        table.table("refresh");
                    }).fail(function () {
                        console.log("获取合同附件信息失败!");
                    });


                    //获取合同计划资金信息
                    $.ajax({
                        url: sysUrl + woo.url.appcontractDetailAFinance,
                        type: "POST",
                        dataType: "jsonp",
                        crossDomain: "true",
                        data: {
                            detailId: detailId
                        }
                    }).done(function (response) {
                        //console.log(response);
                        var table = $("#detail_finance_table");
                        var html = "";
                        $.each(response, function (i, d) {
                            html += '<tr>' +
                                '<th>' + d.Id + '</th>' +
                                '<td class="title">' + d.Name + '</td>' +
                                '<td>' + woo.formatMoney("#,##0.00", d.AmountMoney) + ' 元' + '</td>' +
                                '<td>' + d.PlanCompleteDateTime + '</td>' +
                                '<td>' + d.Remark + '</td>' +
                                '</tr>';
                        });
                        table.find("tbody").html(html);
                        table.table("refresh");
                    }).fail(function () {
                        console.log("获取合同计划资金信息失败!");
                    });


                    //获取合同实际资金信息
                    $.ajax({
                        url: sysUrl + woo.url.appfinanceActualList,
                        type: "POST",
                        dataType: "jsonp",
                        crossDomain: "true",
                        data: {
                            detailId: detailId
                        }
                    }).done(function (response) {
                        //console.log(response);
                        var table = $("#detail_afinance_table");
                        var html = "";
                        $.each(response, function (i, d) {
                            html += '<tr>' +
                                '<th>' + d.Id + '</th>' +
                                '<td class="title">' + woo.formatMoney("#,##0.00", d.AmountMoney) + ' 元' + '</td>' +
                                '<td>' + d.ActualSettlementDate + '</td>' +
                                '<td>' + d.SettlementMethod + '</td>' +
                                '<td>' + d.ConfirmUserId + '</td>' +
                                '</tr>';
                        });
                        table.find("tbody").html(html);
                        table.table("refresh");
                    }).fail(function () {
                        console.log("获取合同实际资金信息失败!");
                    });

                    //获取合同资金统计信息
                    $.ajax({
                        url: sysUrl + woo.url.appcontractDetailFinanceStat,
                        type: "POST",
                        dataType: "jsonp",
                        crossDomain: "true",
                        data: {
                            detailId: detailId
                        }
        }).done(function (r) {
            //console.log(r);
            var html = "";
            html += "<li>" + 
                ("实际收款:" + "<span>" + r.ActMoneryThod + " 元 </span>")
                     + " </li>"
                + "<li>" + 
                ("未收款:" + "<span>" + r.ActNoMoneryThod + " 元 </span>")
                + " </li>"
                + "<li>" + 
                ("开发票金额:" + "<span>" + r.InvoiceMoneryThod + " 元 </span>")
                   + " </li>"
                + "<li>" + 
                ("未开发票金额:" + "<span>" + r.InvoiceNoMoneryThod + " 元 </span>")
                    + " </li>"
                + "<li>" + 
                ("财务应收:" + "<span>" + r.ReceivableThod + " 元 </span>")
                     + " </li>"
                + "<li>" + 
                ("财务预收:" + "<span>" + r.AdvanceThod + " 元 </span>")
                    + " </li>";

            $("#detail_financestat_listview").html(html).listview("refresh");
        }).fail(function () {
            console.log("获取合同资金统计信息失败!");
        });


                    //获取合同备忘信息
                    $.ajax({
                        url: sysUrl + woo.url.appcontractDetailRemark,
                        type: "POST",
                        dataType: "jsonp",
                        crossDomain: "true",
                        data: {
                            detailId: detailId
                        }
                    }).done(function (r) {
                        //console.log(r);
                        var html = "";
                        $.each(r, function (i, d) {
                            html += "<li>时间:<span>" + d.CreateDateTime + "</span><br>事项:<span>" + d.Citem + "</span><p>" + d.Ccontent + "</p></li>";
                        });

                        $("#detail_remark_listview").html(html).listview("refresh");
                    }).fail(function () {
                        console.log("获取合同备忘信息失败!");
                    });

                    //获取合同审批记录信息
                    $.ajax({
                        url: sysUrl + woo.url.appcontractDetailWorkflow,
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
                        $.each(response, function (i, d) {
                            html += '<tr>' +
                                '<th>' + d.Id + '</th>' +
                                '<td class="title">' + d.MissionDic + '</td>' +
                                '<td>' + d.CreateDateTime + '</td>' +
                                '<td>' + d.CompleteDateTime + '</td>' +
                                '<td>' + d.CurrentNodeName + '</td>' +
                                '<td>' + d.AppStateDic + '</td>' +
                                '</tr>';
                        });
                        table.find("tbody").html(html);
                        table.table("refresh");
                    }).fail(function () {
                        console.log("获取合同审批记录信息失败!");
                    });

       // });
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
        var type = woo.getParameterByName("type", url.search);
        window.location.href = url.filename + "?type=" + type;
    });

});

/**
 * 返回查看页面
 */
var backToDetail = function () {
    var url = $.mobile.path.parseUrl(location.href);
    window.location.href = url.hrefNoHash + "#detail";
}


/***************************
 * 查看页保存合同正文
 **************************/

$(document).on("pageshow", "#detail_addFile1", function () {

    var form = $("#detail_addFile1_form");
    form[0].reset(); //重置表单

    //初始化附件类别选项
    initSelectItems(sysUrl + woo.url.contractSelectFile1, function (response) {
        var options = "";
        $.each(response, function (i, item) {
            options += '<option value="' + item.ID + '">' + item.NAME + '</option>';
        });
        form.find("select").empty().append(options);
        form.find("select").selectmenu("refresh");
    });

    /**
     * 文件上传到服务器增加随机前缀
     * @type {string}
     */
    var pre = "MAT" + Math.floor(Math.random() * 100000) + "-";
    form.find('[name=file_pre]').val(pre);

    //初始化上传控件
    if (device.platform == 'android' || device.platform == 'Android') {
        form.find("#choose1_upload").css("display", "none");
        form.find('[name=file_upload]').fileupload({
            url: sysUrl + woo.url.contractUploadFile + "&type=1&pre=" + pre,
            forceIframeTransport: true,
            done: function (e, data) {
                try {
                    //做判断防止android浏览器出错
                    if (data && data.files && data.files[0].name) {
                        form.find('[name=file_name]').val(data.files[0].name + "");
                        form.find('[name=file_displayname]').val(data.files[0].name + "");
                    }
                } catch (e) {
                }

                return false;
            },
            fail: function (e, data) {
                $('#uploadfailure').popup("open");
                return false;
            }
        });
    } else {
        //IOS
        form.find('[name=file_upload]').css("display", "none");
        form.find('#choose1_upload').click(function () {
            event.preventDefault();

            function uploadPhoto(imageURI) {
                var options = new FileUploadOptions();
                options.fileKey = "file";
                options.fileName = imageURI.substr(imageURI.lastIndexOf('/') + 1);
                options.mimeType = "image/jpeg";
                options.chunkedMode = false;

                var ft = new FileTransfer();
                ft.upload(imageURI, sysUrl + woo.url.contractUploadFile + "&type=1&pre=" + pre,
                    function (r) {
                        console.log("Code = " + r.responseCode);
                        console.log("Response = " + r.response);
                        console.log("Sent = " + r.bytesSent);
                        try {
                            form.find('[name=file_name]').val(r.response + "");
                            form.find('[name=file_displayname]').val(r.response + "");
                        } catch (e) {
                        }
                    }, function (error) {
                        console.log("An error has occurred: Code = " + error.code);
                    }, options);
            }


            navigator.camera.getPicture(uploadPhoto, function (message) {
                    console.log('获取文件失败！');
                }, {
                    quality: 80,
                    destinationType: navigator.camera.DestinationType.FILE_URI,
                    sourceType: navigator.camera.PictureSourceType.PHOTOLIBRARY
                }
            );
        });
    }

    //保存附件
    form.unbind('submit').submit(function () {
        $.ajax({
            url: sysUrl + woo.url.contractAddFile1,
            type: "POST",
            dataType: "jsonp",
            crossDomain: "true",
            data: $(this).serialize() + "&detailId=" + detailId + "&type=" + businessType
        }).done(function () {
                //$("#detail_addFile").dialog("close");
                backToDetail();
            }).fail(function () {
                console.log("保存合同正文失败!");
            });

        return false;
    });

    //取消
    form.find("a.cancel").unbind('click').click(function (e) {
        backToDetail();
    });
});


/***************************
 * 查看页快速起草
 **************************/
var first_open_detail_addFile1_draft = true; //是否是第一次打开快速起草页面
$(document).on("pageshow", "#detail_addFile1_draft", function () {

    detailId = $("#detailId").val() || woo.getParameterByName("id", $.mobile.path.parseUrl(location.href).search);

    var form = $("#detail_addFile1_draft_form");

    if (first_open_detail_addFile1_draft) {
        form[0].reset();

        //初始化附件类别选项
        initSelectItems(sysUrl + woo.url.contractSelectFile1, function (response) {
            var options = "";
            $.each(response, function (i, item) {
                options += '<option value="' + item.ID + '">' + item.NAME + '</option>';
            });
            form.find("#file_type").empty().append(options).selectmenu("refresh");
        });

        //初始化模板
        initSelectItems(sysUrl + woo.url.contractSelectTemplate + "&contractid=" + detailId, function (response) {
            var options = "";
            options += '<option value="">--请选择模板--</option>';
            $.each(response, function (i, item) {
                options += '<option value="' + item.ID + '">' + item.NAME + '</option>';
            });
            form.find("#templateId").empty().append(options).selectmenu("refresh");
        });
    }

    form.find("#templateId").off("change").on("change", function () {
        $(this).selectmenu("refresh");
        first_open_detail_addFile1_draft = false;
        $.ajax({
            url: sysUrl + woo.url.contractGetCustomValues,
            type: "POST",
            dataType: "jsonp",
            crossDomain: "true",
            data: {
                templateId: $(this).val()
            }
        }).done(function (response) {
                var html = "";
                var count = 1;
                $.each(response, function (i, item) {
                    html += '<div class="ui-field-contain">' +
                        '<input type="hidden" name="param_id_' + count + '" readOnly value="' + item.VarId + '"/>' +
                        '<label for="param_content_' + count + '">' + item.VarName + ':</label>' +
                        '<textarea name="param_content_' + count + '" placeholder="' + item.VarName + '"></textarea>' +
                        '</div>';
                    count++;
                });
                form.find("#template_values").empty().html(html);
                form.find("textarea").textinput();
            }).fail(function () {
                console.log("获取合同模板自定义变量失败!");
            });
    });


    //保存附件
    form.unbind('submit').submit(function () {
        if (form.find("#templateId").val() != "") {
            $("#draftsubmiting").popup("open");
            $.ajax({
                url: sysUrl + woo.url.contractDetailFile1Draft,
                type: "POST",
                dataType: "jsonp",
                crossDomain: "true",
                data: $(this).serialize() + "&contractId=" + detailId
            }).done(function () {
                    $("#draftsubmiting").popup("close");
                    first_open_detail_addFile1_draft = true;
                    window.setTimeout(function () {
                        backToDetail();
                    }, 500);
                }).fail(function () {
                    console.log("保存合同正文失败!");
                });
        }
        return false;
    });

    //取消
    form.find("a.cancel").unbind('click').click(function (e) {
        backToDetail();
    });
});

/***************************
 * 查看页保存附件
 **************************/

$(document).on("pageshow", "#detail_addFile2", function () {

    var form = $("#detail_addFile2_form");
    form[0].reset(); //重置表单

    //初始化附件类别选项
    initSelectItems(sysUrl + woo.url.contractSelectFile2, function (response) {
        var options = "";
        $.each(response, function (i, item) {
            options += '<option value="' + item.ID + '">' + item.NAME + '</option>';
        });
        form.find("select").empty().append(options);
        form.find("select").selectmenu("refresh");
    });

    /**
     * 文件上传到服务器增加随机前缀
     * @type {string}
     */
    var pre = "MAT" + Math.floor(Math.random() * 100000) + "-";
    form.find('[name=file_pre]').val(pre);

    //初始化上传控件
    if (device.platform == 'android' || device.platform == 'Android') {
        form.find("#choose2_upload").css("display", "none");
        form.find('[name=file_upload]').fileupload({
            url: sysUrl + woo.url.contractUploadFile + "&type=2&pre=" + pre,
            forceIframeTransport: true,
            done: function (e, data) {
                try {
                    //做判断防止android浏览器出错
                    if (data && data.files && data.files[0].name) {
                        form.find('[name=file_name]').val(data.files[0].name + "");
                        form.find('[name=file_displayname]').val(data.files[0].name + "");
                    }
                } catch (e) {
                }

                return false;
            },
            fail: function (e, data) {
                $('#uploadfailure').popup("open");
                return false;
            }
        });
    } else {
        //IOS
        form.find('[name=file_upload]').css("display", "none");
        form.find('#choose2_upload').click(function () {
            event.preventDefault();

            function uploadPhoto(imageURI) {
                var options = new FileUploadOptions();
                options.fileKey = "file";
                options.fileName = imageURI.substr(imageURI.lastIndexOf('/') + 1);
                options.mimeType = "image/jpeg";
                options.chunkedMode = false;

                var ft = new FileTransfer();
                ft.upload(imageURI, sysUrl + woo.url.contractUploadFile + "&type=2&pre=" + pre,
                    function (r) {
                        console.log("Code = " + r.responseCode);
                        console.log("Response = " + r.response);
                        console.log("Sent = " + r.bytesSent);
                        try {
                            form.find('[name=file_name]').val(r.response + "");
                            form.find('[name=file_displayname]').val(r.response + "");
                        } catch (e) {
                        }
                    }, function (error) {
                        console.log("An error has occurred: Code = " + error.code);
                    }, options);
            }


            navigator.camera.getPicture(uploadPhoto, function (message) {
                    console.log('获取文件失败！');
                }, {
                    quality: 80,
                    destinationType: navigator.camera.DestinationType.FILE_URI,
                    sourceType: navigator.camera.PictureSourceType.PHOTOLIBRARY
                }
            );
        });
    }

    //保存附件
    form.unbind('submit').submit(function () {
        $.ajax({
            url: sysUrl + woo.url.contractAddFile2,
            type: "POST",
            dataType: "jsonp",
            crossDomain: "true",
            data: $(this).serialize() + "&detailId=" + detailId + "&historyId=" + historyId + "&type=" + businessType
        }).done(function () {
                //$("#detail_addFile").dialog("close");
                backToDetail();
            }).fail(function () {
                console.log("保存合同附件失败!");
            });

        return false;
    });

    //取消
    form.find("a.cancel").unbind('click').click(function (e) {
        backToDetail();
    });
});

/***************************
 * 查看页保存备忘
 **************************/

$(document).on("pageshow", "#detail_addRemark", function () {

    var form = $("#detail_addRemark_form");
    form[0].reset(); //重置表单

    form.unbind('submit').submit(function () {
        $.ajax({
            url: sysUrl + woo.url.contractAddRemark,
            type: "POST",
            dataType: "jsonp",
            crossDomain: "true",
            data: $(this).serialize() + "&detailId=" + detailId
        }).done(function () {
                //$("#detail_addRemark").dialog("close");
                backToDetail();
            }).fail(function () {
                console.log("查看页保存备忘失败!");
            });

        return false;
    });

    //取消
    form.find("a.cancel").unbind('click').click(function (e) {
        backToDetail();
    });
});

/***************************
 * 查看页提交审批
 **************************/

$(document).on("pageshow", "#detail_commitWorkflow", function () {

    var form = $("#detail_commitWorkflow_form");

    var stateNow = $("#detail_basic").find("[name=state]").text();

    var html = ""; //可以变更的选项
    switch (stateNow) {
        case "未执行":
        {
            html += '<option value="执行中" selected>执行中</option>' +
                '<option value="已作废">已作废</option>';
            break;
        }
        case "变更未执行":
        {
            html += '<option value="执行中" selected>执行中</option>';
            break;
        }
        case "执行中":
        {
            html += '<option value="已完成" selected>已完成</option>' +
                '<option value="已终止">已终止</option>';
            break;
        }
        default:
        {
            html += '<option value="执行中" selected>执行中</option>' +
                '<option value="已作废">已作废</option>';
            break;
        }
    }
    form.find("[name=stateTo]").html(html).selectmenu("refresh");

    //提交审批
    form.unbind('submit').submit(function () {

        form.find("[name=stateNow]").val(stateNow);

        $.ajax({
            url: sysUrl + woo.url.contractCommitWorkflow,
            type: "POST",
            dataType: "jsonp",
            crossDomain: "true",
            data: $(this).serialize() + "&detailId=" + detailId
        }).done(function () {
                //$("#detail_changeState").dialog("close");
                alert("提交成功!", function(){
                	backToDetail();
                });
            }).fail(function () {
                console.log("查看页提交审批失败!");
            });

        return false;
    });

    //取消
    form.find("a.cancel").unbind('click').click(function (e) {
        backToDetail();
    });
});

//TODO:修改状态
