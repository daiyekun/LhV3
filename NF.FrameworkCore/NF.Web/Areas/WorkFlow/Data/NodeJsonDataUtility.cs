using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NF.Common.Utility;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NF.Web.Areas.WorkFlow.Data
{
    /// <summary>
    /// 节点解析
    /// </summary>
    public class NodeJsonDataUtility
    {
        public static FlowNodeDataJson DeserializeToInfo(string jsondata)
        {
            FlowNodeDataJson flowNodeData = new FlowNodeDataJson();
            JObject jobj = JObject.Parse(jsondata);
            flowNodeData.title = jobj.Value<string>("title");
            flowNodeData.initNum = jobj.Value<int>("initnum");
            
            flowNodeData.nodes = GetNodes(jobj);
            flowNodeData.lines = GetLines(jobj);
            flowNodeData.areas = GetAreas(jobj);
            return flowNodeData;
        }
        /// <summary>
        /// 获取节点
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, FlowTempNodeViewDTO> GetNodes(JObject jobj)
        {
           
            Dictionary<string, FlowTempNodeViewDTO> dicnodes = new Dictionary<string, FlowTempNodeViewDTO>();
            IDictionary<string, JToken> rates = (JObject)jobj["nodes"];
            foreach (var jkey in rates.Keys)
            {
                var tnode = rates[jkey];
                var info = JsonUtility.DeserializeObject<FlowTempNodeViewDTO>(tnode.ToString());
                info.strid = jkey;
                dicnodes.Add(jkey, info);
            }
            return dicnodes;

        }
        /// <summary>
        /// 获取线条,Line获取对象和其他不同。因为返回json中没有实体strId
        /// </summary>
        /// <param name="jobj">JSON对象</param>
        /// <returns></returns>
        public static Dictionary<string, TempNodeLineViewDTO> GetLines(JObject jobj)
        {
            try
            {
                Dictionary<string, TempNodeLineViewDTO> diclines = new Dictionary<string, TempNodeLineViewDTO>();
                IDictionary<string, JToken> rates = (JObject)jobj["lines"];
                foreach (var jkey in rates.Keys)
                {
                    var tline = rates[jkey];
                   var info= JsonUtility.DeserializeObject<TempNodeLineViewDTO>(tline.ToString());
                    info.strid = jkey;
                    diclines.Add(jkey, info);
                }
                
              

                return diclines;
            }
            catch (Exception ex)
            {
                return null;
                
            }
        }
        /// <summary>
        /// 获取线条
        /// </summary>
        /// <param name="jobj">JSON对象</param>
        /// <returns></returns>
        public static Dictionary<string, TempNodeAreaViewDTO> GetAreas(JObject jobj)
        {
            

            Dictionary<string, TempNodeAreaViewDTO> dicareas = new Dictionary<string, TempNodeAreaViewDTO>();
            IDictionary<string, JToken> rates = (JObject)jobj["areas"];
            foreach (var jkey in rates.Keys)
            {
                var tnode = rates[jkey];
                var info = JsonUtility.DeserializeObject<TempNodeAreaViewDTO>(tnode.ToString());
                info.strid = jkey;
                dicareas.Add(jkey, info);
            }
            return dicareas;
        }
    }
    
}
