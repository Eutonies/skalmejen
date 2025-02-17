﻿using Microsoft.AspNetCore.Components;
using Skalmejen.Common.Session;
using Skalmejen.UI.Components.Graphics;

namespace Skalmejen.UI.Components;

public partial class WelcomeComponent
{
    [CascadingParameter]
    public SkalmejenSession Session { get; set; }

    private bool IsAuthenticated => Session.AuthenticatedUser != null;

    private string _joinText = "";
    public string JoinText { 
        get => _joinText;
        set
        {
            _joinText = value ?? "";
            VerifyJoinCode();
            InvokeAsync(StateHasChanged);
        }
    }


    private string _userName = "";
    public string UserName
    {
        get => _userName;
        set
        {
            _userName = value ?? "";
            VerifyUserName();
            InvokeAsync(StateHasChanged);
        }
    }

    private bool _joinCodeIsOk = false;
    private bool _userNameIsOk = false;
    private void VerifyJoinCode()
    {
        _joinCodeIsOk = _joinText.Length > 5;
    }

    private void VerifyUserName()
    {
        _userNameIsOk = _userName.Length > 2;
    }

    private bool DisableJoinButton => !(_joinCodeIsOk && _userNameIsOk);




}
