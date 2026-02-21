/**
*业务品类
**/
layui.define(['table', 'tree', 'form'], function (exports) {
    var layer = layui.layer
        , $ = layui.$
        , setter = layui.setter
        , admin = layui.admin
        , tree = layui.tree
        , form = layui.form;
    //---------------加载树begin------------------------------------------------
  function TreeInit(){
    admin.req({
        url: '/Business/BusinessCategory/GetBcCateTreeData?rand=' + wooutil.getRandom()
        , success: function (res) {
            var treedata = res.Data;
            tree.render(
                       {
                           elem: '#BcCategory_Tree',
                           height: "full-95",
                           data: treedata
                           , id:'bctree'
                           , click: function (obj) {
                               //layer.alert(JSON.stringify(obj.data));
                               admin.req({
                                   url: '/Business/BusinessCategory/GetInfoById?Id=' + obj.data.id
                                  , success: function (res) {
                                      if (res.Data.Pid === 0) {//一级品类
                                          active.cateShow();
                                          $("input[name=CaName]").val(res.Data.Name);
                                          $("input[name=CaCode]").val(res.Data.Code);
                                          $("input[name=Id]").val(res.Data.Id);
                                          $("#tempPid").val(res.Data.Id);
                                          $("#tempPName").val(res.Data.Name);
                                      } else {
                                          active.chidCateShow();
                                          $("#tempPid").val(res.Data.Id);
                                          $("#tempPName").val(res.Data.Name);
                                          $("input[name=Pid]").val(res.Data.Pid);
                                          $("input[name=PName]").val(res.Data.PName);
                                          $("input[name=ChidName]").val(res.Data.Name);
                                          $("input[name=ChidCode]").val(res.Data.Code);
                                          $("input[name=Id]").val(res.Data.Id);
                                      }

                                  }
                                  
                               });
                           }
                       });
        }
    });
  }
  TreeInit();
    //---------------加载树end------------------------------------------------
   
    //按钮事件
    var active = {
        cateShow:function(){//一级菜单
            $("#NF-BcCategoryForm").removeClass("layui-hide");
            $("#NF-BcChirdCategoryForm").addClass("layui-hide");
            $("#savebccate").attr("save-btn", "btnCaSave");
            $("input[name=CaName]").val("");
            $("input[name=CaCode]").val("");
           // $("input[name=Id]").val(0);
        },
        chidCateShow:function(){//子菜单
            $("#NF-BcCategoryForm").addClass("layui-hide");
            $("#NF-BcChirdCategoryForm").removeClass("layui-hide");
            $("#savebccate").attr("save-btn", "btnChidCaSave");
            $("input[name=ChidName]").val("");
            $("input[name=ChidCode]").val("");
            //$("input[name=Id]").val(0);
            
        },
        hideForm:function(){//保存完毕关闭所有form
            $("#NF-BcChirdCategoryForm").addClass("layui-hide");
            $("#NF-BcCategoryForm").addClass("layui-hide");
            $("input[name=Id]").val(0);
        },
        add: function () {//点击类别
            active.cateShow();
            $("input[name=Id]").val(0);
        },
        addchild: function () {//新增子类别
            active.chidCateShow();
            $("input[name=Pid]").val($("#tempPid").val());
            $("input[name=PName]").val($("#tempPName").val());
            $("input[name=Id]").val(0);
           
        },
        saveCate: function () {//保存父类列表
          
                var submit = $("#btnCaSave");
            form.on('submit(btnCaSave)', function (data) {
                var resname = wooutil.UniqueValObj({
                    url: '/Business/BusinessCategory/CheckInputValExist',
                    fieldName: 'Name',
                    inputVal: data.field.CaName,
                    currId: $("input[name=Id]").val()
                });
                if (resname) {
                    return layer.msg('此名称已经存在！');
                }
                var rescode = wooutil.UniqueValObj({
                    url: '/Business/BusinessCategory/CheckInputValExist',
                    fieldName: 'Code',
                    inputVal: data.field.CaCode,
                    currId: $("input[name=Id]").val()
                });
                if (rescode) {
                    return layer.msg('此编号已经存在！');
                }
                    admin.req({
                        url: '/Business/BusinessCategory/CateSave'
                        , data: {
                            Name: data.field.CaName,
                            Code: data.field.CaCode,
                            Pid: 0,
                            Id: $("input[name=Id]").val()
                        }
                        , type: 'POST'
                        , success: function (res) {
                            TreeInit();
                            active.hideForm();
                        }
                    });


                    return false;
                });
                submit.trigger('click');
        },
        save: function () {
            var savebtn = $("#savebccate").attr("save-btn");
            if (savebtn === "btnCaSave") {
                active.saveCate();
            } else {
                active.savechild();
            }
            
        }, savechild: function () {//保存子类
            
            var submit = $("#btnChidCaSave");
            form.on('submit(btnChidCaSave)', function (data) {
                var resname = wooutil.UniqueValObj({
                    url: '/Business/BusinessCategory/CheckInputValExist',
                    fieldName: 'Name',
                    inputVal: data.field.ChidName,
                    currId: $("input[name=Id]").val()
                });
                if (resname) {
                    return layer.msg('此名称已经存在！');
                }
                var rescode = wooutil.UniqueValObj({
                    url: '/Business/BusinessCategory/CheckInputValExist',
                    fieldName: 'Code',
                    inputVal: data.field.ChidCode,
                    currId: $("input[name=Id]").val()
                });
                if (rescode) {
                    return layer.msg('此编号已经存在！');
                }
                admin.req({
                    url: '/Business/BusinessCategory/CateSave'
                   , data: {
                       Name: data.field.ChidName,
                       Code: data.field.ChidCode,
                       Pid: data.field.Pid,
                       Id: $("input[name=Id]").val()

                   }
                    , type: 'POST'
                    , success: function (res) {
                        TreeInit();
                        active.hideForm();
                    }
                });


                return false;
            });
            submit.trigger('click');
        },
        delCate: function () {//删除
            layer.confirm('确定要删除吗？', { icon: 3, title: '提示信息' }, function (index) {
                admin.req({
                    url: '/Business/BusinessCategory/Delete'
                   , data: {
                       Id: $("input[name=Id]").val()
                   }
                    , success: function (res) {
                        layer.close(index);
                        TreeInit();
                        active.hideForm();
                    }
                });
            });
            
        }

    };
    $('.layui-btn.bccate-btn').on('click', function () {
        var type = $(this).data('type');
        active[type] ? active[type].call(this) : '';
    });


    exports('businessCategory', {});
});