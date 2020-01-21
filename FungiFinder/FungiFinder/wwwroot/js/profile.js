

function editInfo(id) {
    console.log('hej');
    $.ajax({
        url: '/profile/edit/' + id,
        type: 'GET',
        success: function (result) {
            $("#emailPartial").html(result)
        }
    });




}