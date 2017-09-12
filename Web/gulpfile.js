/// <binding ProjectOpened='convertLess:css, watch' />
var gulp = require("gulp");
var fs = require("fs");
var less = require("gulp-less");
var concat = require("gulp-concat");
var cssmin = require("gulp-cssmin");
var cached = require("gulp-cached");

gulp.task("convertLess:css", function () {
    return gulp.src(['./wwwroot/less/**/*.less', './wwwroot/less/*.less'])
               .pipe(cached('css'))
               .pipe(less())
               .pipe(gulp.dest('wwwroot/css'));
});

gulp.task("compress:css", function () {
    return gulp.src(['./wwwroot/css/**/*.css', './wwwroot/css/*.css'])
               .pipe(concat('wwwroot/css/output/site.css'))
               .pipe(cssmin())
               .pipe(gulp.dest('.'));
});

gulp.task('watch', function () {
    gulp.watch(['./wwwroot/less/**/*.less', './wwwroot/less/*.less'], ['dev']);
});

gulp.task("prod", ["convertLess:css", "compress:css"]);
gulp.task("dev", ["convertLess:css"]);