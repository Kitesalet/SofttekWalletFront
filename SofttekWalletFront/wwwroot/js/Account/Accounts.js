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
        { data: 'type', title: 'Type' },
        {
            data: 'balance',
            title: 'Balance (in $)',
            render: function (data) {
                return '$' + data;
            }
        },
        {
           data: function (data) {
                var buttons =
                    `<td><a href='javascript:AccountInformation(${JSON.stringify(data)})'><i class="fa-solid fa-info-circle m-3 text-secondary"></i></a></td>` +
                    `<td><a href='javascript:DeleteAccount(${JSON.stringify(data)})'><i class="fa-solid fa-trash m-3 text-danger"></i></a></td>` +
                    `<td><a href='javascript:Withdraw(${JSON.stringify(data)})'><i class="fa-solid fa-arrow-up m-3 text-success"></i></a></td>` +
                    `<td><a href='javascript:Deposit(${JSON.stringify(data)})'><i class="fa-solid fa-arrow-down m-3 text-danger"></i></a></td>`;
                return buttons;
            },
            title: 'Basic Operations'
        }
    ]
});


function Deposit(data) {

    $.ajax({
        type: "GET",
        url: "DepositPartial",
        data: data,
        dataType: "html",
        success: function (result) {
            $("#AccountAddPartial").html(result);
            $('#depositModal').modal('show');
        }
    });

}

function Withdraw(data) {

    $.ajax({
        type: "GET",
        url: "ExtractPartial",
        data: data,
        dataType: "html",
        success: function (result) {
            $("#AccountAddPartial").html(result);
            $('#extractModal').modal('show');
        }
    });

}

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

