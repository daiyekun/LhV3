/***
*合同模板
***/
layui.define(['table', 'form', 'wordAddin'], function (exports) {
    var $ = layui.$
   , table = layui.table
   , setter = layui.setter
   , admin = layui.admin
   , form = layui.form
   , wordAddin = layui.wordAddin;
    var loadindex = wooutil.loading();
    //列表
    table.render({
        elem: '#NF-ContTxtTemp-Index'
        , url: '/System/ContTxtTemp/GetList?rand=' + wooutil.getRandom()
        , toolbar: '#toolcontTxtTemp'
        , defaultToolbar: ['filter']
        , cellMinWidth: 80
        , cols: [[
            { type: 'numbers', fixed: 'left' }
            , { type: 'checkbox', fixed: 'left' }
            , { field: 'Name', title: '模板名称', minWidth: 180, width: 220, templet: '#nameTpl', fixed: 'left', sort: true }
            , { field: 'TepTypeDic', title: '模板类别', width: 150 }
            , { field: 'TextTypeDic', title: '文本类别', width: 150 }
            , { field: 'DeptNames', title: '所属机构', width: 150 }
            , { field: 'CreateUserName', title: '建立人', width: 130 }
            , { field: 'ModifyDateTime', title: '更新日期', width: 130 }
            , { field: 'Id', title: 'Id', width: 100, hide: true }
            , {
                field: 'Vesion', title: '版本号', width: 130, templet: function (d) {
                    return d.Vesion + ".0";
                }
            }
            , { field: 'Tstate', title: '状态', width: 140, templet: '#tstateTpl' }
            //, { field: 'WordEdit', title: '状态', width: 140, templet: '#wordEditTpl' }
            , { title: '操作', width: 150, align: 'center', fixed: 'right', toolbar: '#table-conttxttemp-bar' }
        ]]
        , page: true
        , loading: true
        , height: setter.table.height_4
        , limit: setter.table.limit
        , limits: setter.table.limits
        , done: function (res) {   //返回数据执行回调函数
            layer.close(loadindex);    //返回数据关闭loading
            $("input[name=keyWord]").val($("input[name=hide_keyWord]").val());
            $("input[name=hide_keyWord]").val("");

        }

    });
    //事件
    var openAdd = function () {
        layer.open({
         type: 2
         , title: '新增模板'
         , content: '/System/ContTxtTemp/Build'
         , maxmin: true
         , area: ['60%', '80%']
         , btn: ['编辑模板内容','保存模板', '取消']
         , btnAlign: 'c'
         , skin: "layer-ext-myskin"
         , yes: function (index, layero) {
             SaveTempAndOpenWord({
                 index: index,
                 layero: layero,
                 saveurl: '/System/ContTxtTemp/Save'
                

             });
         },
         btn2: function (index, layero) {
             SaveTemp({
                 index: index,
                 layero: layero,
                 saveurl: '/System/ContTxtTemp/Save'
             })
         },success: function (layero, index) {
                layer.full(index);
                wooutil.openTip();
            }
        });
    };
    var active = {
        add: function () {//新增
            openAdd();
        },
        batchdel: function () {//删除
            wooutil.deleteDatas({ tableId: 'NF-ContTxtTemp-Index', table: table, url: '/System/ContTxtTemp/Delete' });
        },
        search: function () {//查询
            $("input[name=hide_keyWord]").val($("input[name=keyWord]").val());
            table.reload('NF-ContTxtTemp-Index', {
            page: { curr: 1 }
            , where: {
                keyWord: $("input[name=keyWord]").val()

            }
        });
        },
        addWater: function () {//上传水印
            layer.open({
            type: 2
         , title: '设置水印模板水印'
         , content: '/System/ContTxtTemp/SetWaterMark'
         , maxmin: true
         , area: ['60%', '80%']
         , btn: ['取消']
         , btnAlign: 'c'
         , skin: "layer-ext-myskin"
         ,success: function (layero, index) {
                   
          }
            });

        }
    }
    /**
    *打开查看页面
    **/
    function openview(obj) {

        layer.open({
            type: 2
                , title: '查看详情'
                , content: '/System/ContTxtTemp/Detail?Id=' + obj.data.Id + "&rand=" + wooutil.getRandom()
                , maxmin: true
                , area: ['60%', '80%']
                , btnAlign: 'c'
                , skin: "layer-nf-nfskin"
                , btn: ['返回']// , area: [window.screen.width / 2 + 'px', window.screen.height / 2 + 'px'] //宽高
            , yes: function (index, layero) {
                
                layer.close(index);
            }
           , success: function (layero, index) {
               layer.load(0, { shade: false, time: 1 * 1000 });//2秒自动关闭
               layer.full(index);
               wooutil.openTip();
              
           }
        });
    }
    /***
    *保存合同模板并打开Word
    *****/
    function SaveTempAndOpenWord(param) {
        /// <summary>保存合同模板并打开Word</summary>
        ///<param name='index'>当前弹框索引</param>
        ///<param name='layero'>当前弹框对象</param>
        ///<param name='saveurl'>保存数据的URL</param>
       
        var iframeWindow = window['layui-layer-iframe' + param.index]
                      , submitID = 'NF-ContTxtTemp-FormSubmit'
                      , submit = param.layero.find('iframe').contents().find('#' + submitID);
        //监听提交
        iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {
            admin.req({
                async: false,//取消异步
                url: '/System/ContTxtTemp/CheckInputValExist',
                data: obj.field,
                type: 'POST',
                done: function (res) {

                    if (res.RetValue > 0) {
                        layer.alert(res.Msg);
                    } else {
                        admin.req({
                            url: param.saveurl,
                            data: obj.field,
                            type: 'POST',
                            success: function (res) {
                                var suc=function(){
                                    table.reload("NF-ContTxtTemp-Index", {
                                        where: { rand: wooutil.getRandom() },
                                        page: { curr: 1 }
                                    });
                                }
                                 
                                wordAddin.OpenWord(res.Data.TempHistId, "TplonreadOrwrite", suc);

                            }});
                       

                    }
                }

            });

            return false;

        });

        submit.trigger('click');
    }
    /***
    *只保存基本信息
    ***/
    function SaveTemp(param) {
        /// <summary>只保存模板基本信息不打开Word</summary>
        ///<param name='index'>当前弹框索引</param>
        ///<param name='layero'>当前弹框对象</param>
        ///<param name='saveurl'>保存数据的URL</param>
        var iframeWindow = window['layui-layer-iframe' + param.index]
                  , submitID = 'NF-ContTxtTemp-FormSubmit'
                  , submit = param.layero.find('iframe').contents().find('#' + submitID);
        //监听提交
        iframeWindow.layui.form.on('submit(' + submitID + ')', function (obj) {

            admin.req({
                async: false,//取消异步
                url: '/System/ContTxtTemp/CheckInputValExist',
                data: obj.field,
                type: 'POST',
                done: function (res) {

                    if (res.RetValue > 0) {
                        layer.alert(res.Msg);
                    } else {

                        wooutil.OpenSubmitForm({
                            url:param.saveurl, //'/System/ContTxtTemp/Save',
                            data: obj.field,
                            table: table,
                            index: param.index,
                            tableId: 'NF-ContTxtTemp-Index'
                        });

                    }



                }

            });



        });

        submit.trigger('click');
        return false;
    }
    /**
    *修改
    **/
    function editFunc(obj, _success) {
        layer.open({
            type: 2
                , title: '修改信息'
                , content: '/System/ContTxtTemp/Build?Id=' + obj.data.Id + "&rand=" + wooutil.getRandom()
                , maxmin: true
                , area: ['60%', '80%']
            // , area: [window.screen.width / 2 + 'px', window.screen.height / 2 + 'px'] //宽高
                , btn: ['编辑模板内容', '保存模板', '取消']
                , btnAlign: 'c'
                , yes: function (index, layero) {
                    SaveTempAndOpenWord({
                        index: index,
                        layero: layero,
                        saveurl: '/System/ContTxtTemp/UpdateSave',
                        tableId: 'NF-ContTxtTemp-Index'

                    });
                   
                },
                btn2: function (index, layero) {
                    SaveTemp({
                        index: index,
                        layero: layero,
                        saveurl: '/System/ContTxtTemp/UpdateSave'
                    })
                    
                },
            success: function (layero, index) {
                layer.full(index);
                wooutil.openTip();
                if (typeof _success === 'function') {
                    setTimeout(function () {
                        _success();
                    }, 500)


                }
            }
        });
    }
    /**
    *模板列表
    **/
    table.on('toolbar(NF-ContTxtTemp-Index)', function (obj) {
        switch (obj.event) {
            case 'add':
                active.add();
                break;
            case 'batchdel':
                active.batchdel();
                break;
            case 'search':
                active.search();
                break;
            case "addWater":
                active.addWater();
                break;
                


        };
    });
    /**
    *行内工具栏
    ***/
    table.on('tool(NF-ContTxtTemp-Index)', function (obj) {
        if (obj.event === 'del') {//删除
            wooutil.deleteInfo({ tableId: "NF-ContTxtTemp-Index", data: obj, url: '/System/ContTxtTemp/Delete' });
        }
        else if (obj.event === 'edit') {//修改
            editFunc(obj,null);
        } else if (obj.event === 'detail') {//查看详情
                openview(obj);
        }
    });
    /**
    *启用禁用
    **/
    form.on('switch(UseVersion)', function (obj) {
        //layer.tips(this.value + ' ' + this.IsSubCompany + '：' + obj.elem.checked, obj.othis);
        var iscomp = obj.elem.checked ? 1 : 0;
        admin.req({
            url: '/System/ContTxtTemp/UpdateField',
            data: { Id: this.value, FieldName: "Tstate", FieldValue: iscomp },
            done: function (res) {
                layer.msg('设置成功！');

            }

        });
    });

    exports('contTxtTempIndex', {});
})