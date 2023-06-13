# Quoter updater

---

### Overview

This app is responsible for updating the main quoter application. Due to the fact
that we can have issues updating a running application, Quoter will start this app
that will handle updating by overwrinting the app's files from a zip.

This updater takes a zip file and unzips it in the specified directory.

- Checks if it needs write (admin) privileges in the install directory. If yes, it will restart requesting higher
	privileges.
- It will stop the process of the application exe if it's running.
- It will create a backup of the install directory. If this fails the updater will stop.
- The zip containing the update will be unzipped in the install directory.
	- If an exception occurs the backup will be restored.
- It will restart the newly updated application

### Arguments

Tha application expects at startup a list of arguments. All arguments are mandatory.

Example:

	-i <install folder> -e <quoter exe name> -u <path to zip> -uid <id> -s <true/false> -r <true/false>
	-i C:\My\Path to\install folder -e MyExeName -u C:\My\Path to\update.zip -uid 4 -s false -r false

**-i = install directory.** This is where the zip will be extracted. All the contents of this folder will be
	copied for backups and if zip extraction fails backup will be restored.

**-e = executable name.** The name of the executable of the application to update from which the version info will be taken

**-u = update zip path.** The full path to the .zip file containing the update. This zip will be unzipped
	in the install directory

**-uid = update id.** The id of the update as is in the application Quoter database. Not used at the moment

**-s = is silent.** Indicates if the updater will update the application silently (the update form will not be shown)

Possible values:
- true
- false

**-r = is restarted.** Indicates if updater was restarted. Normally if the updater needs admin privileges to unzip the
	update it will restart with this as "true". This should be used only by updater, or if you do not want to restart
	the updater for some reason.

Possible values:
- true
- false


###### @2023 Dumitru Mihai Daniel