/***
*业务品类树
****/
layui.define(['layer', 'tree', 'table'], function (exports) {
    var $ = layui.jquery;
    var layer = layui.layer;
    var tree = layui.tree;
    var admin = layui.admin;
    var table = layui.table;
    var arr = [];
    var nfBcTree = {
        //渲染
        render: function (param) {
            if (!nfBcTree.checkParam(param)) {
                return false;
            }
            else {
                nfBcTree.init(param);
            }
        },
        init: function (param) {
            $(param.el).addClass("layui-hide");
            nfBcTree.treeInit(param);
        },
        getIds: function (json) {//获取ID
            for (var i = 0; i < json.length; i++) {
                arr.push(json[i].id);
                if (json[i].children != null && json[i].children != undefined) {
                    if (json[i].children.length > 0) {
                        nfBcTree.getIds(json[i].children);
                    }
                }
            }
            return arr;
        },
        showTree:function(param) {//显示树
            if (!nfBcTree.checkShowParam(param)) {
                return false;
            } else {
                arr = [];
                $(param.el).removeClass("layui-hide");
                layer.open({
                    title: '业务品类',
                    offset: 'l',
                    area: ['260px', '80%'],
                    type: 1,
                    shade: 0,
                    shadeClose: true,
                    btn: ['确定', '取消'],
                    content: $(param.el),
                    yes: function (index, layero) {

                        var checkData = tree.getChecked(param.treeId);
                        var arrids = nfBcTree.getIds(checkData[0].children);
                        //layer.alert(JSON.stringify(arrIds));
                        $(param.hideFild).val(arr.toString());//存储起来供查询使用
                        table.reload(param.tableId, {
                            page: { curr: 1 }
                        , where: {
                            cateIds: arr.toString()
                        }
                        });
                        $(param.el).addClass("layui-hide");
                        layer.close(index);

                    },
                    btn2: function (index, layero) {
                        $(param.el).addClass("layui-hide");
                        layer.close(index);
                    },
                    cancel: function (index, layero) {
                        $(param.el).addClass("layui-hide");
                    }
                });
            }
        },
        checkParam: function (param) {
            if (param.el == "" || param.el == undefined) {
                layer.msg('nfBcTree目录控件参数el不能为空', { icon: 5 });
                return false;
            }
            if (param.treeEl == "" || param.treeEl == undefined) {
                layer.msg('nfBcTree目录控件参数treeEl不能为空', { icon: 5 });
                return false;
            }
            if (param.treeId == "" || param.treeId == undefined) {//菜单ID
                layer.msg('nfBcTree目录控件参数treeId不能为空', { icon: 5 });
                return false;
            } if (param.showf == "" || param.showf == undefined) {//是否显示非业务品类
                param.showf = false;
            }


            return true;
        },
        checkShowParam:function(param) {
            if (param.el == "" || param.el == undefined) {
                layer.msg('nfBcTree目录控件参数el不能为空', { icon: 5 });
                return false;
            }
            if (param.tableId == "" || param.tableId == undefined) {
                layer.msg('nfBcTree目录控件参数tableId不能为空', { icon: 5 });
                return false;
            }
            if (param.treeId == "" || param.treeId == undefined) {//菜单ID
                layer.msg('nfBcTree目录控件参数treeId不能为空', { icon: 5 });
                return false;
            }
            if (param.hideFild == "" || param.hideFild == undefined) {//隐藏文本框，存储选择值
                layer.msg('nfBcTree目录控件参数hideFild不能为空', { icon: 5 });
                return false;
            }
            return true;

        },
        treeInit: function (param) {//初始树形结构
            admin.req({
                url: '/Business/BusinessCategory/GetBcCateTreeData'
                , success: function (res) {
                    var treedata = res.Data;
                    if (param.showf) {
                        treedata.splice(0, 0, {
                            title: '非业务品类'
                          , id: -1
                          , spread: true
                        }
                        );
                    }
                    var rootdata = [{
                        title: '全部类别'
                      , id: 0
                      , checked: true
                      , spread: true,
                        children: treedata
                    }];
                    tree.render({
                        elem: param.treeEl,
                        height: "full-95",
                        data: rootdata //treedata
                                   , id: param.treeId
                                   , showCheckbox: true
                                   , click: function (obj) {


                                   }
                    });
                }
            });
        }

    }



    exports('nfBcTree', nfBcTree);
});