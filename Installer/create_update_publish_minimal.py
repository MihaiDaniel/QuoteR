# -*- coding: utf-8 -*-
import os
import zipfile
import win32api  # pip install pywin32
from pathlib import Path

# Configuration
base_path = Path(r"D:\dev\QuoteR\Quoter\Quoter.App\bin\Release\net6.0-windows-publish")
exe_path = base_path / "Quoter.App.exe"

# Files to include explicitly
files_to_include = {
    "Quoter.App.exe",
    "Quoter.App.dll",
    "Quoter.Framework.dll",
    "Quoter.Shared.dll",
    "Quoter.App.deps.json",
    "Quoter.App.runtimeconfig.json"
}

# Folders to include fully (recursive)
folders_to_include = [
    "en-US",
    "fr-FR",
    "ro-RO"
]

# Get version from .exe file
def get_file_version(exe_file):
    info = win32api.GetFileVersionInfo(str(exe_file), "\\")
    ms = info['FileVersionMS']
    ls = info['FileVersionLS']
    version = f"{ms >> 16}.{ms & 0xFFFF}.{ls >> 16}.{ls & 0xFFFF}"
    return version

# Add files and folders to zip
def create_zip(version):
    zip_name = f"{version}.zip"
    with zipfile.ZipFile(zip_name, 'w', zipfile.ZIP_DEFLATED) as zipf:
        # Include specific files
        for file_name in files_to_include:
            full_path = base_path / file_name
            if full_path.exists():
                rel_path = full_path.relative_to(base_path)
                zipf.write(full_path, arcname=rel_path)
                print(f"✅ Added file: {rel_path}")
            else:
                print(f"⚠️ File not found: {file_name}")

        # Include folders
        for folder in folders_to_include:
            folder_path = base_path / folder
            if folder_path.exists():
                for root, _, files in os.walk(folder_path):
                    for file in files:
                        full_path = Path(root) / file
                        rel_path = full_path.relative_to(base_path)
                        zipf.write(full_path, arcname=rel_path)
                        print(f"ðﾟﾓﾁ Added from {folder}: {rel_path}")
            else:
                print(f"⚠️ Folder not found: {folder}")

    print(f"\n✅ Archive created: {zip_name}")

if __name__ == "__main__":
    version = get_file_version(exe_path)
    create_zip(version)
