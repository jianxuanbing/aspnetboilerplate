(function ($) {
    app.modals.TextModal = function () {

        var _modalManager;
        var $LanguageTextTable = null;

        this.init = function (modalManager) {
            _modalManager = modalManager;

            $LanguageTextTable = _modalManager.getModal()
                .find('#tb_LanguageTexts');

            _modalManager.getModal()
                .find('#TextBaseLanguageSelectionCombobox,#TextTargetLanguageSelectionCombobox')
                .selectpicker({
                    iconBase: "famfamfam-flag",
                    tickIcon: "fa fa-check"
                });

            _modalManager.getModal()
                .find('#TextSourceSelectionCombobox,#TargetValueFilterSelectionCombobox')
                .selectpicker({
                    iconBase: "fa",
                    tickIcon: "fa fa-check"
                });

            LanguagesTextTable.tableInit($LanguageTextTable);

            _modalManager.getModal()
                .find('#RefreshTextsButton').click(function (e) {
                    e.preventDefault();
                    LanguagesTextTable.tableRefresh($LanguageTextTable);
                });

            //表单里的combox选择菜单改变后，就重新获取表格数据
            _modalManager.getModal()
                .find('#TextsFilterForm select').change(function () {
                    LanguagesTextTable.tableRefresh($LanguageTextTable);
            });

            _modalManager.getModal().find('#TextFilter').focus();

            abp.event.on('app.editLanguageTextModalSaved', function () {
                LanguagesTextTable.tableRefresh($LanguageTextTable);
            });
        };
    };

})(jQuery);

var LanguagesTextTable = function () {

    var editTextModal = new app.ModalManager({
        viewUrl: abp.appPath + 'Languages/EditTextModal',
        scriptUrl: abp.appPath + 'Scripts/Project/Languages/EditTextModal.js',
        modalClass: 'EditLanguageTextModal'
    });

    var getQueryParams = function (params) {
        var temp = {   //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            maxResultCount: params.limit,   //页面大小
            skipCount: params.offset,  //页码
            targetLanguageName: $('#TextTargetLanguageSelectionCombobox').val(),
            sourceName: $('#TextSourceSelectionCombobox').val(),
            baseLanguageName: $('#TextBaseLanguageSelectionCombobox').val(),
            targetValueFilter: $('#TargetValueFilterSelectionCombobox').val(),
            filterText: $('#TextFilter').val()
        };
        return temp;
    };
    return {
        tableInit: function ($LanguageTextTable) {
            $LanguageTextTable.bootstrapTable({
                url: '/Languages/GetLanguageTexts',         //请求后台的URL（*）
                method: 'get',                      //请求方式（*）
                striped: true,                      //是否显示行间隔色
                cache: false,                       //是否使用缓存，默认为true，所以一般情况下需要设置一下这个属性（*）
                pagination: true,                   //是否显示分页（*）
                sortable: false,                     //是否启用排序
                sortOrder: "asc",                   //排序方式
                queryParams: getQueryParams,//传递参数（*）
                sidePagination: "server",           //分页方式：client客户端分页，server服务端分页（*）
                pageNumber: 1,                       //初始化加载第一页，默认第一页
                pageSize: 10,                       //每页的记录行数（*）
                pageList: [10, 25, 50, 100],        //可供选择的每页的行数（*）
                search: false,                       //是否显示表格搜索，此搜索是客户端搜索，不会进服务端，所以，个人感觉意义不大
                strictSearch: true,
                singleSelect: true,
                showColumns: true,                  //是否显示所有的列
                showRefresh: true,                  //是否显示刷新按钮
                minimumCountColumns: 2,             //最少允许的列数
                clickToSelect: true,                //是否启用点击选中行
                height: 300,                        //行高，如果没有设置height属性，表格自动根据记录条数觉得表格高度
                uniqueId: "Key",                     //每一行的唯一标识，一般为主键列
                showToggle: true,                    //是否显示详细视图和列表视图的切换按钮
                cardView: false,                    //是否显示详细视图
                detailView: false,                   //是否显示父子表
                columns: [{
                    width: '20%',
                    field: 'Key',
                    title: '键值'
                }, {
                    width: '35%',
                    field: 'BaseValue',
                    title: '默认值'
                }, {
                    width: '35%',
                    field: 'TargetValue',
                    title: '目标值'
                }, {
                    width: '10%',
                    title: '操作',
                    formatter: function (value, row, index) {
                        return "<div class='btn-group'>" +
                            "<button onclick=LanguagesTextTable.tableEditButtonClick('" + row.Key + "') type='button' class='btn btn-success' aria-label='Justify' title='修改'><span class='glyphicon glyphicon-pencil' aria-hidden='true'></span></button>" +
                            "</div>";
                    }
                }]
            });
        },
        tableRefresh: function ($LanguageTextTable) {
            $LanguageTextTable.bootstrapTable('refresh');
        },
        tableEditButtonClick: function(key) {
            editTextModal.open({
                sourceName: $('#TextSourceSelectionCombobox').val(),
                baseLanguageName: $('#TextBaseLanguageSelectionCombobox').val(),
                languageName: $('#TextTargetLanguageSelectionCombobox').val(),
                key: key
            });
        }
    };
}();