var ddr = $("#WxCurrUserId").val();



if (ddr == null || ddr == "") {
    $("#wxnamenull").click();
}


function LoadMainFormData(currId) {
    var $url = woowx.constant.APIBaseURL + "/api/Contract/GetCountViwe";
    $.ajax({
        type: 'GET',
        url: $url,
        data:
        {
            Id: currId
        },
        dataType: 'json',
        timeout: 10000,
        success: function (data) {
            var $data = JSON.parse(data);

            if ($data.Data != null && $data.Data != undefined) {

                $.each($data.Data, function (key, value) {
                  
                    //if (key == "HtXmnr") {
                    //    $("#" + key).text(value);
                    //} else {
                    //    $("#" + key).val(value);

                    //}
                    $("#" + key).val(value);
                });
                
                CheckWflow(currId);
                //GetOptions(currId);//意见
                GetOptions2(currId);
            }
        }, error: function (xhr, type) {
            alert('LoadMainFormData系统异常' + xhr + ":" + type + ":" + xhr.status);
        }
    });


}
//下载文件
function downloadtxt(txtId)
{
    var ext = $("#ext_" + txtId).val();
   // alert(ext);

    if (ext.indexOf("pdf") >= 0 || ext.indexOf("PDF") >= 0) {

        var fileurl = '/js/nf-plugs/pdfjs/web/viewer.html?file=' +
            encodeURIComponent(woowx.constant.APIBaseURL + '/api/Contract/GetPdf?Id=' + txtId);
        window.open(fileurl);
    } else {
        window.open(woowx.constant.APIBaseURL + "/Contract/DownLoadTxt?txtId=" + txtId);

    }
   


}
/***合同文本**/
function ShowContText(currId) {
    var $urls = woowx.constant.APIBaseURL + "/api/Contract/GetContTextViwe";
    $.ajax({///GetCountViwe
        type: 'Get',
        url: $urls,
        data:
        {
            // UserId: 1,
            Id: currId
        },

        dataType: 'json', timeout: 6000,
        success: function (data) {

            var length = data.length;
            var resultstr2 = "";
            if (length > 0) {
               
                for (var i = 0; i < length; i++) {
                    //var downloadurl = woowx.constant.APIBaseURL + "/" + data[0].path;
                    if (i == 0) {
                        resultstr2 += '<div class="page">'
                            + '<div class="page__bd textpaen-body">'
                            + '<input type="hidden" id="ext_' + data[i].id + '" value="' + data[i].extenName + '"/>'
                          
                            + '<div class="textload"><a href="javascript:;" onclick=downloadtxt(' + data[i].id + ') class="textbtn">' + data[i].name + data[i].extenName +'</a></div>'
                            + '</div>'
                            + '</div>'
                    } else {
                        //margin-top:设置条数之间间隔
                        resultstr2 += '<div class="page">'
                            + '<div class="page__bd textpaen-body">'
                            + '<input type="hidden" id="ext_' + data[i].id + '" value="' + data[i].extenName + '"/>'
                           
                            + '<div class="textload"><a href="javascript:;" onclick=downloadtxt(' + data[i].id + ') class="textbtn">' + data[i].name + data[i].extenName +'</a></div>'
                            + '</div>'
                            + '</div>'

                    }
                }
            } else {
                resultstr2 += "<span class='f-red'>没有数据</span>";

            }

            $("#ShowFile").html(resultstr2);
        }
    });
}
//判断是否可以审批
function CheckWflow(currId) {
    var $userId = $("#CurrWxUserId").val();//当前登录的用户
    var $wfitem = $("#WfItem").val();//审批事项
    var $url = woowx.constant.APIBaseURL + "/api/WorkFlow/GetFlowPermission";
    var flowPerm = {
        SpId: currId,
        WfItem: $wfitem,
        UserId: $userId,
        SpType: 3//合同
    };
    var poststr = JSON.stringify(flowPerm);
   
    $.ajax({
        type: 'POST',
        url: $url,
        data: poststr,
        dataType: 'json',
        contentType: 'application/json;charset=UTF-8',
        timeout: 10000,
        success: function (data) {
            var $data = JSON.parse(data);
            var redata = $data.Data;
            if (redata != null && redata != undefined) {

                $("#InstId").val(redata.InstId);
                $("#NodeId").val(redata.NodeId);
                $("#NodeStrId").val(redata.NodeStrId);
                if (redata.Qx === 1) {
                    $("#shenpi").removeClass("hide").addClass("show");
                }


            }
        }, error: function (xhr, type) {
            alert('CheckWflow系统异常' + xhr + ":" + type + ":" + xhr.status);
        }
    });


}
//提交审批意见
function SubmitOption(currId, OptRes, opmsg) {
    var $userId = $("#CurrWxUserId").val();//当前登录的用户
    var $wfitem = $("#WfItem").val();//审批事项
    var $url = woowx.constant.APIBaseURL + "/api/WorkFlow/SubmitAgreeOption";
    if (OptRes === 2) {//不同意
        $url = woowx.constant.APIBaseURL + "/api/WorkFlow/SubmitDisagreeOption";

    }
    var otion = {
        InstId: $("#InstId").val(),//审批实例ID
        ObjType: 3,
        ObjMoney: $("#HtJeD").val(),//合同金额
        SpType: 3,//合同
        ObjId: currId,
        Option: opmsg,
        OptRes: OptRes,
        SubmitWxId: $userId,
        DDs: "1.科室领导（点选）"
    };
    var poststr = JSON.stringify(otion);
   
    $.ajax({
        type: 'POST',
        url: $url,
        data: poststr,
        dataType: 'json',
        contentType: 'application/json;charset=UTF-8',
        timeout: 10000,
        success: function (data) {
            var $data = JSON.parse(data);
            if ($data.Code === 0) {
                //document.body.scrollTop = document.documentElement.scrollTop = 0;//回滚顶部
                getcurrappinfo($userId);
                $("#shenpi").hide();
                GetOptions2(currId);
                //window.location.href = "/Common/SuccMag";
              
                

            } else {
                window.location.href = "/Common/FailMsg";
            }
        }, error: function (xhr, type) {
            var errmsg = "错误类型：" + type + ",xhr.status:" + xhr.status
            window.location.href = "/Common/FailMsg?errmsg=" + errmsg;
          
        }
    });


}
/**
 * 查询当前审批信息
 * */

function getcurrappinfo(userId) {
    var $url = woowx.constant.APIBaseURL + "/api/WorkFlow/GetCurrentAppInfo";
    $.ajax({
        type: 'GET',
        url: $url,
        data: {
            wxcode: userId
        },
        dataType: 'json',
        timeout: 10000,
        success: function (data) {
           
            var $data = JSON.parse(data);
            $("#nextcontId").val($data.Data.NextId);
            $("#approwcount").text($data.Data.RowCount);
            if ($data.Data.RowCount > 0) {
                $("#msgcontent").removeClass("hide");
                $("#msgcontent").show();
                document.getElementById("msgcontent").scrollIntoView();
            } else {
                $.alert("您已经全部处理完毕", "提示", function () {

                });

            }
           
            
        }, error: function (xhr, type) {
            var errmsg = "错误类型：" + type + ",xhr.status:" + xhr.status
            window.location.href = "/Common/FailMsg?errmsg=" + errmsg;

        }
    });

}
function gettitleclass(zt) {
    var zcss = "";
    switch (zt) {
        default ://未审批
            break;
        case 1://审批中
            zcss = "zpzcent";
            break;
        case 2://通过
            zcss = "recent";
            break;
        case 3://打回
            zcss = "bdhcent";
            break;
          

    }
    return zcss;
    
}
//获取审批情况
function GetOptions(currId) {
    var $userId = $("#CurrWxUserId").val();//当前登录的用户

    var $wfitem = $("#WfItem").val();//审批事项
    var $url = woowx.constant.APIBaseURL + "/api/WorkFlow/GetWooFlowTime";
    var flowPerm = {
        SpId: currId,
        WfItem: $wfitem,
        UserId: $userId,
        SpType: 3//合同
    };
   
    var poststr = JSON.stringify(flowPerm);

    $.ajax({
        type: 'POST',
        url: $url,
        data: poststr,
        dataType: 'json',
        contentType: 'application/json;charset=UTF-8',
        timeout: 10000,
        success: function (data) {
            var $data = JSON.parse(data);
            var redata = $data.Data;
            var instzt = 0;//审批实例状态
            var length = redata.length;
            if (redata != null && redata != undefined && length > 0) {
                var timestr = '<ul>';
                var titleclass = "";
                for (var i = 0; i < length; i++) {
                    titleclass = gettitleclass(redata[i].Tstate); //redata[i].Tstate > 0 ? "recent" : "";
                    if (redata[i].Name == "结束") {
                        timestr += '<li class="timeline-item">'
                        if (redata[i].Tstate > 0) {
                            timestr += '<div class="timeline-item-color timeline-item-head-first">'
                            timestr += '<i class="timeline-item-checked   weui-icon-success-no-circle"></i>'
                            timestr += '</div>'

                        } else {
                            timestr += '<div class="timeline-item-color timeline-item-head"></div>'
          
              }
                        timestr += '<div class="timeline-item-tail hide"></div>'
                        timestr += '<div class="timeline-item-content">'
                        var ottime = (redata[i].Options[0] != null && redata[i].Options[0].OpTime!=null) ? redata[i].Options[0].OpTime : ""
                        timestr += '<h4 class="' + titleclass + '">【' + redata[i].Name + '】&nbsp;' + ottime+ '</h4>'
                        timestr += '</div>'
                        timestr += '</li>'
                    } else {
                        timestr += '<li class="timeline-item">'
                        if (redata[i].Tstate == 1) {//审批中
                            timestr += '<div class="timeline-item-bluecolor timeline-item-head-first">'
                            //timestr += '<i class="timeline-item-checked   weui-icon-circle"></i>'
                            timestr += '</div>'
                        }
                        else if (redata[i].Tstate ==2) {//通过
                            timestr += '<div class="timeline-item-color timeline-item-head-first">'
                            timestr += '<i class="timeline-item-checked   weui-icon-success-no-circle"></i>'
                            timestr += '</div>'

                        } else if (redata[i].Tstate == 3) {//打回
                            timestr += '<div class="timeline-item-dhcolor timeline-item-head-first">'
                            timestr += '<i class="timeline-item-checked   weui-icon-success-no-circle"></i>'
                            timestr += '</div>'

                        }
                        else {
                            timestr += '<div class="timeline-item-color timeline-item-head"></div>'
                        }


                        timestr += '<div class="timeline-item-tail"></div>'
                        timestr += '<div class="timeline-item-content">'
                        timestr += '<h4 class="' + titleclass + '">【' + redata[i].Name + '】' + redata[i].Sta+'</h4>'
                        var options = redata[i].Options;
                        for (var j = 0; j < options.length; j++) {
                            timestr += '<div class="' + titleclass + '">'
                            //timestr += '<div>' + options[j].YuJian + '<div>'
                            var spsj = options[j].OpTime == null ? "" : options[j].OpTime;
                            timestr += '<div><span>' + options[j].UserName + ':&nbsp;&nbsp;</sapn><span>' + options[j].YuJian + '</span><div>'
                            timestr += '<div>' + spsj +'<div>'
                            timestr += '</div >';

                        }


                        timestr += '</div>'
                        timestr += '</li>'
                    }

                }


                timestr += '</ul>';


                $('#timelineui').append(timestr);



            } else {
                var resultstr2 = "<span class='f-red'>没有数据</span>";
                $('#timelineui').append(resultstr2);
            }
        }, error: function (xhr, type) {
            alert('CheckWflow系统异常' + xhr + ":" + type + ":" + xhr.status);
        }
    });
}


function GetOptions2(currId) {
    var $userId = $("#CurrWxUserId").val();//当前登录的用户

    var $wfitem = $("#WfItem").val();//审批事项
    var $url = woowx.constant.APIBaseURL + "/api/WorkFlow/GetWooFlowTime";
    var flowPerm = {
        SpId: currId,
        WfItem: $wfitem,
        UserId: $userId,
        SpType: 3//合同
    };

    var poststr = JSON.stringify(flowPerm);

    $.ajax({
        type: 'POST',
        url: $url,
        data: poststr,
        dataType: 'json',
        contentType: 'application/json;charset=UTF-8',
        timeout: 10000,
        success: function (data) {
            var $data = JSON.parse(data);
            var redata = $data.Data;
            var instzt = 0;//审批实例状态
            var length = redata.length;
            if (redata != null && redata != undefined && length > 0) {
                $('#timelineui ul').remove();
                var timestr = '<ul>';
                var titleclass = "";
                for (var i = 0; i < length; i++) {
                    
                   
                    if (redata[i].Name == "结束") {
                        var zt = redata[i].Tstate ==1 ? 2 : redata[i].Tstate
                        titleclass = gettitleclass(zt);
                        timestr += '<li class="timeline-item">'
                        if (redata[i].Tstate > 0) {
                            timestr += '<div class="timeline-item-color timeline-item-head-first">'
                            timestr += '<i class="timeline-item-checked   weui-icon-success-no-circle"></i>'
                            timestr += '</div>'

                        } else {
                            timestr += '<div class="timeline-item-color timeline-item-head"></div>'

                        }
                        timestr += '<div class="timeline-item-tail hide"></div>'
                        timestr +='<div class="timeline-item-content content-wg">'
                        timestr += '<div class="' + titleclass+' flownode">结束</div>'
                        timestr += '<div class="appyijian"></div>'
                        var ottime = (redata[i].Options[0] != null && redata[i].Options[0].OpTime != null) ? redata[i].Options[0].OpTime : ""
                        timestr += '<div class="appuser"><div class="appusername"></div><div>' + ottime+'</div></div>'
                        timestr +='</div>'
                        timestr += '</li>'

                    } else {
                        titleclass = gettitleclass(redata[i].Tstate);
                        timestr += '<li class="timeline-item">'
                        if (redata[i].Tstate == 1) {//审批中
                            timestr += '<div class="timeline-item-bluecolor timeline-item-head-first">'

                            timestr += '</div>'
                        }
                        else if (redata[i].Tstate == 2) {//通过
                            timestr += '<div class="timeline-item-color timeline-item-head-first">'
                            timestr += '<i class="timeline-item-checked   weui-icon-success-no-circle"></i>'
                            timestr += '</div>'

                        } else if (redata[i].Tstate == 3) {//打回
                            timestr += '<div class="timeline-item-dhcolor timeline-item-head-first">'
                            timestr += '<i class="timeline-item-checked   weui-icon-success-no-circle"></i>'
                            timestr += '</div>'

                        }
                        else {
                            timestr += '<div class="timeline-item-color timeline-item-head"></div>'
                        }

                        timestr += '<div class="timeline-item-tail"></div>'
                        timestr += '<div class="timeline-item-content content-wg">'
                        var node = redata[i].Name + redata[i].Sta;
                        timestr += '<div class="' + titleclass+' flownode" >' + node+'</div>'
                        var options = redata[i].Options;
                        for (var j = 0; j < options.length; j++) {
                            var spsj = options[j].OpTime == null ? "" : options[j].OpTime;
                            timestr += '<div class="appyijian">' + options[j].YuJian+'</div>'
                            timestr += '<div class="appuser"><div class="appusername">' + options[j].UserName+'</div><div>' + spsj+'</div></div>'

                        }
                               
                

              
                        timestr += '</div>'
                        timestr += '</li>'
                    }




                    

                }


                timestr += '</ul>';


                $('#timelineui').append(timestr);



            } else {
                var resultstr2 = "<span class='f-red'>没有数据</span>";
                $('#timelineui').append(resultstr2);
            }
        }, error: function (xhr, type) {
            alert('CheckWflow系统异常' + xhr + ":" + type + ":" + xhr.status);
        }
    });
}



$(function () {

    var currId = $("#contId").val();//'@ViewData["contId"]'
    LoadMainFormData(currId);
    ShowContText(currId);
    // CheckWflow(currId);
    //审批弹框
    $(document).on("click", "#shenpi", function () {
        $.modal({
            title: "<span class='f-orange'>审批意见</span>",
            text: "<textarea class='weui-form-area'  name='wfoption' id='wfoption' rows='3' cols=20' placeholder='意见必填'></textarea>",
            buttons: [
                {
                    text: "同意", onClick: function () {
                        var wfoption = $("#wfoption").val();
                        if (wfoption === "" || wfoption === undefined) {
                            wfoption = "同意";
                        }
                        //debugger;
                        //var $userId = $("#CurrWxUserId").val();//当前登录的用户
                        //getcurrappinfo($userId);
                       
                        SubmitOption(currId, 1, wfoption);

                        //if (wfoption !== "" && wfoption !== undefined) {
                        //    SubmitOption(currId, 1, wfoption);
                        //} else {
                        //    $.toptip('请输入意见', 'warning')
                        //}

                    }
                },
                {
                    text: "<span class='f-red'>不同意</span>", onClick: function () {
                        var wfoption = $("#wfoption").val();
                        if (wfoption === "" || wfoption === undefined) {
                            wfoption = "不同意";
                        }
                        SubmitOption(currId, 2, wfoption);
                        //if (wfoption !== "" && wfoption !== undefined) {
                        //    SubmitOption(currId, 2, wfoption);
                        //} else {
                        //    $.toptip('请输入意见', 'warning')
                        //}


                    }
                },
                { text: "取消", className: "default", onClick: function () { } },
            ]
        });
    });
    //审批图标可以移动
    moveEvent("shenpi");
    /**
     * 下一条
     * **/
    $(document).on("click", "#nextrow", function () {
        var rowct = $("#approwcount").text();
        if (rowct <= 0) {
            $.alert("您已经全部处理完毕", "提示", function () {   
            });

        } else {
           
            var nextcontId = $("#nextcontId").val();
            window.location.href = "/Contract/Detail?Id=" + nextcontId

        }

    })
    /**
     * 关闭
     * **/
    $(document).on("click", "#msgcolse", function () {
        $("#msgcontent").hide();

    })
    
});





