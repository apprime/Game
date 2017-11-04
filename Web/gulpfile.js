/// <binding ProjectOpened='watch' />
var gulp = require("gulp");
var fs = require("fs");
var less = require("gulp-less");
var del = require('del');
var concat = require("gulp-concat");
var cssmin = require("gulp-cssmin");
var cached = require("gulp-cached");

var paths =
{
    scripts: ["../Client/scripts/**/*.js", "../Client/scripts/**/*.ts", "../Client/scripts/**/*.map"],
    styles: ["Client/style/**/*.less", "Client/style/*.less"]
};

gulp.task('clean:dev', ['cleanDev:css', 'cleanDev:script']);

gulp.task('clean:prod', ['cleanProd:css', 'cleanProd:script']);

gulp.task('cleanDev:css', function ()
{
    return del(['wwwroot/css/**/*', '!wwwroot/css/site.css']);
});

gulp.task('cleanProd:css', function ()
{
    return del(['wwwroot/css/site.css']);
});

gulp.task('cleanDev:script', function ()
{
    return del(['wwwroot/scripts/**/*', '!wwwroot/scripts/site.js']);
});

gulp.task('cleanProd:script', function ()
{
    return del(['wwwroot/scripts/site.js']);
});

gulp.task('convert:typescript', function ()
{
    gulp.src(paths.scripts).pipe(gulp.dest('wwwroot/js'));
});

gulp.task("convertLess:css", function ()
{
    return gulp.src(paths.styles)
               .pipe(cached('css'))
               .pipe(less())
               .pipe(gulp.dest('wwwroot/css'));
});

gulp.task("compress:css", function ()
{
    return gulp.src(['./wwwroot/css/**/*.css', './wwwroot/css/*.css'])
               .pipe(concat('wwwroot/css/site.css'))
               .pipe(cssmin())
               .pipe(gulp.dest('.'));
});

gulp.task('watch', function () {
    gulp.watch([paths.styles], ['dev']);
});

gulp.task("prod", ["clean:prod", "convertLess:css", "compress:css", "convert:typescript", "clean:dev" ]);
gulp.task("dev", ["clean:dev", "convertLess:css", "convert:typescript" ]);