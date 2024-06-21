namespace Client.Models;

public enum ApiType
{
    GET,
    POST,
    PUT,
    PATCH,
    DELETE
}

public enum State
{
    // Assuming 0 = Unknown, 1 = Active, 2 = Inactive
    Unknown = 0,
    Active = 1,
    Inactive = 2
}

public enum Status
{
    // Assuming 0 = New, 1 = InProgress, 2 = Completed
    New = 0,
    InProgress = 1,
    Completed = 2
}
