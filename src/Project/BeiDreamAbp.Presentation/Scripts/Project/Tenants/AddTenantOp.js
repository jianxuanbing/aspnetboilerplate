(function ($) {
    app.modals.AddTenant = function () {

        var _modalManager;
        var _$form = null;

        //初始化弹出
        this.init = function (modalManager) {
            _modalManager = modalManager;

            _$form = _modalManager.getModal().find('form[name=AddTenantForm]');
            _$form.validate();
        };

        this.save = function () {
            if (!_$form.valid()) {
                return;
            }

            //设置操作按钮不可用，防止重复提交等操作
            _modalManager.setBusy(true);
            //获取表单数据
            var formDatas = _$form.serializeFormToObject();

            abp.ui.setBusy(
                $('#AddTenantModal'),
                abp.ajax({
                    url: abp.appPath + 'Tenants/AddTenant',
                    type: 'POST',
                    data: JSON.stringify(formDatas),
                    success: function (result, data) {
                        abp.message.success(result);
                        //关闭模态窗
                        _modalManager.close();
                    }
                })
            );
            //设置操作按钮可用
            _modalManager.setBusy(false);
        };

    };
})(jQuery);