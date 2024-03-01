using UnityEngine;

public class RawResponse
{
    public readonly string responseType;
    public readonly long requestId;
    public readonly byte[] data;

    public RawResponse(string responseType, long requestId, byte[] data)
    {
        this.responseType = responseType;
        this.requestId = requestId;
        this.data = data;
    }
}
