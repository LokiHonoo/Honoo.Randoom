# Honoo.Randoom

- [Honoo.Randoom](#honoorandoom)
  - [INTRODUCTION](#introduction)
  - [GUIDE](#guide)
    - [NuGet](#nuget)
  - [USAGE](#usage)
  - [LICENSE](#license)

## INTRODUCTION

Just a random.

## GUIDE

### NuGet

<https://www.nuget.org/packages/Honoo.Randoom/>

## USAGE

```c#

private static void Main()
{
    using (var randoom = new Randoom(null, SHA512.Create()))
    {
      // int
      randoom.Next();
      // double
      randoom.NextDouble();
      // char token group
      randoom.NextString(60, 'm');
      // char mask
      randoom.NextString("+mmmmm(-)mmmmm(-)mmmmm(-)mmmmm(-)mmmmm");
    }
}

```

## LICENSE

[MIT](LICENSE) license.
