/// <summary>
/// Base interface for all services.
/// </summary>
public interface IService
{
    /// <summary>
    /// Called automatically when service is registered.
    /// </summary>
    void Initialize();
}
