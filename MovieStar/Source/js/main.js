$('#main_slider .owl-carousel').owlCarousel({
    loop:true,
    animateIn:'fadeIn',
    animateOut:'fadeOut',
    dots:true,
    responsiveClass:true,
    autoplay:true,
    mouseDrag:false,
    touchDrag:false,
    pullDrag:false,
    autoplayTimeout:5000,
    autoplayHoverPause:false,
    responsive:{
        0:{
            items:1,
            nav:false
        },
        600:{
            items:1,
            nav:false
        },
        1000:{
            items:1,
            nav:false,
            loop:true
        }
    }
})

$('#rated_films .owl-carousel').owlCarousel({
    loop:true,
    dots:false,
    margin:30,
    responsiveClass:true,
    autoplay:false,
    mouseDrag:true,
    touchDrag:true,
    pullDrag:true,
    autoplayTimeout:5000,
    autoplayHoverPause:false,
    responsive:{
        0:{
            items:1,
            nav:false
        },
        600:{
            items:1,
            nav:false
        },
        1000:{
            items:4,
            nav:true,
            loop:true
        }
    }
})

$('#heading .hidden_nav_icon').click(function() {
    if($( '#heading .hidden_main_nav' ).height()==0){
        $( '#heading .hidden_main_nav' ).css('height','180px');
    }
    else{
        $( '#heading .hidden_main_nav' ).css('height','0px');
    }
  });

  $('#scroll_nav .hidden_nav_icon').click(function() {
    if($( '#scroll_nav .hidden_main_nav' ).height()==0){
        $( '#scroll_nav .hidden_main_nav' ).css('height','180px');
    }
    else{
        $( '#scroll_nav .hidden_main_nav' ).css('height','0px');
    }
  });


window.onscroll=function(){
    if(document.documentElement.scrollTop > 50){
        $('#scroll_nav').css('top','0%');
    }
    else{
        $('#scroll_nav').css('top','-50%');
    }
}

$('#films .select_list').click(function () {
    if (!$(this).hasClass('active')) {
        $(this).parent().siblings().children('.select_list').removeClass('active');
        $(this).addClass('active');
    }
    else {
        $(this).removeClass('active');
    }
}) 

$(document).ready(function(){
    $('#preloader').css('display','none');
})

$('#selected_film .content .buttons .play').click(function(){
    $('#trailer').css('display','block');
})

$('#main_slider .content .buttons .play').click(function(){
    var src=$(this).next().attr('src');
    $('#trailer').css('display','block');
    $('#trailer #trailer_iframe').attr('src','');
    $('#trailer #trailer_iframe').attr('src',src);
})



$('#trailer').click(function(){
    $(this).css('display','none');
    var video = $("#trailer_iframe").attr("src");
    $("#trailer_iframe").attr("src","");
    $("#trailer_iframe").attr("src",video);
})

$(document).on('click','#news .inner .mini_img img',function () {
    var oldsrc=$(this).parent().parent().prev().children().children('img').attr('src');
    var src=$(this).attr('src');
    $(this).attr('src',oldsrc);
    $(this).parent().parent().prev().children().children('img').attr('src',src);
})

$('#news_main .inner .mini_img img').click(function(){
    var oldsrc=$(this).parent().parent().prev().children().children('img').attr('src');
    var src=$(this).attr('src');
    $(this).attr('src',oldsrc);
    $(this).parent().parent().prev().children().children('img').attr('src',src);
})

$('#personal .posts nav ul #liked').click(function(){
    if(!$(this).hasClass('active')){
        $(this).addClass('active');
        $(this).next().removeClass('active');
        $('#personal .posts .screen .liked').addClass('selected');
        $('#personal .posts .screen .saved').removeClass('selected');
    }
})

$('#personal .posts nav ul #saved').click(function(){
    if(!$(this).hasClass('active')){
        $(this).addClass('active');
        $(this).prev().removeClass('active');
        $('#personal .posts .screen .saved').addClass('selected');
        $('#personal .posts .screen .liked').removeClass('selected');
    }
})

//-------------------------------------------------------------------------

$('#edit_usr_profile .setting_nav ul #edit').click(function(){
    if(!$(this).hasClass('active')){
        $(this).addClass('active');
        $(this).siblings().removeClass('active');
        $('#edit_usr_profile .screen .edit_profile').addClass('selected');
        $('#edit_usr_profile .screen .edit_pass').removeClass('selected');
        $('#edit_usr_profile .screen .edit_pic').removeClass('selected');
    }
})

$('#edit_usr_profile .setting_nav ul #pass').click(function(){
    if(!$(this).hasClass('active')){
        $(this).addClass('active');
        $(this).siblings().removeClass('active');
        $('#edit_usr_profile .screen .edit_pass').addClass('selected');
        $('#edit_usr_profile .screen .edit_pic').removeClass('selected');
        $('#edit_usr_profile .screen .edit_profile').removeClass('selected');
    }
})

$('#edit_usr_profile .setting_nav ul #pic').click(function(){
    if(!$(this).hasClass('active')){
        $(this).addClass('active');
        $(this).siblings().removeClass('active');
        $('#edit_usr_profile .screen .edit_pic').addClass('selected');
        $('#edit_usr_profile .screen .edit_pass').removeClass('selected');
        $('#edit_usr_profile .screen .edit_profile').removeClass('selected');
    }
})

$('#edit_usr_profile .screen .edit_pic .usr_image').click(function(){
    $('#edit_usr_profile .screen .edit_pic .new_img').click();
})

$('#signup .usr_new_image').click(function(){
    $('#signup .image').click();
})

function readURL(input) {
    var url = input.value;
    var ext = url.substring(url.lastIndexOf('.') + 1).toLowerCase();
    if (input.files && input.files[0] && (ext == "gif" || ext == "png" || ext == "jpeg" || ext == "jpg")) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#user_show_img').attr('src', e.target.result);
        }
        reader.readAsDataURL(input.files[0]);
    }
}