using System;
using System.Collections.Generic;

namespace Morgue.Models;

public partial class Request
{
    public Request(int requestId, int statusId, int? executorId, DateOnly creationDate, string? title)
    {
        RequestId = requestId;
        StatusId = statusId;
        ExecutorId = executorId;
        CreationDate = creationDate;
        Title = title;
    }

    public int RequestId { get; set; }

    public int StatusId { get; set; }

    public int? ExecutorId { get; set; }

    public DateOnly CreationDate { get; set; }

    public string? Title { get; set; }

    public virtual User? Executor { get; set; }

    public virtual Status Status { get; set; } = null!;
}
