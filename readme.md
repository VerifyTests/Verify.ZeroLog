# <img src="/src/icon.png" height="30px"> Verify.ZeroLog

[![Discussions](https://img.shields.io/badge/Verify-Discussions-yellow?svg=true&label=)](https://github.com/orgs/VerifyTests/discussions)
[![Build status](https://ci.appveyor.com/api/projects/status/3oa1pc52a38ncma6?svg=true)](https://ci.appveyor.com/project/SimonCropp/verify-zerolog)
[![NuGet Status](https://img.shields.io/nuget/v/Verify.ZeroLog.svg)](https://www.nuget.org/packages/Verify.ZeroLog/)

Extends [Verify](https://github.com/VerifyTests/Verify) to allow verification of [ZeroLog](https://github.com/Abc-Arbitrage/ZeroLog) bits.<!-- singleLineInclude: intro. path: /docs/intro.include.md -->


**See [Milestones](../../milestones?state=closed) for release notes.**


## Sponsors


### Entity Framework Extensions<!-- include: zzz. path: /docs/zzz.include.md -->

[Entity Framework Extensions](https://entityframework-extensions.net/?utm_source=simoncropp&utm_medium=Verify.ZeroLog) is a major sponsor and is proud to contribute to the development this project.

[![Entity Framework Extensions](https://raw.githubusercontent.com/VerifyTests/Verify.ZeroLo/refs/heads/main/docs/zzz.png)](https://entityframework-extensions.net/?utm_source=simoncropp&utm_medium=Verify.ZeroLog)<!-- endInclude -->


## NuGet

 * https://nuget.org/packages/Verify.ZeroLog


## Usage

<!-- snippet: Enable -->
<a id='snippet-Enable'></a>
```cs
[ModuleInitializer]
public static void Initialize() =>
    VerifyZeroLog.Initialize();
```
<sup><a href='/src/Tests/ModuleInitializer.cs#L3-L9' title='Snippet source file'>snippet source</a> | <a href='#snippet-Enable' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

<!-- snippet: Usage -->
<a id='snippet-Usage'></a>
```cs
[Fact]
public Task Usage()
{
    Recording.Start();
    var result = Method();

    return Verify(result);
}

static string Method()
{
    var logger = LogManager.GetLogger<Tests>();
    logger.Error("The error");
    logger.Warn("The warning");
    return "Result";
}
```
<sup><a href='/src/Tests/Tests.cs#L3-L22' title='Snippet source file'>snippet source</a> | <a href='#snippet-Usage' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

Results in:

<!-- snippet: Tests.Usage.verified.txt -->
<a id='snippet-Tests.Usage.verified.txt'></a>
```txt
{
  target: Result,
  log: [
    {
      Error: The error,
      Logger: Tests
    },
    {
      Warn: The warning,
      Logger: Tests
    }
  ]
}
```
<sup><a href='/src/Tests/Tests.Usage.verified.txt#L1-L13' title='Snippet source file'>snippet source</a> | <a href='#snippet-Tests.Usage.verified.txt' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->
