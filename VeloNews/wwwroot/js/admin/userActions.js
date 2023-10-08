$(document).ready(function () {
    const messages = $('.user-activity-block');
    scrollToBottom();
    var hub = new signalR.HubConnectionBuilder()
        .withUrl("/userActivity")
        .build();

    hub.on("AddNewComment", function (username, newsId, commentText) {
        const div = $('<div></div>').addClass('user-activity-block-row');
        div.html(`User ${username} add comment <a href="/News/ShowNews?newsId=${newsId}">at news</a> with text: ${commentText}`);

        $('.user-activity-block').append(div);
        scrollToBottom();
    });

    hub.on("LoggedOut", function (userId, username) {
        const div = $('<div></div>').addClass('user-activity-block-row');
        div.html(`User ${username} logged out the site.`);

        $('.user-activity-block').append(div);
        console.log(userId, username);
        scrollToBottom();
    });
    hub.on("LogIn", function (userId, username) {
        shouldScroll = messages.scrollTop + messages.clientHeight === messages.scrollHeight;

        const div = $('<div></div>').addClass('user-activity-block-row');
        div.html(`User ${username} log in to the site.`);

        $('.user-activity-block').append(div);
        console.log(userId, username);
        scrollToBottom();
    });
    function scrollToBottom() {
        messages.scrollTop(1000 * 10);
    }

    scrollToBottom();

    hub.start();
});