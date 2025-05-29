namespace Raven.Core.Libraries.Enums
{
    /// <summary>
    /// Represents the status of a one-time password (OTP).
    /// </summary>
    /// <remarks>This enumeration is used to indicate whether an OTP has been used, remains unused, or has
    /// expired.</remarks>
    public enum OtpStatus
    {
        USED,

        UNUSED,

        EXPIRED
    }
}
