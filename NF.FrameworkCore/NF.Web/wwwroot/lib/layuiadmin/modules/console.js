/**

 @Name：layuiAdmin 主页控制台
 @Author：贤心
 @Site：http://www.layui.com/admin/
 @License：GPL-2
    
 */


layui.define(function(exports){
  
  /*
    下面通过 layui.use 分段加载不同的模块，实现不同区域的同时渲染，从而保证视图的快速呈现
  */
  
  
  //区块轮播切换
  layui.use(['admin', 'carousel'], function(){
    var $ = layui.$
    ,admin = layui.admin
    ,carousel = layui.carousel
    ,element = layui.element
    ,device = layui.device();

    //轮播切换
    $('.layadmin-carousel').each(function(){
      var othis = $(this);
      carousel.render({
        elem: this
        ,width: '100%'
        ,arrow: 'none'
        ,interval: othis.data('interval')
        ,autoplay: othis.data('autoplay') === true
        ,trigger: (device.ios || device.android) ? 'click' : 'hover'
        ,anim: othis.data('anim')
      });
    });
    //提醒条数查询
    admin.req({
        url: '/Home/GetMsgInfo',
        success: function (res) {
            $("#daichuli").html(res.Data.PedingNum);
            $("#beidahui").html(res.Data.BeiDahuiNum);
            $("#dqjhsk").html(res.Data.DqjhskNum);
            $("#dqjhfk").html(res.Data.DqjhfkNum);
            $("#dclsjsk").html(res.Data.DclsjskNum);
            $("#dclsjfk").html(res.Data.DclsjfkNum);
            $("#dclsp").html(res.Data.DclspNum);
            $("#dclkp").html(res.Data.DclkpNum);
            $("#dqskht").html(res.Data.DqSkHtNum);
            $("#dqfkht").html(res.Data.DqFkHtNum);
            $("#yitongguo").html(res.Data.YiTongGuo);
            $("#Mydesc").html(res.Data.Mydesc);
            $("#Gzdesc").html(res.Data.Gzdesc);
            $("#Jdpdtx").html(res.Data.Jdpdtx);
        }
    });
    element.render('progress');

   
    
  });
   

  //数据概览
  layui.use(['carousel', 'echarts', 'element'], function () {
      var $ = layui.$
      , carousel = layui.carousel
      , echarts = layui.echarts
      , admin = layui.admin
      element = layui.element;
   
    var skhtje=[];
    var fkhtje=[];
    var skje=[];
    var fkje = [];
    var htcounts = [];
    admin.req({
        url: '/Statistics/Chart/GetHtQingKuantongji',
        async: false,
        success: function (res) {
            skhtje=res.Data.SkhtJe;
            fkhtje=res.Data.FkHtJe;
            skje=res.Data.SjSkJe;
            fkje=res.Data.SjFkJe;
            htcounts = res.Data.HtCount;
           
        }
    });
      //饼图
    var arrlb=[];
    var piedata=[];
    admin.req({
        url: '/Statistics/Chart/GetHtLbTjPie',
        async: false,
        success: function (res) {
            arrlb = res.Data.HtLbs;
            piedata = res.Data.HtLbJes;
           
        }
    });
    
    var echartsApp = [], options = [
      //执行情况统计
      {
        title: {
          text: '合同执行情况统计图(单位:元)',
          x: 'center',
          textStyle: {
            fontSize: 14
          }
        },
        tooltip : {
          trigger: 'axis'
        },
        legend: {
          data:['','']
        },
        xAxis : [{
          type : 'category',
          boundaryGap : false,
          data: ['1月','2月','3月','4月','5月','6月','7月','8月','9月','10月','11月','12月']
        }],
        yAxis : [{
          type : 'value'
        }],
        series : [{
          name: '收款合同额',
          type:'line',
          smooth:true,
          itemStyle: {normal: {areaStyle: {type: 'default'}}},
          data: skhtje//[111,222,333,444,555,666,3333,33333,55555,66666,33333,3333]
        },{
            name: '付款合同额',
          type:'line',
          smooth:true,
          itemStyle: {normal: {areaStyle: {type: 'default'}}},
          data:fkhtje //[66,22,33,44,55,66,333,3333,5555,12666,3333,333]
        },
        {
            name: '实际收款额',
            type: 'line',
            smooth: true,
            itemStyle: { normal: { areaStyle: { type: 'default' } } },
            data: skje //[44, 22, 80, 44, 55, 66, 333, 3333, 5555, 12666, 3333, 333]
        },
        {
            name: '实际付款额',
            type: 'line',
            smooth: true,
            itemStyle: { normal: { areaStyle: { type: 'default' } } },
            data: fkje //[77, 22, 100, 44, 55, 66, 333, 3333, 5555, 12666, 3333, 333]
        }]
      },
      
      //合同类别分部
      { 
        title : {
          text: '合同类别-金额情况统计图',
          x: 'center',
          textStyle: {
            fontSize: 14
          }
        },
        tooltip : {
          trigger: 'item',
          formatter: "{a} <br/>{b} : {c} ({d}%)"
        },
        legend: {
          orient : 'vertical',
          x : 'left',
          data:arrlb//['Chrome','Firefox','IE 8.0','Safari','其它浏览器']
        },
        series : [{
          name:'合同金额',
          type:'pie',
          radius : '55%',
          center: ['50%', '50%'],
          data: piedata
          //    [
          //  {value:9052, name:'Chrome'},
          //  {value:1610, name:'Firefox'},
          //  {value:3200, name:'IE 8.0'},
          //  {value:535, name:'Safari'},
          //  {value:1700, name:'其它浏览器'}
          //]
        }]
      },
      
      //新增的用户量
      {
        title: {
          text: '年度条数-金额统计图',
          x: 'center',
          textStyle: {
            fontSize: 14
          }
        },
        tooltip : { //提示框
          trigger: 'axis',
          formatter: "{b}<br>合同条数：{c}"
        },
        xAxis : [{ //X轴
          type : 'category',
          data: ['1月', '2月', '3月', '4月', '5月', '6月', '7月', '8月', '9月', '10月', '11月', '12月']
        }],
        yAxis : [{  //Y轴
          type : 'value'
        }],
        series : [{ //内容
          type: 'line',
          data: htcounts//[200, 300, 400, 610, 150, 270, 380],
        }]
      }
    ]
    ,elemDataView = $('#LAY-index-dataview').children('div')
    ,renderDataView = function(index){
      echartsApp[index] = echarts.init(elemDataView[index], layui.echartsTheme);
      echartsApp[index].setOption(options[index]);
      window.onresize = echartsApp[index].resize;
    };
    
    
    //没找到DOM，终止执行
    if(!elemDataView[0]) return;
    
    
    
    renderDataView(0);
    
    //监听数据概览轮播
    var carouselIndex = 0;
    carousel.on('change(LAY-index-dataview)', function(obj){
      renderDataView(carouselIndex = obj.index);
    });
    
    //监听侧边伸缩
    layui.admin.on('side', function(){
      setTimeout(function(){
        renderDataView(carouselIndex);
      }, 300);
    });
    
    //监听路由
    layui.admin.on('hash(tab)', function(){
      layui.router().path.join('') || renderDataView(carouselIndex);
    });
  });
  
    //进度条
  layui.use(['element'], function () {
      var $ = layui.$
      , element = layui.element
      , admin = layui.admin;
      //element.progress('skhtwcbl', '50%');
      admin.req({
          url: '/Home/GetProgressData',
          async: false,
          success: function (res) {
              setTimeout(function () { 
              element.progress('skhtwcbl', res.Data.SkHtWcBl);//res.Data.SkHtWcBl收款合同完成比例
              element.progress('fkhtwcbl', res.Data.FkHtWcBl);//付款合同完成比例
              element.progress('spwcbl', res.Data.SpWcBl);//收票完成比率
              element.progress('kpwcbl', res.Data.KpWcBl);//开票完成比率
              },1000)
              
          }
      });

  });

  //最新订单
  layui.use('table', function(){
    var $ = layui.$
    ,table = layui.table;
    
    //今日热搜
    table.render({
      elem: '#LAY-index-topSearch'
      , url: '/Home/GetContratsZXZ'
      ,page: true
      ,cols: [[
         {type: 'numbers', fixed: 'left'}
        ,{ field: 'Name', title: '合同名称', minWidth: 300 }
        ,{field: 'Code', title: '合同编号', minWidth: 120, sort: true}
        ,{ field: 'HtJeThond', title: '合同金额',minWidth: 120, sort: true }
        ,{ field: 'HtWcBl', title: '完成比例', minWidth: 120, }
        ,{ field: 'FpJeThod', title: '发票金额', minWidth: 120, }
      ]]
      ,skin: 'line'
    });
    
    //今日热贴
    table.render({
      elem: '#LAY-index-topCard'
      , url: '/Home/GetProjects' //模拟接口
      ,page: true
      ,cellMinWidth: 120
      ,cols: [[
         {type: 'numbers', fixed: 'left'}
        ,{field: 'Name', title: '项目名称', minWidth: 260}
        , { field: 'Code', title: '项目编号', minWidth: 130 }
        , { field: 'XmSkHtJeThod', title: '项目收款合同金额', minWidth: 150 }
        , { field: 'XmFkHtJeThod', title: '项目付款合同金额', minWidth: 150 }
        , { field: 'XmSkJeThod', title: '项目收款金额', minWidth: 140 }
        , { field: 'XmFkJeThod', title: '项目付款金额', minWidth: 140 }
      ]]
      ,skin: 'line'
    });
  });
  
  exports('console', {})
});