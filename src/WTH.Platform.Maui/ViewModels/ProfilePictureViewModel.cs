using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Options;
using WTH.Platform.Maui.Messages;
using Volo.Abp.Account;
using Volo.Abp.Content;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Http.Client;

namespace WTH.Platform.Maui.ViewModels;

public partial class ProfilePictureViewModel : PlatformViewModelBase, ITransientDependency
{
    protected IAccountAppService AccountAppService { get; }
    protected IOptions<AbpRemoteServiceOptions> RemoteServiceOptions { get; }

    public string? AbpAccountPublicUrl => RemoteServiceOptions.Value.RemoteServices
        .GetConfigurationOrDefaultOrNull("AbpAccountPublic")?.BaseUrl.TrimEnd('/');

    public string? ProfilePictureUrl => $"{AbpAccountPublicUrl}/api/account/profile-picture-file/{CurrentUser.Id}";

    [ObservableProperty]
    private ProfilePictureType selectedProfilePictureType;

    [ObservableProperty]
    private ImageSource profilePictureImageSource = default!;

    [ObservableProperty]
    bool isBusy;

    string? temporaryFilePath = default!;

    public ProfilePictureViewModel(IAccountAppService accountAppService, IOptions<AbpRemoteServiceOptions> remoteServiceOptions)
    {
        AccountAppService = accountAppService;
        RemoteServiceOptions = remoteServiceOptions;
    }

    [RelayCommand]
    async Task GetProfilePicture()
    {
        ProfilePictureImageSource = ImageSource.FromUri(new Uri(ProfilePictureUrl!));

        var response = await AccountAppService.GetProfilePictureAsync(CurrentUser.Id!.Value);

        SelectedProfilePictureType = response.Type;
    }

    [RelayCommand]
    void SetPhotoUrl()
    {
        ProfilePictureImageSource = ImageSource.FromUri(new Uri(ProfilePictureUrl!));
    }

    [RelayCommand]
    async Task TakePhoto()
    {
        if (MediaPicker.Default.IsCaptureSupported)
        {
            var photo = await MediaPicker.Default.CapturePhotoAsync();

            if (photo is not null)
            {
                // save the file into local storage
                string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

                using Stream sourceStream = await photo.OpenReadAsync();
                using FileStream localFileStream = File.OpenWrite(localFilePath);

                await sourceStream.CopyToAsync(localFileStream);

                temporaryFilePath = localFilePath;
                ProfilePictureImageSource = localFilePath;
                SelectedProfilePictureType = ProfilePictureType.Image;
            }
        }
    }

    [RelayCommand]
    async Task ChoosePhoto()
    {
        var photo = await MediaPicker.Default.PickPhotoAsync();

        if (photo is not null)
        {
            string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

            using Stream sourceStream = await photo.OpenReadAsync();
            using FileStream localFileStream = File.OpenWrite(localFilePath);

            temporaryFilePath = localFilePath;
            ProfilePictureImageSource = localFilePath;
            SelectedProfilePictureType = ProfilePictureType.Image;
        }
    }

    [RelayCommand]
    async Task Save()
    {
        var input = new ProfilePictureInput
        {
            Type = SelectedProfilePictureType,
        };

        if (SelectedProfilePictureType == ProfilePictureType.Image)
        {
            input.ImageContent = new RemoteStreamContent(File.OpenRead(temporaryFilePath!));
        }

        try
        {
            IsBusy = true;
            await AccountAppService.SetProfilePictureAsync(input);

            await Shell.Current.GoToAsync("..");

            WeakReferenceMessenger.Default.Send(new ProfilePictureChangedMessage(ProfilePictureUrl!));
        }
        catch (AbpRemoteCallException ex)
        {
            HandleException(ex);
        }
        finally
        {
            IsBusy = false;
        }
    }
}
