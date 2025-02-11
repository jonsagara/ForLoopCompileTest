# `for` Loop Compilation Broken in Visual Studio 2022 17.13.0 Preview 2.0

After upgrading to the Visual Studio 2022 17.13.0 Preview 2.0 in December 2024, I could no longer compile my solution. The compilation error occurred on a `for` loop in file that has not changed in the past 5 years. The `for` loop itself has never changed.

Here is the `for` in question:

```csharp
var length = 2;

for (int offset = 0, c1, c2; offset < length;)
{
    // snipped
}
```

In the error list, you get the following:
```
; expected
The name 'c1' does not exist in the current context
; expected
The name 'c2' does not exist in the current context
Only assignment, call, increment, decrement, await, and new object expressions can be used as a statement
Syntax error, ',' expected
Only assignment, call, increment, decrement, await, and new object expressions can be used as a statement
Syntax error, ',' expected
Invalid expression term ')'
```

Interestingly, the compiler has no issue with a single uninitialized variable declaration. This compiles without error:

```csharp
for (int offset = 0, c1; offset < length;)
{
    // snipped
}
```

## Workarounds

If you provide an initial value to `c1`, the code compiles without error:

```csharp
for (int offset = 0, c1 = 0, c2; offset < length;)
{
    // snipped
}
```

The code also compiles if you provide initial values to both `c1` and `c2`:

```csharp
for (int offset = 0, c1 = 0, c2 = 0; offset < length;)
{
    // snipped
}
```


## Steps to Reproduce

First and foremost, clone this repository.

### Visual Studio 2022 17.13 or 17.14 Preview 1

1. Open `ForLoopCompileTest.sln` in Visual Studio 2022
2. `Build` -> `Rebuild Solution`

#### Expected results

The solution compiles successfully.

That for loop has been in our code since 2016, and, according to `git blame`, has never changed. I expect it to still compile.

#### Actual results

The solution fails to compile:

```
; expected
The name 'c1' does not exist in the current context
; expected
The name 'c2' does not exist in the current context
Only assignment, call, increment, decrement, await, and new object expressions can be used as a statement
Syntax error, ',' expected
Only assignment, call, increment, decrement, await, and new object expressions can be used as a statement
Syntax error, ',' expected
Invalid expression term ')'
; expected
The name 'c1' does not exist in the current context
; expected
The name 'c2' does not exist in the current context
Only assignment, call, increment, decrement, await, and new object expressions can be used as a statement
The name 'c3' does not exist in the current context
Only assignment, call, increment, decrement, await, and new object expressions can be used as a statement
Syntax error, ',' expected
Only assignment, call, increment, decrement, await, and new object expressions can be used as a statement
Syntax error, ',' expected
Invalid expression term ')'
```

## Environment

- Visual Studio 2022 17.13
- Visual Studio 2022 17.14.0 Preview 1.0
- .NET SDK 9.0.200
- Windows 11 Version 24H2 (OS Build 26100.3194)

## JetBrains Rider

The solution also fails to compile in JetBrains Rider 2024.3.5.

## Previous reports

[I reported this to VS Feedback](https://developercommunity.visualstudio.com/t/C-Compilation-Broken-in-Visual-Studio-2/10811093?port=1025&fsid=7c485b1e-c617-4201-8f56-671f9a37d1cb&ref=native&refTime=1739301926044&refUserId=6cc687d3-b6cc-4aee-9d0a-36df14375407) when I first encountered it.

