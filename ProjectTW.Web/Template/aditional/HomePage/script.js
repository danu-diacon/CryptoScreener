$(document).ready(function(){


    $('.header_burger').click(function (){
        $('.header_burger, .menu').toggleClass('active');
        $('body').toggleClass('lock')
    });

    $('.slick').slick(
        {
            dots: true, 
            infinite: true,
            autoplay: true,
            autoplaySpeed: 3000,
            arrows: false,
        }
    );
})