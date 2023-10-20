var token = localStorage.getItem('token');
var id = localStorage.getItem('id')


let table = new DataTable('#accounts', {
    paging: true,
    lengthMenu: [1, 5, 10, 20],
    pageLength: 5,
    ajax: {
        url: `https://localhost:7243/api/accounts/${id}`,
        dataSrc: "data",
        headers: { "Authorization": "Bearer " + token },
    },
    columns: [
        { data: 'id', title: 'Id' },
        { data: 'uuid', title: 'UUID' },
        { data: 'accountNumber', title: 'AccountNumber' },
        { data: 'cbu', title: 'CBU' },
        { data: 'alias', title: 'Alias' },
        { data: 'type', title: 'Type' },
        {
            data: function (data) {
                var buttons =
                    `<td><a href='javascript:UpdateProject(${JSON.stringify(data)})'><i class="fa-solid fa-pen-to-square m-3 updateProject"></i></a></td>` +
                    `<td><a href='javascript:DeleteProject(${JSON.stringify(data)})'><i class="fa-solid fa-trash deleteProject"></i></a></td>`;
                return buttons;
            }
        }
    ]
});


function DeleteAccount(data) {
    if (window.confirm("Are you sure you want to delete this account?")) {
        $.ajax({
            type: "GET",
            url: "DeleteAccount",
            data: data,
            'dataType': "html",
            success: function (result) {
                location.reload()
            },
        })
    } else {
        alert("The account wasnt deleted!")
    }

}

function CreateAccountDollar(data) {
    if (window.confirm("Are you sure you want to create a Dollar Account?")) {
        var data = {
            ClientId: id,
            Type: 2
        };
        $.ajax({
            type: "GET",
            url: "CreateAccountDollar",
            data: data,
            'dataType': "html",
            success: function (result) {
                location.reload()
            },
        })
    } else {
        alert("The Dollar account wasnt created!")
    }

}

function CreateAccountCrypto() {
    if (window.confirm("Are you sure you want to create a Crypto Account?")) {
        var data = {
            ClientId: id,
            Type: 3
        };
        $.ajax({
            type: "GET",
            url: "CreateAccountCrypto",
            data: data,
            dataType: "html",
            success: function (result) {
                location.reload();
            },
        });
    } else {
        alert("The Crypto account wasn't created!");
    }
}

function CreateAccountPeso() {
    if (window.confirm("Are you sure you want to create a Peso Account?")) {
        var data = {
            ClientId: id,
            Type: 1
        };
        $.ajax({
            type: "GET",
            url: "CreateAccountPeso",
            data: data,
            dataType: "html",
            success: function (result) {
                location.reload();
            },
        });
    } else {
        alert("The Peso account wasn't created!");
    }
}

function AccountInformation(data) {

        $.ajax({
            type: "GET",
            url: "AccountAddPartial",
            data: data,
            dataType: "html",
            success: function (result) {
                $("#AccountAddPartial").html(result);
                $('#accountModal').modal('show');
            }
        });

}

