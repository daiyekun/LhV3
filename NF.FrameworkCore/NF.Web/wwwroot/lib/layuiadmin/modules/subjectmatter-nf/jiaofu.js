/**
* 标的交付
**/
layui.define(['table', 'laydate', 'commonnf', 'tableSelect', 'selectnfitem', 'upload'], function (exports) {
    var $ = layui.$
   , table = layui.table
   , setter = layui.setter
   , laydate = layui.laydate
   , commonnf = layui.commonnf
   , tableSelect = layui.tableSelect
   , selectnfitem = layui.selectnfitem
   , upload = layui.upload
   , admin = layui.admin;
    var loadindex = wooutil.loading();
    var subIds = wooutil.getUrlVar('subIds');//选择标的ID集合
    //列表
   table.render({
      elem: '#NF-SubDev-List'
     , id: 'SubDevList'
        , url: '/Contract/ContSubjectMatter/GetJiaoFuList?subIds=' + subIds + '&rand=' + wooutil.getRandom()
        , toolbar:false //'#toolCollSubMet'
        , defaultToolbar: ['filter']
        , cellMinWidth: 80
        , cols: [[
            { type: 'numbers', fixed: 'left' }
            //, { type: 'checkbox', fixed: 'left' }
            , { field: 'Name', title: '标的名称', width: 150, fixed: 'left' }
            , { field: 'ContName', title: '合同名称', minWidth: 160, templet: '#ProjectTpl', width: 180 }
            , { field: 'Unit', title: '单位', width: 120 }
            , { field: 'PriceThod', title: '单价', width: 140 }
            , { field: 'Amount', title: '数量', width: 140 }
            , { field: 'TotalThod', title: '小计', width: 160 }
            , { field: 'ComplateAmount', title: '已交付数量', width: 140 }
            , { field: 'CurrDelNum', title: '<i class="layui-icon  layui-icon-edit"></i>本次交付数量', width: 140, edit: 'text' }
            , { field: 'NotDelNum', title: '未交付数量', width: 140 }
            , { field: 'JfBl', title: '交付比例', width: 130 }
            , { field: 'CurrDelmoneyThod', title: '本次交付金额', width: 140 }
            , { field: 'PlanDateTime', title: '计划交付日期', width: 140 }
            , { field: 'CompName', title: '合同对方008', templet: '#compTpl' ,width: 160 }
            , { field: 'Id', title: 'Id', width: 100, hide: true }
        ]]
        , page: false
        , loading: true
        , height: setter.table.height_tab+50
        , limit: setter.table.limit_tab
        , limits: setter.table.limits
        , done: function (res, curr, count) {   //返回数据执行回调函数
            layer.close(loadindex);    //返回数据关闭loading
            active.setInfo(res.data[0]);
            $("#jiaoFuTotal").html(res.msg);//交付总额
            
        }

    });
    /**
    *监听行单击事件
    **/
  table.on('row(NF-SubDev-List)', function (obj) {
     
        active.setInfo(obj.data);
        //设置行样式
        //obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
    });
    /**
    *表格编辑事件
    **/
  table.on('edit(NF-SubDev-List)', function (obj) {
        active.editcell(obj);
    });
    /**相关方法*/
    var active = {
        setInfo: function (objdata) {
            /// <summary>设置基本信息</summary>
            $("#subName").text(objdata.Name);
            $("#subUnit").text(objdata.Unit);
            $("#subprice").text(objdata.PriceThod);
            $("#amount").text(objdata.Amount);
            $("#totalthod").text(objdata.TotalThod);
            $("#devnum").text(objdata.ComplateAmount);
            $("#notdevnum").text(objdata.NotDelNum);
            $("#currdevnum").text(objdata.CurrDelNum);
            $("#currdevmonery").text(objdata.CurrDelmoneyThod);

        }, devAll: function () {
            /// <summary>全部交付</summary>
            table.reload('SubDevList', {//记住如果定义了Id就是定义的ID
                where: {search:1, rand: wooutil.getRandom() },
                page: { curr: 1 }//重新从第 1 页开始
            });
        }, editcell: function (obj) {
            
          var notdevnum = parseFloat(obj.data.NotOldDelNum);
                if (isNaN(notdevnum)) {
                    notdevnum = 0;
                }
            
            var value = obj.value;
            var fval = parseFloat(value);
            var field = obj.field;
            var inputElem = $(this);
            var tdElem = inputElem.closest('td');
            var valueOld = inputElem.prev().text();
            var data = {};
            var errMsg = ''; // 错误信息
            if (field === 'CurrDelNum') {
                if (isNaN(fval)) {
                    errMsg = '输入格式不正确';
                }
                else if (fval > notdevnum) {
                    errMsg = '不能大于剩余交付数量:' + notdevnum;
                }
            }
            if (errMsg) {
                // 如果不满足的时候
                data.CurrDelNum = valueOld;
                layer.msg(errMsg, { time: 1000, anim: 6, shade: 0.01 }, function () {
                    inputElem.blur();
                    obj.update(data);
                    tdElem.click();
                });
            } else {//计算
                var tempobj = new Object();
                var currje = fval * parseFloat(obj.data.Price);
                var jethod = wooutil.numThodFormat(currje.toString());
                var sye=obj.data.NotOldDelNum - fval;
                tempobj.CurrDelmoneyThod = jethod;//当前交付金额千分位
                tempobj.NotDelNum = sye;//当前交付以后剩余量
                tempobj.CurrDelmoney = currje;//当前交付金额
                obj.update(tempobj);
                obj.data.CurrDelmoneyThod = jethod;
                obj.data.CurrDelmoney = currje;
                obj.data.NotDelNum = sye;
               
              
                //获取修改后的数据
                var tbdata = layui.table.cache.SubDevList;
                var jftotalm = 0;
                for (var i = 0; i < tbdata.length;i++){
                    var currje = parseFloat(tbdata[i].CurrDelmoney);
                    if (!isNaN(currje)) {
                        jftotalm = jftotalm + currje;
                    }
                }
                //当前交付总额
                $("#jiaoFuTotal").html(wooutil.numThodFormat(jftotalm.toString()));//交付总额


            }

            
        }

    }





    //注册事件
    $('.layui-btn.layuiadmin-jiaofu-btn').on('click', function () {
        var type = $(this).data('type');
        active[type] ? active[type].call(this) : '';
    });

    //交付日期
    laydate.render({ elem: '#ActualDateTime', trigger: 'click' });
    //交付方式
    commonnf.getdatadic({ dataenum: 30, selectEl: "#DeliverType" });
    //交付人
    selectnfitem.selectUserItem(
                        {
                            tableSelect: tableSelect,
                            elem: '#DeliverUserName',
                            hide_elem: '#DeliverUserId'

                        });
    //上传控件
    var uploadInst = upload.render({
                elem: '#jiaofu-attUpload'
               , url: setter.layupload.uploadIp + '/NfCommon/NfAttachment/Upload?folderIndex=9'
               , accept: setter.layupload.accept
               , exts: setter.layupload.exts
               , size: setter.layupload.size
               , before: function (obj) {
                   layer.load(); //上传loading
               }
               , done: function (res, index, upload) { //上传完毕回调
                   $("input[name=FolderName]").val(res.RetValue.FolderName);
                   $("input[name=GuidFileName]").val(res.RetValue.GuidFileName);
                   $("input[name=FileName]").val(res.RetValue.SourceFileName);
                   $("input[name=Name]").val(res.RetValue.NotExtenFileName);
                   layer.closeAll('loading');
               }
               , error: function (index, upload) {
                   //请求异常回调
                   layer.closeAll('loading');
                   layer.msg('网络异常，请稍后重试！');
               }

    });

    function openPorjview(obj) {

        layer.open({
            type: 2
            , title: '查看详情'
            , content: '/contract/ContractCollection/Detail?Id=' + obj.data.ContId + "&rand=" + wooutil.getRandom()
           // , content: '/Project/ProjectManager/Detail?Id=' + obj.data.ProjectId + "&rand=" + wooutil.getRandom()
            , maxmin: true
            , area: ['60%', '80%']
            , btnAlign: 'c'
            , skin: "layer-nf-nfskin"
            , btn: ['关闭']// , area: [window.screen.width / 2 + 'px', window.screen.height / 2 + 'px'] //宽高

            , success: function (layero, index) {
                layer.load(0, { shade: false, time: 1 * 1000 });//2秒自动关闭
                layer.full(index);
                wooutil.openTip();
                //SetBtnBgColor();
                //  DetailBtnShowAndHide(obj);
            }
        });
    };
    function opencompview(obj) {
        layer.open({
            type: 2
            , title: '查看详情'
            , content: '/Company/Customer/Detail?Id=' + obj.data.CompId + "&rand=" + wooutil.getRandom()
            , maxmin: true
            , area: ['60%', '80%']
            , btnAlign: 'c'
            , skin: "layer-nf-nfskin"
            , btn: ['关闭']
            , success: function (layero, index) {
                layer.load(0, { shade: false, time: 1 * 1000 });
                layer.full(index);
                wooutil.openTip();

            }
        });
    };
    table.on('tool(NF-SubDev-List)', function (obj) {
        switch (obj.event) {
          
            case "Projectdetail":
                {
                    var ress = wooutil.requestpremission({
                        url: '/NfCommon/NfPermission/ViewPermission?rand=' + wooutil.getRandom(),
                        data: {
                            FuncCode: 'queryprojectview',
                            ObjId: obj.data.Id
                        }
                    });
                    if (ress.RetValue == 0) {
                        openPorjview(obj);
                    } else {
                        return layer.alert(ress.Msg);
                    }

                }
                break;
              
            case "compdetail":
                {
                    var ress = wooutil.requestpremission({
                        url: '/NfCommon/NfPermission/ViewPermission?rand=' + wooutil.getRandom(),
                        data: {
                            FuncCode: 'querycustomerview',
                            ObjId: obj.data.CompId
                        }
                    });
                    if (ress.RetValue == 0) {
                        opencompview(obj);
                    } else {
                        return layer.alert(ress.Msg);
                    }
                }
                break;

        }


    });
    exports('jiaoFu', {});
});