var token = localStorage.getItem('token');

var decodedToken = parseJwt(token);

function parseJwt(token) {
    var base64Url = token.split('.')[1];
    var base64 = base64Url.replace('-', '+').replace('_', '/');
    return JSON.parse(window.atob(base64));
}

var id = decodedToken.nameIdentifier;


let table = new DataTable('#accounts', {
    paging: true,
    lengthMenu: [1, 5, 10, 20],
    pageLength: 5,
    ajax: {

        url: `https://localhost:7147/api/accounts/id`,
        dataSrc: "data",
        headers: { "Authorization": "Bearer " + token }
    },
    columns: [
        { data: 'id', title: 'Id' },
        { data: 'type', title: 'Type' },
        { data: 'balance', title: 'Balance' },
        {
            data: function (data) {
                var buttons =
                    `<td><a href='javascript:DeleteAccount(${JSON.stringify(data)})'><i class="fa-solid fa-trash deleteAccount"></i></a></td>`;
                    `<td><a href='javascript:AccountInformation(${JSON.stringify(data)})'><i class="fa-solid fa-info m-3 accountInformation"></i>Account Information</a></td>`;
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

