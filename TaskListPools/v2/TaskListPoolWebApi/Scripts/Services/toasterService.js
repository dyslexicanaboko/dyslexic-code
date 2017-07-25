toastr.options = {
    "closeButton": true,
    "debug": false,
    "newestOnTop": true,
    "progressBar": false,
    "positionClass": "toast-top-center",
    "preventDuplicates": false,
    "onclick": null,
    "showDuration": "300",
    "hideDuration": "1000",
    "timeOut": "5000",
    "extendedTimeOut": "1000",
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
};

var toastMessages = {
    success: function (message) {
        toastr["success"](message, "Success");
    },
    error: function (message) {
        console.log(message);

        __errorMessage(message);
    },
    errorHttp: function (response) {
        var msg = null;

        //console.dir(response);
        if (response.data !== undefined && response.data.ExceptionMessage !== undefined)
            msg = response.data.ExceptionMessage;
        else if (response.message !== undefined)
            msg = response.message;
        else
            msg = response;

        __errorMessage(msg);
    }
};

function __errorMessage(message) {
    toastr["error"](message, "Error");
}

