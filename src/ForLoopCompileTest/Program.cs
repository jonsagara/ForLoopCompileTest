var length = 2;

// This for loop no longer compiles with recent versions of roslyn, even though the
//   code file from which it's extracted hasn't changed in 5 years:
for (int offset = 0, c1, c2; offset < length;)
{
    // snipped
}

// If there's only one extra, uninitialized variable declaration, it compiles:
for (int offset = 0, c1; offset < length;)
{
    // snipped
}

// When you add two or more uninitialized variable declarations, it does not compile:
for (int offset = 0, c1, c2, c3; offset < length;)
{
    // snipped
}

// Workaround: initialize the aforementioned uninitialized variables:
for (int offset = 0, c1 = 0, c2; offset < length;)
{
    // snipped
}

// -OR-

for (int offset = 0, c1 = 0, c2 = 0; offset < length;)
{
    // snipped
}

