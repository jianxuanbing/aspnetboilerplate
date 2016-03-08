var abp = abp || {};
(function ($) {
    if (!sweetAlert || !$) {
        return;
    }

    /* DEFAULTS暂时全部修改本地化中文 *************************************************/

    abp.libs = abp.libs || {};
    abp.libs.sweetAlert = {
        config: {
            'default': {

            },
            info: {
                type: 'info'
            },
            success: {
                type: 'success'
            },
            warn: {
                type: 'warning'
            },
            error: {
                type: 'error'
            },
            confirm: {
                type: 'warning',
                title: '你确定吗?',    //by Edit BeiDream
                showCancelButton: true,
                cancelButtonText: '取消',    //by Edit BeiDream
                confirmButtonColor: "#DD6B55",
                confirmButtonText: '确定'   //by Edit BeiDream
            }
        }
    };

    /* MESSAGE **************************************************/

    var showMessage = function (type, message, title) {
        if (!title) {
            title = message;
            message = undefined;
        }

        var opts = $.extend(
            {},
            abp.libs.sweetAlert.config.default,
            abp.libs.sweetAlert.config[type],
            {
                title: title,
                confirmButtonText: '确定',  //by Add BeiDream
                text: message
            }
        );

        return $.Deferred(function ($dfd) {
            sweetAlert(opts, function () {
                $dfd.resolve();
            });
        });
    };

    abp.message.info = function (message, title) {
        return showMessage('info', message, title);
    };

    abp.message.success = function (message, title) {
        return showMessage('success', message, title);
    };

    abp.message.warn = function (message, title) {
        return showMessage('warn', message, title);
    };

    abp.message.error = function (message, title) {
        return showMessage('error', message, title);
    };

    abp.message.confirm = function (message, titleOrCallback, callback) {
        var userOpts = {
            text: message
        };

        if ($.isFunction(titleOrCallback)) {
            callback = titleOrCallback;
        } else if (titleOrCallback) {
            userOpts.title = titleOrCallback;
        };

        var opts = $.extend(
            {},
            abp.libs.sweetAlert.config.default,
            abp.libs.sweetAlert.config.confirm,
            userOpts
        );

        return $.Deferred(function ($dfd) {
            sweetAlert(opts, function (isConfirmed) {
                callback && callback(isConfirmed);
                $dfd.resolve();
            });
        });
    };
    //abp封装的动态事件,还没研究(此方法的功能是设置sweetAlert的语言本地化)
    abp.event.on('abp.dynamicScriptsInitialized', function () {
        abp.libs.sweetAlert.config.confirm.title = abp.localization.abpWeb('AreYouSure');
        abp.libs.sweetAlert.config.confirm.cancelButtonText = abp.localization.abpWeb('Cancel');
        abp.libs.sweetAlert.config.confirm.confirmButtonText = abp.localization.abpWeb('Yes');
    });

})(jQuery);