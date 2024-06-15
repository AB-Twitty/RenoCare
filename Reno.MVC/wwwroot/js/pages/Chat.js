/*
        // ToDown
        $('#toDown').click(function () {
            $('.chat-discussion').animate({
                scrollDown: 0
            }, 1000);
        });
        $("#toDown").addClass("hidett");

        $('.chat-discussion').scroll(function () {
            if ($(this).scrollDown() < 650) {
                $("#toDown").addClass("hidett").removeClass("showtt");
            } else {
                $("#toDown").removeClass("hidett").addClass("showtt");
            }
        });
        

$('li.chat-user').on('click', function () {
    debugger;
    let userId = $(".li.chat-user.active").data("user-id");
    $('li.chat-user.active').removeClass('active');
    $(this).addClass('active');
    $('#active-chat').val(userId);
});*/

function changeActiveChat(elem) {
    let userId = $(".li.chat-user.active").data("user-id");
    $('li.chat-user.active').removeClass('active');
    $(elem).addClass('active');
    $('#active-chat').val(userId);
}

console.log('@access_token');

// In the client (JavaScript)
const connection = new signalR.HubConnectionBuilder()
    .withUrl("http://localhost:6982/chat", {
        accessTokenFactory: () => '@access_token'
    })
    .build();

connection.on("ReceiveMessage", (sender, receiver, message) => {
    var $chatDiscussion = $('.chat-discussion');
    var active_chat = $('#active-chat').val();

    if (sender == '@user_id' && receiver == active_chat) {
        $chatDiscussion.append(
            `<div class="chat-message right">
                <div class="message">
                    <span class="message-date"> Fri Jan 25 2015 - 11:12:36 </span>
                    <span class="message-content">
                    ${message}
                    </span>
                </div>
            </div>`
        );
    }
    else if (sender == active_chat && receiver == '@user_id') {
        $chatDiscussion.append(
            `<div class="chat-message left">
                <img class="message-avatar" src="imgs/chat_avatar.jpg" alt="">
                <div class="message">
                    <a class="message-author" href="#"> Michael Smith </a>
                    <span class="message-date"> Mon Jan 26 2015 - 18:39:23 </span>
                    <span class="message-content">
                    ${message}
                    </span>
                </div>
            </div>`
        );
    }
    else if (sender != active_chat) {
        // Check if there is a chat user with data attribute that has the value of the sender
        var $chatUser = $('li.chat-user[data-user-id="' + sender + '"]');
        if ($chatUser.length > 0) {
            // If one is found then change background color to light red
            $chatUser.css('background-color', 'lightcoral');
        } else {
            // Else create a new li.chat-user and give it a data attribute with the sender value
            // Set the background to light red
            // Append the new chat user to the desired parent element, for example, ul#chat-users-list
            $('#users-list').prepend(
                `<li onclick="changeActiveChat(this)" class="chat-user" data-user-id="${sender}" style="background-color:lightcoral;">
                    <img class="chat-avatar" src="imgs/chat_avatar.jpg" alt="">
                    <div class="chat-user-name">
                        new sender name
                    </div>
                </li>`
            );
        }
    }

    // Scroll to the bottom of the chat discussion
    $chatDiscussion.scrollTop($chatDiscussion[0].scrollHeight);
});


connection.start().catch(err => console.error(err));
console.log("hvjhhjhh");
console.log(connection);

// Send a message
function sendMessage() {
    var msg = $('#msg').val();
    var active_chat = $('#active-chat').val();
    if (msg && active_chat) {
        connection.invoke("SendMessage", active_chat, msg).catch(err => console.error(err));
        $('#msg').val("");
    }
}
