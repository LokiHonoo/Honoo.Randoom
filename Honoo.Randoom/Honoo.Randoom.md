# Honoo.Randoom

- [Honoo.Randoom](#honoorandoom)
  - [INTRODUCTION](#introduction)
  - [GUIDE](#guide)
    - [GitHub](#github)
  - [USAGE](#usage)
  - [LICENSE](#license)

## INTRODUCTION

Just a random.

## GUIDE

### GitHub

<https://github.com/LokiHonoo/Honoo.Randoom/>

## USAGE

```c#

private static void Main()
{
    using (var randoom = new Randoom(seedBytes, SHA512.Create()))
    {
      // Create int.
      randoom.Next();
      // Create double.
      randoom.NextDouble();
      // Create string from char token group.
      randoom.NextString(60, 'm');
      // Create string from mask.
      randoom.NextString("h[8]{-}h[4]{-}h[4]{-}h[4]{-}h[12]");
    }
}

```

## LICENSE

[MIT](LICENSE) license.
