(function ($) {
    $(function () {

        //添加租户

        var addTenantModal = new app.ModalManager({
            viewUrl: abp.appPath + 'Tenants/AddTenant',      //对应的Action方法返回View
            scriptUrl: abp.appPath + 'Scripts/Project/Tenants/AddTenantOp.js',
            modalClass: 'AddTenant'   //调用的上面js里的对应的方法的名字
        });

        $('#AddTenant').click(function (e) {
            e.preventDefault();
            addTenantModal.open();
        });


    });
})(jQuery);