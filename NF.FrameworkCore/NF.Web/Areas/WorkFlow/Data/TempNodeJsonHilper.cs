using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NF.Web.Areas.WorkFlow.Data
{
    public class TempNodeJsonHilper
    {
        /// <summary>
        /// 获取测试节点数据
        /// </summary>
        public static FlowNodeDataJson GetTempNodeData()
        {
            var jsondata = new FlowNodeDataJson();

            #region 节点
            //节点
            var dicnodes = new Dictionary<string, FlowTempNodeViewDTO>();
            FlowTempNodeViewDTO node1 = new FlowTempNodeViewDTO();
            node1.strid = "demo_node_1";
            node1.name = "开始";
            node1.left = 42;
            node1.top = 38;
            node1.type = "start round mix";
            node1.width = 26;
            node1.height = 26;
            node1.alt = true;
            dicnodes.Add(node1.strid, node1);

            FlowTempNodeViewDTO node2 = new FlowTempNodeViewDTO();
            node2.strid = "demo_node_2";
            node2.name = "结束";
            node2.left = 797;
            node2.top = 42;
            node2.type = "end round mix";
            node2.width = 26;
            node2.height = 26;
            node2.alt = true;
            dicnodes.Add(node2.strid, node2);

            FlowTempNodeViewDTO node3 = new FlowTempNodeViewDTO();
            node3.strid = "demo_node_3";
            node3.name = "入职申请";
            node3.left = 155;
            node3.top = 39;
            node3.type = "task";
            node3.width = 104;
            node3.height = 26;
            node3.alt = true;
            node3.marked = true;
            dicnodes.Add(node3.strid, node3);

            FlowTempNodeViewDTO node4 = new FlowTempNodeViewDTO();
            node4.strid = "demo_node_4";
            node4.name = "人力审批";
            node4.left = 364;
            node4.top = 42;
            node4.type = "task";
            node4.width = 104;
            node4.height = 26;
            node4.alt = true;
            dicnodes.Add(node4.strid, node4);

            FlowTempNodeViewDTO node5 = new FlowTempNodeViewDTO();
            node5.strid = "demo_node_8";
            node5.name = "工资判断";
            node5.left = 571;
            node5.top = 43;
            node5.type = "node";
            node5.width = 104;
            node5.height = 26;
            node5.alt = true;
            dicnodes.Add(node5.strid, node5);

            FlowTempNodeViewDTO node6 = new FlowTempNodeViewDTO();
            node6.strid = "demo_node_9";
            node6.name = "经理终审";
            node6.left = 559;
            node6.top = 141;
            node6.type = "task";
            node6.width = 104;
            node6.height = 26;
            node6.alt = true;
            dicnodes.Add(node6.strid, node6);
            #endregion

            #region 链接线
            var dicline = new Dictionary<string, TempNodeLineViewDTO>();
            var line1 = new TempNodeLineViewDTO();
            line1.strid = "demo_line_5";
            line1.type = "sl";
            line1.from = "demo_node_3";
            line1.to = "demo_node_4";
            line1.name = "提交申请";
            dicline.Add(line1.strid, line1);

            var line2 = new TempNodeLineViewDTO();
            line2.strid = "demo_line_6";
            line2.type = "sl";
            line2.from = "demo_node_1";
            line2.to = "demo_node_3";
            line2.name = "";
            line2.dash = true;
            dicline.Add(line2.strid, line2);

            var line3 = new TempNodeLineViewDTO();
            line3.strid = "demo_line_7";
            
            line3.type = "tb";
            line3.M = 18.5;
            line3.from = "demo_node_4";
            line3.to = "demo_node_3";
            line3.name = "不通过";
            dicline.Add(line3.strid, line3);

            var line4 = new TempNodeLineViewDTO();
            line4.strid = "demo_line_10";
            line4.type = "sl";
            line4.from = "demo_node_4";
            line4.to = "demo_node_8";
            line4.name = "通过";
            line4.dash = true;
            dicline.Add(line4.strid, line4);

            var line6 = new TempNodeLineViewDTO();
            line6.strid = "demo_line_11";
            line6.type = "tb";
            line6.M = 157;
            line6.from = "demo_node_9";
            line6.to = "demo_node_4";
            line6.name = "不接受";
            line6.dash = true;
           
            dicline.Add(line6.strid, line6);

            var line7 = new TempNodeLineViewDTO();
            line7.strid = "demo_line_12";
            line7.type = "sl";
            line7.from = "demo_node_8";
            line7.to = "demo_node_9";
            line7.name = "大于8000";
            dicline.Add(line7.strid, line7);

            var line8 = new TempNodeLineViewDTO();
            line8.strid = "demo_line_13";
            line8.type = "sl";
            line8.from = "demo_node_8";
            line8.to = "demo_node_2";
            line8.name = "小于8000";
            dicline.Add(line8.strid, line8);

            var line9 = new TempNodeLineViewDTO();
            line9.marked = true;
            line9.strid = "demo_line_14";
            line9.type = "sl";
            line9.from = "demo_node_9";
            line9.to = "demo_node_2";
            line9.name = "接受";
            dicline.Add(line9.strid, line9);

            #endregion

            #region 区域

            var dicarea = new Dictionary<string, TempNodeAreaViewDTO>();
            var area1 = new TempNodeAreaViewDTO();
            area1.strid = "1497581247380";
            area1.name = "审议会";
            area1.left = 451;
            area1.top = 110;
            area1.color = "red";
            area1.width = 226;
            area1.height = 108;
            area1.alt = true;
            dicarea.Add(area1.strid, area1);
            #endregion


            #region 赋值
            
            jsondata.lines = dicline;
            jsondata.nodes = dicnodes;
            jsondata.areas = dicarea;
            jsondata.initNum = 16;
            jsondata.title = "测试流程";
            #endregion

            return jsondata;


        }
    }
}
