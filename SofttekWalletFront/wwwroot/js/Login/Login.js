
function RegisterHandler() {
    Swal.fire({
        title: 'You will register a user',
        text: 'Do you want to proceed with the registration?',
        icon: 'question',
        showCancelButton: true,
        confirmButtonText: 'Yes',
        cancelButtonText: 'No'
    }).then((result) => {
        if (result.isConfirmed) {
            var form = document.getElementById('registerForm');

            $.ajax({
                type: 'POST',
                url: 'CreateClient',
                data: $(form).serialize(),
                success: function (response) {
                    Swal.fire('Success', response + ' Going back to the login screen...', 'success');
                    setTimeout(function () {
                        window.location.href = 'https://localhost:7071/'; 
                    }, 2000);
                },
                error: function (response) {
                    Swal.fire('Error', response.responseText, 'error');
                }
            });
        }
    });
}

(function ($) {

	"use strict";

	var fullHeight = function() {

		$('.js-fullheight').css('height', $(window).height());
		$(window).resize(function(){
			$('.js-fullheight').css('height', $(window).height());
		});

	};
	fullHeight();

	$(".toggle-password").click(function() {

	  $(this).toggleClass("fa-eye fa-eye-slash");
	  var input = $($(this).attr("toggle"));
	  if (input.attr("type") == "password") {
	    input.attr("type", "text");
	  } else {
	    input.attr("type", "password");
	  }
	});

})(jQuery);
