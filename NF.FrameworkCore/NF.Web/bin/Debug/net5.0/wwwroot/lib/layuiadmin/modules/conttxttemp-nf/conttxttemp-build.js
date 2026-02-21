layui.define(['table', 'form', 'commonnf', 'formSelects', 'tree', 'checkbox'], function (exports) {
    var $ = layui.$
  , table = layui.table
  , setter = layui.setter
  , admin = layui.admin
  , form = layui.form
  , formSelects = layui.formSelects
  , commonnf = layui.commonnf
  , tree = layui.tree
  , checkbox = layui.checkbox;
    //选择的标的字段
    var selfields = [];
    //修改值
    var upfields = [];
    var isupdate = false;//表格是否有修改
    //模板类别
    //commonnf.getdatadic({ dataenum: 1, selectEl: "#TepType" });
    //文本类别
    commonnf.getdatadic({ dataenum: 16, selectEl: "#TextType" });
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
                                   toolEvent.reloadFdTable();
                               }
                           });
            }
        });
    };
    /**
    *所属机构
    ***/
    function initDepts(dataval) {
        formSelects.config('DeptIds', {
            direction: 'down'
            , success: function (id, url, searchVal, result) {
                formSelects.value('DeptIds', dataval);
            }
        }).data('DeptIds', 'server', {
            url: '/WorkFlow/FlowTemp/GetFlowDeptTree'
            , tree: {
                nextClick: function (id, item, callback) {
                }
            }
        });

    }
    /**
   *合同类别（文本类别）
   ***/
    function initContTxtClass(dataval) {
        formSelects.config('TepTypes', {
            direction: 'down'
            , success: function (id, url, searchVal, result) {
                formSelects.value('TepTypes', dataval);
            }
        }).data('TepTypes', 'server', {
            url: '/WorkFlow/FlowTemp/GetFlowContTxtClassTree?objEnum=1'
            , tree: {
                nextClick: function (id, item, callback) {
                }
            }
        });

    }

    /**
   *修改时赋值
   **/
    var $Id = wooutil.getUrlVar('Id');
    if ($Id !== "" && $Id !== undefined) {
        admin.req({
            url: '/System/ContTxtTemp/ShowView',
            data: { Id: $Id, rand: wooutil.getRandom() },
            done: function (res) {
                form.val("NF-ContTxtTemp-BuildForm", res.Data);
                //字典类别
                initDepts(res.Data.DeptIdsArray);
                initContTxtClass(res.Data.TepTypesArray);
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

    } else {
        initDepts([]);
        TreeInit();
        initContTxtClass([]);
    }
    var tableIni = function () {
        var $Htid = $("#HistId").val();
        var $fdType=$("#FieldTypeVal").val();
        var $bcId=$("#SelBcId").val();
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

    /**
   *是否显示标的
   ***/
    form.on('checkbox(ShowSubMatter)', function (data) {
        if (data.elem.checked) {//需要显示标的
            $("#BiaoDi").removeClass("layui-hide");
            $("#ShowSub").val("1");
            tableIni();
        } else {
            $("#BiaoDi").addClass("layui-hide");
        }


    });
    /**
    *标的显示格式
    ***/
    form.on('radio(FieldType)', function (data) {
        if (data.value == 0) {
            $(".bcCategory").addClass("layui-hide");
        } else {
            $(".bcCategory").removeClass("layui-hide");
            TreeInit();
        }
        toolEvent.reloadFdTable();
        $("#FieldTypeVal").val(data.value);
    });
    /***
    *事件
    ***/
    var toolEvent = {
        /// <summary>重新加载字段表格</summary>
        reloadFdTable:function(){
            var _url = '/ContractDraft/ContTxtTempAndSubField/GetFields?tempId=' + $("#HistId").val()
                                               + "&fdType=" + $('input[name="FieldType"]:checked ').val()
                                               + "&bcId=" + $("#SelBcId").val()
                                               + '&rand=' + wooutil.getRandom();
            table.reload("NF-ContSubFildTemp", {
                url: _url
            });
        },
        renderChk: function (data) {
            /// <summary>初始化选择CheckBox</summary>
            selfields = [];
            layer.open({
                type: 1,
                title: '请选择标的显示字段',
                area: ['635px', '385px'],
                content: '<div id="subFiledbox"></div>',
                btn: ['确认', '取消'],
                yes: function (index, layero) {
                    //layer.alert(selfields.length);
                    admin.req({
                        url: '/ContractDraft/ContTxtTempAndSubField/AddSubTmpField',
                        type: 'POST',
                        data: {
                            selFields: selfields,
                            fieldType: $("#FieldTypeVal").val(),
                            bcId: $("#SelBcId").val(),
                            tempHisId: $("#HistId").val()
                        }, success: function (res) {
                           
                            tableIni();
                            layer.close(index);
                            return layer.msg('保存成功', { icon: 1 });
                            
                        }
                    });
                    return false;
                },
                success: function (layero, index) {
                    $("#subFiledbox").children("li").remove();
                    checkbox({
                        elem: "#subFiledbox"
                        , nodes: data
                        , click: function (node) {
                            var selobj = {};
                            selobj.Id = node.id;
                            selobj.Name = node.name;
                            if (node.on) {
                                selfields.push(selobj);
                            } else {
                                //取消选择
                                for (var i = 0; i < selfields.length; i++) {
                                    if (selfields[i].Id === selobj.Id) {
                                        selfields.splice(i, 1);
                                    }

                                }
                            }

                        }
                        , del: function (node) {
                            layer.alert("不允许删除");
                        }
                    });
                }
            });
        },
        add: function () {
            ///<summary>新增</summary>
            admin.req({
                url: '/ContractDraft/ContTxtTempAndSubField/GetSubChkFields?rand=' + wooutil.getRandom(),
                data: {
                    tempId: $("#HistId").val(),
                    fieldType: $('input[name="FieldType"]:checked ').val(),
                    bcId: $("#SelBcId").val()
                }, success: function (res) {
                    for (var i = 0; i < res.Data.length; i++) {
                        if (res.Data[i].on) {
                            var tobj = {};
                            tobj.Id = res.Data[i].id;
                            tobj.Name = res.Data[i].name;
                            
                            selfields.push(tobj);
                        }

                    }
                    toolEvent.renderChk(res.Data);
                }
            });

        },
        sortFun: function (isup,oid) {
            ///<summary>上移下移</summary>
            admin.req({
                url: '/ContractDraft/ContTxtTempAndSubField/SortField?rand=' + wooutil.getRandom(),
                data: {
                    tempId: $("#HistId").val(),
                    fieldType: $('input[name="FieldType"]:checked ').val(),
                    bcId: $("#SelBcId").val(),
                    Id: oid,
                    up: isup
                },success: function (res)
                {
                    tableIni();
                }
            });

        },
        savefield: function () {
            ///<summary>保存</summary>
            if (isupdate) {
                admin.req({
                    url: '/ContractDraft/ContTxtTempAndSubField/SaveData',
                    data: { fields: upfields },
                    type: 'POST',
                    done: function (res) {
                        //清空变量，防止重复提交
                        upfields = [];
                        isupdate = false;
                        return layer.msg('保存成功', { icon: 1 });
                    }
                });
            } else {
                return layer.msg('数据没有任何修改！', { icon: 5 });
            }
        }
        
    }
    /**
    *标的字段列表
    ***/
    table.on('toolbar(NF-ContSubFildTemp)', function (obj) {
        switch (obj.event) {
            case 'add'://新增
                toolEvent.add();
                break;
            case "saveAll"://保存
                toolEvent.savefield();
                break;
        };
    });
    /**
    *操作栏
    **/
    table.on('tool(NF-ContSubFildTemp)', function (obj) {
        switch (obj.event) {
            case 'up'://上移
                toolEvent.sortFun(true, obj.data.Id);
                break;
            case "down"://下移
                toolEvent.sortFun(false, obj.data.Id);
                break;
            case "del"://删除
                wooutil.deleteInfo({ tableId: "NF-ContSubFildTemp", data: obj, url: '/ContractDraft/ContTxtTempAndSubField/Delete' });
                break;
        };
    });
    /**
    *表格编辑
    **/
    table.on('edit(NF-ContSubFildTemp)', function (obj) {
        isupdate = true;
        var data = obj.data;
        //修改值
        for (var i = 0; i < upfields.length; i++) {
            if (upfields[i].Id === obj.data.Id) {
                upfields.splice(i, 1);
            }
        }
        
        upfields.push(obj.data);
       
    });



    exports('contTxtTempBuild', {});
})