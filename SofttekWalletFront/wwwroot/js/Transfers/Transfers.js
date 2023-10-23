var token = localStorage.getItem('token');
var id = localStorage.getItem('id');

$(document).ready(function () {

    FillOriginDropdown();
    FillDestinationDropdown();
})

function FillDestinationDropdown() {
    $.ajax({
        url: `https://localhost:7243/api/accounts/${id}`,
        method: "GET",
        dataType: 'html',
        headers: {
            'Authorization': 'Bearer ' + token
        },
        success: function (response)
        {
            var select = $('#originAccountSelect');
            select.empty();

            var parsed = JSON.parse(response);
            parsed.data.forEach(function (item) {

                if (item.cbu) {
                    text ='Type - ' + item.type + ' | CBU - ' + item.cbu + ' | Balance - ' + item.balance
                } else {
                    text = 'Type - ' + item.type + ' | UUID - ' + item.uuid + ' | Balance - ' + item.balance
                }

                select.append($('<option>', {
                    value: item.id,
                    text: text
                }))
            }
            )
        }
    }
    )
}

function TransferSubmitHabdler() {

}
function FillOriginDropdown() {
    $.ajax({
        url: `https://localhost:7243/api/accounts/${id}`,
        method: "GET",
        dataType: 'html',
        dataSrc: 'data',
        headers: { "Authorization": "Bearer " + token },
        success: function (response) {
            var select = $('#destinationAccountSelect');
            select.empty();

            var parsed = JSON.parse(response);
            parsed.data.forEach(function (item) {

                if (item.cbu) {
                    text = 'Type - ' + item.type + ' | CBU - ' + item.cbu + ' | Balance - ' + item.balance
                } else {
                    text = 'Type - ' + item.type + ' | UUID - ' + item.uuid + ' | Balance - ' + item.balance
                }

                select.append($('<option>', {
                    value: item.id,
                    text: text
                }))
            }
            )
        }
    }
    )
}


