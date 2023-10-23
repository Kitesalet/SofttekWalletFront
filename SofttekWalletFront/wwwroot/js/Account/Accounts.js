var token = localStorage.getItem('token');
var id = localStorage.getItem('id')


let table = new DataTable('#accounts', {
    paging: true,
    lengthMenu: [1, 5, 10, 20],
    pageLength: 10,
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
    Swal.fire({
        title: 'You will delete this account forever!',
        text: 'Do you want to delete this account?',
        icon: 'question',
        showCancelButton: true,
        confirmButtonText: 'Yes',
        cancelButtonText: 'No'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: 'GET',
                url: 'DeleteAccount',
                data: data,
                dataType: 'html',
                success: function (response) {
                    Swal.fire('Success', `${response} Page will reload...`, 'success');
                    setTimeout(function () {
                        location.reload();
                    }, 2000);
                },
                error: function () {
                    Swal.fire('Error', 'Failed to delete the account', 'error');
                }
            });
        } else {
            Swal.fire('Cancelled', 'The account was not deleted', 'info');
        }
    });
}

function CreateAccountDollar() {
    Swal.fire({
        title: 'You will create an account',
        text: 'Do you want to create a Dollar Account?',
        icon: 'question',
        showCancelButton: true,
        confirmButtonText: 'Yes',
        cancelButtonText: 'No'
    }).then((result) => {
        if (result.isConfirmed) {
            var data = {
                ClientId: id,
                Type: 2
            };
            $.ajax({
                type: 'GET',
                url: 'CreateAccountDollar',
                data: data,
                dataType: 'html',
                success: function (response) {
                    Swal.fire('Success', `${response} Page will reload`, 'success');
                    setTimeout(function () {
                        location.reload();
                    }, 3000);
                },
                error: function (response) { 
                    Swal.fire('Error', 'Failed to create Dollar account', 'error');
                    setTimeout(function () {
                        location.reload();
                    }, 1600);
                }
            });
        } else {
            Swal.fire('Cancelled', 'The Dollar account was not created', 'info');
        }
    });
}
function CreateAccountCrypto() {
    Swal.fire({
        title: 'You will create an account',
        text: 'Do you want to create a Crypto Account?',
        icon: 'question',
        showCancelButton: true,
        confirmButtonText: 'Yes',
        cancelButtonText: 'No'
    }).then((result) => {
        if (result.isConfirmed) {
            var data = {
                ClientId: id,
                Type: 2
            };
            $.ajax({
                type: 'GET',
                url: 'CreateAccountCrypto',
                data: data,
                dataType: 'html',
                success: function (response) {
                    Swal.fire('Success', `${response} Page will reload`, 'success');
                    setTimeout(function () {
                        location.reload();
                    }, 1600);
                },
                error: function (response) {
                    Swal.fire('Error', 'Failed to create Crypto account', 'error');
                    setTimeout(function () {
                        location.reload();
                    }, 2000);
                }
            });
        } else {
            Swal.fire('Cancelled', 'The Crypto account was not created', 'info');
        }
    });
}

function CreateAccountPeso() {
    Swal.fire({
        title: 'You will create an account',
        text: 'Do you want to create a Peso Account?',
        icon: 'question',
        showCancelButton: true,
        confirmButtonText: 'Yes',
        cancelButtonText: 'No'
    }).then((result) => {
        if (result.isConfirmed) {
            var data = {
                ClientId: id,
                Type: 2
            };
            $.ajax({
                type: 'GET',
                url: 'CreateAccountPeso',
                data: data,
                dataType: 'html',
                success: function (response) {
                    Swal.fire('Success', `${response} Page will reload`, 'success');
                    setTimeout(function () {
                        location.reload();
                    }, 3000);
                },
                error: function (response) {
                    Swal.fire('Error', 'Failed to create Peso account', 'error');
                    setTimeout(function () {
                        location.reload();
                    }, 1600);
                }
            });
        } else {
            Swal.fire('Cancelled', 'The Peso account was not created', 'info');
        }
    });
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

