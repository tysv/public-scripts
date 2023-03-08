# public-scripts
bash scripts which are fun, or cool, but not necessarily productive.


#### brentry
  wrapper for the tool rentry which allows updating rentry.co/[url] sites from the command line. Mainly used to automatically generate and remember editcodes
#### cs
  local directory navigation. Essentially cs () { cd "$1" && ls; } with some extra features over time.
#### rmcarefully
  meant to be aliased to rm. will "archive" files in a hidden directory and allow restoration with rmcarefully --undo interface.  
#### scredit
  enables users to easily open and edit commonly accessed files from whatever working directory they are in.
#### scrfzf
  same as scredit, but rewrote to take advantage of fzf. Plans to combine the best parts of both.
#### strbuilder
  shell script where arbitrary length strings are built via descriptive syntax. Mainly used to create terminal width lines.
#### strbuilts
  access to some functions heavily utilizing strbuilder to make commonly required strings. Mainly used to create terminal width lines.
