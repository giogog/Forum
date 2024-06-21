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
    Pending = 0,
    Show = 1,
    Hide = 2
}

public enum Status 
{ 
    Active = 0,
    Inactive = 1
}
