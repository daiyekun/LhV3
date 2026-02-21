/******************
 * Woorich Dev Team
 * By: Jnoodle
 *
 * 基础配置及公用方法
 ******************/

$.validator.messages = {
    required: "必填",
    remote: "请输入这些项",
    email: "请输入一个有效的email地址",
    url: "请输入一个有效的Url地址",
    date: "请输入一个有效的日期",
    dateISO: "Please enter a valid date (ISO).",
    dateDE: "Bitte geben Sie ein gültiges Datum ein.",
    number: "请输入一个有效的数字",
    numberDE: "Bitte geben Sie eine Nummer ein.",
    digits: "只能输入数字",
    creditcard: "请输入有效信用卡号码",
    equalTo: "请再次输入相同的值",
    accept: "请输入一个有效的扩展名.",
    maxlength: $.format("最多能输入 {0} 个字符"),
    minlength: $.format("请输入至少 {0} 个字符"),
    rangelength: $.format("请输入从{0} 到 {1} 字符长度"),
    range: $.format("请输入从 {0} 到 {1}."),
    max: $.format("请输入一个值小于等于 {0}."),
    min: $.format("请输入一个值大于等于 {0}.")
};

(function () {
    window.alert = function (text, alertCallback) {
        text = text.toString().replace(/\\/g, '\\').replace(/\n/g, '<br />').replace(/\r/g, '<br />'); //解析alert内容中的换行符
        var alertdiv = '<div id="alertdiv" style="position:absolute; display:none ; overflow:hidden;  padding:10px 10px 8px; top: 50%; left: 50%; text-align:center; line-height:22px; background-color:#DDE4EE; border:1px solid #ccc">' + text + '<br /><input type="submit" name="button" id="woorichAlertOK" value="确定" style="margin-top:8px;"/></div>'; //自定义div弹窗
        $(document.body).append(alertdiv);  //动态加载div
        $("#alertdiv").css({ "margin-left": $("#alertdiv").width() / 2 * (-1) - 20, "margin-top": $("#alertdiv").height() / 2 * (-1) - 20 }); //设置偏移数值，实现div居中
        $("#alertdiv").show(); //显示
        $("#woorichAlertOK").click(function () {
            $("#alertdiv").remove();
            if (alertCallback)
                alertCallback();
        });
    };
})();

/**
 * woo 公用方法
 */
var woo = {

    /**
     * 分页每页数量
     */
    pageItem: 20,

    /**
     * 服务地址
     */
    url: {
        login: "MobileApp/Service.aspx?cmd=login",
        applogin: "MobileApp/Service/CheckLogin",//APP登录

        //合同
        appContList: "MobileApp/ContractList/GetList",//APP查看合同列表
        appcontractDetailFile1: "MobileApp/ContractList/AppcontractDetailFile1",//APP合同查看合同文本
        APPcontractDetail: "MobileApp/ContractList/GetIDList",//APP根据ID查看合同详情
        appcontractDetailFile2: "MobileApp/ContractList/AppcontractDetailFile2",//APP查看合同附件查询
        appcontractDetailAFinance: "MobileApp/ContractList/AppcontractDetailAFinance",//APP查看计划资金
        appfinanceActualList: "MobileApp/ContractList/AppfinanceActualList",//APP查看实际资金
        appcontractDetailFinanceStat: "MobileApp/ContractList/AppcontractDetailFinanceStat",// APP查看资金统计
        appcontractDetailRemark: "MobileApp/ContractList/AppcontractDetailRemark",//App获取合同备忘录
        appcontractDetailWorkflow: "MobileApp/ContractList/AppcontractDetailWorkflow", //App获取合同审批记录信息
        //APP发票
        appfinanceInvoiceList: "MobileApp/APPInvoiceList/APPfinanceInvoiceList",//APP发票查看
        aPPfinanceInvoiceDert: "MobileApp/APPInvoiceList/APPfinanceInvoiceDert",//APP发票查看
        aPPfinanceInvoiceCONT: "MobileApp/APPInvoiceList/APPfinanceInvoiceCONT",//APP发票查看
        //APP审批
        APPworkflowlist: "MobileApp/APPworkflowlist/workflowlist",//APP审批
        APPsubmitOpinion: "MobileApp/WorkInfo/SubmitAgreeOption", //APP提交审批
        APPSompanylist: "MobileApp/companyList/GetList", //  客户/供应商=
        companyDetail: "MobileApp/companyList/GetIDList",//  app根据id查看客户/供应商的详情
        companyDetailContact: "MobileApp/companyList/companyDetailContact",// 客户联系人
        //app提醒
        APPremindList: "MobileApp/RemindService/GetList",
        APPdeleteRemindById: "MobileApp/RemindService/DeeleteRemin",//删除单条提醒
        APPsetRemindReadedById:"MobileApp/RemindService/UpdateDeeleteAll",//修改提醒单条
        APPdeleteAllRemind: "MobileApp/RemindService/DeeleteAll",
        APPsetAllRemindReaded: "MobileApp/RemindService/UpdatedesAll",
        //客户附件
        companyDetailFile: "MobileApp/companyList/Company_accessory",
        //    category: "MobileApp/Service.aspx?cmd=category",
       

        //合同对方
        //    companyList: "MobileApp/Service.aspx?cmd=companyList", //合同列表

        companySelectType: "MobileApp/Service.aspx?cmd=companySelectType",
        companySelectLevel: "MobileApp/Service.aspx?cmd=companySelectLevel",
        companySelectCLevel: "MobileApp/Service.aspx?cmd=companySelectCLevel",
        companySelectFile: "MobileApp/Service.aspx?cmd=companySelectFile",

        companyUploadFile: "MobileApp/Service.aspx?cmd=companyUploadFile",
        companyAdd: "MobileApp/Service.aspx?cmd=companyAdd",

        //  companyDetail: "MobileApp/companyList/GetIDList",

        companyDetailFinance: "MobileApp/Service.aspx?cmd=companyDetailFinance",
        companyDetailRemark: "MobileApp/Service.aspx?cmd=companyDetailRemark",

        companyAddContact: "MobileApp/Service.aspx?cmd=companyAddContact",
        companyAddFile: "MobileApp/Service.aspx?cmd=companyAddFile",
        companyAddRemark: "MobileApp/Service.aspx?cmd=companyAddRemark",
        companyChangeState: "MobileApp/Service.aspx?cmd=companyChangeState",

        //合同
        contractList: "MobileApp/Service.aspx?cmd=contractList",

        contractSelectType: "MobileApp/Service.aspx?cmd=contractSelectType",
        contractSelectMainDept: "MobileApp/Service.aspx?cmd=contractSelectMainDept",
        contractSelectDept: "MobileApp/Service.aspx?cmd=contractSelectDept",
        contractSelectFile1: "MobileApp/Service.aspx?cmd=contractSelectFile1",
        contractSelectFile2: "MobileApp/Service.aspx?cmd=contractSelectFile2",
        contractSelectTemplate: "MobileApp/ContractTextService.aspx?cmd=contractSelectTemplate", //根据合同Id获取匹配模板
        contractGetCustomValues: "MobileApp/ContractTextService.aspx?cmd=contractGetCustomValues", //根据合同Id获取匹配模板

        contractUploadFile: "MobileApp/Service.aspx?cmd=contractUploadFile",
        contractAdd: "MobileApp/Service.aspx?cmd=contractAdd",

        contractDetail: "MobileApp/Service.aspx?cmd=contractDetail",
        contractDetailFile1: "MobileApp/Service.aspx?cmd=contractDetailFile1",
        contractDetailFile1Draft: "MobileApp/ContractTextService.aspx?cmd=contractCreateContractText", //合同文本快速起草
        contractDetailFile2: "MobileApp/Service.aspx?cmd=contractDetailFile2",
        contractDetailFinance: "MobileApp/Service.aspx?cmd=contractDetailFinance",
        contractDetailAFinance: "MobileApp/Service.aspx?cmd=contractDetailAFinance",
        contractDetailFinanceStat: "MobileApp/Service.aspx?cmd=contractDetailFinanceStat",
        contractDetailRemark: "MobileApp/Service.aspx?cmd=contractDetailRemark",
        contractDetailWorkflow: "MobileApp/Service.aspx?cmd=contractDetailWorkflow",

        contractAddFile1: "MobileApp/Service.aspx?cmd=contractAddFile1",
        contractAddFile2: "MobileApp/Service.aspx?cmd=contractAddFile2",
        contractAddRemark: "MobileApp/Service.aspx?cmd=contractAddRemark",
        contractChangeState: "MobileApp/Service.aspx?cmd=contractChangeState",
        contractCommitWorkflow: "MobileApp/WorkFlowService.aspx?cmd=contractCommitWorkflow",

        //设计变更单
        designChangeList: "MobileApp/Service.aspx?cmd=designChangeList",
        designChangeDetail: "MobileApp/Service.aspx?cmd=designChangeDetail",
        designChangeDetailFiles: "MobileApp/Service.aspx?cmd=designChangeDetailFiles",
        designChangeDetailWorkflow: "MobileApp/Service.aspx?cmd=designChangeDetailWorkflow",
        designChangeState: "MobileApp/Service.aspx?cmd=designChangeState",

        //设计变更费用签证单
        designCostList: "MobileApp/Service.aspx?cmd=designCostList",
        designCostDetail: "MobileApp/Service.aspx?cmd=designCostDetail",
        designCostDetailFiles: "MobileApp/Service.aspx?cmd=designCostDetailFiles",
        designCostDetailWorkflow: "MobileApp/Service.aspx?cmd=designCostDetailWorkflow",
        designCostState: "MobileApp/Service.aspx?cmd=designCostState",

        //合同外委托单
        outSourcedList: "MobileApp/Service.aspx?cmd=outSourcedList",
        outSourcedDetail: "MobileApp/Service.aspx?cmd=outSourcedDetail",
        outSourcedDetailFiles: "MobileApp/Service.aspx?cmd=outSourcedDetailFiles",
        outSourcedDetailWorkflow: "MobileApp/Service.aspx?cmd=outSourcedDetailWorkflow",
        outSourcedState: "MobileApp/Service.aspx?cmd=outSourcedState",

        //合同外委托费用签证单
        outCostList: "MobileApp/Service.aspx?cmd=outCostList",
        outCostDetail: "MobileApp/Service.aspx?cmd=outCostDetail",
        outCostDetailFiles: "MobileApp/Service.aspx?cmd=outCostDetailFiles",
        outCostDetailWorkflow: "MobileApp/Service.aspx?cmd=outCostDetailWorkflow",
        outCostState: "MobileApp/Service.aspx?cmd=outCostState",

        //计划资金
        APPfinance_planlist: "MobileApp/finance_plan/GetMainList", 
              //  APPSompanylist: "MobileApp/companyList/GetList", //  客户/供应商
        //APPfinance_planlist: "MobileApp/finance_plan/GetMainList", // 收款计划
     //   financePlanList: "MobileApp/Service.aspx?cmd=financePlanList",

        //实际资金
        financeActualList: "MobileApp/Finance_actual/GetMainList",
        //根据ID实际资金基本信息
        financeActua_XX: "MobileApp/Finance_actual/XJZJ_XQ",
        //根据ID合同基本信息
        financeActua_HT: "MobileApp/Finance_actual/XJZJ_HT",
        //根据id获取计划资金
        financeActua_JH:"MobileApp/Finance_actual/XJZJ_JHZJ",
       // financeActualList: "MobileApp/Service.aspx?cmd=financeActualList",

        //发票
        financeInvoiceList: "MobileApp/Service.aspx?cmd=financeInvoiceList",

        //审批
        workflowlist: "MobileApp/WorkFlowService.aspx?cmd=workflowlist",
        submitOpinion: "MobileApp/WorkFlowService.aspx?cmd=SubmitOpinion",

        //提醒
        remindList: "MobileApp/RemindService.aspx?cmd=remindList",
        setAllRemindReaded: "MobileApp/RemindService.aspx?cmd=setAllRemindReaded",
        setRemindReadedById: "MobileApp/RemindService.aspx?cmd=setRemindReadedById",
        deleteAllRemind: "MobileApp/RemindService.aspx?cmd=deleteAllRemind",
        deleteRemindById: "MobileApp/RemindService.aspx?cmd=deleteRemindById",

        //推送服务
        regDeviceToken: "MobileApp/RemindService.aspx?cmd=regDeviceToken",
        getPushState: "MobileApp/RemindService.aspx?cmd=getPushState",
        changePushState: "MobileApp/RemindService.aspx?cmd=changePushState",

        //权限
        permission: "MobileApp/Service.aspx?cmd=permission",

        baidu: "http://www.baidu.com"
    },

    /**
     * 初始化全局配置
     */
    globalInitialize: function () {
        this._setDefaultTheme();
        //this._addBackButton();
        this._solvePageTrans();
        this._setDefaultConfig();
        //获取提醒信息
        woo.bll.getRemind();
        setInterval(woo.bll.getRemind, 60000); //隔一段时间查找一次提醒信息
    },

    /**
     * 解决页面切换空白闪烁
     */
    _solvePageTrans: function () {
        $.extend($.mobile, {
            defaultPageTransition: 'none'
        });

        $.mobile.defaultPageTransition = 'none';
        $.mobile.defaultDialogTransition = 'none';
    },

    /**
     * 设置默认样式
     */
    _setDefaultTheme: function () {
        $.mobile.page.prototype.options.headerTheme = "b";
        $.mobile.page.prototype.options.footerTheme = "b";
        //$.mobile.page.prototype.options.backBtnTheme = "b";
    },

    /**
     * 加入返回按钮
     */
    _addBackButton: function () {
        $.mobile.page.prototype.options.addBackBtn = true;
        $.mobile.page.prototype.options.backBtnText = "返回";
    },

    /**
     * 设置默认属性
     */
    _setDefaultConfig: function () {
        $.mobile.selectmenu.prototype.options.nativeMenu = false;  //取消select设备默认行为
        $.mobile.listview.prototype.options.filterPlaceholder = "搜索..."; //设置默认的listview filter placeholder文字

        $.mobile.loadingMessage = "载入中";
        $.mobile.pageLoadErrorMessage = "载入错误";
    },

    /**
     * 设置日期输入框
     * @param type 默认date, 取值date, datetime, time, select
     */
    setDatetimeInput: function (type) {
        //mobiscroll 配置
        var opt = {
            date: { preset: 'date', dateFormat: 'yyyy-mm-dd' },
            datetime: { preset: 'datetime', dateFormat: 'yyyy-mm-dd', timeFormat: 'HH:ii' },
            time: { preset: 'time' },
            select: { preset: 'select' }
        };
        var _type = "date";
        if (typeof type != "undefined") {
            _type = type;
        }
        //$('.dt2').scroller('destroy').scroller($.extend(opt[_type],{ theme: "default", mode: "Scroller", display: "bubble", lang: "zh" }));

        $(".dt").mobiscroll().date({
            dateFormat: 'yyyy-mm-dd',
            display: "bottom",
            mode: "scroller",
            endYear: 2100,
            lang: "zh"
        });
    },

    /**
     * 渲染页面所有元件
     */
    renderAll: function () {
        $.mobile.pageContainer.trigger("create");
    },

    /**
     * 根据url和参数名称获取参数值
     * @param param 参数名称
     * @param url url
     * @returns {Array|{index: number, input: string}|*|string}
     */
    
    getParameterByName: function (param, url) {
        var match = RegExp('[?&]' + param + '=([^&]*)').exec(url);
        return match && decodeURIComponent(match[1].replace(/\+/g, ' '));
    },

    /**
     * 设置本地存储数据 LocalStorage
     * @param key
     * @param value
     */
    setData: function (key, value) {
        if (window.localStorage) {
            localStorage.setItem(key, value);
            console.log("setData:key=" + key + ";value=" + value);
        } else {
            throw "不支持LocalStorage本地存储";
        }
    },

    /**
     * 获取本地存储数据 LocalStorage
     * @param key
     * @returns {*}
     */
    getData: function (key) {
        if (window.localStorage) {
            var value = localStorage.getItem(key);
            console.log("getData:key=" + key + ";value=" + value);
            return value;
        } else {
            throw "不支持LocalStorage本地存储";
        }
    },


    /**
     * 删除本地存储数据 LocalStorage
     * @param key
     * @param value
     */
    deleteData: function (key) {
        if (window.localStorage) {
            localStorage.removeItem(key);
            console.log("deleteData:key=" + key);
        } else {
            throw "不支持LocalStorage本地存储";
        }
    },

    /**
     * 去除前后字符
     * @param str
     * @param chr
     * @returns {XML|string|*|void}
     */
    trim: function (str, chr) {
        var rgxtrim = (!chr) ? new RegExp('^\\s+|\\s+$', 'g') : new RegExp('^' + chr + '+|' + chr + '+$', 'g');
        return str.replace(rgxtrim, '');
    },

    /**
     * 去除最后字符
     * @param str
     * @param chr
     * @returns {XML|string|*|void}
     */
    trimEnd: function (str, chr) {
        var rgxtrim = (!chr) ? new RegExp('\\s+$') : new RegExp(chr + '+$');
        return str.replace(rgxtrim, '');
    },

    /**
     * 去除最前字符
     * @param str
     * @param chr
     * @returns {XML|string|*|void}
     */
    trimBegin: function (str, chr) {
        var rgxtrim = (!chr) ? new RegExp('^\\s+') : new RegExp('^' + chr + '+');
        return str.replace(rgxtrim, '');
    },

    /**
     * 判断变量是否是数字
     * @param n
     * @returns {boolean}
     */
    isNumber: function (n) {
        return !isNaN(parseFloat(n)) && isFinite(n);
    },

    /**
     * 格式化
     * Short, fast, flexible yet standalone. Only 75 lines including MIT license info, blank lines & comments.
     * Accept standard number formatting like #,##0.00 or with negation -000.####.
     * Accept any country format like # ##0,00, #,###.##, #'###.## or any type of non-numbering symbol.
     * Accept any numbers of digit grouping. #,##,#0.000 or #,###0.## are all valid.
     * Accept any redundant/fool-proof formatting. ##,###,##.# or 0#,#00#.###0# are all OK.
     * Auto number rounding.
     * Simple interface, just supply mask & value like this: format( "0.0000", 3.141592)
     * @param b 格式
     * @param a 金额
     * @returns {string}
     */
    formatMoney: function (b, a) {
        if (!b || isNaN(+a)) return a;
        var a = b.charAt(0) == "-" ? -a : +a, j = a < 0 ? a = -a : 0, e = b.match(/[^\d\-\+#]/g), h = e && e[e.length - 1] || ".", e = e && e[1] && e[0] || ",", b = b.split(h), a = a.toFixed(b[1] && b[1].length), a = +a + "", d = b[1] && b[1].lastIndexOf("0"), c = a.split(".");
        if (!c[1] || c[1] && c[1].length <= d) a = (+a).toFixed(d + 1);
        d = b[0].split(e);
        b[0] = d.join("");
        var f = b[0] && b[0].indexOf("0");
        if (f > -1) for (; c[0].length < b[0].length - f;)c[0] = "0" + c[0]; else +c[0] == 0 && (c[0] = "");
        a = a.split(".");
        a[0] = c[0];
        if (c = d[1] && d[d.length -
            1].length) {
            for (var d = a[0], f = "", k = d.length % c, g = 0, i = d.length; g < i; g++)f += d.charAt(g), !((g - k + 1) % c) && g < i - c && (f += e);
            a[0] = f
        }
        a[1] = b[1] && a[1] ? h + a[1] : "";
        return (j ? "-" : "") + a[0] + a[1]
    },

    /**
    /** /Date(1354116249000)/ 转成时间格式
    **/
    GetDateFormat: function (str) {
        return new Date(parseInt(str.substr(6, 13))).toLocaleDateString();
    }
};

/**
 * 界面常用的方法
 * @type {{}}
 */
woo.ui = {

    /**
     * 清除所有的popup
     */
    clearPopup: function () {
        $("[data-role=popup]").popup("close");
    }

};

/**
 * 业务逻辑
 * @type {{isLogin: Function}}
 */
woo.bll = {

    /**
     * 身份信息处理(为了安全起见，系统每进入一次欢迎页，会判断一次登录，判断会和服务器通信，而不是直接取woo.getData("islogin"))
     * 如果登录信息不存在，则根据存储的登录信息重新登录，否则跳转到登陆页面
     * 在非欢迎页判断是否登录，可以直接取woo.getData("islogin")
     *
     * 注意：$.ajax跨域时不响应timeout错误，此处使用jsonp插件
     * http://stackoverflow.com/questions/1002367/jquery-ajax-jsonp-ignores-a-timeout-and-doesnt-fire-the-error-event
     * https://github.com/jaubourg/jquery-jsonp/blob/master/doc/API.md
     */
    Login: function () {

        if (woo.getData("systemurl") !== null) {
            $.jsonp({
                url: woo.getData("systemurl") + woo.url.login,
                cache: "false",
                callbackParameter: "callback",
                timeout: 10000,
                dataType: "jsonp",
                data: {
                    username: woo.getData("username"),
                    password: woo.getData("password")
                },
                success: function (response) {
                    if (response.success) {
                        console.log("登录成功!");
                        woo.setData("userId", response.msg);
                        woo.setData("islogin", "true");

                        var device = null;
                        //ios登记deviceToken
                        if (device && device.platform && (device.platform == 'iOS' || device.platform == 'IOS' || device.platform == 'ios')) {
                            try {
                                var pushNotification = window.plugins.pushNotification;
                                pushNotification.registerDevice({ alert: true, badge: true, sound: true }, function (status) {
                                    woo.setData("deviceToken", status.deviceToken);

                                    $.ajax({
                                        url: woo.getData("systemurl") + woo.url.regDeviceToken,
                                        type: "POST",
                                        dataType: "jsonp",
                                        crossDomain: "true",
                                        data: {
                                            deviceToken: woo.getData("deviceToken"),
                                            userId: woo.getData("userId")
                                        }
                                    }).done(function (response) {
                                        console.log("IOS设备注册成功:" + woo.getData("deviceToken") + response.msg);
                                    }).fail(function () {
                                        console.log("Page #regDeviceToken: 注册DeviceToken失败!");
                                    });
                                });
                            }
                            catch (e) {
                                alert("获取设备标识失败，可能会影响信息推送。");
                                console.log(e);
                            }
                        }

                        $.mobile.changePage("#index", "fade");
                    } else {
                        console.log("登录失败!");
                        woo.setData("islogin", "false");
                        $.mobile.changePage("#login", "fade");
                    }
                },
                error: function () {
                    console.log("登录地址访问失败!");
                    woo.setData("islogin", "false");
                    $.mobile.changePage("#login", "fade");
                }
            });
        } else {
            $.mobile.changePage("#login", "fade");
        }
    },


    /**
     * 获取提醒信息
     */
    getRemind: function () {
        try {
            var dataUrl = woo.getData("systemurl") + woo.url.APPremindList;
            $.ajax({
                url: dataUrl,
                type: "POST",
                dataType: "jsonp",
                crossDomain: "true",
                data: {
                    userId: woo.getData("userId"),
                }
            }).done(function (response) {
                //console.log(response);
                var unReadCount = 0;

                var listview = $("#remind_listview");
                if (listview.length > 0) {
                    var html = "";
                    if (response) {
                        $.each(response.items, function (i, item) {
                            if (item.state == "0") {
                                unReadCount++;
                            }
                            html += "<li class='" + (item.state == "0" ? "unread" : "readed") + "'>" +
                                "<a href='#'><h2 dataid='" + item.Uuid + "'>时间：" + item.date + "<br><span>" + item.info + "</span></h2></a>" +
                                '<a href="#" class="delete" dataid="' + item.Uuid + '">删除</a>' +
                                '</li>';
                        });
                    }

                     listview.html(html).listview("refresh");

                    //点击内容标记为已读
                    listview.find("h2").click(function () {

                        //TODO:点击提醒列表跳转

                        $.ajax({
                            url: woo.getData("systemurl") + woo.url.APPsetRemindReadedById,
                            type: "POST",
                            dataType: "jsonp",
                            crossDomain: "true",
                            data: {
                                remindId: $(this).attr("dataid"),
                                userId: woo.getData("userId")
                            }
                            }).done(function () {
                            woo.bll.getRemind();

                        }).fail(function () {
                            console.log("标记为已读失败!");
                            });
                        woo.bll.getRemind();
                    });

                    //点击按钮删除
                    listview.find("a.delete").click(function () {
                        $.ajax({
                            url: woo.getData("systemurl") + woo.url.APPdeleteRemindById,
                            type: "POST",
                            dataType: "jsonp",
                            crossDomain: "true",
                            data: {
                                remindId: $(this).attr("dataid"),
                                userId: woo.getData("userId"),
                            }
                        }).done(function () {
                            woo.bll.getRemind();

                        }).fail(function () {
                            console.log("删除失败!");
                            });
                        woo.bll.getRemind();
                    });

                }
                else {
                    if (response) {
                        $.each(response.items, function (i, item) {
                            if (item.state == "0") {
                                unReadCount++;
                            }
                        });
                    }
                }

                woo.bll.setRemindBubble(unReadCount);

            }).fail(function () {
                console.log(businessTypeDisplayName + "提醒列表访问失败!");
            });

        } catch (e) {
            console.log(e);
        }
    },

    /**
     * 设置提醒气泡
     * @param count 未读提醒数量，默认取localStorage["unReadRemindCount"]
     */
    setRemindBubble: function (count) {

        var unReadRemindCount = woo.getData("unReadRemindCount") || 0;
        if (typeof count !== 'undefined') {
            //判断count是否是数字
            unReadRemindCount = (woo.isNumber(count) ? count : unReadRemindCount);
        }
        console.log("unReadRemindCount:" + unReadRemindCount);
        var remindMenu = $("#menu_remind");
        remindMenu.find(".bubblecount").remove();

        if (unReadRemindCount > 0) {
            remindMenu.append('<span class="bubblecount">' + unReadRemindCount + '</span>');
        }
    }


};
