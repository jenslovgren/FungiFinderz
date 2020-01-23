let isActiveMail = true;

function editEmail() {
    console.log('hej');
    if (isActiveMail) {
        $.ajax({
            url: '/profile/edit/email',
            type: 'GET',
            success: function (result) {
                $("#emailPartial").html(result)
            }
        });
        isActiveMail = !isActiveMail;
        $("#editEmail").css("background-color", "#17a2b8");
    }
    else {
        $("#emailPartial").empty();
        $("#editEmail").css("background-color", "white");
        isActiveMail = !isActiveMail;
    }

}
let isActivePass = true;

function editPassword() {
    if (isActivePass) {
        $.ajax({
            url: '/profile/edit/password',
            success: function (result) {
                $("#pwPartial").html(result)
            }
        })
        isActivePass = !isActivePass;
        $("#editPassword").css("background-color", "#17a2b8");
    }
    else {
        $("#pwPartial").empty();
        $("#editPassword").css("background-color", "white");
        isActivePass = !isActivePass;
    }
}


