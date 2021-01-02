
function openNav() {
    document.getElementById("mySidebar").style.width = "250px";
    document.getElementById("main").style.marginLeft = "250px";
    document.getElementById("display").style.display = "block";
}

function closeNav() {
    document.getElementById("mySidebar").style.width = "0";
    document.getElementById("main").style.marginLeft = "0";
    document.getElementById("display").style.display = "none";
}
function openNavType() {
    document.getElementById("side-drawer").style.width = "400px";
   
    
}
showInPopup = (url, title) => {
    
    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            document.getElementById("side-drawer").style.width = "400px";
            $('#side-drawer .card-body').html(res);
            $('#side-drawer .card-title').html(title);
            //$('#side-drawer').modal('show');
            // to make popup draggable
            //$('.modal-dialog').draggable({
            //    handle: ".modal-header"
            //});
            console.log(res, "check");
        }

    })
}
function closeNavType() {
    document.getElementById("side-drawer").style.width = "0";
    document.getElementById("tablebody").style.marginLeft = "0";
    
}