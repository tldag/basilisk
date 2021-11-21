
Set-Location $PSScriptRoot

Get-ChildItem -Path "." -Filter "TestOutput" -ErrorAction Ignore | ForEach-Object { Remove-Item $_ -Recurse }
Get-ChildItem -Path "." -Filter "TestResults" -ErrorAction Ignore | ForEach-Object { Remove-Item $_ -Recurse }

$projects = Get-ChildItem -Path "." -Filter "*.csproj" -Recurse

$projects | ForEach-Object {

    $directory = $_.Directory
    $binDirectory = Join-Path $directory "bin"
    $binDirectory = New-Object System.IO.DirectoryInfo $binDirectory
    $objDirectory = Join-Path $directory "obj"
    $objDirectory = New-Object System.IO.DirectoryInfo $objDirectory
    
    if ($binDirectory.Exists) { $binDirectory.Delete($true) }
    if ($objDirectory.Exists) { $objDirectory.Delete($true) }
}

dotnet build -c Release basilisk.sln

if ($IsWindows)
{
    $principal = New-Object Security.Principal.WindowsPrincipal([Security.Principal.WindowsIdentity]::GetCurrent())
    $isAdmin = $principal.IsInRole([Security.Principal.WindowsBuiltInRole]::Administrator)

    $basiliskDir = Join-Path (Get-Location).Drive.Root "basilisk"
    $basiliskDir = New-Object System.IO.DirectoryInfo $basiliskDir
    $basiliskTargetFile = Join-Path $basiliskDir.FullName "Basilisk.Gallery.exe"
    $basiliskTargetFile = New-Object System.IO.FileInfo $basiliskTargetFile

    if ($isAdmin)
    {
        $service = Get-Service "Basilisk.Gallery" -ea Ignore
        
        if ($service)
        {
            Stop-Service "Basilisk.Gallery" -PassThru
            Remove-Service "Basilisk.Gallery"
        }

        if ($basiliskTargetFile.Exists)
        {
            Remove-Item -Path $basiliskTargetFile.FullName
            $basiliskTargetFile = New-Object System.IO.FileInfo $basiliskTargetFile.FullName
        }
    }

    if ($basiliskDir.Exists)
    {
        
        if (-not $basiliskTargetFile.Exists)
        {
            $basiliskSourceFile = Join-Path (Get-Location) "Basilisk.Gallery" "bin" "Release" "net6.0" "win-x64" "publish" "Basilisk.Gallery.exe"
            $basiliskSourceFile = New-Object System.IO.FileInfo $basiliskSourceFile
            $basiliskSourceFile.CopyTo($basiliskTargetFile.FullName)
        }

        if ($isAdmin)
        {
            New-Service -Name "Basilisk.Gallery" -BinaryPathName $basiliskTargetFile.FullName -StartupType Automatic
            Start-Service -Name "Basilisk.Gallery" -PassThru
        }
    }
}
