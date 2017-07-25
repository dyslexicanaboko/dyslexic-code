//Need to learn how to pass in a custom object as parameters
//For now the parameters are hard coded
function getTaskPoolService() {
    //console.log("devTasksFactory");

    return {
        tasks: {
            getAll: function () {
                //console.log("devTasksFactory.getGraphData.userId:" + $rootScope.userId);

                return axios.get(apiTasks("")); //GET at root
            },
            save: function (obj) {
                //devTask.UserId = $rootScope.userId;
                //console.log("taskSave");
                //console.dir(task);

                if (obj.TaskId === 0) {
                    //Initialized to be an empty array
                    obj.TaskGroupLinks = [];

                    //This is going to get overridden on the server side, but it cannot be null going over
                    obj.CreatedOn = new Date();

                    //If there is no TaskId then add
                    return axios.post(apiTasks(""), obj); //POST at root
                }
                else {
                    //If there is a TaskId then update
                    return axios.put(apiTasks(obj.TaskId), obj); //PUT at Id
                }
            },
            delete: function (id) {
                return axios.delete(apiTasks(id));
            }
        },
        taskGroups: {
            getAll: function () {
                //console.log("devTasksFactory.getGraphData.userId:" + $rootScope.userId);

                return axios.get(apiTaskGroups("")); //GET at root
            },
            save: function (obj) {
                //devTask.UserId = $rootScope.userId;
                //console.log("taskSave");
                //console.dir(task);

                if (obj.TaskGroupId === 0) {
                    //Initialized to be an empty array
                    obj.TaskGroupLinks = [];

                    //This is going to get overridden on the server side, but it cannot be null going over
                    obj.CreatedOn = new Date();

                    //If there is no TaskGroupId then add
                    return axios.post(apiTaskGroups(""), obj); //POST at root
                }
                else {
                    //If there is a TaskGroupId then update
                    return axios.put(apiTaskGroups(obj.TaskGroupId), obj); //PUT at Id
                }
            },
            delete: function (id) {
                return axios.delete(apiTaskGroups(id));
            }
        },
        getSummaryGridData: function (dateRange) {
            return axios.get(getBasePath("GetTaskSummary") + getFilterQsp($rootScope.userId, dateRange));
        },
        getTaskIdByTfsDevelopmentTaskId: function (tfsDevTaskId) {
            return axios.get(getBasePath("GetTaskIdByTfsDevelopmentTaskId") + "?UserId=" + $rootScope.userId + "&tfsDevTaskId=" + tfsDevTaskId);
        },
        getTaskLoaded: function (devTaskId) {
            return axios.get(singleArgument("GetTaskLoaded", devTaskId));
        },
        sessionSave: function (devTaskSession) {
            return axios.put(getBasePath("SessionSave"), devTaskSession);
        },
        getTaskSessions: function (devTaskId) {
            return axios.get(singleArgument("GetTaskSessions", devTaskId));
        }
    };

    //api/TaskGroups
}

function setupObject(task) {
    task.TaskGroupLinks = [];
    task.TaskId = 0;
    task.CreatedOn = new Date(); //This is going to get overridden

    return task;
}

function getFilterQsp(userId, dateRange) {
    //"?UserId=" + $rootScope.userId + "&IsActive=&DateStart=2015-10-01&DateEnd=2015-10-31"
    //console.log("getFilterQsp");
    //console.dir(dateRange);

    return new String().concat(
        "?UserId=" + userId,
        "&IsActive=",
        "&DateStart=" + dateRange.start.toISOString(),
        "&DateEnd=" + dateRange.end.toISOString());
}

function getResult(q, promise) {
    var deferred = q.defer();

    setTimeout(function () {
        deferred.resolve(promise);
    }, 3000);

    return deferred.promise;
}

function singleArgument(action, argument) {
    return getBasePath(action) + "/" + argument;
}

function apiTaskGroups(action) {
    return "/api/TaskGroups/" + action;
}

function apiTasks(action) {
    return "/api/Tasks/" + action;
}

function getBasePath(action) {
    return "/api/Tasks/" + action;
}