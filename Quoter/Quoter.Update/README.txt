@2023 Dumitru Mihai Daniel

This updater takes a zip file and unzips it in the specified directory.

- Checks if it needs write (admin) privileges in the install directory. If yes, it will restart requesting higher
	privileges.
- It will stop the process of the application exe if it's running.
- It will create a backup of the install directory. If this fails the updater will stop.
- The zip containing the update will be unzipped in the install directory.
	- If an exception occurs the backup will be restored.
- It will restart the updated application

Expects at startup a list of arguments:
Example:
-i C:\My\Path to\install folder -e MyExeName -u C:\My\Path to\update.zip -r false

-i = install directory. This is where the zip will be extracted. All the contents of this folder will be
	copied for backups and if zip extraction fails backup will be restored.

-e = executable name. The name of the executable from which the version info will be taken

-u = update zip path. The full path to the .zip file containing the update. This zip will be unzipped
	in the install directory

-r = is restarted. Indicates if updater was restarted. Normally if the updater needs admin privileges to unzip the
	update it will restart with this as "true". This should be used only by updater, or if you do not want to restart
	the updater for some reason.
	Possible values:
		true
		false
