var ddr = $("#CurrWxUserId").val();
if (ddr == null || ddr == "") {
    $("#wxnamenull").click();
}


var $wfitems = "";

function LoadMainFormData(currId) {

    var $url = woowx.constant.APIBaseURL + "/api/company/KhView";
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
            $wfitems = $data.Data.WfItem;
            if ($data.Data != null && $data.Data != undefined) {

                $.each($data.Data, function (key, value) {
                    $("#" + key).val(value);
                });
                CheckWflow(currId);
                GetOptions(currId);//意见
            }
        }, error: function (xhr, type) {
           
            alert('LoadMainFormData系统异常' + xhr + ":" + type + ":" + xhr.status);
        }
    });


}
//下载文件
function downloadtxt(txtId) {

    window.open(woowx.constant.APIBaseURL + "/api/company/DownLoadFile?txtId=" + txtId);

}
/***合同文本**/
function ShowContText(currId) {
   
    var $urls = woowx.constant.APIBaseURL + "/api/company/GetcompViwe";
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
            var $data = JSON.parse(data);
            var length = $data.Data.length;
            var resultstr2 = "";
            if (length > 0) {

                for (var i = 0; i < length; i++) {
                    //var downloadurl = woowx.constant.APIBaseURL + "/" + data[0].path;
                    if (i == 0) {
                        resultstr2 += '<div class="page">'
                            + '<div class="page__bd">'
                            + '<a class="weui-cell"  style="color:#000" href="javascript:">'
                            + '<div class="weui-cell__bd"><p>文件名称</p></div>'
                            + '<div class="weui-cell__ft"><span>' + $data.Data[i].FileName+ '</span></div >'
                            + '</a>'
                            + '<a class="weui-cell " style="color:#000"  href="javascript:">'
                            + '<div class="weui-cell__bd"><p>文件类别</p></div>'
                            + '<div class="weui-cell__ft">' + $data.Data[i].CategoryName + '</div>'
                            + '</a>'
                            + '<a href="javascript:;" onclick=downloadtxt(' + $data.Data[i].Id + ') class="weui-btn weui-btn_primary">查看</a>'
                            + '</div>'
                            + '</div>'
                    } else {
                        //margin-top:设置条数之间间隔
                        resultstr2 += '<div class="page">'
                            + '<div class="page__bd">'
                            + '<a class="weui-cell" style="color:#000" href="javascript:">'
                            + '<div class="weui-cell__bd"><p>文件名称</p></div>'
                            + '<div class="weui-cell__ft"><span>' + $data.Data[i].FileName + '</span></div >'
                            + '</a>'
                            + '<a class="weui-cell " style="color:#000"  href="javascript:">'
                            + '<div class="weui-cell__bd"><p>文件类别</p></div>'
                            + '<div class="weui-cell__ft">' + $data.Data[i].CategoryName + '</div>'
                            + '</a>'
                            //+ '<a href="' + downloadurl + '" class="weui-btn weui-btn_primary">下载</a>'
                            + '<a href="javascript:;" onclick=downloadtxt(' + $data.Data[i].Id + ') class="weui-btn weui-btn_primary">查看</a>'
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
    var CType = $("#FinanceType").val();
    if (CType=="0") {
        CType = 0;
    } else if (CType == "1") {
        CType = 1;
    } else if (CType == "2") {
        CType = 2;
    }
    var $userId =  $("#CurrWxUserId").val();//当前登录的用户
    var $wfitem = $wfitems;// $("#WfItemDic").val();//审批事项
    var $url = woowx.constant.APIBaseURL + "/api/WorkFlow/GetFlowPermission";
    var flowPerm = {
        SpId: currId,
        WfItem: $wfitem,
        UserId: $userId,
        SpType: CType
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
    var CType = $("#FinanceType").val();
    if (CType == "0") {
        CType = 0;
    } else if (CType == "1") {
        CType = 1;
    } else if (CType == "2") {
        CType = 2;
    }
    var $userId =$("#CurrWxUserId").val();//当前登录的用户
    var $wfitem = $("#WfItem").val();//审批事项
    var $url = woowx.constant.APIBaseURL + "/api/WorkFlow/SubmitAgreeOption";
    if (OptRes === 2) {//不同意
        $url = woowx.constant.APIBaseURL + "/api/WorkFlow/SubmitDisagreeOption";

    }
    var otion = {
        InstId: $("#InstId").val(),//审批实例ID
        ObjType: CType,
        SpType: CType,//合同
        ObjId: currId,
        Option: opmsg,
        OptRes: OptRes,
        SubmitWxId: $userId
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
                window.location.href = "/Common/SuccMag";
            } else {
                window.location.href = "/Common/FailMsg";
            }
        }, error: function (xhr, type) {
            var errmsg = "错误类型：" + type + ",xhr.status:" + xhr.status
            window.location.href = "/Common/FailMsg?errmsg=" + errmsg;

        }
    });


}
//获取审批情况
function GetOptions(currId) {
    var CType = $("#FinanceType").val();
    if (CType == "0") {
        CType = 0;
    } else if (CType == "1") {
        CType = 1;
    } else if (CType == "2") {
        CType = 2;
    }
   
    var $userId = $("#CurrWxUserId").val();//当前登录的用户
    var $wfitem = $("#WfItem").val();//审批事项
    var $url = woowx.constant.APIBaseURL + "/api/WorkFlow/GetFlowOptions";
    var flowPerm = {
        SpId: currId,
        WfItem: $wfitem,
        UserId: $userId,
        SpType: CType//合同
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
            if (redata != null && redata != undefined) {

                var firstnode = '<li class="timeline-item">'
                firstnode += '<div class="timeline-item-color timeline-item-head-first"><i  class="timeline-item-checked   weui-icon-success-no-circle"></i></div>'
                firstnode += '<div class="timeline-item-tail" ></div>'
                firstnode += '<div class="timeline-item-content"><h4 class="recent">开始</h4>'
                firstnode += '</li>'
                // $('#timelineui').append(firstnode);
                $.each(redata, function (index, item) {
                    instzt = item.Insst;
                    var headfirst = item.Spst === 2 ? "timeline-item-head-first" : "timeline-item-head";
                    var succcirc = item.Spst === 2 ? "" : "";
                    var mli = '<li class="timeline-item">';
                    mli += '<div class="timeline-item-color ' + headfirst + '">';
                    if (item.Spst === 2) {
                        mli += '<i class="timeline-item-checked weui-icon-success-no-circle"></i>';
                    }
                    mli += '</div>';
                    mli += '<div class="timeline-item-tail"></div>';
                    mli += '<div class="timeline-item-content">';

                    if (item.Spst === 2) {

                        mli += '<h4 class="recent">' + item.Nc + '</h4>';
                    } else if (item.Spst === 3) {
                        mli += '<h4 class="dhrecent">' + item.Nc + '【打回】</h4>';
                    } else if (item.Spst === 1) {//审批中
                        mli += '<h4 class="spzrecent">' + item.Nc + '【审批中】</h4>';
                    }
                    else {
                        mli += '<h4>' + item.Nc + '</h4>';
                    }

                    $.each(redata[index].Options, function (opindex, mgitem) {

                        var wxmsg = mgitem.Xm + ':&nbsp;' + mgitem.Yj + '(' + mgitem.Sj + ')';
                        if (item.Spst === 1) {
                            mli += '<p>' + wxmsg + '</p>'
                        }
                        else if (item.Spst === 2) {
                            mli += '<p class="recent">' + wxmsg + '</p>'
                        } else if (item.Spst === 3) {
                            mli += '<p class="dhrecent">' + wxmsg + '</p>'
                        } else {
                            mli += '<p>' + wxmsg + '</p>'
                        }


                    });
                    mli += '</div></li>';
                    $('#timelineui').append(mli);
                });

                var endnode = ' <li class="timeline-item">'
                endnode += ' <div class="timeline-item-color timeline-item-head">'
                if (instzt === 2) {
                    endnode += '<i class="timeline-item-checked weui-icon-success-no-circle"></i>';
                }
                endnode += '</div>'
                endnode += '<div class="timeline-item-tail hide" ></div>'
                endnode += '<div class="timeline-item-content">'
                endnode += instzt === 2 ? '<h4 class="recent">结束</h4>' : '<h4>结束</h4>'
                endnode += '</div></li>'

                $('#timelineui').append(endnode);



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
    ////审批弹框
    $(document).on("click", "#shenpi", function () {
        $.modal({
            title: "<span class='f-orange'>审批意见</span>",
            text: "<textarea class='weui-form-area'  name='wfoption' id='wfoption' rows='3' cols=20'></textarea>",
            buttons: [
                {
                    text: "同意", onClick: function () {
                        var wfoption = $("#wfoption").val();
                        if (wfoption !== "" && wfoption !== undefined) {
                            SubmitOption(currId, 1, wfoption);
                        } else {
                            $.toptip('请输入意见', 'warning')
                        }

                    }
                },
                {
                    text: "<span class='f-red'>不同意</span>", onClick: function () {
                        var wfoption = $("#wfoption").val();
                        if (wfoption !== "" && wfoption !== undefined) {
                            SubmitOption(currId, 2, wfoption);
                        } else {
                            $.toptip('请输入意见', 'warning')
                        }


                    }
                },
                { text: "取消", className: "default", onClick: function () { } },
            ]
        });
    });
    //审批图标可以移动
    moveEvent("shenpi");
});










