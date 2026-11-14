For Desktop application:
To quickly create update packages use the python scripts:
There are 4 versions:
For \bin\Debug
	create_update_debug_full.py
	create_update_debug_minimal.py

And \bin\Release\<publish_folder>
	create_update_publish_full.py
	create_update_publish_minimal.py

The full version includes all dlls (imported nugets)
The minimal version includes only the exe and dlls specific to the project
If no nuget updates have been made then the minimal version can be used safetly


Other info:
From the builds there are some specific runtimes for sqlite that are removed for linux,max,osx (see .csproj) to trim down the app size


Microsoft Store (MSIX):
Use Create-StorePackage.ps1 on Windows with the Windows SDK installed.
The package identity must match the app's reserved identity in Partner Center:

	PackageName: the Identity name from the Partner Center app identity
	Publisher: the exact Publisher value from the Partner Center app identity
	PublisherDisplayName: the publisher name shown in Partner Center

Example:
	.\Create-StorePackage.ps1 -PackageName "your.package.identity" -Publisher "CN=YourPublisherId" -PublisherDisplayName "Your publisher name"

The script publishes a self-contained x64 build and creates an unsigned .msix in AppPackages.
Microsoft signs MSIX packages after Store certification. Increase the package version for each Store update.