if(($args.length -eq 0) -or ($args[0].indexof(".fs") -eq -1))
{
    "You must specify a file with a .fs extension"
}
else
{
    $file_name = $args[0].substring(2, $args[0].indexof(".fs")-2)

    iex "fsc $file_name.fs -a /reference:testing.dll"
    if(Test-Path ".\$file_name.exe")
    {
        iex ".\$file_name.exe"
        iex "del $file_name.exe"
    }
}