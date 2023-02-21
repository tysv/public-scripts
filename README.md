# public-scripts
A public repository for useful bash scripts I wrote. Do not use in production environments without thoroughly vetting code yourself!
Any of these scripts might have strbuilder as a requirement

Details:
brentry     - Wrapper for the rentry.co tool found here: https://github.com/radude/rentry. Grew out of a desire  to edit rentry sites without entering an edit code every time.
cs          - Started as 'cs () { cd "$1" && ls; }' but has grown into more features based on fuzzy finding and shortcut movement.
noty        - Simple way to create and reference short notes on the commandline.
rmcarefully - Built to be aliased to "rm" -- rmcarefully --undo will allow easy file recovery. Does not act identically to rm, and not without side effects.
scredit     - Allows easy editing of files found in any pre-specified directory, from any working directory you happen to be in. Use flags to open files in different editors.
scrfzf      - Nearly same behavior as scredit rewritten to leverage fzf as the fuzzy search. Scredit and scrfzf will eventually merge.
strbuilder  - Built to produce strings by specifying length/repetition, colors, and characters to print. 
strbuilts   - Library of commonly created strbuilder functions (e.g. terminal width line of dash characters)
