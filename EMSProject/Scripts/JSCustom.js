$(document).ready(function () {
    $('#tblcat').DataTable(); // Initialize DataTable after everything is loaded
    $('#tbleqpt').DataTable(); // Initialize DataTable after everything is loaded


    $('#loadingModal').modal('show');
});

$(document).ready(function () {
$("#btn-cate-add").click(function () {
    var Name = $('#namecat').val();
    
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
                $("#namecat").val("");
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
                        '<a href="#" data-id="' + category.Id + '" data-name="' + category.Name + '" class="btn btn-sm btn-warning cat-edit-btn">Edit</a> | ' +
                        '<a href="/category/Delete/' + category.Id + '" class="btn btn-sm btn-danger">Delete</a>'
                    ]).draw(false);
                });

                // Event delegation to handle click events on dynamically added buttons
                $('#tblcat').on('click', '.cat-edit-btn', function (e) {
                    e.preventDefault(); // Prevent default link behavior

                    var id = $(this).data('id'); // Get ID from data attribute
                    var name = $(this).data('name'); // Get Name from data attribute

                    // Populate the modal with data
                    $("#modal-cate-edit #id").val(id);
                    $('#modal-cate-edit #namecat').val(name);

                    // Load Modal
                    $('#modal-cate-edit').modal('show');
                });
            },
            error: function (xhr, status, error) {
                // Display an alert with the error details
                alert("Error: " + error);
            }
        });

    }



    loadData();


    $("#btn-cate-edit").click(function () {
        var Id = $('#modal-cate-edit #id').val();
        var Name = $('#modal-cate-edit #namecat').val();
      
        if (Name == "") {
            alert("Enter Category Name");

        } else {

            $("#btn-cate-edit").text("Updating...");
            $.ajax({
                url: "/category/Update",
                type: "POST",
                dataType: 'json',
                data: {
                    id: Id,
                    name: Name,
                  
                },
                success: function (response) {
                    // Clear Inputs
                    $("#modal-cate-edit input").val("");
                    $("#modal-cate-edit select").val("");
                    $("#modal-cate-edit textarea").val("");

                    alert("Successfully Updated!!!");

                    // Button Text Change
                    $("#btn-cate-edit").text("Update");

                    setTimeout(function () {
                        $("#modal-cate-edit").modal('hide');
                    }, 1500);
                    loadData();

                },
                error: function (xhr, status, error) {
                    alert(error);
                }
            });
        }
    });


});
