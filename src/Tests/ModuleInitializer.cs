public static class ModuleInitializer
{
    #region Enable

    [ModuleInitializer]
    public static void Initialize() =>
        VerifyZeroLog.Enable();

    #endregion

    [ModuleInitializer]
    public static void InitializeOther() =>
        VerifyDiffPlex.Initialize();
}