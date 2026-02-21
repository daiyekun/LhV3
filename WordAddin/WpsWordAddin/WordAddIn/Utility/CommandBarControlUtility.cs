using Common.Model;
using Common.Utility;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Word;
using Microsoft.Office.Tools.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordAddIn.Utility
{
    /// <summary>
    /// 操作word控件
    /// </summary>
    public class CommandBarControlUtility
    {
        public static void WirteControlLog()
        {
            StringBuilder tstrb = new StringBuilder();
            StringBuilder stb = new StringBuilder();
            StringBuilder strbp = new StringBuilder();
            StringBuilder stb1 = null;
            //WordShare.WordApp.CommandBars["Reviewing"].Controls
            foreach (CommandBar cb in WordShare.WordApp.CommandBars)
            {
                tstrb.Append(cb.Name+":"+cb.Id+":"+cb.NameLocal+":"+cb.accName+"\n"+cb.Type.ToString());
                
                if (cb.Name == "Reviewing")
                {
                    foreach (CommandBarControl cbc in cb.Controls)
                    {
                        stb.Append(cbc.Id + ":" + cbc.accName + ":" + cbc.Caption + ":" + cbc.DescriptionText + ">类型:" + cbc.Type.ToString() + "\n");
                        switch (cbc.Type)
                        {
                            case MsoControlType.msoControlDropdown:
                                {
                                    CommandBarComboBox drop = cbc as CommandBarComboBox;
                                    if (drop != null)
                                    {
                                        var lis = drop.DropDownLines;
                                    }
                                }
                                break;
                            case MsoControlType.msoControlSplitButtonPopup:
                            case MsoControlType.msoControlPopup:
                                {
                                    CommandBarPopup ctpopup = cbc as CommandBarPopup;
                                    if (cbc != null)
                                    {
                                        stb1 = new StringBuilder();
                                        foreach (CommandBarControl item in ctpopup.Controls)
                                        {
                                            stb1.Append(item.Id + ":" + item.accName + ":" + item.Caption + item.Type.ToString() + "\n");
                                            if (item.accName == "审阅窗格")
                                            {
                                                CommandBarButton barButton = item as CommandBarButton;
                                               
                                                item.Execute();
                                            }
                                            if (item.accName== "选项...")
                                            {
                                                item.Execute();
                                            }
                                        }
                                        stb.Append(cbc.Id + "子项：{" + stb1.ToString() + "}\n");
                                    }
                                }
                                break;
                           
                               

                        }









                    }
                    LogUtility.WriteLog(typeof(CommandBarControlUtility),"我是测试数据：\n"+ tstrb.ToString());
                    //LogUtility.WriteLog(typeof(CommandBarControlUtility), strbp.ToString());
                    LogUtility.WriteLog(typeof(CommandBarControlUtility), stb.ToString());
                    
                }
            }


        }


        

           
    }
}
