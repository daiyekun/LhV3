layui.define(['table', 'form'], function (exports) {
    var $ = layui.$
        //, table = layui.table
        //, setter = layui.setter
        , layer = layui.layer
        , admin = layui.admin
        , form = layui.form;

    var selectnfitem = {
        selectUserItem: function (param) {
            /// <summary>
            /// 选择用户
            /// </summary>        
            /// <param name="elem" type="String">触发此动作的文本框</param>
            /// <param name="tableSelect" type="Object">TableSelect对象</param>
            /// <param name="hide_elem" type="number">隐藏文本框ID-主要为了存储后台数据库</param>
            /// <param name="suc" type="Function">回调函数，用于扩展业务需要除名称和ID以外的内容</param>
            /// <param name="seltype" type="string">回调函数，用于扩展业务需要除名称和ID以外的内容</param>
            ///<param name="noval" type="string">标识不赋值，交给回调函数处理</param>
            var _seltype = 'radio';
            if (param.seltype != undefined) {
                _seltype = param.seltype;
            }
            param.tableSelect.render({
                elem: param.elem,
                searchKey: 'keyword',	
                searchPlaceholder: '关键词搜索',	
                table: {	
                    url: '/System/UserInfor/GetList?rand=' + wooutil.getRandom(),
                    cols: [[
                        { type: 'numbers', fixed: 'left' }
                        , { type: _seltype, fixed: 'left' }
                        , { field: 'Name', title: '用户名称', minWidth: 150, fixed: 'left' }
                        , { field: 'DeptName', title: '所属机构', width: 130 }
                        , { field: 'DisplyName', title: '显示名称', width: 110 }
                        , { field: 'SexDic', width: 100, title: '性别' }
                        , { field: 'Age', width: 100, title: '年龄', hide: true }
                        , { field: 'Ustart', width: 80, title: '状态', hide: true, templet: '#userstateTpl', unresize: true }
                        , { field: 'Tel', width: 100, title: '电话' }
                        , { field: 'Mobile', width: 120, title: '手机' }
                        , { field: 'Email', width: 120, title: 'E-Mail', hide: true }
                    ]]
                },
                done: function (elem, data) {
                    if (data.isclear) {
                        $(param.elem).val('');
                        $(param.hide_elem).val('');
                    } else {
                        if (param.noval == undefined) {
                        $(param.elem).val(data.data[0].DisplyName);
                           
                        $(param.hide_elem).val(data.data[0].Id);
                        }
                        if (typeof param.suc === 'function') {
                           param.suc(data.data);
                        }
                       
                       
                    }
                }
            });
        },
        selectUserItemQ: function (param) {
            /// <summary>
            /// 选择用户
            /// </summary>        
            /// <param name="elem" type="String">触发此动作的文本框</param>
            /// <param name="tableSelect" type="Object">TableSelect对象</param>
            /// <param name="hide_elem" type="number">隐藏文本框ID-主要为了存储后台数据库</param>
            /// <param name="suc" type="Function">回调函数，用于扩展业务需要除名称和ID以外的内容</param>
            /// <param name="seltype" type="string">回调函数，用于扩展业务需要除名称和ID以外的内容</param>
            ///<param name="noval" type="string">标识不赋值，交给回调函数处理</param>
            var _seltype = 'radio';
            if (param.seltype != undefined) {
                _seltype = param.seltype;
            }
            param.tableSelect.render({
                elem: param.elem,
                searchKey: 'keyword',
                searchPlaceholder: '关键词搜索',
                table: {
                    url: '/System/UserInfor/GetList?ISQy=' + 1 +'&rand=' + wooutil.getRandom(),
                    cols: [[
                        { type: 'numbers', fixed: 'left' }
                        , { type: _seltype, fixed: 'left' }
                        , { field: 'Name', title: '用户名称', minWidth: 150, fixed: 'left' }
                        , { field: 'DeptName', title: '所属机构', width: 130 }
                        , { field: 'DisplyName', title: '显示名称', width: 110 }
                        , { field: 'SexDic', width: 100, title: '性别' }
                        , { field: 'Age', width: 100, title: '年龄', hide: true }
                        , { field: 'Ustart', width: 80, title: '状态', hide: true, templet: '#userstateTpl', unresize: true }
                        , { field: 'Tel', width: 100, title: '电话' }
                        , { field: 'Mobile', width: 120, title: '手机' }
                        , { field: 'Email', width: 120, title: 'E-Mail', hide: true }
                    ]]
                },
                done: function (elem, data) {
                    if (data.isclear) {
                        $(param.elem).val('');
                        $(param.hide_elem).val('');
                    } else {
                        if (param.noval == undefined) {
                            $(param.elem).val(data.data[0].DisplyName);

                            $(param.hide_elem).val(data.data[0].Id);
                        }
                        if (typeof param.suc === 'function') {
                            param.suc(data.data);
                        }


                    }
                }
            });
        },
        selectRoleUserItem: function (param) {
            /// <summary>
            /// 选择用户 在“角色管理”增加“项目负责人”角色
            /// 选择角色为项目负责人的用户
            /// </summary>        
            /// <param name="elem" type="String">触发此动作的文本框</param>
            /// <param name="tableSelect" type="Object">TableSelect对象</param>
            /// <param name="hide_elem" type="number">隐藏文本框ID-主要为了存储后台数据库</param>
            /// <param name="suc" type="Function">回调函数，用于扩展业务需要除名称和ID以外的内容</param>
            /// <param name="seltype" type="string">回调函数，用于扩展业务需要除名称和ID以外的内容</param>
            ///<param name="noval" type="string">标识不赋值，交给回调函数处理</param>
            var _seltype = 'radio';
            if (param.seltype != undefined) {
                _seltype = param.seltype;
            }
            param.tableSelect.render({
                elem: param.elem,
                searchKey: 'keyword',
                searchPlaceholder: '关键词搜索',
                table: {
                    url: '/System/UserInfor/GetList1?rand=' + wooutil.getRandom(),
                    cols: [[
                        { type: 'numbers', fixed: 'left' }
                        , { type: _seltype, fixed: 'left' }
                        , { field: 'Name', title: '用户名称', minWidth: 150, fixed: 'left' }
                        , { field: 'DeptName', title: '所属机构', width: 130 }
                        , { field: 'DisplyName', title: '显示名称', width: 110 }
                        , { field: 'SexDic', width: 100, title: '性别' }
                        , { field: 'Age', width: 100, title: '年龄', hide: true }
                        , { field: 'Ustart', width: 80, title: '状态', hide: true, templet: '#userstateTpl', unresize: true }
                        , { field: 'Tel', width: 100, title: '电话' }
                        , { field: 'Mobile', width: 120, title: '手机' }
                        , { field: 'Email', width: 120, title: 'E-Mail', hide: true }
                    ]]
                },
                done: function (elem, data) {
                    if (data.isclear) {
                        $(param.elem).val('');
                        $(param.hide_elem).val('');
                    } else {
                        if (param.noval == undefined) {
                            $(param.elem).val(data.data[0].DisplyName);
                            $(param.hide_elem).val(data.data[0].Id);
                        }
                        if (typeof param.suc === 'function') {
                            param.suc(data.data);
                        }
                    }
                }
            });
        },
            selectScheduItem: function (param) {
                /// <summary>
                /// 选择进度管理
                /// </summary>        
                /// <param name="elem" type="String">触发此动作的文本框</param>
                /// <param name="tableSelect" type="Object">TableSelect对象</param>
                /// <param name="hide_elem" type="number">隐藏文本框ID-主要为了存储后台数据库</param>
                /// <param name="suc" type="Function">回调函数，用于扩展业务需要除名称和ID以外的内容</param>
                /// <param name="seltype" type="string">回调函数，用于扩展业务需要除名称和ID以外的内容</param>
                ///<param name="noval" type="string">标识不赋值，交给回调函数处理</param>
                var _seltype = 'radio';
                if (param.seltype != undefined) {
                    _seltype = param.seltype;
                }
                param.tableSelect.render({
                    elem: param.elem,
                    searchKey: 'keyword',
                    searchPlaceholder: '关键词搜索',
                    table: {
                        url: '/Schedule/ScheduleManagement/GetList?search=1&rand=' + wooutil.getRandom(),
                        cols: [[
                            { type: 'numbers', fixed: 'left' }
                            , { type: _seltype, fixed: 'left' }
                            , { field: 'ScheduleName', title: '进度名称', minWidth: 150, fixed: 'left' }
                            , { field: 'ScheduleSer', title: '任务关键字', width: 130 }
                            , { field: 'PriorityDic', title: '优先级', width: 110 }
                            , { field: 'ScheduleAttributionDic', width: 100, title: '任务归属' }
                            , { field: 'ScheduleDuixiangName', width: 100, title: '任务对象' }
                            , { field: 'DesigneeName', width: 100, title: '指派给' }
                            , { field: 'StalkerName', width: 120, title: '跟踪者' }
                        ]]
                    },
                    done: function (elem, data) {
                        if (data.isclear) {
                            $(param.elem).val('');
                            $(param.hide_elem).val('');
                        } else {
                            if (param.noval == undefined) {
                                $(param.elem).val(data.data[0].ScheduleName);
                                $(param.hide_elem).val(data.data[0].Id); 
                            }
                            if (typeof param.suc === 'function') {
                                param.suc(data.data);
                            }


                        }
                    }
                });
            },

        selectCompItem: function (param) {
            /// <summary>
            /// 选择合同对方
            /// </summary>        
            /// <param name="elem" type="String">触发此动作的文本框</param>
            /// <param name="tableSelect" type="Object">TableSelect对象</param>
            /// <param name="selitem" type="bool">是否是选择框</param>
            /// <param name="ctype" type="number">0:客户，1：供应商，2：合同对方</param>
            /// <param name="hide_elem" type="number">隐藏文本框ID-主要为了存储后台数据库</param>
            /// <param name="suc" type="Function">回调函数，用于扩展业务需要除名称和ID以外的内容</param>
            var _url;
            var _selitem = false;
            if(param.selitem){
                _selitem=param.selitem;
            }
            if (param.ctype == 0) {
                _url='/Company/Customer/GetList?selitem='+_selitem;
            } else if (param.ctype == 1) {
                _url = '/Company/Supplier/GetList?selitem=' + _selitem;
            } else if (param.ctype == 2) {
                _url = '/Company/Other/GetList?selitem=' + _selitem;
            }else{
               return layer.alert("传递参数ctype未知");
             }
            param.tableSelect.render({
                elem: param.elem,
                searchKey: 'keyWord',
                searchPlaceholder: '关键词搜索',	
                table: {	
                    url:_url+'&rand='+ wooutil.getRandom(),
                    cols: [[
                         { type: 'numbers', fixed: 'left' }
                        , { type: 'radio', fixed: 'left' }
                        , { field: 'Name', title: '名称', minWidth: 150, fixed: 'left'}
                        , { field: 'Code', title: '编号', width: 130 }
                        , { field: 'CompanyTypeClass', title: '类别', width: 120 }
                        , { field: 'FirstContact', width: 120, title: '主要联系人' }
                      
                    ]]
                },
                done: function (elem, data) {
                    if (data.isclear) {
                        $(param.elem).val('');
                        $(param.hide_elem).val('');
                    } else {
                        $(param.elem).val(data.data[0].Name);
                        $(param.hide_elem).val(data.data[0].Id);
                        if (typeof param.suc === 'function') {
                            param.suc(data.data[0]);
                        }
                    }
                }
            });
        },
        //案件纠纷专用 查询客户与供应商
        selectKhGys: function (param) {
            /// <summary>
            /// 选择合同对方
            /// </summary>        
            /// <param name="elem" type="String">触发此动作的文本框</param>
            /// <param name="tableSelect" type="Object">TableSelect对象</param>
            /// <param name="selitem" type="bool">是否是选择框</param>
            /// <param name="ctype" type="number">0:客户，1：供应商，2：合同对方</param>
            /// <param name="hide_elem" type="number">隐藏文本框ID-主要为了存储后台数据库</param>
            /// <param name="suc" type="Function">回调函数，用于扩展业务需要除名称和ID以外的内容</param>
            var _url;
            var _selitem = false;
            if (param.selitem) {
                _selitem = param.selitem;
            }
            if (param.ctype == 0) {
                _url = '/CaseManagement/Ajgl/GetList?selitem=' + _selitem;
            } else {
                return layer.alert("传递参数ctype未知");
            }
            param.tableSelect.render({
                elem: param.elem,
                searchKey: 'keyWord',
                searchPlaceholder: '关键词搜索',
                table: {
                    url: _url + '&rand=' + wooutil.getRandom(),
                    cols: [[
                        { type: 'numbers', fixed: 'left' }
                        , { type: 'radio', fixed: 'left' }
                        , { field: 'Name', title: '名称', minWidth: 150, fixed: 'left' }
                        , { field: 'Code', title: '编号', width: 130 }
                        , { field: 'CompanyTypeClass', title: '类别', width: 120 }
                        , { field: 'FirstContact', width: 120, title: '主要联系人' }

                    ]]
                },
                done: function (elem, data) {
                    if (data.isclear) {
                        $(param.elem).val('');
                        $(param.hide_elem).val('');
                    } else {
                        $(param.elem).val(data.data[0].Name);
                        $(param.hide_elem).val(data.data[0].Id);
                        if (typeof param.suc === 'function') {
                            param.suc(data.data[0]);
                        }
                    }
                }
            });
        },
       //案件纠纷专用 收款合同预付款合同
        selectSkFk: function (param) {
            /// <summary>
            /// 选择付款合同
            /// </summary>        
            /// <param name="elem" type="String">触发此动作的文本框</param>
            /// <param name="tableSelect" type="Object">TableSelect对象</param>
            /// <param name="selitem" type="bool">是否是选择框</param>
            /// <param name="hide_elem" type="number">隐藏文本框ID-主要为了存储后台数据库</param>
            /// <param name="suc" type="Function">回调函数，用于扩展业务需要除名称和ID以外的内容</param>

            var _selitem = false;
            if (param.selitem) {
                _selitem = param.selitem;
            }
            var _url = '/CaseManagement/Ajgl/GetSelectList?selitem=' + _selitem;
            param.tableSelect.render({
                elem: param.elem,
                searchKey: 'keyWord',
                searchPlaceholder: '关键词搜索',
                table: {
                    url: _url + '&rand=' + wooutil.getRandom(),
                    cols: [[
                        { type: 'numbers', fixed: 'left' }
                        , { type: 'radio', fixed: 'left' }
                        , { field: 'Name', title: '合同名称', minWidth: 150, fixed: 'left' }
                        , { field: 'Code', title: '编号', width: 130 }
                        , { field: 'ContAmThod', title: '合同金额', width: 120 }
                        , { field: 'FinanceTypeDesc', title: '资金性质', width: 120 }
                        , { field: 'StateDic', title: '合同状态', width: 120 }
                        , { field: 'CreateUserName', title: '创建人', width: 120 }
                        , { field: 'CreateDateTime', title: '创建时间', width: 120 }
                    ]]
                },
                done: function (elem, data) {
                    if (data.isclear) {
                        $(param.elem).val('');
                        $(param.hide_elem).val('');
                    } else {
                        $(param.elem).val(data.data[0].Name);
                        $(param.hide_elem).val(data.data[0].Id);
                        if (typeof param.suc === 'function') {
                            param.suc(data.data[0]);
                        }
                    }
                }
            });
        },
        //案件纠纷专用 案件明细
        selectAjmx: function (param) {
            /// <summary>
            /// 案件明细
            /// </summary>        
            /// <param name="elem" type="String">触发此动作的文本框</param>
            /// <param name="tableSelect" type="Object">TableSelect对象</param>
            /// <param name="selitem" type="bool">是否是选择框</param>
            /// <param name="hide_elem" type="number">隐藏文本框ID-主要为了存储后台数据库</param>
            /// <param name="suc" type="Function">回调函数，用于扩展业务需要除名称和ID以外的内容</param>

            var _selitem = false;
            if (param.selitem) {
                _selitem = param.selitem;
            }
            var _url = '/CaseManagement/Ajgl/GetAjmx?selitem=' + _selitem;
            param.tableSelect.render({
                elem: param.elem,
                searchKey: 'keyWord',
                searchPlaceholder: '关键词搜索',
                table: {
                    url: _url + '&rand=' + wooutil.getRandom(),
                    cols: [[
                        { type: 'numbers', fixed: 'left' }
                        , { type: 'radio', fixed: 'left' }
                        , { field: 'Name', title: '名称', minWidth: 150, fixed: 'left' }
                        , { field: 'Code', title: '编号', width: 130 }
                        , { field: 'UrgentName', title: '紧急程度', width: 120 }
                        , { field: 'ContIdName', title: '相关合同', width: 120 }
                        , { field: 'Amount', title: '涉及金额(万元)', width: 120 }
                        , { field: 'CName', title: '创建人', width: 120 }
                        , { field: 'CreateDateTime', title: '创建时间', width: 120 }
                    ]]
                },
                done: function (elem, data) {
                    if (data.isclear) {
                        $(param.elem).val('');
                        $(param.hide_elem).val('');
                        $(param.hide_elems).val('');
                        $(param.hide_Je).val('');
                    } else {
                        $(param.elem).val(data.data[0].Name);
                        $(param.hide_elem).val(data.data[0].Id);
                        $(param.hide_elems).val(data.data[0].Code);
                        $(param.hide_Je).val(data.data[0].Amount);
                        if (typeof param.suc === 'function') {
                            param.suc(data.data[0]);
                        }
                    }
                }
            });
        },
        
        selectProjItem: function (param) {
            /// <summary>
            /// 选择项目
            /// </summary>        
            /// <param name="elem" type="String">触发此动作的文本框</param>
            /// <param name="tableSelect" type="Object">TableSelect对象</param>
            /// <param name="selitem" type="bool">是否是选择框</param>
            /// <param name="hide_elem" type="number">隐藏文本框ID-主要为了存储后台数据库</param>
            /// <param name="suc" type="Function">回调函数，用于扩展业务需要除名称和ID以外的内容</param>
            
            var _selitem = false;
            if (param.selitem) {
                _selitem = param.selitem;
            }
            var _url = '/Project/ProjectManager/GetList?selitem=' + _selitem;
            param.tableSelect.render({
                elem: param.elem,
                searchKey: 'keyWord',
                searchPlaceholder: '关键词搜索',
                table: {
                    url: _url + '&rand=' + wooutil.getRandom(),
                    cols: [[
                         { type: 'numbers', fixed: 'left' }
                        , { type: 'radio', fixed: 'left' }
                        , { field: 'Name', title: '项目名称', minWidth: 150, fixed: 'left' }
                        , { field: 'Code', title: '编号', width: 130 }
                        , { field: 'ProjTypeName', title: '类别', width: 120 }
                        , { field: 'CreateUserName', width: 110, title: '建立人' }
                        , { field: 'CreateDateTime', width: 110, title: '建立时间' }
                        , { field: 'PstateDic', width: 120, title: '项目状态' }

                    ]]
                },
                done: function (elem, data) {
                    if (data.isclear) {
                        $(param.elem).val('');
                        $(param.hide_elem).val('');
                    } else {
                        $(param.elem).val(data.data[0].Name);
                        $(param.hide_elem).val(data.data[0].Id);
                        $(param.hide_noelem).val(data.data[0].Code);
                        if (typeof param.suc === 'function') {
                            param.suc(data.data[0]);
                        }
                    }
                }
            });
        },
        selectProjZBItem: function (param) {
            /// <summary>
            /// 选择项目
            /// </summary>        
            /// <param name="elem" type="String">触发此动作的文本框</param>
            /// <param name="tableSelect" type="Object">TableSelect对象</param>
            /// <param name="selitem" type="bool">是否是选择框</param>
            /// <param name="hide_elem" type="number">隐藏文本框ID-主要为了存储后台数据库</param>
            /// <param name="suc" type="Function">回调函数，用于扩展业务需要除名称和ID以外的内容</param>
            var _selitem = false;
            if (param.selitem) {
                _selitem = param.selitem;
            }
         
            var _url = '/Project/ProjectManager/GetList1?selitem=' + _selitem;
            param.tableSelect.render({
                elem: param.elem,
                searchKey: 'keyWord',
                searchPlaceholder: '关键词搜索',
                table: {
                    url: _url + '&rand=' + wooutil.getRandom(),
                    cols: [[
                        { type: 'numbers', fixed: 'left' }
                        , { type: 'radio', fixed: 'left' }
                        , { field: 'Name', title: '项目名称', minWidth: 150, fixed: 'left' }
                        , { field: 'Code', title: '编号', width: 130 }
                        , { field: 'ProjTypeName', title: '类别', width: 120 }
                        , { field: 'CreateUserName', width: 110, title: '建立人' }
                        , { field: 'CreateDateTime', width: 110, title: '建立时间' }
                        , { field: 'PstateDic', width: 120, title: '项目状态' }

                    ]]
                },
                done: function (elem, data) {
                    if (data.isclear) {
                        $(param.elem).val('');
                        $(param.hide_elem).val('');
                    } else {
                        $(param.elem).val(data.data[0].Name);
                        $(param.hide_elem).val(data.data[0].Id);
                        $(param.hide_noelem).val(data.data[0].Code);
                        if (typeof param.suc === 'function') {
                            param.suc(data.data[0]);
                        }
                    }
                }
            });
        },
        selectProjXJItem: function (param) {
            /// <summary>
            /// 选择项目
            /// </summary>        
            /// <param name="elem" type="String">触发此动作的文本框</param>
            /// <param name="tableSelect" type="Object">TableSelect对象</param>
            /// <param name="selitem" type="bool">是否是选择框</param>
            /// <param name="hide_elem" type="number">隐藏文本框ID-主要为了存储后台数据库</param>
            /// <param name="suc" type="Function">回调函数，用于扩展业务需要除名称和ID以外的内容</param>

            var _selitem = false;
            if (param.selitem) {
                _selitem = param.selitem;
            }
            var _url = '/Project/ProjectManager/GetList2?selitem=' + _selitem;
            param.tableSelect.render({
                elem: param.elem,
                searchKey: 'keyWord',
                searchPlaceholder: '关键词搜索',
                table: {
                    url: _url + '&rand=' + wooutil.getRandom(),
                    cols: [[
                        { type: 'numbers', fixed: 'left' }
                        , { type: 'radio', fixed: 'left' }
                        , { field: 'Name', title: '项目名称', minWidth: 150, fixed: 'left' }
                        , { field: 'Code', title: '编号', width: 130 }
                        , { field: 'ProjTypeName', title: '类别', width: 120 }
                        , { field: 'CreateUserName', width: 110, title: '建立人' }
                        , { field: 'CreateDateTime', width: 110, title: '建立时间' }
                        , { field: 'PstateDic', width: 120, title: '项目状态' }

                    ]]
                },
                done: function (elem, data) {
                    if (data.isclear) {
                        $(param.elem).val('');
                        $(param.hide_elem).val('');
                    } else {
                        $(param.elem).val(data.data[0].Name);
                        $(param.hide_elem).val(data.data[0].Id);
                        $(param.hide_noelem).val(data.data[0].Code);
                        if (typeof param.suc === 'function') {
                            param.suc(data.data[0]);
                        }
                    }
                }
            });
        },
        selectProjYTItem: function (param) {
            /// <summary>
            /// 选择项目
            /// </summary>        
            /// <param name="elem" type="String">触发此动作的文本框</param>
            /// <param name="tableSelect" type="Object">TableSelect对象</param>
            /// <param name="selitem" type="bool">是否是选择框</param>
            /// <param name="hide_elem" type="number">隐藏文本框ID-主要为了存储后台数据库</param>
            /// <param name="suc" type="Function">回调函数，用于扩展业务需要除名称和ID以外的内容</param>

            var _selitem = false;
            if (param.selitem) {
                _selitem = param.selitem;
            }
            var _url = '/Project/ProjectManager/GetList3?selitem=' + _selitem;
            param.tableSelect.render({
                elem: param.elem,
                searchKey: 'keyWord',
                searchPlaceholder: '关键词搜索',
                table: {
                    url: _url + '&rand=' + wooutil.getRandom(),
                    cols: [[
                        { type: 'numbers', fixed: 'left' }
                        , { type: 'radio', fixed: 'left' }
                        , { field: 'Name', title: '项目名称', minWidth: 150, fixed: 'left' }
                        , { field: 'Code', title: '编号', width: 130 }
                        , { field: 'ProjTypeName', title: '类别', width: 120 }
                        , { field: 'CreateUserName', width: 110, title: '建立人' }
                        , { field: 'CreateDateTime', width: 110, title: '建立时间' }
                        , { field: 'PstateDic', width: 120, title: '项目状态' }

                    ]]
                },
                done: function (elem, data) {
                    if (data.isclear) {
                        $(param.elem).val('');
                        $(param.hide_elem).val('');
                    } else {
                        $(param.elem).val(data.data[0].Name);
                        $(param.hide_elem).val(data.data[0].Id);
                        $(param.hide_noelem).val(data.data[0].Code);
                        if (typeof param.suc === 'function') {
                            param.suc(data.data[0]);
                        }
                    }
                }
            });
        },
        selectMainDeptItem: function (param) {
            /// <summary>
            /// 选择签约主体
            /// </summary>        
            /// <param name="elem" type="String">触发此动作的文本框</param>
            /// <param name="tableSelect" type="Object">TableSelect对象</param>
            /// <param name="selitem" type="bool">是否是选择框</param>
            /// <param name="hide_elem" type="number">隐藏文本框ID-主要为了存储后台数据库</param>
            /// <param name="suc" type="Function">回调函数，用于扩展业务需要除名称和ID以外的内容</param>

            var _selitem = false;
            if (param.selitem) {
                _selitem = param.selitem;
            }
            var _url = '/System/Department/GetList?selitem=' + _selitem;
            param.tableSelect.render({
                elem: param.elem,
                searchKey: 'keyWord',
                searchPlaceholder: '关键词搜索',
                table: {
                    url: _url + '&rand=' + wooutil.getRandom(),
                    cols: [[
                         { type: 'numbers', fixed: 'left' }
                        , { type: 'radio', fixed: 'left' }
                        , { field: 'Name', title: '名称', minWidth: 150, fixed: 'left' }
                        , { field: 'No', title: '编号', width: 130 }
                        , { field: 'ShortName', title: '简称', width: 120 }

                    ]]
                },
                done: function (elem, data) {
                    if (data.isclear) {
                        $(param.elem).val('');
                        $(param.hide_elem).val('');
                    } else {
                        $(param.elem).val(data.data[0].Name);
                        $(param.hide_elem).val(data.data[0].Id);
                        $(param.hideShortName_elem).val(data.data[0].No);
                        if (typeof param.suc === 'function') {
                            param.suc(data.data[0]);
                        }
                    }
                }
            });
        },
        selectColleCtionItem: function (param) {
            /// <summary>
            /// 选择收款合同
            /// </summary>        
            /// <param name="elem" type="String">触发此动作的文本框</param>
            /// <param name="tableSelect" type="Object">TableSelect对象</param>
            /// <param name="selitem" type="bool">是否是选择框</param>
            /// <param name="hide_elem" type="number">隐藏文本框ID-主要为了存储后台数据库</param>
            /// <param name="suc" type="Function">回调函数，用于扩展业务需要除名称和ID以外的内容</param>
            var _selitem = false;
            if (param.selitem) {
                _selitem = param.selitem;
            }
            
            var _url = '/Contract/ContractCollection/GetSelectList?selitem=' + _selitem;
            if (param.otherwh != undefined) {
                _url = _url + "&otherwh=" + param.otherwh;
            }
            param.tableSelect.render({
                elem: param.elem,
                searchKey: 'keyWord',
                searchPlaceholder: '关键词搜索',
                table: {
                    url: _url + '&rand=' + wooutil.getRandom(),
                    cols: [[
                         { type: 'numbers', fixed: 'left' }
                        , { type: 'radio', fixed: 'left' }
                        , { field: 'Name', title: '合同名称', minWidth: 150, fixed: 'left' }
                        , { field: 'Code', title: '编号', width: 130 }
                        , { field: 'ContAmThod', title: '合同金额', width: 120 }
                        , { field: 'FinanceTypeDesc', title: '资金性质', width: 120 }
                        , { field: 'ContStateDic', title: '合同状态', width: 120 }
                        , { field: 'CreateUserName', title: '创建人', width: 120 }
                        , { field: 'CreateDateTime', title: '创建时间', width: 120 }
                    ]]
                },
                done: function (elem, data) {
                    if (data.isclear) {
                        $(param.elem).val('');
                        $(param.hide_elem).val('');
                    } else {
                        $(param.elem).val(data.data[0].Name);
                        $(param.hide_elem).val(data.data[0].Id);
                        if (typeof param.suc === 'function') {
                            param.suc(data.data[0]);
                        }
                    }
                }
            });
        },
        selectPayCtionItem: function (param) {
            /// <summary>
            /// 选择付款合同
            /// </summary>        
            /// <param name="elem" type="String">触发此动作的文本框</param>
            /// <param name="tableSelect" type="Object">TableSelect对象</param>
            /// <param name="selitem" type="bool">是否是选择框</param>
            /// <param name="hide_elem" type="number">隐藏文本框ID-主要为了存储后台数据库</param>
            /// <param name="suc" type="Function">回调函数，用于扩展业务需要除名称和ID以外的内容</param>

        var _selitem = false;
        if (param.selitem) {
            _selitem = param.selitem;
        }
        var _url = '/Contract/ContractPayment/GetSelectList?selitem=' + _selitem;
        param.tableSelect.render({
            elem: param.elem,
            searchKey: 'keyWord',
            searchPlaceholder: '关键词搜索',
            table: {
                url: _url + '&rand=' + wooutil.getRandom(),
                cols: [[
                         { type: 'numbers', fixed: 'left' }
                        , { type: 'radio', fixed: 'left' }
                        , { field: 'Name', title: '合同名称', minWidth: 150, fixed: 'left' }
                        , { field: 'Code', title: '编号', width: 130 }
                        , { field: 'ContAmThod', title: '合同金额', width: 120 }
                        , { field: 'FinanceTypeDesc', title: '资金性质', width: 120 }
                        , { field: 'StateDic', title: '合同状态', width: 120 }
                        , { field: 'CreateUserName', title: '创建人', width: 120 }
                        , { field: 'CreateDateTime', title: '创建时间', width: 120 }
                ]]
            },
            done: function (elem, data) {
                if (data.isclear) {
                    $(param.elem).val('');
                    $(param.hide_elem).val('');
                } else {
                    $(param.elem).val(data.data[0].Name);
                    $(param.hide_elem).val(data.data[0].Id);
                    if (typeof param.suc === 'function') {
                        param.suc(data.data[0]);
                    }
                }
            }
        });
        },
         selectZbItem: function (param) {
            /// <summary>
            /// 选择招标
            /// </summary>        
            /// <param name="elem" type="String">触发此动作的文本框</param>
            /// <param name="tableSelect" type="Object">TableSelect对象</param>
            /// <param name="selitem" type="bool">是否是选择框</param>
            /// <param name="hide_elem" type="number">隐藏文本框ID-主要为了存储后台数据库</param>
            /// <param name="suc" type="Function">回调函数，用于扩展业务需要除名称和ID以外的内容</param>

            var _selitem = false;
            if (param.selitem) {
                _selitem = param.selitem;
             }

            // /Company/Supplier / GetList ? selitem
             var _url = '/Tender/TenderInfo/GetList?selitem=' + _selitem;
            param.tableSelect.render({
                elem: param.elem,
                searchKey: 'keyWord',
                searchPlaceholder: '关键词搜索',
                table: {
                    url: _url + '&rand=' + wooutil.getRandom(),
                    cols: [[
                        { type: 'numbers', fixed: 'left' }
                        , { type: 'radio', fixed: 'left' }

                        , { field: 'ProjectName', title: '项目名称', minWidth: 150, fixed: 'left' }
                        , { field: 'ZbswName', title: '中标单位', width: 130 }
                        , { field: 'Zbdw', title: '招标人id', width: 130, hide: true } //TenderUserId
                        , { field: 'Zjethis', title: '总金额', width: 130 }
                        , { field: 'ProjectNO', title: '项目编号', width: 120 }
                        , { field: 'FinanceTypeDesc', title: '记录人', width: 120 }
                        , { field: 'FinanceTypeDesc', title: '记录人', width: 120 }
                        , { field: 'TenderStatus', title: '状态', width: 130 }
                        , { field: 'ProjectId', title: '项目id', width: 130}
                        
                    ]]
                },
                done: function (elem, data) {
                    if (data.isclear) {
                        $(param.elem).val('');
                        $(param.hide_elem).val('');
                    } else {
                        $(param.elem).val(data.data[0].ProjectName);
                        $(param.hide_elem).val(data.data[0].Id);
                        if (typeof param.suc === 'function') {
                            param.suc(data.data[0]);
                        }
                    }
                }
            });
        }
        , selectXjItem: function (param) {
            /// <summary>
            /// 选择询价
            /// </summary>        
            /// <param name="elem" type="String">触发此动作的文本框</param>
            /// <param name="tableSelect" type="Object">TableSelect对象</param>
            /// <param name="selitem" type="bool">是否是选择框</param>
            /// <param name="hide_elem" type="number">隐藏文本框ID-主要为了存储后台数据库</param>
            /// <param name="suc" type="Function">回调函数，用于扩展业务需要除名称和ID以外的内容</param>

            var _selitem = false;
            if (param.selitem) {
                _selitem = param.selitem;
            }
            var _url = '/Inquiry/Inquiry/GetList?selitem=' + _selitem;
            param.tableSelect.render({
                elem: param.elem,
                searchKey: 'keyWord',
                searchPlaceholder: '关键词搜索',
                table: {
                    url: _url + '&rand=' + wooutil.getRandom(),
                    cols: [[
                        { type: 'numbers', fixed: 'left' }
                        , { type: 'radio', fixed: 'left' }
                        , { field: 'ProjectName', title: '项目名称', minWidth: 150, fixed: 'left' }
                        , { field: 'ZbdwName', title: '询价单位', width: 130 }
                        , { field: 'Zbdw', title: '招标人id', width: 130, hide: true } //TenderUserId Zjethis
                        , { field: 'Zjethis', title: '总金额', width: 130 }
                        , { field: 'ProjectNumber', title: '项目编号', width: 120 }
                        , { field: 'RecorderName', title: '记录人', width: 120 }
                        , { field: 'InStateDic', title: '状态', width: 130 }
                        , { field: 'ProjectId', title: '项目id', width: 130}
                    ]]
                },
                done: function (elem, data) {
                    if (data.isclear) {
                        $(param.elem).val('');
                        $(param.hide_elem).val('');
                    } else {
                        $(param.elem).val(data.data[0].ProjectName);
                        $(param.hide_elem).val(data.data[0].Id);
                        if (typeof param.suc === 'function') {
                            param.suc(data.data[0]);
                        }
                    }
                }
            });
        }
        , selectYtItem: function (param) {
            /// <summary>
            /// 选择洽谈
            /// </summary>        
            /// <param name="elem" type="String">触发此动作的文本框</param>
            /// <param name="tableSelect" type="Object">TableSelect对象</param>
            /// <param name="selitem" type="bool">是否是选择框</param>
            /// <param name="hide_elem" type="number">隐藏文本框ID-主要为了存储后台数据库</param>
            /// <param name="suc" type="Function">回调函数，用于扩展业务需要除名称和ID以外的内容</param>

            var _selitem = false;
            if (param.selitem) {
                _selitem = param.selitem;
            }
            var _url = '/Questioning/Questioning/GetList?selitem=' + _selitem;
            param.tableSelect.render({
                elem: param.elem,
                searchKey: 'keyWord',
                searchPlaceholder: '关键词搜索',
                table: {
                    url: _url + '&rand=' + wooutil.getRandom(),
                    cols: [[
                        { type: 'numbers', fixed: 'left' }
                        , { type: 'radio', fixed: 'left' }
                        , { field: 'ProjectName', title: '项目名称', minWidth: 150, fixed: 'left' }
                        , { field: 'ZbdwName', title: '洽谈单位', width: 130 }
                        , { field: 'Zbdw', title: '招标人id', width: 130, hide: true } //TenderUserId
                        , { field: 'Zjethis', title: '总金额', width: 130 }
                        , { field: 'ProjectNumber', title: '项目编号', width: 120 }
                        , { field: 'RecorderName', title: '记录人', width: 120 }
                        , { field: 'InStateDic', title: '状态', width: 130 }
                        , { field: 'ProjectId', title: '项目id', width: 130}
                    ]]
                },
                done: function (elem, data) {
                    if (data.isclear) {
                        $(param.elem).val('');
                        $(param.hide_elem).val('');
                    } else {
                        $(param.elem).val(data.data[0].ProjectName);
                        $(param.hide_elem).val(data.data[0].Id);
                        if (typeof param.suc === 'function') {
                            param.suc(data.data[0]);
                        }
                    }
                }
            });
        },
        selectSPItem: function (param) {
            /// <summary>
            /// 选择收票
            /// </summary>        
            /// <param name="elem" type="String">触发此动作的文本框</param>
            /// <param name="tableSelect" type="Object">TableSelect对象</param>
            /// <param name="selitem" type="bool">是否是选择框</param>
            /// <param name="hide_elem" type="number">隐藏文本框ID-主要为了存储后台数据库</param>
            /// <param name="suc" type="Function">回调函数，用于扩展业务需要除名称和ID以外的内容</param>

            var _selitem = false;
            if (param.selitem) {
                _selitem = param.selitem;
            }
            var _url = '/Finance/ContInvoice/GetList?fType=1&selitem=' + _selitem;
            param.tableSelect.render({
                elem: param.elem,
                searchKey: 'keyWord',
                searchPlaceholder: '关键词搜索',
                table: {
                    url: _url + '&rand=' + wooutil.getRandom(),
                    cols: [[
                        { type: 'numbers', fixed: 'left' }
                        , { type: 'radio', fixed: 'left' }
                        , { field: 'InTypeName', title: '类型', minWidth: 150, fixed: 'left' }
                        , { field: 'ContName', title: '合同名称', width: 130 }
                        , { field: 'InCode', title: '发票号', width: 130} 
                        , { field: 'CompName', title: '合同对方', width: 130 }
                        , { field: 'AmountMoneyThod', title: '发票金额', width: 120 }
                        , { field: 'InStateDic', title: '状态', width: 130 }
                    ]]
                },
                done: function (elem, data) {
                    if (data.isclear) {
                        $(param.elem).val('');
                        $(param.hide_elem).val('');
                    } else {
                        $(param.elem).val(data.data[0].InCode);
                        $(param.hide_elem).val(data.data[0].Id);
                        if (typeof param.suc === 'function') {
                            param.suc(data.data[0]);
                        }
                    }
                }
            });
        }, selectKPItem: function (param) {
            /// <summary>
            /// 选择开票
            /// </summary>        
            /// <param name="elem" type="String">触发此动作的文本框</param>
            /// <param name="tableSelect" type="Object">TableSelect对象</param>
            /// <param name="selitem" type="bool">是否是选择框</param>
            /// <param name="hide_elem" type="number">隐藏文本框ID-主要为了存储后台数据库</param>
            /// <param name="suc" type="Function">回调函数，用于扩展业务需要除名称和ID以外的内容</param>

            var _selitem = false;
            if (param.selitem) {
                _selitem = param.selitem;
            }
            var _url = '/Finance/ContInvoice/GetList?fType=0&selitem=' + _selitem;
            param.tableSelect.render({
                elem: param.elem,
                searchKey: 'keyWord',
                searchPlaceholder: '关键词搜索',
                table: {
                    url: _url + '&rand=' + wooutil.getRandom(),
                    cols: [[
                        { type: 'numbers', fixed: 'left' }
                        , { field: 'InTypeName', title: '类型', minWidth: 150, fixed: 'left' }
                        , { field: 'ContName', title: '合同名称', width: 130 }
                        , { field: 'InCode', title: '发票号', width: 130 }
                        , { field: 'CompName', title: '合同对方', width: 130 }
                        , { field: 'AmountMoneyThod', title: '发票金额', width: 120 }
                        , { field: 'InStateDic', title: '状态', width: 130 }
                    ]]
                },
                done: function (elem, data) {
                    if (data.isclear) {
                        $(param.elem).val('');
                        $(param.hide_elem).val('');
                    } else {
                        $(param.elem).val(data.data[0].InCode);
                        $(param.hide_elem).val(data.data[0].Id);
                        if (typeof param.suc === 'function') {
                            param.suc(data.data[0]);
                        }
                    }
                }
            });
        }, selectSJSKItem: function (param) {
            /// <summary>
            /// 实际收款
            /// </summary>        
            /// <param name="elem" type="String">触发此动作的文本框</param>
            /// <param name="tableSelect" type="Object">TableSelect对象</param>
            /// <param name="selitem" type="bool">是否是选择框</param>
            /// <param name="hide_elem" type="number">隐藏文本框ID-主要为了存储后台数据库</param>
            /// <param name="suc" type="Function">回调函数，用于扩展业务需要除名称和ID以外的内容</param>

            var _selitem = false;
            if (param.selitem) {
                _selitem = param.selitem;
            }
            var _url = '/Finance/ContActualFinance/GetList?fType=0&selitem=' + _selitem;
            param.tableSelect.render({
                elem: param.elem,
                searchKey: 'keyWord',
                searchPlaceholder: '关键词搜索',
                table: {
                    url: _url + '&rand=' + wooutil.getRandom(),
                    cols: [[
                        { type: 'numbers', fixed: 'left' }
                        , { type: 'radio', fixed: 'left' }
                        , { field: 'ContName', title: '合同名称', minWidth: 150, fixed: 'left' }
                        , { field: 'ContCode', title: '合同编号', width: 130 }
                        , { field: 'AmountMoney', title: '实际金额', width: 130 }
                    ]]
                },
                done: function (elem, data) {
                    if (data.isclear) {
                        $(param.elem).val('');
                        $(param.hide_elem).val('');
                    } else {
                        $(param.elem).val(data.data[0].ContName);
                        $(param.hide_elem).val(data.data[0].Id);
                        if (typeof param.suc === 'function') {
                            param.suc(data.data[0]);
                        }
                    }
                }
            });
        }, selectSJFKItem: function (param) {
            /// <summary>
            /// 实际付款
            /// </summary>        
            /// <param name="elem" type="String">触发此动作的文本框</param>
            /// <param name="tableSelect" type="Object">TableSelect对象</param>
            /// <param name="selitem" type="bool">是否是选择框</param>
            /// <param name="hide_elem" type="number">隐藏文本框ID-主要为了存储后台数据库</param>
            /// <param name="suc" type="Function">回调函数，用于扩展业务需要除名称和ID以外的内容</param>

            var _selitem = false;
            if (param.selitem) {
                _selitem = param.selitem;
            }
            var _url = '/Finance/ContActualFinance/GetList?fType=1&selitem=' + _selitem;
            param.tableSelect.render({
                elem: param.elem,
                searchKey: 'keyWord',
                searchPlaceholder: '关键词搜索',
                table: {
                    url: _url + '&rand=' + wooutil.getRandom(),
                    cols: [[
                        { type: 'numbers', fixed: 'left' }
                        , { type: 'radio', fixed: 'left' }
                        , { field: 'ContName', title: '合同名称', minWidth: 150, fixed: 'left' }
                        , { field: 'ContCode', title: '合同编号', width: 130 }
                        , { field: 'AmountMoney', title: '实际金额', width: 130 }
                    ]]
                },
                done: function (elem, data) {
                    if (data.isclear) {
                        $(param.elem).val('');
                        $(param.hide_elem).val('');
                    } else {
                        $(param.elem).val(data.data[0].ContName);
                        $(param.hide_elem).val(data.data[0].Id);
                        if (typeof param.suc === 'function') {
                            param.suc(data.data[0]);
                        }
                    }
                }
            });
        }, selectSKcontextItem: function (param) {
            /// <summary>
            /// 收款合同文本
            /// </summary>        
            /// <param name="elem" type="String">触发此动作的文本框</param>
            /// <param name="tableSelect" type="Object">TableSelect对象</param>
            /// <param name="selitem" type="bool">是否是选择框</param>
            /// <param name="hide_elem" type="number">隐藏文本框ID-主要为了存储后台数据库</param>
            /// <param name="suc" type="Function">回调函数，用于扩展业务需要除名称和ID以外的内容</param>

            var _selitem = false;
            if (param.selitem) {
                _selitem = param.selitem;
            }
            var _url = '/ContractDraft/ContText/GetMainList?requestType=0&selitem=' + _selitem;
            param.tableSelect.render({
                elem: param.elem,
                searchKey: 'keyWord',
                searchPlaceholder: '关键词搜索',
                table: {
                    url: _url + '&rand=' + wooutil.getRandom(),
                    cols: [[
                        { type: 'numbers', fixed: 'left' }
                        , { field: 'Name', title: '文本名称', minWidth: 150, fixed: 'left' }
                        , { field: 'CategoryName', title: '文本类别', width: 130 }
                        , { field: 'ContName', title: '合同名称', width: 130 } //TenderUserId
                        , { field: 'ContCode', title: '合同编号', width: 130 }
                        , { field: 'Remark', title: '文本说明', width: 120 }
                    ]]
                },
                done: function (elem, data) {
                    if (data.isclear) {
                        $(param.elem).val('');
                        $(param.hide_elem).val('');
                    } else {
                        $(param.elem).val(data.data[0].Name);
                        $(param.hide_elem).val(data.data[0].Id);
                        if (typeof param.suc === 'function') {
                            param.suc(data.data[0]);
                        }
                    }
                }
            });
        }, selectFKcontextItem: function (param) {
            /// <summary>
            /// 付款合同文本
            /// </summary>        
            /// <param name="elem" type="String">触发此动作的文本框</param>
            /// <param name="tableSelect" type="Object">TableSelect对象</param>
            /// <param name="selitem" type="bool">是否是选择框</param>
            /// <param name="hide_elem" type="number">隐藏文本框ID-主要为了存储后台数据库</param>
            /// <param name="suc" type="Function">回调函数，用于扩展业务需要除名称和ID以外的内容</param>

            var _selitem = false;
            if (param.selitem) {
                _selitem = param.selitem;
            }
            var _url = '/ContractDraft/ContText/GetMainList?requestType=1&selitem=' + _selitem;
            param.tableSelect.render({
                elem: param.elem,
                searchKey: 'keyWord',
                searchPlaceholder: '关键词搜索',
                table: {
                    url: _url + '&rand=' + wooutil.getRandom(),
                    cols: [[
                        { type: 'numbers', fixed: 'left' }
                        , { type: 'radio', fixed: 'left' }
                        , { field: 'Name', title: '文本名称', minWidth: 150, fixed: 'left' }
                        , { field: 'CategoryName', title: '文本类别', width: 130 }
                        , { field: 'ContName', title: '合同名称', width: 130} //TenderUserId
                        , { field: 'ContCode', title: '合同编号', width: 130 }
                        , { field: 'Remark', title: '文本说明', width: 120 }
                    ]]
                },
                done: function (elem, data) {
                    if (data.isclear) {
                        $(param.elem).val('');
                        $(param.hide_elem).val('');
                    } else {
                        $(param.elem).val(data.data[0].Name);
                        $(param.hide_elem).val(data.data[0].Id);
                        if (typeof param.suc === 'function') {
                            param.suc(data.data[0]);
                        }
                    }
                }
            });
        }, selectSKbdItem: function (param) {
            /// <summary>
            /// 收款标的交付明细
            /// </summary>        
            /// <param name="elem" type="String">触发此动作的文本框</param>
            /// <param name="tableSelect" type="Object">TableSelect对象</param>
            /// <param name="selitem" type="bool">是否是选择框</param>
            /// <param name="hide_elem" type="number">隐藏文本框ID-主要为了存储后台数据库</param>
            /// <param name="suc" type="Function">回调函数，用于扩展业务需要除名称和ID以外的内容</param>
            var _selitem = false;
            if (param.selitem) {
                _selitem = param.selitem;
            }
            var _url = '/Contract/ContSubDelivery/GetMainList?fType=0&selitem=' + _selitem;
            param.tableSelect.render({
                elem: param.elem,
                searchKey: 'keyWord',
                searchPlaceholder: '关键词搜索',
                table: {
                    url: _url + '&rand=' + wooutil.getRandom(),
                    cols: [[
                        { type: 'numbers', fixed: 'left' }
                        , { type: 'radio', fixed: 'left' }
                        , { field: 'SubName', title: '标的名称', minWidth: 150, fixed: 'left' }
                        , { field: 'ContName', title: '合同名称', width: 130 } //TenderUserId
                        , { field: 'ContCode', title: '合同编号', width: 130 }
                        , { field: 'ActDate', title: '交付日期', width: 120 }
                        , { field: 'DevNumber', title: '交付数量', width: 120 }
                    ]]
                },
                done: function (elem, data) {
                    if (data.isclear) {
                        $(param.elem).val('');
                        $(param.hide_elem).val('');
                    } else {
                        $(param.elem).val(data.data[0].SubName);
                        $(param.hide_elem).val(data.data[0].Id);
                        if (typeof param.suc === 'function') {
                            param.suc(data.data[0]);
                        }
                    }
                }
            });
        }, selectFKbdItem: function (param) {
            /// <summary>
            /// 付款标的交付明细
            /// </summary>        
            /// <param name="elem" type="String">触发此动作的文本框</param>
            /// <param name="tableSelect" type="Object">TableSelect对象</param>
            /// <param name="selitem" type="bool">是否是选择框</param>
            /// <param name="hide_elem" type="number">隐藏文本框ID-主要为了存储后台数据库</param>
            /// <param name="suc" type="Function">回调函数，用于扩展业务需要除名称和ID以外的内容</param>

            var _selitem = false;
            if (param.selitem) {
                _selitem = param.selitem;
            }
            var _url = '/Contract/ContSubDelivery/GetMainList?fType=1&selitem=' + _selitem;
            param.tableSelect.render({
                elem: param.elem,
                searchKey: 'keyWord',
                searchPlaceholder: '关键词搜索',
                table: {
                    url: _url + '&rand=' + wooutil.getRandom(),
                    cols: [[
                        { type: 'numbers', fixed: 'left' }
                        , { type: 'radio', fixed: 'left' }
                        , { field: 'SubName', title: '标的名称', minWidth: 150, fixed: 'left' }
                        , { field: 'ContName', title: '合同名称', width: 130 } //TenderUserId
                        , { field: 'ContCode', title: '合同编号', width: 130 }
                        , { field: 'ActDate', title: '交付日期', width: 120 }
                        , { field: 'DevNumber', title: '交付数量', width: 120 }
                    ]]
                },
                done: function (elem, data) {
                    if (data.isclear) {
                        $(param.elem).val('');
                        $(param.hide_elem).val('');
                    } else {
                        $(param.elem).val(data.data[0].SubName);
                        $(param.hide_elem).val(data.data[0].Id);
                        if (typeof param.suc === 'function') {
                            param.suc(data.data[0]);
                        }
                    }
                }
            });
        }
    }
    function ssd() {




    }
    exports('selectnfitem', selectnfitem);
});