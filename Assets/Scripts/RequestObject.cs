using System;

[Serializable]
public struct RequestObject : IEquatable<RequestObject>
{
    public string RequestType;
    public string Body;

    public RequestObject(string requestType, string body)
    {
        RequestType = requestType;
        Body = body;
    }

    public override bool Equals(object obj)
    {
        if (obj is RequestObject other)
        {
            return Equals(other);
        }

        return false;
    }

    public bool Equals(RequestObject other)
    {
        return RequestType == other.RequestType && Body == other.Body;
    }

    public override int GetHashCode()
    {
        unchecked // Overflow is fine, just wrap
        {
            int hash = (int)2166136261;
            // Suitable nullity checks etc, of course :)
            hash = (hash * 16777619) ^ (RequestType != null ? RequestType.GetHashCode() : 0);
            hash = (hash * 16777619) ^ (Body != null ? Body.GetHashCode() : 0);
            return hash;
        }
    }

    // Optional: Implement == and != operators
    public static bool operator ==(RequestObject left, RequestObject right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(RequestObject left, RequestObject right)
    {
        return !(left == right);
    }
}
