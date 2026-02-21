using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Utility
{
    /// <summary>
    /// 大小金额装换
    /// </summary>
    public class ConvertCurrency
    {
        /// <summary>
        /// 转换方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string ConvertMoney(double input)
        {
            //if (!WooBase.Utility.PageUtility.I18NIsCN)
            //{
            //    //转到千分位
            //    return string.Format("{0:N}", input);
            //}
            try
            {
                string result = string.Empty;
                string tmpResult;
                var toInput = input.ToString();
                int firtInt = 0;
               // input *= 100;
                if (toInput.IndexOf("0.") != -1)
                {
                    string firtStr = toInput.Substring(0, toInput.IndexOf("."));
                    string secondStr = toInput.Substring(toInput.IndexOf(".") + 1);
                    if (firtStr.Length > 1)
                    {
                        input *= 100;
                    }
                    else
                    {
                        firtInt = 1;
                        input = Convert.ToDouble(firtInt + "." + secondStr) * 100;
                    }
                }
                else
                {
                    input *= 100;
                }

                input = Convert.ToUInt64(Math.Abs(input));
                tmpResult = input.ToString();
                for (int i = 0; i < tmpResult.Length; i++)
                {
                    string tmpChar = tmpResult.Substring(tmpResult.Length - 1 - i, 1);
                    if (i == 0 && tmpChar == "0")
                    {
                        continue;
                    }
                    else if (i == 1 && tmpChar == "0")
                    {
                        continue;
                    }
                    result = Upper(tmpChar) + Unit(i) + result;
                }
                StringBuilder sb = new StringBuilder();
                sb.Append(result.Substring(0, 1));
                for (int m = 1; m < result.Length; m++)
                {
                    if (result.Substring(m, 1) != result.Substring(m - 1, 1))
                    {
                        sb.Append(result.Substring(m, 1));
                    }
                }
                result = sb.ToString();

                if (result.Substring(result.Length - 1, 1) == "零")
                {
                    sb.Replace("零", "元", result.Length - 1, 1);
                }
                else if ((result.Substring(result.Length - 1, 1) == "角" && result.Substring(result.Length - 3, 1) == "零") || (result.Substring(result.Length - 1, 1) == "分" && result.Substring(result.Length - 3, 1) == "零"))
                {
                    sb.Replace("零", "元", result.Length - 3, 1);
                }
                else if (result.Substring(result.Length - 1, 1) == "分" && result.Substring(result.Length - 3, 1) == "角" && result.Substring(result.Length - 5, 1) == "零")
                {
                    sb.Replace("零", "元", result.Length - 5, 1);
                }
                result = sb.ToString();
                result = result.Replace("零仟", "零").Replace("零佰", "零").Replace("零拾", "零");
                while (true)
                {
                    if (result.IndexOf("零零") == -1)
                    {
                        break;
                    }
                    result = result.Replace("零零", "零");
                }
                result = result.Replace("零亿", "亿").Replace("零万", "万").Replace("零元", "元").Replace("亿万", "亿");
                if (result.Substring(result.Length - 1, 1) == "元")
                {
                    return result + "整";
                }
                if (firtInt == 1)
                {
                    string endStr = result.Substring(result.IndexOf("元") + 1);
                    result = "零" + "元" + endStr;
                }
                return result;
            }
            catch
            {
                return "零元整";
            }
        }

        /// <summary>
        /// 小写转换为大写
        /// </summary>
        /// <param name="strBefore"></param>
        /// <returns></returns>
        public string Upper(string strBefore)
        {
            string strAfter = string.Empty;
            switch (strBefore)
            {
                case "0":
                    strAfter = "零";
                    break;
                case "1":
                    strAfter = "壹";
                    break;
                case "2":
                    strAfter = "贰";
                    break;
                case "3":
                    strAfter = "叁";
                    break;
                case "4":
                    strAfter = "肆";
                    break;
                case "5":
                    strAfter = "伍";
                    break;
                case "6":
                    strAfter = "陆";
                    break;
                case "7":
                    strAfter = "柒";
                    break;
                case "8":
                    strAfter = "捌";
                    break;
                case "9":
                    strAfter = "玖";
                    break;
                default:
                    break;
            }
            return strAfter;
        }

        /// <summary>
        /// 得到货币单位
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public string Unit(int i)
        {
            string strUnit = string.Empty;
            switch (i)
            {
                case 0:
                    strUnit = "分";
                    break;
                case 1:
                    strUnit = "角";
                    break;
                default:
                    {
                        i -= 2;
                        if (i / 4 == 0 && i % 4 == 0)
                        {
                            strUnit = "元";
                        }
                        else if (i / 4 == 1 && i % 4 == 0)
                        {
                            strUnit = "万";
                        }
                        else if (i / 4 == 2 && i % 4 == 0)
                        {
                            strUnit = "亿";
                        }
                        else if (i / 4 > 2 && i % 4 == 0)
                        {
                            for (int j = 0; j < i / 4; j++)
                            {
                                strUnit += "万";
                            }
                            strUnit += "亿";
                        }
                        else
                        {
                            strUnit = GetBit(i % 4);
                        }
                    }
                    break;
            }
            return strUnit;
        }

        /// <summary>
        /// 得到位数
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public string GetBit(int i)
        {
            string strBit = string.Empty;
            switch (i)
            {
                case 1:
                    strBit = "拾";
                    break;
                case 2:
                    strBit = "佰";
                    break;
                case 3:
                    strBit = "仟";
                    break;
                default:
                    break;
            }
            return strBit;
        }

    }
}
