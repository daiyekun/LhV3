function ft() {
    $.ajax({
        type: 'get',
        url: woowx.constant.APIBaseURL + 'Company/ComList',
        headers: { 'Access-Control-Allow-Origin': '*' },
        dataType: 'json',
      
        success: function (data) {

        
        }
    });

}