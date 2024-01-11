const manageBlogCategory = new ManageBlogCategory();

const Toast = Swal.mixin({
    toast: true,
    position: 'top-end',
    showConfirmButton: false,
    timer: 3000,
    timerProgressBar: true,
    didOpen: (toast) => {
        toast.addEventListener('mouseenter', Swal.stopTimer)
        toast.addEventListener('mouseleave', Swal.resumeTimer)
    }
})

tinymce.init({
    selector: '#content',
    height: 500,
    menubar: false,
    plugins: [
        'advlist', 'autolink', 'lists', 'link', 'image', 'charmap', 'preview',
        'anchor', 'searchreplace', 'visualblocks', 'fullscreen',
        'insertdatetime', 'media', 'table', 'code', 'help', 'wordcount'
    ],
    toolbar: 'undo redo | blocks | bold italic backcolor | ' +
        'alignleft aligncenter alignright alignjustify | ' +
        'bullist numlist outdent indent | removeformat | help'
});

function AllowUsingGPTData() {
    $("#paste-gpt-data").click(async function () {
        var text = await navigator.clipboard.readText();
        var $visual = $("#gpt-data-visualizer");
        $visual.html(text);

        //set title
        $("#Title").val($visual.find("h1").text());

        //set meta description
        $("#MetaDescription").val($visual.find("meta[name=description]").attr("content"));

        //set content
        tinymce.get('content').setContent($visual.find("article").html());

        //set slug
        $("#Slug").val($visual.find("meta[name=url]").attr("content"));

        SetImageList($visual);
        manageBlogCategory.Init();
        SetTagList($visual);
        SetKeywordList($visual);

        AllowGetCreateContentImagepromt();
    });
}

function SetImageList($visual) {
    function ListGroupItemTemplate(data) {
        return `<li class="list-group-item">
                    <div id="title" class="mb-2">${data.text()}</div>
                    <div class="d-flex align-items-center">
                        <button type="button" data-title="${data.text()}" class="get-content-promt btn btn-primary me-2">
                            <i class="fa-solid fa-robot"></i>
                        </button>
                        <input type="file" data-title="${data.text()}" class="w-25 form-control upload-content-image me-2" />
                        <img data-title="${data.text()}" class="content-image rounded border-1" height="39" src="" />
                    </div>
                </li>`;
    }
    $(".image-list").html("");
    $visual.find("article h2").each(function () {
        $(".image-list").append(ListGroupItemTemplate($(this)));
    });
}

async function SetTagList($visual) {
    async function Template(data) {
        let activeClass;
        const response = await new Promise((resolve, reject) => {
            $.get("/ManageBlog/CheckTagExist", { tagName: data }, resolve).fail(reject);
        });
        activeClass = response ? "text-primary" : "text-secondary";
        return `<tr data-title="${data}">
                    <td class="title ${activeClass}">${data}</td>
                    <td><button class="btn btn-sm btn-danger"><i class="fa-solid fa-trash"></i></button></td>
                </tr>`;
    }

    let tags = [];
    $visual.find("#hashtag li").each(function () {
        tags.push($(this).text());
    });

    $("input[id=Tags]").val(tags.join(","));
    const templates = await Promise.all(tags.map(Template));
    $("#tag-list").html(templates.join(''));
}

function SetKeywordList($visual) {
    var keywords = [];
    $visual.find("#keywords li").each(function () {
        keywords.push($(this).text());
    });
    $("input[id=Keywords]").val(keywords.join(","));
}

function AutoDowloadFile() {
    var blob = new Blob([$("#source-content").text()], { type: 'text/plain' });
    var link = document.createElement('a');
    link.href = window.URL.createObjectURL(blob);
    link.download = $("#source-title").text();
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
    window.URL.revokeObjectURL(link.href);
}

function AllowCopyCreateContentPromt() {
    $("#copy-gpt-promt").click(function () {
        navigator.clipboard.writeText($("#gpt-promt").val());
        Toast.fire({
            icon: 'success',
            title: 'Copy successfully'
        })
    });
}

function AllowGetCreateAvatarImagepromt() {
    $("#get-avatar-promt").click(function () {
        let blogTitle = $("#Title").val();
        let blogDescription = $("#MetaDescription").val();
        $.get("/ManageBlog/GetCreateAvatarPromt",
            {
                blogTitle: blogTitle,
                blogDescription: blogDescription
            }, function (data) {
                navigator.clipboard.writeText(data);
                Toast.fire({
                    icon: 'success',
                    title: 'Đã lấy promt'
                })
            });
    });
    $("#upload-avatar-image").change(function () {
        var input = this;
        if (input.files && input.files[0]) {
            var formData = new FormData();
            formData.append('image', input.files[0]);
            formData.append('title', $("#Title").val());

            fetch('/ManageBlog/CreateImage', {
                method: 'POST',
                body: formData
            })
                .then(response => response.json())
                .then(data => {
                    $("#avatar-image").attr("src", data);
                    $("#AvatarPath").val(data);
                })
        }
    })
}

function AllowGetCreateContentImagepromt() {
    $(".image-list .list-group-item .get-content-promt").click(function () {
        let blogTitle = $("#Title").val();
        let subTitle = $(this)[0].dataset.title;
        $.get("/ManageBlog/GetCreateImagePromt", { blogTitle: blogTitle, subTitle: subTitle }, function (data) {
            navigator.clipboard.writeText(data);
            Toast.fire({
                icon: 'success',
                title: 'Đã lấy promt'
            })
        });
    });
    $(".image-list .list-group-item .upload-content-image").change(async function () {
        var input = this;
        if (input.files && input.files[0]) {
            var formData = new FormData();
            formData.append('image', input.files[0]);
            formData.append('title', $(this)[0].dataset.title);

            fetch('/ManageBlog/CreateImage', {
                method: 'POST',
                body: formData
            })
                .then(response => response.json())
                .then(data => {
                    let $visual = $("#gpt-data-visualizer");
                    $(`.image-list .list-group-item .content-image[data-title='${$(this)[0].dataset.title}']`).attr("src", data);
                    $visual.find(`article h2:contains('${$(this)[0].dataset.title}')`).next().attr("src", data);
                    tinymce.get('content').setContent($visual.find("article").html());
                })
        }
    });
}

var connection = new signalR.HubConnectionBuilder().withUrl("/ManageBlogHub").build();
connection.on("ReceiveMessageBlog", function (message) { Toast.fire({icon: 'success', title: message}) });
connection.start().then(function () {
    $.get("/ManageBlog/SetCreateInitData", { link: $("#source-link").text() }, (res) => {
        $("#gpt-promt").text(res.gptPromt);
        $("#source-title").text(res.sourceTitle);
        $("#source-content").text(res.sourceContent);
        AutoDowloadFile();
        AllowCopyCreateContentPromt();
        AllowUsingGPTData();
        AllowGetCreateAvatarImagepromt();
    });
});

