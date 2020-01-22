function getHtml() {
    var searchQuery = $("#searchTextbox").val();
    console.log(searchQuery);
    if (searchQuery == "") {
        searchQuery = "emptySearchQuery"
    }
    console.log(searchQuery);
    var url = `/librarySearch/${searchQuery}`
    $.ajax({
        url: url,
        type: "GET",
        success: function (result) {
            $("#divResult").html(result);
        }
    });
}