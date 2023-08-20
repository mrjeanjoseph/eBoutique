//Project incomplete. We did not get to implement the full CRUD Operation. We're still missing the Edit and Delete Click operation.

var errMsg = "An error has occur. Please see check the console";
$(document).ready(function () {
    LoadProductData();
});

function EditClick(ProductId) {
    alert(ProductId);
}

function DeleteClick(ProductId) {
    alert(ProductId);
}

function SaveProduct() {
    var ProductModel = {
        ProductId: 0,
        Code: $('#txtCode').val(),
        Name: $('#txtName').val(),
        Category: $('#txtCategory').val(),
        Barcode: $('#txtBarcode').val(),
        StockQty: $('#txtStockQty').val(),
    };
    console.log(JSON.stringify(ProductModel));

    $.ajax({
        //async: true,
        type: 'POST',
        url: '/Product/PostProductDetails',
        data: JSON.stringify(ProductModel),
        dataType: 'JSON',
        contentType: 'application/json;charset=utf-8',
        success: function (result) {
            console.log("in the Success logic")
            if (result.IsValid == true) {
                alert(result.Message)
                LoadProductData();
            }
            else {
                alert(errMsg);
                console.log(result.ErrorMessage);
            }

        },
        error: function (result) {
            alert(errMsg);
            console.log(result)
        }
    });
}

function LoadProductData() {
    $.ajax({
        async: true,
        url: '/Product/GetProductDetails',
        type: 'GET',
        contentType: 'application/json;charset=utf-8',
        dataType: 'JSON',
        success: function (result) {
            var productData = '';
            $.each(result, function (key, value) {
                productData += '<tr>';
                productData += `<td>${value.ProductId}</td>`;
                productData += `<td>${value.Code}</td>`;
                productData += `<td>${value.Name}</td>`;
                productData += `<td>${value.Category}</td>`;
                productData += `<td>${value.Barcode}</td>`;
                productData += `<td>${value.StockQty}</td>`;
                productData += `
                            <td class="text-center">
                                <input type="button" class="btn btn-warning" value="Edit" onclick="EditClick(${value.ProductId})"/>
                                <input type="button" class="btn btn-danger" value="Delete" onclick="DeleteClick(${value.ProductId})"/>
                            </td>`;
                productData += '</tr>';
            });
            $('.loadProduct').html(productData);
        },
        error: function (result) {
            alert(errMsg);
            console.log(result);
        }
    });
}

console.log("logic moved");