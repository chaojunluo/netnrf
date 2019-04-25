$('#authbind').click(function (e) {
    e = e || window.event;
    var target = e.target || e.srcElement;
    if (target.nodeName == "IMG") {
        var vtype = target.src.split('/images/login/')[1].replace(".svg", '');
        //已关联
        if ($(target).next().hasClass('green')) {
            art('确定解除关联？', function () {
                $.ajax({
                    url: "/account/authunbind/" + vtype,
                    success: function (data) {
                        if (data == "success") {
                            location.reload(false)
                        } else {
                            art('fail');
                        }
                    }
                })
            })
        } else {
            var h = 580, w = 780, sh = (window.screen.availHeight - h) / 2, sw = (window.screen.availWidth - w) / 2;
            window.open("/account/auth/" + vtype, 'newwindow', 'height=' + h + ', width=' + w + ', top=' + sh + ', left=' + sw + ', toolbar=no, menubar=no, scrollbars=no, resizable=no,location=no, status=no')
        }
    }
});