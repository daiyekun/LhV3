layui.define(['table', 'form', 'wordAddin', 'tree'], function (exports) {
    var $ = layui.$
  , table = layui.table
  , setter = layui.setter
  , admin = layui.admin
  , form = layui.form
  , tree = layui.tree
  , wordAddin = layui.wordAddin
    ;
    var $Id = wooutil.getUrlVar('Id');
    /**模板历史**/
    table.render({
        elem: '#NF-contTxtHistTemp'
       , url: '/System/ContTxtTemp/GetHistList?tempId=' + $Id + '&rand=' + wooutil.getRandom()
       , toolbar: '#toolNF-contTxtHistTemp'
       , defaultToolbar: ['filter']
       , cellMinWidth: 80
       , cols: [[
             { type: 'numbers', fixed: 'left' }
           , { type: 'checkbox', fixed: 'left' }
           , { field: 'Id', title: 'Id', width: 120 }
            , { field: 'Vesion', title: '版本号', width: 130,templet: function(d){
                return d.Vesion + ".0";
            } }
           , { field: 'ModifyUserName', title: '修改人', width: 150 }
           , { field: 'ModifyDateTime', title: '更新日期', width: 140 }
           , { field: 'UseVersion', title: '状态', width: 140, templet: '#useVersionTpl' }
       ]]
       , page: false
       , loading: true
       , height: setter.table.height_4
       , limit: setter.table.limit
       , limits: setter.table.limits
       , done: function (res) {   //返回数据执行回调函数
           //layer.close(loadindex);    //返回数据关闭loading
       }

    });
    /**
    *标的字段设置
    **/
    var tableIni = function () {
        var $Htid = $("#HistId").val();
        var $fdType = $("#FieldTypeVal").val();
        var $bcId = $("#SelBcId").val();
        var _url = '/ContractDraft/ContTxtTempAndSubField/GetFields?tempId=' + $Htid
            + "&fdType=" + $fdType
            + "&bcId=" + $bcId
            + '&rand=' + wooutil.getRandom();
        table.render({
            elem: '#NF-ContSubFildTemp'
        , url: _url
        , toolbar: '#toolbarSubFiled'
        , defaultToolbar: ['filter']
        , cellMinWidth: 80
        , cols: [[
            { type: 'numbers', fixed: 'left' }
            , { type: 'checkbox', fixed: 'left' }
            , { field: 'Name', title: '字段名称', minWidth: 150, width: 220 }
            , { field: 'Sval', title: '显示名称', edit: 'text', width: 220 }
            , { title: '操作', width: 220, align: 'center', fixed: 'right', toolbar: '#subFiledTool' }
        ]]
            // , page: true
        , loading: true
        , height: setter.table.height_4
        , limit: setter.table.limit
        , limits: setter.table.limits
        , done: function (res) {
            // layer.close(loadindex);    
        }

        });

    }
    /**初始化菜单**/
    function TreeInit() {
        admin.req({
            url: '/Business/BusinessCategory/GetBcCateTreeData?rand=' + wooutil.getRandom()
            , success: function (res) {
                var treedata = res.Data;
                treedata.splice(0, 0, {
                    title: '非业务品类'
                         , id: -1
                         , spread: true
                });
                tree.render(
                           {
                               elem: '#BcCategory_Tree',
                               height: "full-95",
                               data: treedata
                               , id: 'bctree'
                               , click: function (obj) {
                                   selfields = [];
                                   $("#selCateName").html(obj.data.title);
                                   var bcid = obj.data.id;
                                   if (bcid === -1) {
                                       bcid = 0;
                                   }
                                   $("#SelBcId").val(bcid);
                                   //加载Table
                                   active.reloadFdTable();
                               }
                           });
            }
        });
    };
    /**
    *事件
    **/
    var active = {
        batchdel: function () {//删除
            wooutil.deleteDatas({ tableId: 'NF-contTxtHistTemp', table: table, url: '/System/ContTxtTemp/DeleteHist' });
        },
        preview: function (obj) {
            var checkStatus = table.checkStatus("NF-contTxtHistTemp")
               , checkData = checkStatus.data; //得到选中的数据
            if (checkData.length === 0) {
                return layer.msg('请选择数据');
            }
            wordAddin.OpenWord(checkData[0].Id, "Tplonreadonly",null);
        },
        reloadFdTable: function () {
            var _url = '/ContractDraft/ContTxtTempAndSubField/GetFields?tempId=' + $("#HistId").val()
                                               + "&fdType=" + $("#FieldTypeVal").val()
                                               + "&bcId=" + $("#SelBcId").val()
                                               + '&rand=' + wooutil.getRandom();
            table.reload("NF-ContSubFildTemp", {
                url: _url
            });
        }
    }

    /**
    *启用禁用
    **/
    form.on('switch(UseVersion)', function (obj) {
        
        //layer.tips(this.value + ' ' + this.IsSubCompany + '：' + obj.elem.checked, obj.othis);
        var iscomp = obj.elem.checked ? 1 : 0;
        admin.req({
            url: '/System/ContTxtTemp/UpdateHistField',
            data: { Id: this.value, FieldName: "UseVersion", FieldValue: iscomp, OtherId: $Id },
            done: function (res) {
                layer.msg('设置成功！');

            }

        });
    });
    /**
    *模板列表
    **/
    table.on('toolbar(NF-contTxtHistTemp)', function (obj) {
        switch (obj.event) {
            case 'batchdel'://删除
                active.batchdel();
                break;
            case 'preview'://预览
                active.preview(obj);
                break;
           
        };
    });

    /**
   *修改时赋值
   **/
   
    if ($Id !== "" && $Id !== undefined) {
        admin.req({
            url: '/System/ContTxtTemp/ShowView',
            data: { Id: $Id, rand: wooutil.getRandom() },
            done: function (res) {
                form.val("NF-ContTxtTemp-DetailForm", res.Data);
                if (res.Data.ShowSubMatter) {//显示标的
                    $("#BiaoDi").removeClass("layui-hide");
                    tableIni();
                }
                if (res.Data.FieldType === 1) {
                    $(".bcCategory").removeClass("layui-hide");
                    TreeInit();
                }
            }
        });

    } 

    exports('contTxtTempDetail', {});
})