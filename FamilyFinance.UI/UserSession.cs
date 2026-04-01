using System.Security.Claims;
using Blazored.LocalStorage;
using FamilyFinance.DTO.Auths.ResponseModels;
using FamilyFinance.DTO.Expenses.RequestModels;
using FamilyFinance.UI.Contracts;
using FamilyFinance.UI.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace FamilyFinance.UI;

public class UserSession(
    ILocalStorageService localStorageService,
    NavigationManager navigationManager
    ) : AuthenticationStateProvider, IUserSession
{
    #region Consts

    private const string UserIdKey = "family-finance.user-id";
    private const string TokenKey = "family-finance.token";
    private const string NameKey = "family-finance.name";
    private const string EmailKey = "family-finance.email";
    private const string ExcludeCategoryIdsKey = "family-finance.exclude-category-ids";
    private const string ExpenseDraftsKey = "family-finance.expense-drafts";

    #endregion

    #region Properties
    
    public Guid UserId { get; set; }

    public string Token { get; set; } = null!;
    
    public string Name { get; set; } = null!;
    
    public string Email { get; set; } = null!;

    public IReadOnlyCollection<Guid> ExcludeCategoryIds { get; set; } = [];

    public IReadOnlyCollection<AddExpenseRequestModel> ExpenseDrafts { get; set; } = [];

    #endregion

    #region Fields

    private bool isSessionStarted;

    #endregion

    #region Methods
    
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        await LoadSession();

        if (isSessionStarted)
        {
            var authorize = new ClaimsIdentity("Authorize");
            return new AuthenticationState(new ClaimsPrincipal(authorize));
        }

        var anonymous = new ClaimsIdentity();
        return new AuthenticationState(new ClaimsPrincipal(anonymous));
    }

    public async Task StartSession(AuthorizeUserResponseModel responseModel)
    {
        await localStorageService.SetItemAsync(UserIdKey, responseModel.UserId);
        await localStorageService.SetItemAsync(TokenKey, responseModel.Token);
        await localStorageService.SetItemAsync(NameKey, responseModel.Name);
        await localStorageService.SetItemAsync(EmailKey, responseModel.Email);
        await localStorageService.SetItemAsync(ExcludeCategoryIdsKey, ExcludeCategoryIds);
        await localStorageService.SetItemAsync(ExpenseDraftsKey, ExpenseDrafts);
        
        navigationManager.NavigateTo("/");
        
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public async Task EndSession()
    {
        await localStorageService.RemoveItemAsync(UserIdKey);
        await localStorageService.RemoveItemAsync(TokenKey);
        await localStorageService.RemoveItemAsync(NameKey);
        await localStorageService.RemoveItemAsync(EmailKey);
        await localStorageService.RemoveItemAsync(ExcludeCategoryIdsKey);
        await localStorageService.RemoveItemAsync(ExpenseDraftsKey);
        
        navigationManager.NavigateTo("/sign-in");
        
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public async Task SetExcludeCategoryIds(IReadOnlyCollection<Guid> excludeCategoryIds)
    {
        ExcludeCategoryIds = excludeCategoryIds;
        await localStorageService.SetItemAsync(ExcludeCategoryIdsKey, ExcludeCategoryIds);
    }

    public async Task SetExpenseDrafts(IReadOnlyCollection<AddExpenseRequestModel> expenseDrafts)
    {
        ExpenseDrafts = expenseDrafts;
        await localStorageService.SetItemAsync(ExpenseDraftsKey, ExpenseDrafts);
    }

    #endregion

    #region Private Methods

    private async Task LoadSession()
    {
        if ((await localStorageService.GetItemAsStringAsync(TokenKey)).IsNotEmpty())
        {
            await LoadUserData();
            isSessionStarted = true;
        }
        else
        {
            ClearUserData();
            isSessionStarted = false;
        }
    }
    
    private async Task LoadUserData()
    {
        UserId = await GetGuidItemAsync(UserIdKey);
        Token = await GetStringItemAsync(TokenKey);
        Name = await GetStringItemAsync(NameKey);
        Email = await GetStringItemAsync(EmailKey);
        ExcludeCategoryIds = await localStorageService.GetItemAsync<IReadOnlyCollection<Guid>?>(ExcludeCategoryIdsKey) ?? [];
        ExpenseDrafts = await localStorageService.GetItemAsync<IReadOnlyCollection<AddExpenseRequestModel>?>(ExpenseDraftsKey) ?? [];
    }

    private void ClearUserData()
    {
        UserId = Guid.Empty;
        Token = null!;
        Name = null!;
        Email = null!;
        ExcludeCategoryIds = [];
        ExpenseDrafts = [];
    }
    
    private async Task<string> GetStringItemAsync(string key) =>
        (await localStorageService.GetItemAsStringAsync(key))!.Trim('"');

    private async Task<Guid> GetGuidItemAsync(string key) =>
        await localStorageService.GetItemAsync<Guid>(key);

    #endregion
}