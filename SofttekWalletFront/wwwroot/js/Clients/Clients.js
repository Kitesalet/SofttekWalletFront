var token = localStorage.getItem('token');
console.log(token)

var type = localStorage.getItem('type')


let table = new DataTable('#clients', {
    paging: true, 
    lengthMenu: [1, 5, 10, 20], 
    pageLength: 5,
    ajax: {

        url: `https://localhost:7147/api/clients?page=1&units=1000`,
        dataSrc: "data",
        headers: { "Authorization": "Bearer " + token }
    },
    columns: [
        { data: 'id', title: 'Id' },
        { data: 'name', title: 'Name' },
        { data: 'email', title: 'Email' },
        { data: 'type', title: 'Tipo' },
        {
            data: function (data) {
                var buttons =	
                    `<td><a href='javascript:UpdateClient(${JSON.stringify(data)})'><i class="fa-solid fa-pen-to-square m-3 updateClient"></i></a></td>` +
                    `<td><a href='javascript:DeleteClient(${JSON.stringify(data)})'><i class="fa-solid fa-trash deleteClient"></i></a></td>`;
                return buttons;
            }
        }
    ]
});

function DeleteClient(data) {

    if (window.confirm("Are you sure you want to delete the client?"))
    {
       $.ajax({
            type: "GET",
            url: "DeleteClient",
            data: data,
            'dataType': "html",
           success: function (result) {
               location.reload()
            },
       })
    } else {
        alert("The client has not been deleted!")
    }

}
function UpdateClient(data)
{


        $.ajax({
            type: "GET",
            url: "ClientsAddPartial",
            data: data,
            dataType: "html",
            success: function (result) {
                $("#ClientsAddPartial").html(result);
                $('#clientModal').modal('show');
            }
        )
    
}


function AddClient() {

    
        $.ajax({
            type: "GET",
            url: "ClientsAddPartial",
            'dataType': "html",
            success: function (result) {
                $("#ClientsAddPartial").html(result);
                $('#clientModal').modal('show');
            }
        })
    
}