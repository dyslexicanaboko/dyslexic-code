using System;

namespace TaskListPoolWebApi.Models
{
    public class TaskListModel
    {
        public int TaskGroupId { get; set; }

        public bool RenderJsBundles { get; set; } = true;

        public bool RenderJsController { get; set; } = true;
    }
}