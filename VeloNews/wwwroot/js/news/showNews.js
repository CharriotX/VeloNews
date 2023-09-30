$(document).ready(function () {
    $('.comment-input-submit input').click(function () {
        const userText = $("[name=Text]").val();
        const newsId = $(".news-id").val();
        const commentId = $(".comment-id-form").val();
        console.log(commentId);

        $.post(`/api/news/addComment?commentId=${commentId}&newsId=${newsId}&text=${userText}`)
            .then(function (data) {

                if (commentId == 0) {
                    const copyCommentBlock = $(".comment-item.template").clone();
                    copyCommentBlock.removeClass("template");

                    copyCommentBlock
                        .find(".text-username-nick")
                        .text(data.author);

                    copyCommentBlock
                        .find(".comment-item-avatar img")
                        .attr('src', data.authorProfileImageUrl);

                    copyCommentBlock
                        .find(".text-username-date")
                        .text(data.createdTime);

                    copyCommentBlock
                        .find(".item-text-field")
                        .html(userText);

                    $('.comment-list').prepend(copyCommentBlock);
                    $("trix-editor").html(' ');
                }
                else {
                    let comment = $(`.comment-item[data='${commentId}']`);

                    comment
                        .find(".item-text-field")
                        .html(userText);

                    comment
                        .find(".text-username-date")
                        .text('Изменено ' + data.createdTime);

                    $("trix-editor").html(' ');
                    $('.comment-head').text('Add comment');
                }

            });
    });

    $(".remove-comment").click(function () {
        const commentId = $(this)
            .closest(".comment-item")
            .find("[name=commentId]")
            .val();

        $.post(`/api/news/RemoveComment?commentId=${commentId}`);

        $(this)
            .closest(".comment-item")
            .remove();
    });

    $(".edit-comment").click(function () {
        const thisComment = $(this);
        const commentId = thisComment
            .closest(".comment-item")
            .find("[name=commentId]")
            .val();
        $('.comment-head').text("Edit your comment");
        const oldText = $(this)
            .closest('.comment-item-text')
            .find('.item-text-field div')
            .text();

        $('trix-editor').text(oldText);
        $('.comment-id-form').attr('value', commentId);
    })
})