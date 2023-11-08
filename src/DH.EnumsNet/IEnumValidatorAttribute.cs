namespace EnumsNET;
/// <summary>
/// Interface to be implemented on an enum validator attribute class to allow custom validation logic.
/// </summary>
/// <typeparam name="TEnum">The enum type.</typeparam>
public interface IEnumValidatorAttribute<TEnum>
    where TEnum : struct, Enum {
    /// <summary>
    /// Indicates if <paramref name="value"/> is valid.
    /// </summary>
    /// <param name="value">The enum value.</param>
    /// <returns>Indication if <paramref name="value"/> is valid.</returns>
    bool IsValid(TEnum value);
}