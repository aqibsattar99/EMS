$(document).ready(function () {
    $('#tblcat').DataTable(); // Initialize DataTable after everything is loaded
    $('#tbleqpt').DataTable(); // Initialize DataTable after everything is loaded
});

$(document).ready(function () {
$("#btn-cate-add").click(function () {
    var Name = $('#name').val();
    
    if (Name == "") {

        alert("Please enter the name!!!");

    } else {

        $("#btn-cate-add").text("Saving...");
        $.ajax({
            url: "/Category/Create",
            type: "POST",
            dataType: 'json',
            data: {
                Name: Name,
           
            },
            success: function (response) {
                // Clear Input Fields
                $("#name").val("");
                alert("Data Inserted");


                // Button Text Change
                $("#btn-cate-add").text("Save");

                loadData();
            },
            error: function (xhr, status, error) {
                alert("Server Error !!!");
            }
        });
    }

});



    function loadData() {
        $.ajax({
            url: '/category/GetCategories',
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                var table = $('#tblcat').DataTable(); // Initialize DataTable

                table.clear(); // Clear any existing data

                var i = 0;
                $.each(data, function (index, category) {
                    i++;
                    table.row.add([
                        i,
                        category.Name,
                        '<a href="/category/Edit/' + category.Id + '" class="btn btn-sm btn-warning">Edit</a> | ' +
                        '<a href="/category/Details/' + category.Id + '" class="btn btn-sm btn-info">Details</a> | ' +
                        '<a href="/category/Delete/' + category.Id + '" class="btn btn-sm btn-danger">Delete</a>'
                    ]).draw(false);
                });
            },
            error: function (xhr, status, error) {
                alert("An error occurred while loading data: " + error);
            }
        });
    }

    loadData();


});
