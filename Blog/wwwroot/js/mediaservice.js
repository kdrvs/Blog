
const url = '/MediaApi';
const mediaFolder = '/media/';



async function images_popUp() {
    var popUp = document.getElementById('PopUp_Images');
    popUp.hidden = false;
    await render_images();
}

function close_images_popUp() {
    var popUp = document.getElementById('PopUp_Images');
    popUp.hidden = true;
}

async function get_media() {

    try {
        let res = await fetch(url, {
            method: 'GET'
        });
        return await res.json();
    }
    catch {
        fetch_message('Ошибка, попробуйте ещё раз.');
    }
    
}

async function save_image() {
    var form = document.getElementById('new_file');
    var data = new FormData();
    data.append('iForm', form.files[0]);

    await fetch(url, {
        method: 'POST',
        body: data
    }).then(function (response) {
        if (response.status !== 200) {
            console.log('Bad status: ' + response.status);
            fetch_message('Ошибка сервера, попробуйте ещё раз.');
            return;
        }
        response.json().then(function (status) {
            console.log(status);
            form.value = '';
        });
    }).catch(function (err) {
        console.log('Fetch Error: -S', err);
        fetch_message('Ошибка, попробуйте ещё раз.');
    });
    await render_images();
}

async function render_images() {
    let data = await get_media();
    let media_area = document.getElementById('images_list');
    media_area.innerHTML = '';

    

    for (i = 0; i < data.length; i++) {
        let img = document.createElement('img');
        let src = mediaFolder + data[i];
        img.src = src;
        img.className = 'media_preview';
        img.onclick = function () { select_image(src) };
        media_area.appendChild(img);
        
    }
}

function select_image(filepath) {
    let text_area = document.getElementById('input_field');
    let start_carriage = text_area.selectionStart;
    let text = text_area.value.substring(0, start_carriage) + "[img]" + filepath + "[/img]" + text_area.value.substring(start_carriage);
    text_area.value = text;
    close_images_popUp();
}

function fetch_message(text) {
    doc = document.getElementById('fetch_message');
    doc.innerText = text;
    doc.hidden = false;
}