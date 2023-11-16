$(document).ready(function () {
    $('.comment-input-submit input').click(function () {
        const userText = $("[name=Text]").val();
        const newsId = $(".news-id").val();
        const commentId = $(".comment-id-form").val();

        var hub = new signalR.HubConnectionBuilder()
            .withUrl("/userActivity")
            .build();

        var commentData = {
            "newsId": parseInt(newsId),
            "text": userText
        }

        if (commentId == 0) {
            $.post(`/api/newsComments/`, commentData)
                .then(function (data) {
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
                        .html(data.text);

                    $('.comment-list').prepend(copyCommentBlock);
                    $("trix-editor").html(' ');

                });
        }
        else {
            const userText = $("[name=Text]").val();
            var commentData = {
                "id": parseInt(commentId),
                "text": userText
            }
            $.ajax({
                url: `/api/newsComments/${commentId}`,
                type: 'PUT',
                data: commentData,
                success: function () {
                    $('trix-editor').text("");
                    $('.comment-id-form').attr('value', 0);
                    $('.comment-head').text("Add comment: ");

                    $('.comment-item[data=' + commentId + ']')
                        .find(".item-text-field")
                        .html(userText);
                    commentId = 0;
                }
            })
        }
    });

    $(".remove-comment").click(function () {
        const removeBtn = $(this);
        const id = $(this)
            .closest(".comment-item")
            .find("[name=id]")
            .val();

        $.ajax({
            url: `/api/newsComments/${id}`,
            type: 'DELETE',
            success: function () {
                removeBtn
                    .closest(".comment-item")
                    .remove();
            }
        });
    });

    $(".edit-comment").click(function () {
        $('.comment-head').text("Edit your comment");
        const thisComment = $(this);
        const id = thisComment
            .closest(".comment-item")
            .find("[name=id]")
            .val();
        $('.comment-id-form').attr('value', id);

        const oldText = $(this)
            .closest('.comment-item-text')
            .find('.item-text-field div')
            .text();

        $('trix-editor').text(oldText);
    })
})