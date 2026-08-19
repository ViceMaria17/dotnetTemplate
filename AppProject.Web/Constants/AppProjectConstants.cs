using System;

namespace AppProject.Web.Constants;

public class AppProjectConstants
{
    public const string ProjectName = "AppProject";

    public const string LocalStorageKeyPrefix = $"{ProjectName}";

    public const string DefaultLanguage = "en-US";

    public const string LanguageLocalStorageKey = $"{LocalStorageKeyPrefix}Language";
}
