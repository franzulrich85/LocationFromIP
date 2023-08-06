using System.Runtime.Serialization;

namespace LocationFromIP.Application.Enums
{
    public enum ValidationErrorCode
    {
        [EnumMember(Value = "Field.Invalid")]
        FieldValueInvalid,
        [EnumMember(Value = "Field.Missing")]
        FieldMissing,
        [EnumMember(Value = "Field.InvalidQuery")]
        InvalidQuery,
    }
}
