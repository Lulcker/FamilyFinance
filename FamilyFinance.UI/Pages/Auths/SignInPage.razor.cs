using FamilyFinance.DTO.Auths.RequestModels;
using FamilyFinance.ProxyApiMethods.ApiMethods;
using FamilyFinance.UI.Contracts;
using Microsoft.AspNetCore.Components;

namespace FamilyFinance.UI.Pages.Auths;

/// <summary>
/// Страница входа в аккаунт
/// </summary>
public partial class SignInPage(
    AuthsApiHelper authsApiHelper,
    IUserSession userSession
    ) : ComponentBase
{
    #region Fields
    
    private string email = string.Empty;
    
    private string password = string.Empty;
    
    private bool isLoading;
    
    #endregion
    
    #region Properties
    
    private bool IsLoginButtonDisabled => string.IsNullOrEmpty(email) ||
                                          string.IsNullOrEmpty(password) ||
                                          isLoading;
    
    #endregion
    
    #region Methods

    private async Task LoginAsync()
    {
        isLoading = true;

        try
        {
            var result = await authsApiHelper.LoginAsync(new LoginUserRequestModel
            {
                Email = email.Trim(),
                Password = password
            });

            await userSession.StartSession(result);
        }
        finally
        {
            isLoading = false;
            StateHasChanged();
        }
    }

    #endregion
}