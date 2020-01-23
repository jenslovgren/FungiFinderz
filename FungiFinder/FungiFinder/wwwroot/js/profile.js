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
    }
    else {
        $("#emailPartial").html("");
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
    }
    else {
        $("#pwPartial").html("");
        isActivePass = !isActivePass;
    }
}


