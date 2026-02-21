var woowx = {
  
    constant: null,  //静态值
    currinfo:null//当前信息
};
//固定值
woowx.constant = {
   // APIBaseURL: "http://lhxm.5gzvip.idcfengye.com"
    APIBaseURL: "",//目前不支持跨域，别配置，否则ajax请求出错
    page:10

}

//当前信息
woowx.currinfo = {
    WxUserId:"wx"//当前登录微信ID
}
//woowx.constant.APIBaseURL
