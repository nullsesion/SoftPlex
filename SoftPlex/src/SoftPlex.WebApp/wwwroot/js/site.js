// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
/*
	jQuery(function () {
		jQuery(".js-button_add").click(function () {
	        //".js-add-edit-product"
            jQuery( "#dialog-confirm" ).dialog({
                  resizable: true,
                  height: "auto",
                  width: 400,
                  modal: true,
                  buttons: {
                    "Delete all items": function() {
                          jQuery( this ).dialog( "close" );
                    },
                    Cancel: function() {
                        jQuery( this ).dialog( "close" );
                    }
                  }
                });
		});
    });
*/
$(function () {
    var dialog, form,

    // From https://html.spec.whatwg.org/multipage/input.html#e-mail-state-%28type=email%29
    emailRegex = /^[a-zA-Z0-9.!#$%&'*+\/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$/,
    name      = $("#name"),
    email     = $("#email"),
    password  = $("#password"),
    allFields = $([]).add(name).add(email).add(password),
    tips      = $(".validateTips");

    function updateTips(t) {
        tips
            .text(t)
            .addClass("ui-state-highlight");
        setTimeout(function () {
            tips.removeClass("ui-state-highlight", 1500);
        }, 500);
    }

    function checkLength(o, n, min, max) {
        if (o.val().length > max || o.val().length < min) {
            o.addClass("ui-state-error");
            updateTips("Length of " + n + " must be between " +
                min + " and " + max + ".");
            return false;
        } else {
            return true;
        }
    }

    function checkRegexp(o, regexp, n) {
        if (!(regexp.test(o.val()))) {
            o.addClass("ui-state-error");
            updateTips(n);
            return false;
        } else {
            return true;
        }
    }

    function addProduct() {
        alert("blablabla");
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
            form[0].reset();
            allFields.removeClass("ui-state-error");
        }
    });

    form = dialog.find("form.js-add-product").on("submit", function (event) {
        event.preventDefault();
        addProduct();
    });

    $(".js-button_add").on("click", function () {
        dialog.dialog("open");
    });

    $(".js-accordion").accordion();
});
