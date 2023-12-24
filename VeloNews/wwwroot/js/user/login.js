$(document).ready(function () {
    const url = 'http://localhost:5109/api/authentication/GetLoginUsers';

    $.ajax({
        url: url,
        type: 'GET',
        success: function (data) {

            let loginUsersList = $(".login-user-list");
            $.each(data, function (key, val) {
                loginUsersList.append("<div>Username: " + val.username + ". Password: " + val.password + ". Role: " + val.role + "</div>");
            })

            loginUsersList.find('div').addClass("user-login-item");
        }
    })
})