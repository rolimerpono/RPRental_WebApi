

$(document).ready(function () {
    $('.btn-login').click(function () {             
        LoginUser('/ApplicationUser/Login', '#login-form');
    });

});


function LoginUser(url, formSelector) {   

    let is_true = false;

    is_true = IsFieldValid(formSelector);
        

    if (!is_true) {
        return;
    }

    let username = $('#username').val();
    let password = $('#password').val();
    let token = $('input[name="__RequestVerificationToken"]').val();
   

    let data = { Username: username, Password: password, __RequestVerificationToken : token };    

    $.ajax({
        url: url,
        type: 'POST',
        data: data,
        success: function (response) {
            if (response.success) {
                localStorage.setItem('loginTriggered', true);
                localStorage.setItem('loginMsg', response.message);

                if (response.role == 'Admin') {
                    window.location.href = '/Dashboard/Index';
                }
                else {
                    window.location.href = '/Home/Index'
                }

            }
            else {

                ShowToaster('error', 'LOGIN USER', response.message);
            }
        },
        error: function (xhr, status, error) {
            ShowToaster('error', 'LOGIN USER', response.message);
        }
    });
}