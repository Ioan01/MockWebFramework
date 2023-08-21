using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockWebFramework.Http
{
    public enum HttpStatusCode
    {
        Continue = 100,
        SwitchingProtocols = 101,
        Processing = 102,
        EarlyHints = 103,
        OK = 200,
        Created = 201,
        Accepted = 202,
        NonAuthoritativeInformation = 203,
        NoContent = 204,
        ResetContent = 205,
        PartialContent = 206,
        MultiStatus = 207,
        AlreadyReported = 208,
        IMUsed = 226,
        MultipleChoices = 300,
        MovedPermanently = 301,
        Found = 302,
        SeeOther = 303,
        NotModified = 304,
        UseProxy = 305,
        TemporaryRedirect = 307,
        PermanentRedirect = 308,
        BadRequest = 400,
        Unauthorized = 401,
        PaymentRequired = 402,
        Forbidden = 403,
        NotFound = 404,
        MethodNotAllowed = 405,
        NotAcceptable = 406,
        ProxyAuthenticationRequired = 407,
        RequestTimeout = 408,
        Conflict = 409,
        Gone = 410,
        LengthRequired = 411,
        PreconditionFailed = 412,
        PayloadTooLarge = 413,
        URITooLong = 414,
        UnsupportedMediaType = 415,
        RangeNotSatisfiable = 416,
        ExpectationFailed = 417,
        ImATeapot = 418,
        MisdirectedRequest = 421,
        UnprocessableEntity = 422,
        Locked = 423,
        FailedDependency = 424,
        TooEarly = 425,
        UpgradeRequired = 426,
        PreconditionRequired = 428,
        TooManyRequests = 429,
        RequestHeaderFieldsTooLarge = 431,
        UnavailableForLegalReasons = 451,
        InternalServerError = 500,
        NotImplemented = 501,
        BadGateway = 502,
        ServiceUnavailable = 503,
        GatewayTimeout = 504,
        HTTPVersionNotSupported = 505,
        VariantAlsoNegotiates = 506,
        InsufficientStorage = 507,
        LoopDetected = 508,
        NotExtended = 510,
        NetworkAuthenticationRequired = 511
        // You can continue adding more response codes here
    }



    public static class HttpStatusNames
    {
        private static readonly Dictionary<HttpStatusCode, string> _responseCodesDictionary = new Dictionary<HttpStatusCode, string>
    {
        { HttpStatusCode.Continue, "Continue" },
        { HttpStatusCode.SwitchingProtocols, "Switching Protocols" },
        { HttpStatusCode.Processing, "Processing" },
        { HttpStatusCode.EarlyHints, "Early Hints" },
        { HttpStatusCode.OK, "OK" },
        { HttpStatusCode.Created, "Created" },
        { HttpStatusCode.Accepted, "Accepted" },
        { HttpStatusCode.NonAuthoritativeInformation, "Non-Authoritative Information" },
        { HttpStatusCode.NoContent, "No Content" },
        { HttpStatusCode.ResetContent, "Reset Content" },
        { HttpStatusCode.PartialContent, "Partial Content" },
        { HttpStatusCode.MultiStatus, "Multi-Status" },
        { HttpStatusCode.AlreadyReported, "Already Reported" },
        { HttpStatusCode.IMUsed, "IM Used" },
        { HttpStatusCode.MultipleChoices, "Multiple Choices" },
        { HttpStatusCode.MovedPermanently, "Moved Permanently" },
        { HttpStatusCode.Found, "Found" },
        { HttpStatusCode.SeeOther, "See Other" },
        { HttpStatusCode.NotModified, "Not Modified" },
        { HttpStatusCode.UseProxy, "Use Proxy" },
        { HttpStatusCode.TemporaryRedirect, "Temporary Redirect" },
        { HttpStatusCode.PermanentRedirect, "Permanent Redirect" },
        { HttpStatusCode.BadRequest, "Bad Request" },
        { HttpStatusCode.Unauthorized, "Unauthorized" },
        { HttpStatusCode.PaymentRequired, "Payment Required" },
        { HttpStatusCode.Forbidden, "Forbidden" },
        { HttpStatusCode.NotFound, "Not Found" },
        { HttpStatusCode.MethodNotAllowed, "Method Not Allowed" },
        { HttpStatusCode.NotAcceptable, "Not Acceptable" },
        { HttpStatusCode.ProxyAuthenticationRequired, "Proxy Authentication Required" },
        { HttpStatusCode.RequestTimeout, "Request Timeout" },
        { HttpStatusCode.Conflict, "Conflict" },
        { HttpStatusCode.Gone, "Gone" },
        { HttpStatusCode.LengthRequired, "Length Required" },
        { HttpStatusCode.PreconditionFailed, "Precondition Failed" },
        { HttpStatusCode.PayloadTooLarge, "Payload Too Large" },
        { HttpStatusCode.URITooLong, "URI Too Long" },
        { HttpStatusCode.UnsupportedMediaType, "Unsupported Media Type" },
        { HttpStatusCode.RangeNotSatisfiable, "Range Not Satisfiable" },
        { HttpStatusCode.ExpectationFailed, "Expectation Failed" },
        { HttpStatusCode.ImATeapot, "I'm a teapot" },
        { HttpStatusCode.MisdirectedRequest, "Misdirected Request" },
        { HttpStatusCode.UnprocessableEntity, "Unprocessable Entity" },
        { HttpStatusCode.Locked, "Locked" },
        { HttpStatusCode.FailedDependency, "Failed Dependency" },
        { HttpStatusCode.TooEarly, "Too Early" },
        { HttpStatusCode.UpgradeRequired, "Upgrade Required" },
        { HttpStatusCode.PreconditionRequired, "Precondition Required" },
        { HttpStatusCode.TooManyRequests, "Too Many Requests" },
        { HttpStatusCode.RequestHeaderFieldsTooLarge, "Request Header Fields Too Large" },
        { HttpStatusCode.UnavailableForLegalReasons, "Unavailable For Legal Reasons" },
        { HttpStatusCode.InternalServerError, "Internal Server Error" },
        { HttpStatusCode.NotImplemented, "Not Implemented" },
        { HttpStatusCode.BadGateway, "Bad Gateway" },
        { HttpStatusCode.ServiceUnavailable, "Service Unavailable" },
        { HttpStatusCode.GatewayTimeout, "Gateway Timeout" },
        { HttpStatusCode.HTTPVersionNotSupported, "HTTP Version Not Supported" },
        { HttpStatusCode.VariantAlsoNegotiates, "Variant Also Negotiates" },
        { HttpStatusCode.InsufficientStorage, "Insufficient Storage" },
        { HttpStatusCode.LoopDetected, "Loop Detected" },
        { HttpStatusCode.NotExtended, "Not Extended" },
        { HttpStatusCode.NetworkAuthenticationRequired, "Network Authentication Required" }
    };

        public static string GetStatusCodeName(HttpStatusCode statusCode)
        {
            return _responseCodesDictionary.TryGetValue(statusCode, out var description) ? description : "Unknown";
        }
    }


    
}
