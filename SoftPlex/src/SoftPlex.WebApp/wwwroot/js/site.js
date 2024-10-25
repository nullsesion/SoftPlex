// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
// Write your JavaScript code.

$(function () {
    var dialog, form;

    function addProduct() {
        var listRequestProductVersion = [];

        var name_product_id = $("#dialog-form")
            .find('[name="name_product_id"]').val();

        $("#dialog-form")
            .find(".js-product-version")
            .each(function (i, e) {
                listRequestProductVersion.push({
                    "id": $(e).find('[name="productversion_guid"]').val(), 
                    "productId": name_product_id,
                    "name": $(e).find('[name="name"]').val(),
                    "description": $(e).find('[name="description"]').val(),
                    "width": $(e).find('[name="height"]').val() * 1,
                    "height": $(e).find('[name="width"]').val() * 1,
                    "length": $(e).find('[name="length"]').val() * 1
                });
            });
        var d = JSON.stringify({
            "id": name_product_id,
            "name": $("#dialog-form").find('[name="name_product"]').val(),
            "description": $("#dialog-form").find('[name="description_product"]').val(),
            "listRequestProductVersion": listRequestProductVersion
        });

        var settings = {
            "url": "/Product/CreateProduct",
            "method": "POST",
            "timeout": 0,
            "headers": {
                "Content-Type": "application/json",
            },
            "data": d,
        };

        console.log(settings)

        $.ajax(settings).done(function (response) {
            //add добавление ошибок
            console.log(response);
        });
    }

    dialog = $("#dialog-form").dialog({
        autoOpen: false,
        height: 800,
        width: 1400,
        modal: true,
        buttons: {
            "Add Product": addProduct,
            Cancel: function () {
                dialog.dialog("close");
            }
        },
        close: function () {
            $("#js-add-product").trigger("reset");
            //allFields.removeClass("ui-state-error");
        }
    });

    form = dialog.find("form.js-add-product").on("submit", function (event) {
        event.preventDefault();
        addProduct();
    });

    $(".js-button_add").on("click", function () {
        var htmlForm = $("#dialog-form__into")
            .html();

        $("#dialog-form")
            .html(htmlForm);

        $("#dialog-form .js-accordion").accordion();

        dialog.dialog("open");
    });

    $(".js-button_add-res__button").on("click", function () {
        var htmlForm = $(this)
            .closest(".js-button_add-res__wrapp")
            .find(".js-button_add-res__form")
            .html();

        $("#dialog-form")
            .html(htmlForm);

        $("#dialog-form .js-accordion").accordion();

        dialog.dialog("open");
    });
    // "#dialog-form"
    // ".js-add-product"
    /*
        js-button_add-res__wrapp
        js-button_add-res__button
        js-button_add-res__form
    */

});
