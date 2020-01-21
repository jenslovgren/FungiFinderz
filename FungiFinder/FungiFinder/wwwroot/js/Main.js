function uploadFile(inputId) {
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