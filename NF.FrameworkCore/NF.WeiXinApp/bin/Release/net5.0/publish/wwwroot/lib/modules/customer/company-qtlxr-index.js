var ddr = $("#Wxzh").val();
if (ddr == null || ddr == "") {
    $("#wxnamenull").click();
}


function LoadMainFormData(currId) {
   
    var $url = woowx.constant.APIBaseURL + "/api/company/WxQtlxr";
    $.ajax({
        type: 'GET',
        url: $url,
        data:
        {
            Id: currId
        },
        dataType: 'json',
        timeout: 10000,
        success: function (datax) {
            //var $data = JSON.parse(data);
            var result = '';
            var Fh = '';
            var datas = JSON.parse(datax);
            var lo = datas.Data.length;
            for (var i = 0; i < lo; i++) {

                result += '<div class="page" id="Re" name="Rt" > '
                  // + '<a href="Detail?Id=' + datas.Data[i].CompanyId + '"><p">关闭</p></a>'
                    + '<div class="weui-cells__title">' + datas.Data[i].Name + '</div>'
                    + '<div class="weui-cells">'
                    + '<a class="weui-cell  weui-cell_access" >'
                    + '<div class="weui-cell__bd">'
                    + '<p>姓名</p>'
                    + '</div>'
                    + '<div class="weui-cell__ft">' + datas.Data[i].Name + '</div>'
                    + '</a>'
                    + '<a class="weui-cell  weui-cell_access" href="javascript:">'
                    + '<div class="weui-cell__bd">'
                    + '<p>职位</p>'
                    + '</div>'
                    + '<div class="weui-cell__ft">' + datas.Data[i].Position + '</div>'
                    + '</a>'
                    + '<a class="weui-cell  weui-cell_access" href="javascript:">'
                    + '<div class="weui-cell__bd">'
                    + '<p>办公电话</p>'
                    + '</div>'
                    + '<div class="weui-cell__ft">' + datas.Data[i].Tel + '</div>'
                    + '</a>'

                    + '</div>'
                    + '</div>'
            }

            Fh += '<a href="Detail?Id=' + datas.Data[0].CompanyId + '" class="weui-btn weui-btn_mini weui-btn_primary">关闭</a>'

            $("#rank-list").append(Fh+result);
        }, error: function (xhr, type) {
            alert('LoadMainFormData系统异常' + xhr + ":" + type + ":" + xhr.status);
        }
    });


}

function gb(id) {
   redirectTo({
      
        url: 'Detail?Id' + id
    })

}

$(function () {

    var currId = $("#contIds").val();//'@ViewData["contId"]'
    LoadMainFormData(currId);
});










