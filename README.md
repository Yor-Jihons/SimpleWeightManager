# SimpleWeightManager 1.0.0

The product to visualize own weight transition, in order to keep motivation for weight-managing.

## 1. Requirements

- dotnet 6.0.202
- Windows OS
- [NLog - NuGet Gallery](https://www.nuget.org/packages/NLog/)
- [Microsoft.Win32.SystemEvents 6.0.1](https://www.nuget.org/packages/Microsoft.Win32.SystemEvents)
- [ScottPlot 4.1.45](https://www.nuget.org/packages/ScottPlot/4.1.45)
- [ScottPlot.Wpf 4.1.45](https://www.nuget.org/packages/ScottPlot.WPF/5.0.0-beta)
- [System.Drawing.Common 6.0.0](https://www.nuget.org/packages/System.Drawing.Common)

## 2. Install

Step 1. Run the bat-file init.bat.

```
$ init
```
Step 2. Run the bat-file commpile.bat with a command-line argument.
You can pass the arguments { "debug" | "release" | "publish" }.
You can also run dotnet as usual.

```
$ compile publish
```

or like

```
$ dotnet publish -o .\bin\Publish -c Release --self-contained true -r win-x64 -nologo
```

## 3. Usage

Step 1. Run the exe-file SimpleWeightManager.exe.

```
$ SimpleWeightManager
```

Step 2. Click the menu item "操作(O)" in order to go step 3.

Step 3. Select the menu item "データの追加(A)" in order to add the weight data.

Step 4. Input your data.

Step 5. Click the button "登録".

## 4. Licenses

This library is released under the MIT License.

[NLog - NuGet Gallery](https://www.nuget.org/packages/NLog/) is under the BSD-3-Clause license.

The image files in the directory "main/res/Frames" are downloaded from [GAHAG | 著作権フリー写真・イラスト素材集](https://gahag.net/), and under the [CC0](https://creativecommons.org/share-your-work/public-domain/cc0) License.

The Icon file are downloaded from [icon-icons.com](https://icon-icons.com/ja/%E3%82%A2%E3%82%A4%E3%82%B3%E3%83%B3/%E3%82%A2%E3%83%97%E3%83%AA/129133), and under the CC Atribution.

[Microsoft.Win32.SystemEvents 6.0.1](https://www.nuget.org/packages/Microsoft.Win32.SystemEvents) is under the MIT License.

[ScottPlot 4.1.45](https://www.nuget.org/packages/ScottPlot/4.1.45) is under the MIT License.

[ScottPlot.Wpf 4.1.45](https://www.nuget.org/packages/ScottPlot.WPF/5.0.0-beta) is under the MIT License.

[System.Drawing.Common 6.0.0](https://www.nuget.org/packages/System.Drawing.Common) is under the MIT License.

## 5. Development Environment

- dotnet on Windows
- Language: C#
- Framework: WPF

## 6. Changes

## 7. Contact

Author: Yor-Jihons  
GitHub: [SimpleWeightManager](https://github.com/Yor-Jihons/SimpleWeightManager)  
