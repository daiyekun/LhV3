using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MsWord = Microsoft.Office.Interop.Word;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Word;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Windows.Forms;
using Common.Model;
using Common.Utility;

namespace WordAddIn.Utility
{
    /// <summary>
    /// 模板起草操作类
    /// </summary>
    public class ContTextDraftUntitly
    {
        /// <summary>
        /// 写入异常
        /// </summary>
        /// <param name="ex"></param>
        private static void WriteLog(Exception ex)
        {
            LogUtility.WriteLog(typeof(ContTextDraftUntitly), ex);
            
        }
        /// <summary>
        /// 写入异常在返回
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        private static string LogToResult(Exception ex,string msg="")
        {
            WriteLog(ex);
            return string.IsNullOrEmpty(msg)? "ERR":msg;
        }
        /// <summary>
        /// 文件保存
        /// </summary>
        /// <returns></returns>
        public static string ContTextSave(string strFileToUpload)
        {
            try
            {
                //请求路径
                var HttprequestUrl = WordShare.BaseUrl + "ContractDraft/ContTextDraft/SaveContractTextDoc";
                StringBuilder strb = new StringBuilder(HttprequestUrl);
                strb.Append("?uid=" + WordShare.UserId);//登录人ID
                strb.Append("&cttextid=" + WordShare.TempId);//合同文本ID
                var requst = HttpWebRequestOptionFile.MyUploader(strFileToUpload, strb.ToString());
                return requst;
            }
            catch (Exception ex)
            {

               return LogToResult(ex);
            }

        }
        /// <summary>
        /// 标的信息获取
        /// </summary>
        /// <returns></returns>
        public static string GetContractObjectTableIDList()
        {
            //请求路径
            //try
            //{
            //    //var HttprequestUrl = WordShare.BaseUrl + "/BLL/ModuleBusiness/Draft/Operation.aspx?cmd=/ContractAuthoring/getContractObjectTableIDList";
            //    var HttprequestUrl = WordShare.BaseUrl + "/ContractDraft/ContTextDraft/GetContractObjectTableIDList?cmd=/ContractAuthoring/getContractObjectTableIDList";
            //    StringBuilder strb = new StringBuilder(HttprequestUrl);
            //    strb.Append("&uid=" + WordShare.UserId);//登录人ID
            //    strb.Append("&cttextid=" + WordShare.TempId);//合同文本ID
            //    var Responsestr = HttpRequestUtility.HttpPost(HttprequestUrl, strb.ToString());
            //    return Responsestr;
            //}
            //catch (Exception ex)
            //{

            //    return LogToResult(ex);
            //}
            var HttprequestUrl = WordShare.BaseUrl + "ContractDraft/ContTextDraft/GetContractObjectTableIDList";
            StringBuilder strb = new StringBuilder(HttprequestUrl);
            //请求路径
            try
            {
                //var HttprequestUrl = WordShare.BaseUrl + "/BLL/ModuleBusiness/Draft/Operation.aspx?cmd=/ContractAuthoring/getContractObjectTableIDList";


                // strb.Append("cmd=getContractObjectTableIDList");
                strb.Append("?uid=" + WordShare.UserId);//登录人ID
                strb.Append("&cttextid=" + WordShare.TempId);//合同文本ID
                var Responsestr = HttpRequestUtility.HttpPost(HttprequestUrl, strb.ToString());
                return Responsestr;
            }
            catch (Exception ex)
            {

                //return LogToResult(ex,ex.Message+"url:"+ strb.ToString());
                LogUtility.WriteLog(typeof(ContTextDraftUntitly), $"{ex.Message}请求路：{strb.ToString()}");
                return LogToResult(ex);
            }


        }
        /// <summary>
        /// 获取标的表信息返回list集合
        /// </summary>
        /// <returns>Ilist:ContractObjectTable</returns>
        public static IList<ContractObjectTable> GetContractObjectTableIDList_ConvertObj()
        {
            var strobj = GetContractObjectTableIDList();
            var listobj = JsonHelper.DeserializeJsonToList<ContractObjectTable>(strobj);
            return listobj;

        }
        /// <summary>
        /// 获取标的数据-参数的内容都来至GetContractObjectTableIDList
        /// </summary>
        /// <param name="bc_id">业务类实例ID</param>
        /// <param name="field_type">字段类型</param>
        /// <returns></returns>
        public static string getContractObjectsTableData(int bc_id, int field_type)
        {
            //请求路径2
            //var HttprequestUrl = WordShare.BaseUrl + "/BLL/ModuleBusiness/Draft/Operation.aspx?cmd=/ContractAuthoring/getContractObjectsTableData";
            try
            {
                var HttprequestUrl = WordShare.BaseUrl + "ContractDraft/ContTextDraft/GetContractObjectsTableData";
                StringBuilder strb = new StringBuilder(HttprequestUrl);
                //strb.Append("cmd =/ContractAuthoring/getContractObjectsTableData");//登录人ID
                strb.Append("?uid=" + WordShare.UserId);//登录人ID
                strb.Append("&cttextid=" + WordShare.TempId);//合同文本ID
                strb.Append("&bc_id=" + bc_id);//业务类实例ID
                strb.Append("&field_type=" + field_type);//字段类型
                var Responsestr = HttpRequestUtility.HttpPost(HttprequestUrl, strb.ToString());
                return Responsestr;
            }
            catch (Exception ex)
            {

                return LogToResult(ex);
            }

        }
      
        #region 添加标的表格
        /// <summary>
        /// 添加标的表格数据
        /// </summary>
        /// <param name="Control">标的明细控件或者标的概要</param>
        /// <returns>返回200成功</returns>
        public int ShowSubjectMatter(MsWord.ContentControl Control)
        {

            MessageBox.Show("表格");


            try
            {
                //var rng = Control.Range;
                Control.Range.Text = "";
                System.Object missing = System.Type.Missing;
                IList<ContractObjectTable> listtabObj = GetContractObjectTableIDList_ConvertObj();

                var tabscount = listtabObj != null ? listtabObj.Count() : 0;
                if (tabscount > 0)
                {
                    //listtabObj[0].CONFINE_NUM:标示如果超过一定数量都需要显示标的概要，而27是标的明细。所以不需要执行
                    //if (listtabObj[0].CONFINE_NUM > 0 && Control.Tag == "27")
                    //{
                    //    return 100;
                    //}
                    //else if (listtabObj[0].CONFINE_NUM <= 0 && Control.Tag == "38")
                    //{//小于等于0只显示标的明细，38是概要所以直接返回
                    //    return 101;
                    //}

                    if (Control.Tag == "27")
                    {
                        WordDocumentHelper.DocProtectOrUnProtect(1);
                        Control.LockContents = false;
                        Control.LockContentControl = false;
                        //Control.Range.InsertBefore(listtabObj[0].DETAIL_TITLE);
                        //Control.Range.ran.InsertBefore("测试123");
                        Control.Range.Font.Name = "宋体";
                        Control.Range.Font.Size = 10;
                        Control.Range.InsertParagraphAfter();//段落标记
                        Control.Range.SetRange(Control.Range.End, Control.Range.End);
                        //Control.Range.InsertParagraphAfter();//段落标记
                        //Control.Range.SetRange(Control.Range.End, Control.Range.End);
                        var tableIndex = 0;
                        //创建表格
                        CreateSubTable(Control.Range, ref missing, listtabObj, tabscount, ref tableIndex);
                        //Control.Range.Tables.Add(Control.Range, 3, 4, ref missing, ref missing);
                    }

                }

                return 200;
            }
            catch (Exception ex)
            {

                 WriteLog(ex);
                return 500;
            }
        
        
        }
        /// <summary>
        /// 创建表格
        /// </summary>
        /// <param name="rng">标的明细控件Range对象</param>
        /// <param name="missing">默认值</param>
        /// <param name="listtabObj">数据源</param>
        /// <param name="tabscount">table总数</param>
        /// <param name="tableIndex">table索引</param>
        private void CreateSubTable(MsWord.Range rng, ref System.Object missing, IList<ContractObjectTable> listtabObj, int tabscount, ref int tableIndex)
        {
            try
            {

                foreach (var info in listtabObj)
                {
                    tableIndex++;
                    var jsontext = getContractObjectsTableData(info.BC_ID, info.FIELD_TYPE);
                    JArray array = (JArray)JsonConvert.DeserializeObject(jsontext);
                    var rowcount = array.Count();//行数

                    if (rowcount > 0)
                    {
                        var cellcount = array[0].Count();
                        //创建表格及设置表格
                        MsWord.Table tbl = null;

                        if (tableIndex == 1)
                        {
                            if (!string.IsNullOrEmpty(info.BC_DESC))
                            {
                                rng.InsertBefore(info.BC_DESC);
                                rng.InsertParagraphAfter();//段落标记
                                rng.SetRange(rng.End, rng.End);
                            }
                            tbl = rng.Tables.Add(rng, rowcount, cellcount, ref missing, ref missing);

                        }
                        else
                        {

                            //不管有多少个表格，索引都是1
                            object end = rng.Tables[1].Range.End;
                            MsWord.Range tableLocation = WordShare.WordApp.ActiveDocument.Range(ref end, ref end);
                            tableLocation.InsertParagraphAfter();//插入一个段落
                            tableLocation.InsertBefore(info.BC_DESC);
                            tableLocation.SetRange(tableLocation.End, tableLocation.End);
                            tbl = rng.Tables.Add(tableLocation, rowcount, cellcount, ref missing, ref missing);

                        }
                        tbl.Range.Font.Name = "宋体";
                        tbl.Range.Font.Size = 10;
                        tbl.Columns.DistributeWidth();
                        tbl.Borders.OutsideLineStyle = MsWord.WdLineStyle.wdLineStyleSingle;
                        tbl.Borders.InsideLineStyle = MsWord.WdLineStyle.wdLineStyleSingle;
                        tbl.BottomPadding = 0;
                        tbl.TopPadding = 0;
                        //添加数据
                        SetTableData(array, rowcount, cellcount, tbl, info.CONFINE_NUM);


                    }
                }
            }
            catch (Exception ex)
            {

                WriteLog(ex);
            }
        }
        /// <summary>
        /// 为Table表添加数据
        /// </summary>
        /// <param name="array">数据源</param>
        /// <param name="rowcount">总行数</param>
        /// <param name="cellcount">总列数</param>
        /// <param name="tbl">Table 对象</param>
        /// <param name="ShowNumRow">能够显示列表的最大极限行</param>
        private  void SetTableData(JArray array, int rowcount, int cellcount, MsWord.Table tbl,int ShowNumRow)
        {
            var datarow = rowcount - 2;//去掉标题行和结尾合计行存数据总行数
            if (ShowNumRow > 0 && datarow > ShowNumRow)
            {//显示概要
                CreateSubMatterMarkTable(array, rowcount, cellcount, tbl);

            }
            else
            {
                CreateSubMatterDetailTable(array, rowcount, cellcount, tbl);

            }
        }
         
        /// <summary>
        /// 生成标的明细
        /// </summary>
        /// <param name="array">数据</param>
        /// <param name="rowcount">行总数</param>
        /// <param name="cellcount">列总数</param>
        /// <param name="tbl">Table表格</param>
        private void CreateSubMatterDetailTable(JArray array, int rowcount, int cellcount, MsWord.Table tbl)
        {
            //标的明细显示
            for (int r = 1; r <= rowcount; r++)
            {
                for (int c = 1; c <= cellcount; c++)
                {
                    if (r < rowcount)
                    {

                        tbl.Cell(r, c).Range.Text = array[(r - 1)][(c - 1)].ToString();
                        tbl.Cell(r, c).VerticalAlignment = MsWord.WdCellVerticalAlignment.wdCellAlignVerticalCenter;//居中
                    }
                    else
                    { //合计行

                        ShowLastRowMerge(array, cellcount, tbl, r, c);


                    }

                }

            }
        }
        /// <summary>
        /// 超过一定行数生成标的概要
        /// </summary>
        /// <param name="array">数据</param>
        /// <param name="rowcount">行数</param>
        /// <param name="cellcount">单元格</param>
        /// <param name="tbl">Table表格</param>
        private void CreateSubMatterMarkTable(JArray array, int rowcount, int cellcount, MsWord.Table tbl)
        {
            for (int r = 1; r <= rowcount; r++)
            {
                for (int c = 1; c <= cellcount; c++)
                {
                    if (r == 1)
                    {
                        tbl.Cell(r, c).Range.Text = array[(r - 1)][(c - 1)].ToString();
                        tbl.Cell(r, c).VerticalAlignment = MsWord.WdCellVerticalAlignment.wdCellAlignVerticalCenter;//居中
                    }
                    else if (r == 2 && c == 1)
                    {


                        SetGaiYaoXinXi(array, rowcount, cellcount, tbl, r, c);
                        break;

                    }
                    else if (r == rowcount)
                    { //合计行

                        ShowLastRowMerge(array, cellcount, tbl, r, c);


                    }
                    else
                    {
                        break;
                    }

                }

            }
        }
        /// <summary>
        /// 添加概要信息
        /// </summary>
        /// <param name="array">数据源</param>
        /// <param name="rowcount">总行数</param>
        /// <param name="cellcount">总列数</param>
        /// <param name="tbl">表对象</param>
        /// <param name="r">当前行</param>
        /// <param name="c">当前列</param>
        private  void SetGaiYaoXinXi(JArray array, int rowcount, int cellcount, MsWord.Table tbl, int r, int c)
        {
            tbl.Cell(r, c).Merge(tbl.Cell((rowcount - 1), cellcount));//合并
            StringBuilder strb = new StringBuilder();
            StringBuilder strb1 = new StringBuilder();
            strb.Append("合同标的包括");
            for (int i = 0; i < array.Count; i++)
            {
                strb1.Append("'" + array[i][0].ToString() + "'、");
            }
            var strtmp = strb1.ToString().TrimEnd('、');
            strb.Append(strtmp);
            strb.Append(string.Format("等供有{0}条。详情请查看合同系统！", (rowcount - 2)));
            tbl.Cell(r, c).Range.Text = strb.ToString();
        }
        /// <summary>
        /// 最后一行数据显示加合并
        /// </summary>
        /// <param name="array">数据源</param>
        /// <param name="cellcount">总列数</param>
        /// <param name="tbl">表对象</param>
        /// <param name="r">当前行</param>
        /// <param name="c">当前列</param>
        private  void ShowLastRowMerge(JArray array, int cellcount, MsWord.Table tbl, int r, int c)
        {
            switch (c)
            {
                case 1:
                    {
                        tbl.Cell(r, c).Range.Text = array[(r - 1)][(c - 1)].ToString();
                        tbl.Cell(r, c).VerticalAlignment = MsWord.WdCellVerticalAlignment.wdCellAlignVerticalCenter;//居中
                    }
                    break;
                case 2:
                    {
                        if (cellcount >= 5)
                        {
                            tbl.Cell(r, c).Merge(tbl.Cell(r, (cellcount - 2)));
                        }
                        else
                        {
                            tbl.Cell(r, c).Merge(tbl.Cell(r, (cellcount - 1)));
                        }

                        tbl.Cell(r, c).Range.Text = array[(r - 1)][(c - 1)].ToString();
                    }
                    break;

                default:
                    {
                        if (c == cellcount)
                        {
                            if (cellcount >= 5)
                            {

                                tbl.Cell(r, 3).Merge(tbl.Cell(r, 4));
                                tbl.Cell(r, 3).Range.Text = array[(r - 1)][(c - 1)].ToString();
                            }
                            else
                            {
                                tbl.Cell(r, 3).Range.Text = array[(r - 1)][(c - 1)].ToString();
                            }

                        }

                    }
                    break;
            }
        }

        

        #endregion 添加标的表格

        #region 文件本解锁

        /// <summary>
        /// 给合同文本解锁
        /// </summary>
        /// <returns>SUC:成功</returns>
        public static string UnLockContText()
        {

            try
            {
                //请求路径2
                var HttprequestUrl = WordShare.BaseUrl + "/ContractDraft/ContTextLock/UnLockText";
                StringBuilder strb = new StringBuilder(HttprequestUrl);
                strb.Append("cmd=/ContractTextLock/Unlock");
                strb.Append("&uid=" + WordShare.UserId);//登录人ID
                strb.Append("&TextId=" + WordShare.TempId);//合同文本ID
                var Responsestr = HttpRequestUtility.HttpPost(HttprequestUrl, strb.ToString());
                return Responsestr;
            }
            catch (Exception ex)
            {
                LogUtility.WriteLog(typeof(ContTextDraftUntitly),ex.ToString());

                return "";
            }

        }

        #endregion

        #region 
        /// <summary>
        /// 文件下载word保存
        /// </summary>
        /// <returns></returns>
        public static string ContTextRawSave(string strFileToUpload)
        {
            try
            {
                //请求路径
                var HttprequestUrl = WordShare.BaseUrl + "ContractDraft/ContTextDraft/SaveWordRaw";
                StringBuilder strb = new StringBuilder(HttprequestUrl);
                //strb.Append("?cmd=SaveWordRaw");
                strb.Append("?uid=" + WordShare.UserId);//登录人ID
                strb.Append("&cttextid=" + WordShare.TempId);//合同文本ID
                var requst = HttpWebRequestOptionFile.MyUploader(strFileToUpload, strb.ToString());
                return requst;
            }
            catch (Exception ex)
            {

                LogUtility.WriteLog(typeof(ContTextDraftUntitly), ex.ToString());

                return "";
            }

        }

        /// <summary>
        /// 文件下载word保存
        /// </summary>
        /// <returns></returns>
        public static string SaveWordPdf(string strPdfPath)
        {
            //请求路径
            //var HttprequestUrl = WordShare.BaseUrl + "/AjaxPage/WooContractDraft/ContText.aspx?cmd=SaveWordPdf";
            var HttprequestUrl = WordShare.BaseUrl + "ContractDraft/ContTextDraft/SaveWordPdf";
            StringBuilder strb = new StringBuilder(HttprequestUrl);
            strb.Append("?uid=" + WordShare.UserId);//登录人ID
            strb.Append("&cttextid=" + WordShare.TempId);//合同文本ID
            var requst = HttpWebRequestOptionFile.MyUploader(strPdfPath, strb.ToString());
            return requst;

        }
        #endregion





    }
}
