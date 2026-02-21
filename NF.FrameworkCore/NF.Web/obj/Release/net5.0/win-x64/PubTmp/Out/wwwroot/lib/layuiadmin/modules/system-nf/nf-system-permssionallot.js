/**
  权限分配
**/
layui.define(['layer', 'form', 'tree', 'table'], function (exports) {
    var layer = layui.layer
        , $ = layui.$
        , setter = layui.setter
        , form = layui.form
        , tree = layui.tree
        , admin = layui.admin
        , element = layui.element
        , table = layui.table;
    var Id = wooutil.getUrlVar('Id');
    //类型-标识是角色-用户-岗位设置权限
    var setType = wooutil.getUrlVar('setType');

    var loadindex = wooutil.loading();
    //定义列
    var tempcol = [
          { type: 'numbers', fixed: 'left' }
        //{ type: 'checkbox', fixed: 'left' }
        , { field: 'Name', title: '功能名称', fixed: 'left', width: 200 }
        , { field: 'FunIdentify', title: '功能标识', width: 140 }
        , { title: '数据权限', width: 450, templet: "#table-functionallooption" }
        , { field: 'PermssionRang', title: '数据范围', width: 150 }

    ];
    //表格设置
    var tableoption = {
        elem: '#NF-sysfuncpermssionallotTable'
        , url: '/System/SysFunction/GetList?rand=' + wooutil.getRandom()
        , cols: [tempcol]
        , page: true
        , loading: true
        , height: setter.table.height
        , limit: 50 //setter.table.limit
        , limits: setter.table.limits
        , done: function (res) {
            layer.close(loadindex);
        }

    };
    //渲染表格
    var tableInce = table.render(tableoption);

    //菜单树
    admin.req({
        url: '/System/SysFunction/GetMenuTree2?IsUser=' + setType + '&userIdorRoleId=' + Id + '&fpQx=true&rand=' + wooutil.getRandom()
       // url: '/System/SysFunction/GetMenuTree'
        , success: function (res) {
            var treedata = res.Data;
            tree.render({
                elem: '#sysleft_Tree',
                    height: "full-95",
                    data: treedata,
                    click: function (item) {
                                //设置类别
                                //$("input[name='dataType']").val(item.id);
                                tableoption.url = '/System/SysFunction/GetList?modeId=' + item.data.id + '&Id=' + $("input[name=Id]").val();

                                var _setType = $("input[name=setType]").val();
                                if (_setType == 1 || _setType == "1") {
                                    //根据角色模块ID获取对应权限
                                    tableoption.done = RoleFunction(admin, $, item, form);

                                } else if (_setType == 0 || _setType == "0") {
                                    //根据用户模块ID获取对应权限
                                    tableoption.done = UserFunction(admin, $, item, form);
                                }
                                tableInce.reload(tableoption);


                            }
            });
           

        }
    });


    exports('nfsystempermssionallot', {});
});
/**
 * 角色权限树
 * @param {any} admin
 * @param {any} $
 * @param {any} item
 * @param {any} form
 */
function RoleFunction(admin, $, item, form) {
    setTimeout(
        function () {
            admin.req({
                url: '/System/Role/GetRolePermission?rand=' + wooutil.getRandom(),
                data: { roleId: $("input[name=Id]").val(), modeId: item.data.id },
                done: function (res) {
                    $.each(res.Data, function (n, v) {
                        $("input:radio[funcid=" + v.FuncId + "][value=" + v.FuncType + "]").eq(0).attr("checked", "checked");
                        if (v.FuncType == "2") { //如果是机构
                            $("input[name=modeIds_" + v.FuncId + "]").val(v.DeptIds);
                        }

                    });
                    form.render();
                }
            })
        }
    , 1000);
}

/**
 * 用户权限树
 * @param {any} admin
 * @param {any} $
 * @param {any} item
 * @param {any} form
 */
function UserFunction(admin, $, item, form) {
    setTimeout(
        function () {
            admin.req({
                url: '/System/UserInfor/GetUserPermission?rand=' + wooutil.getRandom(),
                data: { userId: $("input[name=Id]").val(), modeId: item.data.id },
                done: function (res) {
                    $.each(res.Data, function (n, v) {
                        $("input:radio[funcid=" + v.FuncId + "][value=" + v.FuncType + "]").eq(0).attr("checked", "checked");
                        if (v.FuncType == "2") { //如果是机构
                            $("input[name=modeIds_" + v.FuncId + "]").val(v.DeptIds);
                        }
                    });
                    form.render();
                }
            })
        }, 1000);
}
