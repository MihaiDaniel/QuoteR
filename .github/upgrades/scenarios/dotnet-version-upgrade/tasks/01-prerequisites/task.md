# 01-prerequisites: Verify SDK and toolchain for .NET 10

Confirm that a .NET 10 SDK is installed and that no `global.json` in the repository pins an incompatible SDK. Check the installed SDK with `dotnet --list-sdks` and review any `global.json` under `E:\projects\quoter`. Confirm that the Windows Forms projects can build with Windows desktop support for net10.0-windows. Record the SDK version used for the upgrade.

**Done when**: A .NET 10 SDK is available, any `global.json` is compatible with it, and the SDK version is recorded in the task progress notes.
