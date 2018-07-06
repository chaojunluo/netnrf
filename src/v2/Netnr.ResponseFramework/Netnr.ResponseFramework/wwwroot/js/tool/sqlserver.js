
function loadTableInfo() {
    $.ajax({
        url: "/tool/SQLServerTableInfo",
        dataType: 'json',
        success: function (data) {
            var htmAll = [], htmHas = [];
            $(data.listAll).each(function () {
                htmAll.push('<option value="' + this + '">' + this + '</option>');
            });
            $(data.listHas).each(function () {
                htmHas.push('<option value="' + this + '">' + this + '</option>');
            });
            $('#labAll').html(data.listAll.length + " 张表");
            $('#labHas').html(data.listHas.length + " 张表");
            $('#seTableNameAll').html(htmAll.join(''));
            $('#seTableNameHas').html(htmHas.join(''));
        }
    })
};
loadTableInfo();

$('#btnBuilder').click(function () {
    var vals = $('#seTableNameAll').val();
    if (vals.length) {
        $('#btnBuilder')[0].disabled = true;
        $.ajax({
            url: '/tool/SQLServerTableInfoBuilder',
            data: {
                names: vals.join(','),
                cover: $('#chkCover')[0].checked ? 1 : 0
            },
            success: function (data) {
                if (data == "success") {
                    loadTableInfo();
                    alert('生成成功');
                } else {
                    alert('生成失败');
                }
            },
            error: function () {
                alert('生成失败');
            },
            complete: function () {
                $('#btnBuilder')[0].disabled = false;
            }
        })
    }
});