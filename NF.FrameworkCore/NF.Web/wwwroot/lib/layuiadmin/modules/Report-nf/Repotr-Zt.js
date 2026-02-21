

/**
*收款合同查看
*/
layui.define(['form'], function (exports) {
    var $ = layui.$

        , form = layui.form;
    
  
    var Wzx = 0;
    var Ywz = 0;
    var Yzz = 0;
    var Zxz = 0;
    var Sptg = 0;
    var date = new Date();

    date.getYear(); //获取当前年份(2位)

    var tt = date.getFullYear(); //获取完整的年份(4位)
    //Bbpj()
    //Hxt()
    //Zxtjt()
    //Zxtjtwer()
    Fpdetail()
    Fpdetail1()
    Htlbjt()
    HtZjFpTj()
    HtqdYsSsjeTj()
    HtLyTj()//合同来源统计
    ///发票内容
    function Fpdetail() {

        var $urls = "/ReportForms/ReportForms/TjNewtimezt";
        $.ajax({
            type: 'Get',
            url: $urls,
            data:
            {
            },
          //  dataType: 'json',

            success: function (data) {
                Wzx = data.wzx;
                Ywz = data.ywz;
                Yzz = data.yzz;
                Zxz = data.zxz;
                Sptg = data.sptg;
                var dom = document.getElementById("container");
                var myChart = echarts.init(dom);
                var app = {};
                var option;
                option = {
                    tooltip: {
                        trigger: 'item'
                    },
                    legend: {
                        top: '5%',
                        left: 'center'
                    },
                    series: [
                        {
                            name: '合同状态统计',
                            type: 'pie',
                            radius: ['40%', '70%'],
                            avoidLabelOverlap: false,
                            itemStyle: {
                                borderRadius: 10,
                                borderColor: '#fff',
                                borderWidth: 2
                            },
                            label: {
                                show: false,
                                position: 'center'
                            },
                            emphasis: {
                                label: {
                                    show: true,
                                    fontSize: '40',
                                    fontWeight: 'bold'
                                }
                            },
                            labelLine: {
                                show: false
                            },
                            data: [
                                { value: Wzx, name: '未执行' },
                                { value: Ywz, name: '执行中' },
                                { value: Yzz, name: '审批通过' },
                                { value: Zxz, name: '已完成' },
                                { value: Sptg, name: '已终止' }
                            ]
                        }
                    ]
                };

                if (option && typeof option === 'object') {
                    myChart.setOption(option);
                }
            }
        });
    }
    ///发票内容
    function Fpdetail1() {

        var $urls = "/ReportForms/ReportForms/TjNewHt";
        $.ajax({
            type: 'Get',
            url: $urls,
            data:
            {
            },
            //  dataType: 'json',

            success: function (data) {
                var dom = document.getElementById("containers");
                var myChart = echarts.init(dom);
                var app = {};
                var option;
                option = {
                    tooltip: {
                        trigger: 'axis',
                        axisPointer: {
                            type: 'cross',
                            crossStyle: {
                                color: '#999'
                            }
                        }
                    },
                    //toolbox: {
                    //    feature: {
                    //        dataView: { show: true, readOnly: false },
                    //        magicType: { show: true, type: ['line', 'bar'] },
                    //        restore: { show: true },
                    //        saveAsImage: { show: true }
                    //    }
                    //},
                    legend: {
                        data: ['收款合同金额', '付款合同金额', '收款合同份数', '付款合同份数']
                    },
                    xAxis: [
                        {
                            type: 'category',
                            data: ['一月', '二月', '三月', '四月', '五月', '六月', '七月', '八月', '九月', '十一月', '十二月'],
                            axisPointer: {
                                type: 'shadow'
                            }
                        }
                    ],
                    yAxis: [
                        {
                            type: 'value',
                            name: '单位：万',
                            min: 0,
                            max: 250,
                            interval: 50,
                            axisLabel: {
                                formatter: '{value} '
                            }
                        },
                        {
                            type: 'value',
                            name: '单位：份',
                            min: 0,
                            max: 100,
                            interval: 5,
                            axisLabel: {
                                formatter: '{value}'
                            }
                        }
                    ],
                    series: [
                        {
                            name: '收款合同金额',
                            type: 'bar',
                            data: [
                                data.yiysk, data.erysk, data.sanysk, data.siysk, data.wuysk, data.liuysk, data.qiysk, data.baysk, data.jiuysk, data.shiysk, data.shiYiysk, data.shiErysk
                            ]
                        },
                        {
                            name: '付款合同金额',
                            type: 'bar',
                            data: [
                                data.yiyfk, data.eryfk, data.sanyfk, data.siyfk, data.wuyfk, data.liuyfk, data.qiyfk, data.bayfk, data.siuyfk, data.shiyfk, data.shiYiyfk, data.shiEryfk
                            ]
                        },
                        {
                            name: '收款合同份数',
                            type: 'line',
                            yAxisIndex: 1,
                            data: [data.yiyskfs, data.eryskfs, data.sanyskfs, data.siyskfs, data.wuyskfs, data.liuyskfs, data.qiyskfs, data.bayskfs, data.jiuyskfs, data.shiyskfs, data.shiYiyskfs, data.shiEryskfs]
                        },
                        {
                            name: '付款合同份数',
                            type: 'line',
                            yAxisIndex: 1,
                            data: [data.yiyfkfs, data.eryfkfs, data.sanyfkfs, data.siyfkfs, data.wuyfkfs, data.liuyfkfs, data.qiyfkfs, data.bayfkfs, data.jiuyfkfs, data.shiyfkfs, data.shiYiyfkfs, data.shiEryfkfs]
                        }
                    ]
                };


                if (option && typeof option === 'object') {
                    myChart.setOption(option);
                }
            }
        });
    }
    function Htlbjt() {
        var $urls = "/ReportForms/ReportForms/Htlbjt";
        $.ajax({
            type: 'Get',
            url: $urls,
            data:
            {
            },
            //  dataType: 'json',

            success: function (data) {
                debugger;
                var dom = document.getElementById("Htlbjt");
                var myChart = echarts.init(dom);
                var app = {};

                var option;



                option = {
                    tooltip: {
                        trigger: 'item',
                        formatter: '{a} <br/>{b}: {c} ({d}%)'
                    },
                    legend: {
                        data: [
                            '收-重点工程类',
                            '收-物资供应类',
                            '收-电子信息类' ,
                            '付-重点工程类',
                            '付-物资供应类',
                            '付-电子信息类',
                           
                          
                        ]
                    },
                    series: [
                        {
                            name: '收款合同类别统计',
                            type: 'pie',
                            selectedMode: 'single',
                            radius: [0, '30%'],
                            label: {
                                position: 'inner',
                                fontSize: 14
                            },
                            labelLine: {
                                show: false
                            },
                            data: [
                                { value: data.skzdgc, name: '收-重点工程类' },
                                { value: data.skwzgy, name: '收-物资供应类' },
                                { value: data.skdzxx, name: '收-电子信息类', selected: true }
                            ]
                        },
                        {
                            name: '付款合同类别统计',
                            type: 'pie',
                            radius: ['45%', '60%'],
                            labelLine: {
                                length: 30
                            },
                            label: {
                                formatter: '{a|{a}}{abg|}\n{hr|}\n  {b|{b}：}{c}  {per|{d}%}  ',
                                backgroundColor: '#F6F8FC',
                                borderColor: '#8C8D8E',
                                borderWidth: 1,
                                borderRadius: 4,
                                rich: {
                                    a: {
                                        color: '#6E7079',
                                        lineHeight: 22,
                                        align: 'center'
                                    },
                                    hr: {
                                        borderColor: '#8C8D8E',
                                        width: '100%',
                                        borderWidth: 1,
                                        height: 0
                                    },
                                    b: {
                                        color: '#4C5058',
                                        fontSize: 14,
                                        fontWeight: 'bold',
                                        lineHeight: 33
                                    },
                                    per: {
                                        color: '#fff',
                                        backgroundColor: '#4C5058',
                                        padding: [3, 4],
                                        borderRadius: 4
                                    }
                                }
                            },
                            data: [
                                { value: data.fkzdgc, name: '付-重点工程类' },
                                { value: data.fkwzgy, name: '付-物资供应类' },
                                { value: data.fkdzxx, name: '付-电子信息类' },
                                
                              
                               
                              
                               
                            ]
                        }
                    ]
                };

                if (option && typeof option === 'object') {
                    myChart.setOption(option);
                }
            }
        });
    }
    ///合同及资金往来
    function HtZjFpTj(tt) {

        var $urls = "/ReportForms/ReportForms/Htwlzjtj";
        $.ajax({
            type: 'Get',
            url: $urls,
            data:
            {
                time: tt
            },
            //  dataType: 'json',

            success: function (data) {
              
                var dom = document.getElementById("HtZjFpTj");
                var myChart = echarts.init(dom);
                var app = {};
                var option;
                var dataMap = {};
              
                function dataFormatter(obj) {
                
                    // prettier-ignore
                  // data: ['一月', '二月', '三月', '四月', '五月', '六月', '七月', '八月', '九月', '十一月', '十二月'],
                    var pList = ['一月', '二月', '三月', '四月', '五月', '六月', '七月', '八月', '九月', '十一月', '十二月'];
                    var temp;
                    for (var year = data.sknf[0]; year <= data.sknf[4]; year++) {
                        
                        var max = 0;
                        var sum = 0;
                        temp = obj[year];
                        for (var i = 0, l = temp.length; i < l; i++) {
                            max = Math.max(max, temp[i]);
                            sum += temp[i];
                            obj[year][i] = {
                                name: pList[i],
                                value: temp[i]
                            };
                        }
                        obj[year + 'max'] = Math.floor(max / 100) * 100;
                        obj[year + 'sum'] = sum;
                    }
                    return obj;
                }
                //prettier-ignore
                //实际付款
               // dataMap.dataGDP = dataFormatter(test1);



                // prettier-ignore
                //实际付款
                dataMap.dataGDP = dataFormatter({
                    //max : 60000,
                    [data.sknf[4]]: [data.sjfk1[4], data.sjfk2[4], data.sjfk3[4], data.sjfk4[4], data.sjfk5[4], data.sjfk6[4], data.sjfk7[4], data.sjfk8[4], data.sjfk9[4], data.sjfk10[4], data.sjfk11[4], data.sjfk12[4]],
                    [data.sknf[3]]: [data.sjfk1[3], data.sjfk2[3], data.sjfk3[3], data.sjfk4[3], data.sjfk5[3], data.sjfk6[3], data.sjfk7[3], data.sjfk8[3], data.sjfk9[3], data.sjfk10[3], data.sjfk11[3], data.sjfk12[3]],
                    [data.sknf[2]]: [data.sjfk1[2], data.sjfk2[2], data.sjfk3[2], data.sjfk4[2], data.sjfk5[2], data.sjfk6[2], data.sjfk7[2], data.sjfk8[2], data.sjfk9[2], data.sjfk10[2], data.sjfk11[2], data.sjfk12[2]],
                    [data.sknf[1]]: [data.sjfk1[1], data.sjfk2[1], data.sjfk3[1], data.sjfk4[1], data.sjfk5[1], data.sjfk6[1], data.sjfk7[1], data.sjfk8[1], data.sjfk9[1], data.sjfk10[1], data.sjfk11[1], data.sjfk12[1]],
                    [data.sknf[0]]: [data.sjfk1[0], data.sjfk2[0], data.sjfk3[0], data.sjfk4[0], data.sjfk5[0], data.sjfk6[0], data.sjfk7[0], data.sjfk8[0], data.sjfk9[0], data.sjfk10[0], data.sjfk11[0], data.sjfk12[0]]
                });
                // prettier-ignore
                //第一产业
                //收款合同
                dataMap.dataPI = dataFormatter(
                    {
                    //max : 4000,
                        [data.sknf[4]]: [data.sk1y[4], data.sk2y[4], data.sk3y[4], data.sk4y[4], data.sk5y[4], data.sk6y[4], data.sk7y[4], data.sk8y[4], data.sk9y[4], data.sk10y[4], data.sk11y[4], data.sk12y[4]],
                        [data.sknf[3]]: [data.sk1y[3], data.sk2y[3], data.sk3y[3], data.sk4y[3], data.sk5y[3], data.sk6y[3], data.sk7y[3], data.sk8y[3], data.sk9y[3], data.sk10y[3], data.sk11y[3], data.sk12y[3]],
                        [data.sknf[2]]: [data.sk1y[2], data.sk2y[2], data.sk3y[2], data.sk4y[2], data.sk5y[2], data.sk6y[2], data.sk7y[2], data.sk8y[2], data.sk9y[2], data.sk10y[2], data.sk11y[2], data.sk12y[2]],
                        [data.sknf[1]]: [data.sk1y[1], data.sk2y[1], data.sk3y[1], data.sk4y[1], data.sk5y[1], data.sk6y[1], data.sk7y[1], data.sk8y[1], data.sk9y[1], data.sk10y[1], data.sk11y[1], data.sk12y[1]],
                        [data.sknf[0]]: [data.sk1y[0], data.sk2y[0], data.sk3y[0], data.sk4y[0], data.sk5y[0], data.sk6y[0], data.sk7y[0], data.sk8y[0], data.sk9y[0], data.sk10y[0], data.sk11y[0], data.sk12y[0]]
                     });
                // //付款合同 prettier-ignore 
                dataMap.dataSI = dataFormatter({
                    //max : 26600,
                    [data.sknf[4]]: [data.fk1y[4], data.fk2y[4], data.fk3y[4], data.fk4y[4], data.fk5y[4], data.fk6y[4], data.fk7y[4], data.sk8y[4], data.fk9y[4], data.fk10y[4], data.fk11y[4], data.fk12y[4]],
                    [data.sknf[3]]: [data.fk1y[3], data.fk2y[3], data.fk3y[3], data.fk4y[3], data.fk5y[3], data.fk6y[3], data.fk7y[3], data.sk8y[3], data.fk9y[3], data.fk10y[3], data.fk11y[3], data.fk12y[3]],
                    [data.sknf[2]]: [data.fk1y[2], data.fk2y[2], data.fk3y[2], data.fk4y[2], data.fk5y[2], data.fk6y[2], data.fk7y[2], data.sk8y[2], data.fk9y[2], data.fk10y[2], data.fk11y[2], data.fk12y[2]],
                    [data.sknf[1]]: [data.fk1y[1], data.fk2y[1], data.fk3y[1], data.fk4y[1], data.fk5y[1], data.fk6y[1], data.fk7y[1], data.sk8y[1], data.fk9y[1], data.fk10y[1], data.fk11y[1], data.fk12y[1]],
                    [data.sknf[0]]: [data.fk1y[0], data.fk2y[0], data.fk3y[0], data.fk4y[0], data.fk5y[0], data.fk6y[0], data.fk7y[0], data.sk8y[0], data.fk9y[0], data.fk10y[0], data.fk11y[0], data.fk12y[0]]
                    });
                // prettier-ignore
                //实际收款
                dataMap.dataTI = dataFormatter({
                    //max : 25000,
                    [data.sknf[4]]: [data.sjsk1[4], data.sjsk2[4], data.sjsk3[4], data.sjsk4[4], data.sjsk5[4], data.sjsk6[4], data.sjsk7[4], data.sjsk8[4], data.sjsk9[4], data.sjsk10[4], data.sjsk11[4], data.sjsk12[4]],
                    [data.sknf[3]]: [data.sjsk1[3], data.sjsk2[3], data.sjsk3[3], data.sjsk4[3], data.sjsk5[3], data.sjsk6[3], data.sjsk7[3], data.sjsk8[3], data.sjsk9[3], data.sjsk10[3], data.sjsk11[3], data.sjsk12[3]],
                    [data.sknf[2]]: [data.sjsk1[2], data.sjsk2[2], data.sjsk3[2], data.sjsk4[2], data.sjsk5[2], data.sjsk6[2], data.sjsk7[2], data.sjsk8[2], data.sjsk9[2], data.sjsk10[2], data.sjsk11[2], data.sjsk12[2]],
                    [data.sknf[1]]: [data.sjsk1[1], data.sjsk2[1], data.sjsk3[1], data.sjsk4[1], data.sjsk5[1], data.sjsk6[1], data.sjsk7[1], data.sjsk8[1], data.sjsk9[1], data.sjsk10[1], data.sjsk11[1], data.sjsk12[1]],
                    [data.sknf[0]]: [data.sjsk1[0], data.sjsk2[0], data.sjsk3[0], data.sjsk4[0], data.sjsk5[0], data.sjsk6[0], data.sjsk7[0], data.sjsk8[0], data.sjsk9[0], data.sjsk10[0], data.sjsk11[0], data.sjsk12[0]]
                   });
                // prettier-ignore
                //房地产
                //收票
                dataMap.dataEstate = dataFormatter({
                    //max : 3600,
                   [data.sknf[4]]: [data.kp1[4], data.kp2[4], data.kp3[4], data.kp4[4], data.kp5[4], data.kp6[4], data.kp7[4], data.kp8[4], data.kp9[4], data.kp10[4], data.kp11[4], data.kp12[4]],
                   [data.sknf[3]]: [data.kp1[3], data.kp2[3], data.kp3[3], data.kp4[3], data.kp5[3], data.kp6[3], data.kp7[3], data.kp8[3], data.kp9[3], data.kp10[3], data.kp11[3], data.kp12[3]],
                   [data.sknf[2]]: [data.kp1[2], data.kp2[2], data.kp3[2], data.kp4[2], data.kp5[2], data.kp6[2], data.kp7[2], data.kp8[2], data.kp9[2], data.kp10[2], data.kp11[2], data.kp12[2]],
                   [data.sknf[1]]: [data.kp1[1], data.kp2[1], data.kp3[1], data.kp4[1], data.kp5[1], data.kp6[1], data.kp7[1], data.kp8[1], data.kp9[1], data.kp10[1], data.kp11[1], data.kp12[1]],
                   [data.sknf[0]]: [data.kp1[0], data.kp2[0], data.kp3[0], data.kp4[0], data.kp5[0], data.kp6[0], data.kp7[0], data.kp8[0], data.kp9[0], data.kp10[0], data.kp11[0], data.kp12[0]]
                    });
                // prettier-ignore
                //开票
                dataMap.dataFinancial = dataFormatter({
                    //max : 3200,
                    [data.sknf[4]]: [data.sp1[4], data.sp2[4], data.sp3[4], data.sp4[4], data.sp5[4], data.sp6[4], data.sp7[4], data.sp8[4], data.sp9[4], data.sp10[4], data.sp11[4], data.sp12[4]],
                    [data.sknf[3]]: [data.sp1[3], data.sp2[3], data.sp3[3], data.sp4[3], data.sp5[3], data.sp6[3], data.sp7[3], data.sp8[3], data.sp9[3], data.sp10[3], data.sp11[3], data.sp12[3]],
                    [data.sknf[2]]: [data.sp1[2], data.sp2[2], data.sp3[2], data.sp4[2], data.sp5[2], data.sp6[2], data.sp7[2], data.sp8[2], data.sp9[2], data.sp10[2], data.sp11[2], data.sp12[2]],
                    [data.sknf[1]]: [data.sp1[1], data.sp2[1], data.sp3[1], data.sp4[1], data.sp5[1], data.sp6[1], data.sp7[1], data.sp8[1], data.sp9[1], data.sp10[1], data.sp11[1], data.sp12[1]],
                    [data.sknf[0]]: [data.sp1[0], data.sp2[0], data.sp3[0], data.sp4[0], data.sp5[0], data.sp6[0], data.sp7[0], data.sp8[0], data.sp9[0], data.sp10[0], data.sp11[0], data.sp12[0]]
                    });
                option = {
                    baseOption: {
                        timeline: {
                            axisType: 'category',
                            autoPlay: true,
                            playInterval: 1000,
                            data: [
                                data.sknf[0] + '-01-01',
                                data.sknf[1] + '-01-01',
                                data.sknf[2] + '-01-01',
                                data.sknf[3] + '-01-01',
                                data.sknf[4] + '-01-01',
                                //'2017-01-01',
                                //'2018-01-01',
                                //'2019-01-01',
                                //'2020-01-01',
                                //'2021-01-01',
                            ],
                            label: {
                                formatter: function (s) {
                                    return new Date(s).getFullYear();
                                }
                            }
                        },
                        //title: {
                        //    subtext: '数据来自国家统计局'
                        //},
                        tooltip: {},
                        legend: {
                            left: 'right',
                                  // 第一产业   第二产业    第三产业    GDP        金融     房地产
                            data: ['收款合同', '付款合同', '实际收款', '实际付款', '开票', '收票'],
                            selected: {
                                实际付款: false,//GDP
                                开票: false,//金融
                                收票: false //房地产
                            }
                        },
                        calculable: true,
                        grid: {
                            top: 80,
                            bottom: 100,
                            tooltip: {
                                trigger: 'axis',
                                axisPointer: {
                                    type: 'shadow',
                                    label: {
                                        show: true,
                                        formatter: function (params) {
                                            return params.value.replace('\n', '');
                                        }
                                    }
                                }
                            }
                        },
                        xAxis: [
                            {
                                type: 'category',
                                axisLabel: { interval: 0 },
                                data: ['一月', '二月', '三月', '四月', '五月', '六月', '七月', '八月', '九月', '十一月', '十二月'],
                                splitLine: { show: false }
                            }
                        ],
                        yAxis: [
                            {
                                type: 'value',
                                name: '元'
                            }
                        ],
                        series: [
                            { name: '实际付款', type: 'bar' },
                            { name: '开票', type: 'bar' },
                            { name: '收票', type: 'bar' },
                            { name: '收款合同', type: 'bar' },
                            { name: '付款合同', type: 'bar' },
                            { name: '实际收款', type: 'bar' },
                            {
                                name: '资金占比',
                                type: 'pie',
                                center: ['75%', '35%'],
                                radius: '28%',
                                z: 100
                            }
                        ]
                    },
                    options: [
                        {
                          //  title: { text: '2017全国宏观经济指标' },
                            series: [
                                { data: dataMap.dataGDP[data.sknf[0]]},
                                { data: dataMap.dataFinancial[data.sknf[0]]},
                                { data: dataMap.dataEstate[data.sknf[0]]},
                                { data: dataMap.dataPI[data.sknf[0]]},
                                { data: dataMap.dataSI[data.sknf[0]]},
                                { data: dataMap.dataTI[data.sknf[0]] },
                                {
                                    data: [
                                        { name: '收款合同', value: dataMap.dataPI[data.sknf[0] + 'sum']},
                                        { name: '付款合同', value: dataMap.dataSI[data.sknf[0] + 'sum']},
                                        { name: '实际收款', value: dataMap.dataTI[data.sknf[0] + 'sum']}
                                    ]
                                }
                            ]
                        },
                        {
                           // title: { text: '2018全国宏观经济指标' },
                            series: [
                                { data: dataMap.dataGDP[data.sknf[1]]},
                                { data: dataMap.dataFinancial[data.sknf[1]]},
                                { data: dataMap.dataEstate[data.sknf[1]]},
                                { data: dataMap.dataPI[data.sknf[1]]},
                                { data: dataMap.dataSI[data.sknf[1]]},
                                { data: dataMap.dataTI[data.sknf[1]] },
                                {
                                    data: [
                                        { name: '收款合同', value: dataMap.dataPI[data.sknf[1] + 'sum']},
                                        { name: '付款合同', value: dataMap.dataSI[data.sknf[1] + 'sum']},
                                        { name: '实际收款', value: dataMap.dataTI[data.sknf[1] + 'sum']}
                                    ]
                                }
                            ]
                        },
                        {
                           // title: { text: '2019全国宏观经济指标' },
                            series: [
                                { data: dataMap.dataGDP[data.sknf[2]]},
                                { data: dataMap.dataFinancial[data.sknf[2]]},
                                { data: dataMap.dataEstate[data.sknf[2]]},
                                { data: dataMap.dataPI[data.sknf[2]]},
                                { data: dataMap.dataSI[data.sknf[2]]},
                                { data: dataMap.dataTI[data.sknf[2]] },
                                {
                                    data: [
                                        { name: '收款合同', value: dataMap.dataPI[data.sknf[2] + 'sum'] },
                                        { name: '付款合同', value: dataMap.dataSI[data.sknf[2] + 'sum'] },
                                        { name: '实际收款', value: dataMap.dataTI[data.sknf[2] + 'sum'] }
                                    ]
                                }
                            ]
                        },
                        {
                           // title: { text: '2020全国宏观经济指标' },
                            series: [
                                { data: dataMap.dataGDP[data.sknf[3]]},
                                { data: dataMap.dataFinancial[data.sknf[3]]},
                                { data: dataMap.dataEstate[data.sknf[3]]},
                                { data: dataMap.dataPI[data.sknf[3]]},
                                { data: dataMap.dataSI[data.sknf[3]]},
                                { data: dataMap.dataTI[data.sknf[3]] },
                                {
                                    data: [
                                        { name: '收款合同', value: dataMap.dataPI[data.sknf[3] + 'sum']},
                                        { name: '付款合同', value: dataMap.dataSI[data.sknf[3] + 'sum']},
                                        { name: '实际收款', value: dataMap.dataTI[data.sknf[3] + 'sum'] }
                                    ]
                                }
                            ]
                        },
                        {
                           // title: { text: '2021全国宏观经济指标' },
                            series: [
                                { data: dataMap.dataGDP[data.sknf[4]]},
                                { data: dataMap.dataFinancial[data.sknf[4]]},
                                { data: dataMap.dataEstate[data.sknf[4]]},
                                { data: dataMap.dataPI[data.sknf[4]]},
                                { data: dataMap.dataSI[data.sknf[4]]},
                                { data: dataMap.dataTI[data.sknf[4]]},
                                {
                                    data: [
                                        { name: '收款合同', value: dataMap.dataPI[data.sknf[4] + 'sum']},
                                        { name: '付款合同', value: dataMap.dataSI[data.sknf[4] + 'sum']},
                                        { name: '实际收款', value: dataMap.dataTI[data.sknf[4] + 'sum']}
                                    ]
                                }
                            ]
                        }
                    ]
                };
                if (option && typeof option === 'object') {
                    myChart.setOption(option);
                }
            }
        });
    }
    ///合同签订、应收、实收金额统计
    function HtqdYsSsjeTj(value) {

        var $urls = "/ReportForms/ReportForms/HtjeTj";
        $.ajax({
            type: 'Get',
            url: $urls,
            data:
            {
                time: value
            },
            //  dataType: 'json',
            success: function (data) {
                var dom = document.getElementById("HtqdYsSsjeTj");
                var myChart = echarts.init(dom);
                var app = {};
                var option;
                setTimeout(function () {
                    option = {
                        legend: {},
                        tooltip: {
                            trigger: 'axis',
                            showContent: false
                        },
                        dataset: {
                            source: [
                                ['product', data.time[0], data.time[1], data.time[2], data.time[3], data.time[4]],
                                ['签约金额', data.qyje[0], data.qyje[1], data.qyje[2], data.qyje[3], data.qyje[4]],
                                ['应收金额', data.ysje[0], data.ysje[1], data.ysje[2], data.ysje[3], data.ysje[4]],
                                ['实收金额', data.ssje[0], data.ssje[1], data.ssje[2], data.ssje[3], data.ssje[4]],
                              // ['Walnut Brownie', 25.2, 37.1, 41.2, 18, 33.9, 49.1]
                            ]
                        },
                        xAxis: { type: 'category' },
                        yAxis: { gridIndex: 0 },
                        grid: { top: '55%' },
                        series: [
                            {
                                type: 'line',
                                smooth: true,
                                seriesLayoutBy: 'row',
                                emphasis: { focus: 'series' }
                            },
                            {
                                type: 'line',
                                smooth: true,
                                seriesLayoutBy: 'row',
                                emphasis: { focus: 'series' }
                            },
                            {
                                type: 'line',
                                smooth: true,
                                seriesLayoutBy: 'row',
                                emphasis: { focus: 'series' }
                            },
                            {
                                type: 'line',
                                smooth: true,
                                seriesLayoutBy: 'row',
                                emphasis: { focus: 'series' }
                            },
                            {
                                type: 'pie',
                                id: 'pie',
                                radius: '30%',
                                center: ['50%', '25%'],
                                emphasis: {
                                    focus: 'self'
                                },
                                label: {
                                    formatter: '{b}: {@2012} ({d}%)'
                                },
                                encode: {
                                    itemName: 'product',
                                    value: '2012',
                                    tooltip: '2012'
                                }
                            }
                        ]
                    };
                    myChart.on('updateAxisPointer', function (event) {
                        const xAxisInfo = event.axesInfo[0];
                        if (xAxisInfo) {
                            const dimension = xAxisInfo.value + 1;
                            myChart.setOption({
                                series: {
                                    id: 'pie',
                                    label: {
                                        formatter: '{b}: {@[' + dimension + ']} ({d}%)'
                                    },
                                    encode: {
                                        value: dimension,
                                        tooltip: dimension
                                    }
                                }
                            });
                        }
                    });
                    myChart.setOption(option);
                });

                if (option && typeof option === 'object') {
                    myChart.setOption(option);
                }

            }
        });
    }
    //合同来源统计
    function HtLyTj(value) {

        var $urls = "/ReportForms/ReportForms/HtLyTj";
        $.ajax({
            type: 'Get',
            url: $urls,
            data:
            {
                time: value
            },
            //  dataType: 'json',
            success: function (data) {
                var dom = document.getElementById("Htlytj");
                var myChart = echarts.init(dom);
                var app = {};

                var option;



                option = {
                    tooltip: {
                        trigger: 'item',
                        formatter: '{a} <br/>{b}: {c} ({d}%)'
                    },
                    legend: {
                        data: [
                            '招标合同-收',
                            '询价合同-收',
                            '指定合同-收',
                            '招标合同-付',
                            '询价合同-付',
                            '指定合同-付'
                          
                        ]
                    },
                    series: [
                        {
                            name: '付款合同',
                            type: 'pie',
                            selectedMode: 'single',
                            radius: [0, '30%'],
                            label: {
                                position: 'inner',
                                fontSize: 14
                            },
                            labelLine: {
                                show: false
                            },
                            data: [
                                { value:data.fzbht, name: '招标合同-付' },
                                { value:data.fxjht, name: '询价合同-付' },
                                { value:data.fzdht, name: '指定合同-付', selected: true }
                            ]
                        },
                        {
                            name: '收款合同',
                            type: 'pie',
                            radius: ['45%', '60%'],
                            labelLine: {
                                length: 30
                            },
                            label: {
                                formatter: '{a|{a}}{abg|}\n{hr|}\n  {b|{b}：}{c}  {per|{d}%}  ',
                                backgroundColor: '#F6F8FC',
                                borderColor: '#8C8D8E',
                                borderWidth: 1,
                                borderRadius: 4,
                                rich: {
                                    a: {
                                        color: '#6E7079',
                                        lineHeight: 22,
                                        align: 'center'
                                    },
                                    hr: {
                                        borderColor: '#8C8D8E',
                                        width: '100%',
                                        borderWidth: 1,
                                        height: 0
                                    },
                                    b: {
                                        color: '#4C5058',
                                        fontSize: 14,
                                        fontWeight: 'bold',
                                        lineHeight: 33
                                    },
                                    per: {
                                        color: '#fff',
                                        backgroundColor: '#4C5058',
                                        padding: [3, 4],
                                        borderRadius: 4
                                    }
                                }
                            },
                            data: [
                                { value:data.szbht, name: '招标合同-收' },
                                { value:data.sxjht, name: '询价合同-收' },
                                { value:data.szdht, name: '指定合同-收' },
                             
                            ]
                        }
                    ]
                };

                if (option && typeof option === 'object') {
                    myChart.setOption(option);
                }

            }
        });
    }


    function Bbpj() {
        var $urls = "/ReportForms/ReportForms/HtjeTj";
        $.ajax({
            type: 'Get',
            url: $urls,
            data:
            {

            },
            //  dataType: 'json',
            success: function (data) {
                var dom = document.getElementById("Bbpj");
                var myChart = echarts.init(dom);
                var app = {};
                var option;
                setTimeout(function () {
                    option = {
                        legend: {},
                        tooltip: {
                            trigger: 'axis',
                            showContent: false
                        },
                        dataset: {
                            source: [
                                ['product', data.time[0], data.time[1], data.time[2], data.time[3], data.time[4]],
                                ['金额一1', data.qyje[0], data.qyje[1], data.qyje[2], data.qyje[3], data.qyje[4]],
                                ['金额二2', data.ysje[0], data.ysje[1], data.ysje[2], data.ysje[3], data.ysje[4]],
                                ['金额三6', data.ssje[0], data.ssje[1], data.ssje[2], data.ssje[3], data.ssje[4]],
                               
                                ['金额一', 24.2],
                                ['金额二', 25.2],
                                ['金额三', 26.2],
                                ['金额四', 27.2]
                            ]
                        },
                      
                        xAxis: { type: 'category' },
                        yAxis: { gridIndex: 0 },
                        grid: { top: '55%' },
                        polar: {
                            radius: [30, '30%'],
                            center: ['60%', '25%']
                        },
                        toolbox: {
                            feature: {
                                saveAsImage: {}
                            }
                        },
                        angleAxis: {
                            max: 100,
                            startAngle: 75
                        },
                        radiusAxis: {
                            type: 'category',
                            data: ['金额一', '金额二', '金额三', '金额四']
                        },
                        series: [
                            {
                                type: 'line',
                                smooth: true,
                                seriesLayoutBy: 'row',
                                emphasis: { focus: 'series' }
                            },
                            {
                                type: 'line',
                                smooth: true,
                                seriesLayoutBy: 'row',
                                emphasis: { focus: 'series' }
                            },
                            {
                                type: 'line',
                                smooth: true,
                                seriesLayoutBy: 'row',
                                emphasis: { focus: 'series' }
                            },
                            {
                                type: 'line',
                                smooth: true,
                                seriesLayoutBy: 'row',
                                emphasis: { focus: 'series' }
                            },
                            {
                                type: 'bar',
                                id: 'bar',

                            //    data: [data.qyje[4], data.ysje[4], data.ssje[4], 25.2],
                                coordinateSystem: 'polar',
                                label: {
                                    show: true,
                                    position: 'middle',
                                    formatter: '{a}: {k}'
                                }
                            }
                        ]
                    };
                    myChart.on('updateAxisPointer', function (event) {
                        const xAxisInfo = event.axesInfo[0];
                        if (xAxisInfo) {
                            const dimension = xAxisInfo.value + 1;
                            myChart.setOption({
                                series: {
                                    id: 'bar',
                                    label: {
                                        formatter: '{a}: {@[' + dimension + ']} ({k}%)'
                                    },
                                    encode: {
                                        value: dimension,
                                        tooltip: dimension
                                    }
                                }
                            });
                        }
                    });
                    myChart.setOption(option);
                });
                if (option && typeof option === 'object') {
                    myChart.setOption(option);
                }
            }
        })
    }
    function Hxt() {
        var dom = document.getElementById("Hxt");
        var myChart = echarts.init(dom);
        var app = {};
        var option;
        option = {
            title: [
                {
                    text: 'Tangential Polar Bar Label Position (middle)'
                }
            ],
            polar: {
                radius: [30, '80%']
            },
            dataset: {
                source: [
                    ['金额一', 55.2],
                    ['金额二', 25.2],
                    ['金额三', 26.2],
                    ['金额四', 27.2]
                ]
            },
            angleAxis: {
                max: 100,
                startAngle: 75
            },
            radiusAxis: {
                type: 'category',
               // data: ['金额一', '金额二', '金额三', '金额四']
            },
            tooltip: {},
            series: {
                type: 'bar',
                // data: [2, 1.2, 2.4, 3.6],
                coordinateSystem: 'polar',
                label: {
                    show: true,
                    position: 'middle',
                    formatter: '{b}: {c}'
                }
            }
        };


        if (option && typeof option === 'object') {
            myChart.setOption(option);
        }
    }
    function Zxtjt() {
        var $urls = "/ReportForms/ReportForms/HtjeTj";
        $.ajax({
            type: 'Get',
            url: $urls,
            data:
            {
               
            },
            //  dataType: 'json',
            success: function (data) {
                var dom = document.getElementById("Zxtjt");
                var myChart = echarts.init(dom);
                var app = {};
                var option;
                var serdate = [0,0,0];
                setTimeout(function () {
                    option = {
                        legend: {},
                        tooltip: {
                            trigger: 'axis',
                            showContent: false
                        },
                        dataset: {
                            source: [
                                ['product',  data.time[0], data.time[1], data.time[2], data.time[3], data.time[4]],
                                ['签约金额', data.qyje[0], data.qyje[1], data.qyje[2], data.qyje[3], 25/*data.qyje[4]*/],
                                ['应收金额', data.ysje[0], data.ysje[1], data.ysje[2], data.ysje[3], data.ysje[4]],
                                ['实收金额', data.ssje[0], data.ssje[1], data.ssje[2], data.ssje[3],5542 /*data.ssje[4]*/],
                                ['Walnut Brownie', 25.2, 37.1, 41.2, 18, 33.9, 49.1]
                            ]
                        },
                        xAxis: { type: 'category' },
                        yAxis: { gridIndex: 0 },

                        polar: {
                            radius: [30, '30%'],
                            center: ['60%', '25%']
                        },
                        grid: { top: '55%' },
                        toolbox: {
                            feature: {
                                saveAsImage: {}
                            }
                        },
                        angleAxis: {
                            max: 40,
                            startAngle: 75
                        },
                        radiusAxis: {
                            type: 'category',
                            data: ['未收金额', '实收金额', '应收金额']
                        },
                        series: [
                            {
                                type: 'line',
                                smooth: true,
                                seriesLayoutBy: 'row',
                                emphasis: { focus: 'series' }
                            },
                            {
                                type: 'line',
                                smooth: true,
                                seriesLayoutBy: 'row',
                                emphasis: { focus: 'series' }
                            },
                            {
                                type: 'line',
                                smooth: true,
                                seriesLayoutBy: 'row',
                                emphasis: { focus: 'series' }
                            },
                            {
                                type: 'line',
                                smooth: true,
                                seriesLayoutBy: 'row',
                                emphasis: { focus: 'series' }
                            },
                            {
                                type: 'bar',
                                id: 'bar',
                              
                                coordinateSystem: 'polar',
                                label: {
                                    show: true,
                                    position: 'middle',
                                    formatter: '{b}: {c}'
                                }
                            }
                        ]
                    };
                    //myChart.on('click', function (params) {
                    //    debugger;
                    //    serdate = [data.qyje[4], data.ysje[4], data.ssje[4]];
                    //    chart.setOption(option, {
                    //        replaceMerge: ['radiusAxis', 'series']
                    //    });
                    //});
                    myChart.on('updateAxisPointer', function (event) {
                      
                        const xAxisInfo = event.axesInfo[0];
                        if (xAxisInfo) {
                           
                            const dimension = xAxisInfo.value + 1;
                            myChart.setOption({
                                series: {
                                    id: 'bar',
                                    label: {
                                        formatter: '{b}: {@[' + dimension + ']} ({c})'
                                    },
                                    encode: {
                                        value: dimension,
                                        tooltip: dimension
                                    }
                                }
                            });
                        }
                    });
                    myChart.setOption(option);
                });

                if (option && typeof option === 'object') {
                    myChart.setOption(option);
                }
            }
        })
    }



    function Zxtjtwer() {
        var $urls = "/ReportForms/ReportForms/HtjeTj";
        $.ajax({
            type: 'Get',
            url: $urls,
            data:
            {

            },
            //  dataType: 'json',
            success: function (data) {
                var dom = document.getElementById("Zxtjtwer");
                var myChart = echarts.init(dom);
                var app = {};

                var option;



                setTimeout(function () {
                    option = {
                        legend: {},
                        tooltip: {
                            trigger: 'axis',
                            showContent: false
                        },
                        dataset: {
                            source: [
                                ['product', '2012', '2013', '2014', '2015', '2016', '2017'],
                                ['Milk Tea', 56.5, 82.1, 88.7, 70.1, 53.4, 85.1],
                                ['Matcha Latte', 51.1, 51.4, 55.1, 53.3, 73.8, 68.7],
                                ['Cheese Cocoa', 40.1, 62.2, 69.5, 36.4, 45.2, 32.5],
                                ['Walnut Brownie', 25.2, 37.1, 41.2, 18, 33.9, 49.1]
                            ]
                        },
                        xAxis: { type: 'category' },
                        yAxis: { gridIndex: 0 },
                        grid: { top: '55%' },
                        polar: {
                            radius: [30, '30%'],
                            center: ['60%', '25%']
                        },
                        grid: { top: '55%' },
                        radiusAxis: {
                            type: 'category',
                            data: ['未收金额', '实收金额', '应收金额']
                        },
                        series: [
                            {
                                type: 'line',
                                smooth: true,
                                seriesLayoutBy: 'row',
                                emphasis: { focus: 'series' }
                            },
                            {
                                type: 'line',
                                smooth: true,
                                seriesLayoutBy: 'row',
                                emphasis: { focus: 'series' }
                            },
                            {
                                type: 'line',
                                smooth: true,
                                seriesLayoutBy: 'row',
                                emphasis: { focus: 'series' }
                            },
                            {
                                type: 'line',
                                smooth: true,
                                seriesLayoutBy: 'row',
                                emphasis: { focus: 'series' }
                            }, {
                                type: 'bar',
                                id: 'bar',
                                coordinateSystem: 'polar',
                                label: {
                                   formatter: '{b}: {@2012} ({d}%)'
                               },
                            }
                            //{
                            //    type: 'bar',
                            //    id: 'bar',
                            //    radius: '30%',
                            //    center: ['50%', '25%'],
                            //    emphasis: {
                            //        focus: 'self'
                            //    },
                            //    label: {
                            //        formatter: '{b}: {@2012} ({d}%)'
                            //    },
                            //    encode: {
                            //        itemName: 'product',
                            //        value: '2012',
                            //        tooltip: '2012'
                            //    }
                            //}
                        ]
                    };
                    myChart.on('updateAxisPointer', function (event) {
                        const xAxisInfo = event.axesInfo[0];
                        if (xAxisInfo) {
                            const dimension = xAxisInfo.value + 1;
                            myChart.setOption({
                                series: {
                                    id: 'bar',
                                    label: {
                                        formatter: '{b}: {@[' + dimension + ']} ({d}%)'
                                    },
                                    encode: {
                                        value: dimension,
                                        tooltip: dimension
                                    }
                                }
                            });
                        }
                    });
                    myChart.setOption(option);
                });

                if (option && typeof option === 'object') {
                    myChart.setOption(option);
                }
            }
        })
    }

    layui.use(['laydate'], function () {
        var laydate = layui.laydate
        laydate.render({
            elem: '#Name'
            , type: 'year',
            done: function (value, date, endDate) {
                HtqdYsSsjeTj(value);
               

            }

        });
        laydate.render({
            elem: '#Times'
            , type: 'year',
            done: function (value, date, endDate) {
                HtZjFpTj(value);
               

            }

        });
       
    });
        exports('CollectionContractDetail', {});
    });