﻿function uploadFile(inputId) {
    let fileUpload = $("#" + inputId).get(0);
    let files = fileUpload.files;
    let formData = new FormData();
    formData.append('file', files[0]);

    $.ajax({
        url: 'Image/File',
        type: 'POST',
        data: formData,
        processData: false,
        contentType: false,
        success: function (result) {
            $("#result").html(result)
        }
    })

}

function getResult() {

    let value = $("#picture").val().toString();
    let temp = value.split("\\");
    let url = temp[temp.length - 1];
  
    
    $.ajax({
        url: "Image/GetPartial/" + url,
        type: 'GET',
        success: function (result) {
            $("#resultHere").html(result)
        }
    })
}

