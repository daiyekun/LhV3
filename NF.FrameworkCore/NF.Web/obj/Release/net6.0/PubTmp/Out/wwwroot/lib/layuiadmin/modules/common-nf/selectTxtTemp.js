/**
*选择合同模板
**/
layui.define(['table', 'form'], function (exports) {
    var $ = layui.$
    , table = layui.table
    , setter = layui.setter
    , admin = layui.admin;
     var wIndex = wooutil.getUrlVar('wIndex');
    //合同类别
     var htlb = wooutil.getUrlVar("htlb");
    //经办结构
     var deptId = wooutil.getUrlVar("deptId");
    var logdindex = layer.load(0, { shade: false });
    //列表
    table.render({
        elem: '#NF-SelTxtTemp-Index'
       , url: '/NfCommon/SelectItem/GetTxtTempList?htLb=' + htlb + '&deptId=' + deptId + '&rand=' + wooutil.getRandom()
       , toolbar: '#toolcontract'
       , defaultToolbar: []
       , cellMinWidth: 80
       , cols: [[
           { type: 'numbers', fixed: 'left' },
           { type: 'radio', fixed: 'left' }
            , { field: 'Name', title: '模板名称', minWidth: 180, width: 220, fixed: 'left' }
            , { field: 'TepTypeDic', title: '模板类别', width: 150 }
            , { field: 'TextTypeDic', title: '文本类别', width: 150 }
            , { field: 'DeptNames', title: '所属机构', width: 150 }
            , { field: 'CreateUserName', title: '建立人', width: 130 }
            , { field: 'ModifyDateTime', title: '更新日期', width: 130 }
            , {
                 field: 'Vesion', title: '版本号', width: 130, templet: function (d) {
                     return d.Vesion + ".0";
                 }
             }
            , { field: 'Id', title: 'Id', width: 100, hide: true }
            , { field: 'UseHistId', title: 'UseHistId', width: 100, hide: true }
           

       ]]
       , page: true
       , loading: true
       , height: '450px'//setter.table.height_tab
       , limit: setter.table.limit
       , limits: setter.table.limits
       , done: function (res, curr, count) {   //返回数据执行回调函数
           layer.close(logdindex);    //返回数据关闭loading
           $("input[name=keyWord]").val($("input[name=hide_keyWord]").val());
           $("input[name=hide_keyWord]").val("");

       }

    });
    /**
    *选择模板以后--单选框触发目前改成行触发
    **/
    //table.on('radio(NF-SelTxtTemp-Index)', function (obj) {
    //    debugger;
    //    var body = parent.layer.getChildFrame('body', wIndex);
    //    body.find('input[name=TempName]').val(obj.data.Name);
    //    body.find('input[name=TemplateId]').val(obj.data.UseHistId);//当前启用的历史模板
    //    //文本类型
    //    body.find('input[name=CategoryId]').val(obj.data.TextType);
    //    body.find('input[name=ContTxtType]').val(obj.data.TextTypeDic);
    //    var _index = $("#currIndex").val();
    //    parent.layer.close(_index);
       
       
       


    //});
    //监听行点击事件
    table.on('row(NF-SelTxtTemp-Index)', function (obj) {
        
        var body = parent.layer.getChildFrame('body', wIndex);
        body.find('input[name=TempName]').val(obj.data.Name);
        body.find('input[name=TemplateId]').val(obj.data.UseHistId);//当前启用的历史模板
        //文本类型
        body.find('input[name=CategoryId]').val(obj.data.TextType);
        body.find('input[name=ContTxtType]').val(obj.data.TextTypeDic);
        var _index = $("#currIndex").val();
        parent.layer.close(_index);

    });


    exports('selContTxtTemp', {});
})