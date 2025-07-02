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