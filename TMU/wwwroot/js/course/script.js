
function showMenuright() {
    $(document).ready(function(){
          $(".Munu-Right").toggle(1000);
     
      });
      var x = document.getElementById("toggle");
      if(x.className=="nav")
      {
        x.className+="responsive";
        document.getElementById("toggle").innerHTML="&#9747";

      }
      else{
        x.className="nav";
        document.getElementById("toggle").innerHTML="&#9776";

      }


}