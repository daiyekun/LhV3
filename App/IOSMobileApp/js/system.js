/******************
 * Woorich Dev Team
 * By: Jnoodle
 *
 * 设置页
 *
 ******************/

woo.globalInitialize();

/***************************
 * 激活PhoneGap
 **************************/

document.addEventListener('deviceready', function () {
    var serviceToggle = $("#toggleService");

    //ios提醒
    if (device.platform == 'iOS' || device.platform == 'IOS' || device.platform == 'ios') {
        serviceToggle.show();

        $("#exit").hide();

        //获取服务器上推送是否开启信息
        $.ajax({
            url: woo.getData("systemurl") + woo.url.getPushState,
            type: "POST",
            dataType: "jsonp",
            crossDomain: "true",
            data: {
                deviceToken: woo.getData("deviceToken")
            }
        }).done(function (response) {
                if (response.msg == "true") {
                    serviceToggle.val("on").slider('refresh');
                } else {
                    serviceToggle.val("off").slider('refresh');
                }
            }).fail(function () {
                console.log("Page #serviceToggle: 推送服务状态获取失败!");
                serviceToggle.val("off").slider('refresh');
            });

        serviceToggle.off("change").on("change", function () {

            if ($(this).val() == "on") {
                $.ajax({
                    url: woo.getData("systemurl") + woo.url.changePushState,
                    type: "POST",
                    dataType: "jsonp",
                    crossDomain: "true",
                    data: {
                        deviceToken: woo.getData("deviceToken"),
                        state: "true"
                    }
                }).done(function (response) {
                        serviceToggle.val("on").slider('refresh');
                    }).fail(function () {
                        serviceToggle.val("off").slider('refresh');
                    });
            } else {
                $.ajax({
                    url: woo.getData("systemurl") + woo.url.changePushState,
                    type: "POST",
                    dataType: "jsonp",
                    crossDomain: "true",
                    data: {
                        deviceToken: woo.getData("deviceToken"),
                        state: "false"
                    }
                }).done(function (response) {
                    }).fail(function () {
                        serviceToggle.val("on").slider('refresh');
                    });
            }
        });
    }

    //安卓提醒
    if (device.platform == 'android' || device.platform == 'Android') {

        serviceToggle.show();

        $("#about_device_info").html('设备名称: ' + device.name + '<br />' +
            '设备平台: ' + device.platform + '<br />' +
            'UUID: ' + device.uuid + '<br />' +
            '版本: ' + device.version + '<br />');

        var myService = cordova.require('cordova/plugin/myService');

        /**
         * 获取状态
         */
        function getStatus() {
            myService.getStatus(
                function (r) {

                    console.log("系统服务：" + r.ServiceRunning)

                    if (r.ServiceRunning) {
                        serviceToggle.val("on").slider('refresh');
                    } else {
                        serviceToggle.val("off").slider('refresh');
                    }

                    serviceToggle.off("change").on("change", function () {

                        if ($(this).val() == "on") {
                            if (!r.ServiceRunning) {
                                startService();
                            }
                        } else {
                            if (r.ServiceRunning) {
                                stopService();
                            }
                        }
                    });
                },
                function (e) {
                    console.log('!!!!!!!!!!!! An error has occurred in getStatus')
                }
            );
        };

        /**
         * Android启动后台服务
         */
        function startService() {
            myService.getStatus(
                function (r) {
                    if (r.ServiceRunning)
                        enableTimer(r);
                    else
                        myService.startService(function (d) {
                                enableTimer(d);
                            },
                            function (e) {
                                console.log('!!!!!!!!!!!! An error has occurred in startService')
                            });
                },
                function (e) {
                    console.log('!!!!!!!!!!!! An error has occurred in getStatus')
                }
            );
        }

        /**
         * Android停止后台服务
         */
        function stopService() {

            myService.getStatus(
                function (r) {
                    if (r.ServiceRunning) {
                        myService.stopService(function (d) {
                                console.log("系统服务停止");
                            },
                            function (e) {
                                handleError(e)
                            });
                    }
                },
                function (e) {
                    console.log('!!!!!!!!!!!! An error has occurred in getStatus')
                }
            );

        }

        /**
         * Android启动后台服务定时器
         */
        function enableTimer(data) {
            if (data.TimerEnabled)
                registerForBootStart(data);
            else
                myService.enableTimer(300000,
                    function (r) {
                        registerForBootStart(r)
                    },
                    function (e) {
                        console.log('!!!!!!!!!!!! An error has occurred in enableTimer')
                    }
                );
        }

        /**
         * Android注册启动服务
         * @param data
         */
        function registerForBootStart(data) {
            if (data.RegisteredForBootStart) {
                console.log('!!!!!!!!!!!! Service started, timer enabled and service registered for Boot start');
            }
            else {
                myService.registerForBootStart(
                    function (r) {
                        console.log('!!!!!!!!!!!! Service started, timer enabled and service registered for Boot start')
                    },
                    function (e) {
                        console.log('!!!!!!!!!!!! An error has occurred in registerForBootStart')
                    }
                );
            }
            setConfig();
        }

        /**
         * 设置：用户Id
         */
        function setConfig() {
            if (woo.getData("userId") !== null) {
                var config = {
                    "userId": woo.getData("userId"),
                    "systemUrl": woo.getData("systemurl")
                    //注意，如果改变登录用户或地址之后，需要重新启动一下服务，才可以激活配置
                };

                console.log("设置用户Id：" + config.userId);
                myService.setConfiguration(config,
                    function (r) {
                    },
                    function (e) {
                        console.log("Error: " + e.ErrorMessage);
                    });
            }
        }

        getStatus();

    } else {
        serviceToggle.hide();
    }

}, true);


$(document).on("pageinit", "#index", function () {
    //退出
    $("#exit").click(function () {
        navigator.app.exitApp();
    });

});

$(document).on("pageshow", "#index", function () {
    $("#sign").attr("src", woo.getData("signature"));
});

//document.addEventListener("deviceready", indexInitialize, true);
$(document).on("pageinit", "#lock", function () {

    $("#lockform").validate();

    $("#lockform_submit").click(function () {
        var v = $("#lockform").validate();
        if (v.form()) {
            woo.setData("lock", $('[name=lockpassword]').val());
        }
    });

    //清除
    $("#lockclear").click(function () {
        woo.deleteData("lock");
        $.mobile.changePage("#index");
    });
});

$(document).on("pageinit", "#signature", function () {

    var canvas = document.getElementById("signsheet");

//    function resizeCanvas() {
//        var ratio = window.devicePixelRatio || 1;
//        canvas.width = canvas.offsetWidth * ratio;
//        canvas.height = canvas.offsetHeight * ratio;
//        canvas.getContext("2d").scale(ratio, ratio);
//    }
//
//    window.onresize = resizeCanvas;
//    resizeCanvas();

    canvas.getContext("2d").scale(1, 1);
    var signaturePad = new SignaturePad(canvas);
    signaturePad.fromDataURL(woo.getData("signature"));

    $("#savesignature").click(function () {
        woo.setData("signature", signaturePad.toDataURL());
        $.mobile.changePage("#index");
    });

    $("#clearsignature").click(function () {
        signaturePad.clear();
        signaturePad.fromDataURL("");
    });

//// Returns signature image as data URL
//    signaturePad.toDataURL();
//
//// Draws signature image from data URL
//    signaturePad.fromDataURL("data:image/png;base64,iVBORw0K...");
//
//// Clears the canvas
//    signaturePad.clear();
//
//// Returns true if canvas is empty, otherwise returns false
//    signaturePad.isEmpty();
});

$(document).on("pageinit", "#checkupdate", function () {

    //TODO:从服务器检查版本

    var updateVersion = function () {
        $('#checkupdate [data-role=content]').html('<p>您使用的版本是最新的。</p>');
    };
    setTimeout(updateVersion, 2000);
});


