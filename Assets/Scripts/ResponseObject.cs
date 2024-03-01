using System;

[Serializable]
public class ResponseObject
{
    public int CodeStatus;
    public RequestObject Request;
    public string Body;

    public ResponseObject(RequestObject requestObject, string body, int codeStatus)
    {
        CodeStatus = codeStatus;
        Request = requestObject;
        Body = body;
    }
}