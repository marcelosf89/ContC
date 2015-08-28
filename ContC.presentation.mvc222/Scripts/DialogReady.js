
function mostrarAviso(outputMsg, isError) {

    var span = "";
    var titleMsg = "";
    var backGroundColor = "";
    var textColor = "";

    if (!isError) {
        titleMsg = "Informação";
        span = "<span class=\"glyphicon glyphicon-info-sign\" aria-hidden=\"true\"></span><span class=\"sr-only\">Info:</span>";
        backGroundColor = "#d9edf7";
        textColor = "#3a87ad";
    } else if (isError === "0") {
        titleMsg = "Sucesso";
        span = "<span class=\"glyphicon glyphicon-ok-sign\" aria-hidden=\"true\"></span><span class=\"sr-only\">Success:</span>";
        backGroundColor = "#dff0d8";
        textColor = "#468847";
    } else if (isError === "1") {
        titleMsg = "Erro";
        span = "<span class=\"glyphicon glyphicon-remove-sign\" aria-hidden=\"true\"></span><span class=\"sr-only\">Error:</span>";
        backGroundColor = "#f2dede";
        textColor = "#b94a48";
    }

    if (!outputMsg)
        outputMsg = "No Message to Display.";

    var msgFormatted = "<p>" + span + " " + outputMsg + "</p><p></p>";   
    $("<div></div>").html(msgFormatted).dialog({
        title: titleMsg,
        resizable: false,
        modal: true,
        maxWidth: 600,
        width: "auto",
        fluid: true,
        buttons: {
            "OK": function () {
                $(this).dialog("close");
            }
        }
    }).prev(".ui-dialog-titlebar").css("background", backGroundColor).css("color", textColor);
}

// on window resize run function
$(window).resize(function () {
    fluidDialog();
});

// catch dialog if opened within a viewport smaller than the dialog width
$(document).on("dialogopen", ".ui-dialog", function (event, ui) {
    fluidDialog();
});

function fluidDialog() {
    var $visible = $(".ui-dialog:visible");
    // each open dialog
    $visible.each(function () {
        var $this = $(this);
        var dialog = $this.find(".ui-dialog-content").data("ui-dialog");
        // if fluid option == true
        if (dialog.options.fluid) {
            var wWidth = $(window).width();
            // check window width against dialog width
            if (wWidth < (parseInt(dialog.options.maxWidth) + 50)) {
                // keep dialog from filling entire screen
                $this.css("max-width", "90%");
            } else {
                // fix maxWidth bug
                $this.css("max-width", dialog.options.maxWidth + "px");
            }
            //reposition dialog
            dialog.option("position", dialog.options.position);
        }
    });

}
