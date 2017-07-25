(function ($, angular) {

    "use strict";

    mySessionsCtrl.$inject = ["$scope", "$filter", "$state", "toaster", "qspValues", "DevTasks", "ServerResponseHelper", "EnumSvc", "_"];

    var _members = null;

    function mySessionsCtrl($scope, $filter, $state, toaster, qspValues, DevTasks, ServerResponseHelper, EnumSvc, _) {
        //console.log("mySessionsCtrl");

        //Storing all of the important stuff as a globally accessible object
        _members = {
            $scope: $scope, 
            $filter: $filter,
            $state: $state,
            toaster: toaster,
            qspValues: qspValues,
            DevTasks: DevTasks, 
            ServerResponseHelper: ServerResponseHelper,
            EnumSvc: EnumSvc,
            _: _
        }

        //Loading this date filter for the google charts, this might be eliminated at one point
        $scope.dateFilter = _members.$filter('date');

        $scope.sessionRowIsBeingEdited = false; //by default

        var dtmStart = new Date(); //This date will probably be off by one hour
        var dtmEnd = dtmStart.addHours(1);
        
        //console.dir(dtmStart);
        //console.dir(dtmEnd);

        //Testing the session object
        $scope.sessionModel = {
            SessionStart: toDateTimeString(dtmStart),
            SessionEnd: toDateTimeString(dtmEnd)
            //HoursEstimate: 5,
            //Notes: "These are stock notes for testing"
        };

        var strDtmStartNoTime = $scope.dateFilter(dtmStart.toISOString(), 'MM/dd/yy');

        $scope.dateTemplate = { dateTime: strDtmStartNoTime };

        //Setting up click events
        //For Task
        $scope.btnTaskAdd_click = btnTaskAdd_click;
        $scope.btnTaskSave_click = btnTaskSave_click;
        $scope.btnTaskDelete_click = btnTaskDelete_click;

        //For Sessions
        $scope.btnSessionAdd_click = btnSessionAdd_click;
        $scope.btnSessionEdit_click = btnSessionEdit_click;
        $scope.btnSessionDelete_click = btnSessionDelete_click;

        //For Datetime template
        $scope.txtDateTemplate_click = txtDateTemplate_click;

        //Keeping this data close for the later Add/Edit/Delete actions
        $scope.devTaskId = qspValues.devTaskId;
        //$scope.tfsWorkItemId = qspValues.tfsWorkItemId;

        //Only display the chart and grid if there is a task already created
        $scope.isExistingTask = qspValues.devTaskId > 0;

        if ($scope.isExistingTask) {
            loadDevTaskToPage(qspValues.devTaskId);
        }
        else {
            _members.$scope.frmDevTask = {
                TfsWorkItemChildId: qspValues.tfsWorkItemId,
                OriginalEstimate: 1
            }
        }
    }

    function loadDevTaskToPage(devTaskId) {
        _members.DevTasks
                .getTaskLoaded(devTaskId)
                .then(function (result) {
                    var devTask = _members.ServerResponseHelper.getData(result);

                    _members.$scope.frmDevTask = devTask;

                    //console.log("loadDevTaskToPage");
                    //console.dir(devTask);

                    //If this is a new task, it won't have sessions yet
                    loadDevTaskSessionsToPage(devTask.Sessions);
                })
                .catch(function (response) {
                    errorMessage(response);
                });
    }

    function loadDevTaskSessionsToPage(devTaskSessions) {
        var isNull = (devTaskSessions === undefined || devTaskSessions === null);

        console.dir(isNull);

        var isEmpty = true;

        if (isNull === false) {
            console.dir(devTaskSessions);

            //It won't be valid if there are no sessions to work with
            isEmpty = devTaskSessions.length === 0;
        }

        if (isEmpty === true) {
            setupDevTaskSessionModelUsingDevTaskModel();
        }
        else {
            setupDevTaskSessionModel(_members._.last(devTaskSessions));

            _members.$scope.sessionsGridData = devTaskSessions;

            _members.$scope.plotsDict = groupSessionByDate(devTaskSessions);

            //Google charts required call
            google.setOnLoadCallback(drawChart(_members.$scope));
        }
    }

    function btnTaskAdd_click(devTask) {
        //console.log("btnTaskAdd_click");
        //console.dir(devTask);

        saveDevTask(devTask, function (objDevTask) {
            //console.dir(objDevTask);

            _members.$scope.isExistingTask = true;

            //First time around
            setupDevTaskSessionModelUsingDevTaskModel();

            successMessage("Task " + objDevTask.DevTaskId + " created successfully. You can start adding sessions below.");
        });
    }

    function setupDevTaskSessionModelUsingDevTaskModel() {
        var m = _members.$scope.frmDevTask;

        var devTaskSession = {
            HoursEstimate: m.OriginalEstimate
            , SessionStart: new Date()
        };

        setupDevTaskSessionModel(devTaskSession);
    }

    function setupDevTaskSessionModel(devTaskSession) {
        var s = devTaskSession;
        var m = _members.$scope.sessionModel;

        //Set the hours estimate to the last entry's estimate for consistency
        m.HoursEstimate = s.HoursEstimate;

        //When a new session is added, the notes should be blanked out just for convenience
        m.Notes = "";

        //I tried using the previous entry's DateTime for a while and I didn't like it
        //so it is always defaulting to today's date now
        //Up the start and end timestamp by one hour
        var dtm = new Date().addHours(1); //Adding an hour because JavaScript is retarded and has a problem with time for some reason
        var dtm2 = dtm.addHours(1);

        m.SessionStart = toDateTimeString(dtm);
        m.SessionEnd = toDateTimeString(dtm2);

        //console.dir(dtm);
        //console.dir(dtm);
    }

    function btnTaskSave_click(devTask) {
        //console.log("btnTaskSave_click");
        //console.log("isFormDirty: " + _members.$scope.formTask.$dirty);
        //console.dir(devTask);

        if (_members.$scope.formTask.$pristine) {
            return;
        }

        //This has to be marked otherwise the object will not be updated via the API
        devTask.IsDirty = true;

        saveDevTask(devTask, function () {
            loadDevTaskSessionsToPage(_members.$scope.sessionsGridData);
        });

        //Resetting the form so that it can be used for editing again
        _members.$scope.formTask.$setPristine();

        successMessage("Task saved successfully.");
    }

    function btnTaskDelete_click(devTaskId) {
        if (!confirm("Are you sure you want to delete this entire task\nand all of its sessions?")) {
            return;
        }
        
        _members.DevTasks
                .taskDelete(devTaskId)
                .then(function (result) {
                    _members.ServerResponseHelper.getData(result);

                    _members.$state.go("myTasks");
                })
                .catch(function (response) {
                    errorMessage(response);
                });
    }

    function saveDevTask(devTask, onSuccess) {
        _members.DevTasks
                .taskSave(devTask)
                .then(function (result) {
                    var objDevTask = _members.ServerResponseHelper.getData(result);

                    //console.log("getData.promise");
                    //console.dir(objDevTask);

                    _members.$scope.devTaskId = objDevTask.DevTaskId;

                    _members.$scope.frmDevTask = objDevTask;

                    if (onSuccess) {
                        onSuccess(objDevTask);
                    }
                })
                .catch(function (response) {
                    errorMessage(response);
                });
    }

    function btnSessionAdd_click(devTaskSession) {
        //console.log("btnSessionAdd_click");
        //console.dir(devTaskSession);

        devTaskSession.DevTaskId = _members.$scope.devTaskId;
        devTaskSession.UpdateType = _members.EnumSvc.DevTaskSession.Operation.Add;

        saveDevTaskSession(devTaskSession);
    }

    function btnSessionEdit_click(devTaskSession) {
        //console.log("btnSessionEdit_click");
        //console.dir(devTaskSession);

        devTaskSession.UpdateType = _members.EnumSvc.DevTaskSession.Operation.Edit;

        saveDevTaskSession(devTaskSession);
    }

    function btnSessionDelete_click(devTaskSession) {
        if (!confirm("Are you sure you want to delete this entry? " + devTaskSession.DevTaskSessionId)) {
            return;
        }

        devTaskSession.UpdateType = _members.EnumSvc.DevTaskSession.Operation.Delete;

        saveDevTaskSession(devTaskSession);
    }
    
    function saveDevTaskSession(devTaskSession) {
        _members.DevTasks
                .sessionSave(devTaskSession)
                .then(function (result) {
                    _members.ServerResponseHelper.getData(result);

                    //console.log("sessionSave");
                    //console.dir(objDevTaskSession);

                    _members.DevTasks
                            .getTaskSessions(_members.$scope.devTaskId)
                            .then(function (result) {
                                var objDevTaskSessions = _members.ServerResponseHelper.getData(result);

                                //console.log("getTaskSessions");
                                //console.dir(objDevTaskSessions);

                                loadDevTaskSessionsToPage(objDevTaskSessions);
                            })
                            .catch(function (response) {
                                errorMessage(response);
                            });
                })
                .catch(function (response) {
                    errorMessage(response);
                });
    }

    function txtDateTemplate_click(dateTemplate, sessionModel) {
        sessionModel.SessionStart = toDateTimeString(changeDate(sessionModel.SessionStart, dateTemplate));

        sessionModel.SessionEnd = toDateTimeString(changeDate(sessionModel.SessionEnd, dateTemplate));
    }

    function toDateTimeString(dateTimeObject) {
        return _members.$scope.dateFilter(dateTimeObject.toISOString(), 'MM/dd/yy HH:mm');
    }
    
    function changeDate(target, template) {
        target = new Date(target);
        template = new Date(template);

        //console.log(template.getMonth() + "," + template.getDate() + "," + template.getFullYear());

        return target.set({ month: template.getMonth(), day: template.getDate(), year: template.getFullYear() });
    }

    function successMessage(message) {
        _members.toaster.success({ title: "Success", body: message });
    }

    function errorMessage(response) {
        console.dir(response);

        _members.toaster.error("Error", response.data.ExceptionMessage);
    }

    function zeroOutTime(fullDateTime) {
        var d = new Date(fullDateTime);

        return new Date(
          d.getFullYear(),
          d.getMonth(),
          d.getDate()
        );
    }

    function groupSessionByDate(devTaskSessions) {
        //console.log("groupSessionByDate");

        var dict = new Dictionary();
        var s;
        var dtm;
        var hours;

        for (var i = 0; i < devTaskSessions.length; i++) {
            s = devTaskSessions[i];
            dtm = zeroOutTime(s.SessionStart);

            //console.log("containsKey") 
            //console.dir(dict.containsKey(dtm));
            
            if (!dict.containsKey(dtm)) {
                dict.add(dtm, s.HoursCompleted);
                
                //console.log("Add [" + dtm.toISOString() + ", " + s.HoursCompleted + "]");
            }
            else {
                hours = dict.get(dtm) + s.HoursCompleted;

                dict.set(dtm, hours);

                //console.log("Set [" + dtm.toISOString() + ", " + hours + "]");
            }
        }

        //console.dir(dict.getMap());

        return dict;
    }

    function Dictionary() {
        var i = 0;
        var dict = Object.create(null);
        var dtm = null;

        this.add = function (key, val) { dict[new Date(key)] = val; i++; };
        this.get = function (key) { return dict[new Date(key)]; };
        this.set = function (key, val) { dict[new Date(key)] = val; };
        this.getMap = function () { return dict; };

        //Using the underscore.js library, does the key exist true or false
        this.containsKey = function (key) {
            //I was trying to use underscore.js but this kept returning undefined...
            //return _members._.findKey(dict, { key: key });
            var isFound = false;

            for (var k in dict) {
                dtm = new Date(k);

                //console.log("k: " + dtm.getTime() + ", key: " + key.getTime());
                if (dtm.getTime() === key.getTime()) {
                    isFound = true;

                    break;
                }
            }

            return isFound;
        };

        this.count = function () {
            return i;
        };
    }

    function togglePlots(scope) {
        scope.plotsVisible = !scope.plotsVisible;

        if (scope.plotsVisible == true) {
            scope.btnTogglePlotsText = "Hide Plots";
        }
        else {
            scope.btnTogglePlotsText = "Show Plots";
        }
    }

    function drawChart(scope) {
        //console.log("mySessions.drawChart");

        var arr = [['Day', 'Completed']];
        var dict = scope.plotsDict;

        //Appending the column names to the data
        if (dict && dict.count() > 0) {
            var pairs = dict.getMap();

            //console.dir(pairs);

            for(var key in pairs) {
                //console.log("[" + key + ", " + pairs[key] + "]");
                //console.log("dtm: " + key + ", formatted: " + scope.dateFilter(key, 'MM/dd/yy'));

                arr.push([scope.dateFilter(new Date(key).toISOString(), 'MM/dd/yy'), pairs[key]]);
            }
        }

        /*
            [
                ['Day', 'Cap Ex', 'Op Ex'],
                ['2015-07-01T00:00:00', 4.01, 3.33],
                ['2015-07-01T00:00:00', 5, 3],
                ['2015-07-01T00:00:00', 2, 8]
            ]
        */

        var data = google.visualization.arrayToDataTable(arr);

        var options = {
            title: 'Task History',
            bar: { groupWidth: "95%" },
            legend: { position: 'none' }
        };

        var view = new google.visualization.DataView(data);
        view.setColumns([0,
                         1, { calc: "stringify", sourceColumn: 1, type: "string", role: "annotation" }]);

        var chart = new google.visualization.ColumnChart(document.getElementById('divSessionsChart'));

        chart.draw(view, options);
    }

    angular
        .module("MyApp.Controllers")
        .directive("googleChartsColumnSessions", function () {
            return {
                restrict: 'EAC',
                link: function (scope, element, attrs) {
                    //console.dir(element);

                    //google.setOnLoadCallback(drawChart(scope));
                    //element.html("It worked!");
                }
            };
        })
        .controller("MySessionsCtrl", mySessionsCtrl);

})(jQuery, angular);