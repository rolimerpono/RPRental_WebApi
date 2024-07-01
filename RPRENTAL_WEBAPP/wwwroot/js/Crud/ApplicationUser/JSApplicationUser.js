

$(document).ready(function () {
    $('.btn-login').click(function () {             
        LoginUser('/ApplicationUser/Login', '#login-form');
    });

    $('.btn-register').click(function () {
        RegisterUser('/ApplicationUser/Register', '#register-form');
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
                    window.location.href = '/Dashboard';
                }
                else {
                    window.location.href = '/Home'
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

function RegisterUser(url, formSelector) {
    

    let is_true = false;

    is_true = IsFieldValid(formSelector);


    if (!is_true) {
        return;
    }

  

    let fullname = $('#FullName').val();
    let email = $('#Email').val();
    let phonenumber = $('#PhoneNumber').val();
    let username = $('#UserName').val();
    let password = $('#Password').val();
    let conpassword = $('#ConPassword').val();



    let data = { FullName: fullname, Email: email, PhoneNumber: phonenumber, UserName: username, Password: password, ConfirmPassword: conpassword };

    $.ajax({
        url: url,
        type: 'POST',
        data: data,
        success: function (response) {
            if (response.success) {    

                if (response.role == 'Admin') {
                    window.location.href = '/Dashboard';
                }
                else {
                    window.location.href = '/Home'
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