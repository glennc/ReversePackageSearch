/// <reference path="node_modules/grunt/lib/grunt.js" />

// node-debug (Resolve-Path ~\AppData\Roaming\npm\node_modules\grunt-cli\bin\grunt) task:target

module.exports = function (grunt) {
    /// <param name="grunt" type="grunt" />
    
    grunt.loadNpmTasks('grunt-contrib-uglify');
    grunt.loadNpmTasks('grunt-contrib-watch');
    grunt.loadNpmTasks('grunt-contrib-copy');
    grunt.loadNpmTasks('grunt-contrib-clean');
    grunt.loadNpmTasks('grunt-contrib-less');

    grunt.initConfig({
        staticFilePattern: '**/*.{js,css,map,html,htm,ico,jpg,jpeg,png,gif,eot,svg,ttf,woff}',
        pkg: grunt.file.readJSON('package.json'),
        uglify: {
            options: {
                banner: '/*! <%= pkg.name %> <%= grunt.template.today("dd-mm-yyyy") %> */\n'
            }
        },
        clean: {
            options: { force: true },
            bower: ['content'],
            assets: ['content'],
        },
        copy: {
            // This is to work around an issue with the dt-angular bower package https://github.com/dt-bower/dt-angular/issues/4
            fix: {
                files: {
                    "bower_components/jquery/jquery.d.ts": ["bower_components/dt-jquery/jquery.d.ts"]
                }
            },
            bower: {
                files: [
                    {   // JavaScript
                        expand: true,
                        flatten: true,
                        cwd: "bower_components/",
                        src: [
                            "modernizr/modernizr.js",
                            "jquery/dist/*.{js,map}",
                            "jquery.validation/jquery.validate.js",
                            "jquery.validation/additional-methods.js",
                            "bootstrap/dist/**/*.js",
                            "respond/dest/**/*.js",
                            "angular/*.{js,.js.map}",
                            "angular-route/*.{js,.js.map}",
                            "angular-bootstrap/ui-bootstrap*"
                        ],
                        dest: "content/js/",
                        options: { force: true }
                    },
                    {   // CSS
                        expand: true,
                        flatten: true,
                        cwd: "bower_components/",
                        src: [
                            "bootstrap/dist/**/*.css",
                        ],
                        dest: "content/css/",
                        options: { force: true }
                    },
                    {   // Fonts
                        expand: true,
                        flatten: true,
                        cwd: "bower_components/",
                        src: [
                            "bootstrap/**/*.{woff,svg,eot,ttf}",
                        ],
                        dest: "content/fonts/",
                        options: { force: true }
                    }
                ]
            },
            app: {
                files: [
                    {
                        src: [
                            'Client/ng-app/app.js'
                        ],
                        dest: "content/js/app.js",
                        options: { force: true }
                    }
                ]
            },
            assets: {
                files: [
                    {
                        expand: true,
                        cwd: "Client/",
                        src: [
                            '<%= staticFilePattern %>',
                            '!**/app.js'
                        ],
                        dest: "content/",
                        options: { force: true }
                    }
                ]
            }
        },
        less: {
            dev: {
                options: {
                    cleancss: false
                },
                files: {
                    "content/css/site.css": "Client/**/*.less"
                }
            },
            release: {
                options: {
                    cleancss: true
                },
                files: {
                    "content/css/site.css": "Client/**/*.less"
                }
            }
        },
        watch: {
            dev: {
                files: ['bower_components/<%= staticFilePattern %>', 'Client/<%= staticFilePattern %>'],
                tasks: ['dev']
            }
        }
    });

    //grunt.registerTask('test', ['jshint', 'qunit']);
    grunt.registerTask('dev', ['clean', 'copy', 'less:dev']);
    grunt.registerTask('release', ['clean', 'copy', 'uglify', 'less:release']);
    grunt.registerTask('default', ['dev']);
};