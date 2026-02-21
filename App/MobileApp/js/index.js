/******************
 * Woorich Dev Team
 * By: Jnoodle
 *
 * 系统首页
 *
 ******************/

woo.globalInitialize();

/**
 * 首页初始化
 */
var indexInitialize = function () {

    //隐藏欢迎页，并根据登录信息跳转页面
    var hideWelcomePage = function () {
        try {
            if (woo.getData("islogin") === null || woo.getData("islogin") === "false") {
                $.mobile.changePage("#login", "fade");
            } else {
                woo.bll.Login();
            }
        } catch (e) {
            $.mobile.changePage("#login", "fade");
        }
    };

    //跳转到解锁页
    var jumpToLock = function () {
        $.mobile.changePage("#lock", "fade");
    };

    //欢迎页面
    $(document).on("pageshow", "#welcome", function () {

        //密码锁
        if (woo.getData("lock") !== null) {
            setTimeout(jumpToLock, 2000);  //展示欢迎页
        } else {

            //TODO:判断网络是否连接
            setTimeout(hideWelcomePage, 2000);  //展示欢迎页
        }
    });

    //密码锁页面
    $(document).on("pageinit", "#lock", function () {

        $("#lockform_submit").click(function () {
            if ($('[name=lockpassword]').val() === woo.getData("lock")) {
                hideWelcomePage();
            } else {
                $("#lockform_error").popup("open");
            }
            return false;
        });
    });

    //login页面载入时
    $(document).on("pageshow", "#login", function () {


        //初始化登录信息，用于系统设置--修改登录信息
        if (woo.getData("systemurl") !== null) {
            $("#loginform").find('[name=username]').val(woo.getData("username"));
            $("#loginform").find('[name=password]').val(woo.getData("password"));
            $("#loginform").find('[name=systemurl]').val(woo.getData("systemurl"));
            $("#loginform").find('[name=issave]').val(woo.getData("issave"));
        }

        //来源页面
        var from = woo.getParameterByName("from", location.href);

        $("#loginform_info").html("请您输入您在EBP系统中的登录信息。");

        $('#loginform_submit').click(function () {

            var validator = $("#loginform").validate();  //表单验证

            if (validator.form()) {

                $("#loginform_info").html("登录中，请稍后……");

                //处理用户输入的登录地址：处理http://和/字符
                var systemurl = $.trim($('[name=systemurl]').val());

//                if (systemurl.indexOf("http://") < 0) {
//                    systemurl = "http://" + systemurl;
//                }
//                systemurl = woo.trimEnd(systemurl, "/") + "/";

                $('[name=systemurl]').val(systemurl);
                var loginUrl = systemurl + woo.url.applogin;

                //登录失败处理
                var loginFailure = function () {
                    woo.setData("islogin", "false");
                    $("#loginform_info").html("登录失败，可能是由于填写信息错误、移动端未启用或者网络故障。");
                };

                //登录表单提交
                $.ajax({
                    url: loginUrl,
                    type: "POST",
                    dataType: "jsonp",
                    crossDomain: "true",
                    data: $('#loginform').serialize()
                }).done(function (response) {
                    if (response.success) {
                            console.log("Page #login: 登录成功!");
                            woo.setData("userId", response.msg);

                            $("#loginform_info").html("登录成功！");
                            if (true || $('[name=issave]').val() === "true") {
                                woo.setData("systemurl", systemurl);
                                woo.setData("username", $('[name=username]').val());
                                woo.setData("password", $('[name=password]').val());
                                woo.setData("islogin", "true");
                            }

                            //判断是登录还是修改登录信息
                            if (from !== "system") {
                                //$.mobile.changePage("#index", "fade"); //跳转到首页
                                window.location.href = "index.html#index";
                            } else {
                                //系统设置--修改登录信息
                                //$.mobile.changePage("system.html", "fade"); //跳转到系统设置
                                window.location.href = "system.html";
                            }
                        } else {
                            console.log("Page #login: 登录信息错误!");
                            loginFailure();
                        }
                    }).fail(function () {
                        console.log("Page #login: 登录地址访问失败!");
                        loginFailure();
                    });

                return false;
            } else {
                return false;
            }
        });
    });

    //index页面载入时
    $(document).on("pageshow", "#index", function () {

            woo.bll.Login();

            $("#index_username").text(woo.getData("username"));

            //ios登记deviceToken
            if (device.platform == 'iOS' || device.platform == 'IOS' || device.platform == 'ios') {
                try {
                    var pushNotification = window.plugins.pushNotification;
                    pushNotification.registerDevice({alert: true, badge: true, sound: true}, function (status) {
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

            //退出
            $("#exitsystem").click(function () {
                navigator.app.exitApp();
            });
        }
    );
};


/***************************
 * 激活PhoneGap
 **************************/

document.addEventListener("deviceready", function () {
    console.log("onDeviceReady");


//
//    try {
//        var pushNotification;
//
//        pushNotification = window.plugins.pushNotification;
//
//        if (device.platform == 'iOS' || device.platform == 'IOS' || device.platform == 'ios') {
//            pushNotification.register(tokenHandler, errorHandler,
//                {"badge": "true", "sound": "true", "alert": "true", "ecb": "onNotificationAPN"});
//        }
//
//        // iOS
//        function tokenHandler(result) {
//            // Your iOS push server needs to know the token before it can push to this device
//            // here is where you might want to send it the token for later use.
//            alert('device token = ' + result)
//        }
//
//        // iOS
//        function onNotificationAPN(event) {
//            if (event.alert) {
//                navigator.notification.alert(event.alert);
//            }
//
//            if (event.sound) {
//                var snd = new Media(event.sound);
//                snd.play();
//            }
//
//            if (event.badge) {
//                pushNotification.setApplicationIconBadgeNumber(successHandler, errorHandler, event.badge);
//            }
//        }
//
//        function errorHandler(error) {
//            alert('error = ' + error)
//        }
//    } catch (e) {
//        alert(e);
//    }

}, false);

$(document).on("pageinit", function () {
    //woo.setDatetimeInput();
    indexInitialize();
});

