
var ddr = $("#Wxid").val();
debugger;
if (ddr == null || ddr == "") {
    $("#wxnamenull").click();
}

var id = $("#id").val();
var $url = woowx.constant.APIBaseURL + "/api/Contract/GetCountViwe";
$.ajax({
    type: 'Get',
    url: $url,
    data:
    {
        // UserId: 1,
        Id: id
    },
    async: false,
    dataType: 'json',
      timeout: 6000,
    success: function (data) {
        var result = "";
        result += '<div class="page">'
            + '<div class="page__bd">'
            + '<div class="weui-cells">'
            + '<div class="weui-cells__title">合同基本信息</div>'
            + '<a class="weui-cell" style="color:#000"  st href="javascript:">'
            + '<div class="weui-cell__bd"><p>合同名称</p></div>'
            + '<div class="weui-cell__ft">' + data.name + '</div >'
            + '</a>'
           
            + '<div class="weui-cell__bd"><p>合同编号</p></div>'
            + '<div class="weui-cell__ft">' + data.code + '</div>'
         
           
            + '<div class="weui-cell__bd"><p>合同类别</p></div>'
            + '<div class="weui-cell__ft">' + data.contTypeName + '</div>'

            + '<div class="weui-cell__bd"><p>执行部门</p></div>'
            + '<div class="weui-cell__ft">' + data.deptName + '</div>'


            + '</div>'

            //'<div class="weui_cell" >'
            //+ '<div class="weui_cell_bd weui_cell_primary"><p>名称</p></div>'
            //+ '<div class="weui_cell_ft">' + data.name + '</div>'
            //+ '</div>'
            //+ '<div class="weui_cell" >'
            //+ '<div class="weui_cell_bd weui_cell_primary"><p>编号</p></div>'
            //+ '<div class="weui_cell_ft">' + data.code + '</div>'
            //+ '</div>'
            //+ '<div class="weui_cell" >'
            //+ '<div class="weui_cell_bd weui_cell_primary"><p>类别</p></div>'
            //+ '<div class="weui_cell_ft">' + data.contTypeName+ '</div>'
            //+ '</div>'
            //+ '<div class="weui_cell" >'
            //+ '<div class="weui_cell_bd weui_cell_primary"><p>执行部门</p></div>'
            //+ '<div class="weui_cell_ft">' + data.deptName+ '</div>'
            //+ '</div>'
            //+ '<div class="weui_cell" >'
            //+ '<div class="weui_cell_bd weui_cell_primary"><p>选择招标</p></div>'
            //+ '<div class="weui_cell_ft">' + data.zbName+ '</div>'
            //+ '</div>'
            //+ '<div class="weui_cell" >'
            //+ '<div class="weui_cell_bd weui_cell_primary"><p>选择询价</p></div>'
            //+ '<div class="weui_cell_ft">' + data.xjName+ '</div>'
            //+ '</div>'
            //+ '<div class="weui_cell" >'
            //+ '<div class="weui_cell_bd weui_cell_primary"><p>选择洽谈</p></div>'
            //+ '<div class="weui_cell_ft">' + data.ytName + '</div>'
            //+ '</div>'
            //+ '<div class="weui_cell" >'
            //+ '<div class="weui_cell_bd weui_cell_primary"><p>签订人身份证号</p></div>'
            //+ '<div class="weui_cell_ft">' + data.contSingNo + '</div>'
            //+ '</div>'
            //+ '<div class="weui_cell" >'
            //+ '<div class="weui_cell_bd weui_cell_primary"><p>合同对方</p></div>'
            //+ '<div class="weui_cell_ft">' + data.compName + '</div>'
            //+ '</div>'
            //+ '<div class="weui_cell" >'
            //+ '<div class="weui_cell_bd weui_cell_primary"><p>合同金额</p></div>'
            //+ '<div class="weui_cell_ft">' + data.amountMoney + '</div>'
            //+ '</div>'
            //+ '<div class="weui_cell" >'
            //+ '<div class="weui_cell_bd weui_cell_primary"><p>签约主体</p></div>'
            //+ '<div class="weui_cell_ft">' + data.mdeptName+ '</div>'
            //+ '</div>'
            //+ '<div class="weui_cell" >'
            //+ '<div class="weui_cell_bd weui_cell_primary"><p>币种</p></div>'
            //+ '<div class="weui_cell_ft">' + data.currencyName+ '</div>'
            //+ '</div>'
            //+ '<div class="weui_cell" >'
            //+ '<div class="weui_cell_bd weui_cell_primary"><p>增值税率</p></div>'
            //+ '<div class="weui_cell_ft">' + data.stampTaxThod+ '</div>'
            //+ '</div>'
            //+ '<div class="weui_cell" >'
            //+ '<div class="weui_cell_bd weui_cell_primary"><p>总包合同</p></div>'
            //+ '<div class="weui_cell_ft">' + data.contSum+ '</div>'
            //+ '</div>'
            //+ '<div class="weui_cell" >'
            //+ '<div class="weui_cell_bd weui_cell_primary"><p>合同来源</p></div>'
            //+ '<div class="weui_cell_ft">' + data.contSName + '</div>'
            //+ '</div>'
            //+ '<div class="weui_cell" >'
            //+ '<div class="weui_cell_bd weui_cell_primary"><p>合同对方编号</p></div>'
            //+ '<div class="weui_cell_ft">' + data.otherCode+ '</div>'
            //+ '</div>'
            //+ '<div class="weui_cell" >'
            //+ '<div class="weui_cell_bd weui_cell_primary"><p>签订日期</p></div>'
            //+ '<div class="weui_cell_ft">' + data.sngnDateTime+ '</div>'
            //+ '</div>'
            //+ '<div class="weui_cell" >'
            //+ '<div class="weui_cell_bd weui_cell_primary"><p>生效日期</p></div>'
            //+ '<div class="weui_cell_ft">' + data.effectiveDateTime + '</div>'
            //+ '</div>'
            //+ '<div class="weui_cell" >'
            //+ '<div class="weui_cell_bd weui_cell_primary"><p>计划完成日期</p></div>'
            //+ '<div class="weui_cell_ft">' + data.planCompleteDateTime + '</div>'
            //+ '</div>'
            //+ '<div class="weui_cell" >'
            //+ '<div class="weui_cell_bd weui_cell_primary"><p>负责人</p></div>'
            //+ '<div class="weui_cell_ft">' + data.principalUserName+ '</div>'
            //+ '</div>'
            //+ '<div class="weui_cell" >'
            //+ '<div class="weui_cell_bd weui_cell_primary"><p>备用1</p></div>'
            //+ '<div class="weui_cell_ft">' + data.reserve1 + '</div>'
            //+ '</div>'
            //+ '<div class="weui_cell" >'
            //+ '<div class="weui_cell_bd weui_cell_primary"><p>备用2</p></div>'
            //+ '<div class="weui_cell_ft">' + data.reserve2 + '</div>'
            //+ '</div>'
            //+ '<div class="weui_cell" >'
            //+ '<div class="weui_cell_bd weui_cell_primary"><p>第三方</p></div>'
            //+ '<div class="weui_cell_ft">' + data.comp3Name + '</div>'
            //+ '</div>'
            //+ '<div class="weui_cell" >'
            //+ '<div class="weui_cell_bd weui_cell_primary"><p>第四方</p></div>'
            //+ '<div class="weui_cell_ft">' + data.comp4Name + '</div>'
            //+ '</div>'
            //+ '<div class="weui_cell" >'
            //+ '<div class="weui_cell_bd weui_cell_primary"><p>所属项目</p></div>'
            //+ '<div class="weui_cell_ft">' + data.projName + '</div>'
            //+ '</div>'
            //+ '<div class="weui_cell" >'
            //+ '<div class="weui_cell_bd weui_cell_primary"><p>合同属性</p></div>'
            //+ '<div class="weui_cell_ft">' + data.contPro+ '</div>'
            //+ '</div>'
            //+ '<div class="weui_cell" >'
            //+ '<div class="weui_cell_bd weui_cell_primary"><p>预估金额</p></div>'
            //+ '<div class="weui_cell_ft">' + data.esAmountThod + '</div>'
            //+ '</div>'
            //+ '<div class="weui_cell" >'
            //+ '<div class="weui_cell_bd weui_cell_primary"><p>预收款</p></div>'
            //+ '<div class="weui_cell_ft">' + data.advAmountThod + '</div>'
            //+ '</div>'
            //+ '<div class="weui_cell" >'
            //+ '<div class="weui_cell_bd weui_cell_primary"><p>实际履行日期</p></div>'
            //+ '<div class="weui_cell_ft">' + data.performanceDateTime + '</div>'
            //+ '</div>'
            //+ '<div class="weui_cell" >'
            //+ '<div class="weui_cell_bd weui_cell_primary"><p>实际完成日期</p></div>'
            //+ '<div class="weui_cell_ft">' + data.actualCompleteDateTime + '</div>'
            //+ '</div>'
            //+ '<div class="weui_cell" >'
            //+ '<div class="weui_cell_bd weui_cell_primary"><p>合同状态</p></div>'
            //+ '<div class="weui_cell_ft">' + data.stateDic + '</div>'
            //+ '</div>'
            //+ '<div class="weui_cell" >'
            //+ '<div class="weui_cell_bd weui_cell_primary"><p>完成金额</p></div>'
            //+ '<div class="weui_cell_ft">' + data.htWcJeThod + '</div>'
            //+ '</div>'
            //+ '<div class="weui_cell" >'
            //+ '<div class="weui_cell_bd weui_cell_primary"><p>完成比例</p></div>'
            //+ '<div class="weui_cell_ft">' + data.htWcBl + '</div>'
            //+ '</div>'
            //+ '<div class="weui_cell" >'
            //+ '<div class="weui_cell_bd weui_cell_primary"><p>票款差额</p></div>'
            //+ '<div class="weui_cell_ft">' + data.piaoKaunCha + '</div>'
            //+ '</div>'
            //+ '<div class="weui_cell" >'
            //+ '<div class="weui_cell_bd weui_cell_primary"><p>建立人</p></div>'
            //+ '<div class="weui_cell_ft">' + data.createUserName + '</div>'
            //+ '</div>'
            //+ '<div class="weui_cell" >'
            //+ '<div class="weui_cell_bd weui_cell_primary"><p>建立时间</p></div>'
            //+ '<div class="weui_cell_ft">' + data.createDateTime+ '</div>'
            //+ '</div>'
            + '</div>'
            + '</div>'

        

        $('#kfMainDetail').html(result);
    },
    error: function (xhr, type) {
        alert('系统请求错误companyDetail');

    }
});

/***合同文本**/
function ShowFile() {
    var $url = woowx.constant.APIBaseURL + "/api/Contract/GetContTextViwe";
    $.ajax({///GetCountViwe
        type: 'Get',
        url: $url,
        data:
        {
            // UserId: 1,
            Id: id
        },
        async: false,
        dataType: 'json', timeout: 6000,
        success: function (data) {
            var length = data.length;
            var resultstr2 = "";
            if (length > 0) {

                for (var i = 0; i < length; i++) {
                    var downloadurl = woowx.constant.APIBaseURL+"/" + data[0].path;
                    if (i == 0) {
                        resultstr2 += '<div class="weui_cells " style="margin-top: 0px;">'
                            + '<div class="weui_cell">'
                            + '<div class="weui_cell_bd weui_cell_primary"><p>文件名称</p></div>'
                            + ' <div class="weui_cell_ft">' + data[i].name + '</div>'
                            + '</div>'
                            + '<div class="weui_cell">'
                            + '<div class="weui_cell_bd weui_cell_primary"><p>文件类别</p></div>'
                            + '<div class="weui_cell_ft">' + data[i].contTxtType + '</div>'
                            + '</div>'
                        
                            + '<div class="weui_cell">'
                            + '<div class="weui_cell_bd weui_cell_primary"><p>下载</p></div>'
                            + '<div class="weui_cell_ft"><a href="' + downloadurl + '">下载</a></div>'
                            + '</div>'
                            + '</div>'
                    } else {
                        //margin-top:设置条数之间间隔
                        resultstr2 += '<div class="weui_cells " style="margin-top: 4px;">'
                            + '<div class="weui_cell">'
                            + '<div class="weui_cell_bd weui_cell_primary"><p>附件名称</p></div>'
                            + ' <div class="weui_cell_ft">' + data[i].name + '</div>'
                            + '</div>'
                            + '<div class="weui_cell">'
                            + '<div class="weui_cell_bd weui_cell_primary"><p>类别</p></div>'
                            + '<div class="weui_cell_ft">' + data[i].contTxtType + '</div>'
                            + '</div>'
                            + '<div class="weui_cell">'
                            + '<div class="weui_cell_bd weui_cell_primary"><p>下载</p></div>'
                            + '<div class="weui_cell_ft"><a href="' + downloadurl + '">下载</a></div>'
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

