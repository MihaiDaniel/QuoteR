# 03-final-validation: Validate the full solution

Run a full solution build and the test suite, then confirm the warning state of all modified projects. Summarize any deferred recommendations from the assessment, such as remaining package upgrades that were intentionally not applied, and note any runtime checks that could not be automated.

**Done when**: `dotnet build Quoter.sln` succeeds with 0 errors and no new warnings in modified projects, the test suite runs and its results are recorded, and deferred items are listed in the final report.
