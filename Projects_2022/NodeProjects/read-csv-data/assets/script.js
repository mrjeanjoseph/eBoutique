// This is the frond end

// $(() =>{
//     console.log( "ready!" );
// })

$(document).ready(function () {
    $("form").submit(function (e) {
        e.preventDefault();
        var mytext = $("#mytext").val();
        $.ajax({
            url: "/ajaxdemo",
            data: { text: mytext },
            method: "POST",
            contentType: "application/x-www-form-urlencoded",
            success: function (res) {
                alert(res.from)
            }, error: function (err) {
                console.log(err);
            }
        })
    })
})