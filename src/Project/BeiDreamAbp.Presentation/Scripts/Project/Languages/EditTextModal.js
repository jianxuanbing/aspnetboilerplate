(function ($) {
    app.modals.EditLanguageTextModal = function () {

        var _modalManager;
        var _$editLanguageTextForm = null;

        this.init = function (modalManager) {
            _modalManager = modalManager;

            _$editLanguageTextForm = _modalManager.getModal().find('form[name=EditLanguageTextForm]');
            _$editLanguageTextForm.validate();
        };

        this.save = function () {
            if (!_$editLanguageTextForm.valid()) {
                return;
            }

            _modalManager.setBusy(true);

            //获取表单数据
            var formDatas = _$editLanguageTextForm.serializeFormToObject();

            abp.ui.setBusy(
                $('#EditLanguageTextModal'),
                abp.ajax({
                    url: abp.appPath + 'Languages/UpdateLanguageText',
                    type: 'POST',
                    data: JSON.stringify(formDatas),
                    success: function (result, data) {
                        abp.message.success(result);
                        //关闭模态窗
                        _modalManager.close();
                        //触发重新加载语言表格数据事件
                        abp.event.trigger('app.editLanguageTextModalSaved');
                    }
                })
            );

            _modalManager.setBusy(false);
        };
    };
})(jQuery);