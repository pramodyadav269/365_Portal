var app = angular.module('MasterPage', []);

// CONTROLLER FUNCTIONS
app.controller("DefaultController", function ($scope, CommonDataService) {
    objDs = CommonDataService;
    //objDs.DS_GetUserTopics();

    //$scope.NotificationText = "Notifications";
    $scope.ActiveContainer = "Topic";

    $scope.Notifications = [
        {
            "ID": 1,
            "Title": "Employee Conduct",
            "Message": "You just completed the <b>Principles Made Practical module",
            "IsRead": true,
            "SortOrder": 1
        },
        {
            "ID": 1,
            "Title": "Employee Conduct",
            "Message": "You’re <b>halfway</b> at completing all your modules! Congrats!",
            "IsRead": true,
            "SortOrder": 1
        }
        ,
        {
            "ID": 1,
            "Title": "Employee Conduct",
            "Message": "You just completed the <b>Staff Development topic",
            "IsRead": true,
            "SortOrder": 1
        }
    ];

});

//COMMON SERVICE OPERATIONS
app.service("CommonDataService", function ($http, $rootScope, $compile) {
    var ds = this;

    ds.DS_GetUserTopics = function (userId) {

        $rootScope.Topics = [
            {
                "TopicID": 1,
                "Title": "Employee Conduct",
                "Description": "Life as an employee can be tough. Let's work together to make it easier.",
                "IsCompleted": true,
                "TotalModules": 10,
                "CompletedModules": 4,
                "Progress": "4/10",
                "ProgressBarText": "4 of 10 Completed",
                "SortOrder": 1
            },
            {
                "TopicID": 2,
                "Title": "Workplace Equity",
                "Description": "How to be more accepting and bare with your colleagues.",
                "IsCompleted": false,
                "TotalModules": 10,
                "CompletedModules": 4,
                "Progress": "4/10",
                "ProgressBarText": "4 of 10 Completed",
                "SortOrder": 2
            },
            {
                "TopicID": 3,
                "Title": "Staff Efficiency",
                "Description": "Increase your productivity while not losing motivation",
                "IsCompleted": false,
                "TotalModules": 10,
                "CompletedModules": 4,
                "Progress": "",
                "ProgressBarText": "4 of 10 Completed",
                "SortOrder": 3
            }
            ,
            {
                "TopicID": 4,
                "Title": "Motivation",
                "Description": "Increase your productivity while not losing motivation",
                "IsCompleted": false,
                "TotalModules": 10,
                "CompletedModules": 4,
                "Progress": "",
                "ProgressBarText": "4 of 10 Completed",
                "SortOrder": 4
            }
        ];

        /*
        $(".mainLoader").show();
        $http({
            method: "POST",
            url: "nlp_report_ajax.aspx?sid=" + QueryString()["sid"],
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
            data: $.param({
                reqType: "5",
                ReportType: reportType
            })
        }).then(function success(response) {
            response = response.data;
            var aa = response.substring(response.indexOf("<Response>", 0) + 10, response.indexOf("</Response>", 0));
            aa = JSON.parse(aa);         
        });
        */
    }
});
