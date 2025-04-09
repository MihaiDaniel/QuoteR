import os
import zipfile
import win32api  # Requires pywin32 package
from pathlib import Path

# Config
base_path = Path(r"D:\dev\QuoteR\Quoter\Quoter.App\bin\Debug\net6.0-windows")
exe_path = base_path / "Quoter.App.exe"
excluded_dirs = {"Collections", "Updater"}
excluded_files = {"Quoter.App.dll.config"}

# Get file version from .exe
def get_file_version(exe_file):
    info = win32api.GetFileVersionInfo(str(exe_file), "\\")
    ms = info['FileVersionMS']
    ls = info['FileVersionLS']
    version = f"{ms >> 16}.{ms & 0xFFFF}.{ls >> 16}.{ls & 0xFFFF}"
    return version

# Create zip archive
def create_zip(version):
    zip_name = f"{version}.zip"
    with zipfile.ZipFile(zip_name, 'w', zipfile.ZIP_DEFLATED) as zipf:
        for root, dirs, files in os.walk(base_path):
            rel_root = os.path.relpath(root, base_path)

            # Skip excluded folders
            dirs[:] = [d for d in dirs if d not in excluded_dirs]

            for file in files:
                if file in excluded_files:
                    continue

                full_path = Path(root) / file
                rel_path = os.path.relpath(full_path, base_path)
                zipf.write(full_path, arcname=rel_path)
                print(f"Added: {rel_path}")
    
    print(f"\n✅ Zip archive created: {zip_name}")

if __name__ == "__main__":
    version = get_file_version(exe_path)
    create_zip(version)