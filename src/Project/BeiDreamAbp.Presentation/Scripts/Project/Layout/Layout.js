(function ($) {
    $(function () {

        $('#UserProfileChangePasswordLink').click(function (e) {
            e.preventDefault();
            $.post('/Profile/ChangePasswordModal', {}, function (changePasswordContent) {
                layer.open({
                    type: 1,
                    title: "修改密码",
                    area: ['600px', '360px'],
                    zIndex:9999,
                    shadeClose: true, //点击遮罩关闭
                    content: changePasswordContent, //注意，如果str是object，那么需要字符拼接。
                    success: function (layero, index) {
                        var $form = layero.find('form[name=ChangePasswordForm]');
                        $form.validate();
                        
                        $('#btnSave').click(function (e) {
                            if (!$form.valid()) {
                                return;
                            }
                            //获取表单数据
                            var formDatas = $form.serializeFormToObject();
                            //todo:Save this Object Data

                            abp.ui.setBusy(
                                $('#ChangePasswordModal'),
                                abp.ajax({
                                    url: abp.appPath + 'Profile/ChangePassword',
                                    type: 'POST',
                                    data: JSON.stringify(formDatas),
                                    success: function (result, data) {
                                        abp.message.success(result);
                                        layer.close(index);
                                    }
                                })
                            );
                        });

                        $('#btnCancel').click(function (e) {
                            layer.close(index);
                        });
                    }
                });
            });
        });


    });
})(jQuery);