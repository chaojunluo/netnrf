top != self && (top.location = self.location);

$(document).keydown(function (e) {
    if ((e.keyCode || e.which || e.charCode) == 13) {
        $('#btnLogin')[0].click()
    }
});

$('#btnLogin').click(function () {
    if ($("#username").val() != "" && $("#userpwd").val() != "" && $('#captcha').val() != "") {
        writemsg('<span class="text-muted">登录中</span>', true);

        $('#btnLogin')[0].disabled = true;

        $.ajax({
            url: "/Account/LoginValidation?" + new Date().valueOf(),
            type: "POST",
            data: $('#formLogin').serialize(),
            dataType: 'json',
            success: function (data) {
                if (data.code == 100) {
                    writemsg('<span class="text-success">' + data.message + '</span>');
                    window.location.href = data.url;
                }
                else {
                    writemsg(data.message);
                    if (data.code == 104) {
                        $('#captcha').val('')[0].focus();
                        $("#img_captcha")[0].click();
                    } else {
                        $("#username").val("");
                        $("#userpwd").val("");
                    }
                }
            },
            error: function () {
                writemsg("网络错误。");
                $("#img_captcha")[0].click();
            },
            complete: function () {
                $('#btnLogin')[0].disabled = false;
            }
        })
    }
    else {
        writemsg("请完整填写信息。");
    }
});

$('#username')[0].focus();

//刷新验证码
$("#img_captcha").click(function () {
    this.src = "/Account/Captcha?" + new Date().valueOf();
});

//输出消息
var defer1;
function writemsg(msg, keep) {
    $('#loginmsg').html(msg);
    clearTimeout(defer1);
    if (!keep) {
        defer1 = setTimeout(function () {
            $('#loginmsg').html('');
        }, 1000 * 7)
    }
}

$(window).on('load resize', function () {
    var cc = $('.cc');
    cc.css('margin-top', $(window).height() / 2 - cc.height() / 2)
});