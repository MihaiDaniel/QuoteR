# .NET 6 to .NET 10 Upgrade Plan

## Overview

Upgrade the Quoter solution (`E:\projects\quoter\Quoter\Quoter.sln`) from .NET 6 to .NET 10 on working branch `upgrade-dotnet-10`, created from `dev/main`. All six SDK-style projects target net6.0 or net6.0-windows and move to net10.0 or net10.0-windows. Incompatible and recommended packages are updated, and build-breaking API changes are fixed inline. The assessment reports 289 NuGet packages (15 with recommended upgrades, 2 incompatible or deprecated), 33,579 lines of code, and roughly 13,000 API findings, mostly Windows Forms and System.Drawing compatibility signals. These findings guide the work, but source changes are driven by compiler errors and applicable breaking changes.

### Selected Strategy
**All-At-Once**: All projects upgraded simultaneously in a single operation.
**Rationale**: 6 projects, all on .NET 6, shallow dependency graph, no .NET Framework projects, no CI-green constraint.

## Upgrade Options

- **Strategy**: All-at-Once (recommended default)
- **Unsupported Packages**: Resolve Inline
- **Unsupported API Handling**: Fix Inline

## Tasks

### 01-prerequisites: Verify SDK and toolchain for .NET 10

Confirm that a .NET 10 SDK is installed and that no `global.json` in the repository pins an incompatible SDK. Check the installed SDK with `dotnet --list-sdks` and review any `global.json` under `E:\projects\quoter`. Confirm that the Windows Forms projects can build with Windows desktop support for net10.0-windows. Record the SDK version used for the upgrade.

**Done when**: A .NET 10 SDK is available, any `global.json` is compatible with it, and the SDK version is recorded in the task progress notes.

### 02-solution-upgrade: Upgrade all projects to .NET 10

Update the target framework of every project in Quoter.sln in one atomic pass: Quoter.Shared, Quoter.Framework, Quoter.Framework.Tests, and Quoter.Web move to net10.0. Quoter.App and Quoter.Update are Windows Forms projects and move to net10.0-windows. Update incompatible and recommended packages: Microsoft.AspNet.WebApi.Core 5.2.9 (incompatible) in Quoter.Web needs a replacement or removal. The EF Core 7.0.x packages (Microsoft.EntityFrameworkCore, Sqlite, Design, Tools, and related packages) move to 10.0.x. Microsoft.AspNetCore.Identity.* and Diagnostics.EntityFrameworkCore move to 10.0.x. Microsoft.VisualStudio.Web.CodeGeneration.Design moves to 10.0.3. System.Text.Json, Microsoft.Extensions.*, and System.Windows.Extensions move to 10.0.x. Newtonsoft.Json 13.0.1 moves to 13.0.4. xunit 2.4.2 (deprecated) in Quoter.Framework.Tests is replaced with a supported version. Quoter.Web carries the largest API and package risk because of its ASP.NET Core and Razor Pages surface. Quoter.App and Quoter.Update carry most of the Windows Forms and System.Drawing findings (about 12,840 and 138 respectively). Restore, then build the whole solution once and fix all compilation errors in a single bounded pass. Resolve breaking changes inline when the compiler reports them. Do not change behavior beyond what compatibility requires.

**Done when**: All six projects target net10.0 or net10.0-windows, no incompatible or deprecated packages remain, and `dotnet build Quoter.sln` completes with 0 errors.

### 03-final-validation: Validate the full solution

Run a full solution build and the test suite, then confirm the warning state of all modified projects. Summarize any deferred recommendations from the assessment, such as remaining package upgrades that were intentionally not applied, and note any runtime checks that could not be automated.

**Done when**: `dotnet build Quoter.sln` succeeds with 0 errors and no new warnings in modified projects, the test suite runs and its results are recorded, and deferred items are listed in the final report.
