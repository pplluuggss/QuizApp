# Script: fix-image-names-targeted.ps1
# Purpose: Rename images under QuizApp\Resources\Images so filenames are valid for MAUI Resizetizer.

$imagesPath = Join-Path $PSScriptRoot '..\QuizApp\Resources\Images' | Resolve-Path -ErrorAction SilentlyContinue
if (-not $imagesPath) {
    $imagesPath = 'C:\Users\kompo\source\repos\QuizApp\QuizApp\Resources\Images'
}

if (-not (Test-Path $imagesPath)) {
    Write-Error "Images folder not found: $imagesPath"
    exit 1
}

$extensions = @('*.png','*.jpg','*.jpeg','*.svg','*.gif','*.webp')
Get-ChildItem -Path $imagesPath -Recurse -File -Include $extensions | ForEach-Object {
    $file = $_
    $ext = $file.Extension.ToLower()
    $name = [System.IO.Path]::GetFileNameWithoutExtension($file.Name)

    # to lower
    $new = $name.ToLower()

    # replace invalid characters with underscore (only allow a-z, 0-9 and underscore)
    $new = ($new -replace '[^a-z0-9_]','_')

    # ensure starts with a letter
    if ($new -notmatch '^[a-z]') { $new = 'a' + $new }

    # ensure ends with a letter
    if ($new -notmatch '[a-z]$') { $new = $new + 'a' }

    $newName = "$new$ext"
    $target = Join-Path $file.DirectoryName $newName

    if ($target -eq $file.FullName) {
        Write-Output "OK: $($file.Name)"
        continue
    }

    # avoid collisions by appending numeric suffix
    $i = 1
    while (Test-Path $target) {
        $target = Join-Path $file.DirectoryName ("{0}_{1}{2}" -f $new, $i, $ext)
        $i++
    }

    Write-Output "Renaming: $($file.Name) -> $(Split-Path $target -Leaf)"
    try {
        Move-Item -LiteralPath $file.FullName -Destination $target
    } catch {
        Write-Error "Failed to rename $($file.FullName): $_"
    }
}
Write-Output "Done."
