
var ddr = $("#Wxid").val();

if (ddr == null || ddr == "") {
    $("#wxnamenull").click();
}

/***用户详情及明细等相关操作***/
$(function () {
    var $url = woowx.constant.APIBaseURL +"/api/User/GetUserByWxId";
    
    $.ajax({
        url: $url
        , type: "GET"
        , dataType: "json"
        , contentType:'application/json;charset=utf-8'
        , timeout:6000
        , data: { "WxId": $("#CurrWxUserId").val() }
        , success: function (data) {
            var $data = JSON.parse(data);
            if ($data.Data != null && $data.Data != undefined) {
            $("#Uname").val($data.Data.Uname);
            $("#UdisName").val($data.Data.UdisName);
            $("#UdepName").val($data.Data.UdepName);
            $("#Utel").val($data.Data.Utel);
            $("#Umobile").val($data.Data.Umobile);
            $("#Uemail").val($data.Data.Uemail);
            }
          
          
        }, error: function (xhr, type) {
            alert('系统异常' + xhr + ":" + type + ":" + xhr.status);
          
            

        }
    });
    

})
