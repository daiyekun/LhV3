/**
*合同新建页面标的新建
**/
layui.define(['table', 'tree', 'form', 'laydate'], function (exports) {
    var layer = layui.layer
        , $ = layui.$
        , setter = layui.setter
        , admin = layui.admin
        , table = layui.table
        , laydate = layui.laydate
        , tree = layui.tree;

    var isupdate = false;//表格是否有修改
    var updatedata = [];//修改的数据
    var tempcontId = 0;//合同ID

    var subMetBuild = {
        render: function (param) {
            tempcontId = param.contId;
            TreeInit();

            setTimeout(function () {
                subList.subtableInit({ contId: param.contId });
            }, 2500)
           // subList.subtableInit({ contId: param.contId });
            active.otherInit();
        }
    };

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
                                   //layer.alert(JSON.stringify(obj.data));
                                   // layer.alert(obj.data.id + ":" + obj.data.title);
                                   $("#selCateName").html(obj.data.title);
                                   $("#cateId").val(obj.data.id);

                                   table.reload("NF-ContSub", {
                                       page: { curr: 1 }
                                     , where: { cateIds: obj.data.id}
                                   });

                               }
                           });
            }
        });
    };



    /***********************合同标的列表-begin***************************************************************************************************/
    var subList = {
        ZheKouLv: function (price, salprice, eobj) {//折扣率
            /// <summary>
            /// 计算折扣率
            /// </summary>
            /// <param name="price" type="String">单价</param>
            /// <param name="salprice" type="String">销售报价</param>
            ///<return type="string">折扣率</return>
            var _val = 0;
            var _price = parseFloat(price);//单价
            var _saleprice = parseFloat(salprice);//销售报价
            if (!isNaN(_price) && _price > 0 && !isNaN(_saleprice) && _saleprice > 0) {
                _val = _saleprice / _price;
            } 
            
            var tempobj = new Object();
            tempobj.Price = _price;
            tempobj.SalePrice = _saleprice;
            tempobj.DiscountRate = wooutil.getPercent2(_val)
            eobj.update(tempobj);
            eobj.data.Price = _price;
            eobj.data.DiscountRate = wooutil.getPercent2(_val);//折扣率
            eobj.data.SalePrice = _saleprice;
           

        },
        XiaoJi: function (price, amount, eobj) {
            /// <summary>
            /// 计算小计
            /// </summary>
            /// <param name="price" type="String">单价</param>
            /// <param name="amount" type="String">数量</param>
            ///<param name="eobj" type="Object">修改行</param>
            var famount = parseFloat(amount);//数量
            var fprice = parseFloat(price);//单价
            if (isNaN(famount)) {
                famount = 0;
            }
            if (isNaN(fprice)) {
                fprice = 0;
            }
            var stotal = famount * fprice;
            var subtotalthod = wooutil.numThodFormat(stotal.toString());
            var tempobj = new Object();
            tempobj.SubTotalThod=subtotalthod;
            tempobj.SubTotal=stotal;
            tempobj.Amount = famount;
            tempobj.Price = fprice;
            eobj.update(tempobj);
            eobj.data.Price = fprice;
            eobj.data.SubTotal = stotal;//小计

        },
        subtableInit: function (param) {//标的列表初始化
            table.render({
                    elem: '#NF-ContSub'
                   , url: '/Contract/ContSubjectMatter/GetList?contId=' + param.contId + '&rand=' + wooutil.getRandom()
                   , toolbar: '#toolcontsub'
                   , defaultToolbar: ['filter']
                   , cols: [[
                       { type: 'numbers', fixed:'left' }
                       , { type: 'checkbox', fixed:'left' }
                       , { field: 'Name', title: '标的名称', width: 120, edit: 'text', fixed:'left' }
                       , { field: 'Spec', title: '规格', width: 120, edit: 'text' }
                       , { field: 'Stype', title: '型号', width: 120, edit: 'text' }
                       , { field: 'Unit', title: '单位', width: 120, edit: 'text' }
                       , { field: 'PriceThod', title: '单价', width: 120, edit: 'text' }
                       , { field: 'Amount', title: '数量', width: 120, edit: 'text' }
                       , { field: 'SubTotalThod', title: '小计', width: 120 }
                       , { field: 'SalePriceThod', title: '报价', width: 120, edit: 'text' }
                       , { field: 'DiscountRate', title: '折扣率(%)', width: 120 }
                       , { field: 'PlanDateTime', title: '计划交付日期', width: 130, edit: 'text', event: 'Plandate' }
                       , { field: 'Remark', title: '备注', width: 130, edit: 'text' }
                       , { field: 'Id', title: 'Id', width: 50, hide: true }
                       //, { title: '操作', width: 150, align: 'center', fixed: 'right', toolbar: '#tabl-contsubbar' }
                   ]]
                   , page: true
                   , loading: true
                   , height: setter.table.height_tab
                   , limit: setter.table.limit_tab
                , limits: setter.table.limits
                , done: function (res) {
                    active.subtotal();//标的总额

                }
            });
            /**编辑**/
            table.on('edit(NF-ContSub)', function (obj) {
                isupdate = true;
                var data = obj.data; //得到所在行所有键值
                var filed = obj.field;
                switch (filed) {
                    case "Amount"://数量
                        {
                            subList.XiaoJi(obj.data.Price, obj.value, obj);   
                        }
                        break;
                    case "PriceThod"://单价
                        {
                            subList.XiaoJi(obj.value, obj.data.Amount, obj);
                            subList.ZheKouLv(obj.value, obj.data.SalePrice,obj);
                            
                        }
                        break;
                       
                    case "SalePriceThod"://销售报价
                        {
                            subList.ZheKouLv(obj.data.Price, obj.value, obj);
                        }
                        break;




                }
                //修改值
                for (var i = 0; i < updatedata.length; i++) {
                    if (updatedata[i].Id === obj.data.Id) {
                        updatedata.splice(i, 1);
                    }
                }
                updatedata.push(obj.data);
            });
            var submatterEvent = {
                batchdel: function () {
                    /// <summary>列表头部-批量删除</summary>
                    wooutil.deleteDatas({ tableId: 'NF-ContSub', url: '/Contract/ContSubjectMatter/Delete' });
                },
                tooldel: function (obj) {
                    /// <summary>列表操作栏-删除</summary>
                    ///<param name='obj'>删除数据对象</param>
                    wooutil.deleteInfo({ tableId: "NF-ContSub", data: obj, url: '/Contract/ContSubjectMatter/Delete' });
                }

            };

            //列表操作栏
            table.on('tool(NF-ContSub)', function (obj) {
                var _data = obj.data;
                switch (obj.event) {
                    case 'del':
                        submatterEvent.tooldel(obj);
                        break;
                    case 'edit':
                        submatterEvent.tooledit(obj);
                        break;
                    case "Plandate"://计划日期
                        {
                            var newdata = {};
                            var field = $(this).data('field');
                            laydate.render({
                                elem: this.firstChild
                                , show: true //直接显示
                                , closeStop: this
                                , type: 'date'
                                , format: "yyyy-MM-dd"
                                , done: function (value, date) {
                                    isupdate = true;
                                    newdata[field] = value;
                                    obj.update(newdata);
                                    for (var i = 0; i < updatedata.length; i++) {
                                        if (updatedata[i].Id === obj.data.Id) {
                                            updatedata.splice(i, 1);
                                        }
                                    }
                                    obj.data.PlanDateTime = value;
                                    updatedata.push(obj.data);

                                }
                            });
                        }
                        break;
                    default:
                        layer.alert("暂不支持（" + obj.event + "）");
                        break;
                }
            });
        }

    }

    var active = {
        addLine: function () {//增加行
            var lbId = $("#cateId").val();
            var linenum = $("#lineNum").val();
           
            if (lbId === null || lbId === undefined || lbId==="") {
                layer.alert("请选择品类！");
            } else if (lbId ==-1) {
                if (linenum === null || linenum==="" || linenum === undefined) {
                   return layer.alert("请输入行数！");
                } else {
                    admin.req({
                        url: '/Contract/ContSubjectMatter/AddLine?rand=' + wooutil.getRandom()
                      , data: { lineNum: linenum }
                      , success: function (res) {
                          table.reload("NF-ContSub", {
                              page: { curr: 1 }
                             , where: {}
                          });
                      }
                    });
                }
            } else {//选择业务品类
                layer.open({
                    type: 2
               , title: '选择单品'
               , content: '/NfCommon/SelectItem/SelectBcInstance'
               , area: ['95%', '95%']
               , btn: ['确定', '取消']
               , btnAlign: 'c'
               , skin: "layer-ext-myskin"
               , yes: function (index, layero) {
                   var iframeWindow = window['layui-layer-iframe' + index];
                   var checkStatus = iframeWindow.layui.table.checkStatus('NF-SelectBcInstance-Index')
                   checkData = checkStatus.data; //得到选中的数据
                   if (checkData.length === 0) {
                       return layer.msg('请选择数据!');
                   } else {
                       var tempIds = [];
                       for (var i = 0; i < checkData.length; i++) {
                           tempIds.push(checkData[i].Id);
                       }
                       admin.req({
                           url: '/Contract/ContSubjectMatter/AddBcLine?rand=' + wooutil.getRandom()
                      , data: { bcIds: tempIds.toString() }
                      , success: function (res) {
                          table.reload("NF-ContSub", {
                              page: { curr: 1 }
                              , where: {}
                          });

                      }
                       });

                       layer.close(index);

                   }
               }
                });

            }
        },
        SaveAll: function () {
            if (isupdate) {
                admin.req({
                    url: '/Contract/ContSubjectMatter/SaveData',
                    data: { subs: updatedata },
                    type: 'POST',
                    done: function (res) {
                        //清空变量，防止重复提交
                        updatedata = [];
                        isupdate = false;
                        active.subtotal();//标的总额
                       return layer.msg('保存成功', { icon: 1 });
                    }
                });
            } else {
                return layer.msg('数据没有任何修改！', { icon: 5 });
            }
        },
        delsel: function () {//删除标的
            var checkStatus = table.checkStatus('NF-ContSub');
            if(checkStatus.data.length<=0){
                layer.msg('请选择数据！');
            } else {
                var deldata = [];
                for (var i = 0; i < checkStatus.data.length; i++) {
                    deldata.push(checkStatus.data[i].Id);
                }

                admin.req({
                    url: '/Contract/ContSubjectMatter/DelSelSub',
                    data: { Ids: deldata.toString() },
                    type: 'POST',
                    done: function (res) {
                        active.subtotal();//标的总额
                        return layer.msg('删除成功', { icon: 1, time: 500 }, function () {
                            table.reload("NF-ContSub", {
                                page: { curr: 1 }
                                 , where: {}
                            });
                        });
                    }
                });

            }
    
            
        },
        otherInit: function () {
            $('.layui-btn.layuiadmin-btn-tags').on('click', function () {
                var type = $(this).data('type');
                active[type] ? active[type].call(this) : '';
            });
        }, subtotal: function () {//计算标的总金额

            admin.req({
                url: '/Contract/ContSubjectMatter/GetSubTotal',
                data: { contId: tempcontId },
                type: 'POST',
                done: function (res) {
                    $("#subtotalsapn").text(res.Data.TotalAmThod);
                }
            });


        }
    }

    /***********************合同标的列表-end***************************************************************************************************/



    exports('subMetBuild', subMetBuild);
});
