$(document).ready(function () {
    $('.comment-input-submit input').click(function () {
        const userText = $("[name=Text]").val();
        const newsId = $(".news-id").val();

        $.post(`/api/news/addComment?newsId=${newsId}&text=${userText}`)
            .then(function (data) {

                console.log(data);
                const copyCommentBlock = $(".comment-item.template").clone();
                copyCommentBlock.removeClass("template");

                copyCommentBlock
                    .find(".text-username-nick")
                    .text(data.author);

                copyCommentBlock
                    .find(".text-username-date")
                    .text(data.createdTime);

                copyCommentBlock
                    .find(".item-text-field")
                    .html(userText);

                $('.comment-list').append(copyCommentBlock);
            });
    });

    $(".remove-button").click(function () {
        const commentId = $(this)
            .closest(".comment-item")
            .find("[name=commentId]")
            .val();

        $.post(`/api/news/RemoveComment?commentId=${commentId}`);

        $(this)
            .closest(".comment-item")
            .remove();
    })
})