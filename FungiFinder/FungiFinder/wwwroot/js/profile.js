let testBtn = document.getElementById("editEmail");


function changeInfo(id) {

    $.ajax({
        url: "profile/edit/" + id,
        type: "GET",
        success: function (result) {
            $("#emailPartial").html(result);
        }
    });




}