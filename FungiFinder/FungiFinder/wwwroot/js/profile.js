

function editInfo(id, partial) {
    console.log('hej');
    $.ajax({
        url: '/profile/edit/' + id,
        type: 'GET',
        success: function (result) {
            $("#" + partial).html(result)
        }
    });

}