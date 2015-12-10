
// Make sure document is loaded before doing anything. Almost always first, with rest of code nested inside.
$(document).ready(function () {
    // This is a jQuery selector. Sets onclick listener.
    $(".dataRow").click(function (firedEvent) {
        console.log("The event fired.");
        // document.location = "/cars/edit/*idhere*"
    });
});


