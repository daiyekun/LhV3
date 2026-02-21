/**
 @Name：选择框
 @Author：dyk 20180814
 */
layui.define(['table', 'form'], function (exports) {
    var $ = layui.$
        , table = layui.table
        , setter = layui.setter
        , admin = layui.admin
        , form = layui.form;
    /**
    *选择用户
    ***/
    table.render({
        elem: '#NF-system-selectUser'
        , url: '/System/UserInfor/GetList?rand=' + wooutil.getRandom()
        , toolbar: '#toolSelectUser'
        , defaultToolbar: ['filter']
        , title: '用户数据'
        , cols: [[
            { type: 'numbers', fixed: 'left' },
            { type: 'checkbox', fixed: 'left' }
            , { field: 'Name', title: '用户名称', minWidth: 160, templet: '#nameTpl', fixed: 'left' }
            , { field: 'DeptName', title: '所属机构', width: 130 }
            , { field: 'DisplyName', title: '显示名称', width: 110 }
            , { field: 'SexDic', width: 100, title: '性别', templet: '#userSexTpl' }
            , { field: 'Age', width: 100, title: '年龄', hide: true }
            , { field: 'Tel', width: 100, title: '电话', hide: true  }
            , { field: 'Mobile', width: 120, title: '手机', hide: true  }
            , { field: 'Email', width: 120, title: 'E-Mail', hide: true  }
            , { field: 'Ustart', width: 80, title: '状态', templet: '#userstateTpl', unresize: true }
           
        ]]
        , page: true
        , loading: true
        , height: setter.table.height_3
        , limit: setter.table.limit
        , limits: setter.table.limits

    });
    
    exports('nf-selectItems', {})
});