# <img src="/src/icon.png" height="30px"> Verify.ZeroLog

[![Build status](https://ci.appveyor.com/api/projects/status/bgvkfjn26l5b4kba?svg=true)](https://ci.appveyor.com/project/SimonCropp/verify-zerolog)
[![NuGet Status](https://img.shields.io/nuget/v/Verify.ZeroLog.svg)](https://www.nuget.org/packages/Verify.ZeroLog/)

Extends [Verify](https://github.com/VerifyTests/Verify) to allow verification of [ZeroLog](https://github.com/Abc-Arbitrage/ZeroLog) bits.



## NuGet package

https://nuget.org/packages/Verify.ZeroLog/


## Usage

<!-- snippet: Enable -->
<a id='snippet-enable'></a>
```cs
[ModuleInitializer]
public static void Initialize() =>
    VerifyZeroLog.Enable();
```
<sup><a href='/src/Tests/ModuleInitializer.cs#L3-L9' title='Snippet source file'>snippet source</a> | <a href='#snippet-enable' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

<!-- snippet: Usage -->
<a id='snippet-usage'></a>
```cs
[Fact]
public Task Usage()
{
    RecordingLogger.Start();
    var result = Method();

    return Verify(result);
}

static string Method()
{
    var logger = LogManager.GetLogger<Tests>();
    logger.Error("The Message");
    return "Result";
}
```
<sup><a href='/src/Tests/Tests.cs#L7-L23' title='Snippet source file'>snippet source</a> | <a href='#snippet-usage' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

Results in:

<!-- snippet: Tests.Usage.verified.txt -->
<a id='snippet-Tests.Usage.verified.txt'></a>
```txt
{
  target: Result,
  logs: [
    {
      Message: The Message,
      Level: Error,
      Logger: Tests,
      KeyValues: {}
    }
  ]
}
```
<sup><a href='/src/Tests/Tests.Usage.verified.txt#L1-L11' title='Snippet source file'>snippet source</a> | <a href='#snippet-Tests.Usage.verified.txt' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->
