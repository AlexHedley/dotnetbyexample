:: To run the C# script, use dotnet-script:
dotnet script Pointers.csx

:: Note: Unsafe code requires AllowUnsafeBlocks in the project settings.
:: For the .csx script, add #r pragma or run with --unsafe flag.
:: To run the F# script, use dotnet fsi:
dotnet fsi Pointers.fsx
