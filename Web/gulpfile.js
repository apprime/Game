var gulp = require("gulp");
var fs = require("fs");
var less = require("gulp-less");
var concat = require("gulp-concat");
var cssmin = require("gulp-cssmin");

gulp.task("convertLess:css", function () {
    return gulp.src(['./wwwroot/less/**/*.less', './wwwroot/less/*.less'])
               .pipe(less())
               .pipe(gulp.dest('wwwroot/css'));
});

gulp.task("compress:css", function () {
    return gulp.src(['./wwwroot/css/**/*.css', './wwwroot/css/*.css'])
               .pipe(concat('wwwroot/css/output/site.css'))
               .pipe(cssmin())
               .pipe(gulp.dest('.'));
});



gulp.task("prod", ["convertLess:css", "compress:css"]);
gulp.task("dev", ["convertLess:css"]);