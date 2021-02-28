function Display(url) {
    $.get(url,
        function (data) {
            $(".modal-body").html(data);
        }).done(function () {
            $("#myModal").modal('show');
        }).fail(function () {
            alert("Failed to load! Please contact the Administrator!");
        });
}