//Load Data in Table when documents is ready

var mainUrl = '/HumanResources/HaitiEmployee';
$(document).ready(function () {
    loadData();
});

//Load Data function  
function loadData() {

    $.ajax({
        url: `${mainUrl}/List`,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (result) {
            var empRecord = '';
            $.each(result, function (key, item) {
                empRecord += `
                <tr>
                    <td>${item.EmployeeID}</td>
                    <td>${item.EmployeeName}</td>
                    <td>${item.EmployeeAge}</td>
                    <td>${item.State}</td>
                    <td>${item.Country}</td>;
                    <td class="text-center">
                        <a type="submit" class="btn btn-warning btn-small" onclick=getbyID(${item.EmployeeID})>Edit</a>
                        <a type="submit" class="btn btn-danger btn-small" onclick=Delele(${item.EmployeeID})>Delete</a>
                    </td>
                </tr>`;
            });
            $('.tbody').html(empRecord);
        },
        error: function (data) {
            $.notify(data.error, {
                globalPosition: "top right",
                className: "danger"
            }); 
            console.log(data.responseText);
        }
    });
}

//Add Data Function   
function Add() {
    var res = validate();
    if (res == false)
        return false;    

    var empObj = {
        EmployeeID: $('#EmployeeID').val(),
        EmployeeName: $('#EmployeeName').val(),
        EmployeeAge: $('#EmployeeAge').val(),
        State: $('#State').val(),
        Country: $('#Country').val()
    };

    $.ajax({
        url: `${mainUrl}/Add`,
        type: "POST",
        dataType: "JSON",
        contentType: "application/json;charset=utf-8",
        data: JSON.stringify(empObj),
        success: function (result) {
            console.log("in the success")
            loadData();
            $('#myModal').modal('hide');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });

}

//Function for getting the Data Based upon Employee ID  
function getbyID(EmpID) {

    $('#EmployeeName, #EmployeeAge, #State, #Country').css('border-color', 'lightgrey');

    $.ajax({
        url: `${mainUrl}/GetbyID/${EmpID}`,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "JSON",
        success: function (result) {
            $('#EmployeeID').val(result.EmployeeID);
            $('#EmployeeName').val(result.EmployeeName);
            $('#EmployeeAge').val(result.EmployeeAge);
            $('#State').val(result.State);
            $('#Country').val(result.Country);

            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });

    return false;
}

//function for updating employee's record  
function Update() {

    var res = validate();

    if (res == false)
        return false;
    
    var empObj = {
        EmployeeID: $('#EmployeeID').val(),
        EmployeeName: $('#EmployeeName').val(),
        EmployeeAge: $('#EmployeeAge').val(),
        State: $('#State').val(),
        Country: $('#Country').val(),
    };

    $.ajax({
        url: `${mainUrl}/Update`,
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
            $('#EmployeeID').val("");
            $('#EmployeeName').val("");
            $('#EmployeeAge').val("");
            $('#State').val("");
            $('#Country').val("");
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

//function for deleting employee's record  
function Delete(Id) {
    var ans = confirm("Are you sure you want to delete this Record?");

    if (ans) {
        $.ajax({
            //url: "/HaitiEmployee/Delete/" + ID,
            url: `${mainUrl}/Delete/${Id}`,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                loadData();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }

}

//Function for clearing the textboxes  
function clearTextBox() {

    $('#EmployeeID').val("");
    $('#EmployeeName').val("");
    $('#EmployeeAge').val("");
    $('#State').val("");
    $('#Country').val("");

    $('#btnUpdate').hide();
    $('#btnAdd').show();

    $('#EmployeeName,#EmployeeAge,#State,#Country').css('border-color', 'lightgrey');
}
//Valdidation using jquery  
function validate() {

    var isValid = true;
    if ($('#EmployeeName').val().trim() == "") {
        $('#EmployeeName').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#EmployeeName').css('border-color', 'lightgrey');
    }

    if ($('#EmployeeAge').val().trim() == "") {
        $('#EmployeeAge').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#EmployeeAge').css('border-color', 'lightgrey');
    }

    if ($('#State').val().trim() == "") {
        $('#State').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#State').css('border-color', 'lightgrey');
    }

    if ($('#Country').val().trim() == "") {
        $('#Country').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Country').css('border-color', 'lightgrey');
    }

    return isValid;
}