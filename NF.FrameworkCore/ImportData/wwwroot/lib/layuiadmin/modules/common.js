/**

 @Name：layuiAdmin 公共业务
 @Author：贤心
 @Site：http://www.layui.com/admin/
 @License：LPPL
    
 */

layui.define(["table"], function (exports) {
    var $ = layui.$
   , layer = layui.layer
   , laytpl = layui.laytpl
   , setter = layui.setter
   , view = layui.view
   , table = layui.table
   , admin = layui.admin;







    //公共业务的逻辑处理可以写在此处，切换任何页面都会执行
    //……


    /**
     * 获取弹框URL参数
     */
    var index = parent.layer.getFrameIndex(window.name);
    var body = layer.getChildFrame('body', index);
    $.extend({

        getUrlVars: function () {
            var vars = [], hash;
            var hashes = body.context.URL.slice(body.context.URL.indexOf('?') + 1).split('&');
            for (var i = 0; i < hashes.length; i++) {
                hash = hashes[i].split('=');
                vars.push(hash[0]);
                vars[hash[0]] = hash[1];
            }
            return vars;
        },
        getUrlVar: function (name) {
            var getval = $.getUrlVars()[name];
            return getval == undefined ? "" : getval;
        },
        getQueryString: function (name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
            var r = window.location.search.substr(1).match(reg);
            if (r != null) return unescape(r[2]); return null;
        }

    });
    /**工具类**/
    wooutil = {
        getUrlVars: function () {
            var vars = [], hash;
            var hashes = body.context.URL.slice(body.context.URL.indexOf('?') + 1).split('&');
            for (var i = 0; i < hashes.length; i++) {
                hash = hashes[i].split('=');
                vars.push(hash[0]);
                vars[hash[0]] = hash[1];
            }
            return vars;
        },
        getUrlVar: function (name) {
            /// <summary>获取URL参数</summary>
            ///<param name='name'>参数名称</param>
            var tempvl = wooutil.getUrlVars()[name];
            return tempvl === undefined ? "" : wooutil.getUrlVars()[name];
        },
        getQueryString: function (name) {
            /// <summary>获取URL参数</summary>
            ///<param name='name'>参数名称</param>
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
            var r = window.location.search.substr(1).match(reg);
            if (r != null) return unescape(r[2]); return null;
        },
        getRandom: function () {
            /// <summary>得到随机值，用于Ajax调用</summary>
            return Math.round(Math.random() * (10000 - 1)).toString();
        },
        reloadTable: function (param) {
            /// <summary>保存等操作需要刷新制定列表</summary>
            layer.msg(param.msg, { icon: 1, time: 500 }, function (msgindex) {
                if (param.nopeg != undefined) {//没有分页
                    table.reload(param.tableId, {
                        where: { rand: wooutil.getRandom() }

                    });
                } else {
                    table.reload(param.tableId, {
                        where: { rand: wooutil.getRandom() },
                        page: { curr: 1 }//重新从第 1 页开始
                    });
                }

            });
        },
        openTip: function () {
            /// <summary>打开弹框给出提示</summary>
            setTimeout(function () {
                layui.layer.tips('点击此处返回大列表', '.layui-layer-setwin .layui-layer-close', {
                    tips: 3
                });
            }, 500)
        },
        deleteDatas: function (param) {
            /// <summary>批量删除</summary>
            var checkStatus = table.checkStatus(param.tableId)
                , checkData = checkStatus.data; //得到选中的数据
            if (checkData.length === 0) {
                return layer.msg('请选择数据');
            }
            var tempId = [];
            for (var i = 0; i < checkData.length; i++) {
                tempId.push(checkData[i].Id);
            }

            layer.confirm('确定删除吗？', { icon: 3, title: '提示信息' }, function (index) {
                admin.req({
                    url: param.url,
                    data: { Ids: tempId.toString() },
                    done: function (res) {
                        if (res.RetValue == 0) {
                            wooutil.reloadTable({
                                tableId: param.tableId,
                                nopage: param.nopage

                            });
                            layer.msg('已删除');
                        } else {
                            var jsondata = JSON.stringify(res.Data);
                            return layer.alert(res.Msg + ">" + jsondata);
                        }



                    }

                })
            })

        }, deleteInfo: function (param) {
            /// <summary>列表控件单个删除</summary>
            var tempId = [];
            layer.confirm('确定要删除吗？', { icon: 3, title: '提示信息' }, function (index) {
                tempId.push(param.data.data.Id);
                admin.req({
                    url: param.url,// '/System/UserInfor/Delete',
                    data: { Ids: tempId.toString() },
                    done: function (res) {
                        if (res.RetValue == 2) {
                            param.data.del();//删除缓存
                            wooutil.reloadTable({
                                tableId: param.tableId,
                                nopage: param.nopage

                            });
                            layer.msg(res.Msg, function () {
                                if (typeof _success === 'function') {
                                    setTimeout(function () {
                                        param.success();
                                    }, 500)

                                }
                            });
                        } else
                        if (res.RetValue != 0) {
                            var jsondata = JSON.stringify(res.Data);
                            return layer.alert(res.Msg + ">" + jsondata);
                        } else {
                            param.data.del();//删除缓存
                            wooutil.reloadTable({
                                tableId: param.tableId,
                                nopage: param.nopage

                            });
                            layer.msg('已删除', function () {
                                if (typeof _success === 'function') {
                                    setTimeout(function () {
                                        param.success();
                                    }, 500)

                                }
                            });
                        }
                    }

                })
            })
        },
        OpenSubmitForm: function (param) {
            var logdindex = layer.load(0, { shade: false });
            //<summary>提交表单</summary>
            if (param.msg == undefined || param.msg == "") { param.msg = "保存成功" }
            admin.req({
                url: param.url,
                data: param.data,
                type: 'POST',
                success: function (res) {
                    layer.close(logdindex);
                    if (res.RetValue === 0) {
                        if (param.tableId !== "" && param.tableId !== undefined) {
                            if (param.showtab) {//新打开tab选项卡
                                layer.msg(param.msg, { icon: 1, time: 500 }, function (msgindex) {
                                    parent.parent.parent.layui.index.openTabsPage(param.taburl, param.tabtip);
                                    wooutil.reloadTable({ msg: param.msg, tableId: param.tableId });
                                    layer.close(param.index);
                                });




                            } else {
                                wooutil.reloadTable({ msg: param.msg, tableId: param.tableId });
                                if (param.psave) {
                                    parent.layer.close(param.index);
                                } else {
                                    layer.close(param.index); //关闭弹层
                                }
                            }
                        } else {
                            layer.msg(param.msg, { time: 500 }, function () {
                                if (param.psave) {
                                    parent.layer.close(param.index);
                                } else {
                                    layer.close(param.index); //关闭弹层
                                }
                            });
                        }

                    } else {
                        layer.msg(res.Msg,
                              {
                                 offset: '15px'
                               , icon: 5
                              });

                    }


                }
            });
        },
        UniqueValObj: function (_data) {
            ///<summary>判断字段值是否存在</summary>
            var result = true;
            admin.req({
                async: false,//取消异步
                url: _data.url,
                data: { FieldName: _data.fieldName, FieldValue: _data.inputVal, Id: _data.currId },
                type: 'POST',
                done: function (res) {
                    result = res.RetValue;

                }

            });
            return result;

        },
        adminreq: function (_url, _data, _msg, _success) {
            ///<summary>ajx请求</summary>
            admin.req({
                url: _url,
                data: _data,
                done: function (res) {
                    layer.msg(_msg, { time: 500 }, function () {
                        if (typeof _success === 'function') {
                            _success();
                        }
                    });

                }

            });

        },
        requestpremission: function (param) {
            ///<summary>获取权限</summary>
            var reqs;
            admin.req({
                async: false,//取消异步
                url: param.url,
                data: param.data,
                done: function (res) {

                    reqs = res;

                }
            });
            return reqs;

        },
        removeselpen: function () {///<summary>清除下拉框多余附加小笔头</summary>
            setTimeout(function () {
                $("select").each(function (_index, el) {

                    if ($(el).attr("lay-verify") != "required") {
                        $(el).siblings("div").find("input").css("background", "url()").css("padding-left", "0px");
                    } else {
                        $(".xm-input").addClass("pen");
                    }

                });
               
            }, 500);
        },
        selpen: function () {///<summary>添加下拉框小笔头</summary>
            setTimeout(function () {
                $("select").each(function (_index, el) {

                    if ($(el).attr("lay-verify") != "" && $(el).attr("lay-verify") != undefined) {
                        $(el).siblings("div").find("input").addClass("selectpen");
                    }

                });

            }, 500)

        },
        showView: function (param) {/// <summary>查看或修改页面绑定数据</summary>
            if (param.objId !== "" && param.objId !== undefined && parseInt(param.objId) > 0) {
               
                    admin.req({
                        url: param.url,
                        data: { Id: param.objId, rand: wooutil.getRandom() },
                        done: function (res) {
                            param.form.val(param.formFilter, res.Data);
                            if (typeof param.success === 'function') {

                                param.success(res.Data);
                            }

                        }
                    });
                
            }

        },
        download: function (param) {
            /// <summary>文件下载</summary>
            /// <param name="url" type="String">下载路径</param>
            /// <param name="Id" type="number">下载数据对象ID</param>
            /// <param name="DownType" type="number">下载类型，默认是0：1:下载模板起草最终Word</param>
            /// <param name="dtype" type="number">下载类别，默认是0：1:标识下载的是历史</param>
            /// <param name="folder" type="number">文件夹索引,参考枚举：UploadAndDownloadFoldersEnum</param>
            var _url = param.url;
            if (param.url == undefined || param.url == "") {
                _url = "/NfCommon/NfAttachment/Download";
            }
            var loadurl = setter.layupload.uploadIp + _url + '?Id=' + param.Id + "&folderIndex=" + param.folder + "&Dtype=" + param.dtype + "&DownType=" + param.downType + "&rand=" + wooutil.getRandom();
            console.log(loadurl);
            window.open(loadurl);
        },
        openPostWindow: function (url, postData, isBlank) {
            /// <summary>
            /// 打开post的新窗口
            /// </summary>        
            /// <param name="url" type="String">路径</param>
            /// <param name="postData" type="Object">Post的数据</param>
            /// <param name="isBlank" type="Boolean">打开新窗口</param>
            var form = $('#nfPostWin');
            form.remove();

            var html = '<form id="nfPostWin" action="' + url + '" target="_blank" method="post"';
            if (isBlank) {
                html += ' target="_blank"';
            }
            html += '>';
            for (var key in postData) {
                var val = postData[key];
                html += '<input type="hidden" name="' + key + '" value="' + val + '" />';
            }

            html += '</form>';

            $('body').append(html);
            form = $('#nfPostWin');
            form.submit();
            form.remove();
        },
        exportexcel: function (obj, param) {
            /// <summary>
            /// 弹出导出Excel界面
            /// </summary>        
            /// <param name="url" type="String">路径</param>
            /// <param name="postData" type="Object">Post的数据</param>
            layer.open({
                type: 1
            , title: '导出数据'
            , content: $('#ExportExcelSet')
            , area: ['370px', '340px']
            , btn: ['导出', '取消']
            , btnAlign: 'c'
            , skin: "layer-ext-myskin"
            , yes: function (index, layero) {
                var iframeWindow = window['layui-layer-iframe' + index]
                var selcell = $("input[type=radio][name=selcell]:checked").val();//所选列
                var selrow = $("input[type=radio][name=selrow]:checked").val();//所选行
                var selcellfields = [];//选择列字段
                var selcelltitles = [];//选择列标题
                var selIds = [];//选择的ID
                var tbcols = obj.config.cols;
                if (selcell == 1) {//选择列
                    $.each(tbcols[0], function (n, v) {
                        if (v.field !== "" && v.field != undefined && !v.hide) {
                            selcellfields.push(v.field);
                            selcelltitles.push(v.title);

                        }
                    });
                } else {//所有数据列
                    $.each(tbcols[0], function (n, v) {
                        if (v.field !== "" && v.field != undefined) {
                            selcellfields.push(v.field);
                            selcelltitles.push(v.title);

                        }

                    });
                }
                if (selrow == 1) {//所选行
                    var checkStatus = table.checkStatus(obj.config.id);
                    var checkdata = checkStatus.data;
                    if (checkdata.length <= 0) {
                        return layer.msg('请选择导出数据！');
                    }
                    $.each(checkdata, function (n, v) {

                        selIds.push(v.Id);
                    });
                }
                var postdata = {};
                postdata.Ids = selIds;
                postdata.SelTitle = selcelltitles;
                postdata.SelField = selcellfields;
                postdata.SelCell = selcell == 1;
                postdata.SelRow = selrow == 1;
                postdata.KeyWord = param.keyword;
                wooutil.openPostWindow(param.url, postdata, true);
                layer.close(index);
            }, success: function () {
                $("#ExportExcelSet").removeClass("layui-hide");
            }
            });
        },
        loading: function () {
            return layer.load(0, { shade: false });
        },
        numThodFormat: function (num) {
            //layer.alert(num);
            /// <summary>
            /// 千分位
            /// </summary>        
            /// <param name="num" type="String">路径</param>
            num = num.replace(/,/g, '');
            var res = num.toString().replace(/\d+/, function (n) { // 先提取整数部分
                return n.replace(/(\d)(?=(\d{3})+$)/g, function ($1) {
                    return $1 + ",";
                });
            });
            return res;
        },
        parseThousandsSeparator: function (str) {
            /// <summary>
            /// 从千分位转到数字
            /// </summary>
            /// <param name="str" type="String">千分位字符串</param>
            /// <returns type="Number" />

            str = new String(str);

            while (str.indexOf(',') > 0) {
                str = str.replace(',', '');
            }
            var num = parseFloat(str);
            if (isNaN(num))
                return 0;

            return num;
        },
        formatFoat: function (src, pos) {
            /// <summary>保留几位小数</summary>
            /// <param name="src" type="Number">原数字</param>
            /// <param name="pos" type="Number">小数位数</param>
            /// <returns type="String" />
            var val = Math.round(src * Math.pow(10, pos)) / Math.pow(10, pos);
            return val.toFixed(pos);
        },
        getPercent: function (val) {
            /// <summary>得到百分比</summary>
            /// <param name="val" type="Number">数字</param>
            /// <returns type="String" />
            val = parseFloat(val);
            if (isNaN(val))
                val = 0;
            return (val * 100).toFixed(2) + '%';
        },
        getPercent2: function (val) {
            /// <summary>得到百分比</summary>
            /// <param name="val" type="Number">数字</param>
            /// <returns type="String" />
            val = parseFloat(val);
            if (isNaN(val))
                val = 0;
            return (val * 100).toFixed(2);
        },
        opencompview: function (obj) {
            /// <summary>合同对方</summary>
            var _openuerl = '/Company/Customer/Detail?Id=' + obj.data.CompId + "&rand=" + wooutil.getRandom();
            if (obj.cType != undefined && obj.cType === 1) {
                _openuerl = '/Company/Supplier/Detail?Id=' + obj.data.CompId + "&rand=" + wooutil.getRandom();
            }
            layer.open({
                type: 2
                , title: '查看对方详情'
                , content: _openuerl
                , maxmin: true
                , area: ['60%', '80%']
                , btnAlign: 'c'
                , skin: "layer-nf-nfskin"
                , btn: ['关闭']// ,
             , success: function (layero, index) {
                 layer.load(0, { shade: false, time: 2 * 1100 });//2秒自动关闭
                 layer.full(index);
                 wooutil.openTip();

             }
            });
        },
        opencontview: function (obj) {
            /// <summary>合同查看</summary>
            layer.open({
                type: 2
                , title: '查看合同详情'
                , content: '/contract/ContractCollection/Detail?Id=' + obj.data.ContId + "&rand=" + wooutil.getRandom()
                , maxmin: true
                , area: ['60%', '80%']
                 , btnAlign: 'c'
                 , skin: "layer-nf-nfskin"
                , btn: ['关闭']
            , success: function (layero, index) {
                layer.load(0, { shade: false, time: 2 * 1100 });//2秒自动关闭
                layer.full(index);
                wooutil.openTip();
            }
            });
        },
        openprojview: function (obj) {
            /// <summary>项目查看</summary>
            layer.open({
                type: 2
                , title: '查看项目详情'
                , content: '/Project/ProjectManager/Detail?Id=' + obj.data.ProjId + "&rand=" + wooutil.getRandom()
                , maxmin: true
                , area: ['60%', '80%']
                , btnAlign: 'c'
                , skin: "layer-nf-nfskin"
                , btn: ["关闭"]
           , success: function (layero, index) {
               layer.load(0, { shade: false, time: 2 * 1100 });//2秒自动关闭
               layer.full(index);
           }
            });
        }
    };
    //其他工具
    wootool = {
        submit: function (param) {
            admin.req({
                url: param.url,
                data: param.data,
                type: 'POST',
                done: function (res) {
                    var vl = parseInt(res.RetValue);
                    if (!isNaN(vl) && vl > 0) {
                        return layer.alert(res.Msg);


                    } else {
                        
                        if (param.showtab) {
                            layer.msg(param.msg, { icon: 1, time: 500 }, function (msgindex) {
                                parent.parent.parent.layui.index.openTabsPage(param.taburl, param.tabtip);
                                // wooutil.reloadTable({ msg: param.msg, tableId: param.tableId });
                                window.parent.layui.table.reload(param.tableId, {
                                    where: { rand: wooutil.getRandom() },
                                    page: { curr: 1 }
                                });
                                layer.close(param.index);
                            });
                        } else {
                            layer.msg(param.msg, { time: 500, icon: 6 }, function () {
                                parent.layer.close(param.index);
                                window.parent.layui.table.reload(param.tableId, {
                                    where: { rand: wooutil.getRandom() },
                                    page: { curr: 1 }
                                });


                            });
                        }
                    }

                }
            });
            return false;
        }
       , del: function (param) {
           // var tempId = [];
           layer.confirm('确定要删除吗？', { icon: 3, title: '提示信息' }, function (index) {
               //tempId.push(Id);
               admin.req({
                   url: param.url,
                   data: { Ids: param.Ids.toString() },
                   done: function (res) {
                       layer.msg('已删除', { time: 400 }, function () {
                           parent.layer.close(param.index);
                           window.parent.layui.table.reload(param.tableId, {
                               where: { rand: wooutil.getRandom() },
                               page: { curr: 1 }
                           });
                       });
                   }
               });
           });
       },
        handlesucc: function (param) {
            var _time = 500;
            if (param.time != undefined) {
                _time = param.time;
            }
            layer.msg(param.msg, { time: _time, icon: 6 }, function () {
                parent.layer.close(param.index);
                window.parent.layui.table.reload(param.tableId, {
                    where: { rand: wooutil.getRandom() },
                    page: { curr: 1 }
                });
            });
        }

    };

    //退出
    try {
        admin.events.logout = function () {
            //执行退出接口
            admin.req({
                url: '/Login/ExitLogin'
                , type: 'get'
                , data: {}
                , done: function (res) { //这里要说明一下：done 是只有 response 的 code 正常才会执行。而 succese 则是只要 http 为 200 就会执行

                    //清空本地记录的 token，并跳转到登入页
                    admin.exit(function () {
                        location.href = '/';
                    });
                }
            });
        };
    } catch (e) {
       // console.log(e.message);
    }


    //对外暴露的接口
    exports('common', {});
});