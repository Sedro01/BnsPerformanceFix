# BnS Performance Fix

A tool to optimize game performance for Blade & Soul.

Blade & Soul's poor performance is caused by a linear-time search for text localization. The game's localization file contains about 700,000 text strings. Many of these strings are for old, outdated content. In a party or raid setting, your game might search this file thousands of times per second.

This tool will strip out unnecessary text using a filter of your choice.

# Usage

The output from this tool is unsigned. You will need LeaN's [SigBypasser](https://drive.google.com/file/d/1pjbL-4qqNDqGYfs0XKwzjwFb7K53-Vnj/view?usp=sharing) to use it.

1. Download the [latest release](https://github.com/Sedro01/BnsPerformanceFix/releases).

1. Find your `local.dat` file in your install directory.

    For example, the 32-bit file for English language should be located here:

    `C:\Program Files (x86)\NCSOFT\BnS\contents\Local\NCWEST\ENGLISH\data\local.dat`

1. Re-build your `local.dat` using a [custom filter](#filter-syntax)

    ```
    .\BnsPerformanceFix.exe --filter custom.txt local.dat
    ```

    Replace your game's `local.dat` with the new `local-custom.dat`.


# Customization

## Extracting local.dat

To see all the game's localization text,

1. Download the [latest release](https://github.com/Sedro01/BnsPerformanceFix/releases).
1. Use BnsDatTool "Dat Files" tab to extract `local.dat`
1. Use BnsDatTool "Bin Files" tab to extract `local.dat.files/localfile.bin`

The text will be output in `local.dat.files/localfile.bin.files/lookup_general.txt`

It will have aliases and text for all localizations:
```
<alias>
Name.AccountLevel_Name_001
</alias>
<text>
1
</text>
<alias>
Name.AccountLevel_Name_002
</alias>
<text>
2
</text>
<alias>
Name.AccountLevel_Name_003
</alias>
<text>
3
</text>
...
```

## Filter syntax

- Filters listed earlier in the file take priority
- Filters are case-insensitive
- The `*` wildcard matches zero or more characters
- The `?` wildcard matches any 1 character
- Lines beginning with `#` are treated as comments
- Lines beginning with `-` are exclusions

## Example filters

This filter will strip all text except those starting with the prefixes listed:

```
UI.*
Text.*
Name.*
```

This filter will delete item text and keep everything else:
```
-Item.*
*
```
