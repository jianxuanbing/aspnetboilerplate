﻿(function ($) {         //传入参数$，保证内部使用的$,是JQuery变量

    //文档加载完毕立即执行的js代码(例如初始化代码)
    $(function () {
        moment.locale('zh-CN');    //设置当前语言环境为中文
        LanguagesTable.tableInit();
        LanguagesButtons.buttonsInit();
        //注册创建或修改完语言保存之后的事件(事件为:重新加载语言表格数据);
        abp.event.on('app.createOrEditLanguageModalSaved', function () {
            LanguagesTable.tableRefresh();
        });
    });

})(jQuery);

var LanguagesTable = function () {

    var getQueryParams = function (params) {
        var temp = {   //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            limit: params.limit,   //页面大小
            offset: params.offset,  //页码
            name: $("#txt_search_name").val(),
            displayName: $("#txt_search_displayName").val()
        };
        return temp;
    };

    return {
        tableInit: function () {
            $('#tb_departments').bootstrapTable({
                url: '/Languages/GetLanguages',         //请求后台的URL（*）
                method: 'get',                      //请求方式（*）
                toolbar: '#toolbar',                //工具按钮用哪个容器
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
                singleSelect:true,
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
                    title: '代码'
                }, {
                    field: 'DisplayName',
                    title: '名称'
                }, {
                    field: 'CreationTime',
                    title: '创建时间',
                    formatter: function (value) {
                        return moment(value).format('LLL');
                    }
                }, {
                    field: 'Icon',
                    title: '图标',
                    formatter: function(value) {
                        return "<i class=" + value + "></i>";
                    }
                }, {
                    field: 'Id',
                    title: '操作',
                    formatter: function (value) {
                        return "<div class='btn-group'>" +
                            "<button onclick=LanguagesButtons.text(" + value + ") type='button' class='btn btn-success' aria-label='Justify' title='修改文本'><span class='glyphicon glyphicon-align-justify' aria-hidden='true'></span></button>" +
                            "<button type='button' onclick=LanguagesButtons.setDefault(" + value + ") class='btn btn-warning' aria-label='Center Align' title='设置为默认语言'><span class='glyphicon glyphicon-ok' aria-hidden='true'></span></button>" +
                            "</div>";
                    }
                }]
            });
        },
        tableRefresh:function() {
            $("#tb_departments").bootstrapTable('refresh');
        } 
    };
}();

var LanguagesButtons = function () {

    var createOrEditModal = new app.ModalManager({
        viewUrl: abp.appPath + 'Languages/CreateOrEditModal',
        scriptUrl: abp.appPath + 'Scripts/Project/Languages/CreateOrEditModal.js',
        modalClass: 'CreateOrEditLanguageModal'
    });

    var textModal = new app.ModalManager({
        viewUrl: abp.appPath + 'Languages/TextModal',
        scriptUrl: abp.appPath + 'Scripts/Project/Languages/TextModal.js',
        modalClass: 'TextModal'
    });

    var queryButtonInit = function () {
        $("#btn_query").click(function () {
            $("#tb_departments").bootstrapTable('refresh');
        });
    };

    var addButtonInit = function () {
        $("#btn_add").click(function () {
            createOrEditModal.open();
        });
    };

    var editButtonInit = function () {
        $("#btn_edit").click(function () {
            var arrselections = $("#tb_departments").bootstrapTable('getSelections');
            if (arrselections.length <= 0) {
                abp.notify.warn('请选择一行数据！');
                return;
            }
            createOrEditModal.open({ id: arrselections[0].Id });
        });
    };

    var deleteButtonInit = function () {
        $("#btn_delete").click(function () {
            var arrselections = $("#tb_departments").bootstrapTable('getSelections');
            if (arrselections.length <= 0) {
                abp.notify.warn('请选择一行数据！');
                return;
            }
            abp.message.confirm(
                "删除后数据不能恢复","确定删除吗?",
                function (isConfirmed) {
                    if (isConfirmed) {
                        abp.ajax({
                            url: abp.appPath + 'Languages/DeleteLanguage',
                            type: 'POST',
                            data: JSON.stringify({
                                id: arrselections[0].Id
                            }),
                            success: function(result, data) {
                                abp.notify.success(result);
                                LanguagesTable.tableRefresh();
                            }
                        });
                    }
                }
            );
        });
    };

    return {
        buttonsInit: function () {
            queryButtonInit();
            addButtonInit();
            editButtonInit();
            deleteButtonInit();
        },
        text: function (id) {
            textModal.open({ id: id });
        },
        setDefault: function (id) {
            abp.ajax({
                url: abp.appPath + 'Languages/SetDefaultLanguage',
                type: 'POST',
                data: JSON.stringify({
                    id: id
                }),
                success: function (result, data) {
                    abp.notify.success(result);
                    LanguagesTable.tableRefresh();
                }
            });
        }
    };
}();