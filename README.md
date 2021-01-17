# BnS Performance Fix

![Build](https://github.com/Sedro01/BnsPerformanceFix/workflows/Build/badge.svg)

A tool to optimize game performance for Blade & Soul.

Blade & Soul's poor performance is caused by a linear-time search for text localization. The game's localization file contains about 700,000 text strings. Many of these strings are for old, outdated content. In a party or raid setting, your game might search this file thousands of times per second.

This tool will strip out unnecessary text so the game has fewer strings to search through.

# Usage

1. Install LeaN's [SigBypasser](https://drive.google.com/file/d/1pjbL-4qqNDqGYfs0XKwzjwFb7K53-Vnj/view?usp=sharing)

1. Download [latest release](https://github.com/Sedro01/BnsPerformanceFix/releases/latest) of your choice

    - **Playable (Recommended):** Contains UI text, chat functionality, skill names, /commands and more

    - **Minimal:** Stripped down version only containing target hp, target range, F8 lobby #, /invite and /join

1. To install, replace your game's `local.dat` and `local64.dat` with the new files
    
    For a typical installation they are located here:

    `C:\Program Files (x86)\NCSOFT\BnS\contents\Local\NCWEST\ENGLISH\data`

    Make sure to back up your old files.

# Customization

To create a `local.dat` with your choice of text, first create a file with the translations you want to keep. Here's an example `filter.txt`:

```yml
# keep most UI text
UI.*
Text.*
Name.*

# only skill names for your class
Skill.Name2.ForceMaster_*
```

Alternatively, download and customize one of the [built-in filters](https://github.com/Sedro01/BnsPerformanceFix/tree/master/filters) such as [playable.txt](https://raw.githubusercontent.com/Sedro01/BnsPerformanceFix/master/filters/playable.txt).

Then run:

```
.\BnsPerformanceFix.exe -f filter.txt local.dat
.\BnsPerformanceFix.exe -f filter.txt local64.dat
```

The output will be named `local[64]-[filter-name].dat`.
To install, replace your game's `local[64].dat` with this file.

# Extracting local.dat

To see all the game's localization text:

1. Download the [latest release](https://github.com/Sedro01/BnsPerformanceFix/releases/latest) of BnsPerformanceFix which includes BnsDatTool
1. Use BnsDatTool "Dat Files" tab to extract `local.dat`
1. Use BnsDatTool "Bin Files" tab to extract `local.dat.files/localfile.bin`

The text will be output in `local.dat.files/localfile.bin.files/lookup_general.txt`

It will have aliases and text for all localizations:
```xml
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

# Filter syntax

A filter is a text file with a list of aliases.

- Patterns listed earlier in the file take priority
- Patterns are case-insensitive (`Name.*` and `name.*` are equivalent)
- The `*` wildcard matches zero or more characters
- The `?` wildcard matches any 1 character
- Lines beginning with `#` are treated as comments
- Lines beginning with `-` are exclusions
- Aliases which do not match any pattern will be excluded

## Examples

This filter will strip all text except those starting with the prefixes listed:

```yml
UI.*
Text.*
Name.*
```

This filter will delete item text and keep everything else:

```yml
-Item.*
*
```

See the [filters directory](https://github.com/Sedro01/BnsPerformanceFix/tree/master/filters) for more examples.
