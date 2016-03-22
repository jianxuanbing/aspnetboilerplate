(function ($) {         //传入参数$，保证内部使用的$,是JQuery变量

    //文档加载完毕立即执行的js代码(例如初始化代码)
    $(function () {
        moment.locale('zh-CN');    //设置当前语言环境为中文
        EditionsTable.tableInit();
        EditionsButtons.buttonsInit();
    });

})(jQuery);

var EditionsTable = function() {
    return {
        tableInit: function () {
            $('#tb_editions').bootstrapTable({
                url: '/Editions/GetEditions',         //请求后台的URL（*）
                method: 'get',                      //请求方式（*）
                toolbar: '#toolbar',                //工具按钮用哪个容器
                striped: true,                      //是否显示行间隔色
                cache: false,                       //是否使用缓存，默认为true，所以一般情况下需要设置一下这个属性（*）
                pagination: false,                   //是否显示分页（*）
                sortable: false,                     //是否启用排序
                sortOrder: "asc",                   //排序方式
                queryParams: {},//传递参数（*）
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
                height: 500,                        //行高，如果没有设置height属性，表格自动根据记录条数觉得表格高度
                uniqueId: "Id",                     //每一行的唯一标识，一般为主键列
                showToggle: true,                    //是否显示详细视图和列表视图的切换按钮
                cardView: false,                    //是否显示详细视图
                detailView: false,                   //是否显示父子表
                columns: [{
                    checkbox: true
                }, {
                    field: 'Name',
                    title: '版本名称'
                }, {
                    field: 'CreationTime',
                    title: '创建时间',
                    formatter: function (value) {
                        return moment(value).format('L');
                    }
                }]
            });
        },
        tableRefresh: function () {
            $("#tb_editions").bootstrapTable('refresh');
        }
    };
}();

var EditionsButtons = function () {
    var createOrEditModal = new app.ModalManager({
        viewUrl: abp.appPath + 'Editions/CreateOrEditModal',
        scriptUrl: abp.appPath + 'Scripts/Project/Editions/CreateOrEditModal.js',
        modalClass: 'CreateOrEditEditionModal'
    });
    var addButtonInit = function () {
        $("#btn_add").click(function () {
            createOrEditModal.open();
        });
    };
    var editButtonInit = function () {
        $("#btn_edit").click(function () {
            var arrselections = $("#tb_editions").bootstrapTable('getSelections');
            if (arrselections.length <= 0) {
                abp.notify.warn('请选择一行数据！');
                return;
            }
            createOrEditModal.open({ id: arrselections[0].Id });
        });
    };
    return {
        buttonsInit: function () {
            addButtonInit();
            editButtonInit();
        }
    };
}();