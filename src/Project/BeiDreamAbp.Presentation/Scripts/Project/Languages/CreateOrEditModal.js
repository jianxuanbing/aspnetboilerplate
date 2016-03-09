(function ($) {
    app.modals.CreateOrEditLanguageModal = function () {

        var _modalManager;
        var _$languageInformationForm = null;

        this.init = function (modalManager) {
            _modalManager = modalManager;

            _modalManager.getModal()
                .find('#LanguageNameEdit')
                .selectpicker({
                    iconBase: "fa",
                    tickIcon: "fa fa-check"
                });

            _modalManager.getModal()
                .find('#LanguageIconEdit')
                .selectpicker({
                    iconBase: "famfamfam-flag",
                    tickIcon: "fa fa-check"
                });

            _$languageInformationForm = _modalManager.getModal().find('form[name=LanguageInformationsForm]');
        };

        this.save = function () {
            //设置操作按钮不可用，防止重复提交等操作
            _modalManager.setBusy(true);
            //获取表单数据
            var formDatas = _$languageInformationForm.serializeFormToObject();

            abp.ui.setBusy(
                $('#AddTenantModal'),
                abp.ajax({
                    url: abp.appPath + 'Languages/CreateOrUpdateLanguage',
                    type: 'POST',
                    data: JSON.stringify({
                        language: formDatas
                    }),
                    success: function (result, data) {
                        abp.message.success(result);
                        //关闭模态窗
                        _modalManager.close();
                        //触发重新加载语言表格数据事件
                        abp.event.trigger('app.createOrEditLanguageModalSaved');
                    }
                })
            );
            //设置操作按钮可用
            _modalManager.setBusy(false);
        };
    };
})(jQuery);