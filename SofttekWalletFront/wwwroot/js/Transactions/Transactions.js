var token = localStorage.getItem('token');
var id = localStorage.getItem('id');
document.getElementById('transactionSelect').addEventListener('change', FilterAccountChange);

    var table = new DataTable('#transactions', {
        paging: true,
        lengthMenu: [1, 5, 10, 20],
        pageLength: 10,
        ajax: {

            url: `https://localhost:7243/api/transactions`,
            dataSrc: "data",
            headers: { "Authorization": "Bearer " + token }
        },
        columns: [
            { data: 'type', title: 'Type' },
            { data: 'concept', title: 'Concept' },
            { data: 'amount', title: 'Amount' },
            { data: 'sourceAccount.cbu', title: 'Source CBU(If Fiduciary)' },
            { data: 'sourceAccount.uuid', title: 'Source UUID(If Crypto)' },
            { data: 'destinationAccount.cbu', title: 'Destination CBU (If Fiduciary)' },
            { data: 'destinationAccount.uuid', title: 'Destination UUID (If Crypto)' },
            { data: 'createdDate', title: 'Created Date' }

        ]
    });

FillAccountDropdown();

function FilterByAccount(account) {

        var oldTable = $('#transactions').DataTable()
        oldTable.destroy()

        let table = new DataTable('#transactions', {
            paging: true,
            lengthMenu: [1, 5, 10, 20],
            pageLength: 10,
            ajax: {

                url: `https://localhost:7243/api/transactions/account/${account}`,
                dataSrc: "data",
                headers: { "Authorization": "Bearer " + token }
            },
            columns: [
                { data: 'type', title: 'Type' },
                {
                    data: 'concept',
                    title: 'concept',
                    render: function (data, type) {
                        if (type === 'display' && data === '') {
                            return '-';
                        }
                        return data;
                    }
                },
                { data: 'amount', title: 'Amount' },
                {
                    data: 'sourceAccount.cbu',
                    title: 'Source CBU(If Fiduciary)',
                    render: function (data, type) {
                        if (type === 'display' && data === 0) {
                            return '-';
                        }
                        return data;
                    }
                },
                {
                    data: 'destinationAccount.cbu',
                    title: 'Destination CBU (If Fiduciary)',
                    render: function (data, type) {
                        if (type === 'display' && data === 0) {
                            return '-'; 
                        }
                        return data;
                    }
                },
                {
                    data: 'sourceAccount.uuid',
                    title: 'Source CBU(If Fiduciary)',
                    render: function (data, type) {
                        if (type === 'display' && data === null) {
                            return '-';
                        }
                        return data;
                    }
                },
                {
                    data: 'destinationAccount.uuid',
                    title: 'Destination CBU (If Fiduciary)',
                    render: function (data, type) {
                        if (type === 'display' && data === null) {
                            return '-';
                        }
                        return data;
                    }
                },
                { data: 'createdDate', title: 'Created Date' }

            ]
        });

    }



function FilterAccountChange() {
    const filterValue = parseInt(document.getElementById('transactionSelect').value);

    FilterByAccount(filterValue);
}

function FillAccountDropdown() {
    $.ajax({
        url: `https://localhost:7243/api/accounts/${id}`,
        method: "GET",
        dataType: 'html',
        headers: {
            'Authorization': 'Bearer ' + token
        },
        success: function (response) {
            var select = $('#transactionSelect');
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



