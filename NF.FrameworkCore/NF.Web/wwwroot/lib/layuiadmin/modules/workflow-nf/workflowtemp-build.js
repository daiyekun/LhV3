/**
*审批模板新建
**/
layui.define(['form','formSelects'], function (exports) {
    var $ = layui.$
   , setter = layui.setter
   , admin = layui.admin
   , form = layui.form
   , formSelects = layui.formSelects;

   
    var wfcomm = {
        /// <summary>获取审批对象列表</summary>  
        /// <param name="param" type="Object">selectEl:select的ID带#</param>
        getObjTypes: function (param) {
            admin.req({
                url: '/WorkFlow/FlowTemp/GetWfObjTypes?rand=' + Math.round(Math.random() * (10000 - 1)).toString()
                       , async: false//取消异步
                       , done: function (res) {
                           $(res.Data).each(function (i, n) {
                               $(param.selectEl).append("<option value='" + n.Value + "'>" + n.Desc + "</option>");
                           });
                           form.render("select");//必须
                       }
            });
        },
        getObjTypeClass(param) {
            var tagIns2 = selectM({
                //元素容器【必填】
                elem: param.selectEl
                //候选数据【必填】
            , data: param.arraydata
                //默认值
           , max: 50
                //input的name 不设置与选择器相同(去#.)
           , name: param.inputName
                //值的分隔符
           , delimiter: ','
                //候选项数据的键名
           , field: { idName: param.idName, titleName: param.titleName }
            });
        }
    }
    //对象列表
    wfcomm.getObjTypes({ selectEl: '#ObjType' });
    /**
    *审批事项
    ***/
    function initFlowItems(dataEnum,dataval) {
        formSelects.config('flowitems', {
            direction: 'down'
             , success: function (id, url, searchVal, result) {
                 formSelects.value('flowitems', dataval);
             }
        }).data('flowitems', 'server', {
            url: '/WorkFlow/FlowTemp/GetFlowItems?objEnum=' + dataEnum
        });
    }
    /**
    *初始化对象类别
    **/
    //function initObjClass(dataEnum,dataval) {
    //    formSelects.config('CategoryIds', {
    //        direction: 'down'
    //        , success: function (id, url, searchVal, result) {
    //            formSelects.value('CategoryIds', dataval);
    //        }
    //    }).data('CategoryIds', 'server', {
    //        url: '/WorkFlow/FlowTemp/GetObjClass?objEnum=' + dataEnum
    //    });
    //}
    /**
  *合类别
  ***/
    function initObjClassTree(dataval, dataEnum) {
        formSelects.config('CategoryIds', {
            direction: 'down'
            , success: function (id, url, searchVal, result) {
                formSelects.value('CategoryIds', dataval);
            }
        }).data('CategoryIds', 'server', {
            url: '/WorkFlow/FlowTemp/GetFlowContTxtClassTree?objEnum=' + dataEnum
            , tree: {
                nextClick: function (id, item, callback) {
                }
            }
        });

    }
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
    *根据选择审批对象获取字典类别
    *获取值对应 NF.ViewModel/Extend/Enums/DataDictionaryEnum
    **/
    function GetDicEnum(selval) {
        var dataenum = 0;
        switch (selval) {
            case "0"://客户
            case 0:
                dataenum = 3;
                break;
            case "1"://供应商
            case 1:
                dataenum = 2;
                break;
            case "2"://其他对方
            case 2:
                dataenum = 4;
                break;
            case "3"://合同
            case "6"://付款
            case 3:
            case 6:
                dataenum = 1;
                break;
            case "4"://收票
            case "5"://开票
            case 4:
            case 5:
                dataenum = 19;
                break;
            case 7://项目
            case "7"://项目
                dataenum = 13;
                break;
            case 8://【询价】
            case "8"://【询价】
                dataenum = 31;
                break;
            case 9://【约谈】
            case "9"://【约谈】
                dataenum = 32;
                break;
            case 10://【招标】
            case "10"://【招标】
                dataenum = 33;
                break;
            default:
                dataenum = -1;
                break;
        }

        return dataenum;
    }

    //选择对象时设置对象类别
    form.on('select(ObjType)', function (data) {
        initFlowItems(data.value,[]);
        //initObjClass(GetDicEnum(data.value), []);
        initObjClassTree([],GetDicEnum(data.value));

    });


    /**
    *修改时赋值
    **/
    var flowTempId = wooutil.getUrlVar('Id');
    if (flowTempId !== "" && flowTempId !== undefined) {
        admin.req({
            url: '/WorkFlow/FlowTemp/ShowView',
            data: { Id: flowTempId, rand: wooutil.getRandom() },
            done: function (res) {
                form.val("NF-FlowTemp-Form", res.Data);
                //字典类别
                //initObjClass(GetDicEnum(res.Data.ObjType), res.Data.CategoryIdsArray);
                initObjClassTree(res.Data.CategoryIdsArray, GetDicEnum(res.Data.ObjType));
                initFlowItems(res.Data.ObjType, res.Data.FlowItemsArray);
                initDepts(res.Data.DeptIdsArray);
            }
        });

    } else {
        initDepts([]);
    }
    exports('workFlowTempBuild', {});
});