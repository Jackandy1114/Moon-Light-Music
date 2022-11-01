namespace Moon_Light_Music.Contracts.Services;

public interface IActivationService
{
    Task ActivateAsync(object activationArgs);
}
