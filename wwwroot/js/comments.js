
const url = '/CommentsApi';

const lastComment = {
    id: '',
};

const PostId = function () {
    let id = document.getElementById("PostId").innerText;
    return id;
    }

const comment = function () {
    let data = {
        PostId: document.getElementById("PostId").innerText,
        AuthorName: document.getElementById("AuthorName").value,
        CommentContent: document.getElementById("CommentContent").value
    }
    return data;
};

async function saveComment() {
    await fetch(url, {
        method: 'POST',
        headers: { 'Content-type': 'application/json' },
        body: JSON.stringify(comment())
    }).then(function (response) {
        if (response.status !== 200) {
            console.log('Bad status: ' + response.status);
            return;
        }
        response.json().then(function (commentId) {
            lastComment.id = "comment" + commentId;
            console.log('CommentId = ' + lastComment.id);

            
        });
    }).catch(function (err) {
        console.log('Fetch Error: -S', err);
    });
    await getComments();
}

function dateParse(datestring) {
    var opt = { hour: 'numeric', minute: 'numeric', year: 'numeric', month: 'long', day: 'numeric' };
    var ms = Date.parse(datestring);
    var date = new Date(ms);
    console.log(date.toLocaleString('ru-RU', opt));
    return date.toLocaleString('ru-RU', opt);

}

async function getComments() {
    await fetch(url + '?postId=' + PostId()).then(function (responce) {
        if (responce.status !== 200) {
            console.log('Bad status: ' + responce.status);
            return;
        }
        responce.json().then(function (allComments) {        

            let commentsBody = document.getElementById('AllComments');
            commentsBody.innerHTML = '';

            for (let comment of allComments.comments) {

                let comId = document.createElement('p');
                comId.className = "commentId";
                comId.innerText = "#" + comment.id + ",          ";

                let comDate = document.createElement('p');
                comDate.innerText = dateParse(comment.date);
                comDate.className = "commentDate";

                let comAuthor = document.createElement('p');
                comAuthor.innerText = comment.autorName;
                comAuthor.className = "commentAuthor";

                let comContent = document.createElement('p');
                comContent.innerText = comment.text;
                comContent.className = "commentText";

                let br = document.createElement('div');
                br.className = "break";

                let div = document.createElement('div');
                div.id = "comment" + comment.id;
                div.className = "comment";

                div.appendChild(comId);
                div.appendChild(comDate);
                div.appendChild(comAuthor);
                div.appendChild(comContent);
                div.appendChild(br);
                commentsBody.appendChild(div);

                document.getElementById("CommentContent").value = '';
            }
           
        });
    }).catch(function (err) {
        console.log('Fetch Error: -S', err);
    });
    if (lastComment.id !== '') {
        console.log(lastComment.id);
        window.location.href = '#' + lastComment.id;
    }
}
