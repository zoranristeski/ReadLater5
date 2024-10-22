// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$("#create-bookmark-btn").click(function (e) {
    e.preventDefault();
    var url = $("#Bookmark_URL").val();
    var shortDescription = $("#Bookmark_ShortDescription").val();
    var selectedCategoryID = $("#SelectedCategoryID").val();
    if (selectedCategoryID == "") {
        selectedCategoryID = 0;
    }
    var newCategory = $("#NewCategory").val();
    let bookmarkViewModel = {
        Bookmark: 
            {
                URL: url,
                ShortDescription: shortDescription,
                CategoryId: selectedCategoryID
        },
        SelectedCategoryID: selectedCategoryID, 
        NewCategory: newCategory
    }
    $.ajax({
        url: '/Bookmarks/Create',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(bookmarkViewModel),
        headers: {
            'RequestVerificationToken' : $('input[name="__RequestVerificationToken"]').val()
        },

        success: function (data) {
            //console.log(data.Bookmark.NewCategory + " - " )
            if (newCategory != "") {
                $("#SelectedCategoryID").append(new Option($("#NewCategory").val(), parseInt($('#SelectedCategoryID option:last-child').val()) + 1));
            }
            $("#Bookmark_URL").val('');
            $("#Bookmark_ShortDescription").val('');
            $("#SelectedCategoryID").val(0);
            $("#NewCategory").val('');
        },
        error: function (xhr) {
            console.log(xhr.responseText);
        }
    });
});



