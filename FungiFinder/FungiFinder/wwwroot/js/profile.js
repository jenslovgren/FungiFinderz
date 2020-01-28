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

function alternativePicture() {
    document.getElementById("profilePic").src = '../Images/UserPics/happyShroom.png';


}

function changeProfilePic(inputId) {
    let fileUpload = $("#" + inputId).get(0);
    let files = fileUpload.files;
    let formData = new FormData();
   
  
    formData.append('profilePic', files[0]);
    $.ajax({
        url: '/profile/changepicture',
        type: 'POST',
        data: formData,
        processData: false,
        contentType: false,
        success: function (result) {
            console.log("jaa");
            $("#pictureErrorHere").text("");
            $("#profilePic").attr("src", "/Images/UserPics/" + result);
        },
        error: function (result) {
            $("#pictureErrorHere").text("Bilden måste vara i filformatet .jpeg");
        }
    })
}



let isActiveMush = true;

function editFavMushroom() {
    if (isActiveMush) {
        $.ajax({
            url: "profile/edit/favoritemushroom",
            type: 'GET',
            success: function (result) {

            }

        })
    }

}



