angular.module("MyApp.Constants", []);
angular.module("MyApp.Services", ["MyApp.Constants"]);
angular.module("MyApp.Controllers", ["MyApp.Constants", "MyApp.Services"]);
angular.module("MyApp.Directives", ["MyApp.Constants"]);
angular.module("MyApp.Filters", ["MyApp.Constants"])

//Any modules that are required by the whole application have to be added here
angular.module("MyApp", ["ngCookies"
                        ,"ngAnimate"
                        ,"ngSanitize"
                        ,"toaster"
                        ,"ui.router"
                        ,"ui.bootstrap"
                        ,"MyApp.Controllers"
                        ,"MyApp.Filters"
                        ,"MyApp.Directives"
]);

