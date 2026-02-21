/**

 @Name：layuiAdmin iframe版全局配置
 @Author：贤心
 @Site：http://www.layui.com/admin/
 @License：LPPL（layui付费产品协议）
    
 */
 
layui.define(['laytpl', 'layer', 'element', 'util'], function(exports){
    exports('setter', {
        container: 'LAY_app' //容器ID
      ,base: layui.cache.base //记录静态资源所在路径
      ,views: layui.cache.base + 'tpl/' //动态模板所在目录
      ,entry: 'index' //默认视图文件名
      ,engine: '.html' //视图文件后缀名
      ,pageTabs: true //是否开启页面选项卡功能。iframe版推荐开启
    
      ,name: 'layuiAdmin'
      ,tableName: 'layuiAdmin' //本地存储表名
      ,MOD_NAME: 'admin' //模块事件名
    
      ,debug: true //是否开启调试模式。如开启，接口异常时会抛出异常 URL 等信息

        //自定义请求字段
      ,request: {
          tokenName: false //自动携带 token 的字段名（如：access_token）。可设置 false 不携带。
      }
    
        //自定义响应字段
      ,response: {
          statusName: 'Code' //数据状态的字段名称
        ,statusCode: {
            ok: 0 //数据状态一切正常的状态码
          ,logout: 1001 //登录状态失效的状态码
        }
        ,msgName: 'Msg' //状态信息的字段名称
        ,dataName: 'Data' //数据详情的字段名称
      },
        NFData: {
            userId: 'userId',
            userName: 'userName',
            disName: 'disName',
            detpId: 'detpId',
            sysTitle: '合同管理系统',
            sysGs: '罗合V6.3'
            , dlname: '登录合同管理系统'
            , superadmin:'SuperAdministrator'//超级管理员登录名称

        },
        Addin:{
            wordAddInVer: 'VSTO272.STD.20170905',//插件版本
        },
        //自己自定义-升级需注意
        table: {
            height: 'full-95',//有头部
            limit: 20,//显示条数    
            height_1: 'full-180',
            height_2: 'full-130',
            height_3: 'full-30',//使用表格的bar
            height_4: 'full-40',//使用表格的bar
            height_tab: 350,//标签使用
            limit_tab: 100,//显示条数-标签使用
            limits:[10, 15, 20, 25, 50,100, 200,500,1000]
        },
        layupload: {//上传组件配置
            size: '5368709120'
            , accept: 'file'
            , exts: 'txt|doc|jpg|gif|png|rar|zip|docx|pdf|xls|xlsx|jpeg|mdi|tif|dwg|psd|3ds|eps|vsd|TXT|DOC|JPG|JPEG|MDI|TIF|GIF|PNG|RAR|ZIP|DOCX|PDF|XLS|XLSX|DWG|PSD|3DS|EPS|VSD'
            , uploadIp: 'http://localhost:5188'
        },
        sysinfo: {//系统信息
            seversion: 'SE',
            lhvs: 'SE',//SE审批,Sing
            Mb:'Mb',//Mb 模板起草

        },
        sysWf:{//流程
            flowType: {//审批类型
                Kh: 0,//客户
                Gys: 1,//供应商
                Qtdf: 2,//其他对方
                Hetong: 3,//合同
                ShouPiao: 4,//收票
                KaiPiao: 5,//开票
                Fukuan: 6,//付款
                Xm: 7,//项目
                Zb:8
            }
        }
      
    
    //扩展的第三方模块
    ,extend: [
      'echarts', //echarts 核心包
      'echartsTheme' //echarts 主题
    ]
    
    //主题配置
    ,theme: {
      //内置主题配色方案
      color: [{
        main: '#20222A' //主题色
        ,selected: '#009688' //选中色
        ,alias: 'default' //默认别名
      },{
        main: '#03152A'
        ,selected: '#3B91FF'
        ,alias: 'dark-blue' //藏蓝
      },{
        main: '#2E241B'
        ,selected: '#A48566'
        ,alias: 'coffee' //咖啡
      },{
        main: '#50314F'
        ,selected: '#7A4D7B'
        ,alias: 'purple-red' //紫红
      },{
        main: '#344058'
        ,logo: '#1E9FFF'
        ,selected: '#1E9FFF'
        ,alias: 'ocean' //海洋
      },{
        main: '#3A3D49'
        ,logo: '#2F9688'
        ,selected: '#5FB878'
        ,alias: 'green' //墨绿
      },{
        main: '#20222A'
        ,logo: '#F78400'
        ,selected: '#F78400'
        ,alias: 'red' //橙色
      },{
        main: '#28333E'
        ,logo: '#AA3130'
        ,selected: '#AA3130'
        ,alias: 'fashion-red' //时尚红
      },{
        main: '#24262F'
        ,logo: '#3A3D49'
        ,selected: '#009688'
        ,alias: 'classic-black' //经典黑
      },{
        logo: '#226A62'
        ,header: '#2F9688'
        ,alias: 'green-header' //墨绿头
      },{
        main: '#344058'
        ,logo: '#0085E8'
        ,selected: '#1E9FFF'
        ,header: '#1E9FFF'
        ,alias: 'ocean-header' //海洋头
      },{
        header: '#393D49'
        ,alias: 'classic-black-header' //经典黑头
      }]
      
      //初始的颜色索引，对应上面的配色方案数组索引
      //如果本地已经有主题色记录，则以本地记录为优先，除非请求本地数据（localStorage）
      ,initColorIndex: 0
    }
  });
});
