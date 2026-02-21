using Common.Model;
using Common.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WordAddIn.ClassLib
{
  /// <summary>
  /// 模板变量帮助类
  /// </summary>
  public  class VariableHelper
    {
       

       /// <summary>
        /// 新增自定义变量
        /// </summary>
        /// <returns></returns>
      public static ContractVariable AddContCustomVar(CustomVarHandleForm varFom, string name)
      {

          if (string.IsNullOrEmpty(name))
          {
              MessageBox.Show("请输入自定义变量名称！");
          }
          else
          {
              if (varFom.IsUpdate == 0)
              {//新增

                  var result = AddCustomVar(name);
                  varFom.lisCustomBox.ValueMember = "VarName";
                  varFom.lisCustomBox.DisplayMember = "VarLabel";
                  varFom.lisCustomBox.Items.Add(result);
                  varFom.Close();
                  MessageBox.Show("操作成功！");
              }
              else
              { //修改

                  var resul = RenCustomVarName(Convert.ToInt32(varFom.TempValue.VarName), name);
                  if (resul == "SUC")
                  {

                      var variable = new ContractVariable();
                      variable.VarName = varFom.TempValue.VarName;
                      variable.VarLabel = name;
                      varFom.lisCustomBox.Items[varFom.lisCustomBox.SelectedIndex] = variable;
                      varFom.Close();
                      MessageBox.Show("操作成功！");
                  }
                  else
                  {
                      MessageBox.Show("操作失败！");
                  }
              }

          }

          return null;

      }


      /// <summary>
        /// 新增自定义变量
        /// </summary>
        /// <returns></returns>
      public static ContractVariable AddCustomVar(string name)
        {
            try
            {

                string cmdStr = "?cmd=/ContractTpl/getCustomContractVarName";
                string func = "GetCustomContractVarName";
                StringBuilder strb = new StringBuilder();
                strb.Append("tplid=" + WordShare.TempId);//模板历史ID
                strb.Append("&varLabel=" + name);
                var responsestr = HttpRequestUtility.SubmitPostRequestCore(strb.ToString(), CreateUrlType.Temp, func, cmdStr);
                ContractVariable info = JsonHelper.DeserializeJsonToObject<ContractVariable>(responsestr);
                return info;
            }
            catch (Exception ex)
            {

                LogUtility.WriteLog(typeof(VariableHelper), ex);
                return null;
            }

        }

        /// <summary>
        /// 重命名
        /// </summary>
        /// <returns></returns>
        public static string RenCustomVarName(int Id, string NewName)
        {
            try
            {
               

                string cmdStr = "?cmd=/ContractTpl/renameCustomContractVarName";
                string func = "RenameCustomContractVarName";
                StringBuilder strb = new StringBuilder();
                strb.Append("varId=" + Id);
                strb.Append("&varName=" + NewName);
                strb.Append("&tplid=" + WordShare.TempId);//模板历史ID
                var responsestr = HttpRequestUtility.SubmitPostRequestCore(strb.ToString(), CreateUrlType.Temp, func, cmdStr);
                return responsestr;
      
            }
            catch (Exception ex)
            {
                LogUtility.WriteLog(typeof(VariableHelper), ex);
                return "";
            }
        }

        /// <summary>
        /// 删除自定义变量名称
        /// </summary>
        /// <returns></returns>
        public static string DelCustomVar(int Id)
        {
            try
            {
              
                string cmdStr = "?cmd=/ContractTpl/delCustomContractVarName";
                string func = "DelCustomContractVarName";
                StringBuilder strb = new StringBuilder();
                strb.Append("varId=" + Id);
                strb.Append("&tplid=" + WordShare.TempId);//模板历史ID
                var responsestr = HttpRequestUtility.SubmitPostRequestCore(strb.ToString(), CreateUrlType.Temp, func, cmdStr);
                return responsestr;
            }
            catch (Exception)
            {

                return "";
            }
        }


        /// <summary>
        ///系统变量获取
        /// </summary>
        /// <param name="getAll">是否是获取所有</param>
        /// <param name="isDraft">是不是生成文本，比如模板起草</param>
        /// <returns></returns>
        public static IList<ContractVariable> GetListSystemVarData(bool getAll = false, bool isDraft = false)
        {
            CreateUrlType urlType = CreateUrlType.Temp;
            string cmdStr = "?cmd=/ContractTpl/GetContractVariables";
            string func = "GetContractVariables";
            StringBuilder strb = new StringBuilder();
            if (isDraft)
            {
                urlType = CreateUrlType.TextDraft;
                cmdStr = "?cmd=/ContractAuthoring/getContractVariables";
                func = "GetContractVariables";
                strb.Append("uid=" + WordShare.UserId);
                strb.Append("&cttextid=" + WordShare.TempId);
                strb.Append("&getAll=" + getAll);
            }
            else
            {

                strb.Append("uid=" + WordShare.UserId);
                strb.Append("&tplid=" + WordShare.TempId);
                strb.Append("&getAll=" + getAll);
            }
            strb.Append("&locale=zh-cn");
            //var responsestr = HttpRequestUtitly.HttpPost4(requrl, strb.ToString());
            var responsestr = HttpRequestUtility.SubmitPostRequestCore(strb.ToString(), urlType, func, cmdStr);
            IList<ContractVariable> listdata = JsonHelper.DeserializeJsonToList<ContractVariable>(responsestr);
            return listdata;
        }

        /// <summary>
        /// 自定义变量获取
        /// </summary>
        /// <param name="getAll">是不是获取所有</param>
        /// <param name="isDraft">是不是生成文本，比如模板起草</param>
        /// <returns></returns>
        public static IList<ContractVariable> GetListCustomVarData(bool getAll=true,bool isDraft=false)
        {
            CreateUrlType urlType = CreateUrlType.Temp;
            string func = "GetTemplateCustVars";
            string cmdStr = "?cmd=/ContractTpl/getTemplateCustVars";
            StringBuilder strb = new StringBuilder();
            if (isDraft)
            {//生成文本
                urlType = CreateUrlType.TextDraft;
                 cmdStr = "?cmd=/ContractAuthoring/getCustomVariables";
                func = "GetCustomVariables";
                 strb.Append("uid=" + WordShare.UserId);
                 strb.Append("&cttextid=" + WordShare.TempId);
            }
            else{
            
           
           // strb.Append("");
            strb.Append("uid=" + WordShare.UserId);
            strb.Append("&tplId=" + WordShare.TempId);
            strb.Append("&getAll=" + getAll);
            
           

            }
            var responsestr = HttpRequestUtility.SubmitPostRequestCore(strb.ToString(), urlType, func, cmdStr);
            IList<ContractVariable> listdata = JsonHelper.DeserializeJsonToList<ContractVariable>(responsestr);
            return listdata;
        }


        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="strb"></param>
        /// <returns></returns>
        private static string SubmitRequst(StringBuilder strb)
        {
            return HttpRequestUtility.SubmitPostRequest(strb.ToString(), CreateUrlType.Temp);
        }

        /// <summary>
        /// 保存模板和系统变量的关系
        /// </summary>
        /// <returns></returns>
        public static string RecordCtVarUsage(int varId)
        {
            string cmdStr="?cmd=/ContractTpl/recordCtVarUsage";
            string func = "RecordCtVarUsage";
            StringBuilder strb = new StringBuilder();
            strb.Append("uid=" + WordShare.UserId);
            strb.Append("&varId=" + varId);//变量ID
            strb.Append("&tplid=" + WordShare.TempId);//模板历史ID
           return HttpRequestUtility.SubmitPostRequestCore(strb.ToString(), CreateUrlType.Temp, func, cmdStr);
           

        }



    }
}
