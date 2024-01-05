$(document).ready(function() {
    $("#submitBtn").click(function(e){
        e.preventDefault();
        var name =  $("#name").val(), email = $("#email").val(),
            contact = $("#contact").val(), profile = $("#profile").val();

        $.post("https://reqres.in/api/users",{
            name: name,
            email: email,
            contact: contact,
            profile: profile
        }, function(response) {
            $("#response").html(`<div class="alert alert-success">${response.name}</div>`);
            clearFields();
        })
    });

    $("#clearBtn").click(function(e){
        e.preventDefault();
        clearFields();
    });


    function clearFields() {
        $("#name").val("");
        $("#email").val("");
        $("#contact").val("");
        $("#profile").val("");
    }
})