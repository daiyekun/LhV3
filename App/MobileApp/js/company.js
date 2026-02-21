/******************
 * Woorich Dev Team
 * By: Jnoodle
 *
 * 合同对方页（客户、供应商）
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
var businessTypeDisplayName = (businessType == 0 ? "客户" : "供应商"); //TODO:根据业务需要修改

/**
 * 修改页面header标题
 * @param pageId 页面Id
 * @param title 标题
 */
var changeHeaderTitle = function (pageId, title) {
    $("#" + pageId).find("[data-role=header]").find("h1").text(title);
};

$("#backtoindex").click(function (e) {
    e.preventDefault();
    window.location.href = "index.html#index";
});
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
            if (woo.getParameterByName("select", $.mobile.path.parseUrl(location.href).search) == "true") {
                //如果是选择
                window.parent.document.getElementById('company').value = this.name;
                window.parent.document.getElementById('companyId').value = this.id;
                parent.hideCompanySelect(); //调用父页面方法
            } else {
                $("#detailId").val(this.id);
                //console.log($("#detailId").val());
                var url = $.mobile.path.parseUrl(location.href);
                window.location.href = url.filename + url.search + "&id=" + this.id + "#detail";
            }
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
        '<td class="title"><a href="#" id="' + Element.Id + '" name="' + Element.Name + '">' + Element.Name + '</a></td>' +
        //  '<td class="title"><a href="#" id="' + Element.ID + '" name="' + Element.NAME + '">' + Element.NAME + '</a></td>' +
        '<td>' + Element.Code + '</td>' +
        '<td>' + Element.FirstContact + '</td>' +
        '<td>' + Element.FirstContactTel + '</td>' +
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


/**
 * 被选中的联系人信息（在选择手机联系人后赋值）
 * @type {{name: string, tel: string, mobile: string, email: string}}
 */
var contactObj = {
    name: "",
    tel: "",
    mobile: "",
    email: ""
};

/**
 * 选择手机联系人
 * contact.name
 * contact.phone
 * contact.email
 * @param callback function(contact) 处理方法
 */
var selectMobileContact = function (callback) {

    /**
     //不再使用插件ContactViewPlugin.java，因为不兼容ios等其他系统
     var contactViewPlugin = new ContactViewPlugin();
     contactViewPlugin.show(onSuccess);

     function onSuccess(contact) {
        try {
            //这里使用了ContactViewPlugin插件，里面暂时只获取name、phone和email，等以后再扩展
            callback(contact);
        } catch (e) {
            console.log(e);
        }
    }
     **/

    $("#detail_selectContact").unbind("pagehide");
    $("#detail_selectContact").bind("pagehide", function () {
        callback(contactObj);
    });

    $.mobile.changePage("#detail_selectContact", {
        role: "dialog"
    });
};

/***************************
 * 选择手机联系人
 **************************/
$(document).on("pageshow", "#detail_selectContact", function () {

    //此页面列出所有的手机联系人，并可以筛选

    var options = new ContactFindOptions();
    options.multiple = true;
    var fields = ["displayName", "name", "phoneNumbers", "emails"];
    navigator.contacts.find(fields, onContactSuccess, onContactError, options);

    function onContactSuccess(contacts) {

        var html = "";

        for (var i = 0; i < contacts.length; i++) {
            if (device.platform == 'android' || device.platform == 'Android') {
                if (contacts[i].displayName != null) {
                    html += "<li cid='" + i + "'>" + contacts[i].displayName + "&nbsp;<span>(";
                    if (contacts[i].phoneNumbers && contacts[i].phoneNumbers != null) {
                        for (var j = 0; j < contacts[i].phoneNumbers.length; j++) {
                            var _type = "手机";
                            switch (contacts[i].phoneNumbers[j].type) {
                                case "mobile":
                                    _type = "手机";
                                    break;
                                case "work":
                                    _type = "工作";
                                    break;
                                default:
                                    _type = "其他";
                                    break;
                            }
                            html += _type + ":" + contacts[i].phoneNumbers[j].value + ";";
                        }
                    }
                    html += ")</span></li>";
                }
            } else {
                //IOS
                if (contacts[i].name != null) {
                    html += "<li cid='" + i + "'>" + contacts[i].name.formatted + "&nbsp;<span>(";
                    if (contacts[i].phoneNumbers && contacts[i].phoneNumbers != null) {
                        for (var j = 0; j < contacts[i].phoneNumbers.length; j++) {
                            var _type = "手机";
                            switch (contacts[i].phoneNumbers[j].type) {
                                case "mobile":
                                    _type = "手机";
                                    break;
                                case "work":
                                    _type = "工作";
                                    break;
                                default:
                                    _type = "其他";
                                    break;
                            }
                            html += _type + ":" + contacts[i].phoneNumbers[j].value + ";";
                        }
                    }
                    html += ")</span></li>";
                }
            }
        }

        $("#detail_selectContact_list").html(html).listview("refresh");
        $("#detail_selectContact_list_loading").hide();
        $("#detail_selectContact_list").find("li").on("click", function () {
            var cid = $(this).attr("cid");
            var _contact = contacts[parseInt(cid)];

            //清空contactObj
            contactObj.name = "";
            contactObj.tel = "";
            contactObj.mobile = "";
            contactObj.email = "";

            contactObj.name = _contact.displayName || _contact.name.formatted;
            if (_contact.phoneNumbers != null) {
                for (var j = 0; j < _contact.phoneNumbers.length; j++) {
                    if (_contact.phoneNumbers[j].type == "mobile") {
                        contactObj.mobile = _contact.phoneNumbers[j].value;
                        j = 100000000000000;
                    }
                }
            }
            if (_contact.phoneNumbers != null) {
                for (var j = 0; j < _contact.phoneNumbers.length; j++) {
                    if (_contact.phoneNumbers[j].type == "work") {
                        contactObj.tel = _contact.phoneNumbers[j].value;
                        j = 100000000000000;
                    }
                }
            }
            contactObj.email = _contact.emails != null ? _contact.emails[0].value : "";

            $('#detail_selectContact').dialog("close");

        });
    };

    function onContactError(contactError) {
        console.log('获取联系人错误!');
    }

});

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
 
    //  var dataUrl = sysUrl + woo.url.APPcompanyList;
    var dataUrl = sysUrl + woo.url.APPSompanylist;
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
        //判断权限
        $.ajax({
            url: sysUrl + woo.url.permission,
            type: "POST",
            dataType: "jsonp",
            crossDomain: "true",
            data: {
                permissionname: businessTypeDisplayName + "建立"
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

    //作为选择框出现
    if (woo.getParameterByName("select", $.mobile.path.parseUrl(location.href).search) == "true") {
        $("#add_data").css("display", "none");
        $("[data-role=footer]").css("display", "none");
        document.getElementById('backtoindex').style.display = "none"; //隐藏返回按钮
    }
});


/***************************
 * 新建/修改页
 **************************/

$(document).on("pageinit", "#add", function () {

    var form = $("#addform");

    //更新标题
    changeHeaderTitle("add", "新建" + businessTypeDisplayName);

    //初始化客户类别选项
    initSelectItems(sysUrl + woo.url.companySelectType, function (response) {
        var options = "";
        $.each(response, function (i, item) {
            options += '<option value="' + item.ID + '">' + item.NAME + '</option>';
        });
        form.find("#category").empty().append(options).selectmenu("refresh");
    });

    //初始化客户级别选项
    initSelectItems(sysUrl + woo.url.companySelectLevel, function (response) {
        var options = "";
        $.each(response, function (i, item) {
            options += '<option value="' + item.ID + '">' + item.NAME + '</option>';
        });
        form.find("#level").empty().append(options).selectmenu("refresh");
    });

    //初始化信用等级选项
    initSelectItems(sysUrl + woo.url.companySelectCLevel, function (response) {
        var options = "";
        $.each(response, function (i, item) {
            options += '<option value="' + item.ID + '">' + item.NAME + '</option>';
        });
        form.find("#clevel").empty().append(options).selectmenu("refresh");
    });

    //初始化客户附件类别选项
    var fileTypeItems = "";
    initSelectItems(sysUrl + woo.url.companySelectFile, function (response) {
        $.each(response, function (i, item) {
            fileTypeItems += '<option value="' + item.ID + '">' + item.NAME + '</option>';
        });
        //$('#collapsible_file select').append(fileTypeItems).selectmenu("refresh");
    });

    //TODO:从联系人中选择

    //选择手机联系人(基本信息-首要联系人)
    $("#selectMainContact").click(function (e) {
        e.preventDefault();
        selectMobileContact(function (contact) {
            form.find("#contactname").val(contact.name);
            form.find("#contacttel").val(contact.tel);
            form.find("#contactmobile").val(contact.mobile);
            form.find("#contactemail").val(contact.email);
        });
    });

    //选择手机联系人
    $("#selectContact").click(function (e) {
        e.preventDefault();
        selectMobileContact(function (contact) {
            var count = addContact("addContact");
            form.find("#contact_name_" + count).val(contact.name);
            form.find("#contact_tel_" + count).val(contact.tel);
            form.find("#contact_mobile_" + count).val(contact.mobile);
            form.find("#contact_email_" + count).val(contact.email);
        });
    });

    var contactCount = 0;

    /**
     * 增加联系人
     * @param id 在什么元素后增加
     * @returns {number} count 联系人表单索引
     */
    var addContact = function (id) {

        /*
         <div data-role="collapsible" data-collapsed="false">
         <h4>客户联系人
         <a href="#" class="ui-btn-right ui-btn ui-icon-delete ui-btn-icon-notext">删除</a>
         </h4>
         <div class="ui-field-contain">
         <label for="contact_name">联系人姓名:</label>
         <input type="text" name="contact_name" id="contact_name" placeholder="联系人姓名" required/>
         </div>
         <div class="ui-field-contain">
         <label for="contact_job">职位:</label>
         <input type="text" name="contact_job" id="contact_job" placeholder="职位"/>
         </div>
         <div class="ui-field-contain">
         <label for="contact_tel">办公电话:</label>
         <input type="text" name="contact_tel" id="contact_tel" placeholder="办公电话"/>
         </div>
         <div class="ui-field-contain">
         <label for="contact_mobile">移动电话:</label>
         <input type="text" name="contact_mobile" id="contact_mobile" placeholder="移动电话"/>
         </div>
         <div class="ui-field-contain">
         <label for="contact_email">Email:</label>
         <input type="email" name="contact_email" id="contact_email" placeholder="Email"/>
         </div>
         </div>
         */

        var count = Math.max($("#collapsible_contact").find("[data-role=collapsible]").length, contactCount) + 1;
        contactCount = count;

        var html = '<div data-role="collapsible" data-collapsed="false">' +
            '<h4>客户联系人' + count + '</h4>' +
            '<a href="#" class="ui-btn ui-icon-delete ui-btn-icon-left ui-corner-all delete">删除</a>' +
            '<div class="ui-field-contain">' +
            '<label for="contact_name_' + count + '">联系人姓名:</label>' +
            '<input type="text" name="contact_name_' + count + '" id="contact_name_' + count + '" placeholder="联系人姓名" required/>' +
            '</div><div class="ui-field-contain">' +
            '<label for="contact_job_' + count + '">职位:</label>' +
            '<input type="text" name="contact_job_' + count + '" id="contact_job_' + count + '" placeholder="职位"/>' +
            '</div><div class="ui-field-contain">' +
            '<label for="contact_tel_' + count + '">办公电话:</label>' +
            '<input type="text" name="contact_tel_' + count + '" id="contact_tel_' + count + '" placeholder="办公电话"/>' +
            '</div><div class="ui-field-contain">' +
            '<label for="contact_mobile_' + count + '">移动电话:</label>' +
            '<input type="text" name="contact_mobile_' + count + '" id="contact_mobile_' + count + '" placeholder="移动电话"/>' +
            '</div><div class="ui-field-contain">' +
            '<label for="contact_email_' + count + '">Email:</label>' +
            '<input type="email" name="contact_email_' + count + '" id="contact_email_' + count + '" placeholder="Email"/>' +
            '</div></div>';

        $("#" + id).after(html);
        var collapsibles = $("#collapsible_contact").find("[data-role=collapsible]");
        collapsibles.collapsible(); //初始化collapsible样式
        collapsibles.find("input").textinput(); //初始化input样式

        //collapsible点击按钮默认是删除自己
        collapsibles.find("a.delete").click(function (e) {
            e.preventDefault();
            $($(this).parents("[data-role=collapsible]")[0]).remove();
        });

        return count;
    }

    //增加联系人
    $("#addContact").click(function (e) {
        e.preventDefault();
        addContact("addContact");
    });

    //本地上传客户附件
    $("#selectFile").click(function (e) {
        e.preventDefault();
        var count = $("#collapsible_file").find("[data-role=collapsible]").length + 1;

        /*
         <div data-role="collapsible" data-collapsed="false">
         <h4>客户资质
         <a href="#" class="ui-btn-right ui-btn ui-icon-delete ui-btn-icon-notext">删除</a>
         </h4>
         <div class="ui-field-contain">
         <input type="file" name="file_upload" id="file_upload" data-role="none"/>
         <input type="text" name="file_name" id="file_name" required/>
         </div>
         <div class="ui-field-contain">
         <label for="file_displayname">名称:</label>
         <input type="text" name="file_displayname" id="file_displayname" placeholder="附件名称"/>
         </div>
         <div class="ui-field-contain">
         <label for="file_type">类别:</label>
         <select name="file_type" id="file_type" placeholder="类别"></select>
         </div>
         <div class="ui-field-contain">
         <label for="file_remark">说明:</label>
         <input type="text" name="file_remark" id="file_remark" placeholder="说明"/>
         </div>
         </div>
         */

        var html = '<div data-role="collapsible" data-collapsed="false">' +
            '<h4>' + businessTypeDisplayName + '资质' + count + '</h4>' +
            '<a href="#" class="ui-btn ui-icon-delete ui-btn-icon-left ui-corner-all delete">删除</a>' +
            '<div class="ui-field-contain">' +
            '<a href="#" class="ui-btn ui-corner-all" id="choose_upload_' + count + '">选择文件</a>' +
            '<input type="file" name="file_upload_' + count + '" id="file_upload_' + count + '" data-role="none"/>' +
            //'<label for="file_name_' + count + '">文件名:</label>' +
            '<input type="hidden" name="file_name_' + count + '" id="file_name_' + count + '" required/>' +
            '<input type="hidden" name="file_pre_' + count + '" id="file_pre_' + count + '" />' +
            '</div><div class="ui-field-contain">' +
            '<label for="file_displayname_' + count + '">名称:</label>' +
            '<input type="text" name="file_displayname_' + count + '" id="file_displayname_' + count + '" placeholder="名称" required/>' +
            '</div><div class="ui-field-contain">' +
            '<label for="file_type_' + count + '">类别:</label>' +
            '<select name="file_type_' + count + '" id="file_type_' + count + '" placeholder="类别"></select>' +
            '</div><div class="ui-field-contain">' +
            '<label for="file_remark_' + count + '">说明:</label>' +
            '<input type="text" name="file_remark_' + count + '" id="file_remark_' + count + '" placeholder="说明"/>' +
            '</div></div>';

        $(this).after(html);
        var collapsibles = $("#collapsible_file").find("[data-role=collapsible]");
        collapsibles.collapsible(); //初始化collapsible样式
        collapsibles.find("input:not(:file,:hidden)").textinput(); //初始化input样式
        collapsibles.find("select").empty().append(fileTypeItems).selectmenu(); //初始化select样式

        //设置文件名随机前缀种子值，防止上传文件重名覆盖
        var pre = "MAT" + Math.floor(Math.random() * 100000) + "-";
        $("#file_pre_" + count).val(pre);

        //collapsible点击按钮默认是删除自己
        collapsibles.find("a.delete").click(function (e) {
            e.preventDefault();
            $($(this).parents("[data-role=collapsible]")[0]).remove();
        });

        //ajax上传文件,使用 uploadify 或 blueimp / jQuery-File-Upload

        //        $('#file_upload_' + count).uploadify({
        //            swf: '/vendor/uploadify.swf',
        //            uploader: sysUrl + woo.url.companyUploadFile + "&type=" + businessType,
        //            buttonText: "选择文件",
        //            onUploadSuccess: function (file, data, response) {
        //                if (!data || data.length < 3) {
        //                    $('#uploadfailure').popup("open");
        //                } else {
        //                    $('#file_name_' + count).val(data);
        //                }
        //            },
        //            onUploadError: function () {
        //                $('#uploadfailure').popup("open");
        //            },
        //            onSelect: function (file) {
        //                $('#file_displayname_' + count).val(file.name);
        //            }
        //        });

        //uploadify不支持ios系统，所以需要选择更好地ajax upload组件 blueimp / jQuery-File-Upload
        if (device.platform == 'android' || device.platform == 'Android') {
            $("#choose_upload_" + count).css("display", "none");
            $('#file_upload_' + count).fileupload({
                url: sysUrl + woo.url.companyUploadFile + "&type=" + businessType + "&pre=" + pre,
                forceIframeTransport: true,
                done: function (e, data) {
                    try {
                        //做判断防止android浏览器出错
                        if (data && data.files && data.files[0].name) {
                            $('#file_name_' + count).val(data.files[0].name + "");
                            $('#file_displayname_' + count).val(data.files[0].name + "");
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
            $("#file_upload_" + count).css("display", "none");
            $('#choose_upload_' + count).click(function () {
                event.preventDefault();

                function uploadPhoto(imageURI) {
                    var options = new FileUploadOptions();
                    options.fileKey = "file";
                    options.fileName = imageURI.substr(imageURI.lastIndexOf('/') + 1);
                    options.mimeType = "image/jpeg";
                    options.chunkedMode = false;

                    var ft = new FileTransfer();
                    ft.upload(imageURI, sysUrl + woo.url.companyUploadFile + "&type=" + businessType + "&pre=" + pre,
                        function (r) {
                            console.log("Code = " + r.responseCode);
                            console.log("Response = " + r.response);
                            console.log("Sent = " + r.bytesSent);
                            try {
                                $('#file_name_' + count).val(r.response + "");
                                $('#file_displayname_' + count).val(r.response + "");
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

            $("#submiting").popup("open");

            $.ajax({
                url: sysUrl + woo.url.companyAdd + "&type=" + businessType,
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
                        getListData(1, sysUrl + woo.url.companyList, getListDataCallBack);//重新刷新数据
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
                console.log("合同对方新建地址访问失败!");
                $("#submiting").popup("close");
                window.setTimeout(function () {
                    $('#submiterror').popup('open');
                }, 1000);
            });
        }

        return false;
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
var categories;
$(document).on("pageshow", "#detail", function () {

    //更新标题
    changeHeaderTitle("detail", "查看" + businessTypeDisplayName);
 
    //详细信息Id
    detailId = $("#detailId").val() || woo.getParameterByName("id", $.mobile.path.parseUrl(location.href).search);

    console.log(detailId);

    if (detailId > 0) {

    

        //获取全部类别信息
        getCategories(function (response) {
            categories = response;

            //获取基本信息
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

                ////判断修改状态权限
                //$.ajax({
                //    url: sysUrl + woo.url.permission,
                //    type: "POST",
                //    dataType: "jsonp",
                //    crossDomain: "true",
                //    data: {
                //        permissionname: businessTypeDisplayName + "状态修改",
                //        id: detailId
                //    }
                //}).done(function (response) {
                //    if (!response.success) {
                //        $("#detail_menu").find("#detail_menu_changeState").hide();
                //    }
                //})
                //    .fail(function () {
                //        console.log("获取权限信息失败!");
                //    });

            }).fail(function () {
                console.log("获取合同对方基本信息失败!");
                });
         ////========================
            $.ajax({
                url: sysUrl + woo.url.companyDetailContact,
                type: "POST",
                dataType: "jsonp",
                crossDomain: "true",
                data: {
                    detailId: detailId
                }
            }).done(function (response) {
                //console.log(response);
                var table = $("#detail_contacts_table");
                var html = "";
          
                $.each(response, function (i, d) {
             
                    html += '<tr>' +
                        '<th>' + d.Id + '</th>' +
                        '<td class="title">' + d.Name + '</td>' +
                        '<td>' + d.Position + '</td>' +
                        '<td>' + d.Tel + '</td>' +
                        '<td>' + d.Mobile + '</td>' +
                        '<td>' + d.Email + '</td>' +
                        '</tr>';
                
                });
                table.find("tbody").html(html);
                table.table("refresh");
            }).fail(function () {
                console.log("获取合同实际资金信息失败!");
            });
        ////=========================
            //获取合同对方联系人信息
            //$.ajax({
            //    url: sysUrl + woo.url.companyDetailContact,
            //    type: "POST",
            //    dataType: "jsonp",
            //    crossDomain: "true",
            //    data: {
            //        detailId: detailId
            //    }
            //}).done(function (response) {
           
            //    var table = $("#detail_contacts_table");
            //    var html = "";
            //    $.each(response, function (t,s) {
            //        html += '<tr>' +
            //            '<th>' + s.Id + '</th>' +
            //            '<td class="title">' + s.FirstContact + '</td>' +
            //            '<td>' + s.FirstContactPosition + '</td>' +
            //            '<td>' + s.FirstContactTel + '</td>' +
            //            '<td>' + s.FirstContactMobile + '</td>' +
            //            '<td>' + s.FirstContactEmail + '</td>' +
            //            '</tr>';
            //    });
            //    table.find("tbody").html(html);
            //    table.table("refresh");
            //}).fail(function () {
            //    console.log("获取合同对方联系人信息失败!");
            //});

            //获取合同对方附件信息
            $.ajax({
                url: sysUrl + woo.url.companyDetailFile,
                type: "POST",
                dataType: "jsonp",
                crossDomain: "true",
                data: {
                    detailId: detailId
                }
            }).done(function (response) {
                ////console.log(response);
                //var table = $("#detail_files_table");
                //var html = "";
                //$.each(response, function (i, d) {
                //    var type = "";
                //    //遍历字典给字典字段赋值
                //    $.each(categories, function (i, v) {
                //        if (v.id == d.type) {
                //            type = v.name;
                //            return false;
                //        }
                //    });
                var table = $("#detail_files_table");
                var html = "";
            
                $.each(response, function (i, d) {
                
                    html += '<tr>' +
                        '<th>' + d.Id + '</th>' +
                        '<td class="title"><a href="' + sysUrl + d.Path + '" target="_blank" onclick="window.open(this.href,\'_system\'); return false;">' + d.Name + '</a></td>' +
                        '<td>' + d.CategoryName + '<td>' +
                      //  '<td>' + b.Remark + '<td>' +
                        '<td>' + d.FileName + '<td>' +
                        '<td>' + d.CreateDateTime + '<td>' +
                        //'<td class="title"><a href="' + sysUrl + d.path + '" target="_blank" onclick="window.open(this.href,\'_system\'); return false;">' + d.Name + '</a></td>' +
                    
                        //'<td>' + d.Remark + '</td>' +
                        '</tr>';
                });
                table.find("tbody").html(html);
                table.table("refresh");
            }).fail(function () {
                console.log("获取合同对方附件信息失败!");
            });

            ////获取合同对方资金统计信息
            //$.ajax({
            //    url: sysUrl + woo.url.companyDetailFinance,
            //    type: "POST",
            //    dataType: "jsonp",
            //    crossDomain: "true",
            //    data: {
            //        detailId: detailId
            //    }
            //}).done(function (r) {
            //    //console.log(r);
            //    var html = "";
            //    html += "<li>" + "合同金额:" + "<span>" + r.sfk_jhfk + "</span></li>"
            //        + "<li>" + (businessType == 0 ? "合同实际收款:" : "合同实际付款:") + "<span>" + r.sfk_sjfk + "</span></li>"
            //        + "<li>" + (businessType == 0 ? "合同未收款:" : "合同未付款:") + "<span>" + r.sfk_jhfkye + "</span></li>"
            //        + "<li>" + (businessType == 0 ? "合同已开发票:" : "合同已收发票:") + "<span>" + r.sfk_ysfp + "</span></li>"
            //        + "<li>" + (businessType == 0 ? "合同未开发票:" : "合同未收发票:") + "<span>" + r.sfk_wsfp + "</span></li>";

            //    $("#detail_finance_listview").html(html).listview("refresh");
            //}).fail(function () {
            //    console.log("获取合同对方资金统计信息失败!");
            //});

            //获取合同对方沟通记录信息
            $.ajax({
                url: sysUrl + woo.url.companyDetailRemark,
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
                    html += "<li>时间:<span>" + d.date + "</span><br>事项:<span>" + d.title + "</span><p>" + d.remark + "</p></li>";
                });

                $("#detail_remark_listview").html(html).listview("refresh");
            }).fail(function () {
                console.log("获取合同对方沟通记录信息失败!");
            });

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

        }
        );

    }

    //返回
    $("#detail_back").click(function () {
        var url = $.mobile.path.parseUrl(location.href);
        var type = woo.getParameterByName("type", url.search);
        window.location.href = url.filename + "?type=" + type;
    });
});
//.fail(function () {
//    console.log("获取权限信息失败!");
//});


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

//});

/**
 * 返回查看页面
 */
var backToDetail = function () {
 
    var url = $.mobile.path.parseUrl(location.href);
    window.location.href = url.hrefNoHash + "#detail";
}

/***************************
 * 查看页保存联系人
 **************************/
var first_open_detail_addContact = true; //是否是第一次打开保存联系人页面
$(document).on("pageshow", "#detail_addContact", function () {

    var form = $("#detail_addContact_form");

    if (first_open_detail_addContact) {
        form[0].reset();
    }
    //选择手机联系人
    $("#detail_addContact_selectContact").unbind("click").click(function (e) {
        e.preventDefault();
        first_open_detail_addContact = false;
        selectMobileContact(function (contact) {
            form.find('[name=contactname]').val(contact.name + "");
            form.find('[name=contacttel]').val(contact.tel + "");
            form.find('[name=contactmobile]').val(contact.mobile + "");
            form.find('[name=contactemail]').val(contact.email + "");
        });
    });

    //保存联系人
    form.unbind('submit').submit(function (e) {
        e.preventDefault();
        $.ajax({
            url: sysUrl + woo.url.companyAddContact,
            type: "POST",
            dataType: "jsonp",
            crossDomain: "true",
            data: $(this).serialize() + "&detailId=" + detailId
        }).done(function () {
            first_open_detail_addContact = true;
            backToDetail();
        }).fail(function () {
            console.log("保存联系人失败!");
        });

        return false;
    });

    //取消
    form.find("a.cancel").click(function (e) {
        backToDetail();
    });
});


/***************************
 * 查看页保存附件
 **************************/

$(document).on("pageshow", "#detail_addFile", function () {

    var form = $("#detail_addFile_form");
    form[0].reset(); //重置表单

    //初始化附件类别选项
    initSelectItems(sysUrl + woo.url.companySelectFile, function (response) {
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
        form.find("#choose_upload").css("display", "none");
        form.find('[name=file_upload]').fileupload({
            url: sysUrl + woo.url.companyUploadFile + "&type=" + businessType + "&pre=" + pre,
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
        form.find('#choose_upload').click(function () {
            event.preventDefault();

            function uploadPhoto(imageURI) {
                var options = new FileUploadOptions();
                options.fileKey = "file";
                options.fileName = imageURI.substr(imageURI.lastIndexOf('/') + 1);
                options.mimeType = "image/jpeg";
                options.chunkedMode = false;

                var ft = new FileTransfer();
                ft.upload(imageURI, sysUrl + woo.url.companyUploadFile + "&type=" + businessType + "&pre=" + pre,
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
    form.unbind('submit').submit(function (e) {
        e.preventDefault();
        $.ajax({
            url: sysUrl + woo.url.companyAddFile,
            type: "POST",
            dataType: "jsonp",
            crossDomain: "true",
            data: $(this).serialize() + "&detailId=" + detailId + "&type=" + businessType
        }).done(function () {
            //$("#detail_addFile").dialog("close");
            backToDetail();
        }).fail(function () {
            console.log("保存附件失败!");
        });

        return false;
    });

    //取消
    form.find("a.cancel").click(function (e) {
        backToDetail();
    });
});

/***************************
 * 查看页保存沟通记录
 **************************/

$(document).on("pageshow", "#detail_addRemark", function () {

    var form = $("#detail_addRemark_form");
    form[0].reset(); //重置表单

    $("#detail_addRemark_form_submit").unbind('click').click(function (e) {
        e.preventDefault();
        $.ajax({
            url: sysUrl + woo.url.companyAddRemark,
            type: "POST",
            dataType: "jsonp",
            crossDomain: "true",
            data: $(form).serialize() + "&detailId=" + detailId
        }).done(function () {
            //$("#detail_addRemark").dialog("close");
            backToDetail();
        }).fail(function () {
            console.log("查看页保存沟通记录失败!");
        });
    });

    //取消
    form.find("a.cancel").click(function (e) {
        backToDetail();
    });
});

/***************************
 * 查看页修改状态
 **************************/

$(document).on("pageshow", "#detail_changeState", function () {

    var form = $("#detail_changeState_form");

    //根据现在状态赋值
    var select = form.find("select");
    var state = $("#detail_basic").find("[name=state]").text();
    select.html('<option value="未审核" ' + (state == "未审核" ? "selected" : "") + '>未审核</option>' +
        '<option value="审核通过" ' + (state == "审核通过" ? "selected" : "") + '>审核通过</option>' +
        '<option value="已终止" ' + (state == "已终止" ? "selected" : "") + '>已终止</option>');
    select.selectmenu("refresh");

    //保存状态
    form.unbind('submit').submit(function (e) {
        e.preventDefault();
        $.ajax({
            url: sysUrl + woo.url.companyChangeState,
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