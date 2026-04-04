namespace _Project.Code.Scripts.Data.TaskData
{
    public static class TaskResultTypeExtensions
    {
        public static string ToDisplayString(this TaskResultType type) => type switch
        {
            TaskResultType.Chair => "Chair",
            TaskResultType.Sofa  => "Sofa",
            TaskResultType.Table => "Table",
            _                    => "—"
        };
    }
}
