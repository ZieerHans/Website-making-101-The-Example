(function () {
  "use strict";

  var toggle = document.getElementById("nav-toggle");
  var nav = document.getElementById("site-nav");
  if (toggle && nav) {
    toggle.addEventListener("click", function () {
      var open = nav.classList.toggle("open");
      toggle.setAttribute("aria-expanded", open ? "true" : "false");
    });
    nav.querySelectorAll("a").forEach(function (link) {
      link.addEventListener("click", function () {
        nav.classList.remove("open");
        toggle.setAttribute("aria-expanded", "false");
      });
    });
  }

  // Highlight the active section in the jump-nav on the Confessions page
  var jumpLinks = document.querySelectorAll(".jump-nav a");
  var sections = document.querySelectorAll(".confession");
  if (jumpLinks.length && sections.length && "IntersectionObserver" in window) {
    var map = {};
    jumpLinks.forEach(function (link) {
      map[link.getAttribute("href").slice(1)] = link;
    });
    var observer = new IntersectionObserver(function (entries) {
      entries.forEach(function (entry) {
        var link = map[entry.target.id];
        if (!link) return;
        if (entry.isIntersecting) {
          jumpLinks.forEach(function (l) { l.classList.remove("current"); });
          link.classList.add("current");
        }
      });
    }, { rootMargin: "-40% 0px -50% 0px" });
    sections.forEach(function (section) { observer.observe(section); });
  }
})();
