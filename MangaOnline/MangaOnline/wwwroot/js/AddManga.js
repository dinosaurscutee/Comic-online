const myDiv = document.getElementById("filter-rate");
let openDropdown = false;
myDiv.addEventListener("click", function (event) {
    const myDiv = document.getElementById("dropdown-category");
    openDropdown = !openDropdown;
    if (openDropdown) {
        myDiv.className = "show-dropdown";
        myDiv.classList.remove("hide-dropdown");
    } else {
        myDiv.className = "hide-dropdown";
        myDiv.classList.remove("show-dropdown");
    }

    // Do something when the div is clicked
});


const fileInput = document.getElementById('fileInput');
const preview = document.getElementById('preview');

fileInput.addEventListener('change', function () {
    const file = this.files[0];
    const reader = new FileReader();

    reader.addEventListener("load", function () {
        preview.src = reader.result;
    }, false);

    if (file) {
        reader.readAsDataURL(file);
    }
});

const submitBtn = document.getElementById("submitBtn");
const status = document.getElementById("status-manga");
const checkboxes = document.getElementsByName('CategoriesId');
const createdAt = document.getElementById('input-createdAt');
let statusValue = document.getElementById('status-manga-value');

let notiStatus = document.getElementById("noti-status");
let notiCategory = document.getElementById("noti-category");
let notiCreatedAt = document.getElementById("CreatedAt");

submitBtn.addEventListener("click", function (e) {
    let fail = false;

    notiStatus.innerHTML = "";
    notiCategory.innerHTML = "";
    notiCreatedAt.innerHTML = "";

    statusValue.value = status.value;
    var values = [];

    // Lặp qua từng checkbox được chọn và lưu giá trị vào mảng
    for (var i = 0; i < checkboxes.length; i++) {
        if (checkboxes[i].checked) {
            values.push(checkboxes[i].value);
        }
    }
    if (values.length <= 0) {
        notiCategory.innerHTML = "Cần chọn ít nhất 1 thể loại";
        fail = true;
    }
    if (status.value === 'Trạng thái') {
        notiStatus.innerHTML = "chưa chọn trạng thái";
        fail = true;
    }
    if (!/^\d+$/.test(createdAt.value)) {
        console.log(createdAt.value);
        notiCreatedAt.innerHTML = "năm sai";
        fail = true;
    }
    if (fail) {
        e.preventDefault();
    }
});