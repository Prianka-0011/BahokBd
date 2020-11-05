$(document).ready(function () {
    $('.sidemenuToggler').on('click', function () {
        $('.wrapper').toggleClass('active');
    });

    $('.prod-btn').click(function () {
        $('.prod-show').toggleClass("show");
        $('.first').toggleClass("rotate");
    });
    $('.cat-btn').click(function () {
        $('.cat-show').toggleClass("show1");
        $('.second').toggleClass("rotate1");
    });