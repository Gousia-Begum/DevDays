$(document).ready(function() {
  $(".nav-move .nav-heading").on("click", function() {
    $(".nav-move")
      .find(".active")
      .removeClass("active");
    $(this).addClass("active");
  });
});

