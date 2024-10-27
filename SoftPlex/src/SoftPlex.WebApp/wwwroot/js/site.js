// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
// Write your JavaScript code.

$(function () {
    var dialog, form;

    function addProduct() {
        $('.error_show_messages_all').hide();
        $(".error_show_messages_all").html("");
        $('.error_show').hide();

        var ProductVersions = [];

        var name_product_id = $("#dialog-form")
            .find('[name="name_product_id"]').val();

        $("#dialog-form")
            .find(".js-product-version")
            .each(function (i, e) {
                ProductVersions.push({
                    "id": $(e).find('[name="productversion_guid"]').val(), 
                    "productId": name_product_id,
                    "name": $(e).find('[name="name"]').val(),
                    "description": $(e).find('[name="description"]').val(),
                    "width": $(e).find('[name="width"]').val() * 1,
                    "height": $(e).find('[name="height"]').val() * 1,
                    "length": $(e).find('[name="length"]').val() * 1,
                });
            });
        var d = JSON.stringify({
            "id": name_product_id,
            "name": $("#dialog-form").find('[name="name_product"]').val(),
            "description": $("#dialog-form").find('[name="description_product"]').val(),
            "ProductVersions": ProductVersions
        });

        var settings = {
            "url": "/Product/CreateProduct",
            "method": "POST",
            "timeout": 0,
            "beforeSend": function () {
                $('.error_show_messages_all').hide();
                $(".error_show_messages_all").html("");
                $('.error_show').hide();
            },
            "headers": {
                "Content-Type": "application/json",
            },
            "data": d,
        };

        //console.log(settings)
   
        $.ajax(settings).done(function (response) {
            //add добавление ошибок
            console.log(response);
            console.log(response.isError);
            if (response.isError) {

                response.errors.forEach((element) => {
                    console.log('.error_show_' + element.entityId);
                    $('.error_show_' + element.entityId).show();
                    $('.error_show_messages_' + element.entityId)
                        .append("<div>" + element.invalidField + ": " + element.message + "</div>")
                        .show();
                });
            } else {
                window.location.href = response.url;
            }

        });
    }

    dialog = $("#dialog-form").dialog({
        autoOpen: false,
        height: 700,
        width: 900,
        modal: true,
        buttons: {
            "Add/Update Product": addProduct,
            Cancel: function () {
                dialog.dialog("close");
            }
        },
        close: function () {
            $("#js-add-product").trigger("reset");
            //allFields.removeClass("ui-state-error");
            dialog.dialog("close");
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
});
