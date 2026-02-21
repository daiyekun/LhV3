/**
*Word插件调用Web核心代码
*@author dyk 2019-9-*
**/
layui.define(['form'], function (exports) {
    var $ = layui.$
        , admin = layui.admin
        , table = layui.table
        form = layui.form
        , setter = layui.setter;
    var userId = setter.NFData.userId;
    var localdata = layui.data(setter.tableName);
    var currentUserID = localdata.userId;
   

    var wordAddin = {
       
        OpenWord: function (HistID, target, success) {
            /// <summary>打开Word</summary>
            /// <param name="HistID" type="Number">模板历史ID</param>
            /// <param name="success" type="Function">打开后的回掉函数</param>
            var virtualPath = '';//'<%=Request.ApplicationPath%>';
            // '<%=LoginUtility.GetCurrentUserID()%>';
            var locationObj = window.location;
            if (virtualPath === '/') {
                virtualPath = '';
            }
            var baseAddr = locationObj.protocol.concat('//', locationObj.host, virtualPath, '/');
            var wT = "TplonreadOrwrite";
            //var onlyDetailTarget = false; //Ext.getCmp('ContTempBuildShowType1').getValue();//显示明细
            if (target === "TplonreadOrwrite") {//建立模板
                wT = "TplonreadOrwrite";
            } else if (target === "Tplonreadonly") {//预览
                wT = "Tplonreadonly";
            }
            else {//多出多少条显示概要
                wT = "TplonreadOrwrite";
            }

            var wordUrl = wordAddin.setAddinData('lhaddin://contractTpl/', baseAddr, HistID, 0, wT);
            console.log(wordUrl);
            var elIframe = document.createElement('IFRAME');
            elIframe.setAttribute('src', wordUrl);
            elIframe.setAttribute('width', 0);
            elIframe.setAttribute('height', 0);
            document.body.appendChild(elIframe);
            layer.msg('正在努力打开Word', {
                icon: 16
                , shade: 0.01
                //, function() {
                //    document.body.removeChild(elIframe);
                //    if (success != null) {
                //        success();
                //    }
                //}
            })
        },
        setAddinData: function (baseurl, baseAddr, contracttextHistId1, contracttextHistId2, wT) {
            var addinJson = {
                addinVar: setter.Addin.wordAddInVer,
                baseAddr: baseAddr,
                fId: contracttextHistId1,
                sId: contracttextHistId2,
                uId: currentUserID,
                wT: wT
            };
            var parmJson = JSON.stringify(addinJson);
            return baseurl + "&" + parmJson + "&json";
        },
        drfopenWord: function (contTextId, isHistory, isReadOnly, isFreeDoc, isReview, isBase) {
            /// <summary>
            /// 打开word
            /// </summary>
            /// <param name="contTextId" type="Number">合同ID</param>
            /// <param name="isHistory" type="Boolean">是历史记录ID</param>
            /// <param name="isReadOnly" type="Boolean">是只读</param>
            /// <param name="isFreeDoc" type="Boolean">是自由起草</param>            
            /// <param name="isReview" type="Boolean">审阅</param>   
            /// <param name="isBase" type="Boolean">原始版本</param>                                   

            // var myuserId = setter.NFData.userId;
            var localdata = layui.data(setter.tableName);
            var userId = localdata.userId;
            var locationObj = window.location;
            var virtualPath = '';
            var baseAddr = locationObj.protocol.concat('//', locationObj.host, virtualPath, '/');
            //var userId = woo.constant.userID;
            var headwordurl = 'lhaddin://contractText/';
            var wT = "TextDraft";//文本起草
            if (isReview) {
                headwordurl = 'lhaddin://contractReview/';
                wT = "contractReview";
            }

            if (isHistory) {
                wT = 'history_readonly';
            } else if (isReadOnly) {
                wT = 'conttext_readonly';
            }
            //if (isFreeDoc) {
            //    wT = 'freedoc';
            //}
            var success = function (data) {

                if (data.ContTempWordEdit != undefined && data.ContTempWordEdit.toLowerCase() === "true") {
                    wT = 'editable';
                    if (isBase) {
                        wT = 'basedoc';
                    }
                }

                // var wordurl = woo.wooContract.contract.setAddinData(headwordurl, woo.constant.baseUrl, contTextId, 0, wT);
                var wordurl = wordAddin.setAddinData(headwordurl, baseAddr, contTextId, 0, wT);
                console.log(wordurl);
                var elIframe = document.createElement('IFRAME');
                elIframe.setAttribute('src', wordurl);
                elIframe.setAttribute('width', 0);
                elIframe.setAttribute('height', 0);
                document.body.appendChild(elIframe);
                layer.msg('正在努力打开Word', {
                    icon: 16
                    , shade: 0.01
                }, function () {
                    document.body.removeChild(elIframe);
                    if (success != null) {
                        success();
                    }
                })

            }

            admin.req({
                url: "/ContractDraft/ContText/GetWordState",
                data: {
                    ContTextId: contTextId,
                    IsHistory: isHistory ? 'true' : 'false',
                    IsReview: isReview ? 'true' : 'false'
                },
                done: function (res) {
                    var data = res.Data;
                    if (!isFreeDoc && !isReadOnly && !isHistory) {
                        $.ajax({
                            url: "/ContractDraft/ContTextLock/Locktext",
                            data: {
                                txtId: contTextId,
                                userId: userId
                            },
                            success: function (result) {
                                success(data);
                            }
                        });

                    } else {
                        success(data);
                    }


                }
            });


        },
        contractTextPreview: function (textid, histid) {
            /// <summary>
            /// 合同文本预览
            /// </summary>
            /// <param name="textid" type="Number">合同文本ID</param>
            /// <param name="histid" type="Number">当前合同文本最新历史ID</param>
            var isHistory = false;
            var numHistoryId = parseInt(histid);
            if (isNaN(numHistoryId) || numHistoryId <= 0) {
                isHistory = false;
            } else {
                textid = numHistoryId;
                isHistory = true;
            }
            wordAddin.drfopenWord(textid, isHistory, true);

        },
        compareContractText: function (histId1, histId2) {
            /// <summary>
            /// 合同文本对比
            /// </summary>
            /// <param name="histId1" type="Number">历史版本1</param>
            /// <param name="histId2" type="Number">历史版本2</param>

            var locationObj = window.location;
            var virtualPath = '';
            var baseAddr = locationObj.protocol.concat('//', locationObj.host, virtualPath, '/');
            var wordUrl = this.setAddinData('lhaddin://contractTextCmp/', baseAddr, histId1, histId2, "wdCompare");
            // var wordUrl = + requestdata;
            console.log(wordUrl);
            var elIframe = document.createElement('IFRAME');
            elIframe.setAttribute('src', wordUrl);
            elIframe.setAttribute('width', 0);
            elIframe.setAttribute('height', 0);
            document.body.appendChild(elIframe);

            layer.msg('正在努力打开Word', {
                icon: 16
                , shade: 0.01

            }, function () {
                if (success != null) {
                    document.body.removeChild(elIframe);
                    success();
                }
            })

        }, downWord: function (param) {
            /// <summary>下载word</summary>
            /// <param name="contTextId" type="Number">合同文本ID</param>
            var localdata = layui.data(setter.tableName);
            var userId = localdata.userId;
            var headwordurl = 'lhaddin://contractFinalProcess/';
            var wT = 'saveWord';
            var locationObj = window.location;
            var virtualPath = '';
            var baseAddr = locationObj.protocol.concat('//', locationObj.host, virtualPath, '/');
            var wordurl = this.setAddinData(headwordurl, baseAddr, param.contTextId, 0, wT);
            console.log(wordurl);
            var elIframe = document.createElement('IFRAME');
            elIframe.setAttribute('src', wordurl);
            elIframe.setAttribute('width', 0);
            elIframe.setAttribute('height', 0);
            document.body.appendChild(elIframe);

            var index = 0;
            layer.msg('正在努力打开加载', {
                icon: 16
                , shade: 0.01

            }, function () {
                document.body.removeChild(elIframe);

            })

            $.ajax({
                url: '/ContractDraft/ContTextDraft/BeginDownWord',
                type: "POST",
                async: false,
                data: {
                    Id: param.contTextId
                },
                dataType: 'json',
                success: function (json) {


                }
            });
            var dolwn = function (pm) {

                wooutil.download({
                    url: pm.url,// '/NfCommon/NfAttachment/Download?DownType=' + dp,
                    Id: pm.Id,
                    folder: 6,//合同文本
                    dtype: pm.dtype,
                    downType: pm.downType
                });

            }
            var index = 0;
            var isload = false;
            var func = function () {
                var url = '/ContractDraft/ContText/GetByID';
                $.ajax({
                    url: url,
                    type: "POST",
                    async: false,
                    data: {
                        Id: param.contTextId
                    },
                    dataType: 'json',
                    success: function (json) {
                        console.log(json.Data.WordPath);
                        if (json.Data.WordPath != null && json.Data.WordPath != '' && !isload) {
                            isload = true;
                            setTimeout(dolwn(param), 3000);

                        }

                        if (index++ < 20 && !isload) {
                            setTimeout(func, 3000);

                        }
                    }
                });
            };
            if (!isload) {
                setTimeout(func, 3000);
            }
        },
        wordSavePdf: function (param) {
            /// <summary>下载Pdf</summary>
            /// <param name="contTextId" type="Number">合同文本ID</param>
            var locationObj = window.location;
            var virtualPath = '';
            var baseAddr = locationObj.protocol.concat('//', locationObj.host, virtualPath, '/');
            var headwordurl = 'lhaddin://contractFinalProcess/';
            var wordurl = wordAddin.setAddinData(headwordurl, baseAddr, param.contTextId, 0, "savePdf");
            console.log(wordurl);
            // woo.util.log(wordurl);

            var elIframe = document.createElement('IFRAME');
            elIframe.setAttribute('src', wordurl);
            elIframe.setAttribute('width', 0);
            elIframe.setAttribute('height', 0);
            document.body.appendChild(elIframe);
            layer.msg('正在努力打开加载', {
                icon: 16
                , shade: 0.01

            }, function () {
                document.body.removeChild(elIframe);

            });
            /**下载**/
            var dolwn = function (pm) {

                wooutil.download({
                    url: pm.url,
                    Id: pm.Id,
                    folder: 6,//合同文本
                    dtype: 2,
                    downType: pm.downType
                });

            }

            var index = 0;
            var isload = false;
            var func = function () {
                var url = '/ContractDraft/ContText/GetByID';
                $.ajax({
                    url: url,
                    type: "POST",
                    async: false,
                    data: {
                        ID: param.contTextId
                    },
                    dataType: 'json',
                    error: function () {
                        layer.msg("系统异常！");
                    },
                    success: function (json) {

                        if (json.Data.Path.lastIndexOf(".pdf") > 0) {

                            setTimeout(dolwn(param), 3000);
                        }

                        if (index++ < 20 && !isload) {
                            setTimeout(func, 3000);
                        } else {
                            Ext.MessageBox.hide();

                        }
                    }
                });
            };
            if (!isload) {
                setTimeout(func, 3000);
            }

        }

    }
    exports('wordAddin', wordAddin);
});