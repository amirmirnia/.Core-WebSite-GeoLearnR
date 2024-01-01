$(document).ready(function(){
    $("#bars").click(function(){
    $("#togglenav").slideDown();
    $("#hilightbody").css("display", "block");
    $("body").css("overflow", "hidden");
    });
    $("#barsclose").click(function(){
    $("#togglenav").slideUp();
    $("#hilightbody").css("display", "none");
    $("body").css("overflow", "auto");
    });
  });
$(document).ready(function(){
   
    typetext("به پایگاه خود آموز GeoLearnR خوش آمدید","#p1");
    $(".togglenav .menutoggle li").click(function(){
        $(".togglenav .menutoggle li:hover ul").slideToggle();
        });
});

function typetext(text,tag) {
    var text =text;
    var i = 0;
    var f=1;
    setInterval(function(){
       
        if(i < text.length && f==1){
            $(tag).append(text.charAt(i));
            i++;
            if(i==(text.length)){
                    f=0;
                   
            }
        } 
        
        //if(f==0){
            
        //    $(tag).text($(tag).text().slice(0, -1));
        //    i--;
        //}
        
       
        
    }, 200); // 500ms delay between each character
}

