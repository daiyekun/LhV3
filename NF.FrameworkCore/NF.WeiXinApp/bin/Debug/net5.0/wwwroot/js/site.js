//底部导航按钮点击切换字体颜色
$(function () {
    $('.weui-tabbar__item').on('click', function () {
        $(this).addClass('weui-bar__item_on').siblings('.weui-bar__item_on').removeClass('weui-bar__item_on');
    });
    //点击底部按钮以后颜色改变
    var $mypage = $("#mypage").val();
    if ($mypage != undefined) {
        $("a[mymen=fm_" + $mypage + "]").addClass('weui-bar__item_on').siblings('.weui-bar__item_on').removeClass('weui-bar__item_on');
    }

   

});
//移动某个对象
function moveEvent(moveId) {
    var div1 = document.querySelector('#' + moveId);
    var maxW = document.body.clientWidth - div1.offsetWidth;
    var maxH = document.body.clientHeight - div1.offsetHeight;

    div1.addEventListener('touchstart', function (e) {
        var ev = e || window.event;
        var touch = ev.targetTouches[0];
        oL = touch.clientX - div1.offsetLeft;
        oT = touch.clientY - div1.offsetTop;
        document.addEventListener("touchmove", defaultEvent, false);
    })


    div1.addEventListener('touchmove', function (e) {
        var ev = e || window.event;
        var touch = ev.targetTouches[0];
        var oLeft = touch.clientX - oL;
        var oTop = touch.clientY - oT;
        if (oLeft < 0) {
            oLeft = 0;
        } else if (oLeft >= maxW) {
            oLeft = maxW;
        }
        if (oTop < 0) {
            oTop = 0;
        } else if (oTop >= maxH) {
            oTop = maxH;
        }

        div1.style.left = oLeft + 'px';
        div1.style.top = oTop + 'px';
        e.preventDefault();

    })
    div1.addEventListener('touchend', function () {
        document.removeEventListener("touchmove", defaultEvent);
    })
    function defaultEvent(e) {


        e.preventDefault();
    }
}
