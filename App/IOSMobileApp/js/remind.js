/******************
 * Woorich Dev Team
 * By: Jnoodle
 *
 * 提醒页
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


/***********************************************************************************/


/***************************
 * 列表页（默认）
 **************************/

$(document).on("pageinit", "#index", function () {

    //全部设置为已读
    $("#readAll").click(function (e) {

        e.preventDefault();
        $.ajax({
            url: sysUrl + woo.url.APPsetAllRemindReaded,
            type: "POST", 
            dataType: "jsonp",
            crossDomain: "true"
        }).done(function () {
                woo.bll.getRemind();

            }).fail(function () {
                console.log("全部设置为已读失败!");
            });
        woo.bll.getRemind();
    });

    //全部删除
    $("#deleteAll").click(function (e) {
        e.preventDefault();
        $.ajax({
            url: sysUrl + woo.url.APPdeleteAllRemind,
            type: "POST",
            dataType: "jsonp",
            crossDomain: "true",
            data: {
                userId: woo.getData("userId"),
            }
        }).done(function () {
                woo.bll.getRemind();

            }).fail(function () {
                console.log("全部删除失败!");
            });
        woo.bll.getRemind();
    });

});